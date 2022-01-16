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

namespace Car_Renting_Management_System
{
    public partial class GenralSettingPrecome : MetroFramework.Forms.MetroForm
    {
        public GenralSettingPrecome()
        {
            InitializeComponent();
        }

        private void GenralSettingPrecome_Load(object sender, EventArgs e)
        {

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

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {

            if (monoFlat_TextBox1.Text != string.Empty && bunifuCustomTextbox1.Text != string.Empty && monoFlat_TextBox2.Text != string.Empty && monoFlat_TextBox3.Text != string.Empty)
            {

                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, "CRMS");



                XmlTextWriter xwriter6 = new XmlTextWriter(specificFolder + "\\generalSettings.xml", Encoding.UTF8);

                xwriter6.Formatting = Formatting.Indented;
                xwriter6.WriteStartElement("GeneralSettings");
                xwriter6.WriteStartElement("CompanyName");
                xwriter6.WriteString(monoFlat_TextBox1.Text);
                xwriter6.WriteEndElement();
                xwriter6.WriteStartElement("CompanyAddress");
                xwriter6.WriteString(bunifuCustomTextbox1.Text);
                xwriter6.WriteEndElement();
                xwriter6.WriteStartElement("Telephone");
                xwriter6.WriteString(monoFlat_TextBox2.Text);
                xwriter6.WriteEndElement();
                xwriter6.WriteStartElement("OptionalTelephone");
                xwriter6.WriteString(monoFlat_TextBox3.Text);
                xwriter6.WriteEndElement();
                xwriter6.WriteEndElement();
                xwriter6.Close();


                accountType ac = new accountType();
                ac.Show();
                this.Hide();


            }
            else {


                ErrorMsgBox er = new ErrorMsgBox("Please fill all the columns.");
                er.Show();

            }


        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            accountType ac = new accountType();
            ac.Show();
            this.Hide();
        }

        private void monoFlat_TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
