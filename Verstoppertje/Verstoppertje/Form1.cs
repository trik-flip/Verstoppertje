using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using Newtonsoft.Json;
using System.Net;
using System.ComponentModel.Design;
using System.Threading;
using Verstoppertje.Connection;

namespace Verstoppertje
{
    public partial class Zoeker_App : Form
    {
        private Game game;
        /// <summary>
        /// Runs front-end and opens Back-end in another Thread
        /// </summary>
        public Zoeker_App()
        {
            InitializeComponent();
            game = new Game(this);
            game.Start();
        }

        /// <summary>
        /// Set's the picture displayd in the camera Feed pictureBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if(game != null && game.CameraCheck())
            {
                if(SelectCameraFeed.Text == "Kitchen")
                    CameraFeed.Image = Properties.Resources.Kitchen;
                else if(SelectCameraFeed.Text == "Living")
                    CameraFeed.Image = Properties.Resources.Living;
                else if(SelectCameraFeed.Text == "Family")
                    CameraFeed.Image = Properties.Resources.Family;
                else if(SelectCameraFeed.Text == "Pantry")
                    CameraFeed.Image = Properties.Resources.Pantry;
                else if(SelectCameraFeed.Text == "Bathroom")
                    CameraFeed.Image = Properties.Resources.Bathroom;
                else if(SelectCameraFeed.Text == "Entrance")
                    CameraFeed.Image = Properties.Resources.Entrance;
                else if(SelectCameraFeed.Text == "Entrance 2")
                    CameraFeed.Image = Properties.Resources.Entrance_2;
                else if(SelectCameraFeed.Text == "Laundry")
                    CameraFeed.Image = Properties.Resources.Laundry;
            }
        }

        #region RichTextBox
        /// <summary>
        /// Is used to add text to the RichTextBox
        /// is used by another Thread
        /// </summary>
        /// <param name="s"></param>
        public void addToRichTextBox(string s)
        {
            if(InvokeRequired)
            {
                Invoke(new Action<string>(addToRichTextBox), new object[] { s });
                return;
            }
            richTextBox1.Text += s;
        }
        /// <summary>
        /// Is used to set the text of the RichTextBox
        /// Is used by another Thread
        /// </summary>
        /// <param name="s"></param>
        public void SetRichTextBox(string s)
        {
            if(InvokeRequired)
            {
                Invoke(new Action<string>(SetRichTextBox), new object[] { s });
                return;
            }
            richTextBox1.Text = s;
        }
        #endregion
        // TODO: Set to white if power-up not active
        /// <summary>
        /// Is used by the Game class, to set a picture to the Front-end
        /// Is used by another Thread then the Front-end is run by
        /// </summary>
        /// <param name="image"></param>
        /// <param name="s"></param>
        public void SetPicture(Image image, string s)
        {
            if(InvokeRequired)
            {
                Invoke(new Action<Image, string>(SetPicture), new object[] { image, s });
                return;
            }
            if(s == "Kitchen")
                pictureBox_Kitchen.Image = image;
            else if(s == "Living")
                pictureBox_Living.Image = image;
            else if(s == "Family")
                pictureBox_Family.Image = image;
            else if(s == "Pantry")
                pictureBox_Pantry.Image = image;
            else if(s == "Bathroom")
                pictureBox_Bathroom.Image = image;
            else if(s == "Entrance")
                pictureBox_Entrance.Image = image;
            else if(s == "Entrance 2")
                pictureBox_Entrance2.Image = image;
            else if(s == "Laundry")
                pictureBox_Laundry.Image = image;

        }
        /// <summary>
        /// Used to trigger a Power-up in the Game Class
        /// is used in it's own thread so the Front-end won't hang it self
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPowerUp_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(game.ActivatePowerUp);
            thread.Start();
        }

        #region toggle lamps
        private void pictureBox_Kitchen_Click(object sender, EventArgs e)
        {
            game.ToggleLamp("Lampje - Kitchen");
            if(game.checkKnop("Kitchen"))
            { game.Win(); }
        }
        private void pictureBox_Living_Click(object sender, EventArgs e)
        {
            game.ToggleLamp("Lampje - Living");
            if(game.checkKnop("Living"))
            { game.Win(); }
        }
        private void pictureBox_Laundry_Click(object sender, EventArgs e)
        {
            game.ToggleLamp("Lampje - Laundry");
            if(game.checkKnop("Laundry"))
            { game.Win(); }
        }
        private void pictureBox_Pantry_Click(object sender, EventArgs e)
        {
            game.ToggleLamp("Lampje - Pantry");
            if(game.checkKnop("Pantry"))
            { game.Win(); }
        }
        private void pictureBox_Entrance2_Click(object sender, EventArgs e)
        {
            game.ToggleLamp("Lampje - Entrance 2");
            if(game.checkKnop("Entrance 2"))
            { game.Win(); }
        }
        private void pictureBox_Bathroom_Click(object sender, EventArgs e)
        {
            game.ToggleLamp("Lampje - Bathroom");
            if(game.checkKnop("Bathroom"))
            { game.Win(); }
        }
        private void pictureBox_Entrance_Click(object sender, EventArgs e)
        {
            game.ToggleLamp("Lampje - Entrance");
            if(game.checkKnop("Entrance"))
            { game.Win(); }
        }
        private void pictureBox_Family_Click(object sender, EventArgs e)
        {
            game.ToggleLamp("Lampje - Family");
            if(game.checkKnop("Family"))
            { game.Win(); }
        }
        #endregion

        #region hover Floorplan
        private void pictureBox_Kitchen_MouseHover(object sender, EventArgs e)
        {
            SelectCameraFeed.Text = "Kitchen";
        }
        private void pictureBox_Living_MouseHover(object sender, EventArgs e)
        {
            SelectCameraFeed.Text = "Living";

        }
        private void pictureBox_Pantry_MouseHover(object sender, EventArgs e)
        {
            SelectCameraFeed.Text = "Pantry";

        }
        private void pictureBox_Laundry_MouseHover(object sender, EventArgs e)
        {
            SelectCameraFeed.Text = "Laundry";

        }
        private void pictureBox_Bathroom_MouseHover(object sender, EventArgs e)
        {
            SelectCameraFeed.Text = "Bathroom";

        }
        private void pictureBox_Entrance2_MouseHover(object sender, EventArgs e)
        {
            SelectCameraFeed.Text = "Entrance 2";

        }
        private void pictureBox_Entrance_MouseHover(object sender, EventArgs e)
        {
            SelectCameraFeed.Text = "Entrance";

        }
        private void pictureBox_Family_MouseHover(object sender, EventArgs e)
        {
            SelectCameraFeed.Text = "Family";

        }
        #endregion
    }
}
