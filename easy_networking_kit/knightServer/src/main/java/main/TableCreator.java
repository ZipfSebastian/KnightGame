package main;

import com.j256.ormlite.support.ConnectionSource;
import com.j256.ormlite.table.TableUtils;
import models.Map;
import models.SpawnPoint;
//import models.SpawnPoint;

import java.sql.SQLException;

/**
 * Created by Kesze on 2016.08.27..
 */
public class TableCreator {
    public static void createTable(ConnectionSource connectionSource) throws SQLException {
        //TableUtils.createTableIfNotExists(connectionSource, Statistics.class);

        //TableUtils.createTableIfNotExists(connectionSource, SpawnPoint.class);
        TableUtils.createTableIfNotExists(connectionSource, Map.class);
        TableUtils.createTableIfNotExists(connectionSource, SpawnPoint.class);
    }
}
