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
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;

namespace Car_Renting_Management_System
{
    public partial class OldPrinter : Form
    {

        string orderId = "";
        string customerId = "";
        string carId = "";

        string CompanyName = "";
        string address = "";
        string telephone1 = "";
        string telephone2 = "";

        public OldPrinter(string oId,string CuId,string CaId)
        {
            InitializeComponent();

            orderId = oId;
            customerId = CuId;
            carId = CaId;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        int XCo = 0;
        int YCo = 0;
        int can = 0;


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            XCo = e.X;
            YCo = e.Y;
            can = 1;
           

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            can = 0;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (can == 1)
            {

                this.SetDesktopLocation(MousePosition.X - XCo, MousePosition.Y - YCo);

            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            can = 1;
            XCo = e.X;
            YCo = e.Y;
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            can = 0;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (can == 1) {


                this.SetDesktopLocation(MousePosition.X - 280, MousePosition.Y - 15);
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "The Invoice";
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 620, 800);
            printDoc.PrintPage += printDoc_PrintPage;
            printDlg.Document = printDoc;

            if (printDlg.ShowDialog() == DialogResult.OK)
            {

                printDoc.Print();
            }

            this.Hide();

        }

        void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {


            Font font10 = new Font("Courier New", 14);
            Font font12 = new Font("Courier New", 12);
            Font heading = new Font("Times New Roman", 18, FontStyle.Bold);

            float leading = 4;
            float lineheight10 = font10.GetHeight() + leading;
            float lineheight12 = font12.GetHeight() + leading;
            float lineheight14 = heading.GetHeight() + leading;

            float startX = 0;
            float startY = leading + 14;
            float Offset = 0;

            StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
            StringFormat formatCenter = new StringFormat(formatLeft);
            StringFormat formatRight = new StringFormat(formatLeft);

            formatCenter.Alignment = StringAlignment.Center;
            formatRight.Alignment = StringAlignment.Far;
            formatLeft.Alignment = StringAlignment.Near;

            SizeF layoutSize = new SizeF(620 - Offset * 2, lineheight14);
            RectangleF layout = new RectangleF(new PointF(startX, startY + Offset), layoutSize);

            Brush brush = Brushes.Blue;
            Brush brush2 = Brushes.Black;



            //Printing area
            e.Graphics.DrawString(CompanyName, heading, brush, layout, formatCenter);

            string se = Regex.Replace(address.ToString(), " *, *", ",");

            string[] addressnew = Regex.Split(se, @"(?<=[.,;])");



            int x = 0;
            int y = 20;

            if (address.Length > 35)
            {
                x = 90;

            }
            else if (address.Length <= 35)
            {

                x = 120;

            }


            foreach (string s in addressnew)
            {
                x += 20;
                e.Graphics.DrawString(s, new Font("Courier New", 15, FontStyle.Bold), Brushes.Black, y, x);

            }
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "img.png";
            Image newImage = Image.FromFile(path);


            e.Graphics.DrawImage(newImage, 440, 154);
            e.Graphics.DrawString(telephone1, new Font("Courier New", 16, FontStyle.Bold), Brushes.Black, 473, 160);


            Brush b1 = Brushes.Black;

            Point point1 = new Point(0, 80);
            Point point2 = new Point(620, 80);
            Point point1N = new Point(0, 70);
            Point point2N = new Point(620, 70);

            Pen p1 = new Pen(b1);

            e.Graphics.DrawLine(p1, point1, point2);
            e.Graphics.DrawLine(p1, point1N, point2N);

            if (telephone2 != "")
            {

                e.Graphics.DrawImage(newImage, 440, 196);
                e.Graphics.DrawString(telephone2, new Font("Courier New", 16, FontStyle.Bold), Brushes.Black, 473, 200);
            }

            var time = DateTime.Now;

            e.Graphics.DrawString(time.ToString(), new Font("Courier New", 13), Brushes.Black, 370, 95);

            Point point3N = new Point(0, 240);
            Point point4N = new Point(620, 240);



            e.Graphics.DrawLine(p1, point3N, point4N);


            //Main Bill Content

            

            string[] names = new string[]
            {
             "Order ID:------------------",
             "Customer Name:-------------",
             "Driver Name:---------------",
             "Basis:---------------------",           
             "Extra Days:----------------",
             "Extra Days Payment:--------",
             "Extra Driver Payment:------",
             "Extra KM Rate:-------------",
             "Extra KM Rate Payment:-----",
             "Total Payment:-------------",
             
            };

            
            int xnn = 400;
            int ynn = 279;
            
            List<String> valuesOfBill = new List<String>();
            valuesOfBill.Add(orderId);
            valuesOfBill.Add(cusName);
            valuesOfBill.Add(driverName);
            valuesOfBill.Add(basis);
            valuesOfBill.Add(extradays);
            valuesOfBill.Add("Rs:"+extradaysAmount+"/-");
            valuesOfBill.Add("Rs:"+extraDriverAmount+"/-");
            valuesOfBill.Add(extraKilloMeter);
            valuesOfBill.Add("Rs:"+extrakilloMeterAmount+"/-");
            valuesOfBill.Add("Rs:" +totalPayment+"/-");
            
            string[] outputA;
            List<String> list = names.ToList();
            outputA = list.ToArray();

            int xn = 95;
            int yn = 279;


            if (driverId=="No-Driver")
            {
                list.RemoveAt(2);
                valuesOfBill.RemoveAt(2);
                list.RemoveAt(5);
                valuesOfBill.RemoveAt(5);
                outputA = list.ToArray();

                //yn = 295;
                //ynn = 295;

            }
            
            for (int s = 0; s < outputA.Length; s++)
            {

                e.Graphics.DrawString(outputA[s], new Font("Courier New", 13), Brushes.Black, xn, yn);
                yn += 30;

            }

            for (int a = 0; a < valuesOfBill.Count; a++)
            {

                e.Graphics.DrawString(valuesOfBill[a], new Font("Courier New", 13), Brushes.Black, xnn, ynn);
                ynn += 30;

            }


            Point point5N = new Point(0, 695);
            Point point6N = new Point(620, 695);



            e.Graphics.DrawLine(p1, point5N, point6N);

            Font welcome = new Font("Courier New", 16, FontStyle.Bold);
            Brush b3 = Brushes.Black;

            SizeF layoutSizeForWelcome = new SizeF(620 - Offset * 2, lineheight14);
            RectangleF layoutforWelcome = new RectangleF(new PointF(startX, startY + 690 + Offset), layoutSizeForWelcome);

            SizeF layoutSizeForWelcome2 = new SizeF(620 - Offset * 2, lineheight14);
            RectangleF layoutforWelcome2 = new RectangleF(new PointF(startX, startY + 715 + Offset), layoutSizeForWelcome2);

            SizeF layoutSizeForWelcome3 = new SizeF(620 - Offset * 2, lineheight14);
            RectangleF layoutforWelcome3 = new RectangleF(new PointF(startX, startY + 753 + Offset), layoutSizeForWelcome3);

            Font welcomefonr = new Font("Courier New", 12, FontStyle.Bold);

            e.Graphics.DrawString("Welcome Again", welcome, b3, layoutforWelcome, formatCenter);

            e.Graphics.DrawString("Thank You.", welcome, b3, layoutforWelcome2, formatCenter);

            e.Graphics.DrawString("Software Solution By G-tech(+940756800519)", welcomefonr, b3, layoutforWelcome3, formatCenter);

            //End Main Bill Content

            //end print Area

            string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string txt = "The invoice was printed on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString() + " for ID " + orderId;
            File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        

       CURDQueryClass c = new CURDQueryClass();

        string cusName = "";
        string driverName = "";
        string driverId = "";
        string basis = "";
        string duration = "";
        string extradays = "";
        string extradaysAmount = "";
        string extraDriverAmount = "";
        string extraKilloMeter = "";
        string extrakilloMeterAmount = "";
        string totalPayment = "";


        private void OldPrinter_Load(object sender, EventArgs e)
        {
            string oldDateQuery = "SELECT * FROM oldbookdata WHERE oderId='" + orderId + "'";
            MySqlDataReader data = c.SelectQueryOutMsg(oldDateQuery);

            while (data.Read()) {

                driverId = data.GetString("driverId");
                basis = data.GetString("basis");
                duration = data.GetString("duration");
                extradays = data.GetString("extraDay");
                extradaysAmount = data.GetString("extraDayAmount");
                extraDriverAmount = data.GetString("extradriverSalary");
                extraKilloMeter = data.GetString("extraMileage");
                extrakilloMeterAmount = data.GetString("extraMileageAmount");
                totalPayment = data.GetString("TotalAmount");

            }

            string selectCustomer = "SELECT customerFname FROM customer WHERE customerId='" + customerId + "'";
            MySqlDataReader dataForCus = c.SelectQueryOutMsg(selectCustomer);

            while (dataForCus.Read()) {

                cusName = dataForCus.GetString(0);
            }

            string selectDriverName = "SELECT driverFname FROM driver WHERE driverId='" + driverId + "'";
            MySqlDataReader dataDriver = c.SelectQueryOutMsg(selectDriverName);

            while (dataDriver.Read()) {


                driverName = dataDriver.GetString(0);
            }

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path + "\\CRMS\\generalSettings.xml");

            CompanyName = xDoc.SelectSingleNode("GeneralSettings/CompanyName").InnerText;
            address = xDoc.SelectSingleNode("GeneralSettings/CompanyAddress").InnerText;
            telephone1 = xDoc.SelectSingleNode("GeneralSettings/Telephone").InnerText;
            telephone2 = xDoc.SelectSingleNode("GeneralSettings/OptionalTelephone").InnerText;

        }

    }
}
