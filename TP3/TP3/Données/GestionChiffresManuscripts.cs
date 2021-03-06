﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using TPARCHIPERCEPTRON.Utilitaires;
using System.Configuration;
using System.Windows.Forms;

namespace TPARCHIPERCEPTRON.Données
{
    public class GestionChiffresManuscripts : ICharData
    {
        private List<CoordDessin> _lstTrainData = new List<CoordDessin>();
        private List<CoordDessin> _lstTestData = new List<CoordDessin>();

        public GestionChiffresManuscripts()
        {
            
        }

        public ImageFormat GetFormat()
        {
            return new ImageFormat() { X = 28, Y = 28 };
        }

        public void SaveCharData(List<CoordDessin> lstCoords, string cheminAcces)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["savePath"] != "" ? ConfigurationManager.AppSettings["savePath"] : "data.csv"))
                {
                    foreach (var coord in lstCoords)
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

        private List<CoordDessin> LoadCharData(string images, string label, int nbImage)
        {
            Dictionary<BitArray, byte> bitArrays = new Dictionary<BitArray, byte>();
            List<CoordDessin> lstCoords = new List<CoordDessin>();
            try
            {
                FileStream ifsImages = new FileStream(images, FileMode.Open); // test images
                FileStream ifsLabels = new FileStream(label, FileMode.Open); // test labels

                BinaryReader brLabels = new BinaryReader(ifsLabels);
                BinaryReader brImages = new BinaryReader(ifsImages);

                int magic1 = brImages.ReadInt32(); // discard
                int numImages = brImages.ReadInt32();
                int numRows = brImages.ReadInt32();
                int numCols = brImages.ReadInt32();

                int magic2 = brLabels.ReadInt32();
                int numLabels = brLabels.ReadInt32();

                byte[][] pixels = new byte[28][];
                for (int i = 0; i < pixels.Length; ++i)
                    pixels[i] = new byte[28];

                // each test image
                for (int di = 0; di < nbImage; ++di)
                {
                    for (int i = 0; i < 28; ++i)
                    {
                        for (int j = 0; j < 28; ++j)
                        {
                            byte b = brImages.ReadByte();
                            pixels[i][j] = b;
                        }
                    }

                    byte lbl = brLabels.ReadByte();

                    DigitImage dImage = new DigitImage(pixels, lbl);
                    bitArrays.Add(dImage.ToBitArray(), lbl);

                } // each image

                foreach (var bitArray in bitArrays)
                    lstCoords.Add(new CoordDessin(bitArray.Key, bitArray.Value.ToString(), 28, 28));

                ifsImages.Close();
                brImages.Close();
                ifsLabels.Close();
                brLabels.Close();

                return lstCoords;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return new List<CoordDessin>();
            }
        }

        public List<CoordDessin> GetTrainData(string s = "")
        {
            if (_lstTrainData.Count == 0)
                _lstTrainData = LoadCharData("Fichiers/train-images.idx3-ubyte", "Fichiers/train-labels.idx1-ubyte", 60000);

            return _lstTrainData;
        }

        public List<CoordDessin> GetTestData(string s ="")
        {
            if (_lstTestData.Count == 0)
                _lstTestData = LoadCharData("Fichiers/t10k-images.idx3-ubyte", "Fichiers/t10k-labels.idx1-ubyte", 10000);

            return _lstTestData;
        }
    } // Program
} // ns




