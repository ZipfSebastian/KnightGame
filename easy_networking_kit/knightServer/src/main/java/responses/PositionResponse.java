package responses;

import models.Response;
import models.Vector2;

/**
 * Created by sinemissione on 2016.11.05..
 */
public class PositionResponse extends Response {

    private int id;
    private Vector2 newPosition;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public Vector2 getNewPosition() {
        return newPosition;
    }

    public void setNewPosition(Vector2 newPosition) {
        this.newPosition = newPosition;
    }
}
