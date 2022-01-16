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
    public partial class CarAvaliableForm : Form
    {
        public CarAvaliableForm()
        {
            InitializeComponent();
        }

        public CarAvaliableForm(string id)
        {
            InitializeComponent();

            if (id != "")
            {

                monoFlat_TextBox1.Text = id;

            }

        }

        CURDQueryClass curdClass = new CURDQueryClass();

        int canMove;
        int Xval;
        int Yval;

        private void CarAvaliableForm_Load(object sender, EventArgs e)
        {
            getDataFromTheDB();
            bunifuFlatButton1.Enabled = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            monoFlat_TextBox1.Text = "";
            this.Close();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            canMove = 0;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
           
            if (canMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Xval, MousePosition.Y - Yval);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            Xval = e.X;
            Yval = e.Y;

        }

        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {

            if (bunifuCustomTextbox1.Text == string.Empty)
            {

                getDataFromTheDB();
                
            }
            else
            {
                ComboBoxSearch(bunifuCustomTextbox1.Text,"carId");
              

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
                ComboBoxSearch(bunifuCustomTextbox2.Text, "carBrand");
            

            }

            if (System.Text.RegularExpressions.Regex.IsMatch(bunifuCustomTextbox2.Text, @"[^a-zA-Z \s]"))
            {
                char[] chars = bunifuCustomTextbox2.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The brand name must be in text format.\nNumbers and symbols are not allowed.");
                er.Show();
                bunifuCustomTextbox2.Text = charsToStr.Remove(charsToStr.Length - 1);


            }

        }


        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string position;

            if (bunifuCustomDataGrid1.CurrentCell.ColumnIndex.Equals(0))
            {

                position = bunifuCustomDataGrid1.CurrentCell.Value.ToString();

                string query = "SELECT carId FROM car WHERE carId='" + position + "'";
                MySqlDataReader data = curdClass.SelectQuery(query);

                while (data.Read())
                {

                    monoFlat_TextBox1.Text = data.GetString(0);
                    
                   

                }
         
            }

        }
      

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
           
            bookForm car = new bookForm();
            this.Hide();
        }


        //Methods

        public void getDataFromTheDB()
        {

            DataTable data = new DataTable();
            data.Columns.Add("Car ID");
            data.Columns.Add("Model");
            data.Columns.Add("Brand");
            data.Columns.Add("Make Year");
            data.Columns.Add("Color");
            data.Columns.Add("Daily Rate");
            data.Columns.Add("Monthly Rate");
            data.Columns.Add("Status");
            data.Columns.Add("Odometer type");
            data.Columns.Add("Odometer Value");

            string querySelect = "SELECT * FROM car WHERE carStatus='IN'";

            MySqlDataReader datas = curdClass.SelectQuery(querySelect);

            while (datas.Read())
            {

                data.Rows.Add(datas.GetString(0), datas.GetString(1), datas.GetString(2), datas.GetString(3), datas.GetString(4), datas.GetString(5), datas.GetString(6), datas.GetString(7),datas.GetString(8),datas.GetString(9));

            }

            bunifuCustomDataGrid1.DataSource = data;
        }


        public void ComboBoxSearch(string status,string where)
        {



            DataTable dTable1 = new DataTable();
            dTable1.Columns.Add("Car ID");
            dTable1.Columns.Add("Model");
            dTable1.Columns.Add("Brand");
            dTable1.Columns.Add("Make Year");
            dTable1.Columns.Add("Color");
            dTable1.Columns.Add("Daily Rate");
            dTable1.Columns.Add("Monthly Rate");
            dTable1.Columns.Add("Status");
            dTable1.Columns.Add("Odometer type");
            dTable1.Columns.Add("Odometer Value");




            string queryForBrand = "SELECT * FROM car WHERE "+where+" like '"+status+"%' and carStatus='IN'";
           MySqlDataReader dataBrand = curdClass.SelectQuery(queryForBrand);
           
           while (dataBrand.Read())
           {
           
               dTable1.Rows.Add(dataBrand.GetString(0), dataBrand.GetString(1), dataBrand.GetString(2), dataBrand.GetString(3), dataBrand.GetString(4), dataBrand.GetString(5), dataBrand.GetString(6), dataBrand.GetString(7), dataBrand.GetString(8), dataBrand.GetString(9));
           
           }
                    
           bunifuCustomDataGrid1.DataSource = dTable1;

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            monoFlat_TextBox1.Text = "";
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - 440, MousePosition.Y - 16);
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            Xval = e.X;
            Yval = e.Y;

        }
    }
}
