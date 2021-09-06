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
    public partial class OrderAvaliable : Form
    {
        public OrderAvaliable()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            monoFlat_TextBox1.Text = "";
            this.Hide();
        }

        int canMove;
        int xCo;
        int yCo;

        CURDQueryClass curdFunction = new CURDQueryClass();

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            canMove = 0;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1) {

                this.SetDesktopLocation(MousePosition.X - xCo, MousePosition.Y - yCo);

            }

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            xCo = e.X;
            yCo = e.Y;
        }

        private void OrderAvaliable_Load(object sender, EventArgs e)
        {
            bunifuFlatButton1.Enabled = false;
            getDataFromTheDB();
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

                data.Rows.Add(datas.GetString("OrderId"), datas.GetString("carId"), datas.GetString("cusId"),datas.GetString("driverId"), datas.GetString("rentedDate"), datas.GetString("returnDate"), datas.GetString("basis"), datas.GetString("duration"),datas.GetString("advancedAmount"), datas.GetString("blance"),datas.GetString("driverAmount"), datas.GetString("totalAmount"), datas.GetString("startMileage"),datas.GetString("totalMileage"),datas.GetString("DiscountAmount"),datas.GetString("DiscountPer"));

            }

            bunifuCustomDataGrid1.DataSource = data;

        }
        

        private void bunifuCustomDataGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          

            
           
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {

            if (bunifuTextbox1.text != string.Empty)
            {

                ComboBoxSearch(bunifuTextbox1.text);
            }
            else {


                getDataFromTheDB();
            }
                 
        }

        public void ComboBoxSearch(string status)
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

            
            string queryForBrand = "SELECT * FROM booking WHERE orderId like'" + status + "%'";
            MySqlDataReader dataBrand = curdFunction.SelectQuery(queryForBrand);

                while (dataBrand.Read())
                {

                dTable1.Rows.Add(dataBrand.GetString("OrderId"), dataBrand.GetString("carId"), dataBrand.GetString("cusId"), dataBrand.GetString("driverId"), dataBrand.GetString("rentedDate"), dataBrand.GetString("returnDate"), dataBrand.GetString("basis"), dataBrand.GetString("duration"), dataBrand.GetString("advancedAmount"), dataBrand.GetString("blance"), dataBrand.GetString("driverAmount"), dataBrand.GetString("totalAmount"), dataBrand.GetString("startMileage"), dataBrand.GetString("totalMileage"), dataBrand.GetString("DiscountAmount"), dataBrand.GetString("DiscountPer"));

            }

           

            bunifuCustomDataGrid1.DataSource = dTable1;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 455, MousePosition.Y - 17);

            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1_MouseDown(sender, e);
        }

        public string current = "";

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bunifuCustomDataGrid1.CurrentCell.ColumnIndex.Equals(0))
            {

                current = bunifuCustomDataGrid1.CurrentCell.Value.ToString();

                monoFlat_TextBox1.Text = current;

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            monoFlat_TextBox1.Text = "";
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void monoFlat_TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (monoFlat_TextBox1.Text != "")
            {

                bunifuFlatButton1.Enabled = true;

            }
            else {

                bunifuFlatButton1.Enabled = false;

            }
        }
    }
}
