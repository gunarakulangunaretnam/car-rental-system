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
    public partial class accountType : Form
    {
        public accountType()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int canmove = 0;
        int xCor = 0;
        int yCor = 0;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canmove == 1) {

                this.SetDesktopLocation(MousePosition.X - xCor, MousePosition.Y - yCor);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            canmove = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canmove = 1;
            xCor = e.X;
            yCor = e.Y;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::Car_Renting_Management_System.Properties.Resources._21;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.Image = global::Car_Renting_Management_System.Properties.Resources.Untitled_12;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox2.Image = global::Car_Renting_Management_System.Properties.Resources._3;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox2.Image = global::Car_Renting_Management_System.Properties.Resources.fixit_thumb;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path + "\\CRMS\\userStatus.xml");
            xmlDoc.SelectSingleNode("status").InnerText = "Staff";
            xmlDoc.Save(path + "\\CRMS\\userStatus.xml");

            LoginForm log = new LoginForm("Staff Login");
            log.Show();
            this.Hide();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path + "\\CRMS\\userStatus.xml");
            xmlDoc.SelectSingleNode("status").InnerText = "Admin";
            xmlDoc.Save(path + "\\CRMS\\userStatus.xml");
            LoginForm log = new LoginForm("Admin Login");
            log.Show();
            this.Hide();
        }

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            canmove = 1;
            xCor = e.X;
            yCor = e.Y;
        }

        private void label3_MouseUp(object sender, MouseEventArgs e)
        {
            canmove = 0;
        }

        private void label3_MouseMove(object sender, MouseEventArgs e)
        {

            if (canmove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 400, MousePosition.Y - 18);
            }

        }
    }
}
