﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPARCHIPERCEPTRON.Utilitaires;

namespace TPARCHIPERCEPTRON.Données
{
    interface ICharData
    {
        ImageFormat GetFormat();
        List<CoordDessin> GetTrainData(string chemin = "");
        List<CoordDessin> GetTestData(string chemin = "");
        void SaveCharData(List<CoordDessin> lstCoords, string cheminAcces);
    }
}
