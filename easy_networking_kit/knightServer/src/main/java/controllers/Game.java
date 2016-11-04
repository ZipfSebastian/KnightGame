package controllers;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.j256.ormlite.dao.Dao;
import com.j256.ormlite.dao.DaoManager;
import com.j256.ormlite.support.ConnectionSource;
import logger.Log;
import models.Client;
import models.Map;
import models.SpawnPoint;
import responses.InitResponse;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

/**
 * Created by Kesze on 2010.01.12..
 */
public class Game extends Thread {

    private int gameID;
    private List<Client> inGameClients;
    private boolean run;
    private List<SpawnPoint> spawnPoints;
    private int clientLoaded = 0;

    public void init (List<Client> currentUsers, int gameID, ConnectionSource connectionSource) {
        try{
            Dao<SpawnPoint,String> spawnPointDao = DaoManager.createDao(connectionSource,SpawnPoint.class);
            Dao<Map,String> mapDao = DaoManager.createDao(connectionSource,Map.class);
            List<Map> maps = mapDao.queryForAll();
            Random r = new Random();
            int randomMapID = maps.get(r.nextInt(maps.size()-1)).getId();
            spawnPoints = spawnPointDao.queryBuilder().where().eq(SpawnPoint.MAP_ID,randomMapID).query();
            inGameClients = currentUsers;
            this.gameID = gameID;
            for(Client client : currentUsers)
            {
                client.setGameID(gameID);
                //cli
            }
            run = true;
        }catch (Exception e){

        }
    }

    public void clientLoaded(Client client){
        try {
            clientLoaded++;
            if (clientLoaded == inGameClients.size()) {

            } else {
                InitResponse response = new InitResponse();

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
                sleep(10);
            } catch (InterruptedException e) {
                Log.write(e);
            }
        }
    }

    public boolean isRun() {
        return run;
    }

    public void setRun(boolean run) {
        this.run = run;
    }

    public List<Client> getInGameClients() {
        return inGameClients;
    }

    public void setInGameClients(List<Client> inGameClients) {
        this.inGameClients = inGameClients;
    }
}
