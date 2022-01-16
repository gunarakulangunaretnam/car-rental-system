using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;
using System.IO;

namespace Car_Renting_Management_System
{
    public partial class ChangeUsername : Form
    {
        public ChangeUsername()
        {
            InitializeComponent();
        }

        int canMo;
        int x;
        int y;
        
        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMo == 1)
            {
                this.SetDesktopLocation(MousePosition.X - x, MousePosition.Y - y);
                
            }
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            canMo = 0;
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            canMo = 1;
            x = e.X;
            y = e.Y;
        }

        string user = "";
        string pass = "";
        string key = "-HS4/b=X4.>yCn+XJeX@7PT=TKMM{8>eAcKa!jB";


        private void ChangeUsername_Load(object sender, EventArgs e)
        {
            string userName = "";
            string password = "";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path + "\\CRMS\\credentials.xml");
            userName = xmlDoc.SelectSingleNode("LoginInfo/Username").InnerText;
            password = xmlDoc.SelectSingleNode("LoginInfo/Password").InnerText;

            user = MySecurity.Decrypt(userName,key);
            pass = MySecurity.Decrypt(password, key);
            
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

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            if (monoFlat_TextBox1.Text != string.Empty && monoFlat_TextBox2.Text != string.Empty)
            {
                if (monoFlat_TextBox1.Text == user)
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(path + "\\CRMS\\credentials.xml");
                    xmlDoc.SelectSingleNode("LoginInfo/Username").InnerText = MySecurity.Encrypt(monoFlat_TextBox2.Text, key);
                    xmlDoc.Save(path + "\\CRMS\\credentials.xml");

                    string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string txt = "The Staff username was changed on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString();
                    File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

                    SuccessMSGBox s = new SuccessMSGBox("Username has been changed.");
                    s.Show();
                }
                else
                {

                    ErrorMsgBox er = new ErrorMsgBox("The old username is wrong.");
                    er.Show();
                }
            }
            else {

                if (monoFlat_TextBox1.Text == string.Empty && monoFlat_TextBox2.Text == string.Empty)
                {

                    ErrorMsgBox er = new ErrorMsgBox("Enter usernames in both in fields.");
                    er.Show();
                }
                else if (monoFlat_TextBox1.Text == string.Empty)
                {

                    ErrorMsgBox er = new ErrorMsgBox("Enter the old username.");
                    er.Show();

                }
                else if (monoFlat_TextBox2.Text == string.Empty)
                {

                    ErrorMsgBox er = new ErrorMsgBox("Enter the new username.");
                    er.Show();
                }
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            canMo = 1;
            x = e.X;
            y = e.Y;
        }

        private void label4_MouseUp(object sender, MouseEventArgs e)
        {
            canMo = 0;
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMo == 1)
            {
                this.SetDesktopLocation(MousePosition.X - 315, MousePosition.Y - 17);

            }
        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
