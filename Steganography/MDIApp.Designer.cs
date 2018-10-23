namespace Steganography
{
    partial class MDIApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIApp));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.passwordBasedSteganographyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eMailApplicationSMTPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eMailApplicationSMTPToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eMailApplicationIMAPToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.passwordBasedSteganographyToolStripMenuItem,
            this.eMailApplicationSMTPToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(484, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // passwordBasedSteganographyToolStripMenuItem
            // 
            this.passwordBasedSteganographyToolStripMenuItem.Name = "passwordBasedSteganographyToolStripMenuItem";
            this.passwordBasedSteganographyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.passwordBasedSteganographyToolStripMenuItem.Size = new System.Drawing.Size(186, 20);
            this.passwordBasedSteganographyToolStripMenuItem.Text = "Password Based Steganography";
            this.passwordBasedSteganographyToolStripMenuItem.Click += new System.EventHandler(this.passwordBasedSteganographyToolStripMenuItem_Click);
            // 
            // eMailApplicationSMTPToolStripMenuItem
            // 
            this.eMailApplicationSMTPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eMailApplicationSMTPToolStripMenuItem1,
            this.eMailApplicationIMAPToolStripMenuItem1});
            this.eMailApplicationSMTPToolStripMenuItem.Name = "eMailApplicationSMTPToolStripMenuItem";
            this.eMailApplicationSMTPToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.eMailApplicationSMTPToolStripMenuItem.Text = "EMail Apps";
            // 
            // eMailApplicationSMTPToolStripMenuItem1
            // 
            this.eMailApplicationSMTPToolStripMenuItem1.Name = "eMailApplicationSMTPToolStripMenuItem1";
            this.eMailApplicationSMTPToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.eMailApplicationSMTPToolStripMenuItem1.Size = new System.Drawing.Size(249, 22);
            this.eMailApplicationSMTPToolStripMenuItem1.Text = "EMail Application (SMTP)";
            this.eMailApplicationSMTPToolStripMenuItem1.Click += new System.EventHandler(this.eMailApplicationSMTPToolStripMenuItem1_Click);
            // 
            // eMailApplicationIMAPToolStripMenuItem1
            // 
            this.eMailApplicationIMAPToolStripMenuItem1.Name = "eMailApplicationIMAPToolStripMenuItem1";
            this.eMailApplicationIMAPToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.eMailApplicationIMAPToolStripMenuItem1.Size = new System.Drawing.Size(249, 22);
            this.eMailApplicationIMAPToolStripMenuItem1.Text = "EMail Application (IMAP)";
            this.eMailApplicationIMAPToolStripMenuItem1.Click += new System.EventHandler(this.eMailApplicationIMAPToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // MDIApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MDIApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Applications";
            this.Load += new System.EventHandler(this.MDIApp_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eMailApplicationSMTPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem passwordBasedSteganographyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eMailApplicationSMTPToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eMailApplicationIMAPToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    }
}