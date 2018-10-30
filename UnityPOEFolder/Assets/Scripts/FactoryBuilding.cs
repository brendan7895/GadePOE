using System.IO;
using UnityEngine;


public class FactoryBuilding : Building
{
    System.Random rand = new System.Random();
    float X_OFF, Y_OFF;
    const float PADDING = 5.12f;

    private int numberOfUnits;
    private int spawnPoint;

    public int NumberOfUnits
    {
        get
        {
            return numberOfUnits;
        }

        set
        {
            numberOfUnits = value;
        }
    }

    public FactoryBuilding(int xPos, int yPos, int health, string team, string symbol, int numberOfUnits, int spawnPoint, string type) : base(xPos, yPos, health, team, symbol, type)
    {
        this.numberOfUnits = numberOfUnits;
        this.spawnPoint = spawnPoint;
    }

    public override bool isDead()
    {
        bool value = false;
        if (health <= 0)
        {
            value = true;
        }
        return value;
    }

    public override string ToString()
    {
        return type + "," + symbol + "," + XPos + "," + YPos + "," + health;
    }

    public Unit SpawnUnit()
    {
        Unit temp = null;

        if (numberOfUnits >= 0)
        {
            int choice = rand.Next(0, 4);

            if (choice == 0) // S M 
            {
                temp = new MeleeUnit(XPos, YPos, 100, 100, 1, 5, 1, Teams(), "L", "Melee");
                if (temp.Team == "S" && type == "Factory")
                {
                    Instantiate(Resources.Load("Melee"), new Vector3(X_OFF + (temp.XPos * PADDING), Y_OFF + (-temp.YPos * PADDING), -1), Quaternion.identity);
                }
                if (temp.Team == "M" && type == "FactoryE")
                {
                    Instantiate(Resources.Load("MeleeEnemy"), new Vector3(X_OFF + (temp.XPos * PADDING), Y_OFF + (-temp.YPos * PADDING), -1), Quaternion.identity);
                }
            }
            if (choice == 1)
            {
                temp = new RangedUnit(XPos, YPos, 100, 100, 1, 10, 5, Teams(), "W", "Ranged");
                if (temp.Team == "S" && type == "Factory")
                {
                    Instantiate(Resources.Load("RangedUnit"), new Vector3(X_OFF + (temp.XPos * PADDING), Y_OFF + (-temp.YPos * PADDING), 0), Quaternion.identity);
                }
                if (temp.Team == "M" && type == "FactoryE")
                {
                    Instantiate(Resources.Load("RangedUnitEnemy"), new Vector3(X_OFF + (temp.XPos * PADDING), Y_OFF + (-temp.YPos * PADDING), 0), Quaternion.identity);
                }
            }
            if (choice == 2)
            {
                temp = new Rogue(XPos, YPos, 100, 100, 1, 12, 5, Teams(), "V", "Rogue");
                if (temp.Team == "S" && type == "Factory")
                {
                    Instantiate(Resources.Load("Rogue"), new Vector3(X_OFF + (temp.XPos * PADDING), Y_OFF + (-temp.YPos * PADDING), 0), Quaternion.identity);
                }
                if (temp.Team == "M" && type == "FactoryE")
                {
                    Instantiate(Resources.Load("RogueEnemy"), new Vector3(X_OFF + (temp.XPos * PADDING), Y_OFF + (-temp.YPos * PADDING), 0), Quaternion.identity);
                }
            }
            if (choice == 3)
            {
                temp = new Dragon(XPos, YPos, 200, 200, 1, 15, 6, Teams(), "D", "Dragon");
                if (temp.Team == "S" && type == "Factory")
                {
                    Instantiate(Resources.Load("Dragon"), new Vector3(X_OFF + (temp.XPos * PADDING), Y_OFF + (-temp.YPos * PADDING), 0), Quaternion.identity);
                }
                if (temp.Team == "M" && type == "FactoryE")
                {
                    Instantiate(Resources.Load("DragonEnemy"), new Vector3(X_OFF + (temp.XPos * PADDING), Y_OFF + (-temp.YPos * PADDING), 0), Quaternion.identity);
                }
            }
            numberOfUnits--;
        }
        return temp;
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

    public override void SaveBuilding()
    {
        if (Directory.Exists("saves") != true)
        {
            Directory.CreateDirectory("saves");

        }

        FileStream build = new FileStream("saves/BuildingSave.file", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(build);
        writer.WriteLine(ToString());
        writer.Close();
        build.Close();
    }
}

