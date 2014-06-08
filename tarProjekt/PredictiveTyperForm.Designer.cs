namespace TarProjekt
{
    partial class PredictiveTyperForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PredictiveTyperForm));
            this.fileText = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smootingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crossentropyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCorpusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoCompleteList = new System.Windows.Forms.ListBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabelLine = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelColumn = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLastWord = new System.Windows.Forms.ToolStripStatusLabel();
            this.brojRjeci = new System.Windows.Forms.ToolStripStatusLabel();
            this.averageSentenceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.numberOfSentencesLab = new System.Windows.Forms.ToolStripStatusLabel();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.loadCorpusDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.listAddOne = new System.Windows.Forms.ListBox();
            this.loadTestSetDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileText
            // 
            this.fileText.AcceptsTab = true;
            this.fileText.AutoWordSelection = true;
            this.fileText.CausesValidation = false;
            this.fileText.HideSelection = false;
            this.fileText.Location = new System.Drawing.Point(0, 27);
            this.fileText.Name = "fileText";
            this.fileText.Size = new System.Drawing.Size(658, 333);
            this.fileText.TabIndex = 4;
            this.fileText.Text = "";
            this.fileText.TextChanged += new System.EventHandler(this.fileText_TextChanged);
            this.fileText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fileText_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.formatToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(658, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.optionsToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // formatToolStripMenuItem
            // 
            this.formatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontToolStripMenuItem});
            this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
            this.formatToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.formatToolStripMenuItem.Text = "Format";
            // 
            // fontToolStripMenuItem
            // 
            this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            this.fontToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.fontToolStripMenuItem.Text = "Font";
            this.fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcutsToolStripMenuItem,
            this.languageModelToolStripMenuItem,
            this.smootingToolStripMenuItem,
            this.analysisToolStripMenuItem,
            this.loadCorpusToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // shortcutsToolStripMenuItem
            // 
            this.shortcutsToolStripMenuItem.Name = "shortcutsToolStripMenuItem";
            this.shortcutsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.shortcutsToolStripMenuItem.Text = "Shortcuts";
            // 
            // languageModelToolStripMenuItem
            // 
            this.languageModelToolStripMenuItem.Name = "languageModelToolStripMenuItem";
            this.languageModelToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.languageModelToolStripMenuItem.Text = "Language model";
            this.languageModelToolStripMenuItem.Click += new System.EventHandler(this.languageModelToolStripMenuItem_Click);
            // 
            // smootingToolStripMenuItem
            // 
            this.smootingToolStripMenuItem.Name = "smootingToolStripMenuItem";
            this.smootingToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.smootingToolStripMenuItem.Text = "Smooting";
            // 
            // analysisToolStripMenuItem
            // 
            this.analysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crossentropyToolStripMenuItem});
            this.analysisToolStripMenuItem.Name = "analysisToolStripMenuItem";
            this.analysisToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.analysisToolStripMenuItem.Text = "Analysis";
            // 
            // crossentropyToolStripMenuItem
            // 
            this.crossentropyToolStripMenuItem.Name = "crossentropyToolStripMenuItem";
            this.crossentropyToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.crossentropyToolStripMenuItem.Text = "Cross-entropy";
            this.crossentropyToolStripMenuItem.Click += new System.EventHandler(this.crossentropyToolStripMenuItem_Click);
            // 
            // loadCorpusToolStripMenuItem
            // 
            this.loadCorpusToolStripMenuItem.Name = "loadCorpusToolStripMenuItem";
            this.loadCorpusToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.loadCorpusToolStripMenuItem.Text = "Load corpus";
            this.loadCorpusToolStripMenuItem.Click += new System.EventHandler(this.loadCorpusToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // autoCompleteList
            // 
            this.autoCompleteList.FormattingEnabled = true;
            this.autoCompleteList.Items.AddRange(new object[] {
            "Predictive Typer"});
            this.autoCompleteList.Location = new System.Drawing.Point(67, 132);
            this.autoCompleteList.Name = "autoCompleteList";
            this.autoCompleteList.Size = new System.Drawing.Size(140, 108);
            this.autoCompleteList.TabIndex = 6;
            this.autoCompleteList.Visible = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelLine,
            this.statusLabelColumn,
            this.statusLastWord,
            this.brojRjeci,
            this.averageSentenceLabel,
            this.numberOfSentencesLab});
            this.statusStrip.Location = new System.Drawing.Point(0, 363);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(658, 22);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "Line: 0";
            // 
            // statusLabelLine
            // 
            this.statusLabelLine.Name = "statusLabelLine";
            this.statusLabelLine.Size = new System.Drawing.Size(44, 17);
            this.statusLabelLine.Text = "Line: 0;";
            // 
            // statusLabelColumn
            // 
            this.statusLabelColumn.Name = "statusLabelColumn";
            this.statusLabelColumn.Size = new System.Drawing.Size(65, 17);
            this.statusLabelColumn.Text = "Column: 0;";
            // 
            // statusLastWord
            // 
            this.statusLastWord.Name = "statusLastWord";
            this.statusLastWord.Size = new System.Drawing.Size(0, 17);
            // 
            // brojRjeci
            // 
            this.brojRjeci.Name = "brojRjeci";
            this.brojRjeci.Size = new System.Drawing.Size(118, 17);
            this.brojRjeci.Text = "toolStripStatusLabel1";
            // 
            // averageSentenceLabel
            // 
            this.averageSentenceLabel.Name = "averageSentenceLabel";
            this.averageSentenceLabel.Size = new System.Drawing.Size(149, 17);
            this.averageSentenceLabel.Text = "Average sentence length: 0";
            // 
            // numberOfSentencesLab
            // 
            this.numberOfSentencesLab.Name = "numberOfSentencesLab";
            this.numberOfSentencesLab.Size = new System.Drawing.Size(132, 17);
            this.numberOfSentencesLab.Text = "Number of sentences: 0";
            // 
            // loadCorpusDialog
            // 
            this.loadCorpusDialog.FileName = "openFileDialog1";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // listAddOne
            // 
            this.listAddOne.FormattingEnabled = true;
            this.listAddOne.Location = new System.Drawing.Point(389, 192);
            this.listAddOne.Name = "listAddOne";
            this.listAddOne.Size = new System.Drawing.Size(189, 95);
            this.listAddOne.TabIndex = 9;
            // 
            // loadTestSetDialog
            // 
            this.loadTestSetDialog.FileName = "openFileDialog1";
            // 
            // PredictiveTyperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 385);
            this.Controls.Add(this.listAddOne);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.autoCompleteList);
            this.Controls.Add(this.fileText);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PredictiveTyperForm";
            this.Text = "Predictive Typer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox fileText;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ListBox autoCompleteList;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shortcutsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smootingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelLine;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelColumn;
        private System.Windows.Forms.ToolStripStatusLabel statusLastWord;
        private System.Windows.Forms.ToolStripMenuItem loadCorpusToolStripMenuItem;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.OpenFileDialog loadCorpusDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripStatusLabel brojRjeci;
        private System.Windows.Forms.ListBox listAddOne;
        private System.Windows.Forms.ToolStripStatusLabel averageSentenceLabel;
        private System.Windows.Forms.ToolStripStatusLabel numberOfSentencesLab;
        private System.Windows.Forms.ToolStripMenuItem crossentropyToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog loadTestSetDialog;
    }
}