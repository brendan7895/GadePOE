namespace Task1
{
    public class GameEngine
    {
        Map gameMap = new Map();

        public void start()
        {
            gameMap.generate();
            
        }

        public string playGame()
        {
            gameMap.moveUnit();
            gameMap.close();
            return gameMap.Redraw();
        }

        public void redraw()
        {
            gameMap.Redraw();
        }

        public string UnitsString(int i)
        {
            return gameMap.UnitsCombo(i);
        }

        public string BuildInfo(int i)
        {
            return gameMap.BuildingCombo(i);
        }

        public int numUnit()
        {
            return gameMap.numUnit();
        }

        public int numBuilding()
        {
            return gameMap.numBuild();
        }

        public void PlaceNewUnit(int counter)
        {
            gameMap.placeNewUnit(counter);
        }

        public void PlaceResource(int counter)
        {
            gameMap.PlaceNewResource(counter);
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
}
