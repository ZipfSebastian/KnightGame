package logger;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Date;

/**
 * Created by zipfs on 2015. 12. 19..
 */
public class Log {

    private static BufferedWriter out;

    public static void write(String message){
        System.out.println(message);
        try {
            out.write(new Date(System.currentTimeMillis())+" "+message);
            out.newLine();
            out.flush();
        } catch (IOException e) {
            System.out.println(e.getMessage());
        }
    }

    public static void writeToAllOutput(String data){
        System.out.println(data);
        try {
            out.write(data);
            out.newLine();
            out.flush();
        } catch (IOException e) {
            System.out.println(e.getMessage());
        }
    }

    public static void write(Exception ex){
        try {
            writeToAllOutput("EXCEPTION: "+ex.getMessage());
            writeToAllOutput("--------------------------------------");
            StackTraceElement[] stackTraceElements = ex.getStackTrace();
            for(StackTraceElement stackTraceElement : stackTraceElements){
                writeToAllOutput(stackTraceElement.toString());
            }
            out.newLine();
            out.flush();
        } catch (IOException e) {
            System.out.println(e.getMessage());
        }
    }

    public static void init(String logPath){
        try {
            out = new BufferedWriter(new FileWriter(logPath,true));
            out.write("\n");
            out.flush();
        } catch (IOException e) {
            System.out.println(e.getMessage());
        }
    }

    public static void shutDown(){
        try {
            out.close();
        } catch (IOException e) {
            System.out.println(e.getMessage());
        }
    }
}
