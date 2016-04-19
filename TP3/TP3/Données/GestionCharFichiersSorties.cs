using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity.Migrations;
using TPARCHIPERCEPTRON.Utilitaires;
using System.Collections;
using System.Windows.Forms;
using TPARCHIPERCEPTRON.Métier;

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
		
        /// <summary>
        /// Permet d'extraire de la base de données dans une matrice les information d'un perceptron pour l'apprentissage automatique.
        /// </summary>
        private List<CoordDessin> ChargerCoordonnees(string chemin)
        {
            _lstCoord = new List<CoordDessin>();
            try {
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["loadPath"] != "" ? ConfigurationManager.AppSettings["loadPath"] : "data.csv"))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        ImageFormat format = new ImageFormat();
                        BitArray bitArray;
                        char c;

                        var values = line.Split(',');
                        c = values[0][0];
                        format.X = Int32.Parse(values[1]);
                        format.Y = Int32.Parse(values[2]);

                        bitArray = new BitArray(format.X * format.Y);

                        for (int i = 0; i < format.X; i++)
                        {
                            line = sr.ReadLine();
                            var ligneValeur = line.Split(',');
                            for (int j = 0; j < format.Y; j++)
                            {
                                bitArray[i + j * format.X] = ligneValeur[j] == "1" ? true : false;
                            }
                        }

                        _lstCoord.Add(new CoordDessin(bitArray, c.ToString(), format.X, format.Y));
                    }
                }
                return _lstCoord;
            }
            catch
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("MessageErreurManuel"), Properties.Resources.ResourceManager.GetString("MessageErreurTitre"));

                return _lstCoord;
            }
}

        /// <summary>
        /// Permet de sauvegarder dans une base de données dans une matrice les informations des perceptrons pour l'apprentissage automatique
        /// </summary>
        /// <param name="fichier">Fichier où extraire les données</param>
        private void SauvegarderCoordonnees(string fichier, List<CoordDessin> lstCoord)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["savePath"] != "" ? ConfigurationManager.AppSettings["savePath"] : "data.csv"))
                {
                    foreach (var coord in lstCoord)
                    {
                        sw.WriteLine(coord.Reponse + "," + (coord.Hauteur / 4) + "," + (coord.Largeur / 4));
                        for (uint i = 0; i < (coord.Hauteur / 4); i++)
                        {
                            for (uint j = 0; j < (coord.Largeur / 4); j++)
                            {
                                sw.Write(coord.BitArrayDessin[(int)(i + (coord.Largeur / 4) * j)] ? "1" + "," : "0" + ',');
                            }
                            sw.WriteLine();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("MessageErreurManuel"), Properties.Resources.ResourceManager.GetString("MessageErreurTitre"));
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

        public List<CoordDessin> GetTrainData(string chemin = "")
        {
            return ChargerCoordonnees(chemin);
        }

        public List<CoordDessin> GetTestData(string chemin = "")
        {
            return new List<CoordDessin>();
        }
    }

}
