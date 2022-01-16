using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;
namespace Car_Renting_Management_System
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
      
        public LoginForm(string type)
        {
            InitializeComponent();
            label3.Text = type;
            
        }



        int canMove;
        int Xval;
        int Yval;

        string key = "-HS4/b=X4.>yCn+XJeX@7PT=TKMM{8>eAcKa!jB";
        byte[] encryptedUser;
        string userName = "";
        string password = "";
        string user = "";
        string pass = "";
        string adminUser = "";
        string adminPass = "";
        string adminUserDe = "";
        string adminPassDe = "";
        string accountType = "";
        private void LoginForm_Load(object sender, EventArgs e)
        {
            //Disabled
            bunifuFlatButton1.Enabled = false;

            string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!File.Exists(pathforLog + "\\CRMS\\ActivityLog.txt"))
            {

                StreamWriter sw = new StreamWriter(File.OpenWrite(pathforLog + "\\CRMS\\ActivityLog.txt"));

                sw.Dispose();

            }

            //black and White Image
            this.bunifuFlatButton1.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_login_173049_ConvertImage;

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path + "\\CRMS\\credentials.xml");
            userName=xmlDoc.SelectSingleNode("LoginInfo/Username").InnerText;
            password = xmlDoc.SelectSingleNode("LoginInfo/Password").InnerText;

            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.Load(path + "\\CRMS\\adminCredentials.xml");
            adminUserDe = xmlDoc2.SelectSingleNode("LoginInfo/username").InnerText;
            adminPassDe= xmlDoc2.SelectSingleNode("LoginInfo/password").InnerText;

            adminUser = MySecurity.Decrypt(adminUserDe, key);
            adminPass= MySecurity.Decrypt(adminPassDe, key);
            user = MySecurity.Decrypt(userName, key);
            pass = MySecurity.Decrypt(password, key);

            XmlDocument xmlDoc3 = new XmlDocument();
            xmlDoc3.Load(path + "\\CRMS\\userStatus.xml");
            accountType = xmlDoc3.SelectSingleNode("status").InnerText;
            

        }



        private void monoFlat_TextBox1_TextChanged(object sender, EventArgs e)
        {


            if (monoFlat_TextBox1.Text == string.Empty)
            {

                monoFlat_TrackBar1.Value = 0;

            }
            else
            {

                monoFlat_TrackBar1.Value = 5;

            }
        }

        private void monoFlat_TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (monoFlat_TextBox1.Text == string.Empty)
            {

                monoFlat_TrackBar1.Value = 5;

            }
            else
            {

                monoFlat_TrackBar1.Value = 10;

            }

        }

        private void monoFlat_TrackBar1_ValueChanged()
        {
            if (monoFlat_TrackBar1.Value == 10)
            {

                bunifuFlatButton1.Enabled = true;
                this.bunifuFlatButton1.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_login_173049;
            }
            else {

                bunifuFlatButton1.Enabled = false;
                this.bunifuFlatButton1.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_login_173049_ConvertImage;

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            if (accountType == "Staff")
            {

                if (monoFlat_TextBox1.Text == user && monoFlat_TextBox2.Text == pass)
                {

                    MainForm main = new MainForm();
                    main.Show();
                    this.Hide();

                }
                else
                {
                    ErrorMsgBox err = new ErrorMsgBox("You have entered an invalid username or password.");
                    err.Show();
                    monoFlat_TextBox1.Text = "";
                    monoFlat_TextBox2.Text = "";
                    monoFlat_TrackBar1.Value = 0;
                }

            }
            else if (accountType == "Admin") {


                if (monoFlat_TextBox1.Text == adminUser && monoFlat_TextBox2.Text == adminPass)
                {

                    MainForm main = new MainForm();
                    main.Show();
                    this.Hide();

                }
                else
                {
                    ErrorMsgBox err = new ErrorMsgBox("You have entered an invalid username or password.");
                    err.Show();
                    monoFlat_TextBox1.Text = "";
                    monoFlat_TextBox2.Text = "";
                    monoFlat_TrackBar1.Value = 0;
                }

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            Xval = e.X;
            Yval = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - Xval, MousePosition.Y - Yval);
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

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            accountType ac = new accountType();
            ac.Show();
            this.Hide();
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 400, MousePosition.Y - 17);
            }
           
        }

        private void label2_MouseUp(object sender, MouseEventArgs e)
        {
            panel1_MouseUp(sender,e);
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            panel1_MouseDown(sender, e);
        }
    }
}
