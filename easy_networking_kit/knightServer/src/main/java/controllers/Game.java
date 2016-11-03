package controllers;

import logger.Log;
import models.Client;

import java.util.List;

/**
 * Created by Kesze on 2010.01.12..
 */
public class Game extends Thread {

    private int gameID;
    private List<Client> inGameClients;
    private boolean run; // amikor elindul a j�t�k true-ra �ll�tsd ut�na le�ll�t�sn�l fals-ra

    public void init (List<Client> currentUsers, int gameID) {
        inGameClients = currentUsers; //�thelyezz�k a currentusersb�l a playereket
        this.gameID = gameID; //�tadjuk a gameID-t
        for(Client client : currentUsers)
        {
            client.setGameID(gameID); //be�ll�tjuk az adott usernek a gameid-j�t
        }
        run = true;
    }

    public void clientLoaded(Client client){

    }

    //..... mindenf�le requestre csin�lsz egy met�dust ami kezeli....


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
