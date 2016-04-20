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
        private TypeEntrainement _typeEntrainement;

        public ImageFormat Format { get { return _format; } }

        /// <summary>
        /// Constructeur
        /// </summary>
        public GestionClassesPerceptrons(TypeEntrainement typeEntrainement)
        {
            _lstPerceptrons = new Dictionary<string, Perceptron>();
            _typeEntrainement = typeEntrainement;
            ChargerCoordonnees(typeEntrainement);
            if (GestionFichierConfig.GetSettingValue("loadPath") == "" || GestionFichierConfig.GetSettingValue("savePath") == "")
            {
                GestionFichierConfig.SetLoadPath("data.csv");
                GestionFichierConfig.SetSavePath("data.csv");
            }
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
                    _gestionSortie = new GestionCharBD();
                    break;
                default:
                    throw new NotImplementedException(); // TODO: Find something to put here
                    break;
            }
            _format = _gestionSortie.GetFormat();
            EntrainementInitial(_gestionSortie.GetTrainData());
        }

        /// <summary>
        /// Sauvegarde les échantillons d'apprentissage dans une base de données.
        /// </summary>
        /// <returns>En cas d'erreur retourne le code d'erreur</returns>
        public void SauvegarderCoordonnees()
        {
            _gestionSortie.SaveCharData(_fichierEntrainement, GestionFichierConfig.GetSettingValue("savePath"));
        }

        /// <summary>
        /// Entraine les perceptrons avec un nouveau caractère
        /// </summary>
        /// <param name="coordo">Les nouvelles coordonnées</param>
        /// <param name="reponse">La réponse associé(caractère) aux coordonnées</param>
        /// <returns>Le résultat de la console</returns>
        public void Entrainement(CoordDessin coordo, string reponse)
        {
            if (coordo.Reponse == "")
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("MessageErreurChar"), Properties.Resources.ResourceManager.GetString("MessageErreurTitre"));
                return;
            }

            if (!_lstPerceptrons.ContainsKey(coordo.Reponse))
            {
                _lstPerceptrons.Add(coordo.Reponse, new Perceptron(coordo.Reponse, 0.1, _gestionSortie.GetFormat()));
            }

            coordo.Reponse = reponse;
            _fichierEntrainement.Add(coordo);
            _perceptronTrainTest.Entrainement(_fichierEntrainement, ref _lstPerceptrons);
        }

        private void EntrainementInitial(List<CoordDessin> coords)
        {
            foreach (var coordo in coords)
            {
                if (!_lstPerceptrons.ContainsKey(coordo.Reponse) && coordo.Reponse != null)
                {
                    _lstPerceptrons.Add(coordo.Reponse, new Perceptron(coordo.Reponse, 0.1, _gestionSortie.GetFormat()));
                }
                _fichierEntrainement.Add(coordo);
            }
            _perceptronTrainTest.Entrainement(_fichierEntrainement, ref _lstPerceptrons);
        }

        /// <summary>
        /// Test le perceptron avec de nouvelles coordonnées.
        /// </summary>
        /// <param name="coord">Les nouvelles coordonnées</param>
        /// <returns>Retourne la liste des valeurs possibles du perceptron</returns>
        public List<string> TesterPerceptron(CoordDessin coord)
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
                if(p.Value.Reponse != "")
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
