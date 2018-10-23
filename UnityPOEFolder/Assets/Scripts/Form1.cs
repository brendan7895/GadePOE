using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Task1
{
    public partial class Form1 : Form
    {
        SoundPlayer player = new SoundPlayer(); //new soundplayer to play the music .wav

        GameEngine Game = new GameEngine();
        int time = 0;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            player.SoundLocation = ("DragonLair.wav"); //points the player to the location
            player.Play(); //starts playing when the form is loaded

            lblMap.Text = "";
            Game.start();
        }

        private void tmTick_Tick(object sender, EventArgs e)
        {
            
            lblMap.Text = Game.playGame();
            lblTime.Text = time.ToString();

            cmbInfo.Items.Clear();

            for (int i = 0; i < Game.numUnit(); i++) //add units to the combo box
            {
                cmbInfo.Items.Add(Game.UnitsString(i));
            }

            for(int i = 0; i < Game.numBuilding(); i++)
            {
                cmbInfo.Items.Add(Game.BuildInfo(i));
            }

            Game.PlaceNewUnit(time);
            Game.PlaceResource(time);

            time++;
        }

        private void btnStart_Click(object sender, EventArgs e) //starts the timer
        {
            tmTick.Enabled = true;
            tmTick.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmTick.Enabled = false;
            tmTick.Stop();           
        }

        private void cmbInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Game.SaveAll();
        }

        private void btnLoad_Click(object sender, EventArgs e) //loads a new game from the save file
        {
            cmbInfo.Items.Clear();
            lblMap.Text = "";
            Game.ReadAll();
            lblMap.Text = Game.playGame();

            for (int i = 0; i < Game.numUnit(); i++) //add units to the combo box
            {
                cmbInfo.Items.Add(Game.UnitsString(i));
            }
            for (int i = 0; i < Game.numBuilding(); i++)
            {
                cmbInfo.Items.Add(Game.BuildInfo(i));
            }
        }

        private void BtnEnd_Click(object sender, EventArgs e) //closes the form and saves the game
        {
            Game.SaveAll();
            this.Close();
        }

        private void btnChange_Click(object sender, EventArgs e) //changes the units teams
        {
            cmbInfo.Items.Clear();
            Game.changeTeams();
            lblMap.Text = Game.playGame();//play game redraws but also plays a turn. Causes everything to move when clicked

            for (int i = 0; i < Game.numUnit(); i++) //add units to the combo box
            {
                cmbInfo.Items.Add(Game.UnitsString(i));
            }
            for (int i = 0; i < Game.numBuilding(); i++)
            {
                cmbInfo.Items.Add(Game.BuildInfo(i));
            }
        }

        private void btnKill_Click(object sender, EventArgs e)// unit HP = 0
        {
            Game.End();
        }

        private void btnRandom_Click(object sender, EventArgs e) //random symbols for all untis
        {
            Game.randomSymbol();

            cmbInfo.Items.Clear();
            Game.changeTeams();
            lblMap.Text = Game.playGame();//play game redraws but also plays a turn. Causes everything to move when clicked

            for (int i = 0; i < Game.numUnit(); i++) //add units to the combo box
            {
                cmbInfo.Items.Add(Game.UnitsString(i));
            }
            for (int i = 0; i < Game.numBuilding(); i++)
            {
                cmbInfo.Items.Add(Game.BuildInfo(i));
            }
        }

        int buttonCheck = 1;

        private void btnMute_Click(object sender, EventArgs e) //mutes and unmutes the music
        {
            player.Stop();

            if(buttonCheck % 2 == 0) //allows the button to change after each click odd unmute even mute
            {
                player.Play();
            }
            buttonCheck++;
        }
    }
}
