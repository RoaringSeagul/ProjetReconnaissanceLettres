using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace TPARCHIPERCEPTRON.Données
{
    /// <summary>
    /// Cette classe gère l'accès aux disques pour le fichiers d'apprentissages. 
    /// Permet de charger ou décharger dans la matrice d'apprentissage.
    /// </summary>
    public class GestionCharBD : ICharData
    {
        private List<CoordDessin> _lstCoord = new List<CoordDessin>();
        private ImageFormat _imageFormat = new ImageFormat() { X = 16, Y = 16 }; // 16x16 est l'image par défaut que l'on utilise.
        PerceptronBd bd = new PerceptronBd();

        /// <summary>
        /// Permet d'extraire de la base de données dans une matrice les information d'un perceptron pour l'apprentissage automatique.
        /// </summary>
        private void ChargerCoordonnees()
        {
            try
            {
                foreach (var p in bd.DessinModels)
                {
                    _lstCoord.Add(new CoordDessin(p.DessinID, ToStringToBit(p.BitArray), p.Lettres, p.Largeur, p.Hauteur));
                }
            }
            catch
            {
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("MessageErreurBD"), Properties.Resources.ResourceManager.GetString("MessageErreurTitre"));
            }
        }

        /// <summary>
        /// Permet de sauvegarder dans une base de données dans une matrice les informations des perceptrons pour l'apprentissage automatique
        /// </summary>
        /// <param name="fichier">Fichier où extraire les données</param>
        private void SauvegarderCoordonnees(string fichier, List<CoordDessin> lstCoord)
        {
            foreach (var c in lstCoord)
            {
                if (c.Id == 0)
                    bd.DessinModels.Add(new DessinModel()
                    {
                        Lettres = c.Reponse,
                        BitArray = ToBitString(c.BitArrayDessin),
                        Hauteur = c.Hauteur,
                        Largeur = c.Largeur
                    });
            }

            bd.SaveChanges();
        }

        private BitArray ToStringToBit(string bitArray)
        {
            BitArray bt = new BitArray(bitArray.Length);

            for (int i = 0; i < bt.Count; i++)
            {
                bt[i] = (bitArray[i] != '0') ? true : false;
            }

            return bt;
        }

        private string ToBitString(BitArray bits)
        {
            string sb = "";

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb += c;
            }

            return sb.ToString();
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

        public List<CoordDessin> GetTrainData(string s = "")
        {
            this.ChargerCoordonnees();
            return _lstCoord;
        }

        public List<CoordDessin> GetTestData(string s = "")
        {
            return new List<CoordDessin>();
        }
    }

}
