using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPARCHIPERCEPTRON.Métier
{
    /// <summary>
    /// Auteur: Jonathan Koch-Roy
    /// Date: 04.17.2016
    /// Description: Classe qui gère le fichier de configuration de l'application courante
    /// </summary>
    class GestionFichierConfig
    {
        /// <summary>
        /// Ajoute une nouvelle clé de configuration si elle n'éxiste pas déjà dans le fichier sinon elle change la valeur de la clé
        /// </summary>
        /// <param name="key">La clé à ajouté ou mettre à jour</param>
        /// <param name="value">Le valeur désiré de la clé choisie</param>
        public static void AddOrUpdate(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Full);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Erreur lors de l'écriture dans le fichier de configuration");
            }
        }

        /// <summary>
        /// Montre tous les settings dans le fichier de configuration courant
        /// </summary>
        public static void ShowAllAppSettingAndConnectionString()
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
                var settings = configFile.AppSettings.Settings;
                foreach (var i in settings.AllKeys)
                {
                    Console.WriteLine(i + " : " + settings[i].Value);
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Erreur lors de l'écriture dans le fichier de configuration");
            }
        }

        /// <summary>
        /// Set la valeur du connectionString courant et ouvre un port SqlConnection
        /// </summary>
        /// <param name="connectKey">Clé de la connection se trouvant dans le fichier de configuration</param>
        public static void SetConnectionString(string connectKey)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionString = ConfigurationManager.ConnectionStrings[connectKey].ConnectionString;
            if (connectionString != null)
            {
                SqlConnection connect = new SqlConnection();
                connect.ConnectionString = connectionString;
                connect.Open();
            }
        }

        /// <summary>
        /// Change le chemin d'accès pour la sauvegarde.
        /// </summary>
        /// <param name="savePath"></param>
        public static void SetSavePath(string savePath)
        {
            AddOrUpdate("savePath", savePath);
        }

        public static void SetLoadPath(string loadPath)
        {
            AddOrUpdate("loadPath", loadPath);
        }

        /// <summary>
        /// Renvois la valeur de la clé dans le fichier de configuration
        /// </summary>
        /// <param name="Key">Clé à chercher</param>
        /// <returns></returns>
        public static string GetSettingValue(string Key)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            var settings = configFile.AppSettings.Settings;
            return settings[Key].Value;
        }
    }
}
