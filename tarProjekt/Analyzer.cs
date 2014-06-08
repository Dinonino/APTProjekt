using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarProjekt
{
    class Analyzer
    {

        LanguageModel languageModel;

        public Analyzer(LanguageModel lm)
        {
            this.languageModel = lm;
        }
        
        public double CalculateCrossEntropy(List<List<String>> testSet)
        {
            double res = 0;
            double wordCount = 0;
            foreach (List<String> sentence in testSet)
            {
                res += CalculateProbabilityOfSentence(sentence);
                wordCount += sentence.Count - 2* languageModel.getLMOrder();
            }
            res = -1 * res / wordCount;
            return res;

        }

        private double CalculateProbabilityOfSentence(List<String> sentence)
        {
            double probabilityInLog2 = 0;
            List<Word> predictions;
            for (int i = 0; i < sentence.Count - 2; i++)
            {
                List<string> wordsForPrediction = sentence.GetRange(i, languageModel.getLMOrder());
                predictions = languageModel.doKneserNeySmooth(wordsForPrediction);
                bool predictionExists = false;
                Word foundPrediction = null;
                foreach (Word prediction in predictions)
                    if (prediction.Content == sentence.ElementAt(i + 2))
                    {
                        predictionExists = true;
                        foundPrediction = prediction;
                    }
                if (predictionExists && foundPrediction.EvaluatedProbability > 0)
                    probabilityInLog2 += TransformToLogFormat(foundPrediction.EvaluatedProbability);
                else
                    probabilityInLog2 += TransformToLogFormat(1.0/1000); 

            }
             return probabilityInLog2;
        }

        private double TransformToLogFormat(double num)
        {
            return Math.Log(num, 2);
        }
    }
}
