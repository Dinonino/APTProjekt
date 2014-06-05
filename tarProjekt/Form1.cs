using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TarProjekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static int partSize = 200;
       

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                textBox1.Text += openFileDialog1.InitialDirectory + openFileDialog1.FileName + "\r\n";
            }


        }

        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> files = textBox1.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> input = new List<string>();
            int i=0;
            BigramMaker bm = new BigramMaker();
            List <string> part= new List<string>();
            foreach (string file in files)
                if (i > 0 && i % partSize == 0)
                {
                    i++;
                    bm.Run(part, i/partSize);
                    part = new List<string>();
                    part.Add(OpenFile(file));
                }
                else
                {
                    part.Add(OpenFile(file));
                    i++;
                }
            bm.Run(part, i / partSize);
            MessageBox.Show("Gotovo je");
                      
        }

        private string OpenFile(string filename)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    String line = sr.ReadToEnd();
                    return line;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The file could not be read: " + e.Message);
                return "";
            }
        }

        private void openAllFilesInFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                List<string> files = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "*.txt", SearchOption.AllDirectories).ToList();
                textBox1.Text = String.Join(Environment.NewLine, files);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form2().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
