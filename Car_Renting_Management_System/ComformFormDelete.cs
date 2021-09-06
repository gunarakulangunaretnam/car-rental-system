using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml;

namespace Car_Renting_Management_System
{
    public partial class ComformFormDelete : Form
    {

        string heading = "";
        string query = "";
        string id = "";
        string type = "";
        public ComformFormDelete(string headingP,string queryP,string idP)
        {
            InitializeComponent();
            heading = headingP;
            query = queryP;
            id = idP;
            
        }

        public ComformFormDelete(string headingP, string queryP, string idP,string typeP)
        {
            InitializeComponent();
            heading = headingP;
            query = queryP;
            id = idP;
            type = typeP;

        }

        public ComformFormDelete(string headingP, string queryP)
        {
            InitializeComponent();
            heading = headingP;
            query = queryP;
           
        }

        CURDQueryClass c = new CURDQueryClass();
        int canMove;
        int xCor;
        int yCor;

        private void ComformFormDelete_Load(object sender, EventArgs e)
        {
            label2.Text = heading;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
         
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }



        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {


            if (type == "carDeletion")
            {

                label2.Text = heading;
                c.CUDOutMsg(query);

                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string txt = "The Car ID " + id + " was Deleted on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString();
                File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);
                SuccessMSGBox s = new SuccessMSGBox("Deleted Successfully.");
                s.ShowDialog();
                this.Hide();


            }
            else if (type == "customerDeletion")
            {


                int customerDelete = 0;

                string sqlCodetoselect = "SELECT deleteCustomer FROM systemcontroller";
                MySqlDataReader data = c.SelectQueryOutMsg(sqlCodetoselect);


                while (data.Read())
                {

                    customerDelete = data.GetInt32(0);

                }

                int finalCus = customerDelete + 1;

                string sqlCodetoUpate = "UPDATE systemcontroller SET deleteCustomer='" + finalCus + "'";
                c.CUDOutMsg(sqlCodetoUpate);

                c.CUDOutMsg(query);
                SuccessMSGBox s = new SuccessMSGBox("Deleted Successfully.");

                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string txt = "The Customer ID " + id + " was Deleted on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString();
                File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

                s.ShowDialog();
                this.Hide();
            }
            else if (type == "driverDeletion") {




                int customerDelete = 0;

                string sqlCodetoselect = "SELECT deleteDriver FROM systemcontroller";
                MySqlDataReader data = c.SelectQueryOutMsg(sqlCodetoselect);


                while (data.Read())
                {

                    customerDelete = data.GetInt32(0);

                }

                int finalCus = customerDelete + 1;

                string sqlCodetoUpate = "UPDATE systemcontroller SET deleteDriver='" + finalCus + "'";
                c.CUDOutMsg(sqlCodetoUpate);

                c.CUDOutMsg(query);
                SuccessMSGBox s = new SuccessMSGBox("Deleted Successfully.");

                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string txt = "The Driver ID " + id + " was Deleted on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString();
                File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

                s.ShowDialog();
                this.Hide();

            } else if (type=="databaseDeletion") {

               
                label2.Text = heading;
                c.CUDOutMsg(query);
                SuccessMSGBox s = new SuccessMSGBox("Deleted Successfully.");
                s.ShowDialog();
                this.Hide();

                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path + "\\CRMS\\databaseController.xml");

                xmlDoc.SelectSingleNode("databaseDeleted").InnerText = "yes";

                xmlDoc.Save(path + "\\CRMS\\databaseController.xml");

                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string txt = "The Database was Deleted on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString();
                File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);
                
            }
            else
            {

                label2.Text = heading;
                c.CUDOutMsg(query);
                SuccessMSGBox s = new SuccessMSGBox("Deleted Successfully.");
                s.ShowDialog();
                this.Hide();

            }
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - xCor, MousePosition.Y - yCor);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            
            canMove = 1;
            xCor = e.X;
            yCor = e.Y;
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            canMove = 0;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove == 1)
            {

                this.SetDesktopLocation(MousePosition.X - 350, MousePosition.Y - 17);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            canMove = 1;
            xCor = e.X;
            yCor = e.Y;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
