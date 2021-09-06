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
    public partial class returning : Form
    {
        CURDQueryClass curdFun = new CURDQueryClass();

        public returning()
        {
            InitializeComponent();
          
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        double duration = 0;
        double perAmount = 0;
        double paidAmount = 0;
        double blanceA = 0;
        string basis = "";
        string driverId = "";
        string carId = "";
        string cusId = "";
        string discountAmount = "";
        string discountPer = "";
        double milage = 0;
        string driverSalary = "";
       

        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now;

        string kmmeter_type = "";
        
        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {
            string sqlCodeForSelect = "SELECT * FROM booking WHERE orderId='" + bunifuCustomTextbox1.Text + "'";
            MySqlDataReader dataForOrder= curdFun.SelectQueryOutMsg(sqlCodeForSelect);


            while (dataForOrder.Read()) {

                bunifuCustomTextbox1.Text = dataForOrder.GetString("orderId");
                bunifuDatepicker1.Value = dataForOrder.GetDateTime("rentedDate");
                bunifuCustomTextbox4.Text = dataForOrder.GetString("startMileage");
                startDate= dataForOrder.GetDateTime("rentedDate");
                endDate= dataForOrder.GetDateTime("returnDate");
                blanceA = dataForOrder.GetDouble("blance");
                bunifuCustomTextbox3.Text = blanceA.ToString();
                perAmount = dataForOrder.GetDouble("perAmount");
                paidAmount = dataForOrder.GetDouble("advancedAmount");           
                duration = dataForOrder.GetDouble("duration");
                driverId = dataForOrder.GetString("driverId");
                carId = dataForOrder.GetString("carId");
                cusId = dataForOrder.GetString("cusId");
                basis = dataForOrder.GetString("basis");
                milage = dataForOrder.GetDouble("totalMileage");
                discountAmount = dataForOrder.GetString("DiscountAmount");
                driverSalary = dataForOrder.GetString("driverAmount");
                discountPer = dataForOrder.GetString("discountPer");
                bunifuDatepicker2.Value = dataForOrder.GetDateTime("returnDate");
            }


            string selectKmMeterTypeCode = "SELECT kmmeter_type FROM car WHERE carid='" + carId + "'";
            MySqlDataReader kmType = curdFun.SelectQueryOutMsg(selectKmMeterTypeCode);

            while (kmType.Read()) {

                kmmeter_type = kmType.GetString(0);

            }

            if (kmmeter_type == "Normal")
            {

                bunifuCustomTextbox2.MaxLength = 5;

            }
            else if (kmmeter_type == "Digital") {

                bunifuCustomTextbox2.MaxLength = 32767;
            }

            bunifuCustomTextbox2.Text = "";
            bunifuCustomTextbox5.Text = "0";
            bunifuCustomTextbox8.Text = "0";
            bunifuCustomTextbox6.Text = "0";
            bunifuCustomTextbox7.Text = "0";
            label17.Visible = false;
            label19.Visible = false;
            label15.Visible = false;

            if (driverId == "No-Driver")
            {

                bunifuCustomTextbox8.Text = "No-Driver";

            }

            if (bunifuCustomTextbox1.Text != string.Empty) {


                bunifuCustomTextbox2.Enabled = true;
            }

        }
        
        double extraDays = 0;
        double extraMileage = 0;
        double extraDriverPayment = 0;

     
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (kmmeter_type == "Normal")
            {

                if (bunifuCustomTextbox2.Text.Length == 5)
                {

                    string carId = "";
                    string cusId = "";

                    string eDays = "";
                    eDays = Regex.Match(label15.Text, @"\d+").Value;


                    string carIdANDCusId = "SELECT carId,cusId FROM booking WHERE orderId='" + bunifuCustomTextbox1.Text + "'";

                    MySqlDataReader data = curdFun.SelectQueryOutMsg(carIdANDCusId);

                    while (data.Read())
                    {

                        carId = data.GetString(0);
                        cusId = data.GetString(1);

                    }

                    string basis = "SELECT basis FROM booking WHERE orderId='" + bunifuCustomTextbox1.Text + "'";
                    MySqlDataReader basisData = curdFun.SelectQueryOutMsg(basis);
                    string basisSt = "";

                    while (basisData.Read())
                    {

                        basisSt = basisData.GetString(0);

                    }

                    double totalAm = Convert.ToDouble(bunifuCustomTextbox7.Text) + paidAmount;
                    double totalDriSalary = 0;
                    if (bunifuCustomTextbox8.Text != "No-Driver")
                    {

                        totalDriSalary = Convert.ToDouble(bunifuCustomTextbox8.Text) + Convert.ToDouble(driverSalary);

                    }

                    string UploadOrderDetails = "INSERT INTO oldbookdata VALUES('" + bunifuCustomTextbox1.Text + "','" + carId + "','" + cusId + "','" + driverId + "','" + bunifuDatepicker1.Value.ToString("yyyy-MM-dd") + "','" + bunifuDatepicker2.Value.ToString("yyyy-MM-dd") + "','" + duration + "','" + totalDriSalary + "','" + paidAmount + "','" + bunifuCustomTextbox3.Text + "','" + eDays + "','" + bunifuCustomTextbox5.Text + "','" + bunifuCustomTextbox8.Text + "','" + extraMileage + "','" + bunifuCustomTextbox6.Text + "','" + totalAm + "','" + basisSt + "','" + discountAmount + "','" + discountPer + "')";
                    curdFun.CUDOutMsg(UploadOrderDetails);


                    string updateCarToIn = "UPDATE car SET carStatus='IN' WHERE carId='" + carId + "'";
                    curdFun.CUDOutMsg(updateCarToIn);

                    if (driverId != "No-Driver")
                    {

                        string updateDriveridQu = "UPDATE driver SET driverStatus='IN' WHERE driverId='" + driverId + "'";
                        curdFun.CUDOutMsg(updateDriveridQu);

                    }


                    string deleteBooking = "DELETE FROM booking WHERE orderId='" + bunifuCustomTextbox1.Text + "'";
                    curdFun.CUDOutMsg(deleteBooking);

                    string updateOdameter = "UPDATE car SET odometer='" + bunifuCustomTextbox2.Text + "' WHERE carid='" + carId + "'";
                    curdFun.CUDOutMsg(updateOdameter);

                    string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string txt = "The Order Number " + bunifuCustomTextbox1.Text + " is returned on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString();
                    File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);


                    OldPrinter bookPrint = new OldPrinter(bunifuCustomTextbox1.Text, cusId, carId);
                    bookPrint.ShowDialog();

                    bunifuCustomTextbox1.Clear();
                    bunifuCustomTextbox2.Clear();
                    bunifuCustomTextbox3.Clear();
                    bunifuCustomTextbox4.Clear();
                    bunifuCustomTextbox5.Clear();
                    bunifuCustomTextbox6.Clear();
                    bunifuCustomTextbox7.Clear();
                    bunifuCustomTextbox8.Clear();

                }
                else
                {

                    ErrorMsgBox erox = new ErrorMsgBox("The odometer value must have five digits.");
                    erox.ShowDialog();

                }
            }
            else if (kmmeter_type == "Digital") {
                
                    string carId = "";
                    string cusId = "";

                    string eDays = "";
                    eDays = Regex.Match(label15.Text, @"\d+").Value;


                    string carIdANDCusId = "SELECT carId,cusId FROM booking WHERE orderId='" + bunifuCustomTextbox1.Text + "'";

                    MySqlDataReader data = curdFun.SelectQueryOutMsg(carIdANDCusId);

                    while (data.Read())
                    {

                        carId = data.GetString(0);
                        cusId = data.GetString(1);

                    }

                    string basis = "SELECT basis FROM booking WHERE orderId='" + bunifuCustomTextbox1.Text + "'";
                    MySqlDataReader basisData = curdFun.SelectQueryOutMsg(basis);
                    string basisSt = "";

                    while (basisData.Read())
                    {

                        basisSt = basisData.GetString(0);

                    }

                    double totalAm = Convert.ToDouble(bunifuCustomTextbox7.Text) + paidAmount;
                    double totalDriSalary = 0;
                    if (bunifuCustomTextbox8.Text != "No-Driver")
                    {

                        totalDriSalary = Convert.ToDouble(bunifuCustomTextbox8.Text) + Convert.ToDouble(driverSalary);

                    }

                    string UploadOrderDetails = "INSERT INTO oldbookdata VALUES('" + bunifuCustomTextbox1.Text + "','" + carId + "','" + cusId + "','" + driverId + "','" + bunifuDatepicker1.Value.ToString("yyyy-MM-dd") + "','" + bunifuDatepicker2.Value.ToString("yyyy-MM-dd") + "','" + duration + "','" + totalDriSalary + "','" + paidAmount + "','" + bunifuCustomTextbox3.Text + "','" + eDays + "','" + bunifuCustomTextbox5.Text + "','" + bunifuCustomTextbox8.Text + "','" + extraMileage + "','" + bunifuCustomTextbox6.Text + "','" + totalAm + "','" + basisSt + "','" + discountAmount + "','" + discountPer + "')";
                    curdFun.CUDOutMsg(UploadOrderDetails);


                    string updateCarToIn = "UPDATE car SET carStatus='IN' WHERE carId='" + carId + "'";
                    curdFun.CUDOutMsg(updateCarToIn);

                    if (driverId != "No-Driver")
                    {

                        string updateDriveridQu = "UPDATE driver SET driverStatus='IN' WHERE driverId='" + driverId + "'";
                        curdFun.CUDOutMsg(updateDriveridQu);

                    }


                    string deleteBooking = "DELETE FROM booking WHERE orderId='" + bunifuCustomTextbox1.Text + "'";
                    curdFun.CUDOutMsg(deleteBooking);

                    string updateOdameter = "UPDATE car SET odometer='" + bunifuCustomTextbox2.Text + "' WHERE carid='" + carId + "'";
                    curdFun.CUDOutMsg(updateOdameter);

                    string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string txt = "The Order Number " + bunifuCustomTextbox1.Text + " is returned on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString();
                    File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);


                    OldPrinter bookPrint = new OldPrinter(bunifuCustomTextbox1.Text, cusId, carId);
                    bookPrint.ShowDialog();

                    bunifuCustomTextbox1.Clear();
                    bunifuCustomTextbox2.Clear();
                    bunifuCustomTextbox3.Clear();
                    bunifuCustomTextbox4.Clear();
                    bunifuCustomTextbox5.Clear();
                    bunifuCustomTextbox6.Clear();
                    bunifuCustomTextbox7.Clear();
                    bunifuCustomTextbox8.Clear();
                   
            }
            
        }

        double days = 0;
        double months = 0;
        double dayMonths =0;
        int a = 0;
        int lastDays = 0;
        double fulldays = 0;
        int dayss = 0;

        double extradays = 0;

        private void bunifuDatepicker2_onValueChanged(object sender, EventArgs e)
        {
            bunifuCustomTextbox2.Text = "";
            bunifuCustomTextbox7.Text = "";
            bunifuCustomTextbox6.Text = "0";
            label17.Visible = false;
            
            if (bunifuCustomTextbox1.Text != string.Empty)
            {

                if (basis == "Daily") {


                    double total = 0;
                    
                    total = duration * perAmount;
                    
                    days = (bunifuDatepicker2.Value - bunifuDatepicker1.Value).Days;

                    double total2 = days * perAmount;

                    double finalTotal = total2 - total;

                    extraDays = days - duration;

                    bunifuCustomTextbox5.Text = finalTotal.ToString();


                    if (finalTotal == 0)
                    {

                        label16.Text = "+";
                        label15.Visible = false;

                    }
                    else if (finalTotal > 0)
                    {


                        label16.Text = "+";
                        label15.Visible = true;


                    }
                    else if (finalTotal < 0)
                    {

                        label16.Text = "-";
                        label15.Visible = false;

                    }

                    
                    if (extraDays == 1)
                    {
                        label15.Visible = true;
                        label15.Text = extraDays.ToString() + " Day Extra";

                    }
                    else if (extraDays > 1)
                    {
                        label15.Visible = true;
                        label15.Text = extraDays.ToString() + " Days Extra";

                    }
                    else if (extraDays < 0)
                    {

                        label15.Visible = false;

                    }


                    if (driverId == "No-Driver")
                    {

                        bunifuCustomTextbox8.Text = "No-Driver";

                    }
                    else
                    {

                        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(path + "\\CRMS\\paymentSettings.xml");

                        double dirverDaySalary = Convert.ToDouble(xmlDoc.SelectSingleNode("paymentSettings/DriverSettings/DriverDailySalary").InnerText);

                        double totalDriverPayment = dirverDaySalary * extraDays;

                        bunifuCustomTextbox8.Text = totalDriverPayment.ToString();


                        if (extraDays == 1)
                        {
                            label19.Visible = true;
                            label19.Text = extraDays.ToString() + " Day Extra";

                        }
                        else if (extraDays > 1)
                        {
                            label19.Visible = true;
                            label19.Text = extraDays.ToString() + " Days Extra";

                        }
                        else if (extraDays <= 0)
                        {

                            label19.Visible = false;

                        }

                        if (totalDriverPayment == 0)
                        {
                            label8.Text = "+";


                        }
                        else if (totalDriverPayment < 0)
                        {

                            label8.Text = "-";


                        }
                        else if (totalDriverPayment > 0)
                        {

                            label8.Text = "+";

                        }


                    }

                }
                else if (basis == "Monthly")
                {

                    double totalDays = (endDate- startDate).Days;

                    double total = duration * perAmount;

                    
                    int year = bunifuDatepicker1.Value.Year;
                    int month = bunifuDatepicker1.Value.Month;

                    int daysS = 0;

                   
                    for (int y = 0; y < duration; y++)
                    {

                        daysS+= DateTime.DaysInMonth(year, month);
                        month++;
                        

                    }

                    days = (bunifuDatepicker2.Value - bunifuDatepicker1.Value).Days;

                    extradays = days - daysS;

                    
                    string carDaySalary = "SELECT carDailyRate FROM car WHERE carId ='" + carId + "'";
                    MySqlDataReader data = curdFun.SelectQueryOutMsg(carDaySalary);

                    double dayRate = 0;


                    while (data.Read()) {


                        dayRate = data.GetDouble(0);
                    }

                    double extraAmount = 0;

                    extraAmount = extradays * dayRate;


                    bunifuCustomTextbox5.Text = extraAmount.ToString();


                    if (extraAmount == 0)
                    {

                        label16.Text = "+";
                        label15.Visible = false;

                    }
                    else if (extraAmount > 0)
                    {


                        label16.Text = "+";
                        label15.Visible = true;


                    }
                    else if (extraAmount < 0)
                    {

                        label16.Text = "-";
                        label15.Visible = false;

                    }

                    if (extradays == 1)
                    {
                        label15.Visible = true;
                        label15.Text = extradays.ToString() + " Day Extra";

                    }
                    else if (extradays > 1)
                    {
                        label15.Visible = true;
                        label15.Text = extradays.ToString() + " Days Extra";

                    }
                    else if (extradays < 0)
                    {

                        label15.Visible = false;

                    }


                    if (driverId == "No-Driver")
                    {

                        bunifuCustomTextbox8.Text = "No-Driver";

                    }
                    else
                    {

                        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(path + "\\CRMS\\paymentSettings.xml");

                        double dirverDaySalary = Convert.ToDouble(xmlDoc.SelectSingleNode("paymentSettings/DriverSettings/DriverDailySalary").InnerText);

                        double totalDriverPayment = dirverDaySalary * extradays;

                        bunifuCustomTextbox8.Text = totalDriverPayment.ToString();


                        if (extradays == 1)
                        {
                            label19.Visible = true;
                            label19.Text = extradays.ToString() + " Day Extra";

                        }
                        else if (extradays > 1)
                        {
                            label19.Visible = true;
                            label19.Text = extradays.ToString() + " Days Extra";

                        }
                        else if (extradays <= 0)
                        {

                            label19.Visible = false;

                        }

                        if (totalDriverPayment == 0)
                        {
                            label8.Text = "+";
                            

                        }
                        else if (totalDriverPayment < 0)
                        {

                            label8.Text = "-";
                            

                        }
                        else if (totalDriverPayment > 0) {

                            label8.Text = "+";
                          
                        }
                        

                    }

                }

            }
            
        }


        private void bunifuCustomTextbox2_TextChanged(object sender, EventArgs e)
        {

            if (bunifuCustomTextbox2.Text != string.Empty)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(bunifuCustomTextbox2.Text, "[^0-9]"))
                {

                    char[] chars = bunifuCustomTextbox2.Text.ToCharArray();
                    string charsToStr = new string(chars);

                    ErrorMsgBox er = new ErrorMsgBox("The odometer rate must be in digit format.\nLetters and symbols are not allowed.");
                    er.Show();
                    bunifuCustomTextbox2.Text = charsToStr.Remove(charsToStr.Length - 1);


                }
                else
                {

                    double startMileage = Convert.ToDouble(bunifuCustomTextbox4.Text);
                    double endMileage = Convert.ToDouble(bunifuCustomTextbox2.Text);

                    string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(path + "\\CRMS\\kmSettings.xml");

                    double MileagePerDayXML = Convert.ToDouble(xmlDoc.SelectSingleNode("kmSettings/perday").InnerText);

                    if (basis == "Daily")
                    {

                        if (endMileage < startMileage == false)
                        {

                            double MileageDiff = endMileage - startMileage;

                            double canDays = days * MileagePerDayXML;


                            double balanceDiff = MileageDiff - canDays;

                            if (balanceDiff >= 0)
                            {
                                extraMileage = balanceDiff;
                            }
                            else
                            {

                                extraMileage = 0;

                            }

                            if (balanceDiff != 0)
                            {
                                string path2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                XmlDocument xmlDoc2 = new XmlDocument();
                                xmlDoc2.Load(path2 + "\\CRMS\\Paymentsettings.xml");

                                string ExtraKmType = xmlDoc2.SelectSingleNode("paymentSettings/ExtraKillometer/KmDepandsOn").InnerText;
                                double extraPerKmPayment = Convert.ToDouble(xmlDoc2.SelectSingleNode("paymentSettings/ExtraKillometer/forExtraKm").InnerText);

                                if (ExtraKmType == "CarRate")
                                {

                                    double perKm = perAmount / MileagePerDayXML;

                                    double finalAns = balanceDiff * perKm;

                                    if (finalAns > 0)
                                    {
                                        bunifuCustomTextbox6.Text = finalAns.ToString();
                                    }
                                    else
                                    {

                                        bunifuCustomTextbox6.Text = "0";

                                    }

                                }
                                else if (ExtraKmType == "CustomRate")
                                {

                                    double finalAns = balanceDiff * extraPerKmPayment;

                                    if (finalAns > 0)
                                    {
                                        bunifuCustomTextbox6.Text = finalAns.ToString();
                                    }
                                    else
                                    {

                                        bunifuCustomTextbox6.Text = "0";

                                    }

                                }

                            }
                            else
                            {
                                bunifuCustomTextbox6.Text = "0";
                            }

                            if (balanceDiff == 0)
                            {

                                label17.Visible = false;

                            }
                            else if (balanceDiff > 0)
                            {

                                label17.Visible = true;
                                label17.Text = balanceDiff + " KM Extra";
                            }
                            else
                            {

                                label17.Visible = false;
                            }

                        }
                        else
                        {
                           
                            double MileageDiff = (endMileage + 100000) - startMileage;

                            double canDays = days * MileagePerDayXML;

                            double balanceDiff = MileageDiff - canDays;

                            if (balanceDiff >= 0)
                            {
                                extraMileage = balanceDiff;
                            }
                            else
                            {

                                extraMileage = 0;

                            }

                            if (balanceDiff != 0)
                            {

                                string path2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                XmlDocument xmlDoc2 = new XmlDocument();
                                xmlDoc2.Load(path2 + "\\CRMS\\Paymentsettings.xml");

                                string ExtraKmType = xmlDoc2.SelectSingleNode("paymentSettings/ExtraKillometer/KmDepandsOn").InnerText;
                                double extraPerKmPayment = Convert.ToDouble(xmlDoc2.SelectSingleNode("paymentSettings/ExtraKillometer/forExtraKm").InnerText);


                                if (ExtraKmType == "CarRate")
                                {

                                    double perKm = perAmount / MileagePerDayXML;
                                    double finalAns = balanceDiff * perKm;
                                    
                                    if (finalAns > 0)
                                    {
                                        bunifuCustomTextbox6.Text = finalAns.ToString();
                                    }
                                    else
                                    {

                                        bunifuCustomTextbox6.Text = "0";

                                    }
                                }
                                else if (ExtraKmType == "CustomRate")
                                {

                                    double perKm = extraPerKmPayment;
                                    double finalAns = balanceDiff * perKm;

                                    if (finalAns > 0)
                                    {
                                        bunifuCustomTextbox6.Text = finalAns.ToString();
                                    }
                                    else
                                    {

                                        bunifuCustomTextbox6.Text = "0";

                                    }

                                }
                            }
                            else
                            {

                                bunifuCustomTextbox6.Text = "0";
                            }


                            if (balanceDiff == 0)
                            {

                                label17.Visible = false;

                            }
                            else if (balanceDiff > 0)
                            {

                                label17.Visible = true;
                                label17.Text = balanceDiff + " KM Extra";
                            }
                            else
                            {

                                label17.Visible = false;
                            }
                        }



                        double box1 = Convert.ToDouble(bunifuCustomTextbox3.Text);
                        double box2 = Convert.ToDouble(bunifuCustomTextbox5.Text);
                        double box3 = Convert.ToDouble(bunifuCustomTextbox6.Text);
                        double box4 = 0;

                        if (bunifuCustomTextbox8.Text != "No-Driver")
                        {

                            box4 = Convert.ToDouble(bunifuCustomTextbox8.Text);
                        }
                        else
                        {


                            box4 = 0;
                        }


                        double total = box1 + box2 + box3 + box4;

                        bunifuCustomTextbox7.Text = total.ToString();

                    }
                    else if (basis == "Monthly")
                    {


                        if (endMileage < startMileage == false)
                        {
                            double next = extradays * MileagePerDayXML;

                            double MileageDiff = endMileage - startMileage;

                            double canDays = milage + next;

                            double balanceDiff = MileageDiff - canDays;


                            if (balanceDiff >= 0)
                            {
                                extraMileage = balanceDiff;
                            }
                            else
                            {

                                extraMileage = 0;

                            }

                            if (balanceDiff != 0)
                            {
                                string path2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                XmlDocument xmlDoc2 = new XmlDocument();
                                xmlDoc2.Load(path2 + "\\CRMS\\Paymentsettings.xml");

                                string ExtraKmType = xmlDoc2.SelectSingleNode("paymentSettings/ExtraKillometer/KmDepandsOn").InnerText;
                                double extraPerKmPayment = Convert.ToDouble(xmlDoc2.SelectSingleNode("paymentSettings/ExtraKillometer/forExtraKm").InnerText);

                                if (ExtraKmType == "CarRate")
                                {

                                    string carDaySalary = "SELECT carDailyRate FROM car WHERE carId ='" + carId + "'";
                                    MySqlDataReader data = curdFun.SelectQueryOutMsg(carDaySalary);

                                    double dayRate = 0;


                                    while (data.Read())
                                    {


                                        dayRate = data.GetDouble(0);
                                    }

                                    double perKm = dayRate / MileagePerDayXML;


                                    double finalAns = (balanceDiff) * perKm;

                                    if (finalAns > 0)
                                    {
                                        bunifuCustomTextbox6.Text = finalAns.ToString();
                                    }
                                    else
                                    {

                                        bunifuCustomTextbox6.Text = "0";

                                    }

                                }
                                else if (ExtraKmType == "CustomRate")
                                {

                                    double finalAns = (balanceDiff) * extraPerKmPayment;

                                    if (finalAns > 0)
                                    {
                                        bunifuCustomTextbox6.Text = finalAns.ToString();
                                    }
                                    else
                                    {

                                        bunifuCustomTextbox6.Text = "0";

                                    }

                                }

                            }
                            else
                            {
                                bunifuCustomTextbox6.Text = "0";
                            }

                            if (balanceDiff == 0)
                            {

                                label17.Visible = false;

                            }
                            else if (balanceDiff > 0)
                            {

                                label17.Visible = true;
                                label17.Text = balanceDiff + " KM Extra";
                            }
                            else
                            {

                                label17.Visible = false;
                            }

                        }
                        else
                        {
                            double next = extradays * MileagePerDayXML;
                            double MileageDiff = (endMileage + 100000) - startMileage;

                            double canDays = milage+next;

                            double balanceDiff = MileageDiff - canDays;

                            if (balanceDiff >= 0)
                            {
                                extraMileage = balanceDiff;
                            }
                            else
                            {

                                extraMileage = 0;

                            }

                            if (balanceDiff != 0)
                            {

                                string path2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                XmlDocument xmlDoc2 = new XmlDocument();
                                xmlDoc2.Load(path2 + "\\CRMS\\Paymentsettings.xml");

                                string ExtraKmType = xmlDoc2.SelectSingleNode("paymentSettings/ExtraKillometer/KmDepandsOn").InnerText;
                                double extraPerKmPayment = Convert.ToDouble(xmlDoc2.SelectSingleNode("paymentSettings/ExtraKillometer/forExtraKm").InnerText);


                                if (ExtraKmType == "CarRate")
                                {

                                    string carDaySalary = "SELECT carDailyRate FROM car WHERE carId ='" + carId + "'";
                                    MySqlDataReader data = curdFun.SelectQueryOutMsg(carDaySalary);

                                    double dayRate = 0;

                                    while (data.Read())
                                    {


                                        dayRate = data.GetDouble(0);
                                    }


                                    double perKm = dayRate / MileagePerDayXML;

                                    double finalAns = balanceDiff * perKm;
                                  
                                    if (finalAns > 0)
                                    {
                                        bunifuCustomTextbox6.Text = finalAns.ToString();
                                    }
                                    else
                                    {

                                        bunifuCustomTextbox6.Text = "0";

                                    }
                                }
                                else if (ExtraKmType == "CustomRate")
                                {

                                    double perKm = extraPerKmPayment;
                                    double finalAns = balanceDiff * perKm;

                                    if (finalAns > 0)
                                    {
                                        bunifuCustomTextbox6.Text = finalAns.ToString();
                                    }
                                    else
                                    {

                                        bunifuCustomTextbox6.Text = "0";

                                    }

                                }
                            }
                            else
                            {

                                bunifuCustomTextbox6.Text = "0";
                            }


                            if (balanceDiff == 0)
                            {

                                label17.Visible = false;

                            }
                            else if (balanceDiff > 0)
                            {

                                label17.Visible = true;
                                label17.Text = balanceDiff + " KM Extra";
                            }
                            else
                            {

                                label17.Visible = false;
                            }
                        }



                        double box1 = Convert.ToDouble(bunifuCustomTextbox3.Text);
                        double box2 = Convert.ToDouble(bunifuCustomTextbox5.Text);
                        double box3 = Convert.ToDouble(bunifuCustomTextbox6.Text);
                        double box4 = 0;

                        if (bunifuCustomTextbox8.Text != "No-Driver")
                        {

                            box4 = Convert.ToDouble(bunifuCustomTextbox8.Text);
                        }
                        else
                        {


                            box4 = 0;
                        }


                        double total = box1 + box2 + box3 + box4;

                        bunifuCustomTextbox7.Text = total.ToString();

                    }
                }

                if (bunifuCustomTextbox2.Text != "")
                {

                    int startMile = Convert.ToInt32(bunifuCustomTextbox4.Text);
                    int endMile = Convert.ToInt32(bunifuCustomTextbox2.Text);

                    if (endMile < startMile && kmmeter_type == "Digital")
                    {

                        bunifuCustomTextbox6.Text = "0";
                        label17.Visible = false;
                        double total = Convert.ToDouble(bunifuCustomTextbox3.Text) + Convert.ToDouble(bunifuCustomTextbox5.Text) + Convert.ToDouble(bunifuCustomTextbox8.Text);

                        bunifuCustomTextbox7.Text = total.ToString();

                    }
                }
            }
                            
        }

        private void returning_Load(object sender, EventArgs e)
        {
            label15.Visible = false;
            label17.Visible = false;
            label19.Visible = false;
            bunifuFlatButton1.Enabled = false;
            bunifuCustomTextbox6.Text = "0";
            bunifuCustomTextbox2.Enabled = false;
         
        }

        private void bunifuDatepicker2_Click(object sender, EventArgs e)
        {
            

        }

        private void bunifuDatepicker2_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void returning_MouseHover(object sender, EventArgs e)
        {
            

        }

        private void groupBox1_Move(object sender, EventArgs e)
        {
           
        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            
          
        }

        private void bunifuDatepicker2_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            bunifuCustomTextbox1.Clear();
            bunifuCustomTextbox2.Clear();
            bunifuCustomTextbox3.Clear();
            bunifuCustomTextbox4.Clear();
            bunifuCustomTextbox5.Clear();
            bunifuCustomTextbox6.Clear();
            bunifuCustomTextbox7.Clear();
            bunifuDatepicker1.Value = DateTime.Now;
            bunifuDatepicker2.Value = DateTime.Now;
            label17.Text = "";
            label15.Text = "";

        }

        private void bunifuCustomTextbox1_Click(object sender, EventArgs e)
        {
            OrderAvaliable o = new OrderAvaliable();
            o.ShowDialog();

            if (o.monoFlat_TextBox1.Text != string.Empty)
            {

                bunifuCustomTextbox1.Text = o.current;
               

            }

           
        }

        private void bunifuCustomTextbox7_TextChanged(object sender, EventArgs e)
        {
            bunifuFlatButton1.Enabled = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuDatepicker1_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuDatepicker1_onValueChanged(object sender, EventArgs e)
        {
            if (bunifuCustomTextbox1.Text != string.Empty)
            {

                string strCode = "SELECT rentedDate FROM booking WHERE orderId='" + bunifuCustomTextbox1.Text + "'";
                MySqlDataReader data = curdFun.SelectQueryOutMsg(strCode);

                DateTime t = DateTime.Now;

                while (data.Read())
                {

                    t = data.GetDateTime(0);
                }

                bunifuDatepicker1.Value = t;

            }
            else {

                bunifuDatepicker1.Value = DateTime.Now.AddDays(2);

            }
        }

        private void bunifuCustomTextbox6_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
