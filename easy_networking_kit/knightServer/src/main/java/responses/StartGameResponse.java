package responses;

import models.Enemy;
import models.Response;
import models.Vector2;

import java.util.List;

/**
 * Created by sinemissione on 2016.11.05..
 */
public class StartGameResponse extends Response{

    private List<Enemy> enemyList;
    private Vector2 position;

    public List<Enemy> getEnemyList() {
        return enemyList;
    }

    public void setEnemyList(List<Enemy> enemyList) {
        this.enemyList = enemyList;
    }

    public Vector2 getPosition() {
        return position;
    }

    public void setPosition(Vector2 position) {
        this.position = position;
    }
}
