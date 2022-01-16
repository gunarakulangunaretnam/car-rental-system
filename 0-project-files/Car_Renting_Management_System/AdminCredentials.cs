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
    public partial class AdminCredentials : Form
    {
        public AdminCredentials()
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

        int canM = 0;
        int XC = 0;
        int YC = 0;

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (canM == 1) {

                this.SetDesktopLocation(MousePosition.X-XC,MousePosition.Y-YC);
            }
            
        }

        private void panel4_MouseUp(object sender, MouseEventArgs e)
        {
            canM = 0;
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            canM = 1;
            XC = e.X;
            YC = e.Y;
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            AdminUsernameChange auc = new AdminUsernameChange();
            auc.ShowDialog();
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            AdminPasswordChange apd = new AdminPasswordChange();
            apd.ShowDialog();
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            canM = 1;
            XC = e.X;
            YC = e.Y;
        }

        private void label4_MouseUp(object sender, MouseEventArgs e)
        {
            canM = 0;
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            if (canM == 1)
            {

                this.SetDesktopLocation(MousePosition.X - XC, MousePosition.Y - YC);
            }
        }
    }
}
