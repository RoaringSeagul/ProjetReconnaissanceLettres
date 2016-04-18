using System.Collections.Generic;
using TPARCHIPERCEPTRON.Données;
using TPARCHIPERCEPTRON.Utilitaires;
using System;
using System.Threading;
using System.Windows.Forms;

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
        private List<CoordDessin> _fichierEntrainement = new List<CoordDessin>();
        private ImageFormat _format;
        private IPerceptronData _gestionPerceptron;
        private ICharData _gestionSortie;

        public ImageFormat Format { get { return _format; } }

        /// <summary>
        /// Constructeur
        /// </summary>
        public GestionClassesPerceptrons(TypeEntrainement typeEntrainement)
        {
            _lstPerceptrons = new Dictionary<string, Perceptron>();
            _typeEntrainement = typeEntrainement;
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
                    _fichierEntrainement = _gestionSortie.GetTrainData();
                    break;
                case TypeEntrainement.MNIST:
                    _gestionSortie = new GestionChiffresManuscripts();
                    break;
                case TypeEntrainement.BD:
                    _fichierEntrainement = _gestionSortie.GetTrainData();
                    _gestionSortie = new GestionCharBD();
                    break;
                default:
                    throw new NotImplementedException(); // TODO: Find something to put here
                    break;
            }
            _format = _gestionSortie.GetFormat();
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
        public void Entrainement(CoordDessin coordo, string reponse)
        {
            _fichierEntrainement.Add(coordo);

            if (_lstPerceptrons.Count == 0 && _typeEntrainement != TypeEntrainement.Manuel)
                Entrainement();

            if (!_lstPerceptrons.ContainsKey(reponse))
            {
                _lstPerceptrons.Add(reponse, new Perceptron(reponse, 0.1, _gestionSortie.GetFormat()));
            }

            coordo.Reponse = reponse;
            _fichierEntrainement.Add(coordo);
            _perceptronTrainTest.Entrainement(new List<CoordDessin>() { coordo }, ref _lstPerceptrons);
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
            return _perceptronTrainTest.Test(coord, ref _lstPerceptrons);
        }

        /// <summary>
        /// Test le perceptron avec un jeu de test.
        /// </summary>
        /// <returns></returns>
        public string TesterPerceptron()
        {
            return _perceptronTrainTest.Test(_gestionSortie.GetTestData(), _lstPerceptrons);
        }

        /// <summary>
        /// Sauvegarde les perceptrons.
        /// </summary>
        public void SauvegarderPerceptrons(string cheminAcces, bool useBD = false)
        {
            if (useBD)
                _gestionPerceptron = new GestionPerceptronBD(_fichierEntrainement);
            else
                _gestionPerceptron = new GestionPerceptronFichiersSorties();

            _gestionPerceptron.SavePerceptrons(_lstPerceptrons, cheminAcces);
        }

        public void ChargerPerceptrons(string cheminAcces)
        {
            switch (_typeEntrainement)
            {
                case TypeEntrainement.Manuel:
                    _gestionPerceptron = new GestionPerceptronFichiersSorties();
                    _lstPerceptrons = _gestionPerceptron.LoadPerceptrons(cheminAcces);
                    break;
                case TypeEntrainement.MNIST:
                    _gestionPerceptron = new GestionPerceptronFichiersSorties();
                    _lstPerceptrons = _gestionPerceptron.LoadPerceptrons(cheminAcces);
                    break;
                case TypeEntrainement.BD:
                    _gestionPerceptron = new GestionPerceptronBD(_fichierEntrainement);
                    _gestionPerceptron.LoadPerceptrons("");
                    break;
                default:
                    break;
            }
        }

        public List<Perceptron> ObtenirPerceptron()
        {
            List<Perceptron> lstPerceptron = new List<Perceptron>();

            foreach (var p in _lstPerceptrons)
            {
                lstPerceptron.Add(p.Value);
            }

            return lstPerceptron;
        }

        public List<CoordDessin> ObtenirCoordDessin()
        {
            return _fichierEntrainement;
        }
    }
}
