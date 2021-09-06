using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml;
using Ionic.Zip;

namespace Car_Renting_Management_System
{
    public partial class settings : Form
    {
        int PW;
        bool Hidden;

        int pw2;
        bool hidden2;

        int pw3;
        bool hidden3;

        int pw4;
        bool hidden4;

        public settings()
        {
            InitializeComponent();

            panel3.Height = 0;
            PW = panel3.Height;
            Hidden = true;

            panel4.Height = 0;
            pw2 = panel4.Height = 0;
            hidden2 = true;

            panel5.Height = 0;
            pw3 = panel5.Height = 0;
            hidden3 = true;

            panel6.Height = 0;
            pw4 = panel6.Height = 0;
            hidden4 = true;

        }
        CURDQueryClass c = new CURDQueryClass();
        private void label2_Click(object sender, EventArgs e)
        {

            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void settings_Load(object sender, EventArgs e)
        {

            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
            
            pictureBox3.Visible = true;
            pictureBox4.Visible = false;

            pictureBox5.Visible = true;
            pictureBox6.Visible = false;

            pictureBox7.Visible = true;
            pictureBox8.Visible = false;




            string databaseDeleted = "";

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path + "\\CRMS\\databaseController.xml");

            databaseDeleted = Convert.ToString(xmlDoc.SelectSingleNode("databaseDeleted").InnerText);

            if (databaseDeleted == "yes")
            {

                bunifuFlatButton5.Enabled = false;
                bunifuFlatButton8.Enabled = false;

                this.bunifuFlatButton5.Iconimage = global::Car_Renting_Management_System.Properties.Resources.re;
                this.bunifuFlatButton8.Iconimage = global::Car_Renting_Management_System.Properties.Resources.re2;

            }
            else if (databaseDeleted == "no") {

                bunifuFlatButton5.Enabled = true;
                bunifuFlatButton8.Enabled = true;

                this.bunifuFlatButton8.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_Blue_Backup_B_66548;
                this.bunifuFlatButton5.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_remove_from_database_49610;


            }

        }

        private void bunifuFlatButton1_MouseHover(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            timer1.Start();

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {

            timer2.Start();
        }

        private void settings_Move(object sender, EventArgs e)
        {


        }

        private void settings_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void bunifuFlatButton1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Process.Start("Notepad", path + "\\CRMS\\ActivityLog.txt");
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {

            string systemThere = "";
            try
            {
                MySqlConnection myCon = new MySqlConnection("SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=");
                myCon.Open();
                string SqlCode = "SHOW DATABASES LIKE 'rentcarsystem'";
                MySqlCommand mycom = new MySqlCommand(SqlCode, myCon);
                
                MySqlDataReader d = mycom.ExecuteReader();
                
                while (d.Read())
                {

                    systemThere = d.GetString(0);
                }

            }
            catch (Exception)
            {

                
            }

            if (systemThere == "rentcarsystem")
            {
                Backup();

            }
            else
            {
                ErrorMsgBox err = new ErrorMsgBox("Unable to backup. The database is not existed.");
                err.Show();
            }
            
        }


        private void Backup()
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Title = "Backup The Database";
            sd.Filter = "SQL File |*.sql";
            sd.FileName = "rentcarsystem.sql";//Default text.
            string path = "";
            if (sd.ShowDialog() == DialogResult.OK)
            {

                path = sd.FileName;


            }
            try
            {
                string constring = "SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=";
                string file = path;
                using (MySqlConnection conn = new MySqlConnection(constring))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(file);
                            conn.Close();
                        }
                    }
                }

                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string txt = "The database was backedup on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString();
                File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

