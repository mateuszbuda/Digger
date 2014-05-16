using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digger
{
    public partial class GameForm : Form
    {
        DiggerGame game;
        string username;

        public GameForm()
        {
            InitializeComponent();
            pictureBoxGuy.Image = graphics.guy;
            pictureBoxMissile.Image = graphics.missile;
            pictureBoxBomb.Image = graphics.bomb;
            pictureBoxInvicloak.Image = graphics.invicloak;
            pictureBoxBonus.Image = graphics.bonustime;
        }

        public IntPtr getDrawSurface()
        {
            return pictureBox.Handle;
        }

        public void setGame(DiggerGame game)
        {
            this.game = game;
        }

        public void updateGuy(int guy)
        {
            labelGuy.Text = guy.ToString();
        }

        public void updatePoints(int points)
        {
            labelPoints.Text = points.ToString();
        }

        public void updateLevel(int level)
        {
            labelLevel.Text = "Poziom " + level.ToString();
        }

        public void updateMissiles(int missiles)
        {
            labelMissile.Text = missiles.ToString();
        }

        public void updateBombs(int bombs)
        {
            labelBomb.Text = bombs.ToString();
        }

        public void updateInvicloacks(int invicloaks)
        {
            labelInvicloak.Text = invicloaks.ToString();
        }

        public void updateBonusTime(int bonusTime)
        {
            labelBonus.Text = bonusTime.ToString();
        }

        private void gameForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxGuy_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {

        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {

        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            game.start();
            labelUsername.Text = username;
            panelMenu.Visible = false;
        }

        private void buttonHighScores_Click(object sender, EventArgs e)
        {

        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {

        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            username = textBoxUsername.Text;
            labelUser.Text = "Gracz: " + username;
            panelLogin.Visible = false;
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
