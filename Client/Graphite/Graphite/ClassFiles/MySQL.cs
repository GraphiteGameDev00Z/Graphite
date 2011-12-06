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
        public string TruePass;
        //MySQL Server Data
        static string strMySQLServerInfo = "SERVER=Graphite.no-ip.org;" +
                                           "DATABASE=ServerData;" +
                                           "UID=root;" +
                                           "PASSWORD=13307A;"
        MySqlConnection MySQLServer = new MySqlConnection(strMySQLServerInfo);
        //classfiles
        WorldPlayers world = new WorldPlayers();

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

                    mySQLCommand.CommandText = ("SELECT * FROM user_accounts WHERE user_name=" + strUsername);
                    mySQLReader = mySQLCommand.ExecuteReader();

                    while (mySQLReader.Read())
                    {
                        TruePass = mySQLReader.GetString(1);

                        if (strPassword == TruePass)
                        {
                            blnCorrect = true;
                            //Get Player Data
                        }
                    }// End Read Loop
                }
                catch { }
            }
            catch { }
        }
    }
}
