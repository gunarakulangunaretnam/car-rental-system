using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Renting_Management_System
{
    public partial class DiscountOptions : Form
    {

        double dura = 0;
        double per = 0;
        double driverSala = 0;

        public DiscountOptions(double duration,double perCh,double driverSalary)
        {
            InitializeComponent();

            dura = duration;
            per = perCh;
            driverSala = driverSalary;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();

            textBox1.Text = "no";
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

                if (numericUpDown1.Value == 0)
                {

                    double totalAm = (dura * per)+driverSala;

                    double dis = totalAm * 0 / 100;

                    double ba = totalAm - dis;

                    label13.Text = "0%";
                    label11.Text = "Rs:" + ba;
                    label9.Text = "Rs:" + dis;
                    Drawing("Rs:" + totalAm + "", "r");

                }
                else
                {
                    double totalAm = (dura * per)+driverSala;

                    label7.Visible = false;
                    label11.Visible = true;
                    label9.Visible = true;

                    label13.Text = numericUpDown1.Value.ToString() + "%";

                    int percentage = Convert.ToInt32(numericUpDown1.Value);

                    double dis = totalAm * percentage / 100;

                    string t = "Rs: " + totalAm.ToString();
                    Drawing(t);
                    label9.Text = "Rs: " + dis.ToString();
                    label11.Text = "Rs: " + (totalAm - dis).ToString();
                }
            
        }


        int canMo = 0;
        int Xc = 0;
        int Yc = 0;


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            canMo = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canMo = 1;

            Xc = e.X;
            Yc = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMo==1) {
                
                this.SetDesktopLocation(MousePosition.X - Xc, MousePosition.Y - Yc);
            }
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
           
                label7.Visible = false;
                label11.Visible = true;
                label9.Visible = true;

                if (numericUpDown1.Text == "")
                {

                double totalAm = (dura * per)+driverSala;

                double dis = totalAm * 0 / 100;

                    double ba = totalAm - dis;

                    label13.Text = "0%";
                    label11.Text = "Rs:" + ba;
                    label9.Text = "Rs:" + dis;
                    Drawing("Rs:" + totalAm + "", "r");

                }
                else if (numericUpDown1.Value == 0) {

                double totalAm = (dura * per)+driverSala;

                double dis = totalAm * 0 / 100;

                    double ba = totalAm - dis;

                    label13.Text = "0%";
                    label11.Text = "Rs:" + ba;
                    label9.Text = "Rs:" + dis;
                    Drawing("Rs:" + totalAm + "", "r");

                }
                else
                {
                     double totalAm = (dura * per)+driverSala;
                     label13.Text = numericUpDown1.Value.ToString() + "%";

                    int percentage = Convert.ToInt32(numericUpDown1.Value);

                    double dis = totalAm * percentage / 100;
                    string t = "Rs: " + totalAm.ToString();

                    Drawing(t);

                    label9.Text = "Rs: " + dis.ToString();
                    label11.Text = "Rs: " + (totalAm - dis).ToString();

                
                } 
        }

      
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void DiscountOptions_Load(object sender, EventArgs e)
        {

            double totalAm = (dura * per)+driverSala;
            label7.Text = "Rs: " + totalAm;
            double dis = totalAm * 0 / 100;

            double ba = totalAm - dis;

            label13.Text = "0%";
            label11.Text = "Rs:" + ba;
            label9.Text = "Rs:" + dis;

            textBox1.Visible = false;
            

        }

        private void DiscountOptions_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

          
            
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void panel2_Paint_2(object sender, PaintEventArgs e)
        {
           
        }

        public void Drawing(string value) {


            SolidBrush so = new SolidBrush(Color.SpringGreen);
            Graphics g = panel2.CreateGraphics();
            g.Clear(panel2.BackColor);
            FontFamily ff = new FontFamily("Arial");
            System.Drawing.Font font = new System.Drawing.Font(ff, 15);
            g.DrawString(value, font, so, new PointF(10, 20));

          
            Pen p = new Pen(Color.Red, 3);
            g.DrawLine(p, 100, 38, 07, 25);
            g.Dispose();
        
        }

        public void Drawing(string value,string td)
        {


            SolidBrush so = new SolidBrush(Color.SpringGreen);
            Graphics g = panel2.CreateGraphics();
            g.Clear(panel2.BackColor);
            FontFamily ff = new FontFamily("Arial");
            System.Drawing.Font font = new System.Drawing.Font(ff, 15);
            g.DrawString(value, font, so, new PointF(10, 20));


           
        }

      

        private void bunifuCustomTextbox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            canMo = 0;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMo == 1) {

                this.SetDesktopLocation(MousePosition.X - 360, MousePosition.Y - 18);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            canMo = 1;
            Xc = e.X;
            Yc = e.Y;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bookForm b = new bookForm();
            this.Hide();
            textBox1.Text = "yes";
        }
    }
}
