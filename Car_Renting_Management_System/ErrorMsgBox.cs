using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Car_Renting_Management_System
{
    public partial class ErrorMsgBox : Form
    {
        public ErrorMsgBox(string msg)
        {
            InitializeComponent();
            SystemSounds.Hand.Play();
            label2.Text = msg;
        }

        int moveSta;
        int Xval;
        int Yval;

        int moveStaForL;
        int XvalForL;
        int YvalForL;

        private void ErrorMsgBox_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       


        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            moveSta = 1;
            Xval = e.X;
            Yval = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            if (moveSta == 1)
            {

                this.SetDesktopLocation(MousePosition.X - Xval, MousePosition.Y - Yval);
            }

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            moveSta = 0;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            moveStaForL = 1;
            XvalForL = e.X;
            YvalForL = e.Y;

        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveStaForL == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 390, MousePosition.Y - 18);
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            moveStaForL = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
