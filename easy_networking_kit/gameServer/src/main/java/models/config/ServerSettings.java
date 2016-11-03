package models.config;


import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;

/**
 * Created by zipfs on 2016. 06. 28..
 */
public class ServerSettings {

    @JacksonXmlProperty(localName = "TCPServeSleep")
    private int tcpServeSleep;
    @JacksonXmlProperty(localName = "UDPServeSleep")
    private int udpServeSleep;
    @JacksonXmlProperty(localName = "TCPClientSleep")
    private int tcpClientSleep;
    @JacksonXmlProperty(localName = "UDPMaxPacketLength")
    private int udpMaxPacketLength;
    @JacksonXmlProperty(localName = "TCPTimeOut")
    private int tcpTimeOut;
    @JacksonXmlProperty(localName = "ServerPortSettings")
    private ServerPortSettings serverPortSettings;

    public ServerPortSettings getServerPortSettings() {
        return serverPortSettings;
    }

    public void setServerPortSettings(ServerPortSettings serverPortSettings) {
        this.serverPortSettings = serverPortSettings;
    }

    public int getTcpServeSleep() {
        return tcpServeSleep;
    }

    public void setTcpServeSleep(int tcpServeSleep) {
        this.tcpServeSleep = tcpServeSleep;
    }

    public int getUdpServeSleep() {
        return udpServeSleep;
    }

    public void setUdpServeSleep(int udpServeSleep) {
        this.udpServeSleep = udpServeSleep;
    }

    public int getTcpClientSleep() {
        return tcpClientSleep;
    }

    public void setTcpClientSleep(int tcpClientSleep) {
        this.tcpClientSleep = tcpClientSleep;
    }

    public int getUdpMaxPacketLength() {
        return udpMaxPacketLength;
    }

    public void setUdpMaxPacketLength(int udpMaxPacketLength) {
        this.udpMaxPacketLength = udpMaxPacketLength;
    }

    public int getTcpTimeOut() {
        return tcpTimeOut;
    }

    public void setTcpTimeOut(int tcpTimeOut) {
        this.tcpTimeOut = tcpTimeOut;
    }

    /*
    <TCPTimeOut>0</TCPTimeOut>
        <TCPServeSleep>5</TCPServeSleep>
        <UDPServeSleep>5</UDPServeSleep>
        <TCPClientSleep>5</TCPClientSleep>
        <UDPMaxPacketLength>2048</UDPMaxPacketLength>
        <ServerPortSettings>
            <UDPPort>65111</UDPPort>
            <TCPPort>65112</TCPPort>
        </ServerPortSettings>
     */
}