                SuccessMSGBox s = new SuccessMSGBox("Backup Completed");
                s.Show();
            }
            catch (Exception)
            {


            }

        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {

            string systemThere = "";
            string SqlCode = "SHOW DATABASES LIKE 'rentcarsystem'";
            MySqlConnection myCon = new MySqlConnection("SERVER=localhost;UID=root;PASSWORD=");
            myCon.Open();
            MySqlCommand myCom = new MySqlCommand(SqlCode, myCon);


            MySqlDataReader d = myCom.ExecuteReader();

            while (d.Read())
            {

                systemThere = d.GetString(0);
            }


            if (systemThere == "rentcarsystem")
            {

                ErrorMsgBox err = new ErrorMsgBox("Unable to restore. The database is already existed.\nIf you want to restore, delete the database and try again.");
                err.Show();
            }
            else
            {
                Restore();


                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path + "\\CRMS\\databaseController.xml");

                xmlDoc.SelectSingleNode("databaseDeleted").InnerText = "no";

                xmlDoc.Save(path + "\\CRMS\\databaseController.xml");
                settings s = new settings();

               
            }
            
        }

        private void Restore()
        {
            
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Restore The Database";
            ofd.Filter = "SQL Files|*.sql";
            string path = "";

            string canRestore = "no";

            if (ofd.ShowDialog() == DialogResult.OK)
            {

                path = ofd.FileName;
                canRestore = "Yes";

                bunifuFlatButton5.Enabled = true;
                bunifuFlatButton8.Enabled = true;
                this.bunifuFlatButton8.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_Blue_Backup_B_66548;
                this.bunifuFlatButton5.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_remove_from_database_49610;
            }

            if (canRestore == "Yes")
            {

                try
                {

                    string cs = @"server=localhost;userid=root;password=";
                    MySqlConnection conn = null;
                    MySqlCommand cmd;

                    try
                    {
                        conn = new MySqlConnection(cs);
                        conn.Open();

                        string s0 = "CREATE DATABASE IF NOT EXISTS `rentcarsystem`;";
                        cmd = new MySqlCommand(s0, conn);
                        cmd.ExecuteNonQuery();


                    }
                    catch (MySqlException ex)

                    {


                    }
                    finally

                    {
                        if (conn != null)
                        {
                            conn.Close();
                        }
                    }



                    string constring = "SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=";
                    string file = path;
                    using (MySqlConnection connn = new MySqlConnection(constring))
                    {
                        using (MySqlCommand cmdd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmdd))
                            {
                                cmdd.Connection = connn;
                                connn.Open();
                                mb.ImportFromFile(file);
                                connn.Close();
                            }
                        }
                    }

                    string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string txt = "The database was restored on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString();
                    File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

                    SuccessMSGBox s = new SuccessMSGBox("Restore Completed");
                    s.Show();

                }
                catch (Exception)
                {


                }

            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            timer3.Start();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            ChangeUsername u = new ChangeUsername();
            u.ShowDialog();
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            changePassword c = new changePassword();
            c.ShowDialog();

        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
                try
                {
                    string constring = "SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=";
                    //  string file = "D:\\";
                    string pathforLog2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    using (MySqlConnection conn = new MySqlConnection(constring))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;
                                conn.Open();
                                mb.ExportToFile(pathforLog2 + "//CRMS//Trash-Bin//rentcarsystem.sql");
                                conn.Close();
                            }
                        }
                    }

                }
                catch (Exception)
                {


                }

                ComformFormDelete cfd = new ComformFormDelete("Do you want to delete the Database", "DROP DATABASE rentcarsystem", "databaseDeletion", "databaseDeletion");
                cfd.ShowDialog();
          
                string databaseDeleted = "";

                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path + "\\CRMS\\databaseController.xml");

                databaseDeleted = Convert.ToString(xmlDoc.SelectSingleNode("databaseDeleted").InnerText);

                if (databaseDeleted == "yes") {

                    bunifuFlatButton5.Enabled = false;
                    bunifuFlatButton8.Enabled = false;

                this.bunifuFlatButton5.Iconimage = global::Car_Renting_Management_System.Properties.Resources.re;
                this.bunifuFlatButton8.Iconimage = global::Car_Renting_Management_System.Properties.Resources.re2;

            }
           
        }

        private void bunifuFlatButton10_Click(object sender, EventArgs e)
        {
            StaffCredentials st = new StaffCredentials();
            st.Show();
        }

        private void bunifuFlatButton11_Click(object sender, EventArgs e)
        {
            AdminCredentials ad = new AdminCredentials();
            ad.Show();
        }


        public static class Util
        {
            public enum Effect { Roll, Slide, Center, Blend }

            public static void Animate(Control ctl, Effect effect, int msec, int angle)
            {
                int flags = effmap[(int)effect];
                if (ctl.Visible) { flags |= 0x10000; angle += 180; }
                else
                {
                    if (ctl.TopLevelControl == ctl) flags |= 0x20000;
                    else if (effect == Effect.Blend) throw new ArgumentException();
                }
                flags |= dirmap[(angle % 360) / 45];
                bool ok = AnimateWindow(ctl.Handle, msec, flags);
                if (!ok) throw new Exception("Animation failed");
                ctl.Visible = !ctl.Visible;
            }

            private static int[] dirmap = { 1, 5, 4, 6, 2, 10, 8, 9 };
            private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };

            [DllImport("user32.dll")]
            private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Hidden == true)
            {

                panel3.Height = panel3.Height + 5;

                if (panel3.Height >= 150)
                {

                    timer1.Stop();
                    Hidden = false;
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = true;
                    this.Refresh();
                }

            }
            else
            {
                panel3.Height = panel3.Height - 5;

                if (panel3.Height <= 0)
                {
                    timer1.Stop();
                    Hidden = true;

                    pictureBox1.Visible = true;
                    pictureBox2.Visible = false;
                    this.Refresh();

                }

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            timer1.Start();

        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            if (hidden2 == true)
            {

                panel4.Height = panel4.Height + 5;

                if (panel4.Height >= 200)
                {

                    timer2.Stop();
                    hidden2 = false;
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = true;
                    this.Refresh();
                }

            }
            else
            {
                panel4.Height = panel4.Height - 5;

                if (panel4.Height <= 0)
                {
                    timer2.Stop();
                    hidden2 = true;

                    pictureBox3.Visible = true;
                    pictureBox4.Visible = false;
                    this.Refresh();

                }
            }
        }

        private void bunifuFlatButton13_Click(object sender, EventArgs e)
        {
            string pathforLog2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string subPath = "//CRMS";
            string fullPath = pathforLog2 + subPath;
            

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Zip Files|*.zip";
            sfd.Title = "Save Files";
            sfd.FileName = "System Settings";

            if (sfd.ShowDialog() == DialogResult.OK) {
                

                string path = @""+sfd.FileName+"\\ReadmeFirst.txt";

                string newpath = path.Replace("System Settings.zip", "");

                

                if (!File.Exists(newpath))
                {
                    File.Create(newpath).Dispose();

                    using (TextWriter tw = new StreamWriter(newpath))
                    {
                        tw.WriteLine("How to restore the settings back up files to the system.");
                        tw.WriteLine("==============================================");
                        tw.WriteLine("\n");
                        tw.WriteLine("\n");

                        tw.WriteLine("Way 01 Easy method.");
                        tw.WriteLine("-------------------");
                        tw.WriteLine("01.Open the system restorer application which is in this folder.");
                        tw.WriteLine("02.Follow the instructions.");
                        tw.WriteLine("03.Select the CRMS folder which in this directory.");
                        tw.WriteLine("04.Finally, Click the restore button.");
                        tw.WriteLine("05.Still doubt?, just open 'Help for Resotre' folder and look at the method1.");
                        tw.WriteLine("\n");
                        tw.WriteLine("\n");
                        tw.WriteLine("Way 02 Manual method.");
                        tw.WriteLine("-------------------");
                        tw.WriteLine("01.Copy the CRMS folder.");
                        tw.WriteLine("02.Make sure to check the view hidden files in the explorer.");
                        tw.WriteLine("03.Naviate to the users->'accountName'->AppData->Roaming.");
                        tw.WriteLine("04.Finally,paste it there.");
                        tw.WriteLine("05.Still doubt?, just open 'Help for Resotre' folder and look at the method2.");

                        tw.WriteLine("\n");
                        tw.WriteLine("\n");
                        tw.WriteLine("Help for Resotre");
                        tw.WriteLine("--------------------");

                        tw.WriteLine("If you are unable to understand how to restore the settings yet. Just open the 'Help for Resotre' folder.\n There are some screenshots images that explains you how to restore the settings.");
                        tw.WriteLine("There are two methods have been shown in screenshot images. The first method is easy.\n You can restore the settings by using the system restorer application.");
                        tw.WriteLine("The second method is the manual way.That is a little bit of difficult.\n The both two methods have been shown as screenshots.");


                        tw.WriteLine("\n");
                        tw.WriteLine("\n");
                        
                        tw.WriteLine("K1 Tech");
                        tw.WriteLine("=======");
                        tw.WriteLine("\n");
                        tw.WriteLine("K1 Tech is a technology bashed company which provides software solutions, network solutions,\nCCTV solution, Computer repairing and other computer related solutions.");
                        tw.WriteLine("Contact us:0756800519 / 0752423036");
                        tw.Close();
                    }

                    string dataPath = System.AppDomain.CurrentDomain.BaseDirectory + "//dataforRestore";

                    if (File.Exists(newpath))
                    {
                        try
                        {
                            ZipFile zf = new ZipFile(sfd.FileName);
                            zf.AddDirectoryByName("CRMS");
                            zf.AddDirectory(fullPath, "CRMS");
                            zf.AddDirectory(dataPath, "");
                            zf.AddFile(newpath, "");

                            zf.Save();

                            SuccessMSGBox suc = new SuccessMSGBox("System settings backed up successfully.");
                            suc.ShowDialog();

                            File.Delete(newpath);

                        }
                        catch (Exception ex)
                        {
                            ErrorMsgBox er = new ErrorMsgBox("Something went wrong.");
                            er.Show();
                        }

                        
                    }
                    
                }
            }

        }

        private void bunifuFlatButton7_Click_1(object sender, EventArgs e)
        {
            genralSettings gs = new genralSettings();
            gs.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            timer3.Start();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            timer3.Start();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (hidden3 == true)
            {

                panel5.Height = panel5.Height + 5;

                if (panel5.Height >= 240)
                {

                    timer3.Stop();
                    hidden3 = false;
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = true;
                    this.Refresh();
                }

            }
            else
            {
                panel5.Height = panel5.Height - 5;

                if (panel5.Height <= 0)
                {
                    timer3.Stop();
                    hidden3 = true;

                    pictureBox5.Visible = true;
                    pictureBox6.Visible = false;
                    this.Refresh();

                }

            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            timer4.Start();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            timer4.Start();
        }

        private void bunifuFlatButton16_Click(object sender, EventArgs e)
        {
            timer4.Start();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (hidden4 == true)
            {

                panel6.Height = panel6.Height + 5;

                if (panel6.Height >= 180)
                {

                    timer4.Stop();
                    hidden4 = false;
                    pictureBox7.Visible = false;
                    pictureBox8.Visible = true;
                    this.Refresh();
                }

            }
            else
            {
                panel6.Height = panel6.Height - 5;

                if (panel6.Height <= 0)
                {
                    timer4.Stop();
                    hidden4 = true;

                    pictureBox7.Visible = true;
                    pictureBox8.Visible = false;
                    this.Refresh();

                }

            }
        }

        private void bunifuFlatButton15_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Process.Start(path+ "//CRMS//Trash-Bin");
        }

        private void bunifuFlatButton6_Click_1(object sender, EventArgs e)
        {
            PaymentSettings ps = new PaymentSettings();
            ps.Show();
        }

        private void bunifuFlatButton12_Click(object sender, EventArgs e)
        {
            KmSettings KmSet = new KmSettings();
            KmSet.Show();
        }

        private void bunifuFlatButton14_Click(object sender, EventArgs e)
        {
            discountSettings ds = new discountSettings();
            ds.Show();
        }
    }
}
