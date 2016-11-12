package server;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.j256.ormlite.support.ConnectionSource;
import constants.Constants;
import handlers.LoginRequest;
import helpers.Session;
import interfaces.RequestHandler;
import interfaces.Reciver;
import logger.Log;
import models.Client;
import thread.ClientThread;
import thread.UDPServeThread;

/**
 * Created by zipfs on 2016. 06. 03..
 * Route the messages to the processor classes
 */
public class Router extends Reciver {

    public Router(ConnectionSource connectionSource) {
        super(connectionSource);
    }

    @Override
    public void onRecive(String message, ClientThread thread) {


        try {
            RequestHandler requestHandler = new ObjectMapper().readValue(message, RequestHandler.class);
            if(requestHandler.getSession()!= null && requestHandler.getSession() != ""){
                Client client = Session.getUserWithSession(requestHandler.getSession());
                if(thread != null) {
                    client.setClientThread(thread);
                }
            }
            requestHandler.setConnectionSource(connectionSource);
            if(thread !=null) {
                requestHandler.setThread(thread);
            }
            requestHandler.onRecive(message);
        }catch (Exception e){
            try {
                Client client = new Client();
                if(thread !=null) {
                    client.setClientThread(thread);
                }
                send(Constants.BAD_REQUEST + "", Constants.TCP, client);
            }catch(Exception e1){
                Log.write(e);
            }
        }
    }
}
