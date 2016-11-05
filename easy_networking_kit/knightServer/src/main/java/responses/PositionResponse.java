package responses;

import models.Response;
import models.Vector2;

/**
 * Created by sinemissione on 2016.11.05..
 */
public class PositionResponse extends Response {

    private int id;
    private Vector2 newPosition;
    private Vector2 moveDirection;

    public Vector2 getMoveDirection() {
        return moveDirection;
    }

    public void setMoveDirection(Vector2 moveDirection) {
        this.moveDirection = moveDirection;
    }

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
