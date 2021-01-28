using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace DefendersGame
{
    
    public partial class StartScreen : Form
    {
        private Random rnd = new Random();
        public StartScreen()
        {
            InitializeComponent();
        }

        private void StartScreen_Load(object sender, EventArgs e)
        {

        }

        private void tmrBackground_Tick(object sender, EventArgs e)
        {
            foreach (Control X in this.Controls)
            {
                if (X is PictureBox && (string)X.Tag == "Star")
                {
                    Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                    X.BackColor = randomColor;
                }

            } 
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            Game StartGame = new Game();
            this.Visible = false;
            StartGame.ShowDialog();
            this.Visible = true;
           
        }

        private void picDefendergif_Click(object sender, EventArgs e)
        {

        }
    }
}
