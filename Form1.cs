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
using System.Media;
using System.Numerics;


namespace DefendersGame
{
    public partial class Game : Form
    {
        List<LanderState> EnemyList = new List<LanderState>();
        PictureBox[] Humans = new PictureBox[10];
        SoundPlayer GameOver = new SoundPlayer(Resource1.GameOver);
        SoundPlayer StartMusic = new SoundPlayer(Resource1.Start);
        SoundPlayer LaserSound = new SoundPlayer(Resource1.LaserSound);
        int Hitpoints;
        bool spawn;
        private Random rnd = new Random();
        Vector2 SpaceShipPosition;
        Vector2 DynamosPosition;
        Vector2 Velocity;
        Vector2 Direction;



        float Speed;
        int Level = 1;
        int Score = 0;
        int Ammo =5;
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
        
        
        public Game()
        {
            InitializeComponent();

            SpawnRate();
            pgrHealth.Maximum = 100;
            pgrHealth.Value = 100;
            spawn = false;
            StartMusic.Play();
            this.panel1.Controls.Add(picSpaceShip);
            this.panel1.Controls.Add(picDynamos6);
            this.panel1.Controls.Add(picDynamos3);
            this.panel1.Controls.Add(picDynamos4);
            this.panel1.Controls.Add(picDynamos5);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        


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
            
            if (e.KeyCode == Keys.Space & Ammo >0)
            {
                NewLaser(Face);
                Ammo -= 1;           
                LaserSound.Play();
                AddAmmo();
              
            }
            if (e.KeyCode == Keys.Tab)
            {
               
              
                DynamosNewLaser();


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
                if (X is PictureBox && ((string)X.Tag == "Lander"||(string)X.Tag == "Swarmer"))
                {
                    if (pgrHealth.Value <= 0)
                    {
                        GameOver.Play();
                        tmrGame.Enabled = false;
                    }
                   else if (((PictureBox)X).Bounds.IntersectsWith(picSpaceShip.Bounds))
                    {
                        // If player gets hit by lander or swarmer they lose health. Enemy gets destroyed.
                        pgrHealth.Value -= 10;
                        Hitpoints += 250;
                        MakeLander();
                        this.panel1.Controls.Remove(X);
                      

                    }
                    
                   
                }


                foreach (Control k in this.panel1.Controls)
                {
                    if ((k is PictureBox && (string)k.Tag == "Laser") && (X is PictureBox && (string)X.Tag == "Lander"|| (string)X.Tag == "Swarmer"))
                    {
                        if (k.Bounds.IntersectsWith(X.Bounds))
                        {
                            // if Player hits Swarmer or Lander they get points 
                            Score+=250;
                            SpawnRate();
                            lblPoints.Text = "Score: "+ Score.ToString();
                            this.panel1.Controls.Remove(X);
                            
                            X.Dispose();
                            this.panel1.Controls.Remove(k);
                            k.Dispose();
                            if (X.Tag == "Lander")
                                MakeLander();
                            else if (X.Tag == "Swarmer")
                                MakeSwamer();
                    
                           
                        }
                    }
                   
                    

                }
            }


        

              
        


                foreach (Control x in this.panel1.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Swarmer")
                {
                    if(picSpaceShip.Top>x.Top)
                    {
                        x.Top += 2;
                    }
                    if(picSpaceShip.Left>x.Left)
                    {
                        x.Left += 2;
                    }
                    if (picSpaceShip.Top<x.Top)
                    {
                        x.Top -= 2;
                    }
                    if (picSpaceShip.Left<x.Left)
                    {
                        x.Left -= 2;
                    }
                }

            }

                    //Move Lander
                    foreach (LanderState landerState in EnemyList)
            {
                var x = landerState.LanderImage;
                if (x is PictureBox && (string)x.Tag == "Lander")
                {
                    if (x.Top <= 0 || x.Bottom >= panel1.ClientSize.Height)
                    {
                        landerState.SetVelocityY(-1 * landerState.Velocity.Y);
                    }
                    else if (x.Left <= 0 || x.Left >= panel1.ClientSize.Width - x.Width)
                    {
                        landerState.SetVelocityX(-landerState.Velocity.X);
                    }


                      


                    landerState.Position = new System.Windows.Vector(x.Left, x.Top) + landerState.Velocity;

                    x.Top = (int)landerState.Position.Y;
                    x.Left = (int)landerState.Position.X;
                }
            }
        }

