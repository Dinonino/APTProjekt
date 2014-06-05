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

namespace TarProjekt
{
    public partial class PredictiveTyperForm : Form
    {
        private LanguageModel languageModel;
        public LanguageModel Model
        {
            get
            {
                return languageModel;
            }
            set
            {
                languageModel = value;
            }
        }
        public PredictiveTyperForm()
        {
            languageModel = new LanguageModel(3);
            InitializeComponent();
        }

        private List<string> getBestPredictions()
        {
            List<string> words = fileText.Text.Split(' ').ToList<String>();
            List<Word> predictions = null;
            if (words.Count > languageModel.getLMOrder() - 1)
            {
                List<string> wordsForPrediction = words.GetRange(words.Count - (languageModel.getLMOrder() - 1) - 1, languageModel.getLMOrder() - 1);
                predictions = languageModel.doKneserNeySmoot(wordsForPrediction);
            }
            else
            {
                predictions = languageModel.mostOccuringWords(10);
            }
            List<string> zaVratit = new List<string>();
            for (int i = 0; i < predictions.Count(); i++)
            {
                zaVratit.Add(predictions.ElementAt(i).Content + " " + predictions.ElementAt(i).EvaluatedProbability);
            }
            return zaVratit;
        }
        private string getLastUncompletedWord()
        {
            string lastUncompletedWord = "";
            int index = fileText.Text.Count() - 1;
            while (index >= 0)
            {
                char lastCharacter = fileText.Text.ElementAt(index);
                if (lastCharacter == ' ' || lastCharacter == '.' || lastCharacter == ',' //ovo se vjerojanto da i drugacije
                   || lastCharacter == '!' || lastCharacter == '\n' || lastCharacter == '\t') //zapisat al mi se neda
                {
                    break;
                }
                lastUncompletedWord = lastCharacter + lastUncompletedWord;
                index--;
            }
            return lastUncompletedWord;
        }
        private void fileText_TextChanged(object sender, EventArgs e)
        {
            string lastUncompletedWord = getLastUncompletedWord();
            Point positionOfList = fileText.GetPositionFromCharIndex(fileText.SelectionStart);
            positionOfList.Y += (int)fileText.Font.GetHeight() * 4;
            autoCompleteList.Location = positionOfList;
            autoCompleteList.Show();
            //ActiveControl = autoCompleteList;
            //autoCompleteList.SetSelected(1, true);
            List<string> lista = getBestPredictions();
            List<string> forOutput = new List<string>();
            for(int i = 0; i < lista.Count(); i++)
            {
                string possibleChoice = lista.ElementAt(i);
                bool isChoice = true;
                for (int j = 0; j < possibleChoice.Count(); j++)
                {
                    if (j == lastUncompletedWord.Count() || j == possibleChoice.Count())
                        break;
                    if (possibleChoice.ElementAt(j) != lastUncompletedWord.ElementAt(j))
                        isChoice = false;
                }
                if(isChoice)
                    forOutput.Add(possibleChoice);
            }
            autoCompleteList.DataSource = forOutput;
            int selection = fileText.SelectionStart;
            int lineNumber = fileText.GetLineFromCharIndex(selection);
            int firstChar = fileText.GetFirstCharIndexFromLine(lineNumber);
            statusLabelLine.Text = "Line: " + lineNumber.ToString() + ";";
            statusLabelColumn.Text = "Column: " + (selection - firstChar).ToString() + ";";
            statusLastWord.Text = lastUncompletedWord;
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
        private void loadCorpusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(loadCorpusDialog.ShowDialog(this) == DialogResult.OK)
            {
                //List<string> input = new List<string>();
                int i = 0;
                int velicinaModela = 3;
                languageModel = new LanguageModel(velicinaModela);
                string text = OpenFile(loadCorpusDialog.FileName);
                languageModel.run(text);
                brojRjeci.Text = languageModel.TotalNumberOfWords.ToString();
                MessageBox.Show("Corpus loaded!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void languageModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadCorpusForm corpusForm = new LoadCorpusForm();
            corpusForm.ShowDialog();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fontDialog.ShowDialog(this) == DialogResult.OK)
            {

            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {

            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string txt = File.ReadAllText(openFileDialog.FileName);
                    fileText.Text = txt;
                  
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Could not read file from disk!");
                }
            }
        }

        private void fileText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (autoCompleteList.Items.Count > 0)
                {
                    e.SuppressKeyPress = true;
                    autoCompleteList.Hide();
                    string[] data = autoCompleteList.SelectedItem.ToString().Split(' ');
                    string word = data[0].Substring(getLastUncompletedWord().Length);
                    fileText.Text += word; //uzmi samo dio rijeci
                    fileText.SelectionStart = fileText.Text.Last();
                    autoCompleteList.Hide();
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                if (autoCompleteList.Items.Count > 0)
                {
                    int indeks = autoCompleteList.SelectedIndex;
                    if (indeks + 1 > autoCompleteList.Items.Count - 1)
                    {
                        indeks = 0;
                    }
                    else
                    {
                        indeks++;
                    }
                    autoCompleteList.SetSelected(indeks, true);
                }
            }
            else if(e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                if(autoCompleteList.Items.Count > 0)
                {
                    int indeks = autoCompleteList.SelectedIndex;
                    if(indeks == 0)
                    {
                        indeks = autoCompleteList.Items.Count - 1;
                    }
                    else
                    {
                        indeks--;
                    }
                    autoCompleteList.SetSelected(indeks, true);
                }
            }
            else if(e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                autoCompleteList.DataSource = null;
            }
        }

    }
}
