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
using System.Text.RegularExpressions;

namespace Car_Renting_Management_System
{
    public partial class carsForm : Form
    {
        public carsForm()
        {
            InitializeComponent();

        }

        public string cleanTextboxes{

            set { monoFlat_TextBox1.Text = value; }
        }

        CURDQueryClass curdClass = new CURDQueryClass(); //Class for Curd function.



        private void label2_Click(object sender, EventArgs e)
        {
            this.Close(); //Closing mark X.
        }

     

        private void carsForm_Load(object sender, EventArgs e)
        {
            getDataFromTheDB();             //load data into DataGridView.
            buttonToGetBlackAndWhite();    //Change the Update and Delete Button to be black and white and Disabled.          
            comboBox1.SelectedIndex = 0;  //comboBox1 must have the first value as default when form loads.
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            //Insertion
            //If non of them empty do let to insert.    

            string meterType = "";

            if (radioButton1.Checked == true)
            {

                meterType = "Normal";

            }
            else if (radioButton2.Checked == true) {


                meterType = "Digital";
            }
                    
            if (monoFlat_TextBox1.Text == string.Empty || monoFlat_TextBox2.Text == string.Empty || monoFlat_TextBox3.Text == string.Empty || monoFlat_TextBox4.Text == string.Empty || monoFlat_TextBox5.Text == string.Empty || monoFlat_TextBox6.Text == string.Empty || monoFlat_TextBox7.Text == string.Empty || monoFlat_TextBox8.Text == string.Empty)
            {

                ErrorMsgBox er = new ErrorMsgBox("Please Fill all the columns");
                er.Show();

            }
            else {

                string canInsert = "yes";

                if (monoFlat_TextBox8.Text.Length < 5)
                {

                    ErrorMsgBox er = new ErrorMsgBox("The mileage meter must have five digits");
                    er.Show();
                    canInsert = "no";

                }
               
           
                 if (canInsert == "yes")
                {

                    string InsertQuery = "INSERT INTO car VALUES ('" + monoFlat_TextBox1.Text + "','" + monoFlat_TextBox2.Text + "','" + monoFlat_TextBox3.Text + "','" + monoFlat_TextBox4.Text + "','" + monoFlat_TextBox5.Text + "','" + monoFlat_TextBox6.Text + "','" + monoFlat_TextBox7.Text + "','IN','" + monoFlat_TextBox8.Text + "','"+meterType+"')";
                    RegisterCarMethod(InsertQuery);
                   
                }
                
            }
            
        }


        string carIdForBug1 = "";
        string meterType = "";
        
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

            //Selection
            string query = "SELECT * FROM car WHERE carId='" + monoFlat_TextBox9.Text + "'";
            string dataComeChecker = "no";
            //The data returns back as MySqlDataReader format. That's why we can loop it.
            MySqlDataReader returnData = curdClass.SelectQuery(query);


            while (returnData.Read()) {

                monoFlat_TextBox1.Text = returnData.GetString(0);
                monoFlat_TextBox2.Text = returnData.GetString(1);
                monoFlat_TextBox3.Text = returnData.GetString(2);
                monoFlat_TextBox4.Text = returnData.GetString(3);
                monoFlat_TextBox5.Text = returnData.GetString(4);
                monoFlat_TextBox6.Text = returnData.GetString(5);
                monoFlat_TextBox7.Text = returnData.GetString(6);
                monoFlat_TextBox8.Text = returnData.GetString("odometer");
                meterType = returnData.GetString("kmmeter_type");

                dataComeChecker = monoFlat_TextBox8.Text;
                carIdForBug1 = returnData.GetString(0); 
            }

            if (dataComeChecker == "no") {

                ErrorMsgBox ee = new ErrorMsgBox("You have entered a wrong car ID");
                ee.Show();

            }

            if (meterType == "Normal")
            {

                radioButton1.Checked = true;
            }
            else if (meterType == "Digital") {

                radioButton2.Checked = true;

            }

            //if the data comes back, Enable the update and delete buttons and change their colors from black and white to colors.
            if (monoFlat_TextBox1.Text.Length > 0 && monoFlat_TextBox6.Text.Length > 0 && monoFlat_TextBox5.Text.Length > 0) {

                buttonToGetColor();
            }
           
        }



        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            //Update