        private void Form1_KeyUp_1(object sender, KeyEventArgs e)
        {
            // Stop ship from moving when no keys are pressed
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

            // Create laser and set it off based on direction of player
            Laser FireLaser = new Laser();
            FireLaser.Direction = Direction;
            FireLaser.LaserLeft = picSpaceShip.Left + (picSpaceShip.Width / 2);
            FireLaser.LaserTop = picSpaceShip.Top + (picSpaceShip.Height / 2);

            FireLaser.LaserImage = Resource1.Laser;
            FireLaser.MakeLaser(this.panel1);
       
          
          

        }
        public void DynamosNewLaser()
        {

           double Angle= DynamosAngle();
            // Create laser and set it off based on direction of player
            DynamosLaser FireLaser = new DynamosLaser();
          
            FireLaser.DynamosLaserLeft = picDynamos3.Left + (picDynamos3.Width / 2);
            FireLaser.DynamosLaserTop = picDynamos3.Top + (picDynamos3.Height / 2);
            
            FireLaser.DynamosLaserImage = Resource1.DynamosLaser;
            FireLaser.MakedDynamosLaser(panel1);
             SpaceShipPosition = new Vector2(picSpaceShip.Left + picSpaceShip.Width/2, picSpaceShip.Top+picSpaceShip.Height/2);
             DynamosPosition = new Vector2(picDynamos3.Left + (picDynamos3.Width / 2), picDynamos3.Top + (picDynamos3.Height / 2));
           Direction = DynamosPosition-SpaceShipPosition ;
            FireLaser.Direction = Direction;
         
        }

        public Double DynamosAngle()
        {


            double DynamosAngle = Math.Atan(SpaceShipPosition.X - DynamosPosition.X) / (SpaceShipPosition.Y - SpaceShipPosition.X);
            return DynamosAngle;
            
        }


        public void MakeLander()
        {
            // Spawn Lander at random location on screen
            Random RandomSpawn = new Random();
            PictureBox Lander = new PictureBox();
            LanderState landerState = new LanderState(Lander);
            EnemyList.Add(landerState);
            Lander.Tag = "Lander";
            Lander.Image = Resource1.Lander;
           
            Lander.Size = new System.Drawing.Size(30, 27);
            Lander.SizeMode = PictureBoxSizeMode.StretchImage;

            Lander.Left = RandomSpawn.Next(0, panel1.Width - Lander.Width);
            Lander.Top = RandomSpawn.Next(0, panel1.Height - Lander.Height);
            this.panel1.Controls.Add(Lander);
            Lander.BringToFront();


            Speed = 6;

            landerState.Position = new System.Windows.Vector(Lander.Width / 2, Lander.Height / 2);
            Double Angle = FindRandomAngle();
            landerState.Velocity = new System.Windows.Vector((double)(Speed * Math.Cos(Angle)), (double)(Speed * Math.Sin(Angle)));
        }

        
        public void MakeSwamer()
        {
            // Spawn swarmer at random location on the panel
             Random RandomSpawn = new Random();
            PictureBox Swamer = new PictureBox();
         
           
            Swamer.Tag = "Swarmer";
           Swamer.Image = Resource1.Swamer;
            
            Swamer.Size = new System.Drawing.Size(30, 27);
            Swamer.SizeMode = PictureBoxSizeMode.StretchImage;
           Swamer.Left = RandomSpawn.Next(0, panel1.Width - Swamer.Width);
            Swamer.Top = RandomSpawn.Next(0, panel1.Height - Swamer.Height);

            this.panel1.Controls.Add(Swamer);
        }


    

        //Set random angle for the Lander to go off at
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


        public async Task SpawnRate()
        {
            foreach (LanderState i in EnemyList)
            {
                // this.panel1.Controls.Remove(i.LanderImage);
            }
            if (Score ==0)
            {
                if (spawn == false)
                spawn = true;
                Level = 1;
                spawn = false;
            }    

            else if (Score == 1250)
            {
                spawn = true;
                Level = 2;
            }
            else if (Score == 4250)
            {
                spawn = true;
                Level = 3;
            }
            else if (Score == 20000)
            {
                spawn = true;
                Level = 4;
            }
            else
            {
                spawn = false;
            }
            if (spawn == true)

            {
                for (int i = 0; i < Level * 3; i++)
                {
                    // 1 seccond delay for spawning of Landers
                    await Task.Delay(1000);
                    MakeLander();
                   
                }


                for (int i = 0; i < Level * 2; i++)
                {
                    await Task.Delay(1000);
                    MakeSwamer();
                    
                }
                spawn = false;
            }
           
        }

        public async void AddAmmo()
        {
            // 1 second delay for reloading Ammo
            if (Ammo == 0)
            {
                await Task.Delay(500);
                Ammo = 5;

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tmrBackground_Tick(object sender, EventArgs e)
        {

            foreach (Control X in this.panel1.Controls)
            {
                if (X is PictureBox && (string)X.Tag == "Star")
                {
                    Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                    X.BackColor = randomColor;
                }

            }
        }

        private void lblHealth_Click(object sender, EventArgs e)
        {

        }

       
    }
}