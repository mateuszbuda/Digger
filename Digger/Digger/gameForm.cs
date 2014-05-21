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
    /// <summary>
    /// Form gry z panelami logowania, menu wszelkich podmenu araz samej gry.
    /// </summary>
    public partial class GameForm : Form
    {
        /// <summary>
        /// Referencja do wyświetlanej gry
        /// </summary>
        DiggerGame game;
        /// <summary>
        /// Aktualnie zalogowany użytkownik
        /// </summary>
        string username;

        /// <summary>
        /// Domyślny konstruktor
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
            pictureBoxGuy.Image = graphics.guy;
            pictureBoxMissile.Image = graphics.missile;
            pictureBoxBomb.Image = graphics.bomb;
            pictureBoxInvicloak.Image = graphics.invicloak;
            pictureBoxBonus.Image = graphics.bonustime;
        }

        /// <summary>
        /// Metoda udostępniająca uchwyt na obiekt, na którym jest rysowana gra
        /// </summary>
        /// <returns></returns>
        public IntPtr getDrawSurface()
        {
            return pictureBox.Handle;
        }

        /// <summary>
        /// Metoda do ustawienia obiektu gry
        /// </summary>
        /// <param name="game"></param>
        public void setGame(DiggerGame game)
        {
            this.game = game;
        }

        /// <summary>
        /// Metoda aktualizująca wyświetlany stan życia głównego bohatera
        /// </summary>
        /// <param name="guy">Aktualna liczba życ bohatea</param>
        public void updateGuy(int guy)
        {
            labelGuy.Text = guy.ToString();
        }

        /// <summary>
        /// Metoda aktualizująca wyświetlane punkty gracza
        /// </summary>
        /// <param name="points">Aktualna liczba punktów gracza</param>
        public void updatePoints(int points)
        {
            labelPoints.Text = points.ToString();
        }

        /// <summary>
        /// Metoda aktualizująca wyświetlany poziom
        /// </summary>
        /// <param name="level">Aktualny poziom gry</param>
        public void updateLevel(int level)
        {
            labelLevel.Text = "Poziom " + level.ToString();
        }

        /// <summary>
        /// Metoda aktualizująca wyświetlaną dostępną liczbę pocisków
        /// </summary>
        /// <param name="missiles">Aktualna liczba dostępnych pocisków</param>
        public void updateMissiles(int missiles)
        {
            labelMissile.Text = missiles.ToString();
        }

        /// <summary>
        /// Metoda aktualizująca wyświetlaną dostępną liczbę bomb
        /// </summary>
        /// <param name="bombs">Aktualna dostępna liczba bomb</param>
        public void updateBombs(int bombs)
        {
            labelBomb.Text = bombs.ToString();
        }

        /// <summary>
        /// Metoda aktualizująca wyświetlaną dostępną liczbę peleryn
        /// </summary>
        /// <param name="invicloaks">Aktualna dostępna liczba peleryn</param>
        public void updateInvicloacks(int invicloaks)
        {
            labelInvicloak.Text = invicloaks.ToString();
        }

        /// <summary>
        /// Metoda aktualizująca wyświetlaną liczbę sekund pozostałego czasu bonusowego
        /// </summary>
        /// <param name="bonusTime">Liczba sekund jak pozostała do końca czasu bonusowego</param>
        public void updateBonusTime(int bonusTime)
        {
            labelBonus.Text = bonusTime.ToString();
        }

        /// <summary>
        /// Obsługa przycisku kontynuacji poprzedniej gry
        /// </summary>
        /// <param name="sender">Przycisk</param>
        /// <param name="e">Argumenty zdarzenia</param>
        private void buttonContinue_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Obsługa przycisku rozpoczęcia nowej gry
        /// </summary>
        /// <param name="sender">Przycisk</param>
        /// <param name="e">Argumenty zdarzenia</param>
        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            game.start();
            labelUsername.Text = username;
            panelMenu.Visible = false;
        }

        /// <summary>
        /// Obsługa przycisku wyświetlającego listę najlepszych wyników
        /// </summary>
        /// <param name="sender">Przycisk</param>
        /// <param name="e">Argumenty zdarzenia</param>
        private void buttonHighScores_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Obsługa przycisku wyświetlającego ustawienia
        /// </summary>
        /// <param name="sender">Przycisk</param>
        /// <param name="e">Argumenty zdarzenia</param>
        private void buttonSettings_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Obsługa przycisku wyświetlającego pomoc
        /// </summary>
        /// <param name="sender">Przycisk</param>
        /// <param name="e">Argumenty zdarzenia</param>
        private void buttonHelp_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Obsługa przycisku zamykającego grę
        /// </summary>
        /// <param name="sender">Przycisk</param>
        /// <param name="e">Argumenty zdarzenia</param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Obsługa logowania
        /// </summary>
        /// <param name="sender">Przycisk</param>
        /// <param name="e">Argumenty zdarzenia</param>
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            username = textBoxUsername.Text;
            labelUser.Text = "Gracz: " + username;
            panelLogin.Visible = false;
        }

        /// <summary>
        /// Obsługa zamknięcia aplikacji
        /// </summary>
        /// <param name="sender">Przycisk</param>
        /// <param name="e">Argumenty zdarzenia</param>
        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
