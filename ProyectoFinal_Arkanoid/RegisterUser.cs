using System;
using System.Windows.Forms;

namespace ProyectoFinal_Arkanoid
{
    public partial class RegisterUser : Form
    {
        public delegate void GetNickname(string text);
        public GetNickname gn;

        public RegisterUser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (textBox1.Text)
                {
                    case string aux when aux.Length > 15:
                        throw new ExceededMaxCharactersException("No se puede introducir un nick de mas de 15 car");
                    case string aux when aux.Trim().Length == 0:
                        throw new EmptyNicknameException("No puede dejar campos vacios");
                    default:
                        gn?.Invoke(textBox1.Text);
                        break;
                }
            }
            catch(EmptyNicknameException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(ExceededMaxCharactersException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}