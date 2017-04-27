using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encryption
{
    public partial class frmMain : Form
    {
        byte[] iv = System.Text.Encoding.UTF8.GetBytes("R@nd0m 1n173cT02");

        public frmMain()
        {
            InitializeComponent();
            txtKey.Text = "This is my key01";
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            
            Encryptor enc = new Encryptor();
            try
            {
                String encryptedText = Convert.ToBase64String(enc.EncryptStringToBytes_AesIV(txtSource.Text, System.Text.Encoding.UTF8.GetBytes(txtKey.Text)));
                txtSource.Text = encryptedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            enc = null;
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            Encryptor enc = new Encryptor();
            try
            {
                String encryptedText = enc.DecryptStringFromBytes_AesIV(Convert.FromBase64String(txtSource.Text), System.Text.Encoding.UTF8.GetBytes(txtKey.Text));
                txtSource.Text = encryptedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            enc = null;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
