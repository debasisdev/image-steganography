using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Steganography
{
    public partial class MDIApp : Form
    {
        public MDIApp()
        {
            InitializeComponent();
        }

        private void MDIApp_Load(object sender, EventArgs e)
        {

        }

        private void passwordBasedSteganographyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordStegan ob = new PasswordStegan();
            ob.MdiParent = this;
            ob.Show();
        }

        private void eMailApplicationSMTPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            eMailSMTP ob = new eMailSMTP();
            ob.MdiParent = this;
            ob.Show();
        }

        private void eMailApplicationIMAPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            eMail ob = new eMail();
            ob.MdiParent=this;
            ob.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "Help.chm");
        }
    }
}
