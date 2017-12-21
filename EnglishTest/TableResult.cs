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
    public partial class TableResult : Form
    {
        private Form1 f1;
        public TableResult(Form1 f)
        {
            InitializeComponent();

            this.f1 = f;
        }

        //структура для заполнения полей в таблице
        struct User
        {
            public int number;
            public string nikname;
            public int rating;
            public int ratingEng;
            public int ratingProg;

            public User(int number, string nikname, int rating, int rtE, int rtP)
            {
                this.number = number;
                this.nikname = nikname;
                this.ratingEng = rtE;
                this.ratingProg = rtP;
                this.rating = rating;
            }
        }

        private void TableResult_Load(object sender, EventArgs e)
        {
            string[] mass = {"hello", "world", "hey", "lololo", "pomidor"};

            string[] dirs = Directory.GetFiles(@"C:/Users/d/Documents/Visual Studio 2008/Projects/EnglishTest/users/", "*");
            string[] files_result = Directory.GetFiles(@"C:/Users/d/Documents/Visual Studio 2008/Projects/EnglishTest/results/" + this.f1.test + "/", "*");

            User[] mUser = new User[files_result.Length];

            //индекс в массиве структур
            int k = 0;
            //перебор файлов и извлечение из них данных
            foreach (string dir in dirs)
            {
                //получаем количество вопросов:
                FileStream file = new FileStream(dir, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(file, Encoding.UTF8);

                string ftext = "*" + reader.ReadToEnd().Replace("\n", "%").Replace("HEng=", "*").Replace("HProg=", "*");
                reader.Close();
                //MessageBox.Show(ftext);

                string nikname = "";
                string ratingEng = "";
                string ratingProg = "";
                bool nk = false;
                bool rE = false;
                bool rP = false;

                foreach (char tt in ftext)
                {
                    string t = tt.ToString();
                    if (t == "*" && !nk && !rP && !rE)
                    {
                        nk = true;
                        continue;
                    }
                    if (t == "*" && nk && !rP && !rE)
                    {
                        nk = false;
                        rE = true;
                        continue;
                    }
                    else if (t == "*" && !nk && rE && !rP)
                    {
                        rE = false;
                        rP = true;
                        continue;
                    }

                    if (t != "%" && !rP && !rE && nk)
                        nikname = nikname + t;
                    else if (t != "%" && !nk && !rP && rE)
                        ratingEng = ratingEng + t;
                    else if (t != "%" && !nk && !rE && rP)
                        ratingProg = ratingProg + t;
                }

                nikname = nikname.Replace("\n", "").Replace("\r", "").Replace(" ", "");
                string pth = "C:/Users/d/Documents/Visual Studio 2008/Projects/EnglishTest/results/" + this.f1.test + "/" + nikname + ".txt";
                
                //получаем рэйтинг по заданию:
                try
                {
                    FileStream fl = new FileStream(pth, FileMode.Open, FileAccess.Read);
                    StreamReader rd = new StreamReader(fl, Encoding.UTF8);

                    int rating_user = Int32.Parse(rd.ReadToEnd().Replace("\n", ""));

                    rd.Close();

                    mUser[k] = new User(k + 1, nikname.Replace("\n", ""), rating_user, Int32.Parse(ratingEng), Int32.Parse(ratingProg));
                    k++;
                }
                catch
                {
                    continue;
                }
            }

            for (int i = 0; i<mUser.Length-1; i++)
                for (int j=1; j<mUser.Length; j++)
                    if (mUser[i].rating < mUser[j].rating)
                    {
                        User x = mUser[i];
                        int save_number = mUser[j].number;

                        mUser[i] = mUser[j];
                        mUser[i].number = x.number;

                        mUser[j] = x;
                        mUser[j].number = save_number;
                    }

            for (int i = 0; i < mUser.Length; i++)
            {
                ListViewItem lvi = new ListViewItem(mUser[i].number.ToString());
                lvi.SubItems.Add(mUser[i].nikname);
                lvi.SubItems.Add(mUser[i].rating.ToString());
                lvi.SubItems.Add(mUser[i].ratingEng.ToString());
                lvi.SubItems.Add(mUser[i].ratingProg.ToString());

                listView1.Items.Add(lvi);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
