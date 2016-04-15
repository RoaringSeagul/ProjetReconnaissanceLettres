using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using TPARCHIPERCEPTRON.Utilitaires;

namespace TPARCHIPERCEPTRON.Données
{
    public class GestionChiffresManuscripts : ICharData
    {
        private List<CoordDessin> _lstCharData = new List<CoordDessin>();
        
        public GestionChiffresManuscripts()
        {
            
        }

        public List<CoordDessin> GetCharData(string cheminAcces = "")
        {
            if (_lstCharData.Count == 0)
                LoadCharData(cheminAcces);

            return _lstCharData;
            
        }

        public ImageFormat GetFormat()
        {
            return new ImageFormat() { X = 28, Y = 28 };
        }

        public void SaveCharData(List<CoordDessin> lstCoords, string cheminAcces)
        {
            throw new NotImplementedException();
        }

        private void LoadCharData(string cheminAcces)
        {
            Dictionary<BitArray, byte> bitArrays = new Dictionary<BitArray, byte>();
            try
            {
                FileStream ifsImages = new FileStream("Fichiers/t10k-images.idx3-ubyte", FileMode.Open); // test images
                FileStream ifsLabels = new FileStream("Fichiers/t10k-labels.idx1-ubyte", FileMode.Open); // test labels

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
                for (int di = 0; di < 10000; ++di)
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

                BitArrayToCoordDessin(bitArrays);

                ifsImages.Close();
                brImages.Close();
                ifsLabels.Close();
                brLabels.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private void BitArrayToCoordDessin(Dictionary<BitArray, byte> bitArrays)
        {
            foreach (var bitArray in bitArrays)
                _lstCharData.Add(new CoordDessin(bitArray.Key, bitArray.Value.ToString()));
        }

    } // Program
} // ns




