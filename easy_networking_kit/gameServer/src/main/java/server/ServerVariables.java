package server;

import logger.Log;
import models.KeyValue;

import java.io.*;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by zipfs on 2016. 01. 09..
 * Server variables what defined in the config file
 */
public class ServerVariables {

    private static BufferedWriter out;
    private static BufferedReader in;
    private static String configPath;

    public static final String KEY_NOT_EXIST = "NONE";
    private static List<KeyValue> comments = new ArrayList<KeyValue>();

    private static List<KeyValue> properties = new ArrayList<KeyValue>();

    /*
    Set a properti to server
     */
    public static void setKey(String key, String value, boolean writeToFile){
        boolean exist = false;
        for(KeyValue keyValue : properties){
            if(keyValue.getKey().equals(key)){
                keyValue.setValue(value);
                exist = true;
            }
        }
        if(!exist){
            properties.add(new KeyValue(key,value));
        }

        if(writeToFile){
            writeConfigToFile();
        }
    }

    /*
    return a property from server setup
     */
    public static String getValue(String key){
        for(KeyValue keyValue : properties){
            if(keyValue.getKey().equals(key)){
                return keyValue.getValue();
            }
        }
        return KEY_NOT_EXIST;
    }

    /*
    Write the configuration to the server's config file
     */
    public static void writeConfigToFile(){
        try {
            out = new BufferedWriter(new FileWriter(configPath, false));
            int i=0;
            for (KeyValue keyValue : properties) {
                for(KeyValue comment : comments){
                    if(Integer.parseInt(comment.getKey()) == i){
                        out.write(comment.getValue());
                        out.newLine();
                        i++;
                    }
                }
                out.write(keyValue.getKey() + " = " + keyValue.getValue());
                out.newLine();
                i++;
            }
            out.flush();
        }catch(Exception e){
            Log.write(e);
        }
    }

    /*
    Read the configuration from the config file
     */
    public static void readFromFile(){
        try {
            String line;
            int i=0;
            while ((line = in.readLine()) != null) {
                if(line.charAt(0) != '/' && line.charAt(1) != '/') {
                    String[] conf = line.split(" = ");
                    setKey(conf[0], conf[1], false);
                }else{
                    comments.add(new KeyValue(i+"", line));
                }
                i++;
            }
        }catch (Exception e){
            Log.write(e);
        }
    }

    /*
    Initialize the server variable service
     */
    public static void init(String path){
        try {
            configPath = path;
            in = new BufferedReader(new FileReader(configPath));

        } catch (IOException e) {
            Log.write(e);
        }
    }

    /*
    Stop the server variable service
     */
    public static void shutDown(){
        try {
            if(out != null) {
                out.close();
            }
            if(in != null) {
                in.close();
            }
        } catch (IOException e) {
            Log.write(e);
        }
    }

    public static String getFullConfigAsString(){
        String conf = "";
        for(KeyValue value : properties){
            conf += value.getKey() + ";"+value.getValue() + "\n";
        }
        return conf;
    }


    public void LoadXML(){

    }
}
