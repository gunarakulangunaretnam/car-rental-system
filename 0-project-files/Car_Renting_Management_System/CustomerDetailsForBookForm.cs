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
    public partial class CustomerDetailsForBookForm : Form
    {
         string value;
        
      
        public CustomerDetailsForBookForm(string id,string orId)
        {
            
            InitializeComponent();
            value = id;
            label10.Text = orId;
            

           
        }

      
        int canMove;
        int xCo;
        int yCo;

        CURDQueryClass curd = new CURDQueryClass();

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CustomerDetailsForBookForm_Load(object sender, EventArgs e)
        {
         
            string sqlCode = "SELECT * FROM customer WHERE customerId='" + value + "'";
            MySqlDataReader da=curd.SelectQueryOutMsg(sqlCode);
            ;

            string gender = "";

            while (da.Read()) {

                monoFlat_TextBox1.Text = da.GetString("customerId");
                monoFlat_TextBox2.Text = da.GetString("customerFname");
                monoFlat_TextBox3.Text = da.GetString("customerLname");
                monoFlat_TextBox4.Text = da.GetString("customerAddress");
                monoFlat_TextBox5.Text = da.GetString("customerPhoneNo");
                monoFlat_TextBox6.Text = da.GetString("customerIdentityCard");
                monoFlat_TextBox7.Text = da.GetString("customerLicence");
                gender = da.GetString("customerGender");
            }

            if (gender == "M")
            {

                monoFlat_TextBox8.Text = "Male";

            }
            else if (gender == "F") {

                monoFlat_TextBox8.Text = "Female";
                    
            }
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

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

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void monoFlat_TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void monoFlat_TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 330, MousePosition.Y - 17);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            xCo = e.X;
            yCo = e.Y;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
