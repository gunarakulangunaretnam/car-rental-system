using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using MySql.Data.MySqlClient;

namespace Car_Renting_Management_System
{
    public partial class MainForm : Form
    {

        int toMove;
        int MValX;
        int MValY;
        string accountType = "";
        
        public MainForm()
        {
            InitializeComponent();
        

        }
        
      

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //When we click the home picture box that is on left top corrner.that will take the program to login page.
            accountType f = new accountType();
            f.Show();
            this.Hide();
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {

            toMove = 0;

        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {

            toMove = 1;
            MValX = e.X;
            MValY = e.Y;

        }
        //when the mouse down that the same time mouse moving.This code will help to move the form.
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {

            if (toMove == 1) {

                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }

        }

        CURDQueryClass c = new CURDQueryClass();
        string d = "";

        private void MainForm_Load(object sender, EventArgs e)
        {
            string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string txt= "----------------------The CRMS System Opened on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString() + "----------------------";
            File.AppendAllText(pathforLog+ "//CRMS//ActivityLog.txt", txt + Environment.NewLine + Environment.NewLine);
           
            mdiConColorChange();
            // sw.WriteLine("The CRMS System started on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString());

            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.Load(path + "\\CRMS\\userStatus.xml");
            accountType = xmlDoc2.SelectSingleNode("status").InnerText;

            
            if (accountType == "Staff")
            {

                bunifuFlatButton7.Visible = false;
            }
            else  if(accountType=="Admin"){

                bunifuFlatButton7.Visible = true;
            }

            label3.Visible = false;

            d = DateTime.Now.ToString("yyyy-MM-dd");

            string selectNotify = "SELECT COUNT(*) FROM booking WHERE returnDate='" + d + "'";

            string total = "";

            MySqlDataReader data = c.SelectQueryOutMsg(selectNotify);

            while (data.Read())
            {


                total = data.GetString(0);
            }

            if (Convert.ToInt32(total) > 0)
            {

                label3.Visible = true;
                label3.Text = total;

            }


        }

     
        private void HeadPanel_MouseDown(object sender, MouseEventArgs e)
        {
            toMove = 1;
            MValX = e.X;
            MValY = e.Y;

        }

        private void HeadPanel_MouseUp(object sender, MouseEventArgs e)
        {
            toMove = 0;
        }

        private void HeadPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (toMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);

            }
        }

      

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            carsForm car = new carsForm();
            car.MdiParent = this; 
            car.Show();
        }


        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            CustomerForm customer = new CustomerForm();
            customer.MdiParent = this;
            customer.Show();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            bookForm book = new bookForm();
            book.MdiParent = this;
            book.Show();

        }

        //This methods for changing the BGColor of MDIContainer.
        void mdiConColorChange() {

            MdiClient ctlMDI;

            // Loop through all of the form's controls looking
            // for the control of type MdiClient.
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;

                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = this.BackColor;
                }
                catch (InvalidCastException exc)
                {
                    // Catch and ignore the error if casting failed.
                }
            }

            
        }

        //When the user does double click the window HeadPanel. Window will respond as following.
        private void HeadPanel_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;

            }
            else if (WindowState == FormWindowState.Maximized){

                WindowState = FormWindowState.Normal;
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            returning re = new returning();
            re.MdiParent = this;
            re.Show();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            OldBookInfo o = new OldBookInfo();
            o.MdiParent = this;
            o.Show();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            profits p = new profits();
            p.MdiParent = this;
            p.Show();
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            settings s = new settings();
            s.MdiParent = this;
            s.Show();
            
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string txt = "----------------------The CRMS System Closed on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString()+ "----------------------";
            File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", Environment.NewLine+ txt + Environment.NewLine + Environment.NewLine +Environment.NewLine + Environment.NewLine + Environment.NewLine);
        }

        private void monoFlat_ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            Drivers dri = new Drivers();
           dri.MdiParent = this;
            dri.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Notification re = new Notification(d);
            re.MdiParent = this;
            re.Show();
        }

     

        private void bunifuFlatButton9_Click_1(object sender, EventArgs e)
        {
            Notification re = new Notification(d);
            re.MdiParent = this;
            re.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OldPrinter old = new OldPrinter("ORD-005", "CUS-001", "poiyt-5656");
            old.Show();
        }
    }
}
