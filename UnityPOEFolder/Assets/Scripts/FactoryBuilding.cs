using System.IO;

namespace Task1
{
    public class FactoryBuilding : Building
    {
        System.Random rand = new System.Random();

        private int numberOfUnits;
        private int unitTick;
        private int spawnPoint;

        public int UnitTick { get => unitTick; set => unitTick = value; }

        public FactoryBuilding(int xPos, int yPos, int health, string team, string symbol, int numberOfUnits, int unitTick, int spawnPoint, string type) : base(xPos, yPos, health, team, symbol, type)
        {
            this.numberOfUnits = numberOfUnits;
            this.unitTick = unitTick;
            this.spawnPoint = spawnPoint;
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
            return type + "," + symbol + "," + xPos + "," + yPos + "," + health;
        }

        public Unit SpawnUnit(int counter)
        {
            Unit temp = null;

            if(counter % unitTick == 0 && numberOfUnits > 0)
            {
                int choice = rand.Next(0, 2);

                switch(choice)
                {
                    case 0:
                        {
                            temp = new MeleeUnit(XPos, YPos, 100, 100, 1, 10, 5, Teams().ToLower(), "L", "Melee");
                        }
                        break;
                    case 1:
                        {
                            temp = new RangedUnit(XPos, YPos, 100, 100, 1, 10, 10, Teams(), "W", "Ranged");
                        }
                        break;
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
}
