using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Graphite.ClassFiles
{
    class WorldPlayers
    {
        //Global Variables

            //Players

            //World

            //Non-Player Data
        public int[] WeaponIDs;

        //Variables
            //Player
        Vector2 PlayerLoc;
        Vector2 PMouseLoc;       
        string strUserName;
        int intUserID;
        int intTeamID;
        bool blnFiring = false;
        int intCurrentWeapID;
        int[] intWeapSlot = {0,0,0};
        int intHatID;
        int intExp;
        int intMoney;
        
        //Sub Class Files
        TCPServer_Class TCPConnect = new TCPServer_Class();

        //Load On Game Start
        public void Initialize()
        {
        
        }
        //Update Player Per Tick
        public void Update()
        { 
            //MOVEMENT~~

            //Change Weap F1-F2-F3-Scroll Wheel
   
            //Fire Current Weap

            //SEND SERVER DATA
            TCPConnect.SendServerData(strUserName, intUserID,Convert.ToInt32(PlayerLoc.X), Convert.ToInt32(PlayerLoc.Y),
            Convert.ToInt32(PMouseLoc.X), Convert.ToInt32(PMouseLoc.Y), blnFiring, intCurrentWeapID);

            // GET SERVER DATA            
        }
        //Logged Into MySQL
        public void OnLoggedIn(string UserName, int UserID, int TeamID, Vector2 LastLocation,
                    int WeapSlot1, int WeapSlot2, int WeapSlot3, int HatID, int Money, int Exp)
        {
            //Update Player Variables From MySQL Database
            PlayerLoc = LastLocation;
            strUserName = UserName;
            intUserID = UserID;
            intTeamID = TeamID;
            intWeapSlot[1] = WeapSlot1;
            intWeapSlot[2] = WeapSlot2;
            intWeapSlot[3] = WeapSlot3;
            intHatID = HatID;
            intMoney = Money;
            intExp = Exp;

            // After User Data is Retrived Connect to RelayServer
            TCPConnect.Connect();
            TCPConnect.Threading();
        }
        //Update Player MySQL
        public void UpdatePlayerBase()
        {

        }
    }
}
