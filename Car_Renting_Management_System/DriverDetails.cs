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
    public partial class DriverDetails : Form
    {
        string driID = "";

        public DriverDetails(string driverId,string orderId)
        {
            InitializeComponent();

            label10.Text = orderId.ToString();
            driID = driverId;

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        int Xco = 0;
        int Yco = 0;
        int can = 0;


        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            can = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Xco = e.X;
            Yco = e.Y;
            can = 1;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (can == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Xco, MousePosition.Y - Yco);

            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            can = 0;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            can = 1;
            Xco = e.X;
            Yco = e.Y;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (can == 1) {


                this.SetDesktopLocation(MousePosition.X - 350, MousePosition.Y - 18);
            }
        }

        CURDQueryClass c = new CURDQueryClass();

        private void DriverDetails_Load(object sender, EventArgs e)
        {

            string DriverSelection = "SELECT * FROM driver WHERE driverId='" + driID + "'";
            MySqlDataReader datas = c.SelectQueryOutMsg(DriverSelection);

            string gender = "";

            while (datas.Read()) {

                monoFlat_TextBox1.Text = datas.GetString(0);
                monoFlat_TextBox2.Text = datas.GetString(1);
                monoFlat_TextBox3.Text = datas.GetString(2);
                monoFlat_TextBox4.Text = datas.GetString(3);
                monoFlat_TextBox5.Text = datas.GetString(4);
                monoFlat_TextBox6.Text = datas.GetString(5);
                monoFlat_TextBox7.Text = datas.GetString(6);
                gender= datas.GetString(7);

            }
            if (gender == "M") {


                monoFlat_TextBox8.Text = "Male";

            }else if (gender == "F")
            {
                monoFlat_TextBox8.Text = "Female";

            }

        }
    }
}
