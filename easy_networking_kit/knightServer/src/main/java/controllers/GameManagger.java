package controllers;


import java.util.HashMap;
import java.util.Map;

/**
 * Created by Kesze on 2010.01.12..
 */
public class GameManagger {

    private static int index;
    private static Map<Integer, Game> games = new HashMap<Integer, Game>(); //key value alapj�n t�rol adatokat

    public static int addGame(Game game){
        games.put(index, game); //a mapba helyzz�k az indexel ell�tott j�t�kot
        index++;
        return index-1;
    }

    public static void gameEnded(int id){
            games.remove(id); //elt�vol�tjuk a j�t�kot a v�gezt�vel
    }

    public static Game getGame(int id){
        return games.get(id); //lek�rj�k id alapj�n a j�t�kot
    }
}
