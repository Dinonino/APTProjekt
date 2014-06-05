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
    public class LanguageModel
    {
        private Dictionary<string, Word> nGrams;

        private int totalNumberOfNGrams;
        private int totalNumberOfWords;
        private long totalNumberOfDiferentBigrams; //potrebno za Kneser Ney metodu
        private int LMOrder;
        private static float KNESER_NEY_SMOOTING_DISCOUNT = 0.75f;
        public int TotalNumberOfWords
        {
            get
            {
                return totalNumberOfWords;
            }
            set
            {
                totalNumberOfWords = value;
            }
        }
        public LanguageModel(int LMOrder)
        {
            nGrams = new Dictionary<string, Word>();
            totalNumberOfDiferentBigrams = 0;
            totalNumberOfWords = 0;
            totalNumberOfNGrams = 0;
            this.LMOrder = LMOrder;
        }
        public void run(string input)
        {
            List<string> sentences = SeperateSentences(input);
            List<List<string>> content = new List<List<string>>();
            foreach(string sentence in sentences)
            {
                content.Add(Tokenize(RemoveExcessAndLowercase(sentence)));
            }
            createModel(content);
        }

        private List<string> SeperateSentences(string input)
        {
            List<string> output = new List<string>();
            output.AddRange(input.Split('.').ToList());
            return output;
        }
        public int getLMOrder()
        {
            return LMOrder;
        }
        private string RemoveExcessAndLowercase(string input)
        {
            StringBuilder sb = new StringBuilder(input.Length);
            bool lastWasASpace = false;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z')
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
        private void createModel(List<List<string>> input)
        {

            foreach (List<string> sentence in input)
            {
                for (int i = 0; i < sentence.Count; i++)
                {
                    string word = sentence.ElementAt(i);
                    totalNumberOfWords++;
                    if (nGrams.ContainsKey(word))
                    {
                        nGrams[word].NumberOfOccurences++;
                    }
                    else
                    {
                        Word newWord = new Word(word);
                        nGrams.Add(word, newWord);
                    }

                    //u ovoj petlji se prave n-grami reda koji je zadan modelom
                    //svaka rijec sadrzi svoje slijedece i za svaki n-gram se prati 
                    //broj ponavljivanja, pogledat klasu Word.
                    Dictionary<string, Word> forUpdate = nGrams[word].FollowersInLanguageModel;
                    for (int j = 1; j <= LMOrder; j++)
                    {
                        if (!(i + j < sentence.Count))
                        {
                            break;
                        }
                        string wordToInsert = sentence.ElementAt(i + j);
                        if (forUpdate.ContainsKey(wordToInsert))
                        {
                            forUpdate[wordToInsert].NumberOfOccurences++;
                        }
                        else
                        {
                            Word newWord = new Word(wordToInsert);
                            forUpdate.Add(wordToInsert, newWord);
                        }
                        forUpdate = forUpdate[wordToInsert].FollowersInLanguageModel;
                    }
                }
            }
            WriteToFile();
        }
        private void setTotalNumberOfDifferentBigrams()
        {
            totalNumberOfNGrams = 0;
            foreach (Word w in nGrams.Values)
            {
                Dictionary<string, Word> nextWords = w.FollowersInLanguageModel;
                if (nextWords != null)
                {
                    totalNumberOfDiferentBigrams += nextWords.Values.Count;
                }
            }
        }
        private int totalNumberOfBigramsForSomeWord(Word word)
        {
            int totalNumberOfBigrams = 0;
            foreach (Word ww in word.FollowersInLanguageModel.Values) //sada zbrajamo sva pojavljivanja bigrama
            {
                totalNumberOfBigrams += ww.NumberOfOccurences;
            }
            return totalNumberOfBigrams;
        }
        private float computeKNProbability(Word wordFromNgram, Word previousWord)
        {
            int numberOfDiffBigramsOfWord = wordFromNgram.FollowersInLanguageModel.Values.Count;
            //continuation probability
            float continuationProbability = numberOfDiffBigramsOfWord / totalNumberOfDiferentBigrams;
            //normalizing constant
            int countW_1 = previousWord.NumberOfOccurences;
            int numOfWordsCanFolowW_1 = previousWord.FollowersInLanguageModel.Values.Count;

            float lambdaWi_1 = (KNESER_NEY_SMOOTING_DISCOUNT / countW_1) * numOfWordsCanFolowW_1;

            //Kneser Ney probability
            int numOfBigramsOfWord = totalNumberOfBigramsForSomeWord(wordFromNgram);
            float knProbability = (Math.Max(numOfBigramsOfWord - KNESER_NEY_SMOOTING_DISCOUNT, 0) / countW_1)
                               + lambdaWi_1 * continuationProbability;

            return knProbability;
        }
        public List<Word> mostOccuringWords(int numberOfWords)
        {
            List<Word> result = nGrams.Values.ToList();
            result.Sort((w1, w2) => w1.NumberOfOccurences.CompareTo(w2.NumberOfOccurences));
            if(numberOfWords < result.Count)
            {
                 int position = result.Count - 1 - numberOfWords;
                 result = result.GetRange(position, numberOfWords);
            }
            for(int i = 0; i < result.Count; i++)
            {
                Word elementAt = result.ElementAt(i);
                double evaluatedProbability = (double) elementAt.NumberOfOccurences / totalNumberOfWords;
                elementAt.EvaluatedProbability = evaluatedProbability;
            }
            result = result.OrderByDescending(x => x.EvaluatedProbability).ToList();
            return result;
        }
        //Good Turing smooting
        public SortedList<float, Word> doGoodTuringSmoot()
        {
            //to do
            return new SortedList<float, Word>();
        }
        //Kenser Ney je rekurzivna metoda smootinga
        public List<Word> doKneserNeySmoot(List<string> words)
        {
            //pronadi ukupan 
            setTotalNumberOfDifferentBigrams();
            //words je sizea za jedan manje od velicine modela
            string firstWordFromNgram = words.ElementAt(0);
            try
            {
                Word word = nGrams[firstWordFromNgram];

                //ne postoji n-gram koji sadrzi prvu trazenu rijec
                //ne mozemo provesti Kneser Ney smoot
                if (word == null)
                {
                    return null;
                }

                //prva rijec je razlicita, polazi se za provjerom i podesavanjem vjerojatnosti rijeci
                Dictionary<string, Word> forProcessing = nGrams[firstWordFromNgram].FollowersInLanguageModel;
                float preveousProbability = 0F;

                for (int i = 1; i < LMOrder - 1; i++)
                {
                    string nextWord = words.ElementAt(i);
                    Word wordFromNgram = forProcessing[nextWord];

                    //ako bilo koja od zadanih rijeci se ne nalazi u n-gramu stupnja n-1 
                    //ne mozemo provesti Kneser Ney smoot
                    if (wordFromNgram == null)
                    {
                        return null;
                    }

                    float knProbabilityOfWord = computeKNProbability(wordFromNgram, word);

                    preveousProbability = knProbabilityOfWord;
                    word = wordFromNgram; //word sluzi za pamcenje prijasnje rijeci
                    forProcessing = word.FollowersInLanguageModel;
                }
                //sada treba izracunati vjerojatnost za svaku rijec svih n-grama
                List<Word> results = new List<Word>();
                foreach (Word w in forProcessing.Values)
                {
                    float knProbabilityOfWord = computeKNProbability(w, word);
                    w.EvaluatedProbability = knProbabilityOfWord;
                    results.Add(w);
                }
                results = results.OrderByDescending(x => x.EvaluatedProbability).ToList();
                return results;
            }
            catch(Exception)
            {
                List<Word> list = new List<Word>();
                return mostOccuringWords(5);
            }

        }

        private void WriteToFile()
        {
            StreamWriter file = new StreamWriter("./izlaz.txt");
            string line = "";
            Dictionary<string, Word> forProcesing = null;
            //sad slijedi jedan od glupljih kodova ikad
            switch (LMOrder)
            {
                case 2:
                    {
                        foreach (Word word in nGrams.Values)
                        {
                            line = word.ToString();
                            file.WriteLine(line);
                            foreach (Word w in word.FollowersInLanguageModel.Values)
                            {
                                line = "\t" + w.ToString();
                                file.WriteLine(line);
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        foreach (Word word in nGrams.Values)
                        {
                            line = word.ToString();
                            file.WriteLine(line);
                            foreach (Word w in word.FollowersInLanguageModel.Values)
                            {
                                line = "\t" + w.ToString();
                                file.WriteLine(line);
                                foreach (Word ww in w.FollowersInLanguageModel.Values)
                                {
                                    line = "\t\t" + ww.ToString();
                                    file.WriteLine(line);
                                }
                            }
                        }
                        file.Write(line);
                        break;
                    }
                case 4:
                    {
                        foreach(Word word in nGrams.Values)
                        {
                            line = word.ToString();
                            file.WriteLine(line);
                            foreach (Word w in word.FollowersInLanguageModel.Values)
                            {
                                line = "\t" + w.ToString();
                                file.WriteLine(line);
                                foreach(Word ww in w.FollowersInLanguageModel.Values)
                                {
                                    line = "\t\t" + ww.ToString();
                                    file.WriteLine(line);
                                    foreach(Word www in ww.FollowersInLanguageModel.Values)
                                    {
                                        line = "\t\t\t" + www.ToString();
                                        file.WriteLine(line);
                                    }
                                }
                            }
                        }
                        break;
                    }
            }
            file.Close();
        }
    }
}
