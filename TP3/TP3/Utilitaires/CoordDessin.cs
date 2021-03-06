﻿using System.Collections;
using System.Drawing;
using System.IO;
    
namespace TPARCHIPERCEPTRON.Utilitaires
{
    /// <summary>
    /// Gère la structure de données qui contient le caractères et les coordonnées des points du dessin de ce caractère
    /// </summary>
    public class CoordDessin
    {
        private BitArray _baDessin = null;
        private string _reponse;
        private int _id = 0;
        private int _largeur = 0;
        private int _hauteur = 0;

        public BitArray BitArrayDessin
        {
            get { return _baDessin; }
        }
        public string Reponse
        {
            get { return _reponse; }
            set { _reponse = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Largeur
        {
            get { return _largeur; }
        }

        public int Hauteur
        {
            get { return _hauteur; }
        }

        /// <summary>
        /// Constructeur, crée un vecteur de bit pour représenter le dessin. Le blanc = 1 et le noir = -1.
        /// </summary>
        /// <param name="largeur"></param>
        /// <param name="hauteur"></param>
        public CoordDessin(int largeur, int hauteur, int largeurTrait, int hauteurTrait)
        {
            _largeur = largeur;
            _hauteur = hauteur;
            _baDessin = new BitArray((largeur / largeurTrait) * (hauteur / hauteurTrait));
        }

        public CoordDessin(BitArray bits, string reponse)
        {
            _baDessin = bits;
            _reponse = reponse;
        }

        public CoordDessin(BitArray bits, string reponse, int largeur, int hauteur)
        {
            _baDessin = bits;
            _reponse = reponse;
            _largeur = largeur;
            _hauteur = hauteur;
        }

        public CoordDessin(int Id, BitArray bits, string reponse, int largeur, int hauteur)
        {
            _id = Id;
            _baDessin = bits;
            _reponse = reponse;
            _largeur = largeur;
            _hauteur = hauteur;
        }

        /// <summary>
        /// Remplie le BitArray de l'objet CoordDessin à partir d'une string
        /// </summary>
        /// <param name="bitArray"></param>
        public void CreerBitArrayString(string bitArray)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                switch (bitArray[i])
                {
                    case '1':
                        BitArrayDessin[i] = true;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Lors de l'ajout d'un point, modifier le vecteur de bits où il y a de nouveaux points noirs.
        /// </summary>
        /// <param name="x">La position x du nouveau point.</param>
        /// <param name="y">La position y du nouveau point.</param>
        /// <param name="tailleX">La taille en x du point</param>
        /// <param name="tailleY">La taille en y du point</param>
        public void AjouterCoordonnees(int x, int y, int tailleX, int tailleY)
        {
            if (x < 0)
                x = 0;

            if (y < 0)
                y = 0;

            if (x + tailleX >= CstApplication.TAILLEDESSINX)
                x = CstApplication.TAILLEDESSINX - tailleX;

            if (y + tailleY >= CstApplication.TAILLEDESSINY)
                y = CstApplication.TAILLEDESSINY - tailleY;

            int j = ((x / tailleX) * (CstApplication.TAILLEDESSINX / tailleX) + (y / tailleY));
            _baDessin[j] = true;
        }
    }
}
