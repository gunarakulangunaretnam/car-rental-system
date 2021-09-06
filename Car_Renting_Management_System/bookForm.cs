using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace Car_Renting_Management_System
{
    public partial class bookForm : Form
    {


        public bookForm()
        {
            InitializeComponent();

        }

        CURDQueryClass curdFunction = new CURDQueryClass();


        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void bunifuCustomTextbox2_Click(object sender, EventArgs e)
        {
            CarAvaliableForm CAF = new CarAvaliableForm(bunifuCustomTextbox2.Text);
            CAF.ShowDialog();

            if (CAF.monoFlat_TextBox1.Text != string.Empty)
            {

                bunifuCustomTextbox2.Text = CAF.monoFlat_TextBox1.Text;
                radioButton1.Checked = true;

            }

        }

        private void bunifuCustomTextbox3_Click(object sender, EventArgs e)
        {

            cusDetailsForm cusD = new cusDetailsForm(bunifuCustomTextbox3.Text);
            cusD.ShowDialog();

            if (cusD.monoFlat_TextBox1.Text != string.Empty) {

                bunifuCustomTextbox3.Text = cusD.monoFlat_TextBox1.Text;

            }
        }

        string kmmter_type = "";

        private void bunifuCustomTextbox2_TextChanged(object sender, EventArgs e)
        {
            
            string id = bunifuCustomTextbox2.Text.Trim();
            string query = "SELECT carDailyRate,carBrand,odometer,kmmeter_type FROM car WHERE carId='" + id + "'";
            MySqlDataReader data = curdFunction.SelectQuery(query);

            while (data.Read())
            {


                bunifuCustomTextbox5.Text = data.GetString(0);
                label13.Text = "Car Brand is " + data.GetString(1);
                bunifuCustomTextbox9.Text = data.GetString(2);
                kmmter_type = data.GetString(3);
               
            }

            if (kmmter_type == "Normal")
            {

                bunifuCustomTextbox9.MaxLength = 5;
            }
            else if(kmmter_type=="Digital"){

                bunifuCustomTextbox9.MaxLength = 32767;

            }

        }

        private void bunifuCustomTextbox3_TextChanged(object sender, EventArgs e)
        {
            string id = bunifuCustomTextbox3.Text.Trim();
            string query = "SELECT customerFname FROM customer WHERE customerId='" + id + "'";
            MySqlDataReader data = curdFunction.SelectQuery(query);

            while (data.Read())
            {


                label14.Text = "Customer Name is " + data.GetString(0);

            }

            AutoIdSystem();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label8.Text = "Days";
            label10.Text = "Per Day Charge";
            bunifuCustomTextbox4.Text = "";
            label15.Visible = false;
            label16.Visible = false;
            label19.Visible = false;


            bunifuCustomTextbox6.Text = "";
            bunifuCustomTextbox7.Text = "";
            bunifuCustomTextbox8.Text = "";


            string id = bunifuCustomTextbox2.Text.Trim();
            string query = "SELECT carDailyRate FROM car WHERE carId='" + id + "'";
            MySqlDataReader data = curdFunction.SelectQuery(query);

            while (data.Read())
            {


                bunifuCustomTextbox5.Text = data.GetString(0);

            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label8.Text = "Months";
            label10.Text = "Per Month Charge";
            label15.Visible = false;
            label16.Visible = false;
            label19.Visible = false;

            bunifuCustomTextbox6.Text = "";
            bunifuCustomTextbox7.Text = "";
            bunifuCustomTextbox8.Text = "";


            string id = bunifuCustomTextbox2.Text.Trim();
            string query = "SELECT carMonthlyRate FROM car WHERE carId='" + id + "'";
            MySqlDataReader data = curdFunction.SelectQuery(query);

            while (data.Read())
            {

                bunifuCustomTextbox5.Text = data.GetString(0);

            }


        }

        int perDayKm = 0;
        int perMonthKm = 3000;
        int totalKm = 0;

        private void bunifuCustomTextbox4_TextChanged(object sender, EventArgs e)
        {

            if (bunifuCustomTextbox4.Text == "" && bunifuCustomTextbox2.Text != string.Empty && bunifuCustomTextbox3.Text != string.Empty && bunifuCustomTextbox10.Text != string.Empty)
            {
                bunifuCustomTextbox11.Text = "0";
                bunifuCustomTextbox6.Text = "0";
                bunifuCustomTextbox7.Text = "0";
                bunifuCustomTextbox8.Text = "0";
                bunifuCustomTextbox7.ReadOnly = true;
                label19.Text = "0Km";

                bunifuCustomTextbox12.Visible = false;
                bunifuCustomTextbox13.Visible = false;
                label22.Visible = false;
                label23.Visible = false;

                label16.Visible = false;
                label19.Visible = false;
                label15.Visible = false;

            }
            else
            {

                if (bunifuCustomTextbox2.Text != string.Empty && bunifuCustomTextbox3.Text != string.Empty && bunifuCustomTextbox4.Text != string.Empty && bunifuCustomTextbox10.Text != string.Empty)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(bunifuCustomTextbox4.Text, "[^0-9]") && bunifuCustomTextbox4.Text != string.Empty)
                    {
                        char[] chars = bunifuCustomTextbox4.Text.ToCharArray();
                        string charsToStr = new string(chars);

                        ErrorMsgBox er = new ErrorMsgBox("The days must be in digit format.\nLetters and symbols are not allowed.");
                        er.Show();
                        bunifuCustomTextbox4.Text = charsToStr.Remove(charsToStr.Length - 1);
                    }
                    else
                    {

                        label16.Visible = true;
                        label19.Visible = true;
                        label15.Visible = true;

                        bunifuCustomTextbox7.ReadOnly = false;

                        int Datedays = Convert.ToInt32(bunifuCustomTextbox4.Text);


                        if (radioButton1.Checked)
                        {

                            var date = bunifuDatepicker1.Value.AddDays(Datedays).ToString("yyyy-MM-dd");
                            label15.Text = date.ToString();

                        }
                        else if (radioButton2.Checked)
                        {
                            var date = bunifuDatepicker1.Value.AddMonths(Datedays).ToString("yyyy-MM-dd");
                            label15.Text = date.ToString();

                        }

                        //The Path
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);


                        //Salary Method.
                        XmlDocument xmlDocForPaymentsettings = new XmlDocument();
                        xmlDocForPaymentsettings.Load(path + "\\CRMS\\Paymentsettings.xml");
                        string paymentMethod = xmlDocForPaymentsettings.SelectSingleNode("paymentSettings/DriverSettings/salaryType").InnerText;
                        //Salary Method.

                        //Day Salary.                        
                        XmlDocument xmlDocsForDriverDay = new XmlDocument();
                        xmlDocsForDriverDay.Load(path + "\\CRMS\\Paymentsettings.xml");
                        double daySalary = Convert.ToDouble(xmlDocsForDriverDay.SelectSingleNode("paymentSettings/DriverSettings/DriverDailySalary").InnerText);
                        //Day Salary.

                        //Month Salary.
                        XmlDocument xmlDocsForDriverMonth = new XmlDocument();
                        xmlDocsForDriverMonth.Load(path + "\\CRMS\\Paymentsettings.xml");
                        double monthSalary = Convert.ToDouble(xmlDocsForDriverMonth.SelectSingleNode("paymentSettings/DriverSettings/DriverMonthlySalary").InnerText);
                        //Month Salary.

                        //Advanced Percentage
                        XmlDocument xmldocForAdvancedPercentage = new XmlDocument();
                        xmldocForAdvancedPercentage.Load(path + "\\CRMS\\Paymentsettings.xml");
                        int advancedPercentage = Convert.ToInt32(xmldocForAdvancedPercentage.SelectSingleNode("paymentSettings/others/AdAmountPer").InnerText);

                        //KM Method.
                        XmlDocument xmlDocsForKmType = new XmlDocument();
                        xmlDocsForKmType.Load(path + "\\CRMS\\kmSettings.xml");
                        string KmType = xmlDocsForKmType.SelectSingleNode("kmSettings/kmType").InnerText;
                        //KM Method


                        //KM Day
                        XmlDocument xmlDocsForKmDay = new XmlDocument();
                        xmlDocsForKmDay.Load(path + "\\CRMS\\kmSettings.xml");
                        int KmDay = Convert.ToInt32(xmlDocsForKmDay.SelectSingleNode("kmSettings/perday").InnerText);
                        //KM Day

                        //KM Month
                        XmlDocument xmlDocsForKmMonth = new XmlDocument();
                        xmlDocsForKmMonth.Load(path + "\\CRMS\\kmSettings.xml");
                        int KmMonth = Convert.ToInt32(xmlDocsForKmMonth.SelectSingleNode("kmSettings/perMonth").InnerText);
                        //KM Month


                        if (paymentMethod == "both")
                        {

                            if (radioButton1.Checked && checkBox1.Checked == false)
                            {


                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perDurationCharage = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                double driverSalary = daySalary * duration;
                                bunifuCustomTextbox11.Text = driverSalary.ToString();

                                double total = duration * perDurationCharage;
                                double totalAmount = total + driverSalary;

                                bunifuCustomTextbox6.Text = totalAmount.ToString();

                                if (KmType == "both" || KmType == "onlyDay")
                                {

                                    int PercentageForAdvanced = advancedPercentage;

                                    double advancedPayment = totalAmount * PercentageForAdvanced / 100;
                                    bunifuCustomTextbox7.Text = advancedPayment.ToString();

                                    int AllowedKm = (duration * KmDay);

                                    totalKm = AllowedKm;

                                    label19.Text = AllowedKm + "KM";

                                }

                            }
                            else if (radioButton2.Checked && checkBox1.Checked == false)
                            {
                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perDurationCharge = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                double driverMonthSalary = monthSalary*duration;
                                bunifuCustomTextbox11.Text = driverMonthSalary.ToString();

                                double total = duration * perDurationCharge;
                                double totalAmount = total + driverMonthSalary;
                                bunifuCustomTextbox6.Text = totalAmount.ToString();

                                if (KmType == "both")
                                {
                                    
                                    int PercentageForAdvanced = advancedPercentage;

                                    double advancedPayment = totalAmount * PercentageForAdvanced / 100;
                                    bunifuCustomTextbox7.Text = advancedPayment.ToString();

                                    int AllowedKm = (duration * KmMonth);
                                    totalKm = AllowedKm;
                                    label19.Text = AllowedKm + "KM";

                                }
                                else if (KmType == "onlyDay")
                                {
                                    int PercentageForAdvanced = advancedPercentage;

                                    double advancedPayment = totalAmount * PercentageForAdvanced / 100;
                                    bunifuCustomTextbox7.Text = advancedPayment.ToString();

                                    DateTime date1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                    DateTime date2 = Convert.ToDateTime(label15.Text);

                                    int daysFull = (date2 - date1).Days;

                                    
                                    int AllowedKm = (daysFull*KmDay);
                                    totalKm = AllowedKm;

                                    label19.Text = AllowedKm + "KM";

                                }

                            }else if(radioButton1.Checked && checkBox1.Checked == true)
                            {

                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perDurationCharage = Convert.ToDouble(bunifuCustomTextbox5.Text);
                                
                                bunifuCustomTextbox11.Text = 0.ToString();
                                double totalAmount = duration * perDurationCharage;

                                bunifuCustomTextbox6.Text = totalAmount.ToString();

                                if (KmType == "both" || KmType == "onlyDay")
                                {

                                    int PercentageForAdvanced = advancedPercentage;

                                    double advancedPayment = totalAmount * PercentageForAdvanced / 100;
                                    bunifuCustomTextbox7.Text = advancedPayment.ToString();

                                    int AllowedKm = (duration * KmDay);

                                    totalKm = AllowedKm;

                                    label19.Text = AllowedKm + "KM";

                                }

                            }else if(radioButton2.Checked && checkBox1.Checked == true)
                            {

                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perDurationCharge = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                bunifuCustomTextbox11.Text = 0.ToString();
                                double totalAmount = duration * perDurationCharge;
                                bunifuCustomTextbox6.Text = totalAmount.ToString();

                                if (KmType == "both")
                                {

                                    int PercentageForAdvanced = advancedPercentage;

                                    double advancedPayment = totalAmount * PercentageForAdvanced / 100;
                                    bunifuCustomTextbox7.Text = advancedPayment.ToString();

                                    int AllowedKm = (duration * KmMonth);
                                    totalKm = AllowedKm;
                                    label19.Text = AllowedKm + "KM";

                                }
                                else if (KmType == "onlyDay")
                                {
                                    int PercentageForAdvanced = advancedPercentage;

                                    double advancedPayment = totalAmount * PercentageForAdvanced / 100;
                                    bunifuCustomTextbox7.Text = advancedPayment.ToString();

                                    DateTime date1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                    DateTime date2 = Convert.ToDateTime(label15.Text);

                                    int daysFull = (date2 - date1).Days;


                                    int AllowedKm = (daysFull * KmDay);
                                    totalKm = AllowedKm;

                                    label19.Text = AllowedKm + "KM";

                                }

                            }

                        }
                        else if (paymentMethod == "onlyDaily")
                        {

                            if (radioButton1.Checked && checkBox1.Checked == false)
                            {

                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perDurationCharage = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                double driverSalary = daySalary * duration;
                                bunifuCustomTextbox11.Text = driverSalary.ToString();

                                double total = duration * perDurationCharage;
                                double totalAmount = total + driverSalary;

                                bunifuCustomTextbox6.Text = totalAmount.ToString();

                                if (KmType == "both" || KmType == "onlyDay")
                                {

                                    int PercentageForAdvanced = advancedPercentage;

                                    double advancedPayment = totalAmount * PercentageForAdvanced / 100;
                                    bunifuCustomTextbox7.Text = advancedPayment.ToString();

                                    int AllowedKm = (duration * KmDay);

                                    totalKm = AllowedKm;

                                    label19.Text = AllowedKm + "KM";

                                }

                            }
                            else if (radioButton2.Checked && checkBox1.Checked == false)
                            {

                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perDurationCharge = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                DateTime dateForDri1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                DateTime dateForDri2 = Convert.ToDateTime(label15.Text);

                                int daysFullD = (dateForDri2 - dateForDri1).Days;

                                double driverMonthSalary = daySalary * daysFullD;
                                bunifuCustomTextbox11.Text = driverMonthSalary.ToString();

                                double total = duration * perDurationCharge;
                                double totalAmount = total + driverMonthSalary;
                                bunifuCustomTextbox6.Text = totalAmount.ToString();

                                if (KmType == "both")
                                {

                                    int PercentageForAdvanced = advancedPercentage;

                                    double advancedPayment = totalAmount * PercentageForAdvanced / 100;
                                    bunifuCustomTextbox7.Text = advancedPayment.ToString();

                                    int AllowedKm = (duration * KmMonth);
                                    totalKm = AllowedKm;
                                    label19.Text = AllowedKm + "KM";

                                }
                                else if (KmType == "onlyDay")
                                {
                                    int PercentageForAdvanced = advancedPercentage;

                                    double advancedPayment = totalAmount * PercentageForAdvanced / 100;
                                    bunifuCustomTextbox7.Text = advancedPayment.ToString();

                                    DateTime date1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                    DateTime date2 = Convert.ToDateTime(label15.Text);

                                    int daysFull = (date2 - date1).Days;


                                    int AllowedKm = (daysFull * KmDay);
                                    totalKm = AllowedKm;

                                    label19.Text = AllowedKm + "KM";

                                }

                            }
                            else if (radioButton1.Checked && checkBox1.Checked == true) {


                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perDurationCharage = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                bunifuCustomTextbox11.Text = 0.ToString();
                                double totalAmount = duration * perDurationCharage;

                                bunifuCustomTextbox6.Text = totalAmount.ToString();

                                if (KmType == "both" || KmType == "onlyDay")
                                {

                                    int PercentageForAdvanced = advancedPercentage;

                                    double advancedPayment = totalAmount * PercentageForAdvanced / 100;
                                    bunifuCustomTextbox7.Text = advancedPayment.ToString();

                                    int AllowedKm = (duration * KmDay);

                                    totalKm = AllowedKm;

                                    label19.Text = AllowedKm + "KM";

                                }


                            }else if(radioButton2.Checked && checkBox1.Checked == true)
                            {
                                
                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perDurationCharge = Convert.ToDouble(bunifuCustomTextbox5.Text);



                                bunifuCustomTextbox11.Text = "0";

                                double totalAmount = duration * perDurationCharge;
                                bunifuCustomTextbox6.Text = totalAmount.ToString();

                                if (KmType == "both")
                                {

                                    int PercentageForAdvanced = advancedPercentage;

                                    double advancedPayment = totalAmount * PercentageForAdvanced / 100;
                                    bunifuCustomTextbox7.Text = advancedPayment.ToString();

                                    int AllowedKm = (duration * KmMonth);
                                    totalKm = AllowedKm;
                                    label19.Text = AllowedKm + "KM";

                                }
                                else if (KmType == "onlyDay")
                                {
                                    int PercentageForAdvanced = advancedPercentage;

                                    double advancedPayment = totalAmount * PercentageForAdvanced / 100;
                                    bunifuCustomTextbox7.Text = advancedPayment.ToString();

                                    DateTime date1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                                    DateTime date2 = Convert.ToDateTime(label15.Text);

                                    int daysFull = (date2 - date1).Days;


                                    int AllowedKm = (daysFull * KmDay);
                                    totalKm = AllowedKm;

                                    label19.Text = AllowedKm + "KM";

                                }

                            }

                        }
 
                    }
                }
                else
                {
                    if (bunifuCustomTextbox2.Text == string.Empty && bunifuCustomTextbox3.Text == string.Empty && bunifuCustomTextbox4.Text != string.Empty)
                    {

                        char[] chars = bunifuCustomTextbox4.Text.ToCharArray();
                        string charsToStr = new string(chars);

                        ErrorMsgBox er = new ErrorMsgBox("01.Please Select the Customer and Car.\n02.Choose the driver options");
                        er.Show();
                        bunifuCustomTextbox4.Text = charsToStr.Remove(charsToStr.Length - 1);

                    }

                    else if (bunifuCustomTextbox3.Text == string.Empty && bunifuCustomTextbox4.Text != string.Empty)
                    {
                        char[] chars = bunifuCustomTextbox4.Text.ToCharArray();
                        string charsToStr = new string(chars);

                        ErrorMsgBox er = new ErrorMsgBox("Please Select the Customer");
                        er.Show();
                        bunifuCustomTextbox4.Text = charsToStr.Remove(charsToStr.Length - 1);
                    }
                    else if (bunifuCustomTextbox2.Text == string.Empty && bunifuCustomTextbox4.Text != string.Empty)
                    {
                        char[] chars = bunifuCustomTextbox4.Text.ToCharArray();
                        string charsToStr = new string(chars);

                        ErrorMsgBox er = new ErrorMsgBox("Please Select the Car");
                        er.Show();
                        bunifuCustomTextbox4.Text = charsToStr.Remove(charsToStr.Length - 1);

                    }
                    else if (bunifuCustomTextbox10.Text == string.Empty && bunifuCustomTextbox4.Text != string.Empty)
                    {

                        char[] chars = bunifuCustomTextbox4.Text.ToCharArray();
                        string charsToStr = new string(chars);

                        ErrorMsgBox er = new ErrorMsgBox("Please choose the driver.");
                        er.Show();
                        bunifuCustomTextbox4.Text = charsToStr.Remove(charsToStr.Length - 1);

                    }

                }

            }
        }

        private void bunifuCustomTextbox7_TextChanged(object sender, EventArgs e)
        {
            double total;
            double advanced;
            double blance;

            try
            {
                
            if (System.Text.RegularExpressions.Regex.IsMatch(bunifuCustomTextbox7.Text, "[^0-9.]"))
            {

                if (bunifuCustomTextbox7.Text != string.Empty)
                {

                    char[] chars = bunifuCustomTextbox7.Text.ToCharArray();
                    string charsToStr = new string(chars);

                    ErrorMsgBox er = new ErrorMsgBox("The advanced amount must be in digits format.\nLetters and symbols are not allowed.");
                    er.Show();
                    bunifuCustomTextbox7.Text = charsToStr.Remove(charsToStr.Length - 1);
                }
                
            }
            else {

                if (bunifuCustomTextbox6.Text != string.Empty && bunifuCustomTextbox7.Text != string.Empty)
                {

                    double totalAmount = Convert.ToDouble(bunifuCustomTextbox6.Text);
                    double advancedA = Convert.ToDouble(bunifuCustomTextbox7.Text);

                    if (advancedA > totalAmount)
                    {

                        ErrorMsgBox er = new ErrorMsgBox("The advanced amount must be less than or equal to the total amount.\n (Rs:"+totalAmount+")");
                        er.Show();

                        bunifuCustomTextbox7.Text = totalAmount.ToString();
                    }
                    else
                    {

                        total = Convert.ToDouble(bunifuCustomTextbox6.Text);
                        advanced = Convert.ToDouble(bunifuCustomTextbox7.Text);
                        blance = total - advanced;
                        bunifuCustomTextbox8.Text = blance.ToString();

                    }
                }


            }
            }
            catch (Exception)
            {

                ErrorMsgBox mb = new ErrorMsgBox("Something went wrong");
                mb.Show();
            }

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void bookForm_Load(object sender, EventArgs e)
        {
            label16.Visible = false;
            this.bunifuDatepicker1.Value = DateTime.Now;
            this.ActiveControl = radioButton1;
            //bunifuCustomTextbox7.ReadOnly = true;

            AutoIdSystem();
            getDataFromTheDB();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
         


            XmlDocument xml2 = new XmlDocument();
            xml2.Load(path + "\\CRMS\\userStatus.xml");

            string userS = xml2.SelectSingleNode("status").InnerText;

            XmlDocument xml3 = new XmlDocument();
            xml3.Load(path + "\\CRMS\\dicountSettings.xml");


            string staffAllowed = xml3.SelectSingleNode("dicountSettings/StaffAllowed").InnerText;
            string controlType = xml3.SelectSingleNode("dicountSettings/controlType").InnerText;
            string adminNeeded = xml3.SelectSingleNode("dicountSettings/adminNeeded").InnerText;



            if (userS == "Admin")
            {


                bunifuFlatButton4.Visible = true;

            }
            else if (userS == "Staff" && staffAllowed == "yes" && controlType == "full")
            {


                bunifuFlatButton4.Visible = true;

            }
            else if (userS == "Staff" && staffAllowed == "yes" && controlType == "fixed") {

                bunifuFlatButton4.Visible = true;
            }
            else
            {

                bunifuFlatButton4.Visible = false;
            }



            bunifuCustomTextbox12.Visible = false;

            bunifuCustomTextbox13.Visible = false;
            label22.Visible = false;
            label23.Visible = false;
        }


        public void AutoIdSystem() {

            int lengthOfBooking = 0;
            int lengthOfBookData = 0;
            int lengthOfDeleteData = 0;
            string orderPreSys = "";

            string sqlCode = "SELECT COUNT(*) FROM booking";
            MySqlDataReader d = curdFunction.SelectQuery(sqlCode);

            while (d.Read())
            {

                lengthOfBooking = d.GetInt32(0);

            }

            string sqlCode2 = "SELECT COUNT(*) FROM oldbookdata";
            MySqlDataReader d2 = curdFunction.SelectQuery(sqlCode2);

            while (d2.Read())
            {

                lengthOfBookData = d2.GetInt32(0);

            }

            string sqlCode3 = "SELECT deleteOrder FROM systemcontroller";


            MySqlDataReader d3 = curdFunction.SelectQueryOutMsg(sqlCode3);

            while (d3.Read())
            {

                lengthOfDeleteData = d3.GetInt32(0);

            }

            int position = (lengthOfBookData + lengthOfBooking + lengthOfDeleteData) + 1;

            if (position < 10)
            {


                orderPreSys = "ORD-00";


            }
            else if (position >= 10 && position < 100)
            {


                orderPreSys = "ORD-0";

            }
            else if (position >= 100)
            {

                orderPreSys = "ORD-";
            }


            bunifuCustomTextbox1.Text = orderPreSys + position.ToString();
            getDataFromTheDB();

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (bunifuCustomTextbox1.Text != string.Empty && bunifuCustomTextbox2.Text != string.Empty && bunifuCustomTextbox4.Text != string.Empty && (radioButton1.Checked == true || radioButton2.Checked == true) && bunifuCustomTextbox5.Text != string.Empty && bunifuCustomTextbox6.Text != string.Empty && bunifuCustomTextbox7.Text != string.Empty && bunifuCustomTextbox8.Text != string.Empty && bunifuCustomTextbox9.Text != string.Empty)
            {
                string driverInfo = "";

                string radioVal = "";

                if (radioButton1.Checked)
                {

                    radioVal = "Daily";

                }
                else if (radioButton2.Checked)
                {

                    radioVal = "Monthly";

                }

                if (bunifuCustomTextbox10.Enabled)
                {


                    driverInfo = bunifuCustomTextbox10.Text;

                }
                else {


                    driverInfo = "No-Driver";

                }

                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path + "\\CRMS\\Paymentsettings.xml");

                double percentTange = Convert.ToDouble(xmlDoc.SelectSingleNode("paymentSettings/others/AdAmountPer").InnerText);

              

                double totalVal = Convert.ToDouble(bunifuCustomTextbox6.Text);
                double mustPay = totalVal * percentTange / 100;

                if (kmmter_type == "Normal")
                {

                    if (bunifuCustomTextbox9.Text.Length == 5)
                    {

                        if (Convert.ToDouble(bunifuCustomTextbox7.Text) < mustPay)
                        {
                            BookErrMsgBox er = new BookErrMsgBox(percentTange + "'% of advance amount Rs:(" + mustPay.ToString() + ") must be paid from the total amount.\nBut more than Rs:" + mustPay.ToString() + " is also acceptable.");
                            er.ShowDialog();

                            bunifuCustomTextbox7.Text = mustPay.ToString();


                        }
                        else
                        {

                            try
                            {
                                string discountPer = bunifuCustomTextbox13.Text;
                                string disper = "";

                                if (discountPer == "")
                                {

                                    disper = "0";
                                }
                                else
                                {

                                    disper = bunifuCustomTextbox13.Text;
                                }

                                string sqlcode = "INSERT INTO booking VALUES('" + bunifuCustomTextbox1.Text + "','" + bunifuCustomTextbox2.Text + "','" + bunifuCustomTextbox3.Text + "','" + driverInfo + "','" + bunifuDatepicker1.Value.ToString("yyyy-MM-dd") + "','" + DateTime.Parse(label15.Text).ToString("yyyy-MM-dd") + "','" + radioVal + "','" + bunifuCustomTextbox4.Text + "','" + bunifuCustomTextbox6.Text + "','" + bunifuCustomTextbox11.Text + "','" + bunifuCustomTextbox7.Text + "','" + bunifuCustomTextbox8.Text + "','" + bunifuCustomTextbox9.Text + "','" + totalKm + "','" + bunifuCustomTextbox5.Text + "','" + bunifuCustomTextbox12.Text + "','" + disper + "')";
                                string sqlcode2 = "UPDATE car SET carStatus='OUT' WHERE carId='" + bunifuCustomTextbox2.Text + "'";
                                MySqlConnection myConnect = new MySqlConnection("SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=");
                                myConnect.Open();
                                MySqlCommand myCommand = new MySqlCommand(sqlcode, myConnect);
                                MySqlCommand mycom2 = new MySqlCommand(sqlcode2, myConnect);
                                mycom2.ExecuteNonQuery();
                                myCommand.ExecuteNonQuery();

                                if (driverInfo != "No-Driver")
                                {

                                    string sqlCode3 = "UPDATE driver SET driverStatus='OUT' WHERE driverId='" + driverInfo + "'";
                                    MySqlCommand mycom3 = new MySqlCommand(sqlCode3, myConnect);
                                    mycom3.ExecuteNonQuery();

                                }

                                string updateOdameter = "UPDATE car SET odometer='" + bunifuCustomTextbox9.Text + "' WHERE carid='" + bunifuCustomTextbox2.Text + "'";
                                curdFunction.CUDOutMsg(updateOdameter);

                                myConnect.Close();
                                getDataFromTheDB();


                                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                string txt = "A car was rented on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString() + " The rent ID is " + bunifuCustomTextbox1.Text;
                                File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

                                bookPrintStage bookPrint = new bookPrintStage(bunifuCustomTextbox1.Text, bunifuCustomTextbox3.Text, bunifuCustomTextbox2.Text);
                                bookPrint.ShowDialog();


                                Rearrange();

                            }
                            catch (Exception ex)
                            {
                                int lenofBooking = 0;
                                string sqlCode = "SELECT COUNT(*) FROM booking";
                                MySqlDataReader d = curdFunction.SelectQuery(sqlCode);

                                while (d.Read())
                                {

                                    lenofBooking = d.GetInt32(0);

                                }

                                bunifuCustomTextbox1.Text = (lenofBooking + 2).ToString();

                            }
                        }
                    }
                    else
                    {

                        ErrorMsgBox err = new ErrorMsgBox("The start mileage value must have five digits.");
                        err.ShowDialog();

                    }

                }
                else if (kmmter_type == "Digital") {

                    

                        if (Convert.ToDouble(bunifuCustomTextbox7.Text) < mustPay)
                        {
                            BookErrMsgBox er = new BookErrMsgBox(percentTange + "'% of advance amount Rs:(" + mustPay.ToString() + ") must be paid from the total amount.\nBut more than Rs:" + mustPay.ToString() + " is also acceptable.");
                            er.ShowDialog();

                            bunifuCustomTextbox7.Text = mustPay.ToString();


                        }
                        else
                        {

                            try
                            {
                                string discountPer = bunifuCustomTextbox13.Text;
                                string disper = "";

                                if (discountPer == "")
                                {

                                    disper = "0";
                                }
                                else
                                {

                                    disper = bunifuCustomTextbox13.Text;
                                }

                                string sqlcode = "INSERT INTO booking VALUES('" + bunifuCustomTextbox1.Text + "','" + bunifuCustomTextbox2.Text + "','" + bunifuCustomTextbox3.Text + "','" + driverInfo + "','" + bunifuDatepicker1.Value.ToString("yyyy-MM-dd") + "','" + DateTime.Parse(label15.Text).ToString("yyyy-MM-dd") + "','" + radioVal + "','" + bunifuCustomTextbox4.Text + "','" + bunifuCustomTextbox6.Text + "','" + bunifuCustomTextbox11.Text + "','" + bunifuCustomTextbox7.Text + "','" + bunifuCustomTextbox8.Text + "','" + bunifuCustomTextbox9.Text + "','" + totalKm + "','" + bunifuCustomTextbox5.Text + "','" + bunifuCustomTextbox12.Text + "','" + disper + "')";
                                string sqlcode2 = "UPDATE car SET carStatus='OUT' WHERE carId='" + bunifuCustomTextbox2.Text + "'";
                                MySqlConnection myConnect = new MySqlConnection("SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=");
                                myConnect.Open();
                                MySqlCommand myCommand = new MySqlCommand(sqlcode, myConnect);
                                MySqlCommand mycom2 = new MySqlCommand(sqlcode2, myConnect);
                                mycom2.ExecuteNonQuery();
                                myCommand.ExecuteNonQuery();

                                if (driverInfo != "No-Driver")
                                {

                                    string sqlCode3 = "UPDATE driver SET driverStatus='OUT' WHERE driverId='" + driverInfo + "'";
                                    MySqlCommand mycom3 = new MySqlCommand(sqlCode3, myConnect);
                                    mycom3.ExecuteNonQuery();

                                }

                                string updateOdameter = "UPDATE car SET odometer='" + bunifuCustomTextbox9.Text + "' WHERE carid='" + bunifuCustomTextbox2.Text + "'";
                                curdFunction.CUDOutMsg(updateOdameter);

                                myConnect.Close();
                                getDataFromTheDB();


                                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                string txt = "A car was rented on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString() + " The rent ID is " + bunifuCustomTextbox1.Text;
                                File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

                                bookPrintStage bookPrint = new bookPrintStage(bunifuCustomTextbox1.Text, bunifuCustomTextbox3.Text, bunifuCustomTextbox2.Text);
                                bookPrint.ShowDialog();


                                Rearrange();

                            }
                            catch (Exception ex)
                            {
                                int lenofBooking = 0;
                                string sqlCode = "SELECT COUNT(*) FROM booking";
                                MySqlDataReader d = curdFunction.SelectQuery(sqlCode);

                                while (d.Read())
                                {

                                    lenofBooking = d.GetInt32(0);

                                }

                                bunifuCustomTextbox1.Text = (lenofBooking + 2).ToString();

                            }
                        }
                    
                }
                
               

            }
            else
            {


                ErrorMsgBox r = new ErrorMsgBox("Please Fill all information");
                r.ShowDialog();
            }


        }

        private void bunifuCustomTextbox7_Click(object sender, EventArgs e)
        {

        }

        public void Rearrange()
        {


            bunifuCustomTextbox1.Clear();
            bunifuCustomTextbox2.Clear();
            bunifuCustomTextbox3.Clear();
            bunifuDatepicker1.Value = DateTime.Now;
            radioButton1.Checked = true;
            bunifuCustomTextbox4.Clear();
            bunifuCustomTextbox5.Clear();
            bunifuCustomTextbox6.Clear();
            bunifuCustomTextbox7.Clear();
            bunifuCustomTextbox8.Clear();
            bunifuCustomTextbox9.Clear();
            bunifuCustomTextbox10.Text = "";
            bunifuCustomTextbox11.Text = "";
            bunifuCustomTextbox12.Text = "";
            bunifuCustomTextbox13.Text = "";
            bunifuCustomTextbox11.Enabled = true;
            checkBox1.Checked = false;
            label14.Text = "";
            label21.Text = "";
            label13.Text = "";


            label15.Visible = false;
            label16.Visible = false;
            label19.Visible = false;


        }

        public void getDataFromTheDB()
        {

            DataTable data = new DataTable();
            data.Columns.Add("Order ID");
            data.Columns.Add("Car ID");
            data.Columns.Add("Customer ID");
            data.Columns.Add("Driver ID");
            data.Columns.Add("Rented Date");
            data.Columns.Add("Return Date");
            data.Columns.Add("Basis");
            data.Columns.Add("Duration");
            data.Columns.Add("Ad.Paid.Amount");
            data.Columns.Add("Blance");
            data.Columns.Add("Driver Salary");
            data.Columns.Add("Total Amount");
            data.Columns.Add("Odometer Value");
            data.Columns.Add("Total Given Mileage");
            data.Columns.Add("Discount Amount");
            data.Columns.Add("Discount Percentage");



            string querySelect = "SELECT * FROM booking";

            MySqlDataReader datas = curdFunction.SelectQuery(querySelect);



            while (datas.Read())
            {

                data.Rows.Add(datas.GetString("OrderId"), datas.GetString("carId"), datas.GetString("cusId"), datas.GetString("driverId"), datas.GetString("rentedDate"), datas.GetString("returnDate"), datas.GetString("basis"), datas.GetString("duration"), datas.GetString("advancedAmount"), datas.GetString("blance"), datas.GetString("driverAmount"),datas.GetString("totalAmount"), datas.GetString("startMileage"), datas.GetString("totalMileage"),datas.GetString("DiscountAmount"), datas.GetString("DiscountPer"));

            }

            bunifuCustomDataGrid1.DataSource = data;

        }






        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            string sqlCode = "SELECT * FROM booking WHERE orderId='" + monoFlat_TextBox8.Text + "'";


            DataTable data = new DataTable();
            data.Columns.Add("Order ID");
            data.Columns.Add("Car ID");
            data.Columns.Add("Customer ID");
            data.Columns.Add("Driver ID");
            data.Columns.Add("Rented Date");
            data.Columns.Add("Return Date");
            data.Columns.Add("Basis");
            data.Columns.Add("Duration");
            data.Columns.Add("Ad.Paid.Amount");
            data.Columns.Add("Blance");
            data.Columns.Add("Driver Salary");
            data.Columns.Add("Total Amount");
            data.Columns.Add("Odometer Value");
            data.Columns.Add("Total Given Mileage");
            data.Columns.Add("Discount Amount");
            data.Columns.Add("Discount Percentage");



            MySqlDataReader datas = curdFunction.SelectQueryOutMsg(sqlCode);



            while (datas.Read())
            {

                data.Rows.Add(datas.GetString("OrderId"), datas.GetString("carId"), datas.GetString("cusId"), datas.GetString("driverId"), datas.GetString("rentedDate"), datas.GetString("returnDate"), datas.GetString("basis"), datas.GetString("duration"), datas.GetString("advancedAmount"), datas.GetString("blance"), datas.GetString("driverAmount"), datas.GetString("totalAmount"), datas.GetString("startMileage"), datas.GetString("totalMileage"),datas.GetString("DiscountAmount"), datas.GetString("DiscountPer"));

            }

            bunifuCustomDataGrid1.DataSource = data;

        }



        private void monoFlat_TextBox8_TextChanged(object sender, EventArgs e)
        {
            if (monoFlat_TextBox8.Text == string.Empty)
            {

                getDataFromTheDB();

            }
        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (bunifuCustomDataGrid1.CurrentCell.ColumnIndex.Equals(0))
            {

                monoFlat_TextBox8.Text = bunifuCustomDataGrid1.CurrentCell.Value.ToString();

            }
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {


        }


        double advancedPaidAmount = 0;

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {

            if (monoFlat_TextBox8.Text != string.Empty)
            {
                string sqlcode = "SELECT carId FROM booking WHERE orderId='" + monoFlat_TextBox8.Text + "'";
                MySqlDataReader d = curdFunction.SelectQueryOutMsg(sqlcode);
                string carId = "";
                double advancedPaidAmount = 0;

                string sqlCode2 = "SELECT advancedAmount FROM booking WHERE orderId='" + monoFlat_TextBox8.Text + "'";
                MySqlDataReader d2 = curdFunction.SelectQueryOutMsg(sqlCode2);

                while (d2.Read()) {

                    advancedPaidAmount = d2.GetDouble(0);

                }

                while (d.Read())
                {

                    carId = d.GetString(0);
                }

                

                CustomerEditForm cus = new CustomerEditForm(monoFlat_TextBox8.Text, carId);
                cus.ShowDialog();
                getDataFromTheDB();


            }
            else {


                ErrorMsgBox er = new ErrorMsgBox("Please Select an order from the bellow list.");
                er.ShowDialog();

            }
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            Rearrange();
            AutoIdSystem();

            bunifuCustomTextbox7.ReadOnly = true;
            bunifuCustomTextbox12.Visible = false;
            bunifuCustomTextbox13.Visible = false;
            label22.Visible = false;
            label23.Visible = false;
        }

        private void bookForm_MouseHover(object sender, EventArgs e)
        {
            //  getDataFromTheDB();
        }

        private void bunifuCustomTextbox1_Click(object sender, EventArgs e)
        {
            AutoIdSystem();


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                bunifuCustomTextbox10.Text = "No-Driver";
                bunifuCustomTextbox10.Enabled = false;
                bunifuCustomTextbox11.Text = "0";
                label21.Visible = false;

                bunifuCustomTextbox4.Text = "";
                bunifuCustomTextbox11.Text = "";
                bunifuCustomTextbox6.Text = "";
                bunifuCustomTextbox7.Text = "";
                bunifuCustomTextbox8.Text = "";
                
                bunifuCustomTextbox12.Visible = false;
                bunifuCustomTextbox13.Visible = false;
                label22.Visible = false;
                label23.Visible = false;
            }
            else {

                bunifuCustomTextbox10.Text = "";
                bunifuCustomTextbox10.Enabled = true;
                bunifuCustomTextbox11.Text = "";
                label21.Text = "";
                label21.Visible = true;



            }
        }

        private void bunifuCustomTextbox10_Click(object sender, EventArgs e)
        {
            DriverSelection Dri = new DriverSelection(bunifuCustomTextbox10.Text);
            Dri.ShowDialog();

            if (Dri.monoFlat_TextBox1.Text != string.Empty)
            {

                bunifuCustomTextbox10.Text = Dri.monoFlat_TextBox1.Text;
                radioButton1.Checked = true;

            }

        }

        private void bunifuCustomTextbox10_TextChanged(object sender, EventArgs e)
        {
            string id = bunifuCustomTextbox10.Text.Trim();
            string query = "SELECT driverFname FROM driver WHERE driverId='" + id + "'";
            MySqlDataReader data = curdFunction.SelectQuery(query);

            while (data.Read())
            {

                label21.Text = "Driver name is " + data.GetString(0);


            }
            
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {


            if (bunifuCustomTextbox4.Text != string.Empty)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                XmlDocument xml2 = new XmlDocument();
                xml2.Load(path + "\\CRMS\\userStatus.xml");

                string userS = xml2.SelectSingleNode("status").InnerText;

                XmlDocument xml3 = new XmlDocument();
                xml3.Load(path + "\\CRMS\\dicountSettings.xml");



                string staffAllowed = xml3.SelectSingleNode("dicountSettings/StaffAllowed").InnerText;
                string controlType = xml3.SelectSingleNode("dicountSettings/controlType").InnerText;
                string adminNeeded = xml3.SelectSingleNode("dicountSettings/adminNeeded").InnerText;


                if (userS == "Admin")
                {

                    callTheDisMethod();
                }
                else if (userS == "Staff" && staffAllowed == "yes" && controlType == "full" && adminNeeded == "no")
                {

                    callTheDisMethod();


                }
                else if (userS == "Staff" && staffAllowed == "yes" && controlType == "full" && adminNeeded == "yes")
                {


                    AdminPassword ap = new AdminPassword(Convert.ToDouble(bunifuCustomTextbox4.Text), Convert.ToDouble(bunifuCustomTextbox5.Text),Convert.ToDouble(bunifuCustomTextbox11.Text));
                    ap.ShowDialog();



                    bunifuCustomTextbox12.Text = ap.textBox1.Text;
                    bunifuCustomTextbox13.Text = ap.textBox2.Text;

                    if (ap.textBox1.Text != string.Empty && ap.textBox1.Text != string.Empty)
                    {

                        bunifuCustomTextbox12.Visible = true;
                        bunifuCustomTextbox13.Visible = true;
                        label22.Visible = true;
                        label23.Visible = true;

                    }

                }
                else if (userS == "Staff" && staffAllowed == "yes" && controlType == "fixed" && adminNeeded == "no") {


                    DiscountFixed Do = new DiscountFixed(Convert.ToDouble(bunifuCustomTextbox4.Text), Convert.ToDouble(bunifuCustomTextbox5.Text),Convert.ToDouble(bunifuCustomTextbox11.Text));
                    Do.ShowDialog();

                    if (Do.textBox1.Text == "yes")
                    {

                        string value = Do.label9.Text;

                        string resultString = Regex.Match(value, @"\d+").Value;

                        bunifuCustomTextbox12.Text = resultString;
                        bunifuCustomTextbox13.Text = Do.label2.Text;
                        bunifuCustomTextbox12.Visible = true;
                        bunifuCustomTextbox13.Visible = true;
                        label22.Visible = true;
                        label23.Visible = true;

                    }

                }else if(userS=="Staff" && staffAllowed=="yes"  && controlType == "fixed" && adminNeeded == "yes")
                {

                    AdminPassword ap = new AdminPassword(Convert.ToDouble(bunifuCustomTextbox4.Text), Convert.ToDouble(bunifuCustomTextbox5.Text),Convert.ToDouble(bunifuCustomTextbox11.Text),"yes");
                    ap.ShowDialog();
                    

                    bunifuCustomTextbox12.Text = ap.textBox1.Text;
                    bunifuCustomTextbox13.Text = ap.textBox2.Text;

                    if (ap.textBox1.Text != string.Empty && ap.textBox1.Text != string.Empty)
                    {

                        bunifuCustomTextbox12.Visible = true;
                        bunifuCustomTextbox13.Visible = true;
                        label22.Visible = true;
                        label23.Visible = true;

                    }

                }
                
            }
            else {

                ErrorMsgBox er = new ErrorMsgBox("First fill the other options.");
                er.Show();


            }


        }

        public void callTheDisMethod() {

            DiscountOptions Do = new DiscountOptions(Convert.ToDouble(bunifuCustomTextbox4.Text), Convert.ToDouble(bunifuCustomTextbox5.Text),Convert.ToDouble(bunifuCustomTextbox11.Text));
            Do.ShowDialog();

            if (Do.textBox1.Text == "yes")
            {

                string value = Do.label9.Text;

                string resultString = Regex.Match(value, @"\d+").Value;

                bunifuCustomTextbox12.Text = resultString;
                bunifuCustomTextbox13.Text = Do.label13.Text;
                bunifuCustomTextbox12.Visible = true;
                bunifuCustomTextbox13.Visible = true;
                label22.Visible = true;
                label23.Visible = true;

            }
        }

        private void bunifuCustomTextbox12_TextChanged(object sender, EventArgs e)
        {

            if (bunifuCustomTextbox12.Text != string.Empty)
            {

                double total = (Convert.ToDouble(bunifuCustomTextbox4.Text) * Convert.ToDouble(bunifuCustomTextbox5.Text));

                string discount = bunifuCustomTextbox12.Text;

                double newTo = (total+Convert.ToDouble(bunifuCustomTextbox11.Text)) - Convert.ToDouble(discount);

                bunifuCustomTextbox6.Text = newTo.ToString();

                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path + "\\CRMS\\paymentSettings.xml");

                double percentTange = Convert.ToDouble(xmlDoc.SelectSingleNode("paymentSettings/others/AdAmountPer").InnerText);



                double mustPay = newTo * percentTange / 100;



                bunifuCustomTextbox7.Text = mustPay.ToString();

            }
        }

        private void bunifuCustomTextbox12_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuCustomTextbox6_TextChanged(object sender, EventArgs e)
        {

        }

     

        private void button1_Click_1(object sender, EventArgs e)
        {
          
        }

        private void bunifuCustomTextbox5_TextChanged(object sender, EventArgs e)
        {
            bunifuCustomTextbox4.Text = "";
            bunifuCustomTextbox11.Text = "";
            bunifuCustomTextbox6.Text = "";
            bunifuCustomTextbox7.Text = "";
            bunifuCustomTextbox8.Text = "";
           
            bunifuCustomTextbox12.Visible = false;
            bunifuCustomTextbox13.Visible = false;
            label22.Visible = false;
            label23.Visible = false;


        }

        private void bunifuFlatButton5_Click_1(object sender, EventArgs e)
        {
            if (monoFlat_TextBox8.Text != string.Empty)
            {

                string qu = "SELECT carId,cusId FROM booking WHERE orderId='" + monoFlat_TextBox8.Text + "'";
                MySqlDataReader data = curdFunction.SelectQueryOutMsg(qu);

                string carId = "";
                string cusId = "";

                while (data.Read())
                {

                    carId = data.GetString(0);
                    cusId = data.GetString(1);

                }

                bookPrintStage b = new bookPrintStage(monoFlat_TextBox8.Text, cusId, carId);
                b.ShowDialog();

            }
            else {

                ErrorMsgBox er = new ErrorMsgBox("Please select a record.");

                er.Show();
            }
        }

        private void bunifuCustomTextbox9_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(bunifuCustomTextbox9.Text, "[^0-9+]"))
            {

                char[] chars = bunifuCustomTextbox9.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The odometer must have only numbers");
                er.Show();
                bunifuCustomTextbox9.Text = charsToStr.Remove(charsToStr.Length - 1);


            }
        }
    }
}