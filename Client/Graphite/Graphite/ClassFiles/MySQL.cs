using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Graphite
{
    class MySQL
    {
        //Launch Game Variables
        public bool blnCorrect = false;

        //Login Variables
        public string strUsername;
        public string strPassword;
        public string strTruePass;
        //MySQL Server Data
        static string strMySQLServerInfo = "SERVER=Graphite.no-ip.org;" +
                                           "DATABASE=ServerData;" +
                                           "UID=root;" +
                                           "PASSWORD=13307A;"
        MySqlConnection MySQLServer = new MySqlConnection(strMySQLServerInfo);
<<<<<<< HEAD
        //classfiles
        WorldPlayers world = new WorldPlayers();

=======
        //Class Files
        
        //Player Login
>>>>>>> 47e3c7f39f91936bd0e6696bdf8fabce5213c18b
        public void Login()
        {
            try
            {
                //Connect to DataBase
                MySQLServer.Open();
                try
                {
                    //Check Login
                    MySqlDataReader mySQLReader = null;

                    MySqlCommand mySQLCommand = MySQLServer.CreateCommand();

                    mySQLCommand.CommandText = ("SELECT * FROM user_accounts WHERE username= '" +strUsername+ "'");

                    mySQLReader = mySQLCommand.ExecuteReader();

                    while (mySQLReader.Read())
                    {

                        strTruePass = mySQLReader.GetString(1);

                        if (strPassword == strTruePass)
                        {
                            blnCorrect = true;
                            //Get Player Data
                            int intUserID = mySQLReader.GetInt32(2);
                            int intTeamID = mySQLReader.GetInt32(3);
                            int LastXPos = mySQLReader.GetInt32(4);
                            int LastYPos = mySQLReader.GetInt32(5);

                        }
                    }// End Read Loop
                }
                catch { }
            }
            catch { }
            MySQLServer.Close();
            //TEMP
            blnCorrect = true;
        }
    }
}
