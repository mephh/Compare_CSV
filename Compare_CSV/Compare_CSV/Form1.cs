using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;   //potrzebne do list<string>

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public string filename { get; set; }
        public string filename2 { get; set; }
        public string columns_ff { get; set; }
        public string columns_sf { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "CSV files|*.csv|All files|*.*";
            openFileDialog1.Title = "Wybierz pliki do porownania";


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                textBox1.Text = filename;
                using (var reader = new StreamReader(filename))
                {
                    columns_ff = reader.ReadLine();
                    textBox3.Text = columns_ff;
                }
                    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        filename2 = openFileDialog1.FileName;
                        textBox2.Text = filename2;
                    }

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Compare();
        }

        public void Compare()
        {
            using (var reader = new StreamReader(filename))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                    listB.Add(values[1]);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }


    }
}
