using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
namespace Car_Renting_Management_System
{
    public partial class AdminPassword : Form
    {
        double firstVal = 0;
        double lastVal = 0;
        double driverSala = 0;
        string fixx = "no";

        public AdminPassword(double fi,double la,double dri)
        {
            InitializeComponent();
            
            firstVal = fi;
            lastVal = la;
            driverSala = dri;
        }

        public AdminPassword(double fi, double la,double dri,string fix)
        {
            InitializeComponent();


            firstVal = fi;
            lastVal = la;
            driverSala = dri;
            fixx = fix;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        int can = 0;
        int xC = 0;
        int yC = 0;

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            can = 0;

        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            can = 1;
            xC = e.X;
            yC = e.Y;
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (can == 1) {

                this.SetDesktopLocation(MousePosition.X-xC,MousePosition.Y-yC);

            }
        }

        private void label2_MouseUp(object sender, MouseEventArgs e)
        {
            can = 0;
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            if (can == 1) {

                this.SetDesktopLocation(MousePosition.X - 360, MousePosition.Y - 18);
            }

        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            can = 1;
            xC = e.X;
            yC = e.Y;
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            XmlDocument xmlAdminPass = new XmlDocument();
            xmlAdminPass.Load(path + "\\CRMS\\adminCredentials.xml");

            string key = "-HS4/b=X4.>yCn+XJeX@7PT=TKMM{8>eAcKa!jB";
            string adminPass = xmlAdminPass.SelectSingleNode("LoginInfo/password").InnerText;

            string nowPass = MySecurity.Decrypt(adminPass, key);

            if (fixx == "no")
            {

                if (monoFlat_TextBox1.Text == nowPass)
                {

                    DiscountOptions Do = new DiscountOptions(firstVal, lastVal,driverSala);
                    Do.ShowDialog();

                    if (Do.textBox1.Text == "yes")
                    {

                        string value = Do.label9.Text;

                        string resultString = Regex.Match(value, @"\d+").Value;

                        textBox1.Text = resultString;
                        textBox2.Text = Do.label13.Text;
                        this.Hide();
                        

                    }

                }
                else
                {

                    ErrorMsgBox er = new ErrorMsgBox("The admin password is wrong.");
                    er.Show();
                }

            }else if (fixx == "yes")
            {

                if (monoFlat_TextBox1.Text == nowPass)
                {

                    DiscountFixed Do = new DiscountFixed(firstVal, lastVal,driverSala);
                    Do.ShowDialog();

                    if (Do.textBox1.Text == "yes")
                    {

                        string value = Do.label9.Text;

                        string resultString = Regex.Match(value, @"\d+").Value;

                        textBox1.Text = resultString;
                        textBox2.Text = Do.label2.Text;
                        this.Hide();
                        
                    }
                }
                else
                {

                    ErrorMsgBox er = new ErrorMsgBox("The admin password is wrong.");
                    er.Show();
                }



            }
        }

        public static class MySecurity
        {
            // This constant is used to determine the keysize of the encryption algorithm in bits.
            // I divide this by 8 within the code below to get the equivalent number of bytes.
            private const int Keysize = 256;

            // This constant determines the number of iterations for the password bytes generation function.
            private const int DerivationIterations = 1000;

            public static string Encrypt(string plainText, string passPhrase)
            {
                // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
                // so that the same Salt and IV values can be used when decrypting.  
                var saltStringBytes = Generate256BitsOfRandomEntropy();
                var ivStringBytes = Generate256BitsOfRandomEntropy();
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = 256;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                                {
                                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                    cryptoStream.FlushFinalBlock();
                                    // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                    var cipherTextBytes = saltStringBytes;
                                    cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                    cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Convert.ToBase64String(cipherTextBytes);
                                }
                            }
                        }
                    }
                }
            }

            public static string Decrypt(string cipherText, string passPhrase)
            {
                // Get the complete stream of bytes that represent:
                // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
                var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
                // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
                var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
                // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
                var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
                // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
                var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

                using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = 256;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    var plainTextBytes = new byte[cipherTextBytes.Length];
                                    var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                }
                            }
                        }
                    }
                }
            }

            private static byte[] Generate256BitsOfRandomEntropy()
            {
                var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with cryptographically secure random bytes.
                    rngCsp.GetBytes(randomBytes);
                }
                return randomBytes;
            }
        }

        private void AdminPassword_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
        }
    }
}
