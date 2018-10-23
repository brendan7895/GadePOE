using System;
using System.IO;

namespace Task1
{
    public partial class Map
    {
        Random rand = new Random();

        int numUnits = 5; //number of units to be placed
        int numBuildings = 3; //number of resource buildings

        string[,] mapArr = new string[20, 20]; //map array

        Unit[] units;
        Building[] buildings;

        public void generate()
        {
            for (int i = 0; i < 20; i++) //populates the map array
            {
                for (int j = 0; j < 20; j++)
                {
                    mapArr[j, i] = ".";
                }
            }

            units = new Unit[numUnits];
            buildings = new Building[numBuildings];

            for (int i = 0; i < numUnits; i++) //creates and places the units in the map
            {
                int x = rand.Next(0, 20);
                int y = rand.Next(0, 20);

                int teamRand = rand.Next(0, 4);

                if (teamRand == 0)
                {
                    units[i] = new MeleeUnit(x, y, 100, 100, 1, 10, 5, Teams().ToLower(), "L", "Melee");
                }
                if (teamRand == 1)
                {
                    units[i] = new RangedUnit(x, y, 100, 100, 1, 10, 10, Teams(), "W", "Ranged");
                }
                if(teamRand == 2)
                {
                    units[i] = new Rogue(x, y, 100, 100, 1, 15, 5, Teams(), "V", "Rogue");
                }
                if(teamRand == 3)
                {
                    units[i] = new Dragon(x, y, 200, 200, 1, 20, 10, Teams(), "D", "Dragon");
                }
                mapArr[units[i].XPos, units[i].YPos] = units[i].Symbol;
            }

            for (int i = 0; i < buildings.Length; i++)
            {
                int x = rand.Next(0, 20);
                int y = rand.Next(0, 20);

                if (mapArr[x, y] == ".")
                {
                    x = rand.Next(0, 20);
                    y = rand.Next(0, 20);
                }
                int teamRand = rand.Next(0, 2);
                int buildingType = rand.Next(0, 2);

                if (teamRand == 0)
                {
                    if (buildingType == 0)
                    {
                        buildings[i] = new ResourceBuilding(x, y, 100, "W", "R", 3, 5, "Resource");
                    }
                    else
                    {
                        buildings[i] = new FactoryBuilding(x, y, 100, "W", "F", 5, 5, 1, "Factory");
                    }
                }
                if (teamRand == 1)
                {
                    if (buildingType == 0)
                    {
                        buildings[i] = new ResourceBuilding(x, y, 100, "F", "R", 3, 5, "Resource");//🏙️🏠
                    }
                    else
                    {
                        buildings[i] = new FactoryBuilding(x, y, 100, "F", "F", 5, 5, 1, "Factory");
                    }
                }

                mapArr[buildings[i].XPos, buildings[i].YPos] = buildings[i].Symbol;

            }

        }

        public void moveUnit()
        {
            int numArr = units.Length;

            for (int i = 0; i < numUnits; i++)
            {
                Unit temp = units[i].closestUnit(units);
                if(units[i].inRange(temp.XPos, temp.YPos) == false)
                {
                    if (units[i].XPos <= temp.XPos)
                    {
                        units[i].updatePos("d");
                        mapArr[units[i].XPos, units[i].YPos] = units[i].Symbol;
                        mapArr[units[i].XPos - 1, units[i].YPos] = ".";
                    }

                    if (units[i].XPos >= temp.XPos)
                    {
                        units[i].updatePos("a");
                        mapArr[units[i].XPos, units[i].YPos] = units[i].Symbol;
                        mapArr[units[i].XPos + 1, units[i].YPos] = ".";
                    }

                    if (units[i].YPos <= temp.YPos)
                    {
                        units[i].updatePos("s");
                        mapArr[units[i].XPos, units[i].YPos] = units[i].Symbol;
                        mapArr[units[i].XPos, units[i].YPos - 1] = ".";
                    }

                    if (units[i].YPos >= temp.YPos)
                    {
                        units[i].updatePos("w");
                        mapArr[units[i].XPos, units[i].YPos] = units[i].Symbol;
                        mapArr[units[i].XPos, units[i].YPos + 1] = ".";
                    }
                }
                
                if(units[i].inRange(temp.XPos, temp.YPos) == true)
                {
                    if(units[i].isDead() == false)
                    {
                        units[i].Attack();
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
                                    mapArr[units[i].XPos, units[i].YPos] = units[i].Symbol;
                                    mapArr[units[i].XPos - 1, units[i].YPos] = ".";

                                }
                                break;
                            case 1:
                                {
                                    units[i].updatePos("a");
                                    mapArr[units[i].XPos, units[i].YPos] = units[i].Symbol;
                                    mapArr[units[i].XPos + 1, units[i].YPos] = ".";

                                }
                                break;
                            case 2:
                                {
                                    units[i].updatePos("s");
                                    mapArr[units[i].XPos, units[i].YPos] = units[i].Symbol;
                                    mapArr[units[i].XPos, units[i].YPos - 1] = ".";
                                }
                                break;
                            case 3:
                                {
                                    units[i].updatePos("w");
                                    mapArr[units[i].XPos, units[i].YPos] = units[i].Symbol;
                                    mapArr[units[i].XPos, units[i].YPos + 1] = ".";
                                }
                                break;
                        }

                    }
                }

