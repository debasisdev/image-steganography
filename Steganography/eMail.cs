using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Limilabs.Client.IMAP;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using Limilabs.Mail.Fluent;
using Limilabs.Mail.Headers;

namespace Steganography
{
    public partial class eMail : Form
    {
        public eMail()    //Animation
        {
            InitializeComponent();
            timer1.Tick += blinkTextbox;
            timer1.Interval = 250;
            timer1.Enabled = true; 
        }
        
        private void blinkTextbox(object sender, EventArgs e)
        {
            if (label5.BackColor == Color.Red) 
                label5.BackColor = Color.White;
            else 
                label5.BackColor = Color.Red;
        } 
        
        private void eMail_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int counter=0;
            using (Imap imap = new Imap())
            {
                string uid = textBox1.Text;
                string pwd = textBox2.Text;
                string target_dir = @"D:\SteganData";

                imap.ConnectSSL("imap.gmail.com");
                imap.Login(uid,pwd);
                imap.SelectInbox();
                //List<long> uids = imap.Search(Flag.All);
                List<long> uids = imap.Search().Where
                    (Expression.GmailRawSearch("subject:Stegano"));
                List<MessageInfo> infos = imap.GetMessageInfoByUID(uids);

                DataTable dt = new DataTable();
                dt.Columns.Add("File Name");
                dt.Columns.Add("File Size");
                
                foreach (MessageInfo info in infos)
                {
                    foreach (MimeStructure attachment in info.BodyStructure.Attachments)
                    {
                        dt.Rows.Add(attachment.SafeFileName.ToString(), attachment.Size.ToString()+" bytes");
                        counter++;
                    }

                    
                    dataGridView1.DataSource = dt;

                    label5.Text = counter + " Mails Found";
                    IMail email = new MailBuilder().CreateFromEml(imap.GetMessageByUID((long)info.Envelope.UID));
                    try
                    {
                        if (!Directory.Exists(target_dir))
                        {
                            Directory.CreateDirectory(target_dir);   
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Input/Output Failed In Disk Handling");
                    } 
                    foreach (MimeData mime in email.Attachments)
                    {
                        mime.Save(@"D:\SteganData\" + mime.SafeFileName);
                    }
                }
                imap.Close();
            }
            DialogResult dr = MessageBox.Show("Files Are Downloaded", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if(dr == DialogResult.OK)
                System.Diagnostics.Process.Start(@"D:\SteganData\");
            label5.Text = counter + " Mails Downloaded";
            LogFile.WriteLogInformation("XMLog.xml", counter + " Images Are Downloaded From: ",textBox1.Text+"@gmail.com");
        }
    }
}
