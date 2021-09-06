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
    public partial class OldBookInfo : Form
    {
        public OldBookInfo()
        {
            InitializeComponent();
        }

        CURDQueryClass curdFunction = new CURDQueryClass();

        private void OldBookInfo_Load(object sender, EventArgs e)
        {

            getDataFromTheDBForReturned();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Returned")
            {


                getDataFromTheDBForReturned();


            }
            else if (comboBox1.Text == "Pendding")
            {


                getDataFromTheDBForPendding();
            }

        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Returned")
            {

                if (bunifuTextbox1.text == string.Empty)
                {

                    getDataFromTheDBForReturned();

                }
                else if (comboBox2.Text == "Order ID" && bunifuTextbox1.text != string.Empty)
                {

                    string query = "SELECT * FROM oldbookdata WHERE oderId LIKE '" + bunifuTextbox1.text + "%'";

                    getDataFromTheDBForReturnedSearching(query);

                }
                else if (comboBox2.Text == "Customer ID")
                {

                    string query = "SELECT * FROM oldbookdata WHERE cusId LIKE '" + bunifuTextbox1.text + "%'";
                    getDataFromTheDBForReturnedSearching(query);

                }
                else if (comboBox2.Text == "Car ID")
                {

                    string query = "SELECT * FROM oldbookdata WHERE carID LIKE '" + bunifuTextbox1.text + "%'";
                    getDataFromTheDBForReturnedSearching(query);

                }

            }



            if (comboBox1.Text == "Pendding")
            {

                if (bunifuTextbox1.text == string.Empty)
                {

                    getDataFromTheDBForPendding();

                }
                else if (comboBox2.Text == "Order ID" && bunifuTextbox1.text != string.Empty)
                {

                    string query = "SELECT * FROM booking WHERE orderId LIKE'" + bunifuTextbox1.text + "%'";
                    getDataFromTheDBForPenddingSerching(query);


                }
                else if (comboBox2.Text == "Customer ID" && bunifuTextbox1.text != string.Empty)
                {

                    string query = "SELECT * FROM booking  WHERE cusId LIKE '" + bunifuTextbox1.text + "%'";
                    getDataFromTheDBForPenddingSerching(query);

                }
                else if (comboBox2.Text == "Car ID" && bunifuTextbox1.text != string.Empty)
                {


                    string query = "SELECT * FROM booking WHERE carId LIKE '" + bunifuTextbox1.text + "%'";
                    getDataFromTheDBForPenddingSerching(query);

                }

            }

        }

        public void getDataFromTheDBForPendding()
        {

            DataTable dTable1 = new DataTable();
            dTable1.Columns.Add("Order ID");
            dTable1.Columns.Add("Car ID");
            dTable1.Columns.Add("Customer ID");
            dTable1.Columns.Add("Driver ID");
            dTable1.Columns.Add("Rented Date");
            dTable1.Columns.Add("Return Date");
            dTable1.Columns.Add("Basis");
            dTable1.Columns.Add("Duration");
            dTable1.Columns.Add("Ad.Paid.Amount");
            dTable1.Columns.Add("Blance");
            dTable1.Columns.Add("Driver Salary");
            dTable1.Columns.Add("Total Amount");
            dTable1.Columns.Add("Odometer Value");
            dTable1.Columns.Add("Total Given Mileage");
            dTable1.Columns.Add("Discount Amount");
            dTable1.Columns.Add("Discount Percentage");



            string querySelect = "SELECT * FROM booking";

            MySqlDataReader datas = curdFunction.SelectQuery(querySelect);

            while (datas.Read())
            {

                dTable1.Rows.Add(datas.GetString("OrderId"), datas.GetString("carId"), datas.GetString("cusId"), datas.GetString("driverId"), datas.GetString("rentedDate"), datas.GetString("returnDate"), datas.GetString("basis"), datas.GetString("duration"), datas.GetString("advancedAmount"), datas.GetString("blance"), datas.GetString("driverAmount"), datas.GetString("totalAmount"), datas.GetString("startMileage"), datas.GetString("totalMileage"), datas.GetString("DiscountAmount"), datas.GetString("DiscountPer"));

            }

            bunifuCustomDataGrid1.DataSource = dTable1;

        }

        public void getDataFromTheDBForReturned()
        {

            DataTable data = new DataTable();
            data.Columns.Add("Order ID");
            data.Columns.Add("Car ID");
            data.Columns.Add("Customer ID");
            data.Columns.Add("Driver ID");
            data.Columns.Add("Rented Date");
            data.Columns.Add("Return Date");
            data.Columns.Add("Duration");
            data.Columns.Add("Advanced Amount");
            data.Columns.Add("Balance Amount");
            data.Columns.Add("Extra Days");
            data.Columns.Add("Extra Days Amount");
            data.Columns.Add("Extra Driver Salary");
            data.Columns.Add("Extra Mileage");
            data.Columns.Add("Extra Mileage Amount");
            data.Columns.Add("Total Amount");
            data.Columns.Add("Discount Amount");
            data.Columns.Add("Discount Percentage");
            data.Columns.Add("Basis");
            data.Columns.Add("Driver Salary");


            string querySelect = "SELECT * FROM oldbookdata";

            MySqlDataReader datas = curdFunction.SelectQuery(querySelect);

            while (datas.Read())
            {

                data.Rows.Add(datas.GetString("oderId"), datas.GetString("carId"), datas.GetString("cusId"), datas.GetString("driverId"), datas.GetString("rentalDate"), datas.GetString("returnedDate"), datas.GetString("Duration"), datas.GetString("advancedAmount"), datas.GetString("balanceAmount"), datas.GetString("extraDay"), datas.GetString("extraDayAmount"), datas.GetString("extradriverSalary"), datas.GetString("extraMileage"), datas.GetString("ExtraMileageAmount"), datas.GetString("TotalAmount"), datas.GetString("DiscountAmount"), datas.GetString("DiscountPer"), datas.GetString("basis"), datas.GetString("driversalary"));

            }

            bunifuCustomDataGrid1.DataSource = data;

        }

        public void getDataFromTheDBForReturnedSearching(string query)
        {

            DataTable data = new DataTable();
            data.Columns.Add("Order ID");
            data.Columns.Add("Car ID");
            data.Columns.Add("Customer ID");
            data.Columns.Add("Driver ID");
            data.Columns.Add("Rented Date");
            data.Columns.Add("Return Date");
            data.Columns.Add("Duration");
            data.Columns.Add("Advanced Amount");
            data.Columns.Add("Balance Amount");
            data.Columns.Add("Extra Days");
            data.Columns.Add("Extra Days Amount");
            data.Columns.Add("Extra Driver Salary");
            data.Columns.Add("Extra Mileage");
            data.Columns.Add("Extra Mileage Amount");
            data.Columns.Add("Total Amount");
            data.Columns.Add("Discount Amount");
            data.Columns.Add("Discount Percentage");
            data.Columns.Add("Basis");
            data.Columns.Add("Driver Salary");


            MySqlDataReader datas = curdFunction.SelectQuery(query);

            while (datas.Read())
            {

                data.Rows.Add(datas.GetString("oderId"), datas.GetString("carId"), datas.GetString("cusId"), datas.GetString("driverId"), datas.GetString("rentalDate"), datas.GetString("returnedDate"), datas.GetString("Duration"), datas.GetString("advancedAmount"), datas.GetString("balanceAmount"), datas.GetString("extraDay"), datas.GetString("extraDayAmount"), datas.GetString("extradriverSalary"), datas.GetString("extraMileage"), datas.GetString("ExtraMileageAmount"), datas.GetString("TotalAmount"), datas.GetString("DiscountAmount"), datas.GetString("DiscountPer"), datas.GetString("basis"), datas.GetString("driversalary"));

            }

            bunifuCustomDataGrid1.DataSource = data;


        }

        public void getDataFromTheDBForPenddingSerching(string query)
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


            MySqlDataReader datas = curdFunction.SelectQuery(query);

            while (datas.Read())
            {

                data.Rows.Add(datas.GetString("OrderId"), datas.GetString("carId"), datas.GetString("cusId"), datas.GetString("driverId"), datas.GetString("rentedDate"), datas.GetString("returnDate"), datas.GetString("basis"), datas.GetString("duration"), datas.GetString("advancedAmount"), datas.GetString("blance"), datas.GetString("driverAmount"), datas.GetString("totalAmount"), datas.GetString("startMileage"), datas.GetString("totalMileage"), datas.GetString("DiscountAmount"), datas.GetString("DiscountPer"));

            }

            bunifuCustomDataGrid1.DataSource = data;

        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void bunifuCustomDataGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void bunifuCustomDataGrid1_CellContextMenuStripChanged(object sender, DataGridViewCellEventArgs e)
        {

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
