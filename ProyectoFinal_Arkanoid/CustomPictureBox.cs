using System.Windows.Forms;

namespace ProyectoFinal_Arkanoid
{
    public class CustomPictureBox : PictureBox
    {
        public int Golpes { get; set; }

        public CustomPictureBox() : base() { }
    }
}