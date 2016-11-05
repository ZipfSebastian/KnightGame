package thread;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.j256.ormlite.support.ConnectionSource;
import constants.Properties;
import server.Router;
import server.ServerVariables;
import logger.Log;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.Socket;
import java.nio.charset.Charset;

/**
 * Created by zipfs on 2015. 12. 19..
 * All client has on of this thread what receive and send messages
 */
public class ClientThread extends Thread {

    private Socket socket;
    private boolean run;
    private BufferedReader inFromClient;
    private long sleepTime;
    private Router router;
    private long lastUpdateTime;
    private boolean isOnline;


    public ClientThread(Socket socket,Router router, ConnectionSource connectionSource, long sleepTime) {
        this.socket = socket;
        this.run = true;
        this.sleepTime = sleepTime;
        this.router = router;
        try {
            this.inFromClient = new BufferedReader(new InputStreamReader(socket.getInputStream()));
        } catch (IOException e) {
            Log.write(e);
        }
    }

    @Override
    public void run() {
        super.run();
        Log.write("Client connected!");
        isOnline = true;
        lastUpdateTime = System.currentTimeMillis();
        while (run) {
            try {
                receive();
                Thread.sleep(sleepTime);
                if(Integer.parseInt(ServerVariables.getValue(Properties.PROP_TIME_OUT))>0) {
                    timeOutIfNoResponse();
                }
            } catch (Exception e) {
                Log.write(e);
            }
        }
    }

    private void timeOutIfNoResponse() {

        if(System.currentTimeMillis() > lastUpdateTime
                + Integer.parseInt(ServerVariables.getValue(Properties.PROP_TIME_OUT))){
            shutDown();
            Log.write("Client time out!");
        }
    }

    private void receive() throws IOException {
        if(socket.isConnected() && socket.getInputStream()!= null && !socket.isClosed()) {
            int avaible = socket.getInputStream().available();
            if (avaible > 0) {
                lastUpdateTime = System.currentTimeMillis();
                String clientSentence = inFromClient.readLine();
                if(!clientSentence.contains("PingRequest") && !clientSentence.contains("MoveRequest")) {
                    System.out.println("REC: " + clientSentence);
                }
                router.onRecive(clientSentence, this);
            }
        }
    }

    public void send(String data) throws IOException {
        byte[] first = (data + System.getProperty("line.separator")).getBytes(Charset.forName("UTF-8"));
        socket.getOutputStream().write(first);
        System.out.println("SEN: "+data);
    }

    public void shutDown() {
        try {
            isOnline = false;
            inFromClient.close();
            socket.close();
        } catch (IOException e) {
            Log.write(e);
        }finally {
            run = false;
        }
    }

    public boolean isRuning() {
        return run;
    }

    public Router getRouter() {
        return router;
    }

    public boolean isOnline() {
        return isOnline;
    }

    public void setIsOnline(boolean isOnline) {
        this.isOnline = isOnline;
    }
}
