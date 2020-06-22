using System.Windows.Forms;

namespace ProyectoFinal_Arkanoid
{
    public class CustomPictureBox : PictureBox
    {
        public int Hits { get; set; }

        public CustomPictureBox() : base() { }
    }
}