using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TarProjekt
{
    class BigramMaker
    {

   

        private List<string> SeperateSentences(List<string> input)
        {
            List<string> output = new List<string>();
            foreach(string text in input)
                output.AddRange(text.Split('.').ToList());
            return output;
        }
        
        private String RemoveExcessAndLowercase(string input)
        {
            StringBuilder sb = new StringBuilder(input.Length);
            bool lastWasASpace = false;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '\'' && (i+1)< input.Length && !char.IsLetter(input[i+1]))
                        ;
                else if ( c == '\'')
                {
                    sb.Append(Char.ToLower(input[i]));
                    lastWasASpace = false;
                }
                else if(c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z')
                {
                    sb.Append(Char.ToLower(input[i]));
                    lastWasASpace = false;
                }
                else
                {
                    if(!lastWasASpace)
                        sb.Append(' ');
                    lastWasASpace = true;
                }
               
            }

            return sb.ToString();
        }

        private List<String> Tokenize(string input)
        {
            List<String> output = new List<string>();
            output = input.Split(' ').ToList();
            output = output.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            return output;
        }

        private Dictionary<string,Ocurrences> CreateBigrams(List<List<string>> input)
        {
            Dictionary<string, Ocurrences> bigrams = new Dictionary<string, Ocurrences>();
            foreach (List<string> sentence in input)
            {
                for (int i = 0; i < sentence.Count; i++)
                {
                    if (bigrams.ContainsKey(sentence.ElementAt(i)))
                    {
                        bigrams[sentence.ElementAt(i)].increaseCount();
                        if (i + 1 < sentence.Count)
                            bigrams[sentence.ElementAt(i)].AddFollower(sentence.ElementAt(i + 1));
                    }
                    else
                    {
                        bigrams[sentence.ElementAt(i)] = new Ocurrences();
                        if (i + 1 < sentence.Count)
                            bigrams[sentence.ElementAt(i)].AddFollower(sentence.ElementAt(i + 1));
                    }

                }
            }
            
            return bigrams;
        }

        public void Run(List<string> input, int part)
        {
            List<string> sentences = SeperateSentences(input);
            List<List<string>> tokensPerSentence = new List<List<string>>();
            foreach (string sentence in sentences)
                tokensPerSentence.Add(Tokenize(RemoveExcessAndLowercase(sentence)));
            //DBC dbc = new DBC();
            //dbc.SaveToDB(CreateBigrams(tokensPerSentence));
            //WriteToFile(CreateBigrams(tokensPerSentence), part);
            WordPredicter wp = new WordPredicter(CreateBigrams(tokensPerSentence));
            SharedClass.wp = wp;

        }




        private void WriteToFile(Dictionary<string, Ocurrences> bigrams,int part)
        {
            StreamWriter file = new StreamWriter("./izlaz_part_"+part+".txt");
            string line = "";
            foreach(string word in bigrams.Keys)
            {
                line = word + ": " + bigrams[word].GetNumberOfOccurrences() + "\n";
                Dictionary<string, int> followers = bigrams[word].GetFollowers();
                foreach (string follower in followers.Keys)
                    line += "\t-" + follower + ": " + followers[follower] + "\n";
                file.WriteLine(line);
            }           
            file.Close();
        }



        
      


    }
}
