using System;
using System.Drawing;
using System.Windows.Forms;
using Arkanoid;


namespace ProyectoFinal_Arkanoid
{
    public partial class Top : Form
    {
        public delegate void OnCloseWindow();
        public OnCloseWindow CloseAction;
        private Label[,] players;
        
        public Top()
        {
            InitializeComponent();
        }

        private void Top_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseAction?.Invoke();
        }
        private void Top_Load(object sender, System.EventArgs e)
        {
            LoadPlayers();
        }
        private void LoadPlayers()
        {
            var playersList = PlayerController.ObtainTopPlayers();
            players = new Label[10,2];

            int sampleTop = label1.Bottom + 50, sampleLeft = 35;

            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    players[i, j] = new Label();

                    if (j == 0)
                    {
                        players[i, j].Text = playersList[i].Nickname;
                        players[i, j].Left = sampleLeft;
                    }
                    else
                    {
                        players[i, j].Text = playersList[i].Score.ToString();
                        players[i, j].Left = Width / 2 + sampleLeft;
                    }

                    players[i, j].Top = sampleTop + 50 * i;

                    players[i, j].Height += 4;
                    players[i, j].Width += 0;
                    
                    players[i,j].BackColor = Color.Transparent;
                    players[i,j].ForeColor =Color.White;
                    players[i,j].Font = new Font("Microsoft YaHei", 14F);
                    players[i,j].TextAlign = ContentAlignment.MiddleCenter;

                    Controls.Add(players[i,j]);
                }
            }
        }
    }
}