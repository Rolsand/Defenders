using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Numerics;


namespace DefendersGame
{
    class DynamosLaser
    {
        public int DynamosLaserSpeed = 6;
        private PictureBox Dynamoslaser = new PictureBox();
        private Timer DynamosLaserTimer = new Timer();
        public int DynamosLaserTop;
        public int DynamosLaserLeft;
        public Vector2 Direction;
        public Vector2 Velocity;

       
       
        public Image DynamosLaserImage;

        public void MakedDynamosLaser(Panel panel)
        {
            Dynamoslaser.Size = new System.Drawing.Size(20, 20);
            Dynamoslaser.SizeMode = PictureBoxSizeMode.StretchImage;
            Dynamoslaser.Tag = "DynamosLaser";
            Dynamoslaser.Image = DynamosLaserImage;
            Dynamoslaser.Top = DynamosLaserTop;
            Dynamoslaser.Left = DynamosLaserLeft;
           
            
            Dynamoslaser.BringToFront();
            panel.Controls.Add(Dynamoslaser);

            DynamosLaserTimer.Interval = DynamosLaserSpeed;
            DynamosLaserTimer.Tick += new EventHandler(DynamosLaserTimerEvent);
            DynamosLaserTimer.Start();
           

          
        }
        public void DynamosLaserTimerEvent(object sender, EventArgs e)
        {
            Direction = Direction + Velocity;
            Dynamoslaser.Left = (int)Direction.X;
            Dynamoslaser.Top = (int)Direction.Y;


        }
    }
}
