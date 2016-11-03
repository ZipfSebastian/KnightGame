package main;

import com.j256.ormlite.logger.LocalLog;
import com.mysql.jdbc.exceptions.jdbc4.CommunicationsException;
import constants.Commands;
import logger.Log;
import server.CommandPrompt;
import server.ServerVariables;
import thread.ServeThread;
import thread.UDPServeThread;

import java.io.BufferedReader;
import java.io.InputStreamReader;

/**
 * Created by zipfs on 2015. 12. 19..
 */
public class ServerApplication {
    //url to noroc server!
    //database_string = "jdbc:mysql://127.0.0.1/strategy?user=NorocUser&password=asdasd";

    private static final String logName = "log.txt";
    private static final String confName = "conf.cfg";

    public static void start() {
        init();
        mainThread();
    }


    /**
     * Initialize the server
     */
    public static void init(){
        System.setProperty(LocalLog.LOCAL_LOG_LEVEL_PROPERTY, "error");
        ServerVariables.init(confName);
        ServerVariables.readFromFile();
        Log.init(logName);
    }

    public static void mainThread(){
        ServeThread serveThread = new ServeThread();
        UDPServeThread udpServeThread = new UDPServeThread();
        CommandPrompt commandPrompt = new CommandPrompt(serveThread,udpServeThread, confName);
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));

        String input = "";
        while(!input.equals(Commands.EXIT)) {
            try {
                input = br.readLine();
                String[] inputData = input.split(Commands.COMMAND_SEPARATOR);
                commandPrompt.commandReceived(inputData);
            }catch (CommunicationsException exc){
                Log.write("Unable to connect to the database!");
            }catch (Exception ex){
                Log.write(ex);
            }
        }
    }
}