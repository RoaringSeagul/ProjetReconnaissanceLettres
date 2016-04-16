using System.Collections.Generic;
using TPARCHIPERCEPTRON.Données;
using TPARCHIPERCEPTRON.Utilitaires;
using System;
using System.Threading;

namespace TPARCHIPERCEPTRON.Métier
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
        private PerceptronTrainTest _perceptronTrainTest;
        private TypeEntrainement _typeEntrainement = TypeEntrainement.Manuel;
        private IPerceptronData _gestionPerceptron;
        private ICharData _gestionSortie;

        /// <summary>
        /// Constructeur
        /// </summary>
        public GestionClassesPerceptrons(TypeEntrainement typeEntrainement)
        {
            _lstPerceptrons = new Dictionary<string, Perceptron>();
            ChargerCoordonnees(typeEntrainement);
        }

        /// <summary>
        /// Charge les échantillons d'apprentissage sauvegardé dans une base de données.
        /// </summary>
        public void ChargerCoordonnees(TypeEntrainement typeEntrainement)
        {
            _perceptronTrainTest = new PerceptronTrainTest();
            switch (typeEntrainement)
            {
                case TypeEntrainement.Manuel:
                    _gestionSortie = new GestionCharFichiersSorties();
                    break;
                case TypeEntrainement.MNIST:
                    _gestionSortie = new GestionChiffresManuscripts();
                    break;
                case TypeEntrainement.BD:
                    _gestionSortie = new GestionCharFichiersSorties(true);
                    break;
                default:
                    throw new NotImplementedException(); // TODO: Find something to put here
                    break;
            }

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
            if (_lstPerceptrons.Count == 0)
                Entrainement();

            if (reponse == "")
                return "";

            string sConsole = "";

            if (!_lstPerceptrons.ContainsKey(reponse))
            {
                _lstPerceptrons.Add(reponse, new Perceptron(reponse, 0.1, _gestionSortie.GetFormat()));
            }

            foreach (var perceptron in _lstPerceptrons)
            {
                sConsole = sConsole + perceptron.Key + " : " + perceptron.Value.Entrainement(new List<CoordDessin> { coordo }) + " ";
            }

            return sConsole;
        }

        /// <summary>
        /// Entraine le perceptron avec les données de la source spécifiée dans le constructeur.
        /// </summary>
        /// <returns></returns>
        public string Entrainement()
        {
            string sConsole = "";

            foreach (var coord in _gestionSortie.GetTrainData())
            {
                if (!_lstPerceptrons.ContainsKey(coord.Reponse))
                {
                    _lstPerceptrons.Add(coord.Reponse, new Perceptron(coord.Reponse, 0.1, _gestionSortie.GetFormat()));
                }
            }

            _perceptronTrainTest.Entrainement(_gestionSortie.GetTrainData(), ref _lstPerceptrons);

            return sConsole;
        }

        /// <summary>
        /// Test le perceptron avec de nouvelles coordonnées.
        /// </summary>
        /// <param name="coord">Les nouvelles coordonnées</param>
        /// <returns>Retourne la liste des valeurs possibles du perceptron</returns>
        public string TesterPerceptron(CoordDessin coord)
        {
            return _perceptronTrainTest.Entrainement(new List<CoordDessin>() { coord }, ref _lstPerceptrons);
        }

        /// <summary>
        /// Sauvegarde les perceptrons.
        /// </summary>
        public void SauvegarderPerceptrons(string cheminAcces, bool useBD = false)
        {
            if (useBD)
                _gestionPerceptron = new GestionPerceptronBD();
            else
                _gestionPerceptron = new GestionPerceptronFichiersSorties();

            _gestionPerceptron.SavePerceptrons(_lstPerceptrons, cheminAcces);
        }

    }
}
