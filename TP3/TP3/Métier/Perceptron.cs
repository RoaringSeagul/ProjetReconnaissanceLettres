using System;
using System.Collections;
using System.Collections.Generic;

namespace TPARCHIPERCEPTRON
{
    /// <summary>
    /// Classe du perceptron. Permet de faire l'apprentissage automatique sur un échantillon d'apprentissage. 
    /// </summary>
    public class Perceptron
    {
        private readonly double _cstApprentissage;
        private double[] _poidsSyn;
        private string _reponse = "?";

        public string Reponse
        {
            get { return _reponse; }
        }

        /// <summary>
        /// Constructeur de la classe. Crée un perceptron pour une réponse(caractère) qu'on veut identifier le pattern(modèle)
        /// </summary>
        /// <param name="reponse">La classe que défini le perceptron</param>
        public Perceptron(string reponse, double cstApprentissage)
        {
            //À COMPLÉTER
            // On assigne notre constante d'apprentissage
            _cstApprentissage = cstApprentissage;

            // On crée notre tableau de poids synaptiques
            _poidsSyn = new Double[64 * 64];

            Random rnd = new Random();
            // On assigne des poids aléatoires à chaques coordonnées
            for (int i = 0; i < 64 * 64; i++)
            {
                _poidsSyn[i] = rnd.NextDouble();
            }
        }

        /// <summary>
        /// Faire l'apprentissage sur un ensemble de coordonnées. Ces coordonnées sont les coordonnées de tous les caractères analysés. 
        /// </summary>
        /// <param name="lstCoord">La liste de coordonnées pour les caractères à analysés.</param>
        /// <returns>Les paramètres de la console</returns>
        public string Entrainement(List<CoordDessin> lstCoord)
        {
            Int32 nbIteration = 0;
            Int32 nbErreur = 0;
            Int32 iResultatEstime = 0;
            Int32 erreurLocale = 0;
            String sResultat = "";
            do
            {
                nbErreur = 0;
                //for (int i = 0; i < ; i++)
                //{

                //}
            } while (nbErreur != 0 && nbIteration < 10000);
            return sResultat;
        }

        /// <summary>
        /// Calcul la valeur(vrai ou faux) pour un les coordonnées d'un caractère. Permet au perceptron d'évaluer la valeur de vérité.
        /// </summary>
        /// <param name="vecteurSyn">Les poids synaptiques du perceptron</param>
        /// <param name="entree">Le vecteur de bit correspondant aux couleurs du caractère</param>
        /// <returns>Vrai ou faux</returns>
        public int ValeurEstime(double[] vecteurSyn, BitArray entree)
        {
            //À COMPLÉTER
            return CstApplication.VRAI;
        }

        /// <summary>
        /// Interroge la neuronnes pour un ensembles des coordonnées(d'un caractère).
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public bool TesterNeurone(CoordDessin coord)
        {
            //À COMPLÉTER
            return CstApplication.VRAI == CstApplication.VRAI ? true : false;
        }

    }
}
