using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Graphite
{
    class MySQL
    {
        //
        //Launch Game Variables
        public bool blnCorrect = false;

        //BYPASS SQL LOGIN !!!!!
        public bool blnBYPASS = false;
        //----------------------

        //Login Variables
        public string strUsername;
        public string strPassword;
        public string strTruePass;
        //MySQL Server Data
        static string strMySQLServerInfo = "SERVER=Graphite.no-ip.org;" +
                                           "DATABASE=ServerData;" +
                                           "UID=root;" +
                                           "PASSWORD=13307A;";
        MySqlConnection MySQLServer = new MySqlConnection(strMySQLServerInfo);

        //classfiles
        WorldPlayers WP = new WorldPlayers();
        TCPServer_Class TCP = new TCPServer_Class();

        //Class Files
        
        //Player Login
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

                    mySQLCommand.CommandText = ("SELECT * FROM user_accounts WHERE user_name= '" +strUsername+ "'");
                    mySQLCommand.Connection = MySQLServer; 
                    mySQLReader = mySQLCommand.ExecuteReader();

                    while (mySQLReader.Read())
                    {

                        strTruePass = mySQLReader.GetString(1);

                        if (strPassword == strTruePass)
                        {
                            blnCorrect = true;
                            //Get Player Data  
                            WP.strUserName = strUsername;
                            WP.intUserID = mySQLReader.GetInt32(2);
                            WP.intTeamID = mySQLReader.GetInt32(3);
                            WP.playerLoc.X = mySQLReader.GetInt32(4);
                            WP.playerLoc.Y = mySQLReader.GetInt32(5);
                            WP.intWeapSlot[1] = mySQLReader.GetInt32(6);
                            WP.intWeapSlot[2] = mySQLReader.GetInt32(7);
                            WP.intWeapSlot[3] = mySQLReader.GetInt32(8);
                            WP.intHatID = mySQLReader.GetInt32(9);
                            WP.intMoney = mySQLReader.GetInt32(10);
                            WP.intExp = mySQLReader.GetInt32(11);

                        }
                    }// End Read Loop
                }
                catch { }
            }
            catch { }
            MySQLServer.Close();
        }
        //Up
    }
}
