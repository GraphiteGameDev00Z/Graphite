using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Graphite
{
    public partial class LaunchScreen : Form
    {
        //Global Variables
        public bool blnStartGame = false;
        //Variables
        private const string FILE_NAME = "PlayerSave.Sledge";
        //Class Files
        MySQL mySQL = new MySQL();
        WorldPlayers WP = new WorldPlayers();

        public LaunchScreen()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            mySQL.strUsername = txtUserName.Text;
            mySQL.strPassword = txtPassword.Text;

            // ByPass LOGIN
            if (mySQL.blnBYPASS == true)
            {
                // Start The Game
                blnStartGame = true;
                this.Close();
            }
            else
            {
                //Check MySQL DATABASE
                mySQL.Login();

                if (mySQL.blnCorrect == true)
                {
                    // Start The Game
                    blnStartGame = true;
                    this.Close();
                }
                else { lblStatus.Text = "Password Or Username Incorrect"; }
            }
        }

        private void btnKeepDetail_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
