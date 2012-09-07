using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security;
using System.Diagnostics;

namespace quickge.tt.src.quickgett
{
    public partial class LoginForm : Form
    {
        public GettClient client;

        public LoginForm(GettClient client)
        {
            this.client = client;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            AttemptLogin();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://ge.tt");
        }

        private void textBox2_keyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                AttemptLogin();
        }

        private void AttemptLogin()
        {
            this.Hide();
            CredentialManager.StoreCredentials(
                (Application.OpenForms["LoginForm"].Controls["textbox1"] as TextBox).Text,
                (Application.OpenForms["LoginForm"].Controls["textbox2"] as TextBox).Text);

            if (client.Login())
            {

                this.Dispose();
                this.Close();
            }
            else
            {
                this.Show();
                (Application.OpenForms["LoginForm"].Controls["textbox1"] as TextBox).Text = "";
                (Application.OpenForms["LoginForm"].Controls["textbox2"] as TextBox).Text = "";
                CredentialManager.ClearCredentials();
            }
        }
    }
}
