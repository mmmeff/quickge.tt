using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.Threading;


namespace quickge.tt.src.quickgett
{
    class QuickGett : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;
        private GettClient client;
        private Boolean running = false;
        

        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new QuickGett());
        }
        
        public QuickGett()
        {

            // Create a tray menu
            trayMenu = new ContextMenu();

            trayMenu.MenuItems.Add(new MenuItem("Configure", Configure));
            trayMenu.MenuItems.Add(new MenuItem("Upload", OnMenuUpload));
            trayMenu.MenuItems.Add(new MenuItem("Exit", OnExit));

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            
            trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;

            //create a client
            client = new GettClient();

            //check for user/pass credentials in settings
            //if missing, prompt for them.

            if (!CredentialManager.ValidCredentials())
            {
                LoginForm lf = new LoginForm(client);
                lf.Show();
            }
            else
            {
                //credentials exist, auto login
                client.Login();
            }

            //start status updating thread
            running = true;
            Thread statusThread = new Thread(new ThreadStart(this.UpdateSysTrayStatusThread));
            statusThread.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Visible = false; // Hide form window.
            this.ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            client.KillKeepAliveThread();
            client = null;
            running = false;
            this.Dispose();
            Application.Exit();
            Application.ExitThread();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                // Release the icon resource.
                trayIcon.Dispose();
            }

            base.Dispose(isDisposing);
        }

        public void OnMenuUpload(Object sender, EventArgs e)
        {
            //browse to and upload file
            OpenFileDialog ofd = new OpenFileDialog();
            System.Windows.Forms.DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                client.Upload(ofd.FileName);
            }
        }

        public void Configure(Object sender, EventArgs e)
        {
            CredentialManager.ClearCredentials();
            LoginForm lf = new LoginForm(client);
            lf.Show();
        }

        public void UpdateSysTrayStatusThread()
        {
            while (running)
            {
                Thread.Sleep(100);
                try
                {
                    trayIcon.Text = client.statusMessage;
                }
                catch (NullReferenceException e)
                {
                    //thread unsafe, errors when closing. Safe to ignore.
                }
            }
        }

        
    }
}
