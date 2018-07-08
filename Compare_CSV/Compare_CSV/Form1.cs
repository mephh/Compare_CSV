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
        public string[] lineBuffer { get; set; }
        List<Results> Lista = new List<Results>();


        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "CSV files|*.csv|All files|*.*";
            openFileDialog1.Title = "Wybierz pliki do porownania";
            int countd = 0;
            int highestdel = 0;
            //OPEN BOTH FILES
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename1 = openFileDialog1.FileName;
                textBox1.Text = filename1;
                using (var reader = new StreamReader(filename1))
                {
                    // OPEN FILE AND CHECK WHICH DELIMITER IS USED
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
                    //ADD COLUMN NAMES TO CHECKLIST BOX
                    columns = columns_ff.Split(delimiter1);
                    foreach (var item1 in columns)
                    {
                        checkedListBox1.Items.Add(item1);
                        dataGridView1.Columns.Add(item1, item1);    //przypisuje nazwy kolumn
                    }
                    //ADD VALUES TO PROPER LISTS
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lineBuffer = line.Split(delimiter1);
                        addRow(lineBuffer[0],lineBuffer[1], lineBuffer[2],  lineBuffer[3],  lineBuffer[4],lineBuffer[5], lineBuffer[6] );
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
                            countd = columns_ff.Split(item).Length -1; //splituje na itemy oddzielone charem -1 bo liczy od 0
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
                        }
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                        }
                        textBox3.Text = "Pierwszy delimiter to: " + delimiter1 + "Drugi delimiter to: " + delimiter2;

                    }
                }

            }
        }

        private void addRow(string v1, string v2, string v3, string v4, string v5, string v6, string v7)
        {
            Lista.Add(new Results{Column1 = v1, Column2 = v2, Column3 = v3, Column4 = v4, Column5 = v5, Column6 = v6, Column7 = v7 });
        }

    private void button5_Click(object sender, EventArgs e) //porownaj
        {
            dataGridView1.Columns.Clear();  //wyczysc grid aby sie nie dopisywal
            dataGridView2.Columns.Clear();
            dataGridView1.DataSource = Lista;



        }
        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }

   
}
