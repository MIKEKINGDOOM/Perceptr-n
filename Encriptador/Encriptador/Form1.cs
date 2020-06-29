using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encriptador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string encriptar;
        
        public string mensaje()
        {
            string frase = txtmensaje.Text;
            char[] arg = frase.ToCharArray();
            for( int i=0; i< frase.Length; i = i +1)
            {
                arg[i] = (char)(arg[i] + (char)11);
                encriptar += arg[i];
            }
            return encriptar;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mensaje();
            txtencriptado.Text = encriptar;
        }
    }
}
