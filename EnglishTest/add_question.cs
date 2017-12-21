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
    public partial class add_question : Form
    {
        private admin f_a;
        public add_question(admin f)
        {
            InitializeComponent();
            this.f_a = f;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string q = textBox1.Text;
            string variants = (richTextBox1.Text + "%").Replace("\n", "%").Replace("\r", "");

            int right = Int32.Parse(textBox2.Text);

            if (q.Length != 0 && variants.Length != 0)
            {
                this.f_a.question.Add(q);
                this.f_a.variants.Add(new List<string>());
                string variant = "";
                foreach (char tt in variants)
                {
                    string t = tt.ToString();
                    if (t != "%")
                        variant = variant + t;
                    else
                    {
                        this.f_a.variants[this.f_a.global_index].Add(variant);
                        variant = "";
                    }
                }
                this.f_a.global_index++;
                this.f_a.right.Add(right);

                this.Close();
            }
            else
            {
                MessageBox.Show("Вы чот не ввели сударь)");
            }
        }
    }
}