            string selectCar = "SELECT carStatus FROM car WHERE carId='" + monoFlat_TextBox9.Text + "'";
            MySqlDataReader datas = curdClass.SelectQueryOutMsg(selectCar);
            string carS = "";


            while (datas.Read())
            {

                carS = datas.GetString(0);

            }

            string meterType = "";

            if (radioButton1.Checked == true) {

                meterType = "Normal";
            }
            else
            {

                meterType = "Digital";

            }

            string selectCarforUpateallNotAllow = "SELECT * FROM car WHERE carId='" + monoFlat_TextBox9.Text + "'";
            MySqlDataReader dataForselectCarforUpateallNotAllow = curdClass.SelectQueryOutMsg(selectCarforUpateallNotAllow);

            string carId = "";
            string model = "";
            string brand = "";
            string makeYear = "";
            string color = "";
            string dailyRate = "";
            string monthlyRate = "";
            string odameterValue = "";
            string meterTypeOld = "";
           

            while (dataForselectCarforUpateallNotAllow.Read())
            {

                carId = dataForselectCarforUpateallNotAllow.GetString("carId");
                model = dataForselectCarforUpateallNotAllow.GetString("carModel");
                brand = dataForselectCarforUpateallNotAllow.GetString("carBrand");
                makeYear = dataForselectCarforUpateallNotAllow.GetString("carMakeYear");
                color = dataForselectCarforUpateallNotAllow.GetString("carColor");
                dailyRate = dataForselectCarforUpateallNotAllow.GetString("carDailyRate");
                monthlyRate = dataForselectCarforUpateallNotAllow.GetString("carMonthlyRate");
                odameterValue = dataForselectCarforUpateallNotAllow.GetString("odometer");
                meterTypeOld = dataForselectCarforUpateallNotAllow.GetString("kmmeter_type");



            }

            string canUpdate = "yes";


            if (monoFlat_TextBox8.Text.Length < 5 && carS != "OUT" && carIdForBug1 == monoFlat_TextBox9.Text)
            {

                ErrorMsgBox er = new ErrorMsgBox("The Odometer must have five digits");
                er.Show();
                canUpdate = "no";

            }


            if (carId == monoFlat_TextBox1.Text && model == monoFlat_TextBox2.Text && brand == monoFlat_TextBox3.Text && makeYear == monoFlat_TextBox4.Text && color == monoFlat_TextBox5.Text && dailyRate == monoFlat_TextBox6.Text && monthlyRate == monoFlat_TextBox7.Text && odameterValue == monoFlat_TextBox8.Text && carIdForBug1 == monoFlat_TextBox9.Text && carS != "OUT" && meterTypeOld==meterType)
            {

                ErrorMsgBox er = new ErrorMsgBox("You did not make any changes to update");
                er.Show();
                canUpdate = "no";

            }


