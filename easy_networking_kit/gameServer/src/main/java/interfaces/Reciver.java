package interfaces;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonTypeInfo;
import com.j256.ormlite.support.ConnectionSource;
import constants.Constants;
import models.Client;
import thread.ClientThread;
import thread.UDPServeThread;

import java.io.IOException;
import java.net.DatagramPacket;

/**
 * Created by sinemissione on 2016.06.24..
 */

public class Reciver {

    @JsonIgnore
    protected ConnectionSource connectionSource;

    public Reciver(ConnectionSource connectionSource){
        this.connectionSource = connectionSource;
    }

    public void onRecive(String message, ClientThread thread){

    }

    public void onRecive(String message, UDPServeThread thread){

    }
    public void send(String message, int socketType, Client client) throws IOException {
        if(socketType == Constants.TCP) {
            client.getClientThread().send(message);
        }else if(socketType == Constants.UDP){
            String udpMessage = message + "\n";
            client.getUdpSocket().send(new DatagramPacket(udpMessage.getBytes(),0, udpMessage.getBytes().length));
        }
    }
}
