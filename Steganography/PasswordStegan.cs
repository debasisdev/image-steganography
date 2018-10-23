using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace Steganography
{
    public partial class PasswordStegan : Form
    {
        private Bitmap OriginalImage = null, EncodedImage = null, MarkedImage = null;
        
        public PasswordStegan()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    OriginalImage = new Bitmap(openFileDialog1.FileName);
                    MarkedImage = null;
                    EncodedImage = null;
                    radOriginal.Checked = true;
                    pictureBox1.Image = OriginalImage;
                    savePictureToolStripMenuItem.Enabled = false;
                    button1.Enabled = true;
                    button2.Enabled = true;

                    // Size to show the whole picture.
                    int wid = Math.Max(this.ClientSize.Width,
                        pictureBox1.Bounds.Right + pictureBox1.Left);
                    int hgt = Math.Max(this.ClientSize.Height,
                        pictureBox1.Bounds.Bottom + pictureBox1.Left);
                    this.ClientSize = new Size(wid, hgt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        // Encode a message into an image.
        private void EncodeMessageInImage(Bitmap bm, Bitmap visible_bm, string password, string message)
        {
            // Initialize a random number generator.
            Random rand = new Random(NumericPassword(password));

            // Create a new HashSet.
            HashSet<string> used_positions = new HashSet<string>();

            // Encode the message length.
            byte[] bytes = BitConverter.GetBytes(message.Length);
            for (int i=0; i < bytes.Length; i++)
            {
                EncodeByte(bm, visible_bm, rand, bytes[i], used_positions);
            }

            // Encode the message.
            char[] chars = message.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                EncodeByte(bm, visible_bm, rand, (byte)chars[i], used_positions);
            }
        }

        // Encode a byte in the picture.
        private void EncodeByte(Bitmap bm, Bitmap visible_bm, Random rand,
            byte value, HashSet<string> used_positions)
        {
            for (int i = 0; i < 8; i++)
            {
                // Pick a position for the ith bit.
                int row, col, pix;
                PickPosition(bm, rand, used_positions, out row, out col, out pix);

                // Get the color's pixel components.
                Color clr = bm.GetPixel(row, col);
                byte r = clr.R;
                byte g = clr.G;
                byte b = clr.B;

                // Get the next bit to store.
                int bit = 0;
                if ((value & 1) == 1) bit = 1;
                
                // Update the color.
                switch (pix)
                {
                    case 0:
                        r = (byte)((r & 0xFE) | bit);
                        break;
                    case 1:
                        g = (byte)((g & 0xFE) | bit);
                        break;
                    case 2:
                        b = (byte)((b & 0xFE) | bit);
                        break;
                }
                clr = Color.FromArgb(clr.A, r, g, b);
                bm.SetPixel(row, col, clr);

                // Display a red pixel.
                visible_bm.SetPixel(row, col, Color.Red);

                // Move to the next bit in the value.
                value >>= 1;
            }
        }

        // Pick an unused (r, c, pixel) combination.
        private void PickPosition(Bitmap bm, Random rand,
            HashSet<string> used_positions,
            out int row, out int col, out int pix)
        {
            for ( ; ; )
            {
                // Pick random r, c, and pix.
                row = rand.Next(0, bm.Width);
                col = rand.Next(0, bm.Height);
                pix = rand.Next(0, 3);

                // See if this location is available.
                string key = 
                    row.ToString() + "/" +
                    col.ToString() + "/" +
                    pix.ToString();
                if (!used_positions.Contains(key)) 
                {
                    used_positions.Add(key);
                    return;
                }
            }
        }

        // Convert a string password into a numeric value.
        private int NumericPassword(string password)
        {
            // Initialize the shift values to different non-zero values.
            int shift1 = 3;
            int shift2 = 17;

            // Process the message.
            char[] chars = password.ToCharArray();
            int value = 0;
            for (int i = 1; i < password.Length; i++)
            {
                // Add the next letter.
                int ch_value = (int)chars[i];
                value ^= (ch_value << shift1);
                value ^= (ch_value << shift2);

                // Change the shifts.
                shift1 = (shift1 + 7) % 19;
                shift2 = (shift2 + 13) % 23;
            }
            return value;            
        }

        // Display the appropriate image.
        private void radOriginal_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = OriginalImage;
        }
        private void radEncoded_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = EncodedImage;
        }
        private void radMarked_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = MarkedImage;
        }

        // Decode the message hidden in a picture.
        private string DecodeMessageInImage(Bitmap bm, string password)
        {
            // Initialize a random number generator.
            Random rand = new Random(NumericPassword(password));

            // Create a new HashSet.
            HashSet<string> used_positions = new HashSet<string>();

            // Make a byte array big enough to hold the message length.
            int len = 0;
            byte[] bytes = BitConverter.GetBytes(len);

            // Decode the message length.
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = DecodeByte(bm, rand, used_positions);
            }
            len = BitConverter.ToInt32(bytes, 0);

            // Sanity check.
            if (len > 10000)
            {
                throw new InvalidDataException(
                    "Message Length Is Too Big To Make Sense. Invalid Password.");
            }

            // Decode the message bytes.
            char[] chars = new char[len];
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)DecodeByte(bm, rand, used_positions);
            }
            return new string(chars);
        }

        // Decode a byte.
        private byte DecodeByte(Bitmap bm, Random rand, HashSet<string> used_positions)
        {
            byte value = 0;
            byte value_mask = 1;
            for (int i = 0; i < 8; i++)
            {
                // Find the position for the ith bit.
                int row, col, pix;
                PickPosition(bm, rand, used_positions, out row, out col, out pix);

                // Get the color component value.
                byte color_value = 0;
                switch (pix)
                {
                    case 0:
                        color_value = bm.GetPixel(row, col).R;
                        break;
                    case 1:
                        color_value = bm.GetPixel(row, col).G;
                        break;
                    case 2:
                        color_value = bm.GetPixel(row, col).B;
                        break;
                }

                // Set the next bit if appropriate.
                if ((color_value & 1) == 1)
                {
                    // Set the bit.
                    value = (byte)(value | value_mask);
                }

                // Move to the next bit.
                value_mask <<= 1;
            }

            return value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            // Copy the original message.
            EncodedImage = (Bitmap)OriginalImage.Clone();
            MarkedImage = (Bitmap)OriginalImage.Clone();

            // Encode.
            try
            {
                EncodeMessageInImage(EncodedImage, MarkedImage,
                    txtPassword.Text, txtMessage.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Display the results.
            radMarked.Checked = true;
            pictureBox1.Image = MarkedImage;

            savePictureToolStripMenuItem.Enabled = true;
            this.Cursor = Cursors.Default;
            LogFile.WriteLogInformation("XMLog.xml", "Encoding Done", "(Password Based)");
        }

        private void savePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EncodedImage == null)
            {
                MessageBox.Show("You Have Not Added A Message To The Image.");
            }
            else
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        FileInfo file_info = new FileInfo(saveFileDialog1.FileName);
                        switch (file_info.Extension)
                        {
                            case ".png":
                                EncodedImage.Save(saveFileDialog1.FileName, ImageFormat.Png);
                                break;
                            case ".bmp":
                                EncodedImage.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                                break;
                            case ".gif":
                                EncodedImage.Save(saveFileDialog1.FileName, ImageFormat.Gif);
                                break;
                            case ".tiff":
                            case ".tif":
                                EncodedImage.Save(saveFileDialog1.FileName, ImageFormat.Tiff);
                                break;
                            case ".jpg":
                            case ".jpeg":
                                EncodedImage.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            txtMessage.Text = "";
            Application.DoEvents();
            // Decode.
            try
            {
                txtMessage.Text = DecodeMessageInImage(OriginalImage, txtPassword.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Cursor = Cursors.Default;
            LogFile.WriteLogInformation("XMLog.xml", "Decoding Done", "(Password Based)");
        }
    }
}