            if (monoFlat_TextBox1.Text == string.Empty || monoFlat_TextBox2.Text == string.Empty || monoFlat_TextBox3.Text == string.Empty || monoFlat_TextBox4.Text == string.Empty || monoFlat_TextBox5.Text == string.Empty || monoFlat_TextBox6.Text == string.Empty || monoFlat_TextBox7.Text == string.Empty || monoFlat_TextBox8.Text == string.Empty)
            {

                ErrorMsgBox er = new ErrorMsgBox("Please Fill all the columns");
                er.Show();

            }
            else
            {



                if (carIdForBug1 == monoFlat_TextBox9.Text)
                {
                    if (carS != "OUT")
                    {

                        if (canUpdate == "yes")
                        {


                            string query = "UPDATE car SET carId='" + monoFlat_TextBox1.Text + "', carModel='" + monoFlat_TextBox2.Text + "',carBrand='" + monoFlat_TextBox3.Text + "',carMakeYear='" + monoFlat_TextBox4.Text + "',carColor='" + monoFlat_TextBox5.Text + "',carDailyRate='" + monoFlat_TextBox6.Text + "',carMonthlyRate='" + monoFlat_TextBox7.Text + "',odometer='" + monoFlat_TextBox8.Text + "',kmmeter_type='" + meterType + "' WHERE carId='" + monoFlat_TextBox9.Text + "'";

                            try
                            {
                                MySqlConnection myConnect = new MySqlConnection("SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=");
                                myConnect.Open();
                                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                                myCommand.ExecuteNonQuery();
                                myConnect.Clone();

                                string pathforLog = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                string txt = "The Car ID " + monoFlat_TextBox1.Text + " was Updated on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString();
                                File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);


                                if (comboBox1.Text == "All")
                                {

                                    ComboBoxSearch(comboBox1.Text);

                                }
                                else if (comboBox1.Text == "Hired")
                                {

                                    ComboBoxSearch(comboBox1.Text);

                                }
                                else if (comboBox1.Text == "Avaliable")
                                {

                                    ComboBoxSearch(comboBox1.Text);
                                }
                                else
                                {

                                    getDataFromTheDB();
                                }

                                SuccessMSGBox s = new SuccessMSGBox("Update Successfully");
                                s.Show();
                                cleanTextBoxes();  //Clear the Textboxes as empty.      

                            }
                            catch (Exception Ex)
                            {
                                string messageFoDuplicatePrimaryKey = Ex.Message;

                                if (messageFoDuplicatePrimaryKey.StartsWith("Duplicate"))
                                {

                                    ErrorMsgBox r = new ErrorMsgBox("The car ID " + monoFlat_TextBox1.Text + " is already existed.");
                                    r.Show();
                                }
                                else
                                {

                                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                        }
                    }
                    else
                    {

                        ErrorMsgBox es = new ErrorMsgBox("Car is in renting, unable to make changes");
                        es.Show();

                    }
                }
                else
                {

                    ErrorMsgBox es = new ErrorMsgBox("You have entered a wrong car ID. \n" + "We automatically fixed the right ID (" + carIdForBug1 + ").");
                    es.Show();
                    monoFlat_TextBox9.Text = carIdForBug1;

                }

            }

        }
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            string selectCar = "SELECT carStatus FROM car WHERE carId='" + monoFlat_TextBox9.Text + "'";
            MySqlDataReader datas = curdClass.SelectQueryOutMsg(selectCar);
            string carS = "";

            while (datas.Read()) {

                carS = datas.GetString(0);
            }

