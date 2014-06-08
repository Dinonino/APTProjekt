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
        
        public double[] CalculateCrossEntropy(List<List<String>> testSet)
        {
            double[] res = {0,0};
            double[] singularRes;
            double wordCount = 0;
            foreach (List<String> sentence in testSet)
            {
                singularRes = CalculateProbabilityOfSentence(sentence);
                res[0]+= singularRes[0];
                res[1]+= singularRes[1];
                wordCount += sentence.Count - 2* languageModel.getLMOrder();
            }
            res[0] = -1 * res[0] / wordCount;
            res[1] = -1 * res[1] / wordCount;
            return res;

        }

        private double[] CalculateProbabilityOfSentence(List<String> sentence)
        {
            double[] result = new double[2];

            #region kneser.ney
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
            result[0]=(probabilityInLog2);
            #endregion

            #region addOne
            probabilityInLog2 = 0;
            predictions = null;
            for (int i = 0; i < sentence.Count - 2; i++)
            {
                List<string> wordsForPrediction = sentence.GetRange(i, languageModel.getLMOrder());
                predictions = languageModel.addOneSmooting(wordsForPrediction);
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
                    probabilityInLog2 += TransformToLogFormat(1.0 / 1000);

            }
            result[1]=(probabilityInLog2);
            #endregion
            return result;
        }

        private double TransformToLogFormat(double num)
        {
            return Math.Log(num, 2);
        }
    }
}
