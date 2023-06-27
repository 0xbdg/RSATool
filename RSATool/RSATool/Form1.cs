using System;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace RSATool
{
    public partial class Form1 : Form
    {   
        public Form1()
        {
            InitializeComponent();
        }
        int RSABit;

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            using (RSACryptoServiceProvider r = new RSACryptoServiceProvider(RSABit))
            {
                publicBox.Text = r.ToXmlString(false);
                privateBox.Text = r.ToXmlString(true);
            }
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var enc = new RSACryptoServiceProvider(RSABit))
                {
                    enc.FromXmlString(textBox5.Text);
                    byte[] prosesenc = enc.Encrypt(Encoding.UTF8.GetBytes(textBox3.Text), true);
                    textBox1.Text = Convert.ToBase64String(prosesenc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error @_@",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dec = new RSACryptoServiceProvider(RSABit))
                {
                    dec.FromXmlString(textBox6.Text);

                    byte[] chiperbyte = Convert.FromBase64String(textBox4.Text);
                    byte[] prosesdec = dec.Decrypt(chiperbyte, true);

                    textBox2.Text = Convert.ToBase64String(prosesdec);
                }
            }
            catch (CryptographicException ec)
            {
                MessageBox.Show("private key is invalid!!", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error @_@", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    RSABit = 1024;
                    break;
                case 1:
                    RSABit = 2048;
                    break;
                case 2:
                    RSABit = 4096;
                    break;
            }
        }
    }
}