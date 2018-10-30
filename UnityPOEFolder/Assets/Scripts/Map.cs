using System;
using System.IO;
using UnityEngine;


public partial class Map : MonoBehaviour
{
    const float PADDING = 5.12f;

    private int count = 0;
    float X_OFF, Y_OFF;

    System.Random rand = new System.Random();

    int numUnits = 10; //number of units to be placed
    int numBuildings = 5; //number of resource buildings

    string[,] mapArr = new string[20, 20]; //map array

    Unit[] units;
    Building[] buildings;

    public void generate()
    {
        X_OFF = -Camera.main.orthographicSize;
        Y_OFF = Camera.main.orthographicSize;

        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 20; x++)
            {
                Instantiate(Resources.Load("Grass"), new Vector3(X_OFF + (x * PADDING), Y_OFF + (-y * PADDING), 1), Quaternion.identity);
            }
        }

        units = new Unit[numUnits];
        buildings = new Building[numBuildings];

        for (int i = 0; i < numUnits; i++) //creates and places the units in the map
        {
            int x = rand.Next(0, 20);
            int y = rand.Next(0, 20);

            int teamRand = rand.Next(0, 4);

            if (teamRand == 0) // S M 
            {
                units[i] = new MeleeUnit(x, y, 100, 100, 1, 5, 1, Teams(), "L", "Melee");
                if(units[i].Team == "S")
                {
                    Instantiate(Resources.Load("Melee"), new Vector3(X_OFF + (units[i].XPos * PADDING), Y_OFF + (-units[i].YPos * PADDING), -1), Quaternion.identity);
                }
                if(units[i].Team == "M")
                {
                    Instantiate(Resources.Load("MeleeEnemy"), new Vector3(X_OFF + (units[i].XPos * PADDING), Y_OFF + (-units[i].YPos * PADDING), -1), Quaternion.identity);
                }
            }
            if (teamRand == 1)
            {
                units[i] = new RangedUnit(x, y, 100, 100, 1, 10, 5, Teams(), "W", "Ranged");
                if (units[i].Team == "S")
                {
                    Instantiate(Resources.Load("RangedUnit"), new Vector3(X_OFF + (units[i].XPos * PADDING), Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (units[i].Team == "M")
                {
                    Instantiate(Resources.Load("RangedUnitEnemy"), new Vector3(X_OFF + (units[i].XPos * PADDING), Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
            }
            if (teamRand == 2)
            {
                units[i] = new Rogue(x, y, 100, 100, 1, 12, 5, Teams(), "V", "Rogue");
                if (units[i].Team == "S")
                {
                    Instantiate(Resources.Load("Rogue"), new Vector3(X_OFF + (units[i].XPos * PADDING), Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (units[i].Team == "M")
                {
                    Instantiate(Resources.Load("RogueEnemy"), new Vector3(X_OFF + (units[i].XPos * PADDING), Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
            }
            if (teamRand == 3)
            {
                units[i] = new Dragon(x, y, 200, 200, 1, 15, 6, Teams(), "D", "Dragon");
                if (units[i].Team == "S")
                {
                    Instantiate(Resources.Load("Dragon"), new Vector3(X_OFF + (units[i].XPos * PADDING), Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (units[i].Team == "M")
                {
                    Instantiate(Resources.Load("DragonEnemy"), new Vector3(X_OFF + (units[i].XPos * PADDING), Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
            }
        }

        for (int i = 0; i < buildings.Length; i++)
        {
            int x = rand.Next(0, 20);
            int y = rand.Next(0, 20);

            int teamRand = rand.Next(0, 2);
            int buildingType = rand.Next(0, 2);

            if (teamRand == 0)
            {
                if (buildingType == 0)
                {
                    buildings[i] = new ResourceBuilding(x, y, 100, "W", "R", 5, "Resource");
                    Instantiate(Resources.Load("ResourceBuilding"), new Vector3(X_OFF + (buildings[i].XPos * PADDING), Y_OFF + (-buildings[i].YPos * PADDING), 0), Quaternion.identity);
                }
                else
                {
                    buildings[i] = new FactoryBuilding(x, y, 100, "W", "F", 5, 1, "Factory");
                    Instantiate(Resources.Load("Factory"), new Vector3(X_OFF + (buildings[i].XPos * PADDING), Y_OFF + (-buildings[i].YPos * PADDING), 0), Quaternion.identity);
                }
            }
            if (teamRand == 1)
            {
                if (buildingType == 0)
                {
                    buildings[i] = new ResourceBuilding(x, y, 100, "F", "R", 5, "ResourceE");//🏙️🏠
                    Instantiate(Resources.Load("ResourceBuildingEnemy"), new Vector3(X_OFF + (buildings[i].XPos * PADDING), Y_OFF + (-buildings[i].YPos * PADDING), 0), Quaternion.identity);
                }
                else
                {
                    buildings[i] = new FactoryBuilding(x, y, 100, "F", "F", 5, 1, "FactoryE");
                    Instantiate(Resources.Load("FactoryEnemy"), new Vector3(X_OFF + (buildings[i].XPos * PADDING), Y_OFF + (-buildings[i].YPos * PADDING), 0), Quaternion.identity);
                }
            }


        }
       
    }

    public void moveUnit()
    {
        for (int i = 0; i < units.Length; i++)
        {
            Unit temp = units[i].closestUnit(units);

            if (units[i].inRange(temp.XPos, temp.YPos) == false)
            {
                if (units[i].XPos <= temp.XPos)
                {
                    units[i].updatePos("d");
                }

                if (units[i].XPos >= temp.XPos)
                {
                    units[i].updatePos("a");
                }

                if (units[i].YPos <= temp.YPos)
                {
                    units[i].updatePos("s");
                }

                if (units[i].YPos >= temp.YPos)
                {
                    units[i].updatePos("w");
                }
            }

            if (units[i].inRange(temp.XPos, temp.YPos) == true)
            {               
                if (units[i].isDead() == false)
                {

                    //units[i].AttackUnit();
                    //temp.HP = temp.HP - units[i].Attack;
                    units[i].HP -= temp.Attack;
                }

            }

            if (units[i].HP <= 25 && units[i].isDead() == false) //units running away below 25%
            {
                int choice = rand.Next(0, 4);

                if (units[i].XPos != 19 && units[i].YPos != 19 && units[i].XPos != 0 && units[i].YPos != 0)
                {
                    switch (choice)
                    {
                        case 0:
                            {
                                units[i].updatePos("d");
                            }
                            break;
                        case 1:
                            {
                                units[i].updatePos("a");
                            }
                            break;
                        case 2:
                            {
                                units[i].updatePos("s");
                            }
                            break;
                        case 3:
                            {
                                units[i].updatePos("w");
                            }
                            break;
                    }
                }
            }
        }
    }

    public void AttackBuilding()
    {
        for(int i = 0; i < buildings.Length; i++)
        {
            for(int j = 0; j < units.Length; j++)
            {
                if(units[j].inRange(buildings[i].XPos, buildings[i].YPos))
                {
                    buildings[i].Health = buildings[i].Health - units[i].Attack;
                }
            }
        }
    }

    public string Teams()
    {
        int i = rand.Next(0, 2);
        string sym = "";

        if (i == 0)
        {
            sym = "S";
        }
        if (i == 1)
        {
            sym = "M";
        }
        return sym;
    }

    public void Redraw()
    {
        DestroyAll();
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].isDead() != true)
            {
                if (units[i].Name == "Melee" && units[i].Team == "S")
                {
                    Instantiate(Resources.Load("Melee"), new Vector3(X_OFF + (units[i].XPos * PADDING) + 1, Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (units[i].Name == "Melee" && units[i].Team == "M")
                {
                    Instantiate(Resources.Load("MeleeEnemy"), new Vector3(X_OFF + (units[i].XPos * PADDING) + 1, Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (units[i].Name == "Ranged" && units[i].Team == "S")
                {
                    Instantiate(Resources.Load("RangedUnit"), new Vector3(X_OFF + (units[i].XPos * PADDING) + 1, Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (units[i].Name == "Ranged" && units[i].Team == "M")
                {
                    Instantiate(Resources.Load("RangedUnitEnemy"), new Vector3(X_OFF + (units[i].XPos * PADDING) + 1, Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (units[i].Name == "Rogue" && units[i].Team == "S")
                {
                    Instantiate(Resources.Load("Rogue"), new Vector3(X_OFF + (units[i].XPos * PADDING) + 1, Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (units[i].Name == "Rogue" && units[i].Team == "M")
                {
                    Instantiate(Resources.Load("RogueEnemy"), new Vector3(X_OFF + (units[i].XPos * PADDING) + 1, Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (units[i].Name == "Dragon" && units[i].Team == "S")
                {
                    Instantiate(Resources.Load("Dragon"), new Vector3(X_OFF + (units[i].XPos * PADDING) + 1, Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (units[i].Name == "Dragon" && units[i].Team == "M")
                {
                    Instantiate(Resources.Load("DragonEnemy"), new Vector3(X_OFF + (units[i].XPos * PADDING) + 1, Y_OFF + (-units[i].YPos * PADDING), 0), Quaternion.identity);
                }
                Instantiate(Resources.Load(units[i].DetermineHP(units[i].HP, units[i].MaxHP)), new Vector3(X_OFF + (units[i].XPos * PADDING) + 1, Y_OFF + (-units[i].YPos * PADDING) + 1, -1), Quaternion.identity);
            }

        }

        for (int i = 0; i < buildings.Length; i++)
        {
            if(buildings[i].isDead() != true)
            {
                if (buildings[i].Type == "Factory")
                {
                    Instantiate(Resources.Load("Factory"), new Vector3(X_OFF + (buildings[i].XPos * PADDING) + 1, Y_OFF + (-buildings[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (buildings[i].Type == "FactoryE")
                {
                    Instantiate(Resources.Load("FactoryEnemy"), new Vector3(X_OFF + (buildings[i].XPos * PADDING) + 1, Y_OFF + (-buildings[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (buildings[i].Type == "Resource")
                {
                    Instantiate(Resources.Load("ResourceBuilding"), new Vector3(X_OFF + (buildings[i].XPos * PADDING) + 1, Y_OFF + (-buildings[i].YPos * PADDING), 0), Quaternion.identity);
                }
                if (buildings[i].Type == "ResourceE")
                {
                    Instantiate(Resources.Load("ResourceBuildingEnemy"), new Vector3(X_OFF + (buildings[i].XPos * PADDING) + 1, Y_OFF + (-buildings[i].YPos * PADDING), 0), Quaternion.identity);
                }
                Instantiate(Resources.Load(buildings[i].DetermineBuildHP(buildings[i].Health, buildings[i].MaxHP)), new Vector3(X_OFF + (buildings[i].XPos * PADDING), Y_OFF + (-buildings[i].YPos * PADDING) + 1, -1), Quaternion.identity);
            }
            
        }

    }

    void DestroyAll()
    {
        GameObject[] killAllUnits = GameObject.FindGameObjectsWithTag("Redraw");

        foreach (GameObject unit in killAllUnits)
        {
            Destroy(unit);
        }
    }

    int arraySize;
    int counter = 1;
    public void placeNewUnit() //places new unit
    {

        arraySize = units.Length + 1;
        for (int i = 0; i < numBuildings - 1; i++) //numbuild -1?
        {
            string buildingType = buildings[i].GetType().ToString();
            string[] splitBuilding = buildingType.Split('.');
            buildingType = splitBuilding[splitBuilding.Length - 1];

            if (buildingType == "FactoryBuilding")
            {
                FactoryBuilding temp = (FactoryBuilding)buildings[i];
                if(temp.NumberOfUnits > 0)
                {
                    Array.Resize(ref units, arraySize);
                    units[arraySize - 1] = temp.SpawnUnit(); //arraySize - 1

                    units[units.Length - 1] = new MeleeUnit(buildings[i].XPos + 1, buildings[i].YPos, 100, 100, 1, 10, 5, Teams().ToLower(), "L", "Melee");

                    Instantiate(Resources.Load("Melee"), new Vector3(X_OFF + (buildings[i].XPos + 1 * PADDING) + 1, Y_OFF + (-buildings[i].YPos * PADDING), -1), Quaternion.identity);
                    //Instantiate(Resources.Load("Melee"), new Vector3(X_OFF + (units[units.Length - 1].XPos + 1 * PADDING) + 1, Y_OFF + (-units[units.Length - 1].YPos * PADDING), -1), Quaternion.identity);
                }

            }



        }

    }

    public void PlaceNewResource() //places the resource on the map
    {
        for (int i = 0; i < numBuildings - 1; i++)
        {
            string buildingType = buildings[i].GetType().ToString();
            string[] splitBuilding = buildingType.Split('.');
            buildingType = splitBuilding[splitBuilding.Length - 1];

            if (buildingType == "ResourceBuilding")
            {
                ResourceBuilding temp = (ResourceBuilding)buildings[i];
                temp.Resources();

                if (temp.Resources() == true)
                {
                    int x = rand.Next(0, 20);
                    int y = rand.Next(0, 20);

                    Instantiate(Resources.Load("Coin"), new Vector3(X_OFF + (x + 1 * PADDING) + 1, Y_OFF + (-y * PADDING), -1), Quaternion.identity);
                }
            }
        }
    }

    public void Save() //calls save methods from unit and building and saves to files
    {
        FileStream file = new FileStream("saves/UnitSave.file", FileMode.Create, FileAccess.Write);
        FileStream build = new FileStream("saves/BuildingSave.file", FileMode.Create, FileAccess.Write);
        file.Close();
        build.Close();

        for (int i = 0; i < units.Length; i++)
        {
            units[i].SaveUnit();
        }

        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i].SaveBuilding();
        }
    }

    public void Read() //reads in the unit and building files into new arrays
    {

        FileStream file = new FileStream("saves/UnitSave.file", FileMode.Open, FileAccess.Read);
        string[] completeFile = File.ReadAllLines("saves/UnitSave.file");

        units = new Unit[completeFile.Length];


        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                mapArr[j, i] = ".";
            }
        }

        for (int i = 0; i < completeFile.Length; i++)
        {
            string[] unit = completeFile[i].Split(',');
            string type = unit[0];
            string symbol = unit[1];
            string team = unit[2];
            int x = Convert.ToInt32(unit[3]);
            int y = Convert.ToInt32(unit[4]);
            int hp = Convert.ToInt32(unit[5]);

            if (type == "Ranged")
            {
                units[i] = new RangedUnit(x, y, hp, 100, 1, 10, 1, team, symbol, type);
            }
            if (type == "Melee")
            {
                units[i] = new MeleeUnit(x, y, hp, 100, 1, 10, 5, team, symbol, type);
            }
            if (type == "Rogue")
            {
                units[i] = new Rogue(x, y, hp, 100, 1, 15, 5, team, symbol, type);
            }
            if (type == "Dragon")
            {
                units[i] = new Dragon(x, y, hp, 200, 1, 20, 6, team, symbol, type);
            }

            mapArr[units[i].XPos, units[i].YPos] = units[i].Symbol;
        }

        file.Close();

        FileStream building = new FileStream("saves/BuildingSave.file", FileMode.Open, FileAccess.Read);
        string[] buildingFile = File.ReadAllLines("saves/BuildingSave.file");

        buildings = new Building[buildingFile.Length];

        for (int i = 0; i < buildingFile.Length; i++)
        {
            string[] buildArr = buildingFile[i].Split(',');
            string bSymbol = buildArr[1];
            string bType = buildArr[0];
            int bX = Convert.ToInt32(buildArr[2]);
            int bY = Convert.ToInt32(buildArr[3]);
            int bHp = Convert.ToInt32(buildArr[4]);


            if (bType == "Resource") //type + ", " + symbol + "," + XPos + "," + YPos + "," + health;
            {
                buildings[i] = new ResourceBuilding(bX, bY, bHp, bSymbol, "R", 5, bType);
            }
            if (bType == "Factory")
            {
                buildings[i] = new FactoryBuilding(bX, bY, bHp, bSymbol, "F", 5, 1, bType);
            }

            mapArr[buildings[i].XPos, buildings[i].YPos] = buildings[i].Symbol;
        }
        building.Close();

    }

    public void Destroy()
    {
        for (int i = 0; i < numUnits; i++)
        {
            units[i].HP = 0;
        }

    }

    public void Change()//teams will be changed not symbols
    {
        for (int i = 0; i < numUnits; i++)
        {
            if (units[i].Team == "M" || units[i].Team == "m")
            {
                units[i].Team = "S";
            }
            else
            {
                units[i].Team = "M";
            }
        }
    }

    public void randSymbols() //randomises the symbols using ascii using 0 to 1000
    {
        for (int i = 0; i < numUnits; i++)
        {
            int unicode = rand.Next(0, 10001);
            char character = (char)unicode;
            string text = character.ToString();
            units[i].Symbol = text;
            mapArr[units[i].XPos, units[i].YPos] = units[i].Symbol;
        }
    }
}

