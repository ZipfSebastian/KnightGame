package server;

import com.fasterxml.jackson.dataformat.xml.XmlMapper;
import com.j256.ormlite.jdbc.JdbcConnectionSource;
import com.j256.ormlite.support.ConnectionSource;
import com.j256.ormlite.table.TableUtils;
import constants.Commands;
import constants.Properties;
import logger.Log;
import models.SystemUser;
import models.config.GameServerConfig;
import thread.ServeThread;
import thread.UDPServeThread;

import java.io.File;

/**
 * Created by zipfs on 2016. 01. 11..
 * Handle the user commands
 */
public class CommandPrompt {

    private ServeThread serveThread;
    private String confName;
    private UDPServeThread udpServeThread;

    public CommandPrompt(ServeThread serveThread, UDPServeThread udpServeThread, String confName) {
        this.serveThread = serveThread;
        this.confName = confName;
        this.udpServeThread = udpServeThread;
        System.out.println("Type the commands!");
    }

    /*
    Handle the user commands
     */
    public void commandReceived(String[] inputData) throws Exception{
        if(inputData[0].equals(Commands.START)){
            startServer(serveThread,udpServeThread);
        }else if(inputData[0].equals(Commands.EXIT)){
            if(serveThread != null && serveThread.isRun()) {
                stopServer(serveThread);
            }
        }else if(inputData.length>1 && inputData[0].equals(Commands.SET)){
            if(inputData[1].equals(Commands.SET_TIMEOUT)){
                ServerVariables.setKey(Properties.PROP_TIME_OUT, inputData[2], Boolean.parseBoolean(inputData[3]));
            }else if(inputData[1].equals(Commands.SET_CLIENT_SLEEP)){
                ServerVariables.setKey(Properties.PROP_CLIENT_SLEEP,inputData[2], Boolean.parseBoolean(inputData[3]));
            }else if(inputData[1].equals(Commands.SET_SERVE_SLEEP)){
                ServerVariables.setKey(Properties.PROP_SERVE_SLEEP,inputData[2], Boolean.parseBoolean(inputData[3]));
            }else if(inputData[1].equals(Commands.SET_PORT)){
                ServerVariables.setKey(Properties.PROP_PORT,inputData[2], Boolean.parseBoolean(inputData[3]));
            }else if(inputData[1].equals(Commands.SET_DATABASE_STRING)){
                ServerVariables.setKey(Properties.PROP_DATABASE_STRING,inputData[2], Boolean.parseBoolean(inputData[3]));
            }else{
                writeUnknownMessage();
            }
            ServerVariables.writeConfigToFile();
        }else if(inputData[0].equals(Commands.HELP)){
            writeHelpMessage();
        }else if(inputData[0].equals(Commands.WRITE_CONFIG)){
            Log.write(ServerVariables.getFullConfigAsString());
        }else{
            writeUnknownMessage();
        }
    }

    /*
    Unknown command typed
     */
    private void writeUnknownMessage(){
        System.out.println("Unknown command or bad syntax! type '"+Commands.HELP+"' to help!");
    }

    /*
    Write a help dialog to console
     */
    private void writeHelpMessage(){
        System.out.println("Default commands: ");
        System.out.println("HELP: "+Commands.HELP);
        System.out.println("START: " + Commands.START);
        System.out.println("EXIT: " + Commands.EXIT);
        System.out.println("Variable setup (Must set before start!): " + Commands.SET + " variables:");
        System.out.println("----------------------------------------------------------");
        System.out.println("    SET : " + Commands.SET_TIMEOUT + " 'time out time (0 is no time out!)'");
        System.out.println("    SET : " + Commands.SET_DATABASE_STRING + " 'database string'");
        System.out.println("    SET : " + Commands.SET_SERVE_SLEEP + " 'serve sleep'");
        System.out.println("    SET : " + Commands.SET_CLIENT_SLEEP + " 'client sleep'");
        System.out.println("    SET : " + Commands.SET_PORT + " 'port'");
        System.out.println("-----------------------------------------------------------");
        System.out.println("Variables saved in config file saved on modify! config name: " + confName);
    }

    /*
    Start the server
     */
    private void startServer(ServeThread serveThread, UDPServeThread udpServeThread) throws Exception{


        ConnectionSource connectionSource =
                new JdbcConnectionSource(ServerVariables.getValue(Properties.PROP_DATABASE_STRING));
        try {
            TableUtils.createTableIfNotExists(connectionSource, SystemUser.class);

            XmlMapper mapper = new XmlMapper();

            GameServerConfig config = mapper.readValue(new File("settings.xml"), GameServerConfig.class);



            Router router = new Router(connectionSource);

            serveThread.Init(Integer.parseInt(ServerVariables.getValue(Properties.PROP_PORT))
                    ,connectionSource,router
                    , Long.parseLong(ServerVariables.getValue(Properties.PROP_SERVE_SLEEP))
                    ,Long.parseLong(ServerVariables.getValue(Properties.PROP_CLIENT_SLEEP)));

            udpServeThread.Init(Integer.parseInt(ServerVariables.getValue(Properties.PROP_UDP_PORT)), router
                    , Integer.parseInt(ServerVariables.getValue(Properties.PROP_UDP_PACKAGE_LENGTH))
                    , Long.parseLong(ServerVariables.getValue(Properties.PROP_CLIENT_SLEEP)));

            serveThread.start();
            Log.write("Running");
        }catch (Exception e){
            Log.write("Unable to connect to database, check you created correctly and db user can connect to it!");
            Log.write(e);
        }
    }

    /*
    Stop the server
     */
    private void stopServer(ServeThread serveThread){
        serveThread.shutDown();
        ServerVariables.shutDown();
        Log.write("Server shutdown!");
        Log.shutDown();
    }
}
