package handlers;

import com.fasterxml.jackson.databind.ObjectMapper;
import constants.CommunicationConstants;
import constants.Constants;
import controllers.Game;
import controllers.GameManagger;
import helpers.Session;
import interfaces.RequestHandler;
import logger.Log;
import models.Client;
//import responses.NameResponse;
import responses.SearchResponse;
import java.util.List;

/**
 * Created by Kesze on 2016.08.27..
 */
public class SearchRequest extends RequestHandler {
    private boolean state;
    public static final int minimalPlayer = 1;

    @Override
    public void onRecive(String request) {
        super.onRecive(request);
        Client client = Session.getUserWithSession(session);
        if(state == true)
        {
            MatchQueue.addClient(client);
        }
        else{
            MatchQueue.removePlayer(client);
        }
        SearchResponse searchResponse = new SearchResponse();
        searchResponse.setSuccess(true);
        searchResponse.setType(CommunicationConstants.SEARCH_RESPONSE);
        if(MatchQueue.waitingPlayersNumber()> minimalPlayer)
        {
            searchResponse.setMatchFind(true);
            List<Client> currentUsers = MatchQueue.getPlayers(minimalPlayer);
            try {
                for(int i=0;i<currentUsers.size();i++){
                    send(new ObjectMapper().writeValueAsString(searchResponse), Constants.TCP, currentUsers.get(i));
                    send(new ObjectMapper().writeValueAsString(searchResponse), Constants.TCP, client);
                }
                Game game = new Game();
                int index = GameManagger.addGame(game);
                game.init(currentUsers, index,connectionSource);
                game.start();
                for(int i=0; i<currentUsers.size();i++) {
                    MatchQueue.removePlayer(currentUsers.get(i));
                }
            }catch (Exception e){
                Log.write(e);
            }
            for(int i=0; i<currentUsers.size();i++) {
                MatchQueue.removePlayer(currentUsers.get(i));
            }
        }
        else
        {
            if(isState() == false)
            {
                try {
                    send(new ObjectMapper().writeValueAsString(searchResponse), Constants.TCP, client);
                }
                catch (Exception e)
                {
                    Log.write(e);
                }
            }
            else {
                searchResponse.setMatchFind(false);
                try {
                    send(new ObjectMapper().writeValueAsString(searchResponse), Constants.TCP, client);
                } catch (Exception e) {
                    Log.write(e);
                }
            }
        }


    }

    public boolean isState() {
        return state;
    }

    public void setState(boolean state) {
        this.state = state;
    }
}