            if (monoFlat_TextBox9.Text == carIdForBug1)
            {

                if (carS != "OUT")
                {
                    ComformFormDelete cfd = new ComformFormDelete("Do you want to delete Car ID '" + monoFlat_TextBox9.Text + "'", "DELETE FROM car WHERE carId='" + monoFlat_TextBox9.Text + "'", monoFlat_TextBox9.Text, "carDeletion");
                    cfd.ShowDialog();



                    if (comboBox1.Text == "All")
                    {

                        ComboBoxSearch(comboBox1.Text);


                    }
                    else if (comboBox1.Text == "Hired")
                    {

                        ComboBoxSearch(comboBox1.Text);

                    }
                    else if (comboBox1.Text == "Avaliable")
                    {

                        ComboBoxSearch(comboBox1.Text);
                    }
                    else
                    {

                        getDataFromTheDB();

                    }

                    string selectCar2 = "SELECT carId FROM car WHERE carId='" + monoFlat_TextBox9.Text + "'";
                    MySqlDataReader datas2 = curdClass.SelectQueryOutMsg(selectCar2);
                    string carS2 = "";

                    while (datas2.Read())
                    {

                        carS2 = datas2.GetString(0);
                    }

                    if (carS2 == string.Empty)
                    {

                        cleanTextBoxes();
                    }

                }
                else
                {

                    ErrorMsgBox es = new ErrorMsgBox("Car is in renting, unable to delete");
                    es.Show();
                }
            }
            else {

                ErrorMsgBox es = new ErrorMsgBox("You have entered a wrong car ID. \n" + "We automatically fixed the right ID (" + carIdForBug1 + ").");
                es.Show();
                monoFlat_TextBox9.Text = carIdForBug1;
            }
        }


 

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Search the database bashed on comboBox1 Text like Hire All and Etc.

            if (comboBox1.Text == "All")
            {
                //in this case text All.
                ComboBoxSearch(comboBox1.Text);

            }
            else if (comboBox1.Text == "Hired")
            {
                //in this case text Hired.
                ComboBoxSearch(comboBox1.Text);

            }

            else if (comboBox1.Text == "Avaliable")
            {
                //in this case text Avaliable.
                ComboBoxSearch(comboBox1.Text);
            }
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            //When the user entering the Car brand we need to update the GridView.

            //But in this case we are sending what is it being typed in the bunifuTextbox1.
            //according to the text the DataGridView will be updated.

           

            if (bunifuTextbox1.text == string.Empty)
            {

                ComboBoxSearch(comboBox1.Text);
            }
            else {

                if (bunifuTextbox1.text != "All" && bunifuTextbox1.text != "Hired" && bunifuTextbox1.text!="Avaliable")
                {

                    ComboBoxSearch(bunifuTextbox1.text);

                }
                
                if (System.Text.RegularExpressions.Regex.IsMatch(bunifuTextbox1.text, @"[^a-zA-Z \s]"))
                {

                    char[] chars = bunifuTextbox1.text.ToCharArray();
                    ErrorMsgBox er = new ErrorMsgBox("The brand name must be in text format.\nNumbers and symbols are not allowed.");
                    er.Show();
                    string charsToStr = new string(chars);
                    bunifuTextbox1.text = charsToStr.Remove(charsToStr.Length - 1);

                }

            }
           
        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //We are displaying data in the form from the DataGridView in a sense of cell click.

            string position;
            string meterType = "";
            
            if (bunifuCustomDataGrid1.CurrentCell.ColumnIndex.Equals(0)) {

                position = bunifuCustomDataGrid1.CurrentCell.Value.ToString();

                string query = "SELECT * FROM car WHERE carId='" + position + "'";
                MySqlDataReader data = curdClass.SelectQuery(query);

                while (data.Read()) {

                    monoFlat_TextBox1.Text = data.GetString(0);
                    monoFlat_TextBox2.Text = data.GetString(1);
                    monoFlat_TextBox3.Text = data.GetString(2);
                    monoFlat_TextBox4.Text = data.GetString(3);
                    monoFlat_TextBox5.Text = data.GetString(4);
                    monoFlat_TextBox6.Text = data.GetString(5);
                    monoFlat_TextBox7.Text = data.GetString(6);
                    monoFlat_TextBox8.Text = data.GetString("odometer");
                    monoFlat_TextBox9.Text = data.GetString(0);
                    carIdForBug1 = data.GetString(0);
                    meterType = data.GetString("kmmeter_type");

                }

                if (meterType == "Normal") {

                    radioButton1.Checked = true;

                }else if (meterType == "Digital") {


                    radioButton2.Checked = true;

                }

                buttonToGetColor();
            }

        }



        //Clear TextBoxes Method.
        public void cleanTextBoxes()
        {

            monoFlat_TextBox1.Text = "";
            monoFlat_TextBox2.Text = "";
            monoFlat_TextBox3.Text = "";
            monoFlat_TextBox4.Text = "";
            monoFlat_TextBox5.Text = "";
            monoFlat_TextBox6.Text = "";
            monoFlat_TextBox7.Text = "";
            monoFlat_TextBox8.Text = "";
            monoFlat_TextBox9.Text = "";
         
            
        }

        

        //Update and Delete Buttons Enabled and Get Color Method.
        public void buttonToGetColor()
        {
            bunifuFlatButton3.Enabled = true;
            bunifuFlatButton4.Enabled = true;


            this.bunifuFlatButton4.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_Remove_27874;
            this.bunifuFlatButton3.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_db_update_32131;
     

        }





        //Update and Delete Buttons Disabled and Lost Color Method.
        public void buttonToGetBlackAndWhite()
        {

            bunifuFlatButton3.Enabled = false;
            bunifuFlatButton4.Enabled = false;

            this.bunifuFlatButton4.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_Remove_27874_ConvertImage; ;
            this.bunifuFlatButton3.Iconimage = global::Car_Renting_Management_System.Properties.Resources.if_db_update_3213_ConvertImage;

        }






        //Load data from the database into DataGridView Method.
       public void getDataFromTheDB()
        {

            DataTable data = new DataTable();
            data.Columns.Add("Car ID");
            data.Columns.Add("Brand");
            data.Columns.Add("Model");
            data.Columns.Add("Make Year");
            data.Columns.Add("Color");
            data.Columns.Add("Daily Rate");
            data.Columns.Add("Monthly Rate");
            data.Columns.Add("Odometer Value");
            data.Columns.Add("Status");
            data.Columns.Add("Odometer Type");

            string querySelect = "SELECT * FROM car";

            MySqlDataReader datas = curdClass.SelectQuery(querySelect);

            while (datas.Read())
            {

                data.Rows.Add(datas.GetString(0), datas.GetString(1), datas.GetString(2), datas.GetString(3), datas.GetString(4), datas.GetString(5), datas.GetString(6), datas.GetString(8),datas.GetString(7),datas.GetString(9));

            }

            bunifuCustomDataGrid1.DataSource = data;
        }





        //ComboBoxSearch Method.Like Hire Basics or All and more.
        public void ComboBoxSearch(string status)
        {

            DataTable dTable1 = new DataTable();
            dTable1.Columns.Add("Car ID");
            dTable1.Columns.Add("Brand");
            dTable1.Columns.Add("Model");
            dTable1.Columns.Add("Make Year");
            dTable1.Columns.Add("Color");
            dTable1.Columns.Add("Daily Rate");
            dTable1.Columns.Add("Monthly Rate");
            dTable1.Columns.Add("Odometer Value");
            dTable1.Columns.Add("Status");
            dTable1.Columns.Add("Odometer Type");


            if (status == "All")
            {

                string queryForAll = "SELECT * FROM car";

                MySqlDataReader reData = curdClass.SelectQuery(queryForAll);


                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(8), reData.GetString(7),reData.GetString(9));

                }

            }
            else if (status == "Hired")
            {

                string queryForHired = "SELECT * FROM car WHERE carStatus='OUT'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForHired);


                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(8), reData.GetString(7));

                }

            }
            else if (status == "Avaliable")
            {


                string queryForAvaliable = "SELECT * FROM car WHERE carStatus='IN'";

                MySqlDataReader reData = curdClass.SelectQuery(queryForAvaliable);


                while (reData.Read())
                {

                    dTable1.Rows.Add(reData.GetString(0), reData.GetString(1), reData.GetString(2), reData.GetString(3), reData.GetString(4), reData.GetString(5), reData.GetString(6), reData.GetString(8), reData.GetString(7));

                }

            }
            else
            {

                string queryType = "";

                string query = "";

                if (comboBox1.Text == "All")
                {

                    query = "SELECT * FROM car WHERE carBrand like'" + status + "%'";

                }
                else if (comboBox1.Text == "Hired")
                {

                    query = "SELECT * FROM car WHERE carBrand like'" + status + "%' AND carStatus='OUT'";

                }
                else if (comboBox1.Text == "Avaliable")
                {

                    query = "SELECT * FROM car WHERE carBrand like'" + status + "%' AND carStatus='IN'";

                }
                else {

                    query = "SELECT * FROM car WHERE carBrand like'" + status + "%'";

                }

                string queryForBrand = query;
                MySqlDataReader dataBrand = curdClass.SelectQuery(queryForBrand);

                while (dataBrand.Read())
                {

                    dTable1.Rows.Add(dataBrand.GetString(0), dataBrand.GetString(1), dataBrand.GetString(2), dataBrand.GetString(3), dataBrand.GetString(4), dataBrand.GetString(5), dataBrand.GetString(6), dataBrand.GetString(8), dataBrand.GetString(7));

                }

            }

            bunifuCustomDataGrid1.DataSource = dTable1;

        }

   
        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            cleanTextBoxes();
            buttonToGetBlackAndWhite();
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

      

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void monoFlat_TextBox9_TextChanged(object sender, EventArgs e)
        {
            //If the TextBox9.length is 0 ,change the update and delete buttons to be disabled and black and white.
            //Becasue we don't need to be enabled when is not updating or deleting.
            if (monoFlat_TextBox9.Text.Length == 0)
            {

                buttonToGetBlackAndWhite();
                cleanTextBoxes();

            }

            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox9.Text, @"[!@#$%^&*\~{}.`,\\;=<|>/?+'\""]"))
            {
                char[] chars = monoFlat_TextBox9.Text.ToCharArray();
                ErrorMsgBox er = new ErrorMsgBox("You cannot enter symbols like " + chars[chars.Length - 1]);
                er.Show();

                string charsToStr = new string(chars);

                monoFlat_TextBox9.Text = charsToStr.Remove(charsToStr.Length - 1);

            }
        }

        private void monoFlat_TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox4.Text, "[^0-9]")) {

                char[] chars = monoFlat_TextBox4.Text.ToCharArray();
                string charsToStr = new string(chars);
               
                ErrorMsgBox er = new ErrorMsgBox("The make year must be in digits format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox4.Text = charsToStr.Remove(charsToStr.Length - 1);

            }

        }

        private void monoFlat_TextBox5_TextChanged(object sender, EventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox5.Text, @"[^a-zA-Z \s]"))
            {
                char[] chars = monoFlat_TextBox5.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The color must be in text format.\nNumbers and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox5.Text = charsToStr.Remove(charsToStr.Length - 1);


            }

        }

        private void monoFlat_TextBox6_TextChanged(object sender, EventArgs e)
        {

            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox6.Text, "[^0-9.]"))
            {

                char[] chars = monoFlat_TextBox6.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The daily rate must be in digit format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox6.Text = charsToStr.Remove(charsToStr.Length - 1);


            }

        }

        private void monoFlat_TextBox7_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox7.Text, "[^0-9.]"))
            {
                char[] chars = monoFlat_TextBox7.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The monthly rate must be in digit format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox7.Text = charsToStr.Remove(charsToStr.Length - 1);

            }

        }

        private void monoFlat_TextBox8_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox8.Text, "[^0-9]"))
            {
                char[] chars = monoFlat_TextBox8.Text.ToCharArray();
                string charsToStr = new string(chars);

                ErrorMsgBox er = new ErrorMsgBox("The odometer must be in digit format.\nLetters and symbols are not allowed.");
                er.Show();
                monoFlat_TextBox8.Text = charsToStr.Remove(charsToStr.Length - 1);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

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
                string txt = "A new car was registered on " + DateTime.Now.ToString("yyyy-MM-dd") + " at " + DateTime.Now.ToLongTimeString() + " The ID is " + monoFlat_TextBox1.Text;
                File.AppendAllText(pathforLog + "//CRMS//ActivityLog.txt", txt + Environment.NewLine);

                SuccessMSGBox s = new SuccessMSGBox("Registered successfully.");
                s.Show();
                getDataFromTheDB();
                cleanTextBoxes();

            }
            catch (Exception Ex)
            {



                string messageFoDuplicatePrimaryKey = Ex.Message;

                if (messageFoDuplicatePrimaryKey.StartsWith("Duplicate"))
                {

                    ErrorMsgBox r = new ErrorMsgBox("The car ID " + monoFlat_TextBox1.Text + " is already existed.");
                    r.Show();
                }
                else
                {

                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }

        private void monoFlat_TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox1.Text, @"[!@#$%^&*\~{}.`,\\;=<|>/?'\""]"))
            {
               char[] chars=monoFlat_TextBox1.Text.ToCharArray(); 
                ErrorMsgBox er = new ErrorMsgBox("You cannot enter symbols like "+chars[chars.Length-1]);
                er.Show();

                string charsToStr = new string(chars);

                monoFlat_TextBox1.Text = charsToStr.Remove(charsToStr.Length - 1);

            }
        }

        private void monoFlat_TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox2.Text, @"[!@#$%^&*\~`{},\\;.=<|>/?'\""]"))
            {
                char[] chars = monoFlat_TextBox2.Text.ToCharArray();
                ErrorMsgBox er = new ErrorMsgBox("You cannot enter symbols like " + chars[chars.Length - 1]);
                er.Show();

                string charsToStr = new string(chars);

                monoFlat_TextBox2.Text = charsToStr.Remove(charsToStr.Length - 1);

            }
        }

        private void monoFlat_TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(monoFlat_TextBox3.Text, @"[^a-zA-Z \s]"))
            {
                

                char[] chars = monoFlat_TextBox3.Text.ToCharArray();
                ErrorMsgBox er = new ErrorMsgBox("The brand name must be in text format.\nNumbers and symbols are not allowed.");
                er.Show();
                string charsToStr = new string(chars);
                monoFlat_TextBox3.Text = charsToStr.Remove(charsToStr.Length - 1);

            }
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
