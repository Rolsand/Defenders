
namespace DefendersGame
{
    partial class Form1
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
            if (disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.pgrHealth = new System.Windows.Forms.ProgressBar();
            this.picSpaceShip = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picSpaceShip)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrGame
            // 
            this.tmrGame.Enabled = true;
            this.tmrGame.Interval = 20;
            this.tmrGame.Tick += new System.EventHandler(this.tmrGame_Tick);
            // 
            // pgrHealth
            // 
            this.pgrHealth.BackColor = System.Drawing.Color.LimeGreen;
            this.pgrHealth.ForeColor = System.Drawing.SystemColors.GrayText;
            this.pgrHealth.Location = new System.Drawing.Point(12, 12);
            this.pgrHealth.Name = "pgrHealth";
            this.pgrHealth.Size = new System.Drawing.Size(100, 23);
            this.pgrHealth.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgrHealth.TabIndex = 1;
            // 
            // picSpaceShip
            // 
            this.picSpaceShip.BackColor = System.Drawing.Color.Transparent;
            this.picSpaceShip.Image = global::DefendersGame.Resource1.DefenderShipRight;
            this.picSpaceShip.Location = new System.Drawing.Point(318, 220);
            this.picSpaceShip.Name = "picSpaceShip";
            this.picSpaceShip.Size = new System.Drawing.Size(38, 38);
            this.picSpaceShip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSpaceShip.TabIndex = 0;
            this.picSpaceShip.TabStop = false;
            this.picSpaceShip.Tag = "Player";
            this.picSpaceShip.UseWaitCursor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(720, 515);
            this.panel1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 586);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pgrHealth);
            this.Controls.Add(this.picSpaceShip);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp_1);
            ((System.ComponentModel.ISupportInitialize)(this.picSpaceShip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picSpaceShip;
        private System.Windows.Forms.Timer tmrGame;
        private System.Windows.Forms.ProgressBar pgrHealth;
        private System.Windows.Forms.Panel panel1;
    }
}

