namespace HSDeckImporter
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bImportDeck = new System.Windows.Forms.Button();
            this.cboDecks = new System.Windows.Forms.ComboBox();
            this.tbGoogleAccessCode = new System.Windows.Forms.TextBox();
            this.lbAccessCode = new System.Windows.Forms.Label();
            this.bAuthorize = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bImportDeck
            // 
            this.bImportDeck.Location = new System.Drawing.Point(158, 56);
            this.bImportDeck.MaximumSize = new System.Drawing.Size(512, 512);
            this.bImportDeck.Name = "bImportDeck";
            this.bImportDeck.Size = new System.Drawing.Size(93, 25);
            this.bImportDeck.TabIndex = 0;
            this.bImportDeck.Text = "Import Deck";
            this.bImportDeck.UseVisualStyleBackColor = true;
            this.bImportDeck.Visible = false;
            this.bImportDeck.Click += new System.EventHandler(this.bImportDeck_Click);
            // 
            // cboDecks
            // 
            this.cboDecks.FormattingEnabled = true;
            this.cboDecks.Location = new System.Drawing.Point(158, 29);
            this.cboDecks.MaximumSize = new System.Drawing.Size(512, 0);
            this.cboDecks.Name = "cboDecks";
            this.cboDecks.Size = new System.Drawing.Size(303, 21);
            this.cboDecks.TabIndex = 1;
            this.cboDecks.Visible = false;
            // 
            // tbGoogleAccessCode
            // 
            this.tbGoogleAccessCode.Location = new System.Drawing.Point(33, 130);
            this.tbGoogleAccessCode.Name = "tbGoogleAccessCode";
            this.tbGoogleAccessCode.Size = new System.Drawing.Size(303, 20);
            this.tbGoogleAccessCode.TabIndex = 2;
            this.tbGoogleAccessCode.Visible = false;
            // 
            // lbAccessCode
            // 
            this.lbAccessCode.AutoSize = true;
            this.lbAccessCode.Location = new System.Drawing.Point(33, 109);
            this.lbAccessCode.Name = "lbAccessCode";
            this.lbAccessCode.Size = new System.Drawing.Size(166, 13);
            this.lbAccessCode.TabIndex = 3;
            this.lbAccessCode.Text = "Paste Google Access Code Here:";
            this.lbAccessCode.Visible = false;
            // 
            // bAuthorize
            // 
            this.bAuthorize.Location = new System.Drawing.Point(33, 161);
            this.bAuthorize.Name = "bAuthorize";
            this.bAuthorize.Size = new System.Drawing.Size(75, 23);
            this.bAuthorize.TabIndex = 4;
            this.bAuthorize.Text = "Authorize";
            this.bAuthorize.UseVisualStyleBackColor = true;
            this.bAuthorize.Visible = false;
            this.bAuthorize.Click += new System.EventHandler(this.bAuthorize_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(37, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(113, 85);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 211);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.bAuthorize);
            this.Controls.Add(this.lbAccessCode);
            this.Controls.Add(this.tbGoogleAccessCode);
            this.Controls.Add(this.cboDecks);
            this.Controls.Add(this.bImportDeck);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "HSDeckImporter";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bImportDeck;
        private System.Windows.Forms.ComboBox cboDecks;
        private System.Windows.Forms.TextBox tbGoogleAccessCode;
        private System.Windows.Forms.Label lbAccessCode;
        private System.Windows.Forms.Button bAuthorize;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

