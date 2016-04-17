using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPARCHIPERCEPTRON.Métier;
using TPARCHIPERCEPTRON.Utilitaires;

namespace TPARCHIPERCEPTRON.Données
{
    public interface IPerceptronData
    {
        Dictionary<string, Perceptron> GetPerceptrons();
        void SavePerceptrons(Dictionary<string, Perceptron> lstPerceptrons, string cheminAcces);
        Dictionary<string, Perceptron> LoadPerceptrons(string cheminAcces);
    }
}
