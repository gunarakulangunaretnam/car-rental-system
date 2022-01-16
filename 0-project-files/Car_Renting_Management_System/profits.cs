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


namespace Car_Renting_Management_System
{
    public partial class profits : Form
    {
        public profits()
        {
            InitializeComponent();
        }

        CURDQueryClass cClass = new CURDQueryClass();

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void profits_Load(object sender, EventArgs e)
        {
            label5.Visible = false;
            label6.Visible = false;
            comboBox2.Visible = false;
            bunifuDatepicker1.Visible = false;
            bunifuDatepicker1.Value = DateTime.Now;

           
      

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Specific Date")
            {

                label5.Visible = true;
                bunifuDatepicker1.Visible = true;

                label6.Visible = false;
                comboBox2.Visible = false;

            }
            else if (comboBox1.Text == "Specific Month")
            {


                label6.Visible = true;
                comboBox2.Visible = true;

                label5.Visible = false;
                bunifuDatepicker1.Visible = false;


            }
            else
            {

                label5.Visible = false;
                label6.Visible = false;
                comboBox2.Visible = false;
                bunifuDatepicker1.Visible = false;

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            double totalValue = 0;

            if (radioButton1.Checked)
            {

                if (comboBox1.Text == "Daily")
                {
                    try
                    {
                        string selectTotal = "SELECT SUM(advancedAmount) FROM booking WHERE basis='Daily'";

                        MySqlDataReader data = cClass.SelectQueryOutMsg(selectTotal);

                        while (data.Read())
                        {

                            totalValue = data.GetDouble(0);
                        }


                        showProfits s = new showProfits(totalValue);
                        s.Show();
                    }
                    catch (Exception)
                    {
                        ErrorMsgBox r = new ErrorMsgBox("No data for daily");
                        r.Show();
                       
                    }
                    
                }
                else if (comboBox1.Text == "Monthly")
                {

                    try
                    {
                        string selectTotalMonth = "SELECT SUM(advancedAmount) FROM booking WHERE basis='Monthly'";
                        MySqlDataReader data = cClass.SelectQueryOutMsg(selectTotalMonth);

                        while (data.Read())
                        {

                            totalValue = data.GetDouble(0);
                        }

                        showProfits s = new showProfits(totalValue);
                        s.Show();

                    }
                    catch (Exception)
                    {

                        ErrorMsgBox r = new ErrorMsgBox("No data for monthly");
                        r.Show();

                    }
                    
                }
                else if (comboBox1.Text == "Specific Date")
                {

                    try
                    {
                        string dataDate = bunifuDatepicker1.Value.ToString("yyyy-MM-dd");
                        string sqlcodeForSpecificDate = "SELECT SUM(advancedAmount) FROM booking WHERE rentedDate='" + dataDate.Trim() + "'";



                        MySqlDataReader data = cClass.SelectQueryOutMsg(sqlcodeForSpecificDate);

                        while (data.Read())
                        {
                            totalValue = data.GetDouble(0);

                        }


                        showProfits s = new showProfits(totalValue);
                        s.Show();

                    }
                    catch (Exception)
                    {

                        ErrorMsgBox r = new ErrorMsgBox("No Data for this Date");
                        r.Show();
                    }

                }
                else if (comboBox1.Text == "Specific Month")
                {


                    if (comboBox2.Text == "January")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '01'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();

                        }


                    }
                    else if (comboBox2.Text == "February")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '02'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();

                        }

                    }
                    else if (comboBox2.Text == "March")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '03'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }


                    }
                    else if (comboBox2.Text == "April")
                    {
                        try
                        {
                            string selectDa = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '04'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }

                    }
                    else if (comboBox2.Text == "May")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '05'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }


                    }
                    else if (comboBox2.Text == "June")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '06'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }

                    }
                    else if (comboBox2.Text == "July")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '07'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }

                    }
                    else if (comboBox2.Text == "August")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '08'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }

                    }
                    else if (comboBox2.Text == "September")
                    {
                        try
                        {
                            string selectDa = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '09'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }
                    }
                    else if (comboBox2.Text == "October")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '10'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }

                    }
                    else if (comboBox2.Text == "November")
                    {
                        try
                        {
                            string selectDa = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '11'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }
                    }
                    else if (comboBox2.Text == "December")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '12'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }

                    }

                }
                else if (comboBox1.Text == "Specific Date Profits") {

                   

                    string date = "SELECT rentedDate,perAmount FROM booking WHERE basis='Daily'";
                    MySqlDataReader dataDates = cClass.SelectQueryOutMsg(date);

                    List<double> days = new List<double>();

                    while (dataDates.Read()) {

                        int curDay = Convert.ToDateTime(bunifuDatepicker1.Value.ToString("yyyy-MM-dd")).Day;
                        int da = dataDates.GetDateTime(0).Day;

                        int fi = curDay - da;
                        
                        days.Add(fi * dataDates.GetDouble(1));
                      

                    }
                    double ans = 0;
                    for (int a = 0; a < days.Count; a++) {

                        ans = ans + days[a];
                    }

                    string dateForMonth = "SELECT rentedDate,perAmount FROM booking WHERE basis='Monthly'";
                    MySqlDataReader dataDatesforMonth = cClass.SelectQueryOutMsg(dateForMonth);

                    List<double> daysforMonth = new List<double>();

                    while (dataDatesforMonth.Read()) {

                        int curDay= Convert.ToDateTime(bunifuDatepicker1.Value.ToString("yyyy-MM-dd")).Day;
                        int da = dataDatesforMonth.GetDateTime(0).Day;

                        int fi=curDay - da;


                        daysforMonth.Add(fi * (dataDatesforMonth.GetDouble(1) / 30));

                    }

                    double ans2 = 0;
                    for (int b = 0; b < days.Count; b++)
                    {

                        ans2 = ans2 + daysforMonth[b];
                    }


                    double finalAns = Math.Round(ans + ans2);

                    showProfits s = new showProfits(0,finalAns);
                    s.Show();
                }

            }
            else if (radioButton2.Checked)
            {

                if (comboBox1.Text == "Daily")
                {

                    string selectTotal = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE basis='Daily'";

                    MySqlDataReader data = cClass.SelectQueryOutMsg(selectTotal);

                    while (data.Read())
                    {

                        totalValue = data.GetDouble(0);
                    }

                    showProfits s = new showProfits(totalValue);
                    s.Show();

                }
                else if (comboBox1.Text == "Monthly")
                {

                    string selectTotal = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE basis='Monthly'";

                    MySqlDataReader data = cClass.SelectQueryOutMsg(selectTotal);

                    while (data.Read())
                    {

                        totalValue = data.GetDouble(0);
                    }

                    showProfits s = new showProfits(totalValue);
                    s.Show();

                }
                else if (comboBox1.Text == "Specific Date")
                {

                    try
                    {
                        string dataDate = bunifuDatepicker1.Value.ToString("yyyy-MM-dd");
                        string sqlcodeForSpecificDate = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE rentalDate='" + dataDate.Trim() + "'";


                        MySqlDataReader data = cClass.SelectQueryOutMsg(sqlcodeForSpecificDate);

                        while (data.Read())
                        {

                            totalValue = data.GetDouble(0);

                        }

                        showProfits s = new showProfits(totalValue);
                        s.Show();
                    }
                    catch (Exception)
                    {

                        ErrorMsgBox r = new ErrorMsgBox("No data for this month");
                        r.Show();

                    }

                }
                else if (comboBox1.Text == "Specific Month")
                {


                    if (comboBox2.Text == "January")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '01'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();

                        }


                    }
                    else if (comboBox2.Text == "February")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '02'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();

                        }

                    }
                    else if (comboBox2.Text == "March")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '03'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }


                    }
                    else if (comboBox2.Text == "April")
                    {
                        try
                        {
                            string selectDa = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '04'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }

                    }
                    else if (comboBox2.Text == "May")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '05'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }


                    }
                    else if (comboBox2.Text == "June")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '06'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }

                    }
                    else if (comboBox2.Text == "July")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '07'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }

                    }
                    else if (comboBox2.Text == "August")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '08'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }

                    }
                    else if (comboBox2.Text == "September")
                    {
                        try
                        {
                            string selectDa = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '09'"; ;
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }
                    }
                    else if (comboBox2.Text == "October")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '10'"; ;
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }

                    }
                    else if (comboBox2.Text == "November")
                    {
                        try
                        {
                            string selectDa = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '11'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }
                    }
                    else if (comboBox2.Text == "December")
                    {

                        try
                        {
                            string selectDa = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '12'";
                            MySqlDataReader da = cClass.SelectQueryOutMsg(selectDa);

                            while (da.Read())
                            {

                                totalValue = da.GetDouble(0);

                            }

                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {

                            totalValue = 0;
                            ErrorMsgBox r = new ErrorMsgBox("No Data for this month.");
                            r.Show();
                        }

                    }
                }
            }
            else if (radioButton3.Checked) {

                if (comboBox1.Text == "Daily")
                {

                    try
                    {
                        double bookingDataTotal = 0;
                        double oldBookDataTotal = 0;
                        string bookingDataSql = "SELECT SUM(advancedAmount) FROM booking WHERE basis = 'Daily'";
                        MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingDataSql);

                        while (bookingData.Read())
                        {

                            bookingDataTotal = bookingData.GetDouble(0);
                        }

                        string oldbookDataSql = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE basis = 'Daily'";
                        MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookDataSql);

                        while (oldBookingData.Read())
                        {

                            oldBookDataTotal = oldBookingData.GetDouble(0);

                        }

                        totalValue = bookingDataTotal + oldBookDataTotal;

                        showProfits s = new showProfits(totalValue);
                        s.Show();

                    }
                    catch (Exception)
                    {
                        try
                        {
                            double bookingDataTotal = 0;
                            string bookingDataSql = "SELECT SUM(advancedAmount) FROM booking WHERE basis = 'Daily'";
                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingDataSql);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }
                            totalValue = bookingDataTotal;
                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                double oldBookDataTotal = 0;
                                string oldbookDataSql = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE basis = 'Daily'";
                                MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookDataSql);

                                while (oldBookingData.Read())
                                {

                                    oldBookDataTotal = oldBookingData.GetDouble(0);

                                }
                                totalValue = oldBookDataTotal;

                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {

                                ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                r.Show();
                            }

                        }

                    }

                }
                else if (comboBox1.Text == "Monthly")
                {

                    try
                    {
                        double bookingDataTotal = 0;
                        double oldBookDataTotal = 0;
                        string bookingDataSql = "SELECT SUM(advancedAmount) FROM booking WHERE basis = 'Monthly'";
                        MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingDataSql);

                        while (bookingData.Read())
                        {

                            bookingDataTotal = bookingData.GetDouble(0);
                        }

                        string oldbookDataSql = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE basis = 'Monthly'";
                        MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookDataSql);

                        while (oldBookingData.Read())
                        {

                            oldBookDataTotal = oldBookingData.GetDouble(0);

                        }

                        totalValue = bookingDataTotal + oldBookDataTotal;

                        showProfits s = new showProfits(totalValue);
                        s.Show();

                    }
                    catch (Exception)
                    {
                        try
                        {
                            double bookingDataTotal = 0;
                            string bookingDataSql = "SELECT SUM(advancedAmount) FROM booking WHERE basis = 'Monthly'";
                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingDataSql);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }
                            totalValue = bookingDataTotal;
                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                double oldBookDataTotal = 0;
                                string oldbookDataSql = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE basis = 'Monthly'";
                                MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookDataSql);

                                while (oldBookingData.Read())
                                {

                                    oldBookDataTotal = oldBookingData.GetDouble(0);

                                }
                                totalValue = oldBookDataTotal;

                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {

                                ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                r.Show();
                            }

                        }

                    }
                }
                else if (comboBox1.Text == "Specific Date")
                {

                    try
                    {
                        double bookingDataTotal = 0;
                        double oldBookDataTotal = 0;
                        string dataDate = bunifuDatepicker1.Value.ToString("yyyy-MM-dd");
                        string sqlcodeForSpecificDate = "SELECT SUM(advancedAmount) FROM booking WHERE rentedDate='" + dataDate.Trim() + "'";
                        MySqlDataReader bookingData = cClass.SelectQueryOutMsg(sqlcodeForSpecificDate);

                        while (bookingData.Read())
                        {

                            bookingDataTotal = bookingData.GetDouble(0);
                        }


                        string sqlcodeForSpecificDateForOldbook = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE rentalDate='" + dataDate.Trim() + "'";
                        MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(sqlcodeForSpecificDateForOldbook);

                        while (oldBookingData.Read())
                        {

                            oldBookDataTotal = oldBookingData.GetDouble(0);

                        }

                        totalValue = bookingDataTotal + oldBookDataTotal;

                        showProfits s = new showProfits(totalValue);
                        s.Show();

                    }
                    catch (Exception)
                    {
                        try
                        {
                            double bookingDataTotal = 0;
                            string dataDate = bunifuDatepicker1.Value.ToString("yyyy-MM-dd");
                            string sqlcodeForSpecificDate = "SELECT SUM(advancedAmount) FROM booking WHERE rentedDate='" + dataDate.Trim() + "'";
                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(sqlcodeForSpecificDate);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }
                            totalValue = bookingDataTotal;
                            showProfits s = new showProfits(totalValue);
                            s.Show();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                double oldBookDataTotal = 0;
                                string dataDate = bunifuDatepicker1.Value.ToString("yyyy-MM-dd");
                                string sqlcodeForSpecificDateForOldbook = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE rentalDate='" + dataDate.Trim() + "'";
                                MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(sqlcodeForSpecificDateForOldbook);

                                while (oldBookingData.Read())
                                {

                                    oldBookDataTotal = oldBookingData.GetDouble(0);

                                }
                                totalValue = oldBookDataTotal;

                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {

                                ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                r.Show();
                            }

                        }

                    }

                }
                else if (comboBox1.Text == "Specific Month") {

                    if (comboBox2.Text == "January")
                    {

                        try
                        {
                            double bookingDataTotal = 0;
                            double oldBookDataTotal = 0;
                            string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '01'";

                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }

                            string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '01'";
                            MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                            while (oldBookingData.Read())
                            {

                                oldBookDataTotal = oldBookingData.GetDouble(0);

                            }

                            totalValue = bookingDataTotal + oldBookDataTotal;

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            try
                            {
                                double bookingDataTotal = 0;
                                string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '01'";

                                MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                                while (bookingData.Read())
                                {

                                    bookingDataTotal = bookingData.GetDouble(0);
                                }
                                totalValue = bookingDataTotal;
                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    double oldBookDataTotal = 0;
                                    string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '01'";
                                    MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                                    while (oldBookingData.Read())
                                    {

                                        oldBookDataTotal = oldBookingData.GetDouble(0);

                                    }

                                    totalValue = oldBookDataTotal;

                                    showProfits s = new showProfits(totalValue);
                                    s.Show();
                                }
                                catch (Exception)
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                    r.Show();
                                }

                            }

                        }

                    }
                    else if (comboBox2.Text == "February")
                    {

                        try
                        {
                            double bookingDataTotal = 0;
                            double oldBookDataTotal = 0;
                            string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '02'";

                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }

                            string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '02'";
                            MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                            while (oldBookingData.Read())
                            {

                                oldBookDataTotal = oldBookingData.GetDouble(0);

                            }

                            totalValue = bookingDataTotal + oldBookDataTotal;

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            try
                            {
                                double bookingDataTotal = 0;
                                string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '02'";

                                MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                                while (bookingData.Read())
                                {

                                    bookingDataTotal = bookingData.GetDouble(0);
                                }
                                totalValue = bookingDataTotal;
                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    double oldBookDataTotal = 0;
                                    string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '02'";
                                    MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                                    while (oldBookingData.Read())
                                    {

                                        oldBookDataTotal = oldBookingData.GetDouble(0);

                                    }

                                    totalValue = oldBookDataTotal;

                                    showProfits s = new showProfits(totalValue);
                                    s.Show();
                                }
                                catch (Exception)
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                    r.Show();
                                }

                            }

                        }

                    }
                    else if (comboBox2.Text == "March")
                    {

                        try
                        {
                            double bookingDataTotal = 0;
                            double oldBookDataTotal = 0;
                            string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '03'";

                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }

                            string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '03'";
                            MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                            while (oldBookingData.Read())
                            {

                                oldBookDataTotal = oldBookingData.GetDouble(0);

                            }

                            totalValue = bookingDataTotal + oldBookDataTotal;

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            try
                            {
                                double bookingDataTotal = 0;
                                string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '03'";

                                MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                                while (bookingData.Read())
                                {

                                    bookingDataTotal = bookingData.GetDouble(0);
                                }
                                totalValue = bookingDataTotal;
                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    double oldBookDataTotal = 0;
                                    string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '03'";
                                    MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                                    while (oldBookingData.Read())
                                    {

                                        oldBookDataTotal = oldBookingData.GetDouble(0);

                                    }

                                    totalValue = oldBookDataTotal;

                                    showProfits s = new showProfits(totalValue);
                                    s.Show();
                                }
                                catch (Exception)
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                    r.Show();
                                }

                            }

                        }

                    }
                    else if (comboBox2.Text == "April")
                    {

                        try
                        {
                            double bookingDataTotal = 0;
                            double oldBookDataTotal = 0;
                            string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '04'";

                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }

                            string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '04'";
                            MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                            while (oldBookingData.Read())
                            {

                                oldBookDataTotal = oldBookingData.GetDouble(0);

                            }

                            totalValue = bookingDataTotal + oldBookDataTotal;

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            try
                            {
                                double bookingDataTotal = 0;
                                string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '04'";

                                MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                                while (bookingData.Read())
                                {

                                    bookingDataTotal = bookingData.GetDouble(0);
                                }
                                totalValue = bookingDataTotal;
                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    double oldBookDataTotal = 0;
                                    string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '04'";
                                    MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                                    while (oldBookingData.Read())
                                    {

                                        oldBookDataTotal = oldBookingData.GetDouble(0);

                                    }

                                    totalValue = oldBookDataTotal;

                                    showProfits s = new showProfits(totalValue);
                                    s.Show();
                                }
                                catch (Exception)
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                    r.Show();
                                }

                            }
                        }
                    }
                    else if (comboBox2.Text == "May")
                    {

                        try
                        {
                            double bookingDataTotal = 0;
                            double oldBookDataTotal = 0;
                            string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '05'";

                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }

                            string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '05'";
                            MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                            while (oldBookingData.Read())
                            {

                                oldBookDataTotal = oldBookingData.GetDouble(0);

                            }

                            totalValue = bookingDataTotal + oldBookDataTotal;

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            try
                            {
                                double bookingDataTotal = 0;
                                string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '05'";

                                MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                                while (bookingData.Read())
                                {

                                    bookingDataTotal = bookingData.GetDouble(0);
                                }
                                totalValue = bookingDataTotal;
                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    double oldBookDataTotal = 0;
                                    string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '05'";
                                    MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                                    while (oldBookingData.Read())
                                    {

                                        oldBookDataTotal = oldBookingData.GetDouble(0);

                                    }

                                    totalValue = oldBookDataTotal;

                                    showProfits s = new showProfits(totalValue);
                                    s.Show();
                                }
                                catch (Exception)
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                    r.Show();
                                }

                            }
                        }
                    }
                    else if (comboBox2.Text == "June")
                    {
                        try
                        {
                            double bookingDataTotal = 0;
                            double oldBookDataTotal = 0;
                            string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '06'";

                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }

                            string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '06'";
                            MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                            while (oldBookingData.Read())
                            {

                                oldBookDataTotal = oldBookingData.GetDouble(0);

                            }

                            totalValue = bookingDataTotal + oldBookDataTotal;

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            try
                            {
                                double bookingDataTotal = 0;
                                string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '06'";

                                MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                                while (bookingData.Read())
                                {

                                    bookingDataTotal = bookingData.GetDouble(0);
                                }
                                totalValue = bookingDataTotal;
                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    double oldBookDataTotal = 0;
                                    string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '06'";
                                    MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                                    while (oldBookingData.Read())
                                    {

                                        oldBookDataTotal = oldBookingData.GetDouble(0);

                                    }

                                    totalValue = oldBookDataTotal;

                                    showProfits s = new showProfits(totalValue);
                                    s.Show();
                                }
                                catch (Exception)
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                    r.Show();
                                }

                            }
                        }
                    }
                    else if (comboBox2.Text == "July")
                    {

                        try
                        {
                            double bookingDataTotal = 0;
                            double oldBookDataTotal = 0;
                            string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '07'";

                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }

                            string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '07'";
                            MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                            while (oldBookingData.Read())
                            {

                                oldBookDataTotal = oldBookingData.GetDouble(0);

                            }

                            totalValue = bookingDataTotal + oldBookDataTotal;

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            try
                            {
                                double bookingDataTotal = 0;
                                string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '07'";

                                MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                                while (bookingData.Read())
                                {

                                    bookingDataTotal = bookingData.GetDouble(0);
                                }
                                totalValue = bookingDataTotal;
                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    double oldBookDataTotal = 0;
                                    string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '07'";
                                    MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                                    while (oldBookingData.Read())
                                    {

                                        oldBookDataTotal = oldBookingData.GetDouble(0);

                                    }

                                    totalValue = oldBookDataTotal;

                                    showProfits s = new showProfits(totalValue);
                                    s.Show();
                                }
                                catch (Exception)
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                    r.Show();
                                }

                            }
                        }
                    }
                    else if (comboBox2.Text == "August")
                    {

                        try
                        {
                            double bookingDataTotal = 0;
                            double oldBookDataTotal = 0;
                            string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '08'";

                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }

                            string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '08'";
                            MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                            while (oldBookingData.Read())
                            {

                                oldBookDataTotal = oldBookingData.GetDouble(0);

                            }

                            totalValue = bookingDataTotal + oldBookDataTotal;

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            try
                            {
                                double bookingDataTotal = 0;
                                string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '08'";

                                MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                                while (bookingData.Read())
                                {

                                    bookingDataTotal = bookingData.GetDouble(0);
                                }
                                totalValue = bookingDataTotal;
                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    double oldBookDataTotal = 0;
                                    string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '08'";
                                    MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                                    while (oldBookingData.Read())
                                    {

                                        oldBookDataTotal = oldBookingData.GetDouble(0);

                                    }

                                    totalValue = oldBookDataTotal;

                                    showProfits s = new showProfits(totalValue);
                                    s.Show();
                                }
                                catch (Exception)
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                    r.Show();
                                }

                            }
                        }
                    }
                    else if (comboBox2.Text == "September")
                    {
                        try
                        {
                            double bookingDataTotal = 0;
                            double oldBookDataTotal = 0;
                            string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '09'";

                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }

                            string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '09'";
                            MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                            while (oldBookingData.Read())
                            {

                                oldBookDataTotal = oldBookingData.GetDouble(0);

                            }

                            totalValue = bookingDataTotal + oldBookDataTotal;

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            try
                            {
                                double bookingDataTotal = 0;
                                string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '09'";

                                MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                                while (bookingData.Read())
                                {

                                    bookingDataTotal = bookingData.GetDouble(0);
                                }
                                totalValue = bookingDataTotal;
                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    double oldBookDataTotal = 0;
                                    string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '09'";
                                    MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                                    while (oldBookingData.Read())
                                    {

                                        oldBookDataTotal = oldBookingData.GetDouble(0);

                                    }

                                    totalValue = oldBookDataTotal;

                                    showProfits s = new showProfits(totalValue);
                                    s.Show();
                                }
                                catch (Exception)
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                    r.Show();
                                }

                            }
                        }
                    }
                    else if (comboBox2.Text == "October")
                    {

                        try
                        {
                            double bookingDataTotal = 0;
                            double oldBookDataTotal = 0;
                            string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '10'";

                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }

                            string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '10'";
                            MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                            while (oldBookingData.Read())
                            {

                                oldBookDataTotal = oldBookingData.GetDouble(0);

                            }

                            totalValue = bookingDataTotal + oldBookDataTotal;

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            try
                            {
                                double bookingDataTotal = 0;
                                string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '10'";

                                MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                                while (bookingData.Read())
                                {

                                    bookingDataTotal = bookingData.GetDouble(0);
                                }
                                totalValue = bookingDataTotal;
                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    double oldBookDataTotal = 0;
                                    string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '10'";
                                    MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                                    while (oldBookingData.Read())
                                    {

                                        oldBookDataTotal = oldBookingData.GetDouble(0);

                                    }

                                    totalValue = oldBookDataTotal;

                                    showProfits s = new showProfits(totalValue);
                                    s.Show();
                                }
                                catch (Exception)
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                    r.Show();
                                }

                            }
                        }
                    }
                    else if (comboBox2.Text == "November")
                    {
                        try
                        {
                            double bookingDataTotal = 0;
                            double oldBookDataTotal = 0;
                            string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '11'";

                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }

                            string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '11'";
                            MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                            while (oldBookingData.Read())
                            {

                                oldBookDataTotal = oldBookingData.GetDouble(0);

                            }

                            totalValue = bookingDataTotal + oldBookDataTotal;

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            try
                            {
                                double bookingDataTotal = 0;
                                string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '11'";

                                MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                                while (bookingData.Read())
                                {

                                    bookingDataTotal = bookingData.GetDouble(0);
                                }
                                totalValue = bookingDataTotal;
                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    double oldBookDataTotal = 0;
                                    string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '11'";
                                    MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                                    while (oldBookingData.Read())
                                    {

                                        oldBookDataTotal = oldBookingData.GetDouble(0);

                                    }

                                    totalValue = oldBookDataTotal;

                                    showProfits s = new showProfits(totalValue);
                                    s.Show();
                                }
                                catch (Exception)
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                    r.Show();
                                }
                            }
                        }
                    }
                    else if (comboBox2.Text == "December") {

                        try
                        {
                            double bookingDataTotal = 0;
                            double oldBookDataTotal = 0;
                            string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '12'";

                            MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                            while (bookingData.Read())
                            {

                                bookingDataTotal = bookingData.GetDouble(0);
                            }

                            string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '12'";
                            MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                            while (oldBookingData.Read())
                            {

                                oldBookDataTotal = oldBookingData.GetDouble(0);

                            }

                            totalValue = bookingDataTotal + oldBookDataTotal;

                            showProfits s = new showProfits(totalValue);
                            s.Show();

                        }
                        catch (Exception)
                        {
                            try
                            {
                                double bookingDataTotal = 0;
                                string bookingdata = "SELECT SUM(advancedAmount) FROM booking WHERE month(rentedDate) = '12'";

                                MySqlDataReader bookingData = cClass.SelectQueryOutMsg(bookingdata);

                                while (bookingData.Read())
                                {

                                    bookingDataTotal = bookingData.GetDouble(0);
                                }
                                totalValue = bookingDataTotal;
                                showProfits s = new showProfits(totalValue);
                                s.Show();
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    double oldBookDataTotal = 0;
                                    string oldbookData = "SELECT SUM(TotalAmount) FROM oldbookdata WHERE month(rentalDate) = '12'";
                                    MySqlDataReader oldBookingData = cClass.SelectQueryOutMsg(oldbookData);

                                    while (oldBookingData.Read())
                                    {

                                        oldBookDataTotal = oldBookingData.GetDouble(0);

                                    }

                                    totalValue = oldBookDataTotal;

                                    showProfits s = new showProfits(totalValue);
                                    s.Show();
                                }
                                catch (Exception)
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("No data for Both.");
                                    r.Show();
                                }
                            }
                        }

                    }           
                }
            }          
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
                
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
           
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
