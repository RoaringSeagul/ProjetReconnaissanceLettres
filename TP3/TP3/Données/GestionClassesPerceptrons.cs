using System.Collections.Generic;

namespace TPARCHIPERCEPTRON
{
    /// <summary>
    /// Gère les fonctionnalités de l'application. Entre autre, permet de :
    /// - Charger les échantillons d'apprentissage,
    /// - Sauvegarder les échantillons d'apprentissage,
    /// - Tester le perceptron
    /// - Entrainer le perceptron
    /// </summary>
    public class GestionClassesPerceptrons
    {
        private Dictionary<string, Perceptron> _lstPerceptrons;
        private GestionFichiersSorties _gestionSortie;

        /// <summary>
        /// Constructeur
        /// </summary>
        public GestionClassesPerceptrons()
        {
            _lstPerceptrons = new Dictionary<string, Perceptron>();
            _gestionSortie = new GestionFichiersSorties();
            
        }

        /// <summary>
        /// Charge les échantillons d'apprentissage sauvegardé dans une base de données.
        /// </summary>
        public void ChargerCoordonnees()
        {
            //À COMPLÉTER
        }

        /// <summary>
        /// Sauvegarde les échantillons d'apprentissage dans une base de données.
        /// </summary>
        /// <returns>En cas d'erreur retourne le code d'erreur</returns>
        public int SauvegarderCoordonnees()
        {
            int erreur = CstApplication.ERREUR;
            //À COMPLÉTER
            return erreur;
        }

        /// <summary>
        /// Entraine les perceptrons avec un nouveau caractère
        /// </summary>
        /// <param name="coordo">Les nouvelles coordonnées</param>
        /// <param name="reponse">La réponse associé(caractère) aux coordonnées</param>
        /// <returns>Le résultat de la console</returns>
        public string Entrainement(CoordDessin coordo, string reponse)
        {
            string sConsole = "";

            if (!_lstPerceptrons.ContainsKey(reponse))
            {
                _lstPerceptrons.Add(reponse, new Perceptron(reponse, 0.10));
            }

            foreach (var perceptron in _lstPerceptrons)
            {
                sConsole = sConsole + perceptron.Key + " : " + perceptron.Value.Entrainement(new List<CoordDessin> { coordo }) + " ";
            }

            return sConsole;
        }


        /// <summary>
        /// Test le perceptron avec de nouvelles coordonnées.
        /// </summary>
        /// <param name="coord">Les nouvelles coordonnées</param>
        /// <returns>Retourne la liste des valeurs possibles du perceptron</returns>
        public string TesterPerceptron(CoordDessin coord)
        {
            string resultat = "";

            foreach (var perceptron in _lstPerceptrons)
            {
                if (perceptron.Value.TesterNeurone(coord))
                {
                    resultat = resultat + perceptron.Key;
                }
            }

            return resultat;
        }

        /// <summary>
        /// Obtient une liste des coordonées.
        /// </summary>
        /// <returns>Une liste des coordonées.</returns>
        public IList<CoordDessin> ObtenirCoordonnees()
        {
            return _gestionSortie.ObtenirCoordonnees();
        }
    }
}
