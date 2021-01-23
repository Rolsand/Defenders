using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace  DefendersGame
{
    class Laser:Form1
    {

        public string Direction;
        private int LaserSpeed = 20;
       private PictureBox laser = new PictureBox();
        private Timer LaserTimer = new Timer();
        public int LaserTop;
        public int LaserLeft;
        public Image LaserImage;

        public void MakeLaser(Panel form)
        {
            laser.Size = new System.Drawing.Size(10, 10);
            laser.SizeMode = PictureBoxSizeMode.StretchImage;
            laser.Tag = "Laser";
            laser.Image = LaserImage;
            laser.Top = LaserTop;
            laser.Left =LaserLeft;
            laser.BringToFront();
            form.Controls.Add(laser);

            LaserTimer.Interval = LaserSpeed;
            LaserTimer.Tick += new EventHandler(LaserTimerEvent);
            LaserTimer.Start();

           
         

        }

        public void LaserTimerEvent(object sender, EventArgs e)
        {
            if (Direction == "Right")
            {
                 laser.Left += LaserSpeed;
            }
            if (Direction == "Left")
            {
                laser.Left -= LaserSpeed;
            }
          if (laser.Left<0||laser.Left>ClientSize.Width||laser.Top<0||laser.Top>ClientSize.Width)
            {
                LaserTimer.Stop();
                LaserTimer.Dispose();
                laser.Dispose();
                LaserTimer = null;
                laser = null;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Laser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Name = "Laser";
            this.ResumeLayout(false);

        }
    }
}
