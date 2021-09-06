using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Renting_Management_System
{
    public partial class showProfits : Form
    {
        public showProfits(double total)
        {
            InitializeComponent();

            label2.Text = "RS:" + total.ToString()+"/-";
        }

        public showProfits(double total,double avg)
        {
            InitializeComponent();
            label3.Visible = false;
            label4.Text = "Profits : " + avg.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        int canMove;
        int Xc;
        int Yc;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

            canMove = 1;
            Xc = e.X;
            Yc = e.Y;

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1) {
                
                this.SetDesktopLocation(MousePosition.X - Xc, MousePosition.Y - Yc);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void showProfits_Load(object sender, EventArgs e)
        {

        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 349, MousePosition.Y - 17);
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1_MouseDown(sender,e);
        }
    }
}
