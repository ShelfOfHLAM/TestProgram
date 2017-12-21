using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EnglishTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menuStrip1.Enabled = false;
            button1.Enabled = false;
            label5.Hide();
            button3.Hide();
        }

        //глобальная переменная
        public string login = "";
        public string test = "";
        private bool status_enter = false;

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form registration = new Registration(this);
            registration.ShowDialog();

            if (login.Length != 0)
            {
                status_enter = true;
                linkLabel1.Hide();
                linkLabel2.Hide();

                label5.Text = login;
                label5.Show();

                menuStrip1.Enabled = true;
                button1.Enabled = true;
            }


        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form login_form = new Login(this);
            login_form.ShowDialog();

            if (login == "word_key_terror")
            {
                button3.Show();
                menuStrip1.Enabled = true;
            }
            else if (login.Length != 0)
            {
                status_enter = true;
                linkLabel1.Hide();
                linkLabel2.Hide();

                label5.Text = login;
                label5.Show();

                menuStrip1.Enabled = true;
            }
        }

        private void вToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void выбратьТестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form test = new FindTest(this);
            test.ShowDialog();

            label4.Text = this.test;
            if (this.test.Length != 0)
                button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pth = @"C:/Users/d/Documents/Visual Studio 2008/Projects/EnglishTest/results/" + this.test + "/";

            if (!Directory.Exists(pth))
                try
                {
                    Directory.CreateDirectory(pth);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message); this.Close();
                }


            Form testing = new Testing(this);
            testing.ShowDialog();

            Form tbr = new TableResult(this);
            tbr.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void таблицаРезультатовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form tresult = new TableResult(this);
            tresult.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void закрытьПрограммуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {            
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip1.Enabled = false;
            button1.Enabled = false;

            this.login = "";
            this.test = "";

            label5.Text = "";
            label5.Hide();
            button3.Hide();

            status_enter = false;

            linkLabel1.Show();
            linkLabel2.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form adm = new admin();
            adm.ShowDialog();
        }
    }
}
