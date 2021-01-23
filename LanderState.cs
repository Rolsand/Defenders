using System.Windows;
using System.Windows.Forms;

namespace DefendersGame
{
    public class LanderState
    {
        public PictureBox LanderImage { get; set; }
        public Vector Position { get; set; }
        private Vector mvelocity;
        public Vector Velocity { get => mvelocity; set => mvelocity = value; } // same as {get {return mvelocity;} set{return mvelocity;}

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