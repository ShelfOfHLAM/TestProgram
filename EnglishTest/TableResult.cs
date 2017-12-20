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
        public TableResult()
        {
            InitializeComponent();
        }

        //структура для заполнения полей в таблице
        struct User
        {
            public int number;
            public string nikname;
            public int rating;
            public int ratingEng;
            public int ratingProg;

            public User(int number, string nikname, int rtE, int rtP)
            {
                this.number = number;
                this.nikname = nikname;
                this.ratingEng = rtE;
                this.ratingProg = rtP;
                this.rating = 100;
            }
        }

        private void TableResult_Load(object sender, EventArgs e)
        {
            string[] mass = {"hello", "world", "hey", "lololo", "pomidor"};

            string[] dirs = Directory.GetFiles(@"C:/Users/d/Documents/Visual Studio 2008/Projects/EnglishTest/users/", "*");

            User[] mUser = new User[dirs.Length];

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

                mUser[k] = new User(k+1, nikname.Replace("\n", ""), Int32.Parse(ratingEng), Int32.Parse(ratingProg));
                k++;
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
    }
}
