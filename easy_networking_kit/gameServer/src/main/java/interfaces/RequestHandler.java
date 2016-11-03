package interfaces;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonTypeInfo;
import com.j256.ormlite.support.ConnectionSource;
import com.sun.xml.internal.bind.v2.runtime.reflect.opt.Const;
import constants.Constants;
import models.Client;
import thread.ClientThread;

import java.io.IOException;
import java.lang.reflect.Type;
import java.net.DatagramPacket;
import java.net.DatagramSocket;

/**
 * Created by sinemissione on 2016.06.24..
 */
@JsonTypeInfo(
        use = JsonTypeInfo.Id.MINIMAL_CLASS,
        include = JsonTypeInfo.As.PROPERTY,
        property = "type")
public class RequestHandler{

    @JsonIgnore
    protected ConnectionSource connectionSource;
    @JsonIgnore
    protected ClientThread thread;

    protected String type;
    protected  String session;

    public void onRecive(String request){

    }

    public void send(String message, int socketType, Client client) throws IOException {
        if(socketType == Constants.TCP) {
            client.getClientThread().send(message);
        }else if(socketType == Constants.UDP){
            String udpMessage = message + "\n";
            client.getUdpSocket().send(new DatagramPacket(udpMessage.getBytes(),0, udpMessage.getBytes().length));
        }
    }

    public void send(String message) throws IOException {
        thread.send(message);
    }

    public ClientThread getThread() {
        return thread;
    }

    public void setThread(ClientThread thread) {
        this.thread = thread;
    }

    public ConnectionSource getConnectionSource() {
        return connectionSource;
    }

    public void setConnectionSource(ConnectionSource connectionSource) {
        this.connectionSource = connectionSource;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getSession() {
        return session;
    }

}
