using System;
using System.Configuration;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TPARCHIPERCEPTRON.Présentation;
using TPARCHIPERCEPTRON.Métier;
using TPARCHIPERCEPTRON.Utilitaires;
using System.Threading;
using System.Collections.Specialized;
using System.Resources;

namespace TPARCHIPERCEPTRON
{
    /// <summary>
    /// Permet d'afficher l'interface pour la reconnaissance de caractères. 
    /// Cet interface fera appel au Perceptron pour identifier le caractère dessiné.
    /// </summary>
    public partial class frmAnalyseEcriture : Form
    {
        TextWriter _writer = null;
        // Le gestionnaire des perceptrons.
        private TypeEntrainement _typeEntrainement = TypeEntrainement.Manuel;
        private GestionClassesPerceptrons _gcpAnalyseEcriture = new GestionClassesPerceptrons(TypeEntrainement.Manuel);
        // La liste contenant les items de menu correspodant aux langues.
        private List<ToolStripMenuItem> _langues = new List<ToolStripMenuItem>();
        // Si un changement de langue est en cours.
        // Évite le changement récursif de la propriété Checked.
        private bool _changementLangueCourante = false;
        // La langue cochée.
        private ToolStripMenuItem _langueCourante = null;

        // Représente une instance de la fenêtre des dessins.
        // Sert à autoriser une instance de la boîte de dialogue.
        private frmAffichageDessins _instanceDessinsForm = null;

        /// <summary>
        /// Constructeur de la form. Initialise les composants
        /// </summary>
        public frmAnalyseEcriture()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr");
            InitializeComponent();
        }


        /// <summary>
        /// Entraine le bon Perceptron avec la valeur entrée dans le TextBox txtValeurEntrainee et le caractère dessiné.
        /// </summary>
        /// <param name="sender">L'objet qui à envoyé cet événement.</param>
        /// <param name="e">Les arguments de cet événement.</param>
        private void btnEntrainement_Click(object sender, EventArgs e)
        {
            rdManual.Enabled = false;
            rdUseMNIST.Enabled = false;
            rdUseBD.Enabled = false;


            ucDessin.Coordonnees.Reponse = txtValeurEntrainee.Text;
            _gcpAnalyseEcriture.Entrainement(ucDessin.Coordonnees, txtValeurEntrainee.Text);
        }

