 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ShopCommix
{
    public partial class Form1 : Form
    {
        string[,] commix = new string[10, 2];
        public Form1()
        {
            InitializeComponent();
            ReadFormFile();
            adind = listBox1.SelectedIndex;
        }
        int adind;
        public  void ReadFormFile()
            
        {
            using (StreamReader redear = new StreamReader(@"Products.txt"))
            {
                string products;
                for (int i = 0; !redear.EndOfStream;i++ )
                {
                    products = redear.ReadLine();
                    commix[i, 0] = products.Split(' ')[0];
                    commix[i, 1] = products.Split(' ')[1];
                }
            }
            refresh();
        }
        private void refresh()
        {
            listBox1.Items.Clear();
            for (int i = 0;i < commix.GetLength(0);i++)
            {
                listBox1.Items.Add($"название {commix[i,0]} ; Цена:  {commix[i,1]}");
            }
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
            string tempOne;
            string tempTwo;
            for (int i = 0; i < commix.GetLength(0); i++)
            {
                for (int j = i + 1; j < commix.GetLength(0) ; j++)
                {
                    if (int.Parse(commix[i, 1]) < int.Parse(commix[j, 1]))
                    {
                        tempOne = commix[i, 0];
                        tempTwo = commix[i, 1];
                        commix[i, 0] = commix[j, 0];
                        commix[i, 1] = commix[j, 1];
                        commix[j, 0] = tempOne;
                        commix[j, 1] = tempTwo;
                    }
                }
            }
            refresh();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "куда вы хотиле сохранить фаил ";
            saveFileDialog1.Filter = "все фаилы|*.*|текстовые фаилы |*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string patc = saveFileDialog1.FileName;
                FileStream dr = new FileStream(patc, FileMode.Create, FileAccess.Write);
                StreamWriter rf = new StreamWriter(dr);
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    rf.WriteLine(listBox1.Items[i]);
                }
                rf.Close();
                dr.Close();
            }
            else
            {
                MessageBox.Show("с фаиллом что-то случилась ");
            }
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (adind != -1)
            {
                listBox1.Items.RemoveAt(adind);
             
            }
            else
            {
                MessageBox.Show("вы не выбрали элемент ");
            }
            
        }
        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            adind = listBox1.SelectedIndex;
            textBox1.Text = commix[adind, 0];
            textBox2.Text = commix[adind, 1];

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            string tempOne;
            string tempTwo;
            for (int i = 0; i < commix.GetLength(0); i++)
            {
                for (int j = i + 1; j < commix.GetLength(0); j++)
                {
                    if (int.Parse(commix[i, 1]) > int.Parse(commix[j, 1]))
                    {
                        tempOne = commix[i, 0];
                        tempTwo = commix[i, 1];
                        commix[i, 0] = commix[j, 0];
                        commix[i, 1] = commix[j, 1];
                        commix[j, 0] = tempOne;
                        commix[j, 1] = tempTwo;
                    }
                }
            }
            refresh();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text)|| !string.IsNullOrEmpty(textBox2.Text))
            {
                commix[listBox1.SelectedIndex, 0] = textBox1.Text;
                commix[listBox1.SelectedIndex, 1] = textBox2.Text;
                refresh();
            }
            else if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("ВВИДИТЕ ИМЯ ");
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("ВВИДИТЕ ЦЕНУ  ");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "куда вы хотиле сохранить фаил ";
            openFileDialog1.Filter = "все фаилы|*.*|текстовые фаилы |*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream file = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                StreamReader reder = new StreamReader(file);
                while (!reder.EndOfStream)
                {
                    listBox1.Items.Add (reder.ReadLine());
                }
                refresh();
                reder.Close();
                file.Close();
            }
        }
    }
    
}
