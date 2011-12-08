using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//--------------
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using MySql.Data.MySqlClient;
//--------------
namespace ServerConsole
{
    class ServerConsole
    {
        //Variables---
        static bool blnGotAccounts = false;
        static bool blnEventStarted = false;
        static string Crlf = Environment.NewLine;
        static int intNumSeconds;
        static int intNumUpdateSeconds;
        static Random Random = new Random();
        static int intNumClients = 0;
        static int intEventLengthMIN = 0;
        static int intUpdateFrequencyMIN = 0;
        // Socket Variables
        static TcpListener ServerSocket = new TcpListener(IPAddress.Any ,16487);
        // MySQL Variables
        static bool blnAllowConnect = false;
        static bool blnUpdateServer = true;
        static bool blnRandomEvents;
        static bool blnShowEvenNums;
        static int intTotalAcounts = 5;
        static int intEventChance;
        static int intEventLength;
        static int intUpdateFrequency;
        static int intPlayerProximity = 1000;
        //Player Process data
        static int[] intPlayerXs;
        static int[] intPlayerYs;
        static string[] strPlayersData;
        // MySQL Server Options
        static string strMySQLServerInfo = "SERVER=192.168.1.101;" +
                                           "DATABASE=ServerData;" +
                                           "UID=root;" +
                                           "PASSWORD=13307A;";

