package responses;

import models.Response;
import models.SpawnPoint;
import models.Vector2;

/**
 * Created by sinemissione on 2016.11.05..
 */
public class LoadGameResponse  extends Response{

    private int mapID;

    public int getMapID() {
        return mapID;
    }

    public void setMapID(int mapID) {
        this.mapID = mapID;
    }
}
