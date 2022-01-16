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
    public partial class CardetailsForBook : Form
    {

        CURDQueryClass curd = new CURDQueryClass();
        string value = "";
        string orderID = "";

        public CardetailsForBook(string id,string orderId)
        {
            InitializeComponent();
            value = id;
            orderID = orderId;


        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        int canmo = 0;
        int xC = 0;
        int yC = 0;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canmo == 1) {

                this.SetDesktopLocation(MousePosition.X - xC, MousePosition.Y - yC);

            }   
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            canmo = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canmo = 1;
            xC = e.X;
            yC = e.Y;

        }

        private void CardetailsForBook_Load(object sender, EventArgs e)
        {
            string sqlCode = "SELECT * FROM car WHERE carId='" + value + "'";
            MySqlDataReader da = curd.SelectQueryOutMsg(sqlCode);
            

            

            while (da.Read())
            {

                monoFlat_TextBox1.Text = da.GetString("carId");
                monoFlat_TextBox2.Text = da.GetString("carModel");
                monoFlat_TextBox3.Text = da.GetString("carBrand");
                monoFlat_TextBox4.Text = da.GetString("carMakeYear");
                monoFlat_TextBox5.Text = da.GetString("carColor");
                monoFlat_TextBox6.Text = da.GetString("carDailyRate");
                monoFlat_TextBox7.Text = da.GetString("carMonthlyRate");
                monoFlat_TextBox9.Text = da.GetString("odometer");

               
            }

            label10.Text = orderID.ToString();
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canmo == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 315, MousePosition.Y - 15);

            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            canmo = 0;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            canmo = 1;
            xC = e.X;
            yC = e.Y;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
