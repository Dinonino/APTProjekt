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
    public enum Smoot
    {
        KNESER_NEY_SMOOTH, ADAPTIVE_SMOOTH, GOOD_TURING_SMOOTH
    }
    public class LanguageModel
    {
        private Dictionary<string, Word> nGrams;

        private int totalNumberOfNGrams;
        private int totalNumberOfWords;
        private long totalNumberOfDiferentBigrams; //potrebno za Kneser Ney metodu
        private int LMOrder;
        private int numberOfSentences;

        private float averageSentenceLength;
        private static float KNESER_NEY_SMOOTING_DISCOUNT = 0.75f;
        public int NumberOfSentences
        {
             get { return numberOfSentences; }
             set { numberOfSentences = value; }
        }
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
        public float AverageSentenceLenght
        {
            get
            {
                return averageSentenceLength;
            }
            set
            {
                averageSentenceLength = value;
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
            InputFormatter inputFormatter = new InputFormatter();
            List<List<string>> content = inputFormatter.FormatData(input);
            createModel(content);
        }
        public List<string> evaluateModel()
        {
            return new List<string>();
        }
        public int getLMOrder()
        {
            return LMOrder;
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
            averageSentenceLength = (float)totalNumberOfWords / input.Count; 
            numberOfSentences = input.Count;
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
        //Add one smooting
        public List<Word> addOneSmooting(List<string> words)
        {
            List<Word> result = new List<Word>();
            string firstWordFromNgram = words.ElementAt(0);
            try
            {
                Word word = nGrams[firstWordFromNgram];

                Dictionary<string, Word> forProcessing = nGrams[firstWordFromNgram].FollowersInLanguageModel;
                for(int i = 1; i < LMOrder - 1; i++)
                {
                    string nextWord = words.ElementAt(i);
                    Word wordFromNgram = forProcessing[nextWord];

                    //word 
                    word = wordFromNgram; //pamcenje prijasnje rijeci
                    forProcessing = word.FollowersInLanguageModel;
                }
                //tu prebrojim broj rjeci s kojima se ne tvori nGram(redna n - 1)
                //u biti od ukupno broja rjeci oduzmemo broj nGrama(reda n) koji tvori n - 1 gram
                //svakome tome koji ne postoji morat ce se dodat 1
                //i tako V puta
                foreach(Word w in forProcessing.Values)
                {
                    double evaluatedProbability = (double)(w.NumberOfOccurences + 1) / (word.NumberOfOccurences + nGrams.Values.Count);
                    w.EvaluatedProbability = evaluatedProbability;
                    result.Add(w);
                }
                return result.OrderByDescending(x => x.EvaluatedProbability).ToList();
            }
            catch(Exception)
            {
                return mostOccuringWords(10).OrderByDescending(x => x.EvaluatedProbability).ToList();
            }
        }
        //Good Turing smooting
        public SortedList<float, Word> doGoodTuringSmoot()
        {
            //to do
            return new SortedList<float, Word>();
        }
        //Kenser Ney je rekurzivna metoda smootinga
        public List<Word> doKneserNeySmooth(List<string> words)
        {
            //pronadi ukupan 
            setTotalNumberOfDifferentBigrams();
            //words je sizea za jedan manje od velicine modela
            string firstWordFromNgram = words.ElementAt(0);
            try
            {
                Word word = nGrams[firstWordFromNgram];

                //prva rijec je razlicita, polazi se za provjerom i podesavanjem vjerojatnosti rijeci
                Dictionary<string, Word> forProcessing = nGrams[firstWordFromNgram].FollowersInLanguageModel;
                float preveousProbability = 0F;

                for (int i = 1; i < LMOrder - 1; i++)
                {
                    string nextWord = words.ElementAt(i);
                    Word wordFromNgram = forProcessing[nextWord];

                    //ako bilo koja od zadanih rijeci se ne nalazi u n-gramu stupnja n-1 
                    //ne mozemo provesti Kneser Ney smoot

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
                return mostOccuringWords(5).OrderByDescending(x => x.EvaluatedProbability).ToList();
            }

        }
        private void computePerplexityCrossEntropy(List<List <string>> testSentences)
        {

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
