using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Digger.Forms;

namespace Digger
{
    public partial class GameForm : Form
    {
        string username;

        public GameForm(string username)
        {
            InitializeComponent();
            pictureBoxGuy.Image = graphics.guy;
            pictureBoxMissile.Image = graphics.missile;
            pictureBoxBomb.Image = graphics.bomb;
            pictureBoxInvicloak.Image = graphics.invicloak;
            pictureBoxBonus.Image = graphics.bonustime;

            this.username = username;

            labelUsername.Text = username;
        }

        public IntPtr getDrawSurface()
        {
            return pictureBox.Handle;
        }

        private void gameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }

        private void gameForm_Load(object sender, EventArgs e)
        {
            using (DiggerGame game = new DiggerGame(getDrawSurface()))
            {
                game.Run();
            }
        }

        private void pictureBoxGuy_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {

        }
    }
}
