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
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace Car_Renting_Management_System
{
    public partial class CustomerEditForm : Form
    {

        CURDQueryClass cu = new CURDQueryClass();
        string val="";
        string carID = "";
         
        public CustomerEditForm(string id,string carId)
        {
            InitializeComponent();
            val = id;
            carID = carId;
         

        }

        int canMove;
        int xCor;
        int yCor;

        double paidAmount = 0;
        double blanceAmount = 0;
        int perDayKm = 0;
        string refund = "no";
        double refundAmount = 0;

        string driverAmount = "";

        string discountAmount = "";
        string discountPer = "";


        private void CustomerEditForm_Load(object sender, EventArgs e)
        {
            string perday = "";

            string sqlCodecar = "SELECT carDailyRate FROM car WHERE carId='" + carID + "'";
            MySqlDataReader d = cu.SelectQueryOutMsg(sqlCodecar);

            while (d.Read()) {


                perday = d.GetString(0);
            }

           

            string sqlCode = "SELECT * FROM booking WHERE orderId='" + val + "'";
            MySqlDataReader info = cu.SelectQueryOutMsg(sqlCode);

            string basisI = "";

            string mile = "";
            

            while (info.Read()) {

                bunifuCustomTextbox1.Text = info.GetString("orderId");
                bunifuCustomTextbox2.Text = info.GetString("carId");
                bunifuCustomTextbox3.Text = info.GetString("cusId");
                bunifuDatepicker1.Value = DateTime.Parse(info.GetString("rentedDate"));
                label17.Text = info.GetString("returnDate");              
                bunifuCustomTextbox4.Text = info.GetString("duration");
                bunifuCustomTextbox5.Text = perday;
                bunifuCustomTextbox6.Text = info.GetString("TotalAmount");
                label15.Text = "Paid Rs: " + info.GetDouble("advancedAmount");
                paidAmount = info.GetDouble("advancedAmount");
                bunifuCustomTextbox8.Text = info.GetString("blance");
                blanceAmount = info.GetDouble("blance");
                mile= bunifuCustomTextbox9.Text = info.GetString("totalMileage");
                basisI = info.GetString("basis");
                driverAmount = info.GetString("driverAmount");

            }
            
            if (driverAmount == "0")
            {

                bunifuCustomTextbox10.Text = "No-Driver";
                bunifuCustomTextbox10.Enabled = false;
            }
            else {

                bunifuCustomTextbox10.Text = driverAmount;
                bunifuCustomTextbox10.Enabled = true;

            }

            if (basisI == "Monthly")
            {

                radioButton2.Checked = true;
                bunifuCustomTextbox4.Text = info.GetString("duration");
                bunifuCustomTextbox7.Text = "0";
                bunifuCustomTextbox8.Text = info.GetString("blance");
                bunifuCustomTextbox9.Text = mile;
                

            }
            else if (basisI == "Daily") {

                radioButton1.Checked = true;
                bunifuCustomTextbox7.Text = "0";

            }

            if (driverAmount == "0")
            {


                checkBox1.Visible = false;

            }
            else
            {


                checkBox1.Visible = true;
            }

            string DiscountQu = "SELECT DiscountAmount,DiscountPer FROM booking WHERE orderId='" + val + "'";
            MySqlDataReader dataS = cu.SelectQueryOutMsg(DiscountQu);

            while (dataS.Read())
            {

                discountAmount = dataS.GetString(0);
                discountPer = dataS.GetString(1);

            }

            if (discountAmount != "0")
            {


                label21.Text = "Rs: " + discountAmount + "(" + discountPer + ") Offed";
            }
            else
            {

                label21.Visible = false;

            }

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path + "\\CRMS\\paymentSettings.xml");
            perDayKm = Convert.ToInt32(xmlDoc.SelectSingleNode("paymentSettings/others/AdAmountPer").InnerText);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
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
            xCor = e.X;
            yCor =e.Y;
            
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - xCor, MousePosition.Y - yCor);
            }
        }

        private void bunifuCustomTextbox2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;

            string sqlcode = "SELECT carDailyRate FROM car WHERE carId='" + carID + "'";
            MySqlDataReader d = cu.SelectQueryOutMsg(sqlcode);

            while (d.Read()) {

                bunifuCustomTextbox5.Text = d.GetString(0);
            }

            label8.Text = "Days";
            label10.Text = "Per Day Charge";
            bunifuCustomTextbox4.Text = "";
            bunifuCustomTextbox8.Text = "";
            bunifuCustomTextbox7.Text = "";
           // bunifuCustomTextbox6.Text = "";
            bunifuCustomTextbox9.Text = "";

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;

            string sqlcode = "SELECT carMonthlyRate FROM car WHERE carId='" + carID + "'";
            MySqlDataReader d = cu.SelectQueryOutMsg(sqlcode);

            while (d.Read())
            {

                bunifuCustomTextbox5.Text = d.GetString(0);
            }


            label8.Text = "Months";
            label10.Text = "Per Month Charge";
            bunifuCustomTextbox4.Text = "";
            bunifuCustomTextbox8.Text = "";
            bunifuCustomTextbox7.Text = "";
            //bunifuCustomTextbox6.Text = "";
            bunifuCustomTextbox9.Text = "";
        }


        private void bunifuCustomTextbox4_TextChanged(object sender, EventArgs e)
        {
           
            try
            {

                if (System.Text.RegularExpressions.Regex.IsMatch(bunifuCustomTextbox4.Text, "[^0-9]"))
                {
                    char[] chars = bunifuCustomTextbox4.Text.ToCharArray();
                    string charsToStr = new string(chars);

                    ErrorMsgBox er = new ErrorMsgBox("The duration must be in digit format.\nLetters and symbols are not allowed.");
                    er.Show();
                    bunifuCustomTextbox4.Text = charsToStr.Remove(charsToStr.Length - 1);

                }
                else
                {

                    if (bunifuCustomTextbox4.Text == string.Empty)
                    {

                       

                    }
                    else
                    {

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


                        int Datedays = Convert.ToInt32(bunifuCustomTextbox4.Text);
                        double perC = Convert.ToDouble(bunifuCustomTextbox5.Text);


                        if (radioButton1.Checked)
                        {

                            var date = bunifuDatepicker1.Value.AddDays(Datedays).ToString("yyyy-MM-dd");
                            label17.Text = date.ToString();

                        }
                        else if (radioButton2.Checked)
                        {
                            var date = bunifuDatepicker1.Value.AddMonths(Datedays).ToString("yyyy-MM-dd");
                            label17.Text = date.ToString();

                        }


                        if (paymentMethod == "both")
                        {
                          
                            if (radioButton1.Checked && driverAmount != "0")
                            {

                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perAmount = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                double totalAmount = duration * perAmount;

                                double totalDriversalary = duration * daySalary;

                                bunifuCustomTextbox10.Text = totalDriversalary.ToString();

                                double fullTotal = 0;

                                if(discountAmount=="0")
                                {

                                    fullTotal = totalDriversalary + totalAmount;

                                }
                                else if (discountAmount != "0")
                                {

                                    fullTotal = (totalDriversalary + totalAmount) - Convert.ToDouble(discountAmount);

                                }

                                if (fullTotal < 0)
                                {

                                    bunifuCustomTextbox6.Text = "0";

                                }
                                else 
                                {

                                    bunifuCustomTextbox6.Text = fullTotal.ToString();

                                }

                                double balanceAmount = fullTotal - paidAmount;

                                bunifuCustomTextbox8.Text = balanceAmount.ToString();

                                if (KmType == "both" || KmType == "onlyDay")
                                {

                                    int AllowedKm = (duration * KmDay);

                                    bunifuCustomTextbox9.Text = AllowedKm.ToString();

                                }

                                if (balanceAmount < 0)
                                {

                                    bunifuCustomTextbox8.Text = "0";
                                    refund = "yes";
                                    double myNegInt = System.Math.Abs(balanceAmount);
                                    label18.ForeColor = System.Drawing.Color.Red;
                                    label18.Text = "Refund Rs:" + myNegInt;
                                    refundAmount = myNegInt;

                                }
                                else if (balanceAmount >= 0)
                                {

                                    refund = "no";
                                    label18.ForeColor = System.Drawing.Color.LightGreen;

                                }
                                
                            }
                            else if (radioButton2.Checked && driverAmount != "0")
                            {

                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perAMount = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                double totalAmount = duration * perAMount;

                                double totalDriverSalary = duration * monthSalary;

                                bunifuCustomTextbox10.Text = totalDriverSalary.ToString();


                                double totalFull = 0;

                                if (discountAmount == "0")
                                {

                                    totalFull = totalAmount + totalDriverSalary;
                                  
                                }
                                else if (discountAmount != "0")
                                {

                                    totalFull = (totalAmount + totalDriverSalary) - Convert.ToDouble(discountAmount);
                                    
                                }

                                if (totalFull < 0)
                                {

                                    bunifuCustomTextbox6.Text = "0";

                                }
                                else
                                {

                                    bunifuCustomTextbox6.Text = totalFull.ToString();

                                }
                               
                                double balanceAmount = totalFull - paidAmount;
                                bunifuCustomTextbox8.Text = balanceAmount.ToString();

                                if (KmType == "both")
                                {
                                    int AllowedKm = (duration * KmMonth);

                                    bunifuCustomTextbox9.Text = AllowedKm.ToString();

                                }
                                else if (KmType == "onlyDay")
                                {


                                    DateTime date1 = Convert.ToDateTime(bunifuDatepicker1.Value.ToString("yyyy-MM-dd"));
                                    DateTime date2 = Convert.ToDateTime(label17.Text);

                                    int daysFull = (date2 - date1).Days;


                                    int AllowedKm = (daysFull * KmDay);


                                    bunifuCustomTextbox9.Text = AllowedKm.ToString();

                                }

                                if (balanceAmount < 0)
                                {
                                    bunifuCustomTextbox8.Text = "0";
                                    refund = "yes";
                                    double myNegInt = System.Math.Abs(balanceAmount);
                                    label18.ForeColor = System.Drawing.Color.Red;
                                    label18.Text = "Refund Rs:" + myNegInt;
                                    refundAmount = myNegInt;

                                }
                                else if (balanceAmount >= 0)
                                {


                                    refund = "no";
                                    label18.ForeColor = System.Drawing.Color.LightGreen;

                                }


                            }
                            else if (radioButton1.Checked && driverAmount == "0")
                            {
                               
                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perCharge = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                double totalAmount = 0;
                               

                                if (discountAmount == "0")
                                {

                                    totalAmount = duration * perCharge;

                                }
                                else if (discountAmount != "0")
                                {

                                    totalAmount = (duration * perCharge) - Convert.ToDouble(discountAmount);

                                }

                                if (totalAmount < 0)
                                {

                                    bunifuCustomTextbox6.Text = "0";
                                }
                                else
                                {

                                    bunifuCustomTextbox6.Text = totalAmount.ToString();
                                    
                                }


                                double balanceAmount = totalAmount - paidAmount;

                                bunifuCustomTextbox8.Text = balanceAmount.ToString();

                                if (KmType == "both" || KmType == "onlyDay")
                                {
                                    int AllowedKm = (duration * KmDay);

                                    bunifuCustomTextbox9.Text = AllowedKm.ToString();

                                }


                                if (balanceAmount < 0)
                                {

                                    bunifuCustomTextbox8.Text = "0";
                                    refund = "yes";
                                    double myNegInt =System.Math.Abs(balanceAmount);
                                    
                                    label18.ForeColor = System.Drawing.Color.Red;
                                    label18.Text = "Refund Rs:" + myNegInt;
                                    refundAmount = myNegInt;

                                }
                                else if (balanceAmount >= 0)
                                {

                                    refund = "no";
                                    label18.ForeColor = System.Drawing.Color.LightGreen;

                                }


                            }
                            else if (radioButton2.Checked && driverAmount == "0")
                            {


                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perCharge = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                double total = 0;

                                if (discountAmount == "0")
                                {

                                    total = duration * perCharge;

                                }
                                else if (discountAmount != "0")
                                {

                                    total = (duration * perCharge) - Convert.ToDouble(discountAmount);

                                }

                                if (total < 0)
                                {

                                    bunifuCustomTextbox6.Text = "";
                                }
                                else 
                                {

                                    bunifuCustomTextbox6.Text = total.ToString();
                                }

                                double balanceAmount = total - paidAmount;

                                bunifuCustomTextbox8.Text = balanceAmount.ToString();

                                if (KmType == "both")
                                {
                                    
                                    int AllowedKm = (duration * KmMonth);

                                    bunifuCustomTextbox9.Text = AllowedKm.ToString();

                                }
                                else if (KmType == "onlyDay")
                                {

                                    DateTime date1 = Convert.ToDateTime(bunifuDatepicker1.Value.ToString("yyyy-MM-dd"));
                                    DateTime date2 = Convert.ToDateTime(label17.Text);

                                    int daysFull = (date2 - date1).Days;


                                    int AllowedKm = (daysFull * KmDay);


                                    bunifuCustomTextbox9.Text = AllowedKm.ToString();


                                }

                                if (balanceAmount < 0)
                                {

                                    bunifuCustomTextbox8.Text = "0";
                                    refund = "yes";
                                    double myNegInt = System.Math.Abs(balanceAmount);
                                    label18.ForeColor = System.Drawing.Color.Red;
                                    label18.Text = "Refund Rs:" + myNegInt;
                                    refundAmount = myNegInt;

                                }
                                else if (balanceAmount >= 0)
                                {

                                    refund = "no";
                                    label18.ForeColor = System.Drawing.Color.LightGreen;

                                }

                            }

                        }
                        else if (paymentMethod == "onlyDaily") {


                            if (radioButton1.Checked && driverAmount != "0")
                            {

                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perAmount = Convert.ToInt32(bunifuCustomTextbox5.Text);

                                double driverSalary = daySalary * duration;
                                bunifuCustomTextbox10.Text = driverSalary.ToString();

                                double totalAmount = duration * perAmount;

                                double total = 0;

                                if (discountAmount == "0")
                                {

                                    total = driverSalary + totalAmount;

                                }
                                else if (discountAmount != "0")
                                {

                                    total = (driverSalary + totalAmount) - Convert.ToDouble(discountAmount);

                                }

                                double balanceAmount = total - paidAmount;

                                if (total < 0)
                                {

                                    bunifuCustomTextbox6.Text = "0";

                                }
                                else
                                {

                                    bunifuCustomTextbox6.Text = total.ToString();

                                }
                                bunifuCustomTextbox8.Text = balanceAmount.ToString();

                                
                                if (KmType == "both" || KmType == "onlyDay")
                                {

                                    int AllowedKm = (duration * KmDay);

                                    bunifuCustomTextbox9.Text = AllowedKm.ToString();

                                }

                                if (balanceAmount < 0)
                                {

                                    bunifuCustomTextbox8.Text = "0";
                                    refund = "yes";
                                    double myNegInt = System.Math.Abs(balanceAmount);
                                    label18.ForeColor = System.Drawing.Color.Red;
                                    label18.Text = "Refund Rs:" + myNegInt;
                                    refundAmount = myNegInt;

                                }
                                else if (balanceAmount >= 0)
                                {

                                    refund = "no";
                                    label18.ForeColor = System.Drawing.Color.LightGreen;

                                }
                                
                            }
                            else if (radioButton2.Checked && driverAmount != "0") {

                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perA = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                DateTime date1 = Convert.ToDateTime(bunifuDatepicker1.Value.ToString("yyyy-MM-dd"));
                                DateTime date2 = Convert.ToDateTime(label17.Text);

                                int daysFull = (date2 - date1).Days;

                                double driverSalary = daySalary * daysFull;

                                bunifuCustomTextbox10.Text = driverSalary.ToString();

                                double totalAmount = duration * perA;

                                double total = 0;

                                if (discountAmount == "0")
                                {

                                    total = totalAmount + driverSalary;

                                }
                                else if (discountAmount != "0")
                                {

                                    total = (totalAmount + driverSalary) - Convert.ToDouble(discountAmount);

                                }

                                double balanceAmount = total - paidAmount;

                                if (total < 0)
                                {

                                    bunifuCustomTextbox6.Text = "0";
                                }
                                else {

                                    bunifuCustomTextbox6.Text = total.ToString();

                                }

                                bunifuCustomTextbox8.Text = balanceAmount.ToString();
                                

                                if (KmType == "both")
                                {
                                    int AllowedKm = (duration * KmMonth);

                                    bunifuCustomTextbox9.Text = AllowedKm.ToString();

                                }
                                else if (KmType == "onlyDay")
                                {

                                    DateTime date1For = Convert.ToDateTime(bunifuDatepicker1.Value.ToString("yyyy-MM-dd"));
                                    DateTime date2For = Convert.ToDateTime(label17.Text);

                                    int daysFullFor = (date2For - date1For).Days;


                                    int AllowedKm = (daysFullFor * KmDay);


                                    bunifuCustomTextbox9.Text = AllowedKm.ToString();


                                }

                                if (balanceAmount < 0)
                                {

                                    bunifuCustomTextbox8.Text = "0";
                                    refund = "yes";
                                    double myNegInt = System.Math.Abs(balanceAmount);
                                    label18.ForeColor = System.Drawing.Color.Red;
                                    label18.Text = "Refund Rs:" + myNegInt;
                                    refundAmount = myNegInt;

                                }
                                else if (balanceAmount >= 0)
                                {

                                    refund = "no";
                                    label18.ForeColor = System.Drawing.Color.LightGreen;

                                }


                            }
                            else if (radioButton1.Checked && driverAmount == "0")
                            {

                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perCharge = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                double totalAmount = 0;

                                if (discountAmount == "0")
                                {

                                    totalAmount = duration * perCharge;

                                }
                                else if (discountAmount != "0")
                                {

                                    totalAmount = (duration * perCharge) - Convert.ToDouble(discountAmount);

                                }

                                if (totalAmount < 0)
                                {


                                    bunifuCustomTextbox6.Text = "0";
                                }
                                else {

                                    bunifuCustomTextbox6.Text = totalAmount.ToString();

                                }


                                double balanceAmount = totalAmount - paidAmount;

                                bunifuCustomTextbox8.Text = balanceAmount.ToString();

                                if (KmType == "both" || KmType == "onlyDay")
                                {
                                    int AllowedKm = (duration * KmDay);

                                    bunifuCustomTextbox9.Text = AllowedKm.ToString();

                                }


                                if (balanceAmount < 0)
                                {

                                    bunifuCustomTextbox8.Text = "0";
                                    refund = "yes";
                                    double myNegInt = System.Math.Abs(balanceAmount);
                                    label18.ForeColor = System.Drawing.Color.Red;
                                    label18.Text = "Refund Rs:" + myNegInt;
                                    refundAmount = myNegInt;

                                }
                                else if (balanceAmount >= 0)
                                {

                                    refund = "no";
                                    label18.ForeColor = System.Drawing.Color.LightGreen;

                                }


                            }
                            else if (radioButton2.Checked && driverAmount == "0")
                            {


                                int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                                double perCharge = Convert.ToDouble(bunifuCustomTextbox5.Text);

                                double total = 0;

                                if (discountAmount == "0")
                                {

                                    total = duration * perCharge;

                                }
                                else if (discountAmount != "0")
                                {

                                    total = (duration * perCharge) - Convert.ToDouble(discountAmount);

                                }

                                if (total < 0)
                                {

                                    bunifuCustomTextbox6.Text = "0";
                                }
                                else 
                                {

                                    bunifuCustomTextbox6.Text = total.ToString();

                                }
                                double balanceAmount = total - paidAmount;

                                bunifuCustomTextbox8.Text = balanceAmount.ToString();

                                if (KmType == "both")
                                {


                                    int AllowedKm = (duration * KmMonth);

                                    bunifuCustomTextbox9.Text = AllowedKm.ToString();

                                }
                                else if (KmType == "onlyDay")
                                {

                                    DateTime date1 = Convert.ToDateTime(bunifuDatepicker1.Value.ToString("yyyy-MM-dd"));
                                    DateTime date2 = Convert.ToDateTime(label17.Text);

                                    int daysFull = (date2 - date1).Days;


                                    int AllowedKm = (daysFull * KmDay);


                                    bunifuCustomTextbox9.Text = AllowedKm.ToString();


                                }

                                if (balanceAmount < 0)
                                {

                                    bunifuCustomTextbox8.Text = "0";
                                    refund = "yes";
                                    double myNegInt = System.Math.Abs(balanceAmount);
                                    label18.ForeColor = System.Drawing.Color.Red;
                                    label18.Text = "Refund Rs:" + myNegInt;
                                    refundAmount = myNegInt;

                                }
                                else if (balanceAmount >= 0)
                                {

                                    refund = "no";
                                    label18.ForeColor = System.Drawing.Color.LightGreen;

                                }

                            }

                        }
                        
                    }

                }
            }
            catch (Exception)
            {


            }
        
        }
        

        private void bunifuCustomTextbox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
            if (bunifuCustomTextbox7.Text != string.Empty)
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
                else
                {
                    double totalAmount = Convert.ToDouble(bunifuCustomTextbox6.Text);
                    double advancedA = Convert.ToDouble(bunifuCustomTextbox7.Text);
                    if (advancedA > totalAmount)
                    {

                        ErrorMsgBox er = new ErrorMsgBox("The advanced amount must be less than or equal to the total amount.\n (Rs:" + totalAmount + ")");
                        er.Show();

                        bunifuCustomTextbox7.Text = totalAmount.ToString();
                    }
                    else
                    {
                        if (bunifuCustomTextbox6.Text != "")
                        {

                            double totalA = Convert.ToDouble(bunifuCustomTextbox6.Text);

                            double advancedType = Convert.ToDouble(bunifuCustomTextbox7.Text);
                            double a = advancedType + paidAmount;

                            double total = Convert.ToDouble(bunifuCustomTextbox6.Text);

                            double final = total - a;

                            bunifuCustomTextbox8.Text = final.ToString();
                        }
                    }

                }
            }
            else{

                label15.Text = "Paid Rs: "+paidAmount.ToString();
                
            }
            }
            catch (Exception)
            {

                ErrorMsgBox er = new ErrorMsgBox("Something went wrong.");
                er.Show();
            }

        }

        private void bunifuCustomTextbox7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomTextbox4_Click(object sender, EventArgs e)
        {
            bunifuCustomTextbox7.Text = "0";
        }

        double final2 = 0;

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path + "\\CRMS\\Paymentsettings.xml");

            double percentTange = Convert.ToDouble(xmlDoc.SelectSingleNode("paymentSettings/others/AdAmountPer").InnerText);

            string resultString = Regex.Match(label18.Text, @"\d+").Value;
            string mustPay = resultString;
            
            if (Convert.ToDouble(bunifuCustomTextbox7.Text) >= final2)
            {

                try
                {
                    string basis = "";

                    if (radioButton1.Checked)
                    {

                        basis = "Daily";
                    }
                    else if (radioButton2.Checked)
                    {

                        basis = "Monthly";
                    }

                    if (radioButton2.Checked)
                    {

                        double monthRate = 0;

                        string query = "SELECT carMonthlyRate FROM car WHERE carId='" + bunifuCustomTextbox2.Text + "'";
                        MySqlDataReader data = cu.SelectQueryOutMsg(query);

                        while (data.Read())
                        {

                            monthRate = data.GetDouble(0);

                        }

                        string upateRe = "UPDATE booking SET perAmount='" + monthRate + "' WHERE orderId='" + bunifuCustomTextbox1.Text + "'";
                        cu.CUDOutMsg(upateRe);


                        string upateRe2 = "UPDATE booking SET totalMileage='" + bunifuCustomTextbox9.Text + "' WHERE orderId='" + bunifuCustomTextbox1.Text + "'";
                        cu.CUDOutMsg(upateRe2);

                        if (checkBox1.Checked)
                        {
                            string updateDriverAm = "UPDATE booking SET driverAmount='0' WHERE orderId='" + bunifuCustomTextbox1.Text + "'";
                            cu.CUDOutMsg(updateDriverAm);
                        }


                    }


                    if (checkBox1.Checked) {


                        string upateRe = "UPDATE booking SET driverId='No-Driver' WHERE orderId='" + bunifuCustomTextbox1.Text + "'";
                        cu.CUDOutMsg(upateRe);

                        string upateRe2 = "UPDATE booking SET driverAmount='0' WHERE orderId='" + bunifuCustomTextbox1.Text + "'";
                        cu.CUDOutMsg(upateRe2);
                    }

                    double advanced = Convert.ToDouble(bunifuCustomTextbox7.Text);
                    double advanA = advanced + paidAmount;

                    
                    if (refund == "yes")
                    {

                        double paidA = 0;

                        string querySE = "SELECT advancedAmount FROM booking WHERE orderId='" + bunifuCustomTextbox1.Text + "'";
                        MySqlDataReader dataA = cu.SelectQueryOutMsg(querySE);

                        while (dataA.Read())
                        {

                            paidA = dataA.GetDouble(0);

                        }

                        double paidLast = paidA - refundAmount;

                  

                        string sqlcode = "UPDATE booking SET basis='" + basis + "',returnDate='" + label17.Text + "',duration='" + bunifuCustomTextbox4.Text + "',TotalAmount='" + bunifuCustomTextbox6.Text + "',driverAmount='" + bunifuCustomTextbox10.Text + "',advancedAmount='" + paidLast + "',blance='" + bunifuCustomTextbox8.Text + "',totalMileage='" + bunifuCustomTextbox9.Text + "' WHERE orderId='" + val + "'";
                        cu.CUDOutMsg(sqlcode);


                    }
                    else {

                        string sqlcode = "UPDATE booking SET basis='" + basis + "',returnDate='" + label17.Text + "',duration='" + bunifuCustomTextbox4.Text + "',TotalAmount='" + bunifuCustomTextbox6.Text + "',driverAmount='" + bunifuCustomTextbox10.Text + "',advancedAmount='" + advanA + "',blance='" + bunifuCustomTextbox8.Text + "',totalMileage='" + bunifuCustomTextbox9.Text + "' WHERE orderId='" + val + "'";
                        cu.CUDOutMsg(sqlcode);

                    }


                    if (refund == "yes")
                    {
                        PrintLogDetails();
                        bookPrintStage p2 = new bookPrintStage(bunifuCustomTextbox1.Text,bunifuCustomTextbox3.Text,bunifuCustomTextbox2.Text,"update","yes",refundAmount.ToString());
                        p2.ShowDialog();

                    }
                    else if (refund == "no") {

                        PrintLogDetails();
                        bookPrintStage p = new bookPrintStage(bunifuCustomTextbox1.Text,bunifuCustomTextbox3.Text,bunifuCustomTextbox2.Text,"update");
                        p.ShowDialog();

                    }

                   

                    this.Hide();


                }

                catch (Exception ex)
                {

                }
            }
            else
            {

                BookErrMsgBox er = new BookErrMsgBox(percentTange + "'% of advance amount Rs:(" + final2.ToString() + ") must be paid from the total amount.\nBut more than Rs:" + final2.ToString() + " is also acceptable.");
                er.ShowDialog();
                bunifuCustomTextbox7.Text = final2.ToString();


            }
        }


        public void PrintLogDetails() {


            string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string txt = "Some changes were made in Order ID " + bunifuCustomTextbox1.Text + " on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString();
            File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

        }

        private void bunifuCustomTextbox7_Click_1(object sender, EventArgs e)
        {
          
            

        }

        private void bunifuCustomTextbox6_Click(object sender, EventArgs e)
        {
           
        }

      
        private void bunifuCustomTextbox6_TextChanged(object sender, EventArgs e)
        {

            if (bunifuCustomTextbox6.Text != "")
            {

                string sqlCode = "SELECT TotalAmount FROM booking WHERE orderId='" + val + "'";
                MySqlDataReader data = cu.SelectQueryOutMsg(sqlCode);

                double OldTotal = 0;

                while (data.Read())
                {

                    OldTotal = data.GetDouble(0);

                }



                double totalAmoount = Convert.ToDouble(bunifuCustomTextbox6.Text);

                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path + "\\CRMS\\paymentSettings.xml");

                double percentTange = Convert.ToDouble(xmlDoc.SelectSingleNode("paymentSettings/others/AdAmountPer").InnerText);

                final2 = (totalAmoount - OldTotal) * percentTange / 100;

                label18.Text = "Ad.Pay " + final2.ToString();
            }

            if (final2 < 0)
            {

                label18.Text = "Ad.Pay 0";
            }
        }

        private void CustomerEditForm_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void bunifuCustomTextbox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - 275, MousePosition.Y - 17);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            xCor = e.X;
            yCor = e.Y;

        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
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
          
            if (checkBox1.Checked)
            {
                if (radioButton1.Checked)
                {

                    if (paymentMethod == "both" || paymentMethod == "onlyDaily")
                    {
                        bunifuCustomTextbox10.Text = "0";

                        int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                        double driverVal = duration * daySalary;

                        double totalAmount = Convert.ToDouble(bunifuCustomTextbox6.Text);


                        double balanceTotal = totalAmount - driverVal;

                        bunifuCustomTextbox6.Text = balanceTotal.ToString();

                        double lastTotal = balanceTotal - paidAmount;

                        double balanceAmount =Convert.ToDouble(bunifuCustomTextbox8.Text = lastTotal.ToString());
                        
                        if (balanceAmount < 0)
                        {
                            bunifuCustomTextbox8.Text = "0";
                            refund = "yes";
                            double myNegInt = System.Math.Abs(balanceAmount);
                            label18.ForeColor = System.Drawing.Color.Red;
                            label18.Text = "Refund Rs:" + myNegInt;
                            refundAmount = myNegInt;

                        }
                        else if (balanceAmount >= 0)
                        {

                            refund = "no";
                            label18.ForeColor = System.Drawing.Color.LightGreen;

                        }

                    }

                }
                else if (radioButton2.Checked) {

                    if (paymentMethod == "both")
                    {

                        bunifuCustomTextbox10.Text = "0";

                        int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                        double driverVal = duration * monthSalary;
                        
                        double totalAmount = Convert.ToDouble(bunifuCustomTextbox6.Text);
                        
                        double balanceTotal = totalAmount - driverVal;

                        bunifuCustomTextbox6.Text = balanceTotal.ToString();

                       double lastTotal = balanceTotal - paidAmount;

                       double balanceAmount=Convert.ToDouble(bunifuCustomTextbox8.Text = lastTotal.ToString());

                        if (balanceAmount < 0)
                        {
                           
                            bunifuCustomTextbox8.Text = "0";
                            refund = "yes";
                            double myNegInt = System.Math.Abs(balanceAmount);
                            label18.ForeColor = System.Drawing.Color.Red;
                            label18.Text = "Refund Rs:" + myNegInt;
                            refundAmount = myNegInt;

                        }
                        else if (balanceAmount >= 0)
                        {

                            refund = "no";
                            label18.ForeColor = System.Drawing.Color.LightGreen;

                        }


                    }
                    else if (paymentMethod == "onlyDaily") {


                        DateTime date1 = Convert.ToDateTime(bunifuDatepicker1.Value.ToString("yyyy-MM-dd"));
                        DateTime date2 = Convert.ToDateTime(label17.Text);

                        int daysFull = (date2 - date1).Days;

                        bunifuCustomTextbox10.Text = "0";

                        int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);

                        double driverVal = daySalary * daysFull;
                        
                        double totalAmount = Convert.ToDouble(bunifuCustomTextbox6.Text);
                        
                        double balanceTotal = totalAmount - driverVal;
                        
                        bunifuCustomTextbox6.Text = balanceTotal.ToString();

                        double lastTotal = balanceTotal - paidAmount;

                      double balanceAmount=Convert.ToDouble(bunifuCustomTextbox8.Text = lastTotal.ToString());

                        if (balanceAmount < 0)
                        {

                            bunifuCustomTextbox8.Text = "0";
                            refund = "yes";
                            double myNegInt = System.Math.Abs(balanceAmount);
                            label18.ForeColor = System.Drawing.Color.Red;
                            label18.Text = "Refund Rs:" + myNegInt;
                            refundAmount = myNegInt;

                        }
                        else if (balanceAmount >= 0)
                        {

                            refund = "no";
                            label18.ForeColor = System.Drawing.Color.LightGreen;

                        }

                    }
                }
              
            }
            else {

                if (radioButton1.Checked)
                {
                    if (paymentMethod == "both" || paymentMethod == "onlyDaily")
                    {
                       
                        int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                        double driverVal = duration * daySalary;

                        bunifuCustomTextbox10.Text = driverVal.ToString();

                        double totalAmount = Convert.ToDouble(bunifuCustomTextbox6.Text);


                        double balanceTotal = totalAmount + driverVal;

                        bunifuCustomTextbox6.Text = balanceTotal.ToString();

                        double lastTotal = balanceTotal - paidAmount;

                        bunifuCustomTextbox8.Text = lastTotal.ToString();

                    }

                }else if (radioButton2.Checked)
                {
                    if (paymentMethod == "both")
                    {
                        

                        int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);
                        double driverVal = duration * monthSalary;

                        bunifuCustomTextbox10.Text = driverVal.ToString();

                        
                        double totalAmount = Convert.ToDouble(bunifuCustomTextbox6.Text);
                        

                        double balanceTotal = totalAmount + driverVal;

                        bunifuCustomTextbox6.Text = balanceTotal.ToString();

                        double lastTotal = balanceTotal - paidAmount;

                        bunifuCustomTextbox8.Text = lastTotal.ToString();

                    }
                    else if (paymentMethod == "onlyDaily") {

                        DateTime date1 = Convert.ToDateTime(bunifuDatepicker1.Value.ToString("yyyy-MM-dd"));
                        DateTime date2 = Convert.ToDateTime(label17.Text);

                        int daysFull = (date2 - date1).Days;

                        bunifuCustomTextbox10.Text = "0";

                        int duration = Convert.ToInt32(bunifuCustomTextbox4.Text);

                        double driverVal = daySalary * daysFull;

                        bunifuCustomTextbox10.Text = driverVal.ToString(); ;
                        
                        double totalAmount = Convert.ToDouble(bunifuCustomTextbox6.Text);
                        
                        double balanceTotal = totalAmount + driverVal;

                        bunifuCustomTextbox6.Text = balanceTotal.ToString();

                        double lastTotal = balanceTotal - paidAmount;

                        bunifuCustomTextbox8.Text = lastTotal.ToString();

                    }

                }
               
            }
        }

        private void bunifuCustomTextbox10_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
