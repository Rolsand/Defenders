using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace DefendersGame
{
    public partial class Form1 : Form
    {
        List<LanderState> EnemyList = new List<LanderState>();


        float Speed;
        int Level = 1;

        enum ShipMovement
        {
            None,
            Up,
            Down,
            Left,
            Right,
        }

        string Face = "Right";
        int Movementspeed = 12;

        ShipMovement Ship = ShipMovement.None;

        public Form1()
        {
            InitializeComponent();


            pgrHealth.Maximum = 100;
            pgrHealth.Value = 100;
            SpawnRate();


            this.panel1.Controls.Add(picSpaceShip);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Image RotateImage = picSpaceShip.Image;


            if (e.KeyCode == Keys.Up)
            {
                Ship = ShipMovement.Up;
            }
            else if (e.KeyCode == Keys.Down)
            {
                Ship = ShipMovement.Down;
            }
            else if (e.KeyCode == Keys.Left)
            {
                Ship = ShipMovement.Left;
                picSpaceShip.Image = Resource1.DefenderShipLeft;
                Face = "Left";
            }
            else if (e.KeyCode == Keys.Right)
            {
                Ship = ShipMovement.Right;
                picSpaceShip.Image = Resource1.DefenderShipRight;
                Face = "Right";
            }

            if (e.KeyCode == Keys.Space)
                NewLaser(Face);
            if (e.KeyCode == Keys.Tab)
            {
                SpawnRate();
            }
        }

        private void StartGame()
        {
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }


        private void tmrGame_Tick(object sender, EventArgs e)
        {
            PlayerMovement();

            // Collision check

            foreach (Control X in this.panel1.Controls)
            {
                if (X is PictureBox && X.Tag == "Lander")
                {
                    if (pgrHealth.Value <= 0)
                    {
                        tmrGame.Enabled = false;
                    }
                   else if (((PictureBox)X).Bounds.IntersectsWith(picSpaceShip.Bounds))
                    {
                        pgrHealth.Value -= 10;

                        MakeLander();
                        this.panel1.Controls.Remove(X);
                    }
                   
                }


                foreach (Control k in this.panel1.Controls)
                {
                    if ((k is PictureBox && (string)k.Tag == "Laser") && (X is PictureBox && (string)X.Tag == "Lander"))
                    {
                        if (k.Bounds.IntersectsWith(X.Bounds))
                        {
                            this.panel1.Controls.Remove(X);
                            X.Dispose();
                            MakeLander();
                        }
                    }
                }
            }


            //Move Lander
            foreach (LanderState landerState in EnemyList)
            {
                var x = landerState.LanderImage;
                if (x is PictureBox && x.Tag == "Lander")
                {
                    if (x.Top <= 0 || x.Bottom >= panel1.ClientSize.Height)
                    {
                        landerState.SetVelocityY(-1 * landerState.Velocity.Y);
                    }
                    else if (x.Left <= 0 || x.Left >= panel1.ClientSize.Width - x.Width)
                    {
                        landerState.SetVelocityX(-landerState.Velocity.X);
                    }

                    //MOVE THE BALL

                    //Set ball poostion 
                    //code to move paddles 


                    landerState.Position = new Vector(x.Left, x.Top) + landerState.Velocity;

                    x.Top = (int)landerState.Position.Y;
                    x.Left = (int)landerState.Position.X;
                }
            }
        }

        private void Form1_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                Ship = ShipMovement.None;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void PlayerMovement()
        {
            if (picSpaceShip.Top <= 0)
            {
                picSpaceShip.Top += Movementspeed;
            }
            // if the ship is less than zero on x-axis then move right
            else if (picSpaceShip.Left <= 0)
            {
                picSpaceShip.Left += Movementspeed;
            }
            //If bottom of the ship is greater than Client Height then move up
            else if (picSpaceShip.Bottom >= panel1.Height)
            {
                picSpaceShip.Top -= Movementspeed;
            }
            // If ship is greater than client width then move left.
            else if (picSpaceShip.Right >= panel1.Width)
            {
                picSpaceShip.Left -= Movementspeed;
            }

            if (Ship == ShipMovement.Up)
            {
                picSpaceShip.Top -= Movementspeed;
            }
            else if (Ship == ShipMovement.Down)
                picSpaceShip.Top += Movementspeed;
            else if (Ship == ShipMovement.Right)
                picSpaceShip.Left += Movementspeed;
            else if (Ship == ShipMovement.Left)
                picSpaceShip.Left -= Movementspeed;
        }

        public void NewLaser(string Direction)
        {
            Laser FireLaser = new Laser();
            FireLaser.Direction = Direction;
            FireLaser.LaserLeft = picSpaceShip.Left + (picSpaceShip.Width / 2);
            FireLaser.LaserTop = picSpaceShip.Top + (picSpaceShip.Height / 2);

            FireLaser.LaserImage = Resource1.Laser;
            FireLaser.MakeLaser(panel1);
           
        }

        public void MakeLander()
        {
            Random RandomSpawn = new Random();
            PictureBox Lander = new PictureBox();
            LanderState landerState = new LanderState(Lander);
            EnemyList.Add(landerState);
            Lander.Tag = "Lander";
            Lander.Image = Resource1.Lander;
            Lander.BorderStyle = BorderStyle.FixedSingle;
            Lander.Size = new System.Drawing.Size(30, 27);
            Lander.SizeMode = PictureBoxSizeMode.StretchImage;

            Lander.Left = RandomSpawn.Next(0, panel1.Width - Lander.Width);
            Lander.Top = RandomSpawn.Next(50, panel1.Height - Lander.Height);
            this.panel1.Controls.Add(Lander);
            Lander.BringToFront();


            Speed = 6;


            landerState.Position = new Vector(Lander.Width / 2, Lander.Height / 2);
            Double Angle = FindRandomAngle();
            landerState.Velocity = new Vector((double)(Speed * Math.Cos(Angle)), (double)(Speed * Math.Sin(Angle)));
        }

        public Double FindRandomAngle()
        {
            Random RandomAngle = new Random();

            int r = 0;
            while (r % 2 == 0)
            {
                r = RandomAngle.Next(1, 8);
            }

            double Angle = r * 45 * Math.PI / 100;
            return Angle;
        }


        public void SpawnRate()
        {
           // foreach (LanderState i in EnemyList)
            {
                //this.panel1.Controls.Remove(i.LanderImage);
            }

           // EnemyList.Clear();

            for (int i = 0; i < 3; i++)
            {
                MakeLander();
            }
                
        }
    }
}