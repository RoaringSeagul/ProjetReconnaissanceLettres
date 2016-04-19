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
using System.Collections;
using System.Text;

namespace TPARCHIPERCEPTRON.Données
{
    /// <summary>
    /// Cette classe gère l'accès aux disques pour le fichiers d'apprentissages. 
    /// Permet de charger ou décharger dans la matrice d'apprentissage.
    /// </summary>
    public class GestionPerceptronBD : IPerceptronData
    {
        private Dictionary<string, Perceptron> _lstPerceptrons;
        private ICharData _gestionnaireChar = new GestionCharBD();
        private List<CoordDessin> _lstCoord;
        PerceptronBd bd = new PerceptronBd();

        protected GestionPerceptronBD() { }

        public GestionPerceptronBD(List<CoordDessin> lstCoord)
        {
            if (_lstCoord != null)
                _lstCoord.AddRange(lstCoord);
            else
                _lstCoord = lstCoord;
        }

        /// <summary>
        /// Permet d'extraire de la base de données dans une matrice les information d'un perceptron pour l'apprentissage automatique.
        /// </summary>
        private void ChargerCoordonnees()
        {
            ImageFormat imgFrmt = new ImageFormat() { X = 16, Y = 16 };

            _lstCoord = _gestionnaireChar.GetTrainData();

            List<string> lstKnownChar = new List<string>();

            foreach (var cd in _lstCoord)
            {
                if (!lstKnownChar.Contains(cd.Reponse))
                    lstKnownChar.Add(cd.Reponse);
            }

            foreach (var c in lstKnownChar)
            {
                Perceptron p = new Perceptron(c, 0.1, imgFrmt);
                p.Entrainement(_lstCoord.Where(x => x.Reponse == c).ToList());
                if (_lstPerceptrons == null)
                {
                    _lstPerceptrons = new Dictionary<string, Perceptron>();
                    _lstPerceptrons.Add(c, p);
                }
                else
                    _lstPerceptrons.Add(c, p);
            }
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

        public Dictionary<string, Perceptron> GetPerceptrons()
        {
            return _lstPerceptrons;
        }

        private string ToBitString(BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }

        public void SavePerceptrons(Dictionary<string, Perceptron> lstPerceptrons, string cheminAcces)
        {
            _gestionnaireChar.SaveCharData(_lstCoord, "");
        }

        public Dictionary<string, Perceptron> LoadPerceptrons(string cheminAcces)
        {
            this.ChargerCoordonnees();
            return _lstPerceptrons;
        }
    }

}
