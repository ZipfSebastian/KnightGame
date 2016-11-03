package controllers;


import java.util.Map;

/**
 * Created by Kesze on 2010.01.12..
 */
public class GameManagger {

    private static int index;
    private static Map<Integer, Game> games; //key value alapján tárol adatokat

    public static int addGame(Game game){
        games.put(index, game); //a mapba helyzzük az indexel ellátott játékot
        index++;
        return index-1;
    }

    public static void gameEnded(int id){
            games.remove(id); //eltávolítjuk a játékot a végeztével
    }

    public static Game getGame(int id){
        return games.get(id); //lekérjük id alapján a játékot
    }
}