        //Server Load
        static void Main()
        {
            //Server Tick Time MILISECONDS
            int intServerTickSpeed = 1000;

            //Creat Server Timers
            TimerCallback ServerTick = new TimerCallback(Tick);

            //Display and Store Server Begin Start Time
            Console.WriteLine("Server Started: " + DateTime.Now.ToString("HH:mm:ss") + Crlf);
            //Start Accepting Clients
            ServerSocket.Start();
            Console.WriteLine("Server Opening Sockets....," + DateTime.Now.ToString("HH:mm:ss"));
            // Start Server Tick
            Timer stateTimer = new Timer(ServerTick, null, 0, intServerTickSpeed);

            // loop here forever Checking for Clients
            while (true)
            {
                if (blnAllowConnect == true)
                {
                    //WAIT FOR CLIENT TO CONNECT
                    //blocks until a client has connected to the server
                    TcpClient client = ServerSocket.AcceptTcpClient();
                    intNumClients++;
                    Console.WriteLine(Crlf + "New Client Connected " + DateTime.Now.ToString("HH:mm:ss"));
                    Console.WriteLine("Clients Connected = " + intNumClients.ToString()+Crlf);
                    //create a thread to handle communication
                    //with connected client
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClients));
                    clientThread.Start(client);
                }
            }
        }
        //Server Ticker
        static public void Tick(Object stateInfo)
        {
            intNumUpdateSeconds += 1;

            //Download Config on Startup
            if (blnUpdateServer == true)
            {
                if (blnGotAccounts == false)
                {
                    GetServerVariables();
                }
                if (intNumUpdateSeconds == intUpdateFrequency && blnGotAccounts == true)
                {
                    intNumUpdateSeconds = 0;
                    GetServerVariables();
                }
            }
            //RANDOM EVENT
            if (blnEventStarted == false)
            {
                intNumSeconds = 0;
                if (blnRandomEvents == true)
                {
                    int intRandomNumber = Random.Next(0, intEventChance);
                    if (blnShowEvenNums == true)
                    {
                        Console.WriteLine(intRandomNumber.ToString());
                    }
                    if (intRandomNumber > 10 && intRandomNumber < 20)
                    {
                        Console.WriteLine(Crlf + "EVENT STARTED " + DateTime.Now.ToString("HH:mm:ss"));
                        blnEventStarted = true;
                        //EXECUTE EVENT

                        //-------------
                    }
                }
            }
            if (blnEventStarted == true)
            {
                if (intNumSeconds == intEventLength)
                {
                    Console.WriteLine(Crlf + "EVENT ENDED " + DateTime.Now.ToString("HH:mm:ss"));
                    blnEventStarted = false;
                }
                else
                {
                    intNumSeconds += 1;
                }              
            }
        }

        //Download Server Config          --MySQL--
        static private void GetServerVariables()
        {
            Console.WriteLine(Crlf+ "Fetching Server Config..., " + DateTime.Now.ToString("HH:mm:ss"));
            MySqlConnection mySQLServer = new MySqlConnection(strMySQLServerInfo);
            blnGotAccounts = true;
            // Connect to MySQL
            try
            {
                Console.WriteLine("Connecting to MySQL...., " + DateTime.Now.ToString("HH:mm:ss"));
                mySQLServer.Open();
                //Read Files
                try
                {
                    Console.WriteLine("Getting Variables from MySQL...., " + DateTime.Now.ToString("HH:mm:ss") + Crlf);
                    MySqlDataReader mySQLReader = null;

                    MySqlCommand mySQLCommand = mySQLServer.CreateCommand();

                    mySQLCommand.CommandText = "SELECT * FROM server_config LIMIT 0, 1";

                    mySQLReader = mySQLCommand.ExecuteReader();
                    //Get Variables from Database

                    //Store Data into Variables
                    while (mySQLReader.Read())
                    {
                        //Get Variables from MySQL
                        int intTotalAcounts2 = mySQLReader.GetInt32(0);
                        bool blnRandomEvents2 = mySQLReader.GetBoolean(1);
                        bool blnShowEvenNums2 = mySQLReader.GetBoolean(2);
                        int intEventChance2 = mySQLReader.GetInt32(3);
                        int intEventLengthMIN2 = mySQLReader.GetInt32(4);
                        int intUpdateFrequencyMIN2 = mySQLReader.GetInt32(5);
                        int intPlayerProximity2 = mySQLReader.GetInt32(6);
                        bool blnAllowConnect2 = mySQLReader.GetBoolean(7);

                        //Update Variables as needed
                        if (intTotalAcounts2 != intTotalAcounts)
                        {
                            intTotalAcounts = intTotalAcounts2;
                            Console.WriteLine("Total Acounts = " + intTotalAcounts.ToString());
                        }
                        if (blnRandomEvents2 != blnRandomEvents)
                        {
                            blnRandomEvents = blnRandomEvents2;
                            Console.WriteLine("Random Events = " + blnRandomEvents.ToString());
                        }
                        if (blnShowEvenNums2 != blnShowEvenNums)
                        {
                            blnShowEvenNums = blnShowEvenNums2;
                            Console.WriteLine("Show Event Chance = " + blnShowEvenNums.ToString());
                        }
                        if (intEventChance2 != intEventChance)
                        {
                            intEventChance = intEventChance2;
                            Console.WriteLine("Event Chance Range = 0 to " + intEventChance.ToString());
                        }
                        if (intEventLengthMIN != intEventLengthMIN2)
                        {
                            intEventLengthMIN = intEventLengthMIN2;
                            Console.WriteLine("Event Lenght = " + intEventLengthMIN.ToString() + " Min");
                        }
                        if (intUpdateFrequencyMIN2 != intUpdateFrequencyMIN)
                        {
                            intUpdateFrequencyMIN = intUpdateFrequencyMIN2;
                            Console.WriteLine("Update Frequency = " + intUpdateFrequencyMIN.ToString() + " Min");
                        }
                        if (intPlayerProximity2 != intPlayerProximity)
                        {
                            intPlayerProximity = intPlayerProximity2;
                            Console.WriteLine("Player Proximity = " + intPlayerProximity.ToString() + " Pixel Box Radius");
                        }
                        if (blnAllowConnect2 != blnAllowConnect)
                        {
                            blnAllowConnect = blnAllowConnect2;
                            Console.WriteLine("Allow Connections = " + blnAllowConnect.ToString());
                        }

                        //Convert Min to miliSeconds
                        intEventLength = intEventLengthMIN * 60;
                        intUpdateFrequency = intUpdateFrequencyMIN * 60;
                    }
                    mySQLReader.Close();
                    Console.WriteLine(Crlf + "Finished Fetching Server Config " + DateTime.Now.ToString("HH:mm:ss") + Crlf);
                }
                catch (Exception Error)
                {
                    //Print Error To Consol
                    Console.WriteLine(Crlf + Error.ToString() + Crlf);
                    Console.WriteLine("Get Variables from MySQL FAILED, " + DateTime.Now.ToString("HH:mm:ss"));
                }
            }
            catch (Exception Error)
            {
                //Print Error To Consol
                Console.WriteLine(Crlf + Error.ToString() + Crlf);
                Console.WriteLine("Connection to MySQL FAILED, " + DateTime.Now.ToString("HH:mm:ss") + Crlf);
                blnUpdateServer = false;
                blnAllowConnect = true;
                Console.WriteLine("Update Server " + blnUpdateServer.ToString());
                Console.WriteLine("Allow Connections " + blnAllowConnect.ToString() + Crlf);
                //Show Default Values
                Console.WriteLine("Server Has Kept Default/Last Updated Values....." + DateTime.Now.ToString("HH:mm:ss") + Crlf);
                Console.WriteLine("Total Acounts = " + intTotalAcounts.ToString());
                Console.WriteLine("Random Events = " + blnRandomEvents.ToString());
                Console.WriteLine("Show Event Chance = " + blnShowEvenNums.ToString());
                Console.WriteLine("Event Chance Range = 0 to " + intEventChance.ToString());
                Console.WriteLine("Event Lenght = " + intEventLength.ToString() + " Seconds");
                Console.WriteLine("Player Proximity = " + intPlayerProximity.ToString() + " Pixel Box Radius");
            }           
            try
            {
                mySQLServer.Close();
            }
            catch (Exception Error)
            {
                //Print Error To Consol
                Console.WriteLine(Crlf + Error.ToString()+ Crlf);
                Console.WriteLine("Failed disconnect  ?... what ? " + DateTime.Now.ToString("HH:mm:ss"));
            }

            // ReDeclare Player Arrays
            intPlayerXs = new int[intTotalAcounts];
            intPlayerYs = new int[intTotalAcounts];
            strPlayersData = new string[intTotalAcounts];
        }
        //Update Player Location On Quit  --MySQL--
        static private void SavePlayerData(int UserID, string UserName)
        {
            //Open connection
            MySqlConnection mySQLPlayerUpdate = new MySqlConnection(strMySQLServerInfo);
            try
            {
                mySQLPlayerUpdate.Open();
                try
                {
                    MySqlCommand mySQLCommand = mySQLPlayerUpdate.CreateCommand();

                    mySQLCommand.CommandText = "UPDATE user_accounts SET Last_X='" + intPlayerXs[UserID] + "', Last_Y='" + intPlayerYs[UserID] +
                        "' WHERE User_ID='" + UserID + "'";

                    mySQLCommand.ExecuteNonQuery();
                    Console.WriteLine(Crlf + "Player " + UserName + " User ID " + UserID + " Location To DataBase " + DateTime.Now.ToString("HH:mm:ss"));
                }
                catch (Exception Error)
                {
                    //Print Error To Consol
                    Console.WriteLine(Crlf + Error.ToString() + Crlf);
                    Console.WriteLine("Update Player Location to DataBase Failed, " + DateTime.Now.ToString("HH:mm:ss"));
                }
            }
            catch (Exception Error)
            {
                //Print Error To Consol
                Console.WriteLine(Crlf + Error.ToString() + Crlf);
                Console.WriteLine("Connect To player DataBase Failed, " + DateTime.Now.ToString("HH:mm:ss"));
            }
            mySQLPlayerUpdate.Close();



        }

        //HANDLE CLIENTS
        static private void HandleClients(object client)
        {
            //Client Variables
            bool blnReturnData = false;

            string strUserName = "";
            int intPMouseX;
            int intPMouseY;
            bool blnFireing;
            int intCurrentWeap;
            int intUserID = 0;
            int intHatID;
            int intTeamID;
            int intStealthIndex;

            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            string strRecivedData = "";
            //TEMP strRETURNDATA

            byte[] DataRecived = new byte[4096];
            int bytesRead;

            // loops until client disconnect or error
            // waits for player data then sends responce
            while (true)
            {
                bytesRead = 0;
                try
                {
                    //Blocks LOOP until a client sends Data
                    bytesRead = clientStream.Read(DataRecived, 0, 4096);
                    clientStream.Flush();
                }
                catch
                {
                    //a socket error has occured
                    break;
                }
                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();
                strRecivedData = (encoder.GetString(DataRecived, 0, bytesRead));

                //PRINT DATA
                //Console.WriteLine(strRecivedData);

                int intDataCount = 0;
                //Break String into DataPoints
                string[] DataArray = strRecivedData.Split('~');

                foreach (string DataPoint in DataArray)
                {
                    if (intDataCount == 0) { strUserName = DataPoint; }
                    if (intDataCount == 1) { intUserID = Convert.ToInt32(DataPoint); }
                    if (intDataCount == 2) { intTeamID = Convert.ToInt32(DataPoint); }
                    if (intDataCount == 3) { intPlayerXs[intUserID] = Convert.ToInt32(DataPoint); }
                    if (intDataCount == 4) { intPlayerYs[intUserID] = Convert.ToInt32(DataPoint); }
                    if (intDataCount == 5) { intPMouseX = Convert.ToInt32(DataPoint); }
                    if (intDataCount == 6) { intPMouseY = Convert.ToInt32(DataPoint); }
                    if (intDataCount == 7) { blnFireing = Convert.ToBoolean(DataPoint); }
                    if (intDataCount == 8) { intCurrentWeap = Convert.ToInt32(DataPoint); }
                    if (intDataCount == 9) { intHatID = Convert.ToInt32(DataPoint); }
                    if (intDataCount == 10) { intStealthIndex = Convert.ToInt32(DataPoint); }
                    intDataCount++;
                }
                //add recieved data to Return Data String
                strPlayersData[intUserID] = strRecivedData;
                //PROCESS DATA TO RETURN

                //Clear ReturnData
                string strReturnData = "@";

                for (int Index = 0; Index < intTotalAcounts; Index++)
                {
                    if (intPlayerXs[Index] + intPlayerProximity >= intPlayerXs[intUserID] &&
                        intPlayerXs[Index] - intPlayerProximity <= intPlayerXs[intUserID]){
                        if (intPlayerYs[Index] + intPlayerProximity >= intPlayerYs[intUserID] &&
                            intPlayerYs[Index] - intPlayerProximity <= intPlayerYs[intUserID])
                        {
                            //ADD PLAYER DATA TO SEND DATA STRING
                            strReturnData += strPlayersData[Index]+"@";
                        }
                    }
                }

                //SEND RETURN DATA
                if (blnReturnData == true)
                {
                    try
                    {
                        ASCIIEncoding SendEncoder = new ASCIIEncoding();
                        byte[] buffer = SendEncoder.GetBytes(strReturnData);

                        clientStream.Write(buffer, 0, buffer.Length);
                        clientStream.Flush();
                    }
                    catch
                    {
                        //a socket error has occured
                        break;
                    }
                }
                else { blnReturnData = true; }

            } //END OF LOOP

            // Save play Location on Quit
            if (blnUpdateServer == true)
            {
                SavePlayerData(intUserID, strUserName);
            }
            // RESET PLAYER ARRAY             X     Y
            strPlayersData[intUserID] = "~NUll~0~10000~10000~0~0~False~0";
            intPlayerXs[intUserID] = 10000;
            intPlayerYs[intUserID] = 10000;

            intNumClients--;
            Console.WriteLine(Crlf + "Client Disconnected " + DateTime.Now.ToString("HH:mm:ss"));
            Console.WriteLine("Clients Connected = " + intNumClients.ToString()+Crlf);
            tcpClient.Close();
     
        }
    }
}
