using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoFinal_Arkanoid
{
    public partial class ControlArkanoid : UserControl
    {
        private CustomPictureBox[,] cpb;
        private Panel scorePanel, blackPanel;
        private Label remainingLifes, score;
        private PictureBox ball;
        
        private double TimeElapsed = 0, TimeLimit = 4;
        private int remainingPb = 0;
        
        private PictureBox heart;
        private PictureBox[] hearts;

        private delegate void BallActions();
        private readonly BallActions BallMovements;
        public Action FinishGame, WinningGame;
        public ControlArkanoid()
        {
            InitializeComponent();
            
            BallMovements = BounceBall;
            BallMovements += MoveBall;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;       
                return handleParam;
            }
        }

        private void ControlArkanoid_Load(object sender, EventArgs e)
        {
            BackColor = Color.Transparent;
            ScoresPanel();
            
            pictureBox1.BackgroundImage = Image.FromFile("../../Recursos/NaveArkanoid.png");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;

            pictureBox1.Top = Height - pictureBox1.Height - 80;
            pictureBox1.Left = (Width / 2) - (pictureBox1.Width / 2);
            
            ball = new PictureBox();
            ball.Width = ball.Height = 25;
            
            ball.BackgroundImage = Image.FromFile("../../Recursos/bola.png");
            ball.BackgroundImageLayout = ImageLayout.Stretch;

            ball.Top = pictureBox1.Top - ball.Height;
            ball.Left = pictureBox1.Left + (pictureBox1.Width / 2) - (ball.Width / 2);

            Controls.Add(ball);
    
            LoadTiles();
            
        }
        private void LoadTiles()
        {
            int xAxis = 10, yAxis = 5;
            remainingPb = xAxis * yAxis;

            int pbWidth = (Width - (xAxis - 5)) / xAxis;
            int pbHeight = (int)(Height * 0.3) / yAxis;

            cpb = new CustomPictureBox[yAxis, xAxis];

            for (int i = 0; i < yAxis; i++)
            {
                
                for (int j = 0; j < xAxis; j++)
                {
                    
                    cpb[i, j] = new CustomPictureBox();

                    if (i == 4)
                        cpb[i, j].Hits = 2;
                    else
                        cpb[i, j].Hits = 1;

                    cpb[i, j].Height = pbHeight;
                    cpb[i, j].Width = pbWidth;

                    
                    cpb[i, j].Left = j * pbWidth;
                    cpb[i, j].Top = i * pbHeight + scorePanel.Height + 1;

                    int imageBack;
                    if (i == 0)
                        imageBack = 9;
                    else if (i == 1)
                        imageBack = 1;
                    else if (i == 2)
                        imageBack = 2;
                    else
                        imageBack = 3;

                    if (i == 4)
                    {
                        cpb[i, j].BackgroundImage = Image.FromFile("../../Recursos/tb4.png");
                        cpb[i, j].Tag = "blinded";
                    }
                    else
                    {
                        cpb[i, j].BackgroundImage = Image.FromFile("../../Recursos/" + imageBack + ".png");
                        cpb[i, j].Tag = "tileTag";
                    }

                    cpb[i, j].BackgroundImageLayout = ImageLayout.Stretch;

                    Controls.Add(cpb[i, j]);
                }
            }
        }
        private void ControlArkanoid_MouseMove(object sender, MouseEventArgs e)
        {
            if (!DatosJuego.GameStarted)
            {
                if (e.X < (Width - pictureBox1.Width))
                {
                    pictureBox1.Left = e.X;
                    ball.Left = pictureBox1.Left + (pictureBox1.Width / 2) - (ball.Width / 2);
                }
            }
            else
            {
                if (e.X < (Width - pictureBox1.Width))
                    pictureBox1.Left = e.X;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!DatosJuego.GameStarted)
                return;
            
            try
            {
                BallMovements?.Invoke();
            }
            catch(OutOfBoundsException ex)
            {
                try
                {
                    DatosJuego.lifes--;
                    DatosJuego.GameStarted = false;
                    timer1.Stop();

                    RepositionElements();
                    UpdateElements();

                    if (DatosJuego.lifes == 0)
                    {
                        throw new NoRemainingLifesException("F");
                    }
                }
                catch (NoRemainingLifesException ex2)
                {
                    timer1.Stop();
                    FinishGame?.Invoke();
                }
            }
        }

        private void ControlArkanoid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!DatosJuego.GameStarted)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Space:
                            DatosJuego.GameStarted = true;
                            timer1.Start();
                            break;
                        default:
                            throw new WrongKeyPressedException("Presione la tecla ESPACIO para iniciar el juego");
                    }
                }
            }
            catch(WrongKeyPressedException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void BounceBall()
        {
            if (ball.Top < scorePanel.Height)
            {
                DatosJuego.dirY = -DatosJuego.dirY;
                return;
            }

            if (ball.Bottom > Height)
            {
                throw new OutOfBoundsException("");

            }

            if (ball.Left < 0 || ball.Right > Width)
            {
                DatosJuego.dirX = -DatosJuego.dirX;
                return;
            }

            if (ball.Bounds.IntersectsWith(pictureBox1.Bounds))
            {
                DatosJuego.dirY = -DatosJuego.dirY;
                return;
            }

            for (int i = 4; i >= 0; i--)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (cpb[i, j] != null && ball.Bounds.IntersectsWith(cpb[i, j].Bounds))
                    {
                        DatosJuego.score += (int)(cpb[i, j].Hits * DatosJuego.ticksCount);
                        cpb[i, j].Hits--;

                        if (cpb[i, j].Hits == 0)
                        {
                            Controls.Remove(cpb[i, j]);
                            cpb[i, j] = null;
                            remainingPb--;
                        }
                        else if(cpb[i, j].Tag.Equals("blinded"))
                            cpb[i, j].BackgroundImage = Image.FromFile("../../Recursos/tb5.png");

                        DatosJuego.dirY = -DatosJuego.dirY;
                        
                        score.Text = DatosJuego.score.ToString();
                        if (remainingPb == 0)
                            WinningGame?.Invoke();
                        return;
                    }
                }
            }
        }
        private void MoveBall()
        {
            ball.Left += DatosJuego.dirX;
            ball.Top += DatosJuego.dirY;
        }
        private void ScoresPanel()
        {
            scorePanel = new Panel();
            
            scorePanel.Width = Width;
            scorePanel.Height = (int)(Height * 0.07);

            scorePanel.Top = scorePanel.Left = 0;

            scorePanel.BackColor = Color.Transparent;

            
            
            heart = new PictureBox();

            heart.Height = heart.Width = scorePanel.Height;

            heart.Top = 0;
            heart.Left = 20;

            heart.BackgroundImage = Image.FromFile("../../Recursos/Heart.png");
            heart.BackgroundImageLayout = ImageLayout.Stretch;
            

            hearts = new PictureBox[DatosJuego.lifes];

            for(int i = 0; i < DatosJuego.lifes; i++)
            {
               
                hearts[i] = new PictureBox();

                hearts[i].Height = hearts[i].Width = scorePanel.Height;

                hearts[i].BackgroundImage = Image.FromFile("../../Recursos/Heart.png");
                hearts[i].BackgroundImageLayout = ImageLayout.Stretch;

                hearts[i].Top = 0;

                if (i == 0)
                    
                    hearts[i].Left = scorePanel.Width / 2;
                else
                {
                    hearts[i].Left = hearts[i - 1].Right + 5;
                }
            }
            

            remainingLifes = new Label();
            score = new Label();

            
            remainingLifes.ForeColor = score.ForeColor = Color.White;

            remainingLifes.Text = "x " + DatosJuego.lifes.ToString();
            score.Text = DatosJuego.score.ToString();

            remainingLifes.Font = score.Font = new Font("Microsoft YaHei", 24F);
            remainingLifes.TextAlign = score.TextAlign = ContentAlignment.MiddleCenter;

            remainingLifes.Left = heart.Right + 5;
            score.Left = Width - 100;

            remainingLifes.Height = score.Height = scorePanel.Height;

            scorePanel.Controls.Add(heart);
            scorePanel.Controls.Add(remainingLifes);
            scorePanel.Controls.Add(score);

            foreach(var pb in hearts)
            {
                scorePanel.Controls.Add(pb);
            }

            Controls.Add(scorePanel);
        }

        private void RepositionElements()
        {
            pictureBox1.Left = (Width / 2) - (pictureBox1.Width / 2);
            ball.Top = pictureBox1.Top - ball.Height;
            ball.Left = pictureBox1.Left + (pictureBox1.Width / 2) - (ball.Width / 2);
        }

        private void UpdateElements()
        {
            remainingLifes.Text = "x " + DatosJuego.lifes.ToString();

            scorePanel.Controls.Remove(hearts[DatosJuego.lifes]);
            hearts[DatosJuego.lifes] = null;
        }
    }
    
}