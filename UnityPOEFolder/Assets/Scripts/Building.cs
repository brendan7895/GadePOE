using UnityEngine;

public abstract class Building
{
    protected int xPos;
    protected int yPos;
    protected int health;
    protected string team;
    protected string symbol;
    protected string type;
    protected int maxHP = 100;

    public int XPos
    {
        get
        {
            return xPos;
        }

        set
        {
            xPos = value;
        }
    }

    public int YPos
    {
        get
        {
            return yPos;
        }

        set
        {
            yPos = value;
        }
    }

    public string Symbol
    {
        get
        {
            return symbol;
        }

        set
        {
            symbol = value;
        }
    }

    public string Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public int MaxHP
    {
        get
        {
            return maxHP;
        }

        set
        {
            maxHP = value;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public Building(int xPos, int yPos, int health, string team, string symbol, string type)
    {
        this.XPos = xPos;
        this.YPos = yPos;
        this.health = health;
        this.team = team;
        this.Symbol = symbol;
        this.type = type;
    }

    public abstract bool isDead();
    public abstract string ToString();
    public abstract void SaveBuilding();

    public string DetermineBuildHP(int hp, int maxHP)
    {
        string returnVal = "HP";
        double result = ((double)hp / (double)maxHP) * 20;
        int num = Mathf.CeilToInt((float)result);
        returnVal += num;
        return returnVal;
    }
}

