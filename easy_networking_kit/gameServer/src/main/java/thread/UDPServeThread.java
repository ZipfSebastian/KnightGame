package thread;

import com.j256.ormlite.support.ConnectionSource;
import helpers.Session;
import logger.Log;
import models.Client;
import models.SystemUser;
import server.Router;

import java.io.*;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by sinemissione on 2016.06.24..
 */
public class UDPServeThread extends Thread{

    private boolean run;
    private int udpPort;
    private Router router;
    private long sleepTime;
    private int bufferSize;

    /*
    Initialize the serve thread
     */
    public void Init(int udpPort,Router router, int bufferSize,long sleepTime){
        Session.Init();
        this.router = router;
        this.sleepTime = sleepTime;
        this.udpPort = udpPort;
        this.run = true;
        this.bufferSize = bufferSize;
    }

    @Override
    public void run() {
        super.run();
        while(run) {
            try {
                /*
                DatagramSocket socket = new DatagramSocket(udpPort);
                byte[] data = new byte[bufferSize];
                DatagramPacket packet = new DatagramPacket(data,0,bufferSize);
                socket.receive(packet);
                InputStream stream = new ByteArrayInputStream(packet.getData());
                BufferedReader in = new BufferedReader(new InputStreamReader(stream));
                String message = in.readLine();
                Request request = Request.fromJson(message);
                Client user = Session.getUserWithSession(request.getSession());
                user.setUdpSocket(socket);
                router.onRecive(message, this);
                Thread.sleep(sleepTime);*/
            }catch (Exception e) {
                if(!e.getMessage().equals("socket closed")) {
                    Log.write(e);
                }
            }
        }
    }

    public boolean isRun() {
        return run;
    }
}
