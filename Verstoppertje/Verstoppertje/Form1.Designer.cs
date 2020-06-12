namespace Verstoppertje
{
    partial class Zoeker_App
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Zoeker_App));
            this.CameraFeedButton = new System.Windows.Forms.Button();
            this.SelectCameraFeed = new System.Windows.Forms.ComboBox();
            this.FloorPlan = new System.Windows.Forms.PictureBox();
            this.CameraFeed = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonPowerUp = new System.Windows.Forms.Button();
            this.pictureBox_Kitchen = new System.Windows.Forms.PictureBox();
            this.pictureBox_Living = new System.Windows.Forms.PictureBox();
            this.pictureBox_Family = new System.Windows.Forms.PictureBox();
            this.pictureBox_Bathroom = new System.Windows.Forms.PictureBox();
            this.pictureBox_Laundry = new System.Windows.Forms.PictureBox();
            this.pictureBox_Pantry = new System.Windows.Forms.PictureBox();
            this.pictureBox_Entrance2 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Entrance = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.FloorPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraFeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Kitchen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Living)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Family)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Bathroom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Laundry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Pantry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Entrance2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Entrance)).BeginInit();
            this.SuspendLayout();
            // 
            // CameraFeedButton
            // 
            this.CameraFeedButton.Location = new System.Drawing.Point(12, 43);
            this.CameraFeedButton.Name = "CameraFeedButton";
            this.CameraFeedButton.Size = new System.Drawing.Size(122, 23);
            this.CameraFeedButton.TabIndex = 0;
            this.CameraFeedButton.Text = "SelectFeed";
            this.CameraFeedButton.UseVisualStyleBackColor = true;
            this.CameraFeedButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // SelectCameraFeed
            // 
            this.SelectCameraFeed.FormattingEnabled = true;
            this.SelectCameraFeed.Items.AddRange(new object[] {
            "Family",
            "Living",
            "Kitchen",
            "Bathroom",
            "Pantry",
            "Laundry",
            "Entrance",
            "Entrance 2"});
            this.SelectCameraFeed.Location = new System.Drawing.Point(13, 13);
            this.SelectCameraFeed.Name = "SelectCameraFeed";
            this.SelectCameraFeed.Size = new System.Drawing.Size(121, 24);
            this.SelectCameraFeed.TabIndex = 1;
            this.SelectCameraFeed.Text = "Family";
            // 
            // FloorPlan
            // 
            this.FloorPlan.Image = ((System.Drawing.Image)(resources.GetObject("FloorPlan.Image")));
            this.FloorPlan.Location = new System.Drawing.Point(354, 43);
            this.FloorPlan.Name = "FloorPlan";
            this.FloorPlan.Size = new System.Drawing.Size(798, 569);
            this.FloorPlan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FloorPlan.TabIndex = 3;
            this.FloorPlan.TabStop = false;
            // 
            // CameraFeed
            // 
            this.CameraFeed.Location = new System.Drawing.Point(12, 276);
            this.CameraFeed.Name = "CameraFeed";
            this.CameraFeed.Size = new System.Drawing.Size(336, 336);
            this.CameraFeed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CameraFeed.TabIndex = 4;
            this.CameraFeed.TabStop = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(13, 72);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(334, 198);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // buttonPowerUp
            // 
            this.buttonPowerUp.Location = new System.Drawing.Point(141, 13);
            this.buttonPowerUp.Name = "buttonPowerUp";
            this.buttonPowerUp.Size = new System.Drawing.Size(142, 54);
            this.buttonPowerUp.TabIndex = 7;
            this.buttonPowerUp.Text = "Use Power-Up";
            this.buttonPowerUp.UseVisualStyleBackColor = true;
            this.buttonPowerUp.Click += new System.EventHandler(this.buttonPowerUp_Click);
            // 
            // pictureBox_Kitchen
            // 
            this.pictureBox_Kitchen.Location = new System.Drawing.Point(382, 63);
            this.pictureBox_Kitchen.Name = "pictureBox_Kitchen";
            this.pictureBox_Kitchen.Size = new System.Drawing.Size(249, 285);
            this.pictureBox_Kitchen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Kitchen.TabIndex = 8;
            this.pictureBox_Kitchen.TabStop = false;
            this.pictureBox_Kitchen.Click += new System.EventHandler(this.pictureBox_Kitchen_Click);
            this.pictureBox_Kitchen.MouseHover += new System.EventHandler(this.pictureBox_Kitchen_MouseHover);
            // 
            // pictureBox_Living
            // 
            this.pictureBox_Living.Location = new System.Drawing.Point(637, 176);
            this.pictureBox_Living.Name = "pictureBox_Living";
            this.pictureBox_Living.Size = new System.Drawing.Size(446, 233);
            this.pictureBox_Living.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Living.TabIndex = 9;
            this.pictureBox_Living.TabStop = false;
            this.pictureBox_Living.Click += new System.EventHandler(this.pictureBox_Living_Click);
            this.pictureBox_Living.MouseHover += new System.EventHandler(this.pictureBox_Living_MouseHover);
            // 
            // pictureBox_Family
            // 
            this.pictureBox_Family.Location = new System.Drawing.Point(862, 407);
            this.pictureBox_Family.Name = "pictureBox_Family";
            this.pictureBox_Family.Size = new System.Drawing.Size(231, 189);
            this.pictureBox_Family.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Family.TabIndex = 10;
            this.pictureBox_Family.TabStop = false;
            this.pictureBox_Family.Click += new System.EventHandler(this.pictureBox_Family_Click);
            this.pictureBox_Family.MouseHover += new System.EventHandler(this.pictureBox_Family_MouseHover);
            // 
            // pictureBox_Bathroom
            // 
            this.pictureBox_Bathroom.Location = new System.Drawing.Point(505, 492);
            this.pictureBox_Bathroom.Name = "pictureBox_Bathroom";
            this.pictureBox_Bathroom.Size = new System.Drawing.Size(126, 93);
            this.pictureBox_Bathroom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Bathroom.TabIndex = 11;
            this.pictureBox_Bathroom.TabStop = false;
            this.pictureBox_Bathroom.Click += new System.EventHandler(this.pictureBox_Bathroom_Click);
            this.pictureBox_Bathroom.MouseHover += new System.EventHandler(this.pictureBox_Bathroom_MouseHover);
            // 
            // pictureBox_Laundry
            // 
            this.pictureBox_Laundry.Location = new System.Drawing.Point(395, 394);
            this.pictureBox_Laundry.Name = "pictureBox_Laundry";
            this.pictureBox_Laundry.Size = new System.Drawing.Size(123, 190);
            this.pictureBox_Laundry.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Laundry.TabIndex = 12;
            this.pictureBox_Laundry.TabStop = false;
            this.pictureBox_Laundry.Click += new System.EventHandler(this.pictureBox_Laundry_Click);
            this.pictureBox_Laundry.MouseHover += new System.EventHandler(this.pictureBox_Laundry_MouseHover);
            // 
            // pictureBox_Pantry
            // 
            this.pictureBox_Pantry.Location = new System.Drawing.Point(395, 354);
            this.pictureBox_Pantry.Name = "pictureBox_Pantry";
            this.pictureBox_Pantry.Size = new System.Drawing.Size(123, 34);
            this.pictureBox_Pantry.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Pantry.TabIndex = 13;
            this.pictureBox_Pantry.TabStop = false;
            this.pictureBox_Pantry.Click += new System.EventHandler(this.pictureBox_Pantry_Click);
            this.pictureBox_Pantry.MouseHover += new System.EventHandler(this.pictureBox_Pantry_MouseHover);
            // 
            // pictureBox_Entrance2
            // 
            this.pictureBox_Entrance2.Location = new System.Drawing.Point(525, 355);
            this.pictureBox_Entrance2.Name = "pictureBox_Entrance2";
            this.pictureBox_Entrance2.Size = new System.Drawing.Size(106, 131);
            this.pictureBox_Entrance2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Entrance2.TabIndex = 14;
            this.pictureBox_Entrance2.TabStop = false;
            this.pictureBox_Entrance2.Click += new System.EventHandler(this.pictureBox_Entrance2_Click);
            this.pictureBox_Entrance2.MouseHover += new System.EventHandler(this.pictureBox_Entrance2_MouseHover);
            // 
            // pictureBox_Entrance
            // 
            this.pictureBox_Entrance.Location = new System.Drawing.Point(636, 415);
            this.pictureBox_Entrance.Name = "pictureBox_Entrance";
            this.pictureBox_Entrance.Size = new System.Drawing.Size(226, 168);
            this.pictureBox_Entrance.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Entrance.TabIndex = 15;
            this.pictureBox_Entrance.TabStop = false;
            this.pictureBox_Entrance.Click += new System.EventHandler(this.pictureBox_Entrance_Click);
            this.pictureBox_Entrance.MouseHover += new System.EventHandler(this.pictureBox_Entrance_MouseHover);
            // 
            // Zoeker_App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1201, 721);
            this.Controls.Add(this.pictureBox_Entrance);
            this.Controls.Add(this.pictureBox_Entrance2);
            this.Controls.Add(this.pictureBox_Pantry);
            this.Controls.Add(this.pictureBox_Bathroom);
            this.Controls.Add(this.pictureBox_Laundry);
            this.Controls.Add(this.pictureBox_Family);
            this.Controls.Add(this.pictureBox_Living);
            this.Controls.Add(this.pictureBox_Kitchen);
            this.Controls.Add(this.buttonPowerUp);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.CameraFeed);
            this.Controls.Add(this.FloorPlan);
            this.Controls.Add(this.SelectCameraFeed);
            this.Controls.Add(this.CameraFeedButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Zoeker_App";
            this.Text = "Zoeker App";
            ((System.ComponentModel.ISupportInitialize)(this.FloorPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraFeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Kitchen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Living)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Family)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Bathroom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Laundry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Pantry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Entrance2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Entrance)).EndInit();
            this.ResumeLayout(false);

        }

#endregion

        private System.Windows.Forms.Button CameraFeedButton;
        private System.Windows.Forms.ComboBox SelectCameraFeed;
        private System.Windows.Forms.PictureBox FloorPlan;
        private System.Windows.Forms.PictureBox CameraFeed;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonPowerUp;
        private System.Windows.Forms.PictureBox pictureBox_Kitchen;
        private System.Windows.Forms.PictureBox pictureBox_Living;
        private System.Windows.Forms.PictureBox pictureBox_Family;
        private System.Windows.Forms.PictureBox pictureBox_Bathroom;
        private System.Windows.Forms.PictureBox pictureBox_Laundry;
        private System.Windows.Forms.PictureBox pictureBox_Pantry;
        private System.Windows.Forms.PictureBox pictureBox_Entrance2;
        private System.Windows.Forms.PictureBox pictureBox_Entrance;
    }
}

