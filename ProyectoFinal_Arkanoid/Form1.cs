using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Arkanoid.Controlador;

namespace ProyectoFinal_Arkanoid
{
    
    public partial class Form1 : Form
    {
        private ControlArkanoid ca;
        private Player currentPlayer;


        public Form1()
        {
            InitializeComponent();
            Height = ClientSize.Height;
            Width = ClientSize.Width;
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.BackColor = Color.Transparent;
            BackgroundImage = Image.FromFile("../../Recursos/Fondo.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;
           
        }

       private void btnIniciar_Click(object sender, EventArgs e)
       {
           ca = new ControlArkanoid();

           ca.Dock = DockStyle.Fill;

           ca.Width = Width;
           ca.Height = Height;

           ca.TerminarJuego = () =>
           {
               ca = null; ca = new ControlArkanoid();

               MessageBox.Show("Has perdido");

               ca.Hide();
               tableLayoutPanel1.Show();
                
           };
          
           ca.WinningGame = () =>
           {
               PlayerController.CreateNewScore(currentPlayer.idPlayer, DatosJuego.score);

               MessageBox.Show("Has ganado!");

               ca.Hide();
               tableLayoutPanel1.Show();
           };

           tableLayoutPanel1.Hide();

           RegisterUser fr = new RegisterUser();

           fr.gn = (string nick) => 
           {
               if (PlayerController.CreatePlayer(nick))
               {
                   MessageBox.Show($"Bienvenido nuevamenete {nick}");
               }
               else
               {
                   MessageBox.Show($"Gracias por registrarte {nick}");
               }

               currentPlayer = new Player(nick, 0);

               fr.Dispose();
           };

           fr.Show();

           Controls.Add(ca);
       }

       private void btnPuntajes_Click(object sender, EventArgs e)
       {
           Top ft = new Top
           {
               CloseAction = () =>
               {
                   Show();
               }
           };

           ft.Show();
           Hide();
           
       }

       private void btnSalir_Click(object sender, EventArgs e)
       {
           Application.Exit();
       }

       private void pictureBox1_Click(object sender, EventArgs e)
       {
           throw new System.NotImplementedException();
       }
    }
}