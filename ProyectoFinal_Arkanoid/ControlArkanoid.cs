using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoFinal_Arkanoid
{
    public partial class ControlArkanoid : UserControl
    {
        private CustomPictureBox[,] cpb;
        private PictureBox ball;

        private delegate void AccionesPelota();
        private readonly AccionesPelota MovimientoPelota;
        public Action TerminarJuego;
        public ControlArkanoid()
        {
            InitializeComponent();
            
            MovimientoPelota = RebotarPelota;
            MovimientoPelota += MoverPelota;
        }

        private void ControlArkanoid_Load(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Image.FromFile("../../Recursos/NaveArkanoid.png");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;

            pictureBox1.Top = Height - pictureBox1.Height - 80;
            pictureBox1.Left = (Width / 2) - (pictureBox1.Width / 2);
            
            ball = new PictureBox();
            ball.Width = ball.Height = 20;
            ball.BackgroundImage = Image.FromFile("../../Recursos/Bola.png");
            ball.BackgroundImageLayout = ImageLayout.Stretch;

            ball.Top = pictureBox1.Top - ball.Height;
            ball.Left = pictureBox1.Left + (pictureBox1.Width / 2) - (ball.Width / 2);

            Controls.Add(ball);

            LoadTiles();
            timer1.Start();
        }
        private void LoadTiles()
        {
            int xAxis = 10, yAxis = 5;

            int pbWidth = (Width - (xAxis - 5)) / xAxis;
            int pbHeight = (int)(Height * 0.3) / yAxis;

            cpb = new CustomPictureBox[yAxis, xAxis];

            for (int i = 0; i < yAxis; i++)
            {
                for (int j = 0; j < xAxis; j++)
                {
                    cpb[i, j] = new CustomPictureBox();

                    if (i == 0)
                        cpb[i, j].Golpes = 2;
                    else
                        cpb[i, j].Golpes = 1;

                    cpb[i, j].Height = pbHeight;
                    cpb[i, j].Width = pbWidth;

                    // Posicion de left, y posicion de top
                    cpb[i, j].Left = j * pbWidth;
                    cpb[i, j].Top = i * pbHeight;

                    cpb[i, j].BackgroundImage = Image.FromFile("../../Recursos/" + GRN() + ".png");
                    cpb[i, j].BackgroundImageLayout = ImageLayout.Stretch;

                    cpb[i, j].Tag = "tileTag";

                    Controls.Add(cpb[i, j]);
                }
            }
        }
        private int GRN()
        {
            return new Random().Next(1, 8);
        }
    }
}