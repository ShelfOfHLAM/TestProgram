using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EnglishTest
{
    public partial class Login : Form
    {
        private Form1 f1;
        public Login(Form1 f)
        {
            InitializeComponent();
            this.f1 = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //достаём логин
            string login = textBox1.Text;

            string file_url = "./../../../users/" + login + ".txt";

            try
            {
                FileStream file = new FileStream(file_url, FileMode.Open, FileAccess.Read);

                f1.login = login;
   
                this.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Пользователя с таким ником не существует");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
