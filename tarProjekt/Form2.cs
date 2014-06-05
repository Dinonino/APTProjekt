using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TarProjekt
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        WordPredicter wp = SharedClass.wp;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<string> words = textBox1.Text.Split(' ').ToList();
            string letters = words.Last();
            string predecessor = "";
            if (words.Count > 1)
                predecessor = words.ElementAt(words.Count - 2);
            textBox2.Text = string.Join("\r\n", wp.GetPredictions(letters, predecessor));

        
        }


    }
}
