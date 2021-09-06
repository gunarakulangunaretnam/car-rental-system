using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;

namespace Car_Renting_Management_System
{
    class CURDQueryClass
    {
        

        public void CUD(string query) {

            try
            {
                MySqlConnection myConnect = new MySqlConnection("SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=");
                myConnect.Open();
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                myCommand.ExecuteNonQuery();
                myConnect.Close();
                SuccessMSGBox success = new SuccessMSGBox("Process Successful!");
                success.Show();
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
               

            }         
           
        }

        public void CUDOutMsg(string query)
        {


            try
            {
                MySqlConnection myConnect = new MySqlConnection("SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=");
                myConnect.Open();
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                myCommand.ExecuteNonQuery();
                myConnect.Close();

            }
            catch (Exception Ex)
            {
                
                    MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

        }
        
        public MySqlDataReader SelectQuery(string query) {

            try
            {
                MySqlConnection myConnect = new MySqlConnection("SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=");
                myConnect.Open();
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();
                return data;
                myConnect.Close();
                SuccessMSGBox success = new SuccessMSGBox("Process Successful!");
                success.Show();
            }
            catch (Exception Ex)
            {
                 MessageBox.Show(Ex.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                 throw Ex;
            }
        }
        
        public MySqlDataReader SelectQueryOutMsg(string query)
        {

            try
            {
                MySqlConnection myConnect = new MySqlConnection("SERVER=localhost;DATABASE=rentcarsystem;UID=root;PASSWORD=");
                myConnect.Open();
                MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                MySqlDataReader data = myCommand.ExecuteReader();
                return data;
                myConnect.Close();
               
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw Ex;
            }
        }
        
    }
}
