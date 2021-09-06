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
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace Car_Renting_Management_System
{
    public partial class bookPrintStage : Form
    {
        string orderID = "";
        string cusID = "";
        string carID = "";
        
        public bookPrintStage(string orderId,string cusId,string carId)
        {
            InitializeComponent();
            System.Media.SystemSounds.Beep.Play();
            orderID = orderId;
            cusID = cusId;
            carID = carId;

        }

        string update = "";
        string refund = "";
        string refundAmount = "";

        public bookPrintStage(string orderId, string cusId, string carId,string up)
        {
            InitializeComponent();
            System.Media.SystemSounds.Beep.Play();
            orderID = orderId;
            cusID = cusId;
            carID = carId;
            update = up;

        }

      
        public bookPrintStage(string orderId, string cusId, string carId, string up,string re,string reA)
        {
            InitializeComponent();
            System.Media.SystemSounds.Beep.Play();
            orderID = orderId;
            cusID = cusId;
            carID = carId;
            update = up;
            refund = re;
            refundAmount = reA;

        }

        int canMove;
        int XCor;
        int YCor;

        bookForm b = new bookForm();
        

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
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
            XCor = e.X;
            YCor = e.Y;

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            if (canMove ==1) {

                this.SetDesktopLocation(MousePosition.X - XCor, MousePosition.Y - YCor);

            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

       
        string basis = "";
        string rentalDate = "";
        string returnDate = "";

        double totalAmount = 0;
        double advancedPaidAmount =0;
        double blanceAmount = 0;
        string kmUnit = "";
        string carKm = "";
        int duration = 0;

        string carId = "";
        string carBrand = "";


        string customerId = "";
        string customerName = "";

        string CompanyName = "";
        string address = "";
        string telephone1 = "";
        string telephone2 = "";

        string driverID = "";
        string driverName = "";
        string driverAmount = "";

        string discountPer = "";
        string discountAmount = "";

        string odameter = "";

        string mileage = "";

        
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {

            MySqlConnection mycon1 = new MySqlConnection("SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=");
            mycon1.Open();

            string queryForOrder = "SELECT * FROM booking WHERE orderId='" + orderID + "'";


            MySqlCommand mycomForOrder = new MySqlCommand(queryForOrder, mycon1);
            MySqlDataReader OrderData = mycomForOrder.ExecuteReader();

            while (OrderData.Read())
            {
                
                basis = OrderData.GetString("basis");
                rentalDate = OrderData.GetString("rentedDate");
                returnDate = OrderData.GetString("returnDate");
                totalAmount = Convert.ToDouble(OrderData.GetString("TotalAmount"));
                advancedPaidAmount = Convert.ToDouble(OrderData.GetString("advancedAmount"));
                blanceAmount = Convert.ToDouble(OrderData.GetString("blance"));
                kmUnit = OrderData.GetString("startMileage");
                duration = OrderData.GetInt32("duration");
                driverID = OrderData.GetString("driverid");
                driverAmount = OrderData.GetString("driverAmount");
                discountPer = OrderData.GetString("discountPer");
                discountAmount = OrderData.GetString("discountAmount");
                mileage = OrderData.GetString("totalMileage");


            }


       
            mycon1.Close();

            string quForDrivername = "Select driverFname FROM driver WHERE driverId='" + driverID + "'";

            CURDQueryClass cu = new CURDQueryClass();

            MySqlDataReader dataDriver = cu.SelectQueryOutMsg(quForDrivername);

            while (dataDriver.Read())
            {


                driverName = dataDriver.GetString(0);

            }

            

            MySqlConnection mycon3 = new MySqlConnection("SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=");
            mycon3.Open();

            string queryForCustomer = "SELECT * FROM customer WHERE customerId='" + cusID + "'";



            MySqlCommand myComForCustomer = new MySqlCommand(queryForCustomer, mycon3);
            MySqlDataReader CustomerData = myComForCustomer.ExecuteReader();

            while (CustomerData.Read())
            {

                customerId = CustomerData.GetString("customerId");
                customerName = CustomerData.GetString("customerFname");

            }
            mycon3.Close();



            string carD="SELECT odometer FROM car WHERE carId='"+carID+"'";
            MySqlDataReader carData = cu.SelectQueryOutMsg(carD);

            while (carData.Read())
            {

                odameter = carData.GetString(0);

            }
            
            PrintDialog printDlg = new PrintDialog();         
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "The Invoice";
            printDoc.DefaultPageSettings.PaperSize=new PaperSize("A4", 620, 800);
            printDoc.PrintPage += printDoc_PrintPage;
            printDlg.Document = printDoc;

            if (printDlg.ShowDialog() == DialogResult.OK) {

                printDoc.Print();
            }

            this.Hide();
            
        }

        
        void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
           

            Font font10 = new Font("Courier New", 14);
            Font font12 = new Font("Courier New", 12);
            Font heading = new Font("Times New Roman", 18,FontStyle.Bold);

            float leading = 4;
            float lineheight10 = font10.GetHeight() + leading;
            float lineheight12 = font12.GetHeight() + leading;
            float lineheight14 = heading.GetHeight() + leading;

            float startX = 0;
            float startY = leading+14;
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

            }else if (address.Length <= 35)
            {
                
                x = 120;

            }
            
           
            foreach (string s in addressnew) {
                x += 20;
                e.Graphics.DrawString(s, new Font("Courier New", 15,FontStyle.Bold), Brushes.Black, y, x);

            }
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "img.png";
            Image newImage = Image.FromFile(path);


            e.Graphics.DrawImage(newImage,440, 154);
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

            e.Graphics.DrawString(time.ToString(),new Font("Courier New", 13), Brushes.Black, 370, 95);

            Point point3N = new Point(0, 240);
            Point point4N = new Point(620, 240);

 

            e.Graphics.DrawLine(p1, point3N, point4N);


            //Main Bill Content


            string draDays = "Days:----------------------";
            string duraMonths = "Months:--------------------";

            string duraSys = "";

            if (basis == "Daily") {

                duraSys = draDays;

            } else if (basis== "Monthly") {


                duraSys = duraMonths;
            }

            string[] names = new string[]
            {
             "Order ID:------------------",
             "Customer Name:-------------",
             "Driver Name:---------------",
             "Basis:---------------------",
             "Rented Date:---------------",
             "Return Date:---------------",
              duraSys,
             "Advanced Paid Payment:-----",
             "Discount:------------------",
             "Total Payment:-------------",
             "Balance Payment:-----------",
             "Given Mileage:-------------",
             "Odometer Value:------------"

            };


            if (refund == "yes")
            {

                names[7] = "Refund Amount:-------------";
                MessageBox.Show("Test");

            }

            int xnn = 400;
            int ynn = 279;
            
            string discountText = "Rs:"+discountAmount +"/-"+ "("+ discountPer + ")";
            
            List<String> valuesOfBill = new List<String>();
            valuesOfBill.Add(orderID);
            valuesOfBill.Add(customerName);
            valuesOfBill.Add(driverName);
            valuesOfBill.Add(basis);
            valuesOfBill.Add(rentalDate);
            valuesOfBill.Add(returnDate);
            valuesOfBill.Add(duration.ToString());

            if (refund == "yes") {

                valuesOfBill.Add("Rs:"+refundAmount+"/-");

            }
            else
            {

                valuesOfBill.Add("Rs:" + advancedPaidAmount.ToString()+"/-");

            }

            double totalAmo = totalAmount +Convert.ToDouble(discountAmount);

            valuesOfBill.Add(discountText);
            valuesOfBill.Add("Rs:" + totalAmo.ToString()+"/-");
            valuesOfBill.Add("Rs:"+blanceAmount.ToString()+"/-");
            valuesOfBill.Add(mileage.ToString()+"KM");
            valuesOfBill.Add(odameter.ToString());
            
            
            string[] outputA;
            List<String> list = names.ToList();
            outputA = list.ToArray();

            int xn = 95;
            int yn = 279;  


            if (discountAmount == "0" && driverAmount!="0")
            {
                list.RemoveAt(8);
                valuesOfBill.RemoveAt(8);
                outputA = list.ToArray();
                
                yn = 295;
                ynn = 295;

            }
            else if (driverAmount == "0" && discountAmount!="0")
            {

                
                list.RemoveAt(2);
                valuesOfBill.RemoveAt(2);
                yn = 292;
                ynn = 292;
                outputA = list.ToArray();
               
                
            }
            else if (discountAmount=="0" && driverAmount=="0") {

                list.RemoveAt(8);
               
                list.RemoveAt(2);
                valuesOfBill.RemoveAt(8);
                valuesOfBill.RemoveAt(2);
                outputA = list.ToArray();
                yn = 305;
                ynn = 305;
                    
                
            }


            if (update == "update" && discountAmount == "0")
            {

                e.Graphics.DrawString("(Invoice Updated)", new Font("Courier New", 13, FontStyle.Bold), Brushes.Black, 398, 120);
                
            }

            if (update == "update" && discountAmount != "0")
            {
                e.Graphics.DrawString("(Invoice Updated)", new Font("Courier New", 13, FontStyle.Bold), Brushes.Black, 398, 120);
               
              

            }

            for (int s = 0; s < outputA.Length; s++)
            {

                e.Graphics.DrawString(outputA[s], new Font("Courier New", 13), Brushes.Black, xn, yn);
                yn += 30;
                
            }

            for (int a = 0; a < valuesOfBill.Count; a++) {

                e.Graphics.DrawString(valuesOfBill[a], new Font("Courier New", 13), Brushes.Black, xnn, ynn);
                ynn += 30;

            }


            Point point5N = new Point(0, 695);
            Point point6N = new Point(620, 695);


           
            e.Graphics.DrawLine(p1, point5N, point6N);

            Font welcome = new Font("Courier New", 16, FontStyle.Bold);
            Brush b3 = Brushes.Black;

            SizeF layoutSizeForWelcome = new SizeF(620 - Offset * 2, lineheight14);
            RectangleF layoutforWelcome = new RectangleF(new PointF(startX, startY+690 + Offset), layoutSizeForWelcome);

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
            string txt = "The invoice was printed on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString()+" for ID "+orderID;
            File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

        }
       
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 250, MousePosition.Y - 18);

            }

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            XCor = e.X;
            YCor = e.Y;

        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void bookPrintStage_Load(object sender, EventArgs e)
        {

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
