package helpers;

import models.Client;

import java.util.*;
import java.util.concurrent.ConcurrentMap;
import java.util.concurrent.LinkedBlockingDeque;

/**
 * Created by zipfs on 2015. 12. 19..
 */
public class Session {


    private static int sessionLength = 10;
    private static Map<String, Client> users;
    private static List<String> sessions;


    public static void Init(){
        users = Collections.synchronizedMap(new LinkedHashMap());
        sessions = new ArrayList<String>();
    }

    public static String generateSession(){
        String session = "";
        for(int i = 0;i<sessionLength;i++){
            Random r = new Random();
            session+=r.nextInt(9);

        }
        for(String savedSession : sessions){
            if(session.equals(savedSession)){
                session = generateSession();
            }
        }
        sessions.add(session);
        return  session;
    }

    public static Client getUserWithSession(String session){
        return users.get(session);
    }

    /**
     * @param user add a user to session
     * @return
     */
    public static String addUser(Client user){
        for(Client client : users.values()){
            if(client.getUser().getEmail().equals(user.getUser().getEmail())){
                client.getClientThread().shutDown();
            }
        }
        String session = generateSession();
        users.put(session,user);
        return session;
    }

    public static void setSessionLength(int length){
        sessionLength = length;
    }

    public static int getSessionLength() {
        return sessionLength;
    }

    public static Map<String,Client> getUsers() {
        return users;
    }
}
