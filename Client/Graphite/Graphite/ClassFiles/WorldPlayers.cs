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
        public int[] HatIDs;

        //Variables
        //Player (sent to server)
        public Vector2 playerLoc;
        public Vector2 pMouseLoc;
        public string strUserName = "TEST";  //unless set by Login
        public int intUserID = 1;            //unless set by Login
        public int intTeamID = 1;            //unless set by Login
        public bool blnFiring = false;
        public int intCurrentWeapID;
        public int intHatID;
        int intPlayerWeight;

        //Player (not sent to server)
        Vector2 moveSpeed;        
        public int[] intWeapSlot = { 0, 0, 0 };
        public int intExp;
        public int intMoney;
        //Sub Class Files
        TCPServer_Class TCPConnect = new TCPServer_Class();

        //Load On Game Start
        public void Initialize()
        {
            TCPConnect.Connect();
            TCPConnect.Threading();
        }
        //Update Player Per Tick
        public void Update()
        {
            KeyboardState keystate = Keyboard.GetState();
                                                                        // Change For Weight System
            if (keystate.IsKeyDown(Keys.LeftControl)) { moveSpeed -= new Vector2(2, 2); } //Crouching
            else if (keystate.IsKeyDown(Keys.LeftShift)) { moveSpeed += new Vector2(2, 2); }//Sprinting

            moveSpeed.Normalize(); //prevents moving faster diagonally

            //gotta make these vars for keymapping
            if (keystate.IsKeyDown(Keys.W)) { playerLoc.Y -= moveSpeed.Y; }
            if (keystate.IsKeyDown(Keys.S)) { playerLoc.Y += moveSpeed.Y; }
            if (keystate.IsKeyDown(Keys.A)) { playerLoc.X -= moveSpeed.X; }
            if (keystate.IsKeyDown(Keys.D)) { playerLoc.X += moveSpeed.X; }
            //TEMP
            if (keystate.IsKeyDown(Keys.Escape)) { Environment.Exit(0); }

            //Mouse
            MouseState mousestate = Mouse.GetState();
            pMouseLoc = new Vector2(mousestate.X, mousestate.Y);

            if (mousestate.LeftButton == ButtonState.Pressed)
            {
                blnFiring = true;
            }
            else { blnFiring = false;}

            //Change Weap 1-2-3-Scroll Wheel

                //SEND SERVER DATA (add )
                TCPConnect.SendServerData(strUserName, intUserID, intTeamID, Convert.ToInt32(playerLoc.X), Convert.ToInt32(playerLoc.Y),
                Convert.ToInt32(pMouseLoc.X), Convert.ToInt32(pMouseLoc.Y), blnFiring, intCurrentWeapID, intHatID, intPlayerWeight);

            // GET SERVER DATA
        }
        //Update Player MySQL
        public void UpdatePlayerBase()
        {

        }

    }
}

