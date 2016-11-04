package models;

import com.j256.ormlite.field.DatabaseField;
import com.j256.ormlite.table.DatabaseTable;

/**
 * Created by sinemissione on 2016.11.05..
 */

@DatabaseTable(tableName = "spawn_point")
public class SpawnPoint {

    public static final String ID = "id";
    public static final String MAP_ID = "mapID";
    public static final String X = "x";
    public static final String Y = "y";

    @DatabaseField(generatedId = true, columnName = ID)
    public int id;
    @DatabaseField(columnName = MAP_ID)
    public int mapID;
    @DatabaseField(columnName = X)
    public float x;
    @DatabaseField(columnName = Y)
    public float y;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getMapID() {
        return mapID;
    }

    public void setMapID(int mapID) {
        this.mapID = mapID;
    }

    public float getX() {
        return x;
    }

    public void setX(float x) {
        this.x = x;
    }

    public float getY() {
        return y;
    }

    public void setY(float y) {
        this.y = y;
    }
}
