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

            CredentialManager.StoreCredentials(
                (Application.OpenForms["LoginForm"].Controls["textbox1"] as TextBox).Text,
                (Application.OpenForms["LoginForm"].Controls["textbox2"] as TextBox).Text);

            if (client.Login())
            {
                this.Hide();
            }
            else
            {
                (Application.OpenForms["LoginForm"].Controls["textbox1"] as TextBox).Text = "";
                (Application.OpenForms["LoginForm"].Controls["textbox2"] as TextBox).Text = "";
                CredentialManager.ClearCredentials();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://ge.tt");
        }

    }
}
