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
    class DynamosLaser:Game
    {
        public int DynamosLaserSpeed = 1;
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
            Dynamoslaser.BorderStyle = BorderStyle.FixedSingle;
           
        
            Dynamoslaser.BringToFront();
            panel.Controls.Add(Dynamoslaser);

            DynamosLaserTimer.Interval = DynamosLaserSpeed;
            DynamosLaserTimer.Tick += new EventHandler(DynamosLaserTimerEvent);
            DynamosLaserTimer.Start();
           

          
        }
        public void DynamosLaserTimerEvent(object sender, EventArgs e)
        {
            Direction = new Vector2(Dynamoslaser.Left, Dynamoslaser.Top) + Velocity;

                Dynamoslaser.Left = (int)Direction.X;
            Dynamoslaser.Top = (int)Direction.Y;
            if (Dynamoslaser.Left < 0 || Dynamoslaser.Right >ClientSize.Width|| Dynamoslaser.Top <0||Dynamoslaser.Top> ClientSize.Height)
            {
                
                DynamosLaserTimer.Stop();
                DynamosLaserTimer.Dispose();
                Dynamoslaser.Dispose();
                DynamosLaserTimer = null;
                Dynamoslaser = null;



            }


        }
    }
}
