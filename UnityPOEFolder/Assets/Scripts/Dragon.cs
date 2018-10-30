using System;
using System.IO;
using UnityEngine;


public class Dragon : Unit
{
    public Dragon(int xPos, int yPos, int HP, int maxHP, int speed, int attack, int atkRange, string team, string symbol, string name) : base(xPos, yPos, HP, maxHP, speed, attack, atkRange, team, symbol, name)
    {

    }

    public override void AttackUnit()
    {
        HP -= attack;
    }

    public override bool inRange(int enemyX, int enemyY)
    {
        bool value = false;
        int x = Math.Abs(xPos - enemyX);
        int y = Math.Abs(yPos - enemyY);
        int abs = x + y;

        if (abs <= atkRange)
        {
            value = true;
        }
        return value;
    }

    public override bool isDead()
    {
        bool value = false;
        if (HP <= 0)
        {
            value = true;
        }
        return value;
    }

    public override string ToString()
    {
        if (HP <= 0)
        {
            symbol = "Dead";
        }
        return Name + "," + symbol + "," + team + "," + xPos + "," + yPos + "," + HP;
    }

    public override void SaveUnit() //saves the unit to string to a text file
    {
        if (Directory.Exists("saves") != true)
        {
            Directory.CreateDirectory("saves");

        }

        FileStream file = new FileStream("saves/UnitSave.file", FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(file);
        writer.WriteLine(ToString());
        writer.Close();
        file.Close();
    }
}

