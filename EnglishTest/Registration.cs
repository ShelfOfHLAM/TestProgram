using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace EnglishTest
{
    public partial class Registration : Form
    {
        private Form1 f1;
        public Registration(Form1 f)
        {
            InitializeComponent();
            this.f1 = f;
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //глобальная - для логина
        string login = "";

        private void button1_Click(object sender, EventArgs e)
        {
            //получаем login
            login = textBox1.Text;
            //знания по его мнению
            int HEng = (Int32)numericUpDown1.Value;
            int HProg = (Int32)numericUpDown2.Value;

            if ((HEng >= 0 && HEng <= 9) && (HProg >= 0 && HProg <= 9) && (login.Length != 0))
            {

                string file_url = "C:/Users/d/Documents/Visual Studio 2008/Projects/EnglishTest/users/" + login + ".txt";
                string write_to_file = login + "\r\nHEng=" + HEng.ToString() + "\r\nHProg=" + HProg.ToString();

                try
                {
                    //File.Create(file_user);
                    File.WriteAllText(file_url, write_to_file);
                    MessageBox.Show("Подзравляю, вы успешно зарегистрировались!");

                    //передаём логин
                    //Form1 main = this.Owner as Form1;
                    f1.login = login;

                    this.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Такой пользователь уже существует");
                }
            }
            else MessageBox.Show("Параметры знаний должны быть в пределах от 0 до 9");
        }
    }
}
