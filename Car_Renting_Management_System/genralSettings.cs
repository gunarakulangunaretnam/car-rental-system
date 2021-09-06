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
using System.IO;

namespace Car_Renting_Management_System
{
    public partial class genralSettings : Form
    {
        public genralSettings()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        int MoveAccess = 0;
        int XCoordiate = 0;
        int YCoordiate = 0;

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string GenPath = path + "\\CRMS\\generalSettings.xml";


            string GenPath2 = path + "\\CRMS";

            if (File.Exists(GenPath))
            {

                XmlDocument xDOc = new XmlDocument();
                xDOc.Load(GenPath);

                xDOc.SelectSingleNode("GeneralSettings/CompanyName").InnerText = monoFlat_TextBox1.Text;
                xDOc.SelectSingleNode("GeneralSettings/CompanyAddress").InnerText = bunifuCustomTextbox1.Text;
                xDOc.SelectSingleNode("GeneralSettings/Telephone").InnerText = monoFlat_TextBox2.Text;
                xDOc.SelectSingleNode("GeneralSettings/OptionalTelephone").InnerText = monoFlat_TextBox3.Text;

                xDOc.Save(GenPath);

                SuccessMSGBox su = new SuccessMSGBox("Changes have been made successfully.");
                su.Show();

            }
            else {



                XmlTextWriter XW = new XmlTextWriter(GenPath2 + "//generalSettings.xml", Encoding.UTF8);
                XW.Formatting = Formatting.Indented;
                XW.WriteStartElement("GeneralSettings");
                XW.WriteStartElement("CompanyName");
                XW.WriteString(monoFlat_TextBox1.Text);
                XW.WriteEndElement();
                XW.WriteStartElement("CompanyAddress");
                XW.WriteString(bunifuCustomTextbox1.Text);
                XW.WriteEndElement();
                XW.WriteStartElement("Telephone");
                XW.WriteString(monoFlat_TextBox2.Text);
                XW.WriteEndElement();
                XW.WriteStartElement("OptionalTelephone");
                XW.WriteString(monoFlat_TextBox3.Text);
                XW.WriteEndElement();
                XW.WriteEndElement();
                XW.Close();

                SuccessMSGBox su = new SuccessMSGBox("Changes have been made successfully.");
                su.Show();

            }

        }

        private void genralSettings_Load(object sender, EventArgs e)
        {



            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string GenPath = path + "\\CRMS\\generalSettings.xml";


            if (File.Exists(GenPath))
            {
                XmlDocument xDOc = new XmlDocument();
                xDOc.Load(path + "\\CRMS\\generalSettings.xml");

                monoFlat_TextBox1.Text = xDOc.SelectSingleNode("GeneralSettings/CompanyName").InnerText;
                bunifuCustomTextbox1.Text = xDOc.SelectSingleNode("GeneralSettings/CompanyAddress").InnerText;
                monoFlat_TextBox2.Text = xDOc.SelectSingleNode("GeneralSettings/Telephone").InnerText;
                monoFlat_TextBox3.Text = xDOc.SelectSingleNode("GeneralSettings/OptionalTelephone").InnerText;

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            MoveAccess = 1;
            XCoordiate = e.X;
            YCoordiate = e.Y;
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            MoveAccess = 0;
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (MoveAccess == 1) {

                this.SetDesktopLocation(MousePosition.X - XCoordiate, MousePosition.Y - YCoordiate);

            }
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            MoveAccess = 1;
            XCoordiate = e.X;
            YCoordiate = e.Y;
        }

        private void label4_MouseUp(object sender, MouseEventArgs e)
        {

            MoveAccess = 0;

        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            if (MoveAccess == 1)
            {

                this.SetDesktopLocation(MousePosition.X - XCoordiate-350, MousePosition.Y - YCoordiate-15);

            }
        }

        private void monoFlat_TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox2.Text, "[^0-9+]"))
            {

                char[] chars = monoFlat_TextBox2.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The Telephone number must be in digit format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox2.Text = charsToStr.Remove(charsToStr.Length - 1);


            }
        }

        private void monoFlat_TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox3.Text, "[^0-9+]"))
            {

                char[] chars = monoFlat_TextBox3.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The Telephone number must be in digit format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox3.Text = charsToStr.Remove(charsToStr.Length - 1);


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
