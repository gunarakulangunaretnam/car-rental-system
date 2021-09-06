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
    public partial class DriverSelection : Form
    {
        public DriverSelection()
        {
            InitializeComponent();
        }

        public DriverSelection(string id)
        {
            InitializeComponent();

            if (id != "")
            {

                monoFlat_TextBox1.Text = id;

            }
        }

        CURDQueryClass curdClass = new CURDQueryClass();

        private void label3_Click(object sender, EventArgs e)
        {
            monoFlat_TextBox1.Text = "";
            this.Hide();
          
        }

        private void monoFlat_TextBox1_TextChanged(object sender, EventArgs e)
        {
            
            if (monoFlat_TextBox1.Text == string.Empty)
            {

                bunifuFlatButton1.Enabled = false;

            }
            else
            {

                bunifuFlatButton1.Enabled = true;

            }
        }


      

        private void DriverSelection_Load(object sender, EventArgs e)
        {
            bunifuFlatButton1.Enabled = false;
            getDataFromTheDB();
            comboBox1.SelectedIndex = 0;
        }


        public void getDataFromTheDB()
        {

            DataTable data = new DataTable();
            data.Columns.Add("Driver ID");
            data.Columns.Add("Firstname");
            data.Columns.Add("Lastname");
            data.Columns.Add("Address");
            data.Columns.Add("Phone Number");
            data.Columns.Add("Identity Card");
            data.Columns.Add("driving licence No");
            data.Columns.Add("Gender");
            data.Columns.Add("Status");

            string querySelect = "SELECT * FROM driver WHERE driverStatus='IN'";

            MySqlDataReader datas = curdClass.SelectQuery(querySelect);

            while (datas.Read())
            {

                data.Rows.Add(datas.GetString(0), datas.GetString(1), datas.GetString(2), datas.GetString(3), datas.GetString(4), datas.GetString(5), datas.GetString(6), datas.GetString(7),datas.GetString(8));

            }

            bunifuCustomDataGrid1.DataSource = data;
        }


        public void ComboBoxSearch(string status, string where)
        {



            DataTable dTable1 = new DataTable();
            dTable1.Columns.Add("Driver ID");
            dTable1.Columns.Add("Firstname");
            dTable1.Columns.Add("Lastname");
            dTable1.Columns.Add("Address");
            dTable1.Columns.Add("Phone Number");
            dTable1.Columns.Add("Identity Card");
            dTable1.Columns.Add("driving licence No");
            dTable1.Columns.Add("Gender");
            dTable1.Columns.Add("Status");


            if (status == "All")
            {

                string queryForAll = "SELECT * FROM driver WHERE driverStatus='IN'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForAll);


                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7), reData.GetString(8));

                }

            }
            else if (status == "Male")
            {

                string queryForHired = "SELECT * FROM driver WHERE driverGender='M' AND driverStatus='IN'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForHired);


                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7), reData.GetString(8));

                }

            }
            else if (status == "Female")
            {


                string queryForAvaliable = "SELECT * FROM driver WHERE driverGender='F' AND driverStatus='IN'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForAvaliable);


                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7), reData.GetString(8));

                }

            }
            else
            {


                string queryForBrand = "SELECT * FROM driver WHERE " + where + " like '" + status + "%' AND driverStatus='IN'";
                MySqlDataReader dataBrand = curdClass.SelectQuery(queryForBrand);

                while (dataBrand.Read())
                {

                    dTable1.Rows.Add(dataBrand.GetString(0), dataBrand.GetString(1), dataBrand.GetString(2), dataBrand.GetString(3), dataBrand.GetString(4), dataBrand.GetString(5), dataBrand.GetString(6), dataBrand.GetString(7),dataBrand.GetString(8));

                }

            }

            bunifuCustomDataGrid1.DataSource = dTable1;

        }

        public void ComboBoxSearch(string status)
        {



            DataTable dTable1 = new DataTable();
            dTable1.Columns.Add("Customer ID");
            dTable1.Columns.Add("Firstname");
            dTable1.Columns.Add("Lastname");
            dTable1.Columns.Add("Address");
            dTable1.Columns.Add("Phone Number");
            dTable1.Columns.Add("Identity Card");
            dTable1.Columns.Add("driving licence No");
            dTable1.Columns.Add("Gender");
            dTable1.Columns.Add("Status");


            if (status == "All")
            {

                string queryForAll = "SELECT * FROM driver WHERE driverStatus='IN'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForAll);


                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7),reData.GetString(8));

                }

            }
            else if (status == "Male")
            {

                string queryForHired = "SELECT * FROM driver WHERE driverGender='M' AND driverStatus='IN'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForHired);


                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7),reData.GetString(8));

                }

            }
            else if (status == "Female")
            {


                string queryForAvaliable = "SELECT * FROM driver WHERE driverGender='F' AND  driverStatus='IN'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForAvaliable);


                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7),reData.GetString(8));

                }

            }

            bunifuCustomDataGrid1.DataSource = dTable1;

        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        int move;
        int xCor;
        int Ycor;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            move = 1;
            xCor = e.X;
            Ycor = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == 1)
            {

                this.SetDesktopLocation(MousePosition.X - xCor, MousePosition.Y - Ycor);
            }

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            move = 0;
        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string position;

            if (bunifuCustomDataGrid1.CurrentCell.ColumnIndex.Equals(0))
            {
                position = bunifuCustomDataGrid1.CurrentCell.Value.ToString();

                string query = "SELECT DriverId FROM Driver WHERE driverId='" + position + "'";
                MySqlDataReader data = curdClass.SelectQuery(query);

                while (data.Read())
                {

                    monoFlat_TextBox1.Text = data.GetString(0);

                }

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            monoFlat_TextBox1.Text = "";
        }

        private void bunifuCustomTextbox1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void bunifuCustomTextbox2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {
             if (bunifuCustomTextbox1.Text == string.Empty) {

                getDataFromTheDB();

            }
            else{

                ComboBoxSearch(bunifuCustomTextbox1.Text, "driverId");
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(bunifuCustomTextbox1.Text, @"[!@#$%^&*\~{}.`,\\;=<|>/?+'\""]"))
            {
                char[] chars = bunifuCustomTextbox1.Text.ToCharArray();
                ErrorMsgBox er = new ErrorMsgBox("You cannot enter symbols like " + chars[chars.Length - 1]);
                er.Show();

                string charsToStr = new string(chars);

                bunifuCustomTextbox1.Text = charsToStr.Remove(charsToStr.Length - 1);

            }

        }

        private void bunifuCustomTextbox2_TextChanged(object sender, EventArgs e)
        {
            if (bunifuCustomTextbox2.Text == string.Empty)
            {

                getDataFromTheDB();

            }
            else
            {

                ComboBoxSearch(bunifuCustomTextbox2.Text, "driverFname");

            }

            if (System.Text.RegularExpressions.Regex.IsMatch(bunifuCustomTextbox2.Text, @"[^a-zA-Z \s]"))
            {
                char[] chars = bunifuCustomTextbox2.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The first name must be in text format.\nNumbers and symbols are not allowed.");
                er.Show();
                bunifuCustomTextbox2.Text = charsToStr.Remove(charsToStr.Length - 1);


            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "All")
            {
                //in this case text All.
                ComboBoxSearch(comboBox1.Text);

            }
            else if (comboBox1.Text == "Male")
            {
                //in this case text Hired.
                ComboBoxSearch(comboBox1.Text);

            }

            else if (comboBox1.Text == "Female")
            {
                //in this case text Avaliable.
                ComboBoxSearch(comboBox1.Text);
            }

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {

            if (move == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 444, MousePosition.Y - 17);
            }


        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            move = 1;
            xCor = e.X;
            Ycor = e.Y;
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            move = 0;
        }
    }
}
