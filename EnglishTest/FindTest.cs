﻿using System;
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
    public partial class FindTest : Form
    {
        private Form1 f1;
        public FindTest(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public string Reverse_Str(string st)
        {
            char[] arr = st.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        private void FindTest_Load(object sender, EventArgs e)
        {
            try
            {
                string[] dirs = Directory.GetFiles(@"C:/Users/d/Documents/Visual Studio 2008/Projects/EnglishTest/tests/", "*");
                foreach (string dir in dirs)
                {
                    string st = Reverse_Str(dir);
                    string name_file = "";
                    for (int i = 0; i < st.Length; i++)
                    {
                        if (!Char.ToString(st[i]).Equals("/"))
                            name_file = name_file + st[i];
                        else
                            break;
                    }

                    name_file = Reverse_Str(name_file);

                    comboBox1.Items.Add(name_file);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Object selectedItem = comboBox1.SelectedItem;
            string name_test = selectedItem.ToString();

            this.f1.test = name_test;

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}