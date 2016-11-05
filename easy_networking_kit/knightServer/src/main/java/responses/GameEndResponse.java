package responses;

import models.Response;

/**
 * Created by sinemissione on 2016.11.05..
 */
public class GameEndResponse extends Response {

    private boolean victory;
    private int points;

    public boolean isVictory() {
        return victory;
    }

    public void setVictory(boolean victory) {
        this.victory = victory;
    }

    public int getPoints() {
        return points;
    }

    public void setPoints(int points) {
        this.points = points;
    }
}
