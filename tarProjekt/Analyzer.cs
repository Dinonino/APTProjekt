using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarProjekt
{
    class Analyzer
    {

        LanguageModel lm;

        public Analyzer(LanguageModel lm)
        {
            this.lm = lm;
        }
        
        public void CalculateCrossEntropy(List<List<String>> testSet)
        { 
            
        }

        private double CalculateProbabilityOfSentence(List<String> sentence)
        {
            double probability = 1;

            return probability;
        }
    }
}
