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
    public partial class PaymentSettings : Form
    {
        public PaymentSettings()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        int canmove;
        int xC;
        int yC;

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {

            if (canmove == 1) {

                this.SetDesktopLocation(MousePosition.X - xC, MousePosition.Y - yC);
            }

        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            canmove = 1;
            xC = e.X;
            yC = e.Y;
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            canmove = 0;
        }

        private void SystemConfig_Load(object sender, EventArgs e)
        {
    

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            XmlDocument xmlDocs = new XmlDocument();
            xmlDocs.Load(path + "\\CRMS\\Paymentsettings.xml");

            double driverDailySalary = 0;
            double driverMonthlySalary = 0;
            string SalaryMode = "";

            string carRateType = "";
            int extraKm = 0;

            int AdPer = 0;


            driverDailySalary = Convert.ToDouble(xmlDocs.SelectSingleNode("paymentSettings/DriverSettings/DriverDailySalary").InnerText);
            driverMonthlySalary = Convert.ToDouble(xmlDocs.SelectSingleNode("paymentSettings/DriverSettings/DriverMonthlySalary").InnerText);
            SalaryMode = xmlDocs.SelectSingleNode("paymentSettings/DriverSettings/salaryType").InnerText;

            carRateType = xmlDocs.SelectSingleNode("paymentSettings/ExtraKillometer/KmDepandsOn").InnerText;

            extraKm = Convert.ToInt32(xmlDocs.SelectSingleNode("paymentSettings/ExtraKillometer/forExtraKm").InnerText);

            AdPer = Convert.ToInt32(xmlDocs.SelectSingleNode("paymentSettings/others/AdAmountPer").InnerText);

            monoFlat_TextBox2.Text = driverDailySalary.ToString();
            monoFlat_TextBox1.Text = driverMonthlySalary.ToString();

            if (SalaryMode == "both")
            {

                radioButton1.Checked = true;

            }
            else if (SalaryMode == "onlyDaily") {


                radioButton2.Checked = true;

            }


            if (carRateType == "CarRate")
            {


                radioButton4.Checked = true;

            }
            else if (carRateType == "CustomRate") {

                radioButton3.Checked = true;
            }


            

            monoFlat_TextBox4.Text = extraKm.ToString();

            numericUpDown1.Value = AdPer;

            if (radioButton4.Checked) {


                monoFlat_TextBox4.Visible = false;
                label11.Visible = false;

            }


            label12.Text = numericUpDown1.Value.ToString()+"%";

        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path+ "\\CRMS\\Paymentsettings.xml");


            if (radioButton1.Checked == true)
            {


                xDoc.SelectSingleNode("paymentSettings/DriverSettings/salaryType").InnerText = "both";

            }
            else if (radioButton2.Checked == true) {

                
                xDoc.SelectSingleNode("paymentSettings/DriverSettings/salaryType").InnerText = "onlyDaily";


            }

            xDoc.SelectSingleNode("paymentSettings/DriverSettings/DriverDailySalary").InnerText = monoFlat_TextBox2.Text;
            xDoc.SelectSingleNode("paymentSettings/DriverSettings/DriverMonthlySalary").InnerText = monoFlat_TextBox1.Text;


            if (radioButton3.Checked)
            {

                xDoc.SelectSingleNode("paymentSettings/ExtraKillometer/KmDepandsOn").InnerText = "CustomRate";

            }
            else if (radioButton4.Checked) {


                xDoc.SelectSingleNode("paymentSettings/ExtraKillometer/KmDepandsOn").InnerText = "CarRate";
            }


            xDoc.SelectSingleNode("paymentSettings/ExtraKillometer/forExtraKm").InnerText = monoFlat_TextBox4.Text;

            xDoc.SelectSingleNode("paymentSettings/others/AdAmountPer").InnerText = numericUpDown1.Value.ToString();

            xDoc.Save(path + "\\CRMS\\Paymentsettings.xml");

            SuccessMSGBox su = new SuccessMSGBox("Changes have been made successfully.");
            su.Show();

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            canmove = 1;
            xC = e.X;
            yC = e.Y;
        }

        private void label4_MouseUp(object sender, MouseEventArgs e)
        {
            canmove = 0;
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            if (canmove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 600, MousePosition.Y - 17);
            }
        }

        private void monoFlat_TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox2.Text, "[^0-9+]"))
            {

                char[] chars = monoFlat_TextBox2.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The Payment must be in digit format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox2.Text = charsToStr.Remove(charsToStr.Length - 1);


            }
        }

        private void monoFlat_TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox2.Text, "[^0-9+]"))
            {

                char[] chars = monoFlat_TextBox2.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The Payment must be in digit format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox2.Text = charsToStr.Remove(charsToStr.Length - 1);


            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked) {

                monoFlat_TextBox4.Visible = false;
                label11.Visible = false;

            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked) {

                monoFlat_TextBox4.Visible = true;
                label11.Visible = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) {


                monoFlat_TextBox1.Visible = true;
                label3.Visible = true;

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton2.Checked) {


                monoFlat_TextBox1.Visible = false;
                label3.Visible = false;

            }
               
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
           
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

            label12.Text = numericUpDown1.Value.ToString() + "%";
        }

        private void monoFlat_TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox2.Text, "[^0-9+]"))
            {

                char[] chars = monoFlat_TextBox2.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The Payment must be in digit format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox2.Text = charsToStr.Remove(charsToStr.Length - 1);


            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            if (numericUpDown1.Text == "")
            {

                label12.Text = "0%";

            }
            else
            {

                label12.Text = numericUpDown1.Value.ToString() + "%";

            }
        }
    }
}
