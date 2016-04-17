namespace TPARCHIPERCEPTRON
{
    partial class frmAnalyseEcriture
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnalyseEcriture));
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.grpEntrainement = new System.Windows.Forms.GroupBox();
            this.lblValeurEntraine = new System.Windows.Forms.Label();
            this.txtValeurEntrainee = new System.Windows.Forms.TextBox();
            this.btnEntrainement = new System.Windows.Forms.Button();
            this.grpDessinEntrainement = new System.Windows.Forms.GroupBox();
            this.rdManual = new System.Windows.Forms.RadioButton();
            this.rdUseMNIST = new System.Windows.Forms.RadioButton();
            this.rdUseBD = new System.Windows.Forms.RadioButton();
            this.btnEffacer = new System.Windows.Forms.Button();
            this.grpTests = new System.Windows.Forms.GroupBox();
            this.lblValeurTestee = new System.Windows.Forms.Label();
            this.txtValeurTestee = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.mnuPrincipal = new System.Windows.Forms.MenuStrip();
            this.tsmiLangue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAfficherDessins = new System.Windows.Forms.ToolStripMenuItem();
            this.sauvegarderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baseDeDonnéesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ucDessin = new TPARCHIPERCEPTRON.ucZoneDessin();
            this.chargerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSauvegarder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBaseDeDonnées = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFichier = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTestConf = new System.Windows.Forms.Button();
            this.grpEntrainement.SuspendLayout();
            this.grpDessinEntrainement.SuspendLayout();
            this.grpTests.SuspendLayout();
            this.mnuPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtConsole
            // 
            resources.ApplyResources(this.txtConsole, "txtConsole");
            this.txtConsole.Name = "txtConsole";
            // 
            // grpEntrainement
            // 
            this.grpEntrainement.Controls.Add(this.lblValeurEntraine);
            this.grpEntrainement.Controls.Add(this.txtValeurEntrainee);
            this.grpEntrainement.Controls.Add(this.btnEntrainement);
            resources.ApplyResources(this.grpEntrainement, "grpEntrainement");
            this.grpEntrainement.Name = "grpEntrainement";
            this.grpEntrainement.TabStop = false;
            // 
            // lblValeurEntraine
            // 
            resources.ApplyResources(this.lblValeurEntraine, "lblValeurEntraine");
            this.lblValeurEntraine.Name = "lblValeurEntraine";
            // 
            // txtValeurEntrainee
            // 
            resources.ApplyResources(this.txtValeurEntrainee, "txtValeurEntrainee");
            this.txtValeurEntrainee.Name = "txtValeurEntrainee";
            // 
            // btnEntrainement
            // 
            resources.ApplyResources(this.btnEntrainement, "btnEntrainement");
            this.btnEntrainement.Name = "btnEntrainement";
            this.btnEntrainement.UseVisualStyleBackColor = true;
            this.btnEntrainement.Click += new System.EventHandler(this.btnEntrainement_Click);
            // 
            // grpDessinEntrainement
            // 
            this.grpDessinEntrainement.Controls.Add(this.btnTestConf);
            this.grpDessinEntrainement.Controls.Add(this.rdManual);
            this.grpDessinEntrainement.Controls.Add(this.rdUseMNIST);
            this.grpDessinEntrainement.Controls.Add(this.rdUseBD);
            this.grpDessinEntrainement.Controls.Add(this.btnEffacer);
            this.grpDessinEntrainement.Controls.Add(this.ucDessin);
            resources.ApplyResources(this.grpDessinEntrainement, "grpDessinEntrainement");
            this.grpDessinEntrainement.Name = "grpDessinEntrainement";
            this.grpDessinEntrainement.TabStop = false;
            // 
            // rdManual
            // 
            resources.ApplyResources(this.rdManual, "rdManual");
            this.rdManual.Checked = true;
            this.rdManual.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdManual.Name = "rdManual";
            this.rdManual.TabStop = true;
            this.rdManual.UseVisualStyleBackColor = true;
            this.rdManual.CheckedChanged += new System.EventHandler(this.rdManual_CheckedChanged);
            // 
            // rdUseMNIST
            // 
            resources.ApplyResources(this.rdUseMNIST, "rdUseMNIST");
            this.rdUseMNIST.Name = "rdUseMNIST";
            this.rdUseMNIST.TabStop = true;
            this.rdUseMNIST.UseVisualStyleBackColor = true;
            this.rdUseMNIST.CheckedChanged += new System.EventHandler(this.rdUseMNIST_CheckedChanged);
            // 
            // rdUseBD
            // 
            resources.ApplyResources(this.rdUseBD, "rdUseBD");
            this.rdUseBD.Name = "rdUseBD";
            this.rdUseBD.UseVisualStyleBackColor = true;
            this.rdUseBD.CheckedChanged += new System.EventHandler(this.rdUseBD_CheckedChanged);
            // 
            // btnEffacer
            // 
            resources.ApplyResources(this.btnEffacer, "btnEffacer");
            this.btnEffacer.Name = "btnEffacer";
            this.btnEffacer.UseVisualStyleBackColor = true;
            this.btnEffacer.Click += new System.EventHandler(this.btnEffacer_Click);
            // 
            // grpTests
            // 
            this.grpTests.Controls.Add(this.lblValeurTestee);
            this.grpTests.Controls.Add(this.txtValeurTestee);
            this.grpTests.Controls.Add(this.btnTest);
            resources.ApplyResources(this.grpTests, "grpTests");
            this.grpTests.Name = "grpTests";
            this.grpTests.TabStop = false;
            // 
            // lblValeurTestee
            // 
            resources.ApplyResources(this.lblValeurTestee, "lblValeurTestee");
            this.lblValeurTestee.Name = "lblValeurTestee";
            // 
            // txtValeurTestee
            // 
            resources.ApplyResources(this.txtValeurTestee, "txtValeurTestee");
            this.txtValeurTestee.Name = "txtValeurTestee";
            this.txtValeurTestee.ReadOnly = true;
            // 
            // btnTest
            // 
            resources.ApplyResources(this.btnTest, "btnTest");
            this.btnTest.Name = "btnTest";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // mnuPrincipal
            // 
            this.mnuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLangue,
            this.tsmiAfficherDessins,
