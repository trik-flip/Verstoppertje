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
        private readonly string url = "http://localhost:8080/json.htm?username=cm9vdA==&password=cm9vdA==&";
        private string singelDevice = "type=devices&rid=";
        private WebClient client = new WebClient();
        private List<Switch> switches = new List<Switch>();
        public Zoeker_App()
        {
            InitializeComponent();
            Listener listener = new Listener(this);
            listener.start();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(SelectCameraFeed.Text == "Kitchen")
                this.CameraFeed.Image = Properties.Resources.Kitchen;
            else if(SelectCameraFeed.Text == "Living")
                this.CameraFeed.Image = Properties.Resources.Living;
            else if(SelectCameraFeed.Text == "Family")
                this.CameraFeed.Image = Properties.Resources.Family;
            else if(SelectCameraFeed.Text == "Pantry")
                this.CameraFeed.Image = Properties.Resources.Pantry;
            else if(SelectCameraFeed.Text == "Bathroom")
                this.CameraFeed.Image = Properties.Resources.Bathroom;
            else if(SelectCameraFeed.Text == "Entrance")
                this.CameraFeed.Image = Properties.Resources.Entrance;
            else if(SelectCameraFeed.Text == "Entrance 2")
                this.CameraFeed.Image = Properties.Resources.Entrance_2;
            else if(SelectCameraFeed.Text == "Laundry")
                this.CameraFeed.Image = Properties.Resources.Laundry;
        }
        public void addToRichTextBox(string s)
        {
            if(InvokeRequired)
            {
                this.Invoke(new Action<string>(addToRichTextBox), new object[] { s });
                return;
            }
            richTextBox1.Text += s;
        }
        public void SetRichTextBox(string s)
        {
            if(InvokeRequired)
            {
                this.Invoke(new Action<string>(SetRichTextBox), new object[] { s });
                return;
            }
            richTextBox1.Text = s;
        }
        private void pictureBox_Kitchen_Click(object sender, EventArgs e)
        {

        }
        public void SetPicture(Image image, string s)
        {
            if(InvokeRequired)
            {
                this.Invoke(new Action<Image,string>(SetPicture), new object[] { image,s });
                return;
            }
            if(s == "Kitchen")
                this.pictureBox_Kitchen.Image = image;
            else if(s == "Living")
                this.pictureBox_Living.Image = image;
            else if(s == "Family")
                this.pictureBox_Family.Image = image;
            else if(s == "Pantry")
                this.pictureBox_Pantry.Image = image;
            else if(s == "Bathroom")
                this.pictureBox_Bathroom.Image = image;
            else if(s == "Entrance")
                this.pictureBox_Entrance.Image = image;
            else if(s == "Entrance 2")
                this.pictureBox_Entrance2.Image = image;
            else if(s == "Laundry")
                this.pictureBox_Laundry.Image = image;
        }

        private void pictureBox_Living_Click(object sender, EventArgs e)
        {

        }
    }
}
