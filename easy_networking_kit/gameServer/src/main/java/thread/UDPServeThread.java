package thread;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.j256.ormlite.support.ConnectionSource;
import helpers.Session;
import interfaces.RequestHandler;
import logger.Log;
import models.Client;
import models.SystemUser;
import server.Router;

import java.io.*;
import java.net.*;
import java.nio.charset.Charset;
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
    private DatagramSocket socket;
    public static DatagramSocket udpSocket;

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
        try {
            socket = new DatagramSocket(udpPort);
            udpSocket = socket;
        }catch (Exception e){
            Log.write(e);
        }
    }

    @Override
    public void run() {
        super.run();
        while(run) {
            try {
                byte[] data = new byte[bufferSize];
                DatagramPacket packet = new DatagramPacket(data,0,bufferSize);
                socket.receive(packet);
                InputStream stream = new ByteArrayInputStream(packet.getData());
                BufferedReader in = new BufferedReader(new InputStreamReader(stream));
                String message = in.readLine();
                InetAddress IPAddress = packet.getAddress();
                int port = packet.getPort();

                Log.write("RECV UDP: " + message);

                try {
                    RequestHandler request = new ObjectMapper().readValue(message, RequestHandler.class);
                    Client user = Session.getUserWithSession(request.getSession());
                    if(user!=null){

                        router.onRecive(message, null);
                        user.setInetAddress(IPAddress);
                        user.setUdpPort(port);
                    }
                }catch (Exception e){
                    send("BAD_REQUEST",IPAddress, port);
                }
                Thread.sleep(sleepTime);
            }catch (Exception e) {
                if(!e.getMessage().equals("socket closed")) {
                    Log.write(e);
                }
            }
        }
    }

    public static void send(String data, InetAddress inetAddress, int udpPort){
        byte[] first = (data + System.getProperty("line.separator")).getBytes(Charset.forName("UTF-8"));
        DatagramPacket packet = new DatagramPacket(first,first.length, inetAddress,udpPort);
        try {
            udpSocket.send(packet);
            Log.write("SENT UDP: "+data);
        } catch (IOException e) {
            Log.write(e);
        }
        if(!data.contains("PositionResponse") && !data.contains("BAD")) {
            System.out.println("SEN: " + data);
        }
    }

    public static void send(String data, Client client){
        byte[] first = (data + System.getProperty("line.separator")).getBytes(Charset.forName("UTF-8"));
        DatagramPacket packet = new DatagramPacket(first,first.length,client.getInetAddress(),client.getUdpPort());
        try {
            udpSocket.send(packet);
            Log.write("SENT UDP: "+data);
        } catch (IOException e) {
            Log.write(e);
        }
        if(!data.contains("PositionResponse") && !data.contains("BAD")) {
            System.out.println("SEN: " + data);
        }
    }

    public boolean isRun() {
        return run;
    }
}
