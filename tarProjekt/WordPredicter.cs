using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarProjekt
{
    
    
    class WordNumberPair
    {
        string word;
        int numberOfOccurences;

        public string Word
        {
          get { return word; }
          set { word = value; }
        }
        

        public int NumberOfOccurences
        {
          get { return numberOfOccurences; }
          set { numberOfOccurences = value; }
        }

        

        public WordNumberPair(string word, int num)
        {
            this.word = word;
            this.numberOfOccurences = num;
        }
    }
    
    
    public class WordPredicter
    {
        private static int numOfSuggestions = 10; 
        
        Dictionary<string, Ocurrences> bigramData;
        List<WordNumberPair> allWords = new List<WordNumberPair>();

        public WordPredicter(Dictionary<string, Ocurrences> bigramData)
        {
            this.bigramData = bigramData;
            allWords = SortBigramWords();

        }

        private List<WordNumberPair> SortBigramWords()
        {
            List<WordNumberPair> sortedPairs = new List<WordNumberPair>();


            foreach(string key in bigramData.Keys)
            {
                WordNumberPair wnp = new WordNumberPair(key, bigramData[key].GetNumberOfOccurrences());
                sortedPairs.Add(wnp);
            }

            sortedPairs.Sort((firstPair,nextPair) =>
                {
                    return nextPair.NumberOfOccurences.CompareTo(firstPair.NumberOfOccurences);
                }
            );
            int sum = 0;
            foreach (WordNumberPair wp in sortedPairs)
                sum += wp.NumberOfOccurences;
            return sortedPairs;
        }

        private List<WordNumberPair> SortFollowers(Dictionary<string, int> followers)
        {
            List<WordNumberPair> sortedFollowers = new List<WordNumberPair>();

            foreach (string key in followers.Keys)
            {
                WordNumberPair wnp = new WordNumberPair(key, followers[key]);
                sortedFollowers.Add(wnp);
            }

            sortedFollowers.Sort((firstPair, nextPair) =>
            {
                return (nextPair.NumberOfOccurences / bigramData[nextPair.Word].GetNumberOfOccurrences()).CompareTo(firstPair.NumberOfOccurences /  bigramData[firstPair.Word].GetNumberOfOccurrences());
            }
            );

            return sortedFollowers;
        }

        public List<string> GetPredictions(string currentLetters, string predecessor)
        { 
            List<string> predictions = new List<string>();
            int i = 0;
            if (bigramData.ContainsKey(predecessor))
            {
                List<WordNumberPair> sortedFollowers = SortFollowers(bigramData[predecessor].GetFollowers());
                foreach (WordNumberPair follower in sortedFollowers)
                {
                    if (i <= numOfSuggestions && follower.Word.StartsWith(currentLetters))
                    {
                        predictions.Add(follower.Word);
                        i++;
                    }
                }
            }
            int rest = numOfSuggestions - i;
            foreach (WordNumberPair word in allWords)
            {
                if (i <= numOfSuggestions && word.Word.StartsWith(currentLetters) && !predictions.Contains(word.Word))
                {
                    predictions.Add(word.Word);
                    i++;
                }
            }

            
            return predictions;
        }
    }
}
