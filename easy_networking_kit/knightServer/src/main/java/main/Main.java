package main;

import com.j256.ormlite.jdbc.JdbcConnectionSource;
import com.j256.ormlite.support.ConnectionSource;
import constants.Properties;
import server.ServerVariables;

/**
 * Created by sinemissione on 2016.11.03..
 */
public class Main {

    public static void main(String[] args) {
        ServerApplication.start();
        try {
            ConnectionSource connectionSource =
                    new JdbcConnectionSource(ServerVariables.getValue(Properties.PROP_DATABASE_STRING));
            TableCreator.createTable(connectionSource);
        }catch (Exception e){

        }
    }
}
