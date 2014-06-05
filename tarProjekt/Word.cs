using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarProjekt
{
    public class Word
    {
        private string content;
        private double evaluatedProbability;
        private int numberOfOccurences;
        private Dictionary<string, Word> followersInLanguageModel;

        public Word(string content)
        {
            this.content = content;
            followersInLanguageModel = new Dictionary<string, Word>();
            numberOfOccurences = 1;
        }
        public Dictionary<string, Word> FollowersInLanguageModel
        {
            get
            {
                return followersInLanguageModel;
            }
            set
            {
                followersInLanguageModel = value;
            }
        }
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }
        public double EvaluatedProbability
        {
            get
            {
                return evaluatedProbability;
            }
            set
            {
                evaluatedProbability = value;
            }
        }
        public int NumberOfOccurences
        {
            get
            {
                return numberOfOccurences;
            }
            set
            {
                numberOfOccurences = value;
            }
        }
        public override string ToString()
        {
            return content + " " + evaluatedProbability;
        }
    }
}
