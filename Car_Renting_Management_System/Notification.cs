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
    public partial class Notification : Form
    {

        string date = "";
        public Notification(string d)
        {
            InitializeComponent();
            date = d;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            getDataFromTheDB();
        }

        CURDQueryClass curdFunction = new CURDQueryClass();

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



            string querySelect = "SELECT * FROM booking WHERE returnDate='" + date+"'";

            MySqlDataReader datas = curdFunction.SelectQuery(querySelect);



            while (datas.Read())
            {

                data.Rows.Add(datas.GetString("OrderId"), datas.GetString("carId"), datas.GetString("cusId"), datas.GetString("driverId"), datas.GetString("rentedDate"), datas.GetString("returnDate"), datas.GetString("basis"), datas.GetString("duration"), datas.GetString("advancedAmount"), datas.GetString("blance"), datas.GetString("driverAmount"), datas.GetString("totalAmount"), datas.GetString("startMileage"), datas.GetString("totalMileage"), datas.GetString("DiscountAmount"), datas.GetString("DiscountPer"));

            }

            bunifuCustomDataGrid1.DataSource = data;

        }
        int rowIndex = 0;
        private void bunifuCustomDataGrid1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Show Car Details", MenuItemNew_Click));
                m.MenuItems.Add(new MenuItem("Show Customer Details", MenuItemNew2_Click));
                m.MenuItems.Add(new MenuItem("Show Driver Details", MenuItemNew3_Click));


                rowIndex = e.RowIndex;

                m.Show(bunifuCustomDataGrid1, bunifuCustomDataGrid1.PointToClient(Cursor.Position));

            }
        }

        private void MenuItemNew_Click(Object sender, System.EventArgs e)
        {

            try
            {
                string indexPosition = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Car ID"].Value.ToString();
                string orderId = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Order ID"].Value.ToString();


                CardetailsForBook cus = new CardetailsForBook(indexPosition, orderId);
                cus.Show();

            }
            catch (Exception)
            {
                ErrorMsgBox r = new ErrorMsgBox("Please select a record");
                r.Show();
            }
        }

        private void MenuItemNew2_Click(Object sender, System.EventArgs e)
        {

            try
            {
                string indexPosition = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Customer ID"].Value.ToString();
                string orderId = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Order ID"].Value.ToString();


                CustomerDetailsForBookForm cus = new CustomerDetailsForBookForm(indexPosition, orderId);
                cus.Show();

            }
            catch (Exception)
            {
                ErrorMsgBox r = new ErrorMsgBox("Please select a record");
                r.Show();
            }

        }
        private void MenuItemNew3_Click(Object sender, System.EventArgs e)
        {
            try
            {
                string indexPosition = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Driver ID"].Value.ToString();
                string orderId = bunifuCustomDataGrid1.Rows[rowIndex].Cells["Order ID"].Value.ToString();

                if (indexPosition == "No-Driver")
                {

                    ErrorMsgBox er = new ErrorMsgBox("It does not have Driver.");
                    er.Show();

                }
                else
                {

                    DriverDetails dri = new DriverDetails(indexPosition, orderId);
                    dri.Show();
                }

            }
            catch (Exception)
            {
                ErrorMsgBox r = new ErrorMsgBox("Please select a record");
                r.Show();
            }

        }

     
    }
}
