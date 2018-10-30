using UnityEngine;

public class GameEngine
{
    Map gameMap = new Map();
    private int count = 0;

    public void start()
    {
        gameMap.generate();
    }

    public void playGame()
    {
        if(count % 5 == 0)
        {
            //gameMap.PlaceNewResource();
            gameMap.placeNewUnit();
        }
        count++;

        gameMap.AttackBuilding();
        gameMap.moveUnit();       
        gameMap.Redraw();
    }

    public void redraw()
    {
        gameMap.Redraw();
    }  

    //public void PlaceNewUnit()
    //{
    //    gameMap.placeNewUnit();
    //}

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

