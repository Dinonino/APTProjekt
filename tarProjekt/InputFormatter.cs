using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarProjekt
{
    class InputFormatter
    {
        
        public List<List<string>> FormatData(string input)
        {
            List<string> sentences = SeperateSentences(input);
            List<List<string>> content = new List<List<string>>();
            foreach(string sentence in sentences)
            {
                content.Add(Tokenize(RemoveExcessAndLowercase(sentence)));
            }
            return content;
        }
        
        private List<string> SeperateSentences(string input)
        {
            List<string> output = new List<string>();
            output.AddRange(input.Split('.').ToList());
            return output;
        }

        private string RemoveExcessAndLowercase(string input)
        {
            StringBuilder sb = new StringBuilder(input.Length);
            bool lastWasASpace = false;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (i + 1 < input.Length && c == '\'' && Char.IsLetter(input[i + 1]))
                {
                    sb.Append(Char.ToLower(input[i]));
                    lastWasASpace = false;
                }
                else if (Char.IsLetter(c))
                {
                    sb.Append(Char.ToLower(input[i]));
                    lastWasASpace = false;
                }
                else
                {
                    if (!lastWasASpace)
                        sb.Append(' ');
                    lastWasASpace = true;
                }
            }

            return sb.ToString();
        }

        private List<string> Tokenize(string input)
        {
            List<string> output = new List<string>();
            output = input.Split(' ').ToList();
            output = output.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            return output;
        }

    }
}
