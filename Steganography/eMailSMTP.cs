using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Mail;
using System.Net.Mime;
using System.Xml;

namespace Steganography
{   
    public partial class eMailSMTP : Form
    {
        MailMessage mail = new MailMessage();
        public eMailSMTP()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button2.Enabled = false;
            using (MailMessage mailMessage = new MailMessage()) 
            {
                try
                {
                    string domain = "";
                    SmtpClient SmtpServer = new SmtpClient();
                    if (radioButton1.Checked == true)
                    {
                        SmtpServer.Host = "smtp.gmail.com";
                        domain = "@gmail.com";
                        SmtpServer.EnableSsl = true;
                    }
                    if (radioButton2.Checked == true)
                    {
                        SmtpServer.Host = "smtp.mail.yahoo.com";
                        domain = "@yahoo.com";
                        SmtpServer.EnableSsl = false;
                    }
                    if (radioButton3.Checked == true)
                    {
                        SmtpServer.Host = "smtp.live.com";
                        domain = "@hotmail.com";
                        SmtpServer.EnableSsl = true;
                    }
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new NetworkCredential(textBox1.Text + domain, textBox2.Text);

                    mail = new MailMessage();
                    mail.From = new MailAddress(textBox1.Text + domain);
                    mail.Subject = textBox4.Text;
                    mail.Body = richTextBox1.Text;
                    mail.To.Add(textBox3.Text + "@gmail.com");
                    mail.IsBodyHtml = true;
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    if (textBox5.Text != "")
                    {
                        Attachment att = new Attachment(textBox5.Text);
                        mail.Attachments.Add(att);
                    }
                    SmtpServer.Send(mail);
                    MessageBox.Show("Message Sent!!!", "EMail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Text = textBox2.Text = textBox3.Text = textBox5.Text = richTextBox1.Text = "";
                    LogFile.WriteLogInformation("XMLog.xml", "E-Mail Sent From:" + mail.From.ToString(), " To:" + mail.To.ToString());
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error Occurred!!! Protocol Level :)", "EMail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                finally
                {
                    button3.Enabled = true;
                    button2.Enabled = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = openFileDialog1.FileName;
            }
        }
    }
}
