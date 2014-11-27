using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LZWCoding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //讀取檔案位址(need OpenFileDialog)
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = openFileDialog1.FileName;
            try
            {
                StreamReader sr1 = new StreamReader(textBox1.Text, Encoding.Default);
                textBox2.Text = sr1.ReadToEnd();
            }
            catch( Exception ex ) {}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") || textBox4.Text.Equals(""))
            {
                MessageBox.Show("尚有位置沒有填寫");
            }
            else
            {
                LZWEncoder.LZW_Encode encoder = new LZWEncoder.LZW_Encode(textBox2.Text);
                Dictionary<string, int> dictionary = encoder.getDictionary;
                textBox3.Text = encoder.getOutput;
                using (StreamWriter sw1 = new StreamWriter(textBox4.Text + @"\Output.txt"))
                {
                    sw1.Write( textBox3.Text );
                }
                LZWDecoder.LZW_Decode decoder = new LZWDecoder.LZW_Decode(dictionary, textBox3.Text);
                textBox5.Text = decoder.getInput;
                using (StreamWriter sw1 = new StreamWriter(textBox4.Text + @"\Origin Input.txt"))
                {
                    sw1.Write(textBox5.Text);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
                //讀取資料夾位址(need FolderBrowserDialog)
                folderBrowserDialog1.SelectedPath = "";
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    textBox4.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
