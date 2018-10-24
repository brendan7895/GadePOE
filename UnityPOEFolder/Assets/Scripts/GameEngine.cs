using UnityEngine;

public class GameEngine
{
    Map gameMap = new Map();

    public void start()
    {
        gameMap.generate();
    }

    public void playGame()
    {
        gameMap.moveUnit();
        gameMap.close();
        gameMap.Redraw();
    }

    public void redraw()
    {
        gameMap.Redraw();
    }  

    public void PlaceNewUnit()
    {
        gameMap.placeNewUnit();
    }

    public void PlaceResource()
    {
        gameMap.PlaceNewResource();
    }

    public void SaveAll()
    {
        gameMap.Save();
    }

    public void ReadAll()
    {
        gameMap.Read();
    }

    public void End()
    {
        gameMap.Destroy();
    }

    public void changeTeams()
    {
        gameMap.Change();
    }

    public void randomSymbol()
    {
        gameMap.randSymbols();
    }
}

