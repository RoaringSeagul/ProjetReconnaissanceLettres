using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity.Migrations;
using TPARCHIPERCEPTRON.Utilitaires;

namespace TPARCHIPERCEPTRON.Données
{
    /// <summary>
    /// Cette classe gère l'accès aux disques pour le fichiers d'apprentissages. 
    /// Permet de charger ou décharger dans la matrice d'apprentissage.
    /// </summary>
    public class GestionCharFichiersSorties : ICharData
    {
        private List<CoordDessin> _lstCoord = new List<CoordDessin>();
        private ImageFormat _imageFormat = new ImageFormat() { X = 16, Y = 16 }; // 16x16 est l'image par défaut que l'on utilise.
        Entities bd = new Entities();

        public GestionCharFichiersSorties(bool utiliseBD = false)
        {
            if (utiliseBD)
            {
                ChargerCoordonnees();
            }
        }

        /// <summary>
        /// Permet d'extraire de la base de données dans une matrice les information d'un perceptron pour l'apprentissage automatique.
        /// </summary>
        private List<CoordDessin> ChargerCoordonnees()
        {
            _lstCoord = new List<CoordDessin>();

            foreach (var p in bd.Perceptrons)
            {
                CoordDessin c = new CoordDessin(16, 16, 1, 1);

                foreach (var s in p.BitArray)
                {
                    for (int y = 0; y <= 16; y++)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            if (s != '0')
                                c.AjouterCoordonnees(x, y, 1, 1);
                        }
                    }
                }

            }

            return _lstCoord;
        }

        /// <summary>
        /// Permet de sauvegarder dans une base de données dans une matrice les informations des perceptrons pour l'apprentissage automatique
        /// </summary>
        /// <param name="fichier">Fichier où extraire les données</param>
        private void SauvegarderCoordonnees(string fichier, List<CoordDessin> lstCoord)
        {
            foreach (var c in lstCoord)
            {
                bd.Perceptrons.AddOrUpdate(p => p.LettresPerceptron, new PerceptronModel() { LettresPerceptron = c.Reponse, BitArray = c.BitArrayDessin.ToString() });
            }
        }

        /// <summary>
        /// Permet d'obtenir la liste des perceptrons.
        /// </summary>
        public List<CoordDessin> GetCoordDessin()
        {
            return _lstCoord;
        }


        /// <summary>
        /// Permet de mélanger aléatoirement les échantillons d'apprentissages(coordonnées) dans le but d'améliorer l'apprentissage.
        /// </summary>
        /// <param name="lstCoord">Les coordonnées à mélanger</param>
        private void MelangerEchantillon(List<CoordDessin> lstCoord)
        {
            Random r1 = new Random();
            Random r2 = new Random();
            int index1;
            int index2;
            CoordDessin coordTemp;

            for (int i = 0; i < CstApplication.MAXITERATION; i++)
            {
                index1 = r1.Next(lstCoord.Count);
                index2 = r2.Next(lstCoord.Count);

                coordTemp = lstCoord[index1];
                lstCoord[index1] = lstCoord[index2];
                lstCoord[index2] = coordTemp;
            }
        }

        public ImageFormat GetFormat()
        {
            return _imageFormat;
        }
        


        public void SaveCharData(List<CoordDessin> lstCoords, string cheminAcces)
        {
            SauvegarderCoordonnees(cheminAcces, lstCoords);
        }

        public List<CoordDessin> GetTrainData()
        {
            return _lstCoord;
        }

        public List<CoordDessin> GetTestData()
        {
            return new List<CoordDessin>();
        }
    }

}