        /// <summary>
        /// Appel le perceptron pour vérifier quel neuronne identifie le mieux le caractère dessiné.
        /// </summary>
        /// <param name="sender">L'objet qui à envoyé cet événement.</param>
        /// <param name="e">Les arguments de cet événement.</param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            if (_gcpAnalyseEcriture != null)
            {
                txtConsole.Clear();
                if (!chkTestJeu.Checked)
                {
                    var resultat = _gcpAnalyseEcriture.TesterPerceptron(ucDessin.Coordonnees);
                    txtValeurTestee.Text = resultat[0];
                    for (int i = 1; i < resultat.Count; i++)
                    {
                        Console.WriteLine(resultat[i]);
                    }

                }
                else
                    Console.WriteLine(_gcpAnalyseEcriture.TesterPerceptron());

            }
        }

        /// <summary>
        /// Efface le caractère dessiné et sa matrice.
        /// </summary>
        /// <param name="sender">L'objet qui à envoyé cet événement.</param>
        /// <param name="e">Les arguments de cet événement.</param>
        private void btnEffacer_Click(object sender, EventArgs e)
        {
            ucDessin.EffacerDessin();
        }

        /// <summary>
        /// Lors de la fermeture de la form, enregistré les données des perceptrons.
        /// </summary>
        /// <param name="sender">L'objet qui à envoyé cet événement.</param>
        /// <param name="e">Les arguments de cet événement.</param>
        private void frmAnalyseEcriture_FormClosing(object sender, FormClosingEventArgs e)
        {
            _gcpAnalyseEcriture.SauvegarderCoordonnees();
        }

        /// <summary>
        /// Ajoute une langue au menu des langues.
        /// </summary>
        /// <param name="culture">La culture correspondant à la langue à ajouter.</param>
        private void AjouterLangue(CultureInfo culture)
        {
            ToolStripMenuItem itemMenu = new ToolStripMenuItem(culture.NativeName)
            {
                Tag = culture
            };
            itemMenu.Click += new EventHandler(tsmLanguesItem_Click);
            tsmiLangue.DropDownItems.Add(itemMenu);
            _langues.Add(itemMenu);
        }

        /// <summary>
        /// Appelé lors du chargement de la Form.
        /// Sert à ajouter les langues dans le menu de la langue.
        /// </summary>
        /// <param name="sender">L'objet qui à envoyé cet événement.</param>
        /// <param name="e">Les arguments de cet événement.</param>
        private void frmAnalyseEcriture_Load(object sender, EventArgs e)
        {
            AjouterLangue(CultureInfo.GetCultureInfo("fr"));
            AjouterLangue(CultureInfo.GetCultureInfo("en"));
            _writer = new TextBoxStreamWriter(txtConsole);
            Console.SetOut(_writer);
            _langues[0].Checked = true;
            var LangueCourante = (CultureInfo)_langues[0].Tag;
            ChangeLanguage(LangueCourante.Name);
        }

        /// <summary>
        /// Appelé lors de l'appui du bouton "Afficher les dessins".
        /// Affiche la fenêtre des dessins.
        /// </summary>
        /// <param name="sender">L'objet qui à envoyé cet événement.</param>
        /// <param name="e">Les arguments de cet événement.</param>
        private void tsmiAfficherDessins_Click(object sender, EventArgs e)
        {
            // Si la fenêtre n'est pas encore ouverte, la créer.
            if (_instanceDessinsForm == null) // && !rdUseMNIST.Checked)
            {
                frmAffichageDessins affichageDessin = new frmAffichageDessins();
                affichageDessin.GestionnairePerceptron = _gcpAnalyseEcriture;

                // Ajouter un événement qui met _instanceDessinsForm à null lorsque
                // cette fenêtre se ferme.
                _instanceDessinsForm = affichageDessin;
                affichageDessin.FormClosed += (s, ea) => _instanceDessinsForm = null;

                affichageDessin.Show();

                // Amener la fenêtre existante ou nouvellement crée
                // en avant-plan.
                _instanceDessinsForm.BringToFront();
            }
            //else if (rdUseMNIST.Checked)
                //MessageBox.Show(Properties.Resources.ResourceManager.GetString("MessageErreurMNIST"), Properties.Resources.ResourceManager.GetString("MessageErreurTitre"));
        }


        // La priorité fait que même si deux évènements sont appelés, le dernier a précédence alors ça retourne la bonne réponse.
        private void rdManual_CheckedChanged(object sender, EventArgs e)
        {
            if (rdManual.Checked)
            {
                _typeEntrainement = TypeEntrainement.Manuel;
                _gcpAnalyseEcriture = new GestionClassesPerceptrons(TypeEntrainement.Manuel);
                ucDessin.Resize(_gcpAnalyseEcriture.Format.X * CstApplication.HAUTEURTRAIT, _gcpAnalyseEcriture.Format.Y * CstApplication.LARGEURTRAIT);
            }
        }

        private void rdUseBD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUseBD.Checked)
            {
                _typeEntrainement = TypeEntrainement.BD;
                _gcpAnalyseEcriture = new GestionClassesPerceptrons(TypeEntrainement.BD);
                ucDessin.Resize(_gcpAnalyseEcriture.Format.X * CstApplication.HAUTEURTRAIT, _gcpAnalyseEcriture.Format.Y * CstApplication.LARGEURTRAIT);
            }
        }

        private void rdUseMNIST_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUseMNIST.Checked)
            {
                _typeEntrainement = TypeEntrainement.MNIST;
                _gcpAnalyseEcriture = new GestionClassesPerceptrons(TypeEntrainement.MNIST);
                ucDessin.Resize(_gcpAnalyseEcriture.Format.X * CstApplication.HAUTEURTRAIT, _gcpAnalyseEcriture.Format.Y * CstApplication.LARGEURTRAIT);
            }
        }

        private void baseDeDonnéesToolStripMenuItem_Click(object sender, EventArgs e)
        {
             _gcpAnalyseEcriture.SauvegarderPerceptrons("", true);
        }

        private void fichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fDialog = new FolderBrowserDialog();
            fDialog.ShowDialog();
            _gcpAnalyseEcriture.SauvegarderPerceptrons(fDialog.SelectedPath, false);
        }

        private void chargerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rdUseBD.Checked)
            {
                _gcpAnalyseEcriture.ChargerPerceptrons("");
            }
            else
            {
                OpenFileDialog fDialog = new OpenFileDialog();
                fDialog.ShowDialog();
                _gcpAnalyseEcriture.ChargerPerceptrons(fDialog.FileName);
            }
			
            rdManual.Enabled = false;
            rdUseMNIST.Enabled = false;
            rdUseBD.Enabled = false;
        }
        private void tsmLanguesItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            foreach (var i in _langues)
                i.Checked = false;

            item.Checked = true;

            var lang = (CultureInfo)item.Tag;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang.Name);
            ChangeLanguage(lang.Name);
            this.Refresh();
        }

        private void ChangeLanguage(string lang)
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmAnalyseEcriture));
            resources.ApplyResources(this, "$this", new CultureInfo(lang));

            foreach (ToolStripItem item in mnuPrincipal.Items)
            {
                if (item is ToolStripDropDownItem)
                    foreach (ToolStripItem dropDownItem in ((ToolStripDropDownItem)item).DropDownItems)
                    {
                        resources.ApplyResources(dropDownItem, dropDownItem.Name, new CultureInfo(lang));
                    }
                //Also apply resources to main toolstrip items. 
                resources.ApplyResources(item, item.Name, new CultureInfo(lang));
            }
            foreach (Control c in this.Controls)
            {
                resources.ApplyResources(c, c.Name, new CultureInfo(lang));
                if (c.ToString().StartsWith("System.Windows.Forms.GroupBox"))
                {
                    foreach (Control child in c.Controls)
                    {
                        resources.ApplyResources(child, child.Name, new CultureInfo(lang));
                    }
                }
            }
        }

        private void btnTestConf_Click(object sender, EventArgs e)
        {
            GestionFichierConfig.ShowAllAppSettingAndConnectionString();
        }
    }
}
