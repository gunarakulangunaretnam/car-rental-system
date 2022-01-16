using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.IO;
using System.Xml;
using System.Security.Cryptography;

namespace Car_Renting_Management_System
{
    public partial class SplshScreen : Form
    {
        public SplshScreen()
        {
            InitializeComponent();
        }

      

        private void SplshScreen_Load(object sender, EventArgs e)
        {
            
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string specificFolder = Path.Combine(folder, "CRMS");
            

            Directory.CreateDirectory(specificFolder);

            string key = "-HS4/b=X4.>yCn+XJeX@7PT=TKMM{8>eAcKa!jB";
            string userName = "guna";
            string password = "guna123";
            string adminUser = "kuna";
            string adminPass = "123";
            string encuser = "";
            string encpass = "";
            string adminUserEn = "";
            string adminPassEn = "";
             

            encuser = MySecurity.Encrypt(userName, key);
            encpass = MySecurity.Encrypt(password, key);
            adminUserEn = MySecurity.Encrypt(adminUser, key);
            adminPassEn = MySecurity.Encrypt(adminPass, key);
            

            if (!File.Exists(specificFolder + "//credentials.xml") || !File.Exists(specificFolder + "//settings.xml"))
            {

                XmlTextWriter xwriter = new XmlTextWriter(specificFolder + "\\credentials.xml", Encoding.UTF8);
                xwriter.Formatting = Formatting.Indented;
                xwriter.WriteStartElement("LoginInfo");
                xwriter.WriteStartElement("Username");
                xwriter.WriteString(encuser);
                xwriter.WriteEndElement();
                xwriter.WriteStartElement("Password");
                xwriter.WriteString(encpass);
                xwriter.WriteEndElement();
                xwriter.WriteEndElement();
                xwriter.Close();

                XmlTextWriter xwriter2 = new XmlTextWriter(specificFolder + "\\Paymentsettings.xml", Encoding.UTF8);

                 xwriter2.WriteStartElement("paymentSettings");

                 xwriter2.Formatting = Formatting.Indented;
                 xwriter2.WriteStartElement("DriverSettings");
                 xwriter2.WriteStartElement("DriverDailySalary");
                 xwriter2.WriteString("1000");
                 xwriter2.WriteEndElement();
                 xwriter2.WriteStartElement("DriverMonthlySalary");
                 xwriter2.WriteString("30000");
                 xwriter2.WriteEndElement();
                 xwriter2.WriteStartElement("salaryType");
                 xwriter2.WriteString("both");
                 xwriter2.WriteEndElement();
                 xwriter2.WriteEndElement();

                xwriter2.WriteStartElement("ExtraKillometer");
                xwriter2.WriteStartElement("KmDepandsOn");
                xwriter2.WriteString("CarRate");
                xwriter2.WriteEndElement();
                xwriter2.WriteStartElement("forExtraKm");
                xwriter2.WriteString("0");
                xwriter2.WriteEndElement();
                xwriter2.WriteEndElement();

                xwriter2.WriteStartElement("others");
                xwriter2.WriteStartElement("AdAmountPer");
                xwriter2.WriteString("25");
                xwriter2.WriteEndElement();
                xwriter2.WriteEndElement();

                xwriter2.WriteEndElement();



                xwriter2.Close();

                XmlTextWriter xwriter3 = new XmlTextWriter(specificFolder + "\\adminCredentials.xml", Encoding.UTF8);

                xwriter3.Formatting = Formatting.Indented;
                xwriter3.WriteStartElement("LoginInfo");
                xwriter3.WriteStartElement("username");
                xwriter3.WriteString(adminUserEn);
                xwriter3.WriteEndElement();
                xwriter3.WriteStartElement("password");
                xwriter3.WriteString(adminPassEn);
                xwriter3.WriteEndElement();
                xwriter3.WriteEndElement();
                xwriter3.Close();

                XmlTextWriter xwriter4 = new XmlTextWriter(specificFolder + "\\userStatus.xml", Encoding.UTF8);

                xwriter4.Formatting = Formatting.Indented;
                xwriter4.WriteStartElement("status");
                xwriter4.WriteString("null");
                xwriter4.WriteEndElement();
                xwriter4.Close();

                XmlTextWriter xwriter5 = new XmlTextWriter(specificFolder + "\\databaseController.xml", Encoding.UTF8);

                xwriter5.Formatting = Formatting.Indented;
                xwriter5.WriteStartElement("databaseDeleted");
                xwriter5.WriteString("no");
                xwriter5.WriteEndElement();
                xwriter5.Close();


                XmlTextWriter xwriter6 = new XmlTextWriter(specificFolder + "\\kmSettings.xml", Encoding.UTF8);

                xwriter6.Formatting = Formatting.Indented;
                xwriter6.WriteStartElement("kmSettings");

                xwriter6.WriteStartElement("kmType");
                xwriter6.WriteString("both");
                xwriter6.WriteEndElement();

                xwriter6.WriteStartElement("perday");
                xwriter6.WriteString("100");
                xwriter6.WriteEndElement();

                xwriter6.WriteStartElement("perMonth");
                xwriter6.WriteString("3000");
                xwriter6.WriteEndElement();

                xwriter6.WriteEndElement();
                xwriter6.Close();


                XmlTextWriter xwriter7 = new XmlTextWriter(specificFolder + "\\dicountSettings.xml", Encoding.UTF8);

                xwriter7.Formatting = Formatting.Indented;
                xwriter7.WriteStartElement("dicountSettings");

                xwriter7.WriteStartElement("StaffAllowed");
                xwriter7.WriteString("no");
                xwriter7.WriteEndElement();

                xwriter7.WriteStartElement("controlType");
                xwriter7.WriteString("full");
                xwriter7.WriteEndElement();

                xwriter7.WriteStartElement("adminNeeded");
                xwriter7.WriteString("no");
                xwriter7.WriteEndElement();

                xwriter7.WriteStartElement("fixedPerVAl");
                xwriter7.WriteString("0");
                xwriter7.WriteEndElement();

                xwriter7.WriteEndElement();
                xwriter7.Close();
                trashBinApp();

            }

            timer2.Enabled = true;
            timer2.Start();
            timer2.Interval = 125;

            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = 75;

            SoundPlayer p = new SoundPlayer("so1.wav");

            p.Play();

            this.Opacity = 0.1;
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (this.Opacity <= 1.0)
            {

                this.Opacity += 0.05;

            }
            else {

                timer1.Stop();
            }

            if (progressBar1.Value == 100)
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, "CRMS");

