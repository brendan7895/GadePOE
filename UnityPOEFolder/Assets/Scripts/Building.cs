namespace Task1
{
    public abstract class Building
    {
        protected int xPos;
        protected int yPos;
        protected int health;
        protected string team;
        protected string symbol;
        protected string type;

        public int XPos { get => xPos;}
        public int YPos { get => yPos;}
        public string Symbol { get => symbol; set => symbol = value; }

        public Building(int xPos, int yPos, int health, string team, string symbol, string type)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.health = health;
            this.team = team;
            this.Symbol = symbol;
            this.type = type;
        }

        public abstract bool isDead();
        public abstract string ToString();
        public abstract void SaveBuilding();
    }
}
