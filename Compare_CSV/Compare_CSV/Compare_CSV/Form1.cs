using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;   //potrzebne do list<string>
using System.Linq;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public string filename1 { get; set; }
        public string filename2 { get; set; }
        public string columns_ff { get; set; }
        public string columns_sf { get; set; }
        char[] delimiters = {';', ',', '.', ' '};
        char delimiter1 = ' ';
        char delimiter2 = ' ';
        string[] columns;
        List<string> listA = new List<string>();
        List<string> listB = new List<string>();


        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "CSV files|*.csv|All files|*.*";
            openFileDialog1.Title = "Wybierz pliki do porownania";
            int countd = 0;
            int highestdel = 0;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename1 = openFileDialog1.FileName;
                textBox1.Text = filename1;
                using (var reader = new StreamReader(filename1))
                {
                    columns_ff = reader.ReadLine();
                    columns_ff.Trim();
                    foreach (var item in delimiters)
                    {
                        countd = columns_ff.Split(item).Length - 1; //splituje na itemy oddzielone charem -1 bo liczy od 0
                        
                        if (countd > highestdel)
                        {
                            highestdel = countd;
                            delimiter1 = item;
                        } 
                    }
                    columns = columns_ff.Split(delimiter1);
                    foreach (var item1 in columns)
                    {
                        checkedListBox1.Items.Add(item1);
                        //dataGridView1.Columns.Add(item1, item1);    //przypisuje nazwy kolumn
                    }
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        listA.Add(line);
                        //dataGridView1.Rows.Add(line.Split(delimiter1));  //dodaje wiersze 
                    }
                    
                    textBox3.Text = "Pierwszy delimiter to: "+delimiter1;
                }
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        filename2 = openFileDialog1.FileName;
                        textBox2.Text = filename2;
                    using (var reader = new StreamReader(filename2))
                    {
                        columns_ff = reader.ReadLine();
                        columns_ff.Trim();
                        foreach (var item in delimiters)
                        {
                            highestdel = 0;
                            countd = columns_ff.Split(item).Length - 1; //splituje na itemy oddzielone charem -1 bo liczy od 0
                            if (countd > highestdel)
                            {
                                highestdel = countd;
                                delimiter2 = item;
                            }
                        }
                        columns = columns_ff.Split(delimiter2);
                        foreach (var item1 in columns)
                        {
                            checkedListBox2.Items.Add(item1);
                            //dataGridView2.Columns.Add(item1, item1);  //dodaje kolumny
                        }
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            listB.Add(line);
                            //dataGridView2.Rows.Add(line.Split(delimiter2));   //dodaje wiersze
                        }
                        textBox3.Text = "Pierwszy delimiter to: " + delimiter1 + "Drugi delimiter to: " + delimiter2;

                    }
                }

            }
        }
        private void button5_Click(object sender, EventArgs e) //porownaj
        {
            dataGridView1.Columns.Clear();  //wyczysc grid aby sie nie dopisywal
            foreach (int index in checkedListBox1.CheckedIndices)
            {
                dataGridView1.Columns.Add(columns[index], columns[index]);
            }
            foreach (int index in checkedListBox2.CheckedIndices)
            {
                dataGridView2.Columns.Add(columns[index], columns[index]);
            }
            /// <summary>
            /// Zwraca unikalne wartosci list1.
            /// </summary>
            /// <param name="list1"></param>
            /// <param name="list2"></param>
          

        }
        static List<string> unique(List<string> list1, List<string> list2)
        {
            var list3 = list2.Union(list1).ToList(); //mozna dac do juz istniejacej listy ale tak latwiej jest to zrozumiec
            list3.RemoveRange(0, list2.Count);
            return list3;
        }


        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