                timer1.Stop();
                timer2.Stop();

                string GenPath = folder + "\\CRMS\\generalSettings.xml";

               


                if (File.Exists(GenPath))
                {

                    accountType ac = new accountType();
                    ac.Show();
                    this.Hide();

             
                }
                else
                {


                    GenralSettingPrecome sp = new GenralSettingPrecome();
                    sp.Show();
                    this.Hide();

                }


            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void trashBinApp() {

            string pathforLog2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string subPath = "//CRMS//Trash-Bin";
            bool exits = Directory.Exists(pathforLog2 + subPath);

            if (!exits)
            {
                Directory.CreateDirectory(pathforLog2 + subPath);

            }


            string path = System.AppDomain.CurrentDomain.BaseDirectory + "good.ico";



            ApplyFolderIcon(pathforLog2 + subPath, path);

        }

        private static void ApplyFolderIcon(string targetFolderPath, string iconFilePath)
        {
            var iniPath = Path.Combine(targetFolderPath, "desktop.ini");
            if (File.Exists(iniPath))
            {
                //remove hidden and system attributes to make ini file writable
                File.SetAttributes(
                   iniPath,
                   File.GetAttributes(iniPath) &
                   ~(FileAttributes.Hidden | FileAttributes.System));
            }

            //create new ini file with the required contents
            var iniContents = new StringBuilder()
                .AppendLine("[.ShellClassInfo]")
                .AppendLine($"IconResource={iconFilePath},0")
                .AppendLine($"IconFile={iconFilePath}")
                .AppendLine("IconIndex=0")
                .ToString();
            File.WriteAllText(iniPath, iniContents);

            //hide the ini file and set it as system
            File.SetAttributes(
               iniPath,
               File.GetAttributes(iniPath) | FileAttributes.Hidden | FileAttributes.System);
            //set the folder as system
            File.SetAttributes(
                targetFolderPath,
                File.GetAttributes(targetFolderPath) | FileAttributes.System);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();
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
    }
}
