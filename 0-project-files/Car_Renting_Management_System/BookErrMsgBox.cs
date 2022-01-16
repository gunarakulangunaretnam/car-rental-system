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
    public partial class BookErrMsgBox : Form
    {
        public BookErrMsgBox(string msg)
        {
            InitializeComponent();
            label2.Text = msg;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        int can;
        int xco;
        int yco;
        
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (can == 1) {

                this.SetDesktopLocation(MousePosition.X - xco, MousePosition.Y - yco);

            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            can = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            can = 1;
            xco = e.X;
            yco = e.Y;
        }

        private void BookErrMsgBox_Load(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Hand.Play();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            can = 0;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            can = 1;
            xco = e.X;
            yco = e.Y;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (can == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 411, MousePosition.Y - 16);

            }
        }
    }
}
