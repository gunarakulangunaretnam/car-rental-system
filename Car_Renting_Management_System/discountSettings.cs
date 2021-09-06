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

namespace Car_Renting_Management_System
{
    public partial class discountSettings : Form
    {
        public discountSettings()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        int canMove = 0;
        int Xc = 0;
        int Yc = 0;

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            canMove = 0;

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1) {

                this.SetDesktopLocation(MousePosition.X - Xc, MousePosition.Y - Yc);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            Xc = e.X;
            Yc = e.Y;

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            Xc = e.X;
            Yc = e.Y;
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 375, MousePosition.Y - 18);
            }
        }

        private void discountSettings_Load(object sender, EventArgs e)
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            XmlDocument xD = new XmlDocument();
            xD.Load(path + "\\CRMS\\dicountSettings.xml");

            string staffAl = xD.SelectSingleNode("dicountSettings/StaffAllowed").InnerText;
            string adminPer = xD.SelectSingleNode("dicountSettings/adminNeeded").InnerText;
            string controlType = xD.SelectSingleNode("dicountSettings/controlType").InnerText;
            int fixedVal = Convert.ToInt32(xD.SelectSingleNode("dicountSettings/fixedPerVAl").InnerText);

            label13.Visible = false;
            label12.Visible = false;

            if (staffAl == "no")
            {

                bunifuiOSSwitch1.Value = false;


            }
            else if (staffAl == "yes") {



                bunifuiOSSwitch1.Value = true;

            }


            if (adminPer == "no")
            {


                bunifuiOSSwitch2.Value = false;
            }
            else if (adminPer == "yes") {

                bunifuiOSSwitch2.Value = true;
            }

            if (controlType == "full")
            {

                radioButton1.Checked = true;

            }
            else if (controlType == "fixed")
            {

                radioButton2.Checked = true;

                numericUpDown1.Value = fixedVal;

                label12.Visible = true;
                label13.Visible = true;
              

                label13.Text = fixedVal.ToString() + "%";

            }
               
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) {

                numericUpDown1.Visible = false;
                label13.Visible = false;
                label12.Visible = false;
                label7.Visible = false;

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) {

                numericUpDown1.Visible= true;
                label13.Visible = true;
                label12.Visible = true;
                label7.Visible = true;

            }
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            XmlDocument xml = new XmlDocument();
            xml.Load(path + "\\CRMS\\dicountSettings.xml");

            if (radioButton1.Checked)
            {

                xml.SelectSingleNode("dicountSettings/controlType").InnerText = "full";


            } else if (radioButton2.Checked) {


                xml.SelectSingleNode("dicountSettings/controlType").InnerText = "fixed";

            }

            if (bunifuiOSSwitch1.Value == false)
            {

                xml.SelectSingleNode("dicountSettings/StaffAllowed").InnerText = "no";
                
            }
            else if (bunifuiOSSwitch1.Value == true) {

                xml.SelectSingleNode("dicountSettings/StaffAllowed").InnerText = "yes";
            }



            if (bunifuiOSSwitch2.Value == false)
            {

                xml.SelectSingleNode("dicountSettings/adminNeeded").InnerText = "no";

            }
            else if (bunifuiOSSwitch2.Value == true)
            {

                xml.SelectSingleNode("dicountSettings/adminNeeded").InnerText = "yes";
            }

            if (numericUpDown1.Enabled == true) {



                xml.SelectSingleNode("dicountSettings/fixedPerVAl").InnerText = numericUpDown1.Value.ToString();
            }


            xml.Save(path + "\\CRMS\\dicountSettings.xml");

            SuccessMSGBox su = new SuccessMSGBox("Changes have been made successfully.");
            su.Show();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            label13.Text = numericUpDown1.Value.ToString() + "%";
        }

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            if (numericUpDown1.Text == "")
            {

                label13.Text = "0%";

            }
            else
            {

                label13.Text = numericUpDown1.Value.ToString() + "%";

            }
        }
    }
}
