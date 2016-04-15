using System;
using System.Collections;
using System.Collections.Generic;

namespace TPARCHIPERCEPTRON
{
    /// <summary>
    /// Classe du perceptron. Permet de faire l'apprentissage automatique sur un échantillon d'apprentissage. 
    /// </summary>
    public class LocalPerceptron
    {
        private readonly double _cstApprentissage;
        private double[] _poidsSyn;
        private string _reponse;

        public string Reponse
        {
            get { return _reponse; }
        }

        /// <summary>
        /// Constructeur de la classe. Crée un perceptron pour une réponse(caractère) qu'on veut identifier le pattern(modèle)
        /// </summary>
        /// <param name="reponse">La classe que défini le perceptron</param>
        public LocalPerceptron(string reponse, double cstApprentissage)
        {
            //À COMPLÉTER
            // On assigne notre constante d'apprentissage
            _cstApprentissage = cstApprentissage;

            // On crée notre tableau de poids synaptiques
            _poidsSyn = new Double[16 * 16];

            Random rnd = new Random();
            // On assigne des poids aléatoires à chaques coordonnées
            for (int i = 0; i < 16 * 16; i++)
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
            String sResultat = "";
            Int32 nbIteration = 0;
            Int32 nbErreur = 0;
            Int32 iResultatEstime = 0;
            Int32 erreurLocale = 0;
            string s = "";

            foreach (var coord in _lstCoords)
            {
                bool estBonneLettre = (coord.Reponse == this.Reponse);
                bool estBonneValeurSelonPerceptron = (ValeurEstime(_poidsSyn, coord.BitArrayDessin) == 1);
                bool estBonneValeurEstime = ((estBonneLettre && estBonneValeurSelonPerceptron) || (!estBonneLettre && !estBonneValeurSelonPerceptron));
                s = (estBonneValeurEstime ? "1" : "0");
                if (!estBonneValeurEstime && estBonneLettre)
                {
                    for (int i = 0; i < _poidsSyn.Length; i++)
                    {
                        _poidsSyn[i] += _cstApprentissage * (coord.BitArrayDessin[i] ? 1 : -1);
                    }
                }
                else if(!estBonneLettre && !estBonneValeurEstime)
                {
                    for (int i = 0; i < _poidsSyn.Length; i++)
                    {
                        _poidsSyn[i] += _cstApprentissage * (coord.BitArrayDessin[i] ? -1 : 1);
                    }
                }
            }
            
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

            return (somme / _poidsSyn.Length)  >= 0 ? CstApplication.VRAI : CstApplication.FAUX;
        }

        /// <summary>
        /// Interroge la neuronnes pour un ensembles des coordonnées(d'un caractère).
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public bool TesterNeurone(CoordDessin coord)
        {
            bool estBonneLettre = (coord.Reponse == this.Reponse);
            bool estBonneValeurSelonPerceptron = (ValeurEstime(_poidsSyn, coord.BitArrayDessin) == 1);
            return estBonneValeurSelonPerceptron;
        }

    }
}
