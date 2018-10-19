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
    public partial class Testing : Form
    {
        private Form1 f1;
        public Testing(Form1 f1)
        {
            InitializeComponent();
            this.f1 = f1;
        }

        //public void R(int count, int nstring, string text);

        List<string> questions = new List<string>();
        List<List<string>> variables = new List<List<string>>();
        List<int> right = new List<int>();

        private void Testing_Load(object sender, EventArgs e)
        {
            label2.Text = this.f1.test.ToString();
            label5.Text = this.f1.login.ToString();

            //получаем количество вопросов:
            FileStream file = new FileStream("./../../../tests/" + this.f1.test.ToString(), FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file, Encoding.UTF8);

            string ftext = reader.ReadToEnd();
            reader.Close();

            bool to_write = false;
            string number = "";

            for (int i = 0; i < ftext.Length; i++)
            {
                if (to_write && !Char.ToString(ftext[i]).Equals("%"))
                    number = number + ftext[i];
                else if (to_write)
                {
                    ftext.Substring(0, i);
                    break;
                }

                if (Char.ToString(ftext[i]).Equals("*"))
                    to_write = true;
            }

            int count_q = Int32.Parse(number);

            ///////////////////////////////
            bool q = false;
            bool v = false;
            bool r = false;

            string name_q = "";
            string variable = "";
            string right_q = "";

            int number_q = -1;

            for (int i=0; i<ftext.Length; i++)
            {
                if (Char.ToString(ftext[i]).Equals(":"))
                {
                    q = true;
                    continue;
                }
                else if (Char.ToString(ftext[i]).Equals("-"))
                {
                    v = true;
                    continue;
                }
                else if (Char.ToString(ftext[i]).Equals("R"))
                {
                    r = true;
                    continue;
                }

                if (q && !r && !v && !Char.ToString(ftext[i]).Equals("%"))
                    name_q = name_q + ftext[i];
                else if (!q && !r && v && !Char.ToString(ftext[i]).Equals("%"))
                    variable = variable + ftext[i];
                else if (r && !q && !v && !Char.ToString(ftext[i]).Equals("%"))
                    right_q = right_q + ftext[i];
                else if (q && !r && !v && Char.ToString(ftext[i]).Equals("%"))
                {
                    questions.Add(name_q);
                    variables.Add(new List<string>());
                    number_q++;
                    name_q = "";

                    q = false;
                }
                else if (!q && !r && v && Char.ToString(ftext[i]).Equals("%"))
                {
                    variables[number_q].Add(variable);
                    variable = "";

                    v = false;
                }
                else if (r && !q && !v && Char.ToString(ftext[i]).Equals("%"))
                {
                    right.Add(Int32.Parse(right_q));
                    right_q = "";

                    r = false;
                }
            }

            label6.Text = questions[0];

            listBox1.Items.AddRange(variables[0].ToArray());
            
//            for (int i = 0; i < variables[0].Count; i++)
          //  {
            //    listBox1.Items.Insert(i, variables[0][i]);
            //}
            
        }

        List<int> variant = new List<int>();
        int global_index = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedCountry = listBox1.SelectedIndex;
            variant.Add(selectedCountry+1);

            global_index++;

            if (global_index == questions.Count)
            {

                string message_result = "Вы ответили не правильно на следующие вопросы:\n";
                try
                {
                    double right_variants = 0;
                    for (int i = 0; i < variant.Count; i++)
                    {
                        if (variant[i] == right[i])
                            right_variants++;
                        else
                            message_result = message_result + (i + 1).ToString() + " ";
                    }

                    double rating_test = Math.Round(right_variants / Double.Parse(global_index.ToString()) * 10);

                    string pth = @"./../../../results/" + this.f1.test + "/";

                    if (!Directory.Exists(pth))
                        try
                        {
                            Directory.CreateDirectory(pth);
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message); this.Close();
                        }

                    File.WriteAllText(pth + this.f1.login + ".txt", rating_test.ToString());

                    if (right_variants == global_index)
                        MessageBox.Show("Молодец, все ответы правильные");
                    else
                        MessageBox.Show(message_result);

                    this.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else
            {
                label6.Text = questions[global_index];

                listBox1.Items.Clear();
                listBox1.Items.AddRange(variables[global_index].ToArray());
            }
        }
    }
}
