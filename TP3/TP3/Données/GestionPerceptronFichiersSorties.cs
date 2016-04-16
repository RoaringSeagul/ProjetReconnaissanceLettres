using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity.Migrations;
using TPARCHIPERCEPTRON.Utilitaires;
using TPARCHIPERCEPTRON.Métier;

namespace TPARCHIPERCEPTRON.Données
{
    /// <summary>
    /// Cette classe gère l'accès aux disques pour le fichiers d'apprentissages. 
    /// Permet de charger ou décharger dans la matrice d'apprentissage.
    /// </summary>
    public class GestionPerceptronFichiersSorties : IPerceptronData
    {
        private List<Perceptron> _lstPerceptrons = new List<Perceptron>();

        /// <summary>
        /// Permet d'extraire de la base de données dans une matrice les information d'un perceptron pour l'apprentissage automatique.
        /// </summary>
        private void ChargerCoordonnees()
        {
            var coords = new Double[16, 16];

            //foreach (var p in bd.Perceptrons)
            //{
            //    foreach (var s in p.BitArray)
            //    {
            //        for (int y = 0; y <= 16; y++)
            //        {
            //            for (int x = 0; x < 16; x++)
            //            {
            //                // TODO: Implémenter
            //            }
            //        }
            //    }

            //}
        }

        /// <summary>
        /// Permet de sauvegarder dans une base de données dans une matrice les informations des perceptrons pour l'apprentissage automatique
        /// </summary>
        /// <param name="fichier">Fichier où extraire les données</param>
        public int SauvegarderCoordonnees(string fichier, List<CoordDessin> lstCoord)
        {
            foreach (var c in lstCoord)
            {
                //bd.Perceptrons.AddOrUpdate(p => p.LettresPerceptron, new SavedPerceptron() { LettresPerceptron = c.Reponse, BitArray = c.BitArrayDessin.ToString() });
            }

            return CstApplication.OK;
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

        public List<Perceptron> GetPerceptrons()
        {
            return _lstPerceptrons;
        }

        public void SavePerceptrons(Dictionary<string, Perceptron> lstPerceptrons, string cheminAcces)
        {
            using (StreamWriter sw = new StreamWriter(cheminAcces + "\\Perceptrons.csv"))
            {
                foreach (var perceptron in lstPerceptrons)
                {
                    sw.WriteLine(perceptron.Key);
                    for (uint i = 0; i < perceptron.Value.Format.X; i++)
                    {
                        for (uint j = 0;  j < perceptron.Value.Format.Y;  j++)
                        {
                            sw.Write(perceptron.Value.GetWeightAt(i, j).ToString() + ',');
                        }
                        sw.WriteLine();
                    }
                }
            }
        }
    }

}
