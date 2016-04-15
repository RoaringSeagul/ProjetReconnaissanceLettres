using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Data.Entity.Migrations;
using TPARCHIPERCEPTRON.Utilitaires;
using TPARCHIPERCEPTRON.Métier;
using System.Xml;
using System.Linq;

namespace TPARCHIPERCEPTRON.Données
{
    /// <summary>
    /// Cette classe gère l'accès aux disques pour le fichiers d'apprentissages. 
    /// Permet de charger ou décharger dans la matrice d'apprentissage.
    /// </summary>
    public class GestionPerceptronBD : IPerceptronData
    {
        private List<Perceptron> _lstPerceptrons;
        private List<CoordDessin> _lstCoord;
        Entities bd = new Entities();

        /// <summary>
        /// Permet d'extraire de la base de données dans une matrice les information d'un perceptron pour l'apprentissage automatique.
        /// </summary>
        private void ChargerCoordonnees()
        {

            var coords = new Double[16, 16];
            IEnumerable<Perceptron> lstPercept = bd.Perceptrons;

            foreach (var p in lstPercept)
            {
                CoordDessin c = new CoordDessin(16, 16);
                c.Reponse = p.LettresPerceptron;
                c.CreerBitArrayString(p.BitArray);
                _lstCoord.Add(c);
            }
        }

        /// <summary>
        /// Permet de sauvegarder dans une base de données dans une matrice les informations des perceptrons pour l'apprentissage automatique
        /// </summary>
        /// <param name="fichier">Fichier où extraire les données</param>
        public int SauvegarderCoordonnees(List<CoordDessin> lstCoord)
        {
            foreach (var c in lstCoord)
            {
                bd.Perceptrons.Add(new Perceptron() { LettresPerceptron = c.Reponse, BitArray = c.BitArrayDessin.ToString() });
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

        public void SavePerceptrons(List<Perceptron> lstPerceptrons, string cheminAcces)
        {
            throw new NotImplementedException();
        }
    }

}
