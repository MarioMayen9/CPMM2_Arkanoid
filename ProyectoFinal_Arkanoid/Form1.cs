using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal_Arkanoid
{
    
    public partial class Form1 : Form
    {
        private ControlArkanoid ca;

        public Form1()
        {
            InitializeComponent();
            Height = ClientSize.Height;
            Width = ClientSize.Width;
            WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ca = new ControlArkanoid();

            ca.Dock = DockStyle.Fill;

            ca.Width = Width;
            ca.Height = Height;

            ca.TerminarJuego = () =>
            {
                ca = null;
                ca = new ControlArkanoid();

                MessageBox.Show("Has perdido");

                ca.Hide();
                
            };
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Controls.Add(ca);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sin implementar aun");
        }
    }
}