using System.IO;
using UnityEngine;

public class ResourceBuilding : Building
{
    protected string type;
    protected int total;

    public int Total
    {
        get
        {
            return total;
        }

        set
        {
            total = value;
        }
    }

    public ResourceBuilding(int xPos, int yPos, int health, string team, string symbol, int total, string type) : base(xPos, yPos, health, team, symbol, type)
    {
        this.type = type;
        this.Total = total;
    }

    public override bool isDead()
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string ToString()
    {
        return type + "," + symbol + "," + XPos + "," + YPos + "," + health;
    }

    public bool Resources()
    {
        bool value = false;
        Total--;
        value = true;
        if (Total <= 0)
        {
            value = false;
        }
        return value;
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

