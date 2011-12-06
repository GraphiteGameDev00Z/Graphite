using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.ComponentModel;

namespace Graphite
{
    class TCPServer_Class
    {
        //Global Variables
        public bool CloseThread = false;
        
        //Variables
        static string strServerIP = "127.0.0.1";
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(strServerIP), 16487);
        TcpClient client = new TcpClient();

        public string strRecivedData;
        bool blnDataReceivedStart = false;

        public void Connect()
        { 
            try
            {
                client.Connect(serverEndPoint);
            }
            catch
            {
                //COULD NOT CONNECT

            }        
        }
        public void Threading()
        {
            Thread ReceiveDataThread = new Thread(new ThreadStart(ReceiveData));
            if (blnDataReceivedStart == false)
            {
                //START THREAD
                ReceiveDataThread.Start();
            }
            if (CloseThread == true)
            {
                ReceiveDataThread.Abort();
            }
        }

        //SEND PLAYER DATA TO SERVER    --string--
        public void SendServerData(string strUserName, int intUSERID, int Xpos, int Ypos,
            int MouseXpos, int MouseYpos, bool Firing, int intCurrentWeapon)
        {
            string strData = strUserName + "~" + intUSERID.ToString() + "~" + Xpos.ToString() + "~" + Ypos.ToString() +
   "~" + MouseXpos.ToString() + "~" + MouseYpos.ToString() + "~" + Firing.ToString() +
   "~" + intCurrentWeapon.ToString() + "~";

            NetworkStream clientStream = client.GetStream();

            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = encoder.GetBytes(strData);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }

        // RECEIVE DATA FROM SERVER     --string-- 
        public void ReceiveData()
        {
            blnDataReceivedStart = true;

            while (true)
            {
                TcpClient tcpClient = (TcpClient)client;
                NetworkStream clientStream = tcpClient.GetStream();

                byte[] DataRecived = new byte[4096];
                int bytesRead;

                //Receive Data
                bytesRead = 0;
                try
                {
                    //Blocks LOOP until a Server sends Data
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
                    //the Server has disconnected the Player
                    break;
                 }
                    //message has successfully been received
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    strRecivedData = (encoder.GetString(DataRecived, 0, bytesRead));
            }//loop
        }
    }
}

