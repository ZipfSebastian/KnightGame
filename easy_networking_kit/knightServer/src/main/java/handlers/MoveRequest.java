package handlers;

import controllers.Game;
import controllers.GameManagger;
import helpers.Session;
import interfaces.RequestHandler;
import models.Client;
import models.Vector2;

/**
 * Created by sinemissione on 2016.11.05..
 */
public class MoveRequest extends RequestHandler {

    private Vector2 moveDirection;
    private Vector2 newPosition;

    @Override
    public void onRecive(String request) {
        super.onRecive(request);
        Client client = Session.getUserWithSession(session); //sessionnel ell�tott cliensek
        Game game = GameManagger.getGame(client.getGameID()); //game lek�r�s
        game.moveRequest(client, newPosition, moveDirection);
    }

    public Vector2 getMoveDirection() {
        return moveDirection;
    }

    public void setMoveDirection(Vector2 moveDirection) {
        this.moveDirection = moveDirection;
    }

    public Vector2 getNewPosition() {
        return newPosition;
    }

    public void setNewPosition(Vector2 newPosition) {
        this.newPosition = newPosition;
    }
}
