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

        public LaunchScreen()
        {
            InitializeComponent();
        }

        private void LaunchScreen_Load(object sender, EventArgs e)
        {

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            mySQL.strUsername = txtUserName.Text;
            mySQL.strPassword = txtPassword.Text;

            //Check MySQL DATABASE
            mySQL.Login();

            if (mySQL.blnCorrect == true)
            {
                blnStartGame = true;
                this.Hide();
            }
            else { lblStatus.Text = "Password Or Username Incorrect"; }
        }

        private void btnKeepDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (btnKeepDetail.Checked == true) 
            {
                if (File.Exists(FILE_NAME))
                {
                    return;
                }
                using (FileStream fs = new FileStream(FILE_NAME, FileMode.CreateNew))
                {
                    // Create the writer for data.
                    using (BinaryWriter w = new BinaryWriter(fs))
                    {
                        // Write data to Test.data.
                        for (int i = 0; i < 11; i++)
                        {
                            w.Write("HELLO");
                        }
                    }
                }
            }
            if (btnKeepDetail.Checked == false)
            {
            }
        }
    }
}