                if (units[i].isDead() == true)
                {
                    mapArr[units[i].XPos, units[i].YPos] = ".";
                }
            }
            
        }
        public void close()
        {
            for (int k = 0; k < numUnits; k++)
            {
                units[k].closestUnit(units);
            }

        }

        public int numUnit()
        {
            return units.Length;
        }
        public int numBuild()
        {
            return numBuildings;
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

        public string Redraw()
        {
            string value = "";
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    value += mapArr[j, i];
                }
                value += "\n";
            }
            return value;
        }

        public string UnitsCombo(int i) //returns the units to string for the  combo box
        {
            return units[i].ToString();
        }

        public string BuildingCombo(int i)
        {
            return buildings[i].ToString();
        }

        int arraySize;

        public void placeNewUnit(int counter) //places new unit
        {
            arraySize = units.Length + 1;
            for (int i = 0; i < numBuildings - 1; i++)
            {
                string buildingType = buildings[i].GetType().ToString();
                string[] splitBuilding = buildingType.Split('.');
                buildingType = splitBuilding[splitBuilding.Length - 1];

                if (buildingType == "FactoryBuilding")
                {
                    FactoryBuilding temp = (FactoryBuilding)buildings[i];

                    if (counter % temp.UnitTick == 0)
                    {
                        Array.Resize(ref units, arraySize);
                        units[arraySize - 1] = temp.SpawnUnit(counter);

                        mapArr[buildings[i].XPos + 1, buildings[i].YPos] = units[i].Symbol;

                    }
                }
            }
        }

        public void PlaceNewResource(int counter) //places the resource on the map
        {
            for (int i = 0; i < numBuildings - 1; i++)
            {
                string buildingType = buildings[i].GetType().ToString();
                string[] splitBuilding = buildingType.Split('.');
                buildingType = splitBuilding[splitBuilding.Length - 1];

                if (buildingType == "ResourceBuilding")
                {
                    ResourceBuilding temp = (ResourceBuilding)buildings[i];
                    temp.Resources(counter);

                    if (temp.Resources(counter - 1) == true)
                    {
                        int x = rand.Next(0, 20);
                        int y = rand.Next(0, 20);

                        if (mapArr[x, y] == ".")
                        {
                            mapArr[x, y] = "x";
                        }
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
                    units[i] = new RangedUnit(x, y, hp, 100, 1, 10, 5, team, symbol, type);
                }
                if (type == "Melee")
                {
                    units[i] = new MeleeUnit(x, y, hp, 100, 1, 10, 10, team, symbol, type);
                }
                if(type == "Rogue")
                {
                    units[i] = new Rogue(x, y, hp, 100, 1, 15, 5, team, symbol, type);
                }
                if(type == "Dragon")
                {
                    units[i] = new Dragon(x, y, hp, 200, 1, 20, 10, team, symbol, type);
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
                    buildings[i] = new ResourceBuilding(bX, bY, bHp, bSymbol, "R", 3, 5, bType);
                }
                if (bType == "Factory")
                {
                    buildings[i] = new FactoryBuilding(bX, bY, bHp, bSymbol, "F", 5, 5, 1, bType);
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
            for(int i = 0; i < numUnits; i++)
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
            for(int i = 0; i < numUnits; i++)
            {
                int unicode = rand.Next(0, 10001);
                char character = (char)unicode;
                string text = character.ToString();
                units[i].Symbol = text;
                mapArr[units[i].XPos, units[i].YPos] = units[i].Symbol;
            }
        }
    }
}
