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
    public partial class SuccessMSGBox : Form
    {
        public SuccessMSGBox(string msg)
        {
            InitializeComponent();
            SystemSounds.Asterisk.Play(); 
            label2.Text = msg;
        }

        int toMove;
        int MValX;
        int MValY;


        private void SuccessMSGBox_Load(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            toMove = 1;
            MValX = e.X;
            MValY = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (toMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);

            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            toMove = 0;

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            toMove = 1;
            MValX = e.X;
            MValY = e.Y;
          
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            toMove = 0;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (toMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 250, MousePosition.Y - 16);

            }
        }
    }
}
