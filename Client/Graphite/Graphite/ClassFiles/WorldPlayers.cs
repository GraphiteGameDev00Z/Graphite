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



namespace Graphite
{
    class WorldPlayers
    {

        //Global Variables

        //Players

        //World

        //Non-Player Data
        public int[] WeaponIDs;

        //Variables
        //Player (sent to server)
        public Vector2 playerLoc;
        public Vector2 pMouseLoc;
        string strUserName;
        int intUserID;
        int intTeamID;
        bool blnFiring = false;

        int intCurrentWeapID;
        int[] intWeapSlot = { 0, 0, 0 };
        int intHatID;
        int intExp;
        int intMoney;
        //Player (not sent to server)
        Vector2 moveSpeed;

        //Sub Class Files
        TCPServer_Class TCPConnect = new TCPServer_Class();

        //Load On Game Start
        public void Initialize()
        {

        }
        //Update Player Per Tick
        public void Update()
        {
            KeyboardState keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.LeftControl)) { moveSpeed -= new Vector2(2, 2); } //Crouching
            else if (keystate.IsKeyDown(Keys.LeftShift)) { moveSpeed += new Vector2(2, 2); }//Sprinting

            moveSpeed.Normalize(); //prevents moving faster diagonally

            //gotta make these vars for keymapping
            if (keystate.IsKeyDown(Keys.W)) { playerLoc.Y -= moveSpeed.Y; }
            if (keystate.IsKeyDown(Keys.S)) { playerLoc.Y += moveSpeed.Y; }
            if (keystate.IsKeyDown(Keys.A)) { playerLoc.X -= moveSpeed.X; }
            if (keystate.IsKeyDown(Keys.D)) { playerLoc.X += moveSpeed.X; }



            //Change Weap F1-F2-F3-Scroll Wheel

            MouseState mousestate = Mouse.GetState();




            if (mousestate.LeftButton == ButtonState.Pressed)
            {
                blnFiring = true;
            }

            //SEND SERVER DATA (add playerWeight)
            TCPConnect.SendServerData(strUserName, intUserID, Convert.ToInt32(playerLoc.X), Convert.ToInt32(playerLoc.Y),
            Convert.ToInt32(pMouseLoc.X), Convert.ToInt32(pMouseLoc.Y), blnFiring, intCurrentWeapID);

            // GET SERVER DATA            
        }
        //Logged Into MySQL

        public void OnLoggedIn(string UserName, int UserID, int TeamID, Vector2 LastLocation,
                    int WeapSlot1, int WeapSlot2, int WeapSlot3, int HatID, int Money, int Exp)
        {
            //Update Player Variables From MySQL Database
            playerLoc = LastLocation;
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

    

