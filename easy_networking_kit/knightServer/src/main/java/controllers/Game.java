package controllers;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.j256.ormlite.dao.Dao;
import com.j256.ormlite.dao.DaoManager;
import com.j256.ormlite.support.ConnectionSource;
import constants.CommunicationConstants;
import logger.Log;
import models.*;
import responses.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

/**
 * Created by Kesze on 2010.01.12..
 */
public class Game extends Thread {

    private int gameID;
    private List<Player> inGameClients = new ArrayList<Player>();
    private boolean run;
    private List<SpawnPoint> spawnPoints;
    private int clientLoaded = 0;

    public void init (List<Client> currentUsers, int gameID, ConnectionSource connectionSource) {
        try{
            Dao<SpawnPoint,String> spawnPointDao = DaoManager.createDao(connectionSource,SpawnPoint.class);
            Dao<Map,String> mapDao = DaoManager.createDao(connectionSource,Map.class);
            List<Map> maps = mapDao.queryForAll();
            Random r = new Random();
            int randomMapID = maps.get(r.nextInt(maps.size())).getId();
            spawnPoints = spawnPointDao.queryBuilder().where().eq(SpawnPoint.MAP_ID,randomMapID).query();

            this.gameID = gameID;
            int i = 0;
            for(Client client : currentUsers)
            {
                Player player = new Player();
                player.setClient(client);
                player.setSpawnPoint(spawnPoints.get(i));
                player.setPosition(new Vector2(spawnPoints.get(i).getX(),spawnPoints.get(i).getY()));
                player.setId(i);
                inGameClients.add(player);
                LoadGameResponse response = new LoadGameResponse();
                response.setMapID(randomMapID);
                response.setType(CommunicationConstants.LOAD_GAME_RESPONSE);
                Log.write(player.getClient().getUser().getName());
                player.getClient().getClientThread().send(new ObjectMapper().writeValueAsString(response));
                i++;
            }

            Log.write("GameServerStarted!");
        }catch (Exception e){
            Log.write(e);
        }
    }

    public void clientLoaded(Client client){
        try {
            clientLoaded++;
            Log.write("ClientLoaded");
            if (clientLoaded == inGameClients.size()) {
                for(Player player : inGameClients){
                    StartGameResponse response = new StartGameResponse();
                    List<Enemy> enemies = new ArrayList<Enemy>();
                    for(Player enemyPlayer : inGameClients){
                        if(enemyPlayer != player){
                            Enemy enemy = new Enemy();
                            enemy.setId(enemyPlayer.getId());
                            enemy.setPosition(enemyPlayer.getPosition());
                            enemies.add(enemy);
                        }
                    }
                    response.setType(CommunicationConstants.START_GAME_RESPONSE);
                    response.setEnemyList(enemies);
                    response.setPosition(player.getPosition());
                    player.getClient().getClientThread().send(new ObjectMapper().writeValueAsString(response));

                }
                run = true;
                this.start();
            } else {
                InitResponse response = new InitResponse();
                response.setType(CommunicationConstants.INIT_RESPONSE);
                client.getClientThread().send(new ObjectMapper().writeValueAsString(response));
            }
        }catch (Exception e){
            Log.write(e);
        }
    }


    @Override
    public void run() {
        super.run();
        while(run)
        {
            try {
                sleep(100);
                sendPositions();
            } catch (InterruptedException e) {
                Log.write(e);
            }
        }
    }

    private void sendPositions() {
        try {
            for (Player player : inGameClients) {
                for (Player enemy : inGameClients) {
                    if (player != enemy && player.getClient().isOnline()) {
                        PositionResponse positionResponse = new PositionResponse();
                        positionResponse.setType(CommunicationConstants.POSITION_RESPONSE);
                        positionResponse.setId(enemy.getId());
                        positionResponse.setNewPosition(enemy.getPosition());
                        player.getClient().getClientThread().send(new ObjectMapper().writeValueAsString(positionResponse));
                    }
                }
            }
        }catch(Exception e){
            run =  false;
            for (Player player : inGameClients) {
                GameEndResponse endResponse = new GameEndResponse();
                endResponse.setType(CommunicationConstants.GAME_END_RESPONSE);
                endResponse.setPoints(100);
                endResponse.setVictory(false);
                try {
                    if(player.getClient().isOnline()) {
                        player.getClient().getClientThread().send(new ObjectMapper().writeValueAsString(endResponse));
                    }
                }catch (Exception ex){
                    Log.write(ex);
                }
            }
        }
    }

    public boolean isRun() {
        return run;
    }

    public void setRun(boolean run) {
        this.run = run;
    }

    public List<Player> getInGameClients() {
        return inGameClients;
    }

    public void moveRequest(Client client, Vector2 newPosition) {
        for(Player player : inGameClients){
            if(player.getClient() == client){
                player.setPosition(newPosition);
            }

        }
    }
}
