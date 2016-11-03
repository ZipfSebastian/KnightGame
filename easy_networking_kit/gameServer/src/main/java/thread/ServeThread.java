package thread;

import com.j256.ormlite.support.ConnectionSource;
import helpers.Session;
import logger.Log;
import server.Router;

import java.io.IOException;
import java.net.DatagramSocket;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by zipfs on 2015. 12. 19..
 * This thread wait for connections and create a client thread to communicate with client
 */
public class ServeThread extends Thread {

    private boolean run;
    private ServerSocket welcomeSocket;
    private ConnectionSource connectionSource;
    private List<ClientThread> runningThreads;
    private long sleepTime;
    private long clientSleepTime;
    private Router router;

    /*
    Initialize the serve thread
     */
    public void Init(int tcpPort, ConnectionSource connectionSource, Router router, long sleepTime, long clientSleepTime){
        try {
            Session.Init();
            this.sleepTime = sleepTime;
            this.clientSleepTime = clientSleepTime;
            this.runningThreads = new ArrayList<ClientThread>();
            this.connectionSource = connectionSource;
            this.run = true;
            this.welcomeSocket = new ServerSocket(tcpPort);
            this.router = router;
        } catch (IOException e) {
            Log.write(e);
        }
    }

    @Override
    public void run() {
        super.run();
        while(run) {
            try {
                //Log.write("Run!");
                checkClients();
                if(welcomeSocket != null) {
                    Socket socket = welcomeSocket.accept();
                    runningThreads.add(new ClientThread(socket,router, connectionSource, clientSleepTime));
                    runningThreads.get(runningThreads.size() - 1).start();
                    Log.write("Connection on server thread!" + socket.getInetAddress().getHostAddress());
                }
                Thread.sleep(sleepTime);
            }catch (Exception e) {
                if(!e.getMessage().equals("socket closed")) {
                    Log.write(e);
                }
            }
        }
    }

    /*
    Remove the stopped client threads
     */
    private void checkClients() {
        for(int i=0;i<runningThreads.size();i++){
            if(!runningThreads.get(i).isRuning()){
                runningThreads.remove(i);
            }
        }
    }

    /*
    Stop the waiting to connections
     */
    public void shutDown(){
        for(ClientThread clientThread : runningThreads){
            clientThread.shutDown();
        }
        this.run = false;
        try {
            welcomeSocket.close();
        } catch (IOException e) {
            Log.write(e);
        }
    }

    public boolean isRun() {
        return run;
    }
}
