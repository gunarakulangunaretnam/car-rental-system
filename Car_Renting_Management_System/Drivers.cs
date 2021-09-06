using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Car_Renting_Management_System
{
    public partial class Drivers : Form
    {
        public Drivers()
        {
            InitializeComponent();
        }

        CURDQueryClass curdClass= new CURDQueryClass();
        int totalDriver = 0;
        string DriverID = "";

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Drivers_Load(object sender, EventArgs e)
        {
            getDataFromTheDB();
            buttonToGetBlackAndWhite();
            comboBox1.SelectedIndex = 0;

            getDriverTotalFinal();
        }

        public void getDataFromTheDB()
        {

            DataTable data = new DataTable();
            data.Columns.Add("Driver ID");
            data.Columns.Add("Firstname");
            data.Columns.Add("Lastname");
            data.Columns.Add("Address");
            data.Columns.Add("Phone Number");
            data.Columns.Add("Identity Card");
            data.Columns.Add("Driving Licence No");
            data.Columns.Add("Gender");
            data.Columns.Add("Status");


            string querySelect = "SELECT * FROM driver";

            MySqlDataReader datas = curdClass.SelectQuery(querySelect);

            while (datas.Read())
            {

                data.Rows.Add(datas.GetString(0), datas.GetString(1), datas.GetString(2), datas.GetString(3), datas.GetString(4), datas.GetString(5), datas.GetString(6), datas.GetString(7),datas.GetString(8));

            }

            bunifuCustomDataGrid1.DataSource = data;
        }

        public void buttonToGetBlackAndWhite()
        {

            bunifuFlatButton3.Enabled = false;
            bunifuFlatButton4.Enabled = false;

            this.bunifuFlatButton4.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_Remove_27874_ConvertImage; ;
            this.bunifuFlatButton3.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_db_update_3213_ConvertImage;

        }

        public void getDriverTotlal()
        {

            string qu = "SELECT COUNT(*) FROM driver";
            MySqlDataReader cusQu = curdClass.SelectQueryOutMsg(qu);

            while (cusQu.Read())
            {

                totalDriver = cusQu.GetInt32(0);

            }

        }

        public void getDriverTotalFinal()
        {


            getDriverTotlal();

            int cusdeleteTotal = 0;

            string sqlcodeToSelectDeleteCus = "SELECT deleteDriver FROM systemcontroller";
            MySqlDataReader dataDe = curdClass.SelectQueryOutMsg(sqlcodeToSelectDeleteCus);

            while (dataDe.Read())
            {

                cusdeleteTotal = dataDe.GetInt32(0);

            }

            string cusStr = "";

            int totalCus = totalDriver + cusdeleteTotal + 1;

            if (totalCus < 10)
            {


                cusStr = "DRI-00";


            }
            else if (totalCus >= 10 && totalCus < 100)
            {


                cusStr = "DRI-0";

            }
            else if (totalCus >= 100)
            {


                cusStr = "DRI-";
            }


            bunifuCustomTextbox1.Text = cusStr + totalCus.ToString();


        }

        private void monoFlat_TextBox8_TextChanged(object sender, EventArgs e)
        {

            if (monoFlat_TextBox8.Text.Length == 0)
            {

                buttonToGetBlackAndWhite();
                cleanTextBoxes();
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox8.Text, @"[!@#$%^&*\~{}.`,\\;=<|>/?+'\""]"))
            {
                char[] chars = monoFlat_TextBox8.Text.ToCharArray();
                ErrorMsgBox er = new ErrorMsgBox("You cannot enter symbols like " + chars[chars.Length - 1]);
                er.Show();

                string charsToStr = new string(chars);

                monoFlat_TextBox8.Text = charsToStr.Remove(charsToStr.Length - 1);

            }
        }

        public void cleanTextBoxes()
        {

            bunifuCustomTextbox1.Text = "";
            monoFlat_TextBox2.Text = "";
            monoFlat_TextBox3.Text = "";
            monoFlat_TextBox4.Text = "";
            monoFlat_TextBox5.Text = "";
            monoFlat_TextBox6.Text = "";
            monoFlat_TextBox7.Text = "";
            monoFlat_TextBox8.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;

        }

        private void bunifuCustomTextbox1_Click(object sender, EventArgs e)
        {
            getDriverTotalFinal();
        }

        private void monoFlat_TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox2.Text, @"[^a-zA-Z \s]"))
            {
                char[] chars = monoFlat_TextBox2.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The first name must be in text format.\nNumbers and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox2.Text = charsToStr.Remove(charsToStr.Length - 1);


            }
        }

        private void monoFlat_TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox3.Text, @"[^a-zA-Z \s]"))
            {
                char[] chars = monoFlat_TextBox3.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The last name must be in text format.\nNumbers and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox3.Text = charsToStr.Remove(charsToStr.Length - 1);


            }
        }

        private void monoFlat_TextBox5_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox5.Text, "[^0-9+]"))
            {

                char[] chars = monoFlat_TextBox5.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The phone number must be in digit format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox5.Text = charsToStr.Remove(charsToStr.Length - 1);


            }
        }

        private void monoFlat_TextBox6_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox6.Text, @"[!@#$%^&*\~`{},\\;.=<|>+/?'\""]"))
            {
                char[] chars = monoFlat_TextBox6.Text.ToCharArray();
                ErrorMsgBox er = new ErrorMsgBox("You cannot enter symbols like " + chars[chars.Length - 1]);
                er.Show();

                string charsToStr = new string(chars);

                monoFlat_TextBox6.Text = charsToStr.Remove(charsToStr.Length - 1);

            }
        }

        private void monoFlat_TextBox7_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox7.Text, @"[!@#$%^&*\~`{},\+\;.=<|>/?'\""]"))
            {
                char[] chars = monoFlat_TextBox7.Text.ToCharArray();
                ErrorMsgBox er = new ErrorMsgBox("You cannot enter symbols like " + chars[chars.Length - 1]);
                er.Show();

                string charsToStr = new string(chars);

                monoFlat_TextBox7.Text = charsToStr.Remove(charsToStr.Length - 1);

            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            //Selection
            string query = "SELECT * FROM driver WHERE driverId='" + monoFlat_TextBox8.Text + "'";

            //The data returns back as MySqlDataReader format. That's why we can loop it.
            MySqlDataReader returnData = curdClass.SelectQuery(query);

            string radioVal;
            string dataComeChecker = "no";
            while (returnData.Read())
            {

                bunifuCustomTextbox1.Text = returnData.GetString(0);
                monoFlat_TextBox2.Text = returnData.GetString(1);
                monoFlat_TextBox3.Text = returnData.GetString(2);
                monoFlat_TextBox4.Text = returnData.GetString(3);
                monoFlat_TextBox5.Text = returnData.GetString(4);
                monoFlat_TextBox6.Text = returnData.GetString(5);
                monoFlat_TextBox7.Text = returnData.GetString(6);
                radioVal = returnData.GetString(7);
                DriverID = returnData.GetString(0);
                dataComeChecker = returnData.GetString(0);

                if (radioVal == "M")
                {
                    radioButton1.Checked = true;
                }

                if (radioVal == "F")
                {

                    radioButton2.Checked = true;

                }

                //if the data comes back, Enable the update and delete buttons and change their colors from black and white to colors.
                if (bunifuCustomTextbox1.Text.Length > 0 && monoFlat_TextBox6.Text.Length > 0 && monoFlat_TextBox5.Text.Length > 0)
                {

                    buttonToGetColor();
                }

                if (dataComeChecker == "no")
                {

                    ErrorMsgBox ee = new ErrorMsgBox("You have entered a wrong customer ID");
                    ee.Show();
                }
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {

            string sqlValue = "";

            if (radioButton1.Checked)
            {

                sqlValue = "M";

            }
            else if (radioButton2.Checked)
            {

                sqlValue = "F";

            }


            string UpdatePer = "yes";

            string cusId = "";
            string firstname = "";
            string lastname = "";
            string address = "";
            string phoneNo = "";
            string idCard = "";
            string Dli = "";
            string gender = "";


            string queryTogetDriverDetails = "SELECT * FROM driver WHERE driverId='" + monoFlat_TextBox8.Text + "'";
            MySqlDataReader dataCus = curdClass.SelectQueryOutMsg(queryTogetDriverDetails);

            while (dataCus.Read())
            {

                cusId = dataCus.GetString(0);
                firstname = dataCus.GetString(1);
                lastname = dataCus.GetString(2);
                address = dataCus.GetString(3);
                phoneNo = dataCus.GetString(4);
                idCard = dataCus.GetString(5);
                Dli = dataCus.GetString(6);
                gender = dataCus.GetString(7);

            }


            if (firstname == monoFlat_TextBox2.Text && lastname == monoFlat_TextBox3.Text && address == monoFlat_TextBox4.Text && phoneNo == monoFlat_TextBox5.Text && idCard == monoFlat_TextBox6.Text && Dli == monoFlat_TextBox7.Text && gender == sqlValue)
            {

                UpdatePer = "no";
            }


            if (bunifuCustomTextbox1.Text == string.Empty || monoFlat_TextBox2.Text == string.Empty || monoFlat_TextBox3.Text == string.Empty || monoFlat_TextBox4.Text == string.Empty || monoFlat_TextBox5.Text == string.Empty || monoFlat_TextBox6.Text == string.Empty || monoFlat_TextBox6.Text == string.Empty || monoFlat_TextBox7.Text == string.Empty)
            {

                ErrorMsgBox su = new ErrorMsgBox("Please Fill all the columns.");
                su.Show();

            }
            else
            {

                if (DriverID == monoFlat_TextBox8.Text)
                {

                    if (UpdatePer == "yes")
                    {

                        string query = "UPDATE driver SET driverFname='" + monoFlat_TextBox2.Text + "',driverLname='" + monoFlat_TextBox3.Text + "',driverAddress='" + monoFlat_TextBox4.Text + "',driverPhoneNo='" + monoFlat_TextBox5.Text + "',driverIdentityCard='" + monoFlat_TextBox6.Text + "',driverLicence='" + monoFlat_TextBox7.Text + "',driverGender='" + sqlValue + "' WHERE driverId='" + monoFlat_TextBox8.Text + "'";
                        curdClass.CUDOutMsg(query);

                        string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        string txt = "The Driver ID " + bunifuCustomTextbox1.Text + " was Updated on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString();
                        File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

                        SuccessMSGBox su = new SuccessMSGBox("Update successfully.");
                        su.Show();

                        getDataFromTheDB();//Load the data into DataGridView Again after Update completed to be look real updates in the gridView.
                        cleanTextBoxes();  //Clear the Textboxes as empty.
                    }
                    else
                    {

                        ErrorMsgBox ee = new ErrorMsgBox("You did not make any changes to update.");
                        ee.Show();

                    }
                }
                else
                {

                    ErrorMsgBox es = new ErrorMsgBox("You have entered a wrong Driver ID. \n" + "We automatically fixed the right ID (" + DriverID + ").");
                    es.Show();
                    monoFlat_TextBox8.Text = DriverID;

                }
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            //Deletion

            if (monoFlat_TextBox8.Text == DriverID)
            {

                string bookCus = "SELECT driverid from booking";
                MySqlDataReader data = curdClass.SelectQuery(bookCus);
                List<string> bookC = new List<string>();


                while (data.Read())
                {


                    bookC.Add(data.GetString(0));

                }

                int canDelete = 0;
                foreach (string item in bookC)
                {
                 
                    if (monoFlat_TextBox8.Text == item)
                    {

                        canDelete = 1;
                    }

               

                }

                if (canDelete == 1)//One means cannot delete
                {
                    ErrorMsgBox es = new ErrorMsgBox("Connot be deleted, the driver is in renting.");
                    es.ShowDialog();
                }
                else
                {
                    ComformFormDelete cfd = new ComformFormDelete("Do you want to delete driver ID '" + monoFlat_TextBox8.Text + "'", "DELETE FROM driver WHERE driverId='" + monoFlat_TextBox8.Text + "'", monoFlat_TextBox8.Text, "driverDeletion");
                    cfd.ShowDialog();

                }



                getDataFromTheDB();//Load the data into DataGridView Again after deletion completed to be look real updates in the gridView.
                cleanTextBoxes(); //Clear the Textboxes as empty.
            }
            else {

                ErrorMsgBox es = new ErrorMsgBox("You have entered a wrong driver ID. \n" + "We automatically fixed the right ID (" + DriverID + ").");
                es.Show();
                monoFlat_TextBox8.Text = DriverID;

            }

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            //Insertion
            //If non of them empty do let to insert.  

            string value = "";
            string sqlValue = "";
            string canUpload = "yes";
            string genderSelected = "no";

            if (radioButton1.Checked)
            {

                value = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {

                value = radioButton2.Text;
            }


            if (value == "Male")
            {

                sqlValue = "M";
                genderSelected = "yes";
            }
            else if (value == "Female")
            {

                sqlValue = "F";
                genderSelected = "yes";

            }


            string errorType = "";

            if (bunifuCustomTextbox1.Text == string.Empty || monoFlat_TextBox2.Text == string.Empty || monoFlat_TextBox3.Text == string.Empty || monoFlat_TextBox4.Text == string.Empty || monoFlat_TextBox5.Text == string.Empty || monoFlat_TextBox6.Text == string.Empty || monoFlat_TextBox6.Text == string.Empty || monoFlat_TextBox7.Text == string.Empty)
            {

                errorType = "Please Fill all the columns.";
                canUpload = "no";

            }
            else if (genderSelected == "no")
            {

                errorType = "Gender must be selected.";
                canUpload = "no";

            }

            if (canUpload == "yes")
            {

                string InsertQuery = "INSERT INTO driver VALUES ('" + bunifuCustomTextbox1.Text + "','" + monoFlat_TextBox2.Text + "','" + monoFlat_TextBox3.Text + "','" + monoFlat_TextBox4.Text + "','" + monoFlat_TextBox5.Text + "','" + monoFlat_TextBox6.Text + "','" + monoFlat_TextBox7.Text + "','" + sqlValue + "','IN')";

                RegisterCarMethod(InsertQuery);

                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string txt = "A new driver was registered on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString() + " The ID is " + bunifuCustomTextbox1.Text;
                File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

                getDataFromTheDB(); //Load the data into DataGridView Again after Insertion to be look real updates.

            }
            else
            {

                ErrorMsgBox er = new ErrorMsgBox(errorType);
                er.Show();

            }
        }

        public void RegisterCarMethod(string query)
        {
            try
            {
                MySqlConnection myConnect = new MySqlConnection("SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=");
                myConnect.Open();
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                myCommand.ExecuteNonQuery();
                myConnect.Clone();

                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string txt = "A new driver was registered on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString() + " The ID is " + bunifuCustomTextbox1.Text;
                File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

                SuccessMSGBox s = new SuccessMSGBox("Registered successfully");
                s.Show();
                getDataFromTheDB();
                cleanTextBoxes();

            }
            catch (Exception Ex)
            {



                string messageFoDuplicatePrimaryKey = Ex.Message;

                if (messageFoDuplicatePrimaryKey.StartsWith("Duplicate"))
                {

                    ErrorMsgBox r = new ErrorMsgBox("The driver ID " + bunifuCustomTextbox1.Text + " is already existed.");
                    r.Show();
                }
                else
                {

                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }

        public void ComboBoxSearch(string status)
        {

            DataTable dTable1 = new DataTable();
            dTable1.Columns.Add("Driver ID");
            dTable1.Columns.Add("Firstname");
            dTable1.Columns.Add("Lastname");
            dTable1.Columns.Add("Address");
            dTable1.Columns.Add("Phone Number");
            dTable1.Columns.Add("Identity Card");
            dTable1.Columns.Add("Driving Licence No");
            dTable1.Columns.Add("Gender");
            dTable1.Columns.Add("Status");



            if (status == "All")
            {

                string queryForAll = "SELECT * FROM driver";

                MySqlDataReader reData = curdClass.SelectQuery(queryForAll);


                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7),reData.GetString(8));

                }

            }
            else if (status == "Male")
            {

                string queryForHired = "SELECT * FROM driver WHERE driverGender='M'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForHired);


                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7),reData.GetString(8));

                }

            }
            else if (status == "Female")
            {


                string queryForAvaliable = "SELECT * FROM driver WHERE driverGender='F'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForAvaliable);

               
                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7),reData.GetString(8));

                }

            }
            else if (status == "Hired Driver")
            {


                string queryForAvaliable = "SELECT * FROM driver WHERE status='OUT'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForAvaliable);

               
                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7),reData.GetString(8));

                }

            }
            else if (status == "Avaliable Driver") {


                string queryForAvaliable = "SELECT * FROM driver WHERE status='IN'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForAvaliable);


                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(7),reData.GetString(8));

                }

            }
            else
            {

                string queryType = "";

                string query = "";

                if (comboBox1.Text == "All")
                {

                    query = "SELECT * FROM driver WHERE 	driverFname like'" + status + "%'";

                }
                else if (comboBox1.Text == "Male")
                {

                    query = "SELECT * FROM driver WHERE 	driverFname like'" + status + "%' AND driverGender='M'";

                }
                else if (comboBox1.Text == "Female")
                {

                    query = "SELECT * FROM driver WHERE 	driverFname like'" + status + "%' AND driverGender='F'";

                }
                else
                {

                    query = "SELECT * FROM driver WHERE 	driverFname like'" + status + "%'";

                }

                string queryForBrand = query;
                MySqlDataReader dataBrand = curdClass.SelectQuery(queryForBrand);

                while (dataBrand.Read())
                {

                    dTable1.Rows.Add(dataBrand.GetString(0), dataBrand.GetString(1), dataBrand.GetString(2), dataBrand.GetString(3), dataBrand.GetString(4), dataBrand.GetString(5), dataBrand.GetString(6), dataBrand.GetString(7),dataBrand.GetString(8));

                }

            }

            bunifuCustomDataGrid1.DataSource = dTable1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "All")
            {
                //in this case text All.
                ComboBoxSearch(comboBox1.Text);

            }
            else if (comboBox1.Text == "Male")
            {
                //in this case text Hired.
                ComboBoxSearch(comboBox1.Text);

            }

            else if (comboBox1.Text == "Female")
            {
                //in this case text Avaliable.
                ComboBoxSearch(comboBox1.Text);
            }
            else if (comboBox1.Text == "Hired Driver")
            {

                ComboBoxSearch(comboBox1.Text);

            }
            else if (comboBox1.Text == "Avaliable Driver") {

                ComboBoxSearch(comboBox1.Text);
            }
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {




            if (bunifuTextbox1.text == string.Empty)
            {

                ComboBoxSearch(comboBox1.Text);
            }
            else
            {

                if (bunifuTextbox1.text != "All" && bunifuTextbox1.text != "Male" && bunifuTextbox1.text != "Female" && bunifuTextbox1.text != "Hired Driver" && bunifuTextbox1.text != "Avaliable Driver")
                {

                    ComboBoxSearch(bunifuTextbox1.text);

                }

                if (System.Text.RegularExpressions.Regex.IsMatch(bunifuTextbox1.text, @"[^a-zA-Z \s]"))
                {
                    char[] chars = bunifuTextbox1.text.ToCharArray();
                    string charsToStr = new string(chars);

                    ErrorMsgBox er = new ErrorMsgBox("The first name must be in text format.\nNumbers and symbols are not allowed.");
                    er.Show();
                    bunifuTextbox1.text = charsToStr.Remove(charsToStr.Length - 1);


                }

            }
        }

        public void buttonToGetColor()
        {
            bunifuFlatButton3.Enabled = true;
            bunifuFlatButton4.Enabled = true;


            this.bunifuFlatButton4.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_Remove_27874;
            this.bunifuFlatButton3.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_db_update_32131;


        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            cleanTextBoxes();
        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string position;

            if (bunifuCustomDataGrid1.CurrentCell.ColumnIndex.Equals(0))
            {

                position = bunifuCustomDataGrid1.CurrentCell.Value.ToString();

                string query = "SELECT * FROM driver WHERE driverId='" + position + "'";
                MySqlDataReader data = curdClass.SelectQuery(query);

                string RadioVal;

                while (data.Read())
                {

                    bunifuCustomTextbox1.Text = data.GetString(0);
                    monoFlat_TextBox2.Text = data.GetString(1);
                    monoFlat_TextBox3.Text = data.GetString(2);
                    monoFlat_TextBox4.Text = data.GetString(3);
                    monoFlat_TextBox5.Text = data.GetString(4);
                    monoFlat_TextBox6.Text = data.GetString(5);
                    monoFlat_TextBox7.Text = data.GetString(6);
                    RadioVal = data.GetString(7);
                    monoFlat_TextBox8.Text = data.GetString(0);

                    DriverID = data.GetString(0);

                    if (RadioVal == "M")
                    {

                        radioButton1.Checked = true;
                    }
                    else if (RadioVal == "F")
                    {

                        radioButton2.Checked = true;

                    }
                }

                buttonToGetColor();
            }

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
