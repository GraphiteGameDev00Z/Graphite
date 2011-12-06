using System;
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
        public bool blnStartGame = false;
        MySQL mySQL = new MySQL();

        public LaunchScreen()
        {
            InitializeComponent();
            LauncherTimer.Start();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            mySQL.strUsername = txtUserName.Text;
            mySQL.strPassword = txtPassword.Text;

            //Check MySQL DATABASE
            mySQL.Login();

            //TEMP
            blnStartGame = true;
            this.Hide();

            if (mySQL.blnCorrect == true)
            {
                blnStartGame = true;
                this.Hide();
            }
            else { lblStatus.Text = "Incorrect Username Or Password"; }

            
        }

        private void LauncherTimer_Tick(object sender, EventArgs e)
        {
            label1.Text = mySQL.TruePass;
            label2.Text = mySQL.strPassword;
            label3.Text = mySQL.strUsername;
        }
    }
}
