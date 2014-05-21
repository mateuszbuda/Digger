using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;

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
            richTextBoxHelp.Text = "Plansza gry składa się z pól, z których część jest odkopana a część zakopana. Wchodząc na pole zakopane gracz odkopuje to pole. Niektórzy przeciwnicy również mają możliwość odkopywania pól.Głównym celem gry na każdym poziomie jest zebranie wszystkich diamentów, które są na planszy. Po ich zebraniu, gracz automatycznie przechodzi do następnego poziomu i otrzymuje k∙300 punktów, gdzie k to numer ukończonego poziomu. Dodatkowo, jeśli gracz zniszczył co najmniej 90% przeciwników na danym poziomie, otrzymuje dodatkowe życie. Z każdym poziomem na planszy jest coraz więcej trudniejszych przeciwników.";
            richTextBoxHelp.Text += "Początkowo bohater ma trzy życia i nie ma do dyspozycji żadnej broni. Jego zadaniem jest zebranie wszystkich diamentów na kolejnych poziomach. Próbują mu w tym przeszkodzić przeciwnicy, których liczba i trudność wzrastają z kolejnymi poziomami.";
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
        /// Otwiera panel ustawień
        /// </summary>
        public void openSettings()
        {
            if (!panelLogin.Visible)
                panelSettings.Visible = true;
        }

        /// <summary>
        /// Otwiera panel pomocy
        /// </summary>
        public void openHelp()
        {
            if (!panelLogin.Visible)
                panelHelp.Visible = true;
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
            panelSettings.Visible = true;
        }

        /// <summary>
        /// Obsługa przycisku wyświetlającego pomoc
        /// </summary>
        /// <param name="sender">Przycisk</param>
        /// <param name="e">Argumenty zdarzenia</param>
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            panelHelp.Visible = true;
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

        /// <summary>
        /// Wyjście z panelu Ustawień
        /// </summary>
        /// <param name="sender">Nadawca zradzenia</param>
        /// <param name="e">Argumenty zdarzenia</param>
        private void button1_Click(object sender, EventArgs e)
        {
            panelSettings.Visible = false;
        }

        /// <summary>
        /// Obsługa pojawienia się panelu ustawień
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia</param>
        private void panelSettings_VisibleChanged(object sender, EventArgs e)
        {
            if (panelSettings.Visible)
            {
                textSettingsBomb.Text = Settings.bomb.ToString();
                textSettingsCloak.Text = Settings.invclk.ToString();
                textSettingsDown.Text = Settings.down.ToString();
                textSettingsFire.Text = Settings.fire.ToString();
                textSettingsHelp.Text = Settings.help.ToString();
                textSettingsLeft.Text = Settings.left.ToString();
                textSettingsPause.Text = Settings.pause.ToString();
                textSettingsRight.Text = Settings.right.ToString();
                textSettingsSave.Text = Settings.save.ToString();
                textSettingsSettings.Text = Settings.options.ToString();
                textSettingsUp.Text = Settings.up.ToString();
            }
        }

        /// <summary>
        /// Konwerter przycisku WinFormsów na Przyciski XNA.
        /// </summary>
        /// <param name="k">Przeycisk WinFormsowy</param>
        /// <returns>Przycisk XNA</returns>
        private Microsoft.Xna.Framework.Input.Keys getKey(System.Windows.Forms.Keys k)
        {
            return (Microsoft.Xna.Framework.Input.Keys)Enum.Parse(typeof(Microsoft.Xna.Framework.Input.Keys), k.ToString());
        }

        /// <summary>
        /// Zmiana ustwienia przycisku ruchu w lewo
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia z nowym przyciskiem</param>
        private void textSettingsLeft_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Left)
                textSettingsLeft.Text = "Left";
            Settings.left = getKey(e.KeyCode);
        }

        /// <summary>
        /// Zmiana ustwienia przycisku ruchu w prawo
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia z nowym przyciskiem</param>
        private void textSettingsRight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Right)
                textSettingsRight.Text = "Right";
            Settings.right = getKey(e.KeyCode);
        }


        /// <summary>
        /// Zmiana ustwienia przycisku ruchu do góry
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia z nowym przyciskiem</param>
        private void textSettingsUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Up)
                textSettingsUp.Text = "Up";
            Settings.up = getKey(e.KeyCode);
        }

        /// <summary>
        /// Zmiana ustwienia przycisku ruchu w dół
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia z nowym przyciskiem</param>
        private void textSettingsDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Down)
                textSettingsDown.Text = "Down";
            Settings.down = getKey(e.KeyCode);
        }

        /// <summary>
        /// Zmiana ustwienia przycisku zastwienia bomby
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia z nowym przyciskiem</param>
        private void textSettingsBomb_KeyDown(object sender, KeyEventArgs e)
        {
            Settings.bomb = getKey(e.KeyCode);
        }

        /// <summary>
        /// Zmiana ustwienia przycisku użycia peleryny
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia z nowym przyciskiem</param>
        private void textSettingsCloak_KeyDown(object sender, KeyEventArgs e)
        {
            Settings.invclk = getKey(e.KeyCode);
        }

        /// <summary>
        /// Zmiana ustwienia przycisku wystrzału
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia z nowym przyciskiem</param>
        private void textSettingsFire_KeyDown(object sender, KeyEventArgs e)
        {
            Settings.fire = getKey(e.KeyCode);
        }

        /// <summary>
        /// Zmiana ustwienia przycisku pauzy
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia z nowym przyciskiem</param>
        private void textSettingsPause_KeyDown(object sender, KeyEventArgs e)
        {
            Settings.pause = getKey(e.KeyCode);
        }

        /// <summary>
        /// Zmiana ustwienia przycisku pomocy
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia z nowym przyciskiem</param>
        private void textSettingsHelp_KeyDown(object sender, KeyEventArgs e)
        {
            Settings.help = getKey(e.KeyCode);
        }

        /// <summary>
        /// Zmiana ustwienia przycisku ustwień
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia z nowym przyciskiem</param>
        private void textSettingsSettings_KeyDown(object sender, KeyEventArgs e)
        {
            Settings.options = getKey(e.KeyCode);
        }


        /// <summary>
        /// Zmiana ustwienia przycisku zapisu gry
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia z nowym przyciskiem</param>
        private void textSettingsSave_KeyDown(object sender, KeyEventArgs e)
        {
            Settings.save = getKey(e.KeyCode);
        }


        /// <summary>
        /// Zamknięcie panelu pomocy
        /// </summary>
        /// <param name="sender">Nadawca</param>
        /// <param name="e">Argumenty zdarzenia</param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            panelHelp.Visible = false;
        }

    }
}
