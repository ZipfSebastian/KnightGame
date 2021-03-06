package models;

/**
 * Created by sinemissione on 2016.11.05..
 */
public class Player {

    private Client client;
    private SpawnPoint spawnPoint;
    private Vector2 position;
    private Vector2 moveDirection;
    private int id;

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

    public Client getClient() {
        return client;
    }

    public void setClient(Client client) {
        this.client = client;
    }

    public SpawnPoint getSpawnPoint() {
        return spawnPoint;
    }

    public void setSpawnPoint(SpawnPoint spawnPoint) {
        this.spawnPoint = spawnPoint;
    }

    public Vector2 getPosition() {
        return position;
    }

    public void setPosition(Vector2 position) {
        this.position = position;
    }
}
