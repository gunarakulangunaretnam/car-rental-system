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
    public partial class StaffCredentials : Form
    {
        public StaffCredentials()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            ChangeUsername cu = new ChangeUsername();
            cu.ShowDialog();
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            changePassword cp = new changePassword();
            cp.ShowDialog();
        }

        int canMove = 0;
        int xCor = 0;
        int yCor = 0;


        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {

            if (canMove == 1) {

                this.SetDesktopLocation(MousePosition.X - xCor, MousePosition.Y - yCor);
            }

        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            xCor = e.X;
            yCor = e.Y;
        }

        private void panel4_MouseLeave(object sender, EventArgs e)
        {
          
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - xCor, MousePosition.Y - yCor);
            }

        }

        private void label4_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            xCor = e.X;
            yCor = e.Y;
        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