<<<<<<< HEAD
            this.sauvegarderToolStripMenuItem,
            this.chargerToolStripMenuItem});
=======
            this.tsmiSauvegarder});
>>>>>>> ba4093f9fcefec70cbaeea93233946ae1f951901
            resources.ApplyResources(this.mnuPrincipal, "mnuPrincipal");
            this.mnuPrincipal.Name = "mnuPrincipal";
            // 
            // tsmiLangue
            // 
            this.tsmiLangue.Name = "tsmiLangue";
            resources.ApplyResources(this.tsmiLangue, "tsmiLangue");
            // 
            // tsmiAfficherDessins
            // 
            this.tsmiAfficherDessins.Name = "tsmiAfficherDessins";
            resources.ApplyResources(this.tsmiAfficherDessins, "tsmiAfficherDessins");
            this.tsmiAfficherDessins.Click += new System.EventHandler(this.tsmiAfficherDessins_Click);
            // 
            // tsmiSauvegarder
            // 
            this.tsmiSauvegarder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBaseDeDonnées,
            this.tsmiFichier});
            this.tsmiSauvegarder.Name = "tsmiSauvegarder";
            resources.ApplyResources(this.tsmiSauvegarder, "tsmiSauvegarder");
            // 
            // tsmiBaseDeDonnées
            // 
            this.tsmiBaseDeDonnées.Name = "tsmiBaseDeDonnées";
            resources.ApplyResources(this.tsmiBaseDeDonnées, "tsmiBaseDeDonnées");
            this.tsmiBaseDeDonnées.Click += new System.EventHandler(this.baseDeDonnéesToolStripMenuItem_Click);
            // 
            // tsmiFichier
            // 
            this.tsmiFichier.Name = "tsmiFichier";
            resources.ApplyResources(this.tsmiFichier, "tsmiFichier");
            this.tsmiFichier.Click += new System.EventHandler(this.fichierToolStripMenuItem_Click);
            // 
            // btnTestConf
            // 
            resources.ApplyResources(this.btnTestConf, "btnTestConf");
            this.btnTestConf.Name = "btnTestConf";
            this.btnTestConf.UseVisualStyleBackColor = true;
            this.btnTestConf.Click += new System.EventHandler(this.btnTestConf_Click);
            // 
            // ucDessin
            // 
            this.ucDessin.BackColor = System.Drawing.Color.White;
            this.ucDessin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.ucDessin, "ucDessin");
            this.ucDessin.Name = "ucDessin";
            // 
            // chargerToolStripMenuItem
            // 
            this.chargerToolStripMenuItem.Name = "chargerToolStripMenuItem";
            resources.ApplyResources(this.chargerToolStripMenuItem, "chargerToolStripMenuItem");
            this.chargerToolStripMenuItem.Click += new System.EventHandler(this.chargerToolStripMenuItem_Click);
            // 
            // frmAnalyseEcriture
            // 
            this.AcceptButton = this.btnEntrainement;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpTests);
            this.Controls.Add(this.grpEntrainement);
            this.Controls.Add(this.grpDessinEntrainement);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.mnuPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mnuPrincipal;
            this.MaximizeBox = false;
            this.Name = "frmAnalyseEcriture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAnalyseEcriture_FormClosing);
            this.Load += new System.EventHandler(this.frmAnalyseEcriture_Load);
            this.grpEntrainement.ResumeLayout(false);
            this.grpEntrainement.PerformLayout();
            this.grpDessinEntrainement.ResumeLayout(false);
            this.grpDessinEntrainement.PerformLayout();
            this.grpTests.ResumeLayout(false);
            this.grpTests.PerformLayout();
            this.mnuPrincipal.ResumeLayout(false);
            this.mnuPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.GroupBox grpEntrainement;
        private System.Windows.Forms.Label lblValeurEntraine;
        private System.Windows.Forms.TextBox txtValeurEntrainee;
        private System.Windows.Forms.Button btnEntrainement;
        private System.Windows.Forms.GroupBox grpDessinEntrainement;
        private ucZoneDessin ucDessin;
        private System.Windows.Forms.GroupBox grpTests;
        private System.Windows.Forms.Label lblValeurTestee;
        private System.Windows.Forms.TextBox txtValeurTestee;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnEffacer;
        private System.Windows.Forms.MenuStrip mnuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem tsmiLangue;
        private System.Windows.Forms.ToolStripMenuItem tsmiAfficherDessins;
        private System.Windows.Forms.RadioButton rdUseMNIST;
        private System.Windows.Forms.RadioButton rdUseBD;
        private System.Windows.Forms.RadioButton rdManual;
<<<<<<< HEAD
        private System.Windows.Forms.ToolStripMenuItem sauvegarderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baseDeDonnéesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chargerToolStripMenuItem;
=======
        private System.Windows.Forms.ToolStripMenuItem tsmiSauvegarder;
        private System.Windows.Forms.ToolStripMenuItem tsmiBaseDeDonnées;
        private System.Windows.Forms.ToolStripMenuItem tsmiFichier;
        private System.Windows.Forms.Button btnTestConf;
>>>>>>> ba4093f9fcefec70cbaeea93233946ae1f951901
    }
}

