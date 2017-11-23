namespace GraphicManager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.axeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuColorAxe = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuTitleAxe = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuEchelleAxe = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuPoliceAxe = new System.Windows.Forms.ToolStripMenuItem();
            this.ligneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.courbeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuColorCourbe = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuAdoucirCourbe = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuColorBatonnets = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuChangerCourbeBatonnets = new System.Windows.Forms.ToolStripMenuItem();
            this.autreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuColorGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuColorForm = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuShowGrille = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuTitleGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuColorGrille = new System.Windows.Forms.ToolStripMenuItem();
            this.PN_Graphic = new GraphicManager.DoubleBufferPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.axeToolStripMenuItem,
            this.ligneToolStripMenuItem,
            this.autreToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(706, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // axeToolStripMenuItem
            // 
            this.axeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuColorAxe,
            this.MnuTitleAxe,
            this.MnuEchelleAxe,
            this.MnuPoliceAxe});
            this.axeToolStripMenuItem.Name = "axeToolStripMenuItem";
            this.axeToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.axeToolStripMenuItem.Text = "Axe";
            // 
            // MnuColorAxe
            // 
            this.MnuColorAxe.Name = "MnuColorAxe";
            this.MnuColorAxe.Size = new System.Drawing.Size(356, 22);
            this.MnuColorAxe.Text = "Couleur et épaisseur des axes";
            // 
            // MnuTitleAxe
            // 
            this.MnuTitleAxe.Name = "MnuTitleAxe";
            this.MnuTitleAxe.Size = new System.Drawing.Size(356, 22);
            this.MnuTitleAxe.Text = "Changer le titre des axes";
            // 
            // MnuEchelleAxe
            // 
            this.MnuEchelleAxe.Name = "MnuEchelleAxe";
            this.MnuEchelleAxe.Size = new System.Drawing.Size(356, 22);
            this.MnuEchelleAxe.Text = "Définir l\'échelle des axes";
            // 
            // MnuPoliceAxe
            // 
            this.MnuPoliceAxe.Name = "MnuPoliceAxe";
            this.MnuPoliceAxe.Size = new System.Drawing.Size(356, 22);
            this.MnuPoliceAxe.Text = "Changer la police et la couleur des étiquettes des axes";
            // 
            // ligneToolStripMenuItem
            // 
            this.ligneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.courbeToolStripMenuItem,
            this.MnuColorBatonnets,
            this.MnuChangerCourbeBatonnets});
            this.ligneToolStripMenuItem.Name = "ligneToolStripMenuItem";
            this.ligneToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.ligneToolStripMenuItem.Text = "Ligne";
            // 
            // courbeToolStripMenuItem
            // 
            this.courbeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuColorCourbe,
            this.MnuAdoucirCourbe});
            this.courbeToolStripMenuItem.Name = "courbeToolStripMenuItem";
            this.courbeToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.courbeToolStripMenuItem.Text = "Courbe";
            // 
            // MnuColorCourbe
            // 
            this.MnuColorCourbe.Name = "MnuColorCourbe";
            this.MnuColorCourbe.Size = new System.Drawing.Size(249, 22);
            this.MnuColorCourbe.Text = "Couleur et épaisseur de la courbe";
            // 
            // MnuAdoucirCourbe
            // 
            this.MnuAdoucirCourbe.Name = "MnuAdoucirCourbe";
            this.MnuAdoucirCourbe.Size = new System.Drawing.Size(249, 22);
            this.MnuAdoucirCourbe.Text = "Adoucir ou non la courbe";
            this.MnuAdoucirCourbe.Click += new System.EventHandler(this.MnuAdoucirCourbe_Click);
            // 
            // MnuColorBatonnets
            // 
            this.MnuColorBatonnets.Name = "MnuColorBatonnets";
            this.MnuColorBatonnets.Size = new System.Drawing.Size(326, 22);
            this.MnuColorBatonnets.Text = "Couleur des bâtonnets";
            // 
            // MnuChangerCourbeBatonnets
            // 
            this.MnuChangerCourbeBatonnets.Name = "MnuChangerCourbeBatonnets";
            this.MnuChangerCourbeBatonnets.Size = new System.Drawing.Size(326, 22);
            this.MnuChangerCourbeBatonnets.Text = "Afficher les données en courbe ou en bâtonnets";
            // 
            // autreToolStripMenuItem
            // 
            this.autreToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuColorGraph,
            this.MnuColorForm,
            this.MnuShowGrille,
            this.MnuTitleGraph,
            this.MnuColorGrille});
            this.autreToolStripMenuItem.Name = "autreToolStripMenuItem";
            this.autreToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.autreToolStripMenuItem.Text = "Autre";
            // 
            // MnuColorGraph
            // 
            this.MnuColorGraph.Name = "MnuColorGraph";
            this.MnuColorGraph.Size = new System.Drawing.Size(349, 22);
            this.MnuColorGraph.Text = "Couleur du fond du graphique";
            // 
            // MnuColorForm
            // 
            this.MnuColorForm.Name = "MnuColorForm";
            this.MnuColorForm.Size = new System.Drawing.Size(349, 22);
            this.MnuColorForm.Text = "Couleur du fond du formulaire";
            // 
            // MnuShowGrille
            // 
            this.MnuShowGrille.Name = "MnuShowGrille";
            this.MnuShowGrille.Size = new System.Drawing.Size(349, 22);
            this.MnuShowGrille.Text = "Afficher ou non la grille";
            this.MnuShowGrille.Click += new System.EventHandler(this.MnuShowGrille_Click);
            // 
            // MnuTitleGraph
            // 
            this.MnuTitleGraph.Name = "MnuTitleGraph";
            this.MnuTitleGraph.Size = new System.Drawing.Size(349, 22);
            this.MnuTitleGraph.Text = "Changer le titre du graphique (texte, police, couleur)";
            // 
            // MnuColorGrille
            // 
            this.MnuColorGrille.Name = "MnuColorGrille";
            this.MnuColorGrille.Size = new System.Drawing.Size(349, 22);
            this.MnuColorGrille.Text = "Couleur et épaisseur de la grille";
            // 
            // PN_Graphic
            // 
            this.PN_Graphic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PN_Graphic.Location = new System.Drawing.Point(8, 60);
            this.PN_Graphic.Name = "PN_Graphic";
            this.PN_Graphic.Size = new System.Drawing.Size(686, 481);
            this.PN_Graphic.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 551);
            this.Controls.Add(this.PN_Graphic);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem axeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuColorAxe;
        private System.Windows.Forms.ToolStripMenuItem MnuTitleAxe;
        private System.Windows.Forms.ToolStripMenuItem MnuEchelleAxe;
        private System.Windows.Forms.ToolStripMenuItem ligneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuPoliceAxe;
        private System.Windows.Forms.ToolStripMenuItem courbeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuColorBatonnets;
        private System.Windows.Forms.ToolStripMenuItem MnuColorCourbe;
        private System.Windows.Forms.ToolStripMenuItem MnuAdoucirCourbe;
        private System.Windows.Forms.ToolStripMenuItem MnuChangerCourbeBatonnets;
        private System.Windows.Forms.ToolStripMenuItem autreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuColorGraph;
        private System.Windows.Forms.ToolStripMenuItem MnuColorForm;
        private System.Windows.Forms.ToolStripMenuItem MnuShowGrille;
        private System.Windows.Forms.ToolStripMenuItem MnuTitleGraph;
        private System.Windows.Forms.ToolStripMenuItem MnuColorGrille;
        private DoubleBufferPanel PN_Graphic;
    }
}

