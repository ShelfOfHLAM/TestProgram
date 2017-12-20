using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        private void вToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip1.Enabled = false;
            button1.Enabled = false;

            label5.Text = "";
            label5.Hide();

            status_enter = false;

            linkLabel1.Show();
            linkLabel2.Show();
        }

        private void выбратьТестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form test = new FindTest(this);
            test.ShowDialog();

            label4.Text = this.test;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form testing = new Testing(this);
            testing.ShowDialog();
        }
    }
}
