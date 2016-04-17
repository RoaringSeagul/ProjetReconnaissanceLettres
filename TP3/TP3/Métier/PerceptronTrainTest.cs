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

        public void Entrainement(List<CoordDessin> coords, ref Dictionary<string, Perceptron> perceptrons)
        {
            foreach (var perceptron in perceptrons.Values)
            {
                ThreadPool.QueueUserWorkItem(delegate {
                    perceptron.Entrainement(coords.GetRange(0, (int)((Double)coords.Count * _ratio)));
                });
            }
        }

        public string Test(List<CoordDessin> coords, Dictionary<string, Perceptron> perceptrons)
        {
            int nbTest = coords.Count;
            int nbBonneReponse = 0;
            foreach (var coord in coords)
            {
                string reponse = "";
                int resultatReponse = Int32.MinValue;
                foreach (var perceptron in perceptrons.Values)
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

        public string Test(CoordDessin coord, ref Dictionary<string, Perceptron> perceptrons)
        {
            string reponse = "";
            int resultatReponse = Int32.MinValue;
            foreach (var perceptron in perceptrons.Values)
            {
                int valeur = perceptron.TesterNeurone(coord);
                if (valeur > resultatReponse)
                {
                    resultatReponse = valeur;
                    reponse = perceptron.Reponse;
                }
            }

            return reponse;
        }
    }
}
