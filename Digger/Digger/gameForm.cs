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
    public partial class gameForm : Form
    {
        public gameForm()
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

        private void gameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void gameForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxGuy_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {

        }
    }
}
