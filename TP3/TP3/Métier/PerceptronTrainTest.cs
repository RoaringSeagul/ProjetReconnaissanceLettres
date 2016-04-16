using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPARCHIPERCEPTRON.Utilitaires;

namespace TPARCHIPERCEPTRON.Métier
{
    public class PerceptronTrainTest
    {
        private readonly Double _ratio;
        public PerceptronTrainTest(double ratio = 1) {
            _ratio = ratio;
        }

        public string Entrainement(List<CoordDessin> coords, ref Dictionary<string, Perceptron> perceptrons)
        {
            string sConsole = "";
            foreach (var perceptron in perceptrons.Values)
            {
                ThreadPool.QueueUserWorkItem(delegate {
                    String s = perceptron.Entrainement(coords.GetRange(0, (int)((Double)coords.Count * _ratio)));
                    sConsole = sConsole + perceptron.Reponse + " : " + s + " ";
                });
            }
            return sConsole;
        }

        public string Test(List<CoordDessin> coords, List<Perceptron> perceptrons)
        {
            int nbTest = (int)((Double)coords.Count * (1 - _ratio));
            int nbBonneReponse = 0;
            foreach (var coord in coords.GetRange((int)((Double)coords.Count * _ratio), nbTest))
            {
                string reponse = "";
                int resultatReponse = 0;
                foreach (var perceptron in perceptrons)
                {
                    int valeur = perceptron.TesterNeurone(coord);
                    if (valeur > resultatReponse)
                    {
                        resultatReponse = valeur;
                        reponse = perceptron.Reponse;
                    }
                }
                if (reponse == coord.Reponse)
                {
                    nbBonneReponse++;
                }
            }
            return "Resultat: " + nbBonneReponse + " / " + nbTest;
        }
    }
}
