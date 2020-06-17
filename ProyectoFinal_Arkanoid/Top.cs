using System;
using System.Windows.Forms;

namespace ProyectoFinal_Arkanoid
{
    public partial class Top : Form
    {
        public delegate void OnCloseWindow();
        public OnCloseWindow CloseAction;
        
        public Top()
        {
            InitializeComponent();
        }

        private void Top_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseAction?.Invoke();
        }
        
    }
}