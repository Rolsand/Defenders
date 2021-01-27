using System.Windows;
using System.Windows.Forms;

namespace DefendersGame
{
    public class LanderState
    {
        public PictureBox LanderImage { get; set; }
        public PictureBox SwarmerImage { get; set; }
        public Vector Position { get; set; }
        private Vector mvelocity;
        public Vector Velocity  
        
        {
        get {return mvelocity;}
       set{mvelocity= value;}
}

        public LanderState(PictureBox landerImage)
        {
            LanderImage = landerImage;
        }

        public void SetVelocityX(double value)
        {
            mvelocity.X = value;
        }
        public void SetVelocityY(double value)
        {
            mvelocity.Y = value;
        }
    }
}