package models;

import thread.ClientThread;
import thread.UDPServeThread;

import java.net.DatagramSocket;
import java.net.InetAddress;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by zipfs on 2016. 06. 04..
 */
public class Client {
    private SystemUser user;
    private ClientThread clientThread;
    private int gameID;
    private InetAddress inetAddress;
    private int udpPort;

    public InetAddress getInetAddress() {
        return inetAddress;
    }

    public void setInetAddress(InetAddress inetAddress) {
        this.inetAddress = inetAddress;
    }

    public int getUdpPort() {
        return udpPort;
    }

    public void setUdpPort(int udpPort) {
        this.udpPort = udpPort;
    }

    public Client(){

    }

    public Client(SystemUser user){
        this.user = user;
    }

    private String session;

    private int selectedGameType;

    private int status;


    public String getSession() {
        return session;
    }

    public void setSession(String session) {
        this.session = session;
    }

    public int getSelectedGameType() {
        return selectedGameType;
    }

    public void setSelectedGameType(int selectedGameType) {
        this.selectedGameType = selectedGameType;
    }

    public int getStatus() {
        return status;
    }

    public void setStatus(int status) {
        this.status = status;
    }

    public SystemUser getUser() {
        return user;
    }

    public void setUser(SystemUser user) {
        this.user = user;
    }

    public ClientThread getClientThread() {
        return clientThread;
    }

    public void setClientThread(ClientThread clientThread) {
        this.clientThread = clientThread;
    }
/*
    public String toMessage() {
        return user.toMessage();
    }
    {"@class" : "handlers.LoginRequest", "userName" : "szebi", "password" : "szebi"}
    {"@class" : ".SubA", "a" : 5}
*/

    public int getGameID() {
        return gameID;
    }

    public void setGameID(int gameID) {
        this.gameID = gameID;
    }

    public boolean isOnline(){
        return this.clientThread.isOnline();

    }
}
