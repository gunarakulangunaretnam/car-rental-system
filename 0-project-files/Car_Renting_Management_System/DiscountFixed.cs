using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace Car_Renting_Management_System
{
    public partial class DiscountFixed : Form
    {

        double dura = 0;
        double per = 0;
        double driverSala = 0;

        public DiscountFixed(double duration, double perCh,double dri)
        {
            InitializeComponent();

            dura = duration;
            per = perCh;
            driverSala = dri;

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        int can = 0;
        int Xc = 0;
        int Yc = 0;

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            can = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            can = 1;
            Xc = e.X;
            Yc = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (can == 1) {

                this.SetDesktopLocation(MousePosition.X - Xc, MousePosition.Y - Yc);

            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            can = 0;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (can == 1) {

                this.SetDesktopLocation(MousePosition.X - 360, MousePosition.Y - 18);
            }

            
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            can = 1;
            Xc = e.X;
            Yc = e.Y;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


       
        private void DiscountFixed_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            XmlDocument xml = new XmlDocument();

            xml.Load(path + "\\CRMS\\dicountSettings.xml");

            string discountVAl = xml.SelectSingleNode("dicountSettings/fixedPerVAl").InnerText;

            int disMount = Convert.ToInt32(discountVAl);

            double totalAm = (dura * per)+driverSala;

            double dis = totalAm * disMount / 100;

            double ba = totalAm - dis;


            label2.Text = discountVAl + "%";
            label11.Text = "Rs:" + ba;
            label9.Text = "Rs:" + dis;

           
            timer1.Start();
        }

        public void Drawing(string value)
        {


            SolidBrush so = new SolidBrush(Color.SpringGreen);
            Graphics g = panel2.CreateGraphics();
            g.Clear(panel2.BackColor);
            FontFamily ff = new FontFamily("Arial");
            System.Drawing.Font font = new System.Drawing.Font(ff, 15);
            g.DrawString(value, font, so, new PointF(10, 20));
           

            Pen p = new Pen(Color.Red, 3);
            g.DrawLine(p, 100, 38, 07, 25);
            g.Dispose();

            timer1.Start();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
          
        }


        int timer = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer++;
            
            double totalAm = (dura * per)+driverSala;
            string write = "Rs:" + totalAm;
            Drawing(write);

           
            if (timer == 1) {

                timer1.Stop();
               
            }
            
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "yes";
            this.Hide();
        }
    }
}
