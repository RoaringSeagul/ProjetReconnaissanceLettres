using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using TPARCHIPERCEPTRON.Utilitaires;

namespace TPARCHIPERCEPTRON.Métier
{
    /// <summary>
    /// Classe du perceptron. Permet de faire l'apprentissage automatique sur un échantillon d'apprentissage. 
    /// </summary>
    public class Perceptron
    {
        private readonly double _cstApprentissage;
        private double[] _poidsSyn;
        private string _reponse;

        public ImageFormat Format { get; set; }

        public string Reponse
        {
            get { return _reponse; }
        }

        /// <summary>
        /// Constructeur de la classe. Crée un perceptron pour une réponse(caractère) qu'on veut identifier le pattern(modèle)
        /// </summary>
        /// <param name="reponse">La classe que défini le perceptron</param>
        public Perceptron(string reponse, double cstApprentissage, ImageFormat format)
        {
            //À COMPLÉTER
            // On assigne notre constante d'apprentissage
            _cstApprentissage = cstApprentissage;

            Format = format;

            // On crée notre tableau de poids synaptiques
            _poidsSyn = new Double[format.X * format.Y];

            Random rnd = new Random();
            // On assigne des poids aléatoires à chaques coordonnées
            for (int i = 0; i < _poidsSyn.Length; i++)
            {
                _poidsSyn[i] = rnd.NextDouble();
            }

            _reponse = reponse;
        }

        /// <summary>
        /// Faire l'apprentissage sur un ensemble de coordonnées. Ces coordonnées sont les coordonnées de tous les caractères analysés. 
        /// </summary>
        /// <param name="lstCoord">La liste de coordonnées pour les caractères à analysés.</param>
        /// <returns>Les paramètres de la console</returns>
        public string Entrainement(List<CoordDessin> _lstCoords)
        {
            string s = "";
            Int32 nbErreur = 0;
            Int32 nbIteration = 0;
            List<int> lstNbErreur = new List<int>();    
            do
            {
                nbErreur = 0;
                foreach (var coord in _lstCoords)
                {
                    int iEstBonneLettre = (coord.Reponse == this.Reponse ? 1 : 0);
                    int iBonneValeurSelonPerceptron = ValeurEstime(_poidsSyn, coord.BitArrayDessin);
                    int iErreurLocale = iEstBonneLettre - iBonneValeurSelonPerceptron;

                    if (iErreurLocale != 0)
                    {
                        //Console.WriteLine(this.Reponse + " " + nbIteration);
                        _poidsSyn[0] += _cstApprentissage * iErreurLocale;
                        for (int i = 1; i < _poidsSyn.Length; i++)
                        {
                            _poidsSyn[i] += _cstApprentissage * iErreurLocale * (coord.BitArrayDessin[i] ? 1 : -1);
                        }
                        nbErreur++;
                    }
                }
                nbIteration++;
            }while (nbErreur != 0 && nbIteration < 10000);
            return s;
        }

        /// <summary>
        /// Calcul la valeur(vrai ou faux) pour un les coordonnées d'un caractère. Permet au perceptron d'évaluer la valeur de vérité.
        /// </summary>
        /// <param name="vecteurSyn">Les poids synaptiques du perceptron</param>
        /// <param name="entree">Le vecteur de bit correspondant aux couleurs du caractère</param>
        /// <returns>Vrai ou faux</returns>
        public int ValeurEstime(double[] vecteurSyn, BitArray entree)
        {
            double somme = _poidsSyn[0];
            for (int i = 1; i < _poidsSyn.Length; i++)
                somme += _poidsSyn[i] * (entree[i] ? 1 : -1);

            return somme >= 0 ? 1 : 0;
        }

        /// <summary>
        /// Interroge la neuronnes pour un ensembles des coordonnées(d'un caractère).
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public int TesterNeurone(CoordDessin coord)
        {
            bool estBonneLettre = (coord.Reponse == this.Reponse);
            return ValeurEstime(_poidsSyn, coord.BitArrayDessin);
        }

        public double GetWeightAt(uint x, uint y)
        {
            if (x <= Format.X && y <= Format.Y)
                return _poidsSyn[x * Format.Y + y];
            else
                return double.MaxValue;
        }

    }
}
