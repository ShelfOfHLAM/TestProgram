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
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        //глобальные данные по вопросам и вариантам ответов
        public List<string> question = new List<string>();
        public List<List<string>> variants = new List<List<string>>();
        public List<int> right = new List<int>();

        //индекс для отслеживания номера вопроса
        public int global_index = 0;
        int global_index_pr = 0;

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                int count_q = Int32.Parse(label6.Text);
                int now_count_q = Int32.Parse(label7.Text);

                if (count_q > now_count_q)
                {
                    global_index_pr = global_index;
                    Form a_q = new add_question(this);
                    a_q.ShowDialog();

                    if (global_index > global_index_pr)
                        label7.Text = global_index.ToString();
                }
                else MessageBox.Show("Количество вопросов не совпадает с вашим желанием =)");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int count_q = 0;
            int now_q = Int32.Parse(label7.Text);
            try
            {
                count_q = Int32.Parse(textBox3.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            if (count_q >= now_q)
                label6.Text = count_q.ToString();
            else MessageBox.Show("Вы указываете не то количество, которое хотите");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name_test = textBox2.Text;
            string path = textBox1.Text.Replace("\\", "/") + "/" + name_test;
            int count_q = Int32.Parse(textBox3.Text);

            string tfile = "*" + count_q.ToString() + "%\r\n";

            for (int i = 0; i < this.question.Count; i++)
            {
                tfile = tfile + (i+1).ToString() + ":" + this.question[i] + "%\r\n";
                for (int j = 0; j < this.variants[i].Count; j++)
                    tfile = tfile + "-" + variants[i][j] + "%\r\n";
                tfile = tfile + "R" + right[i] + "%\r\n";
            }

            try
            {
                File.WriteAllText(path, tfile);
                MessageBox.Show("Тест создан," + name_test + " поздравляем!!!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Тест с таким названием уже есть");
            }
        }
    }
}
