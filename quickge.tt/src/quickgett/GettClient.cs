using System;
using Gett.Sharing;
using System.Threading;
using System.IO;
using System.Net;
using System.Windows.Forms;


namespace quickge.tt.src.quickgett
{
    public class GettClient
    {
        private GettUser user;
        private Boolean alive, loggedin;
        private int refreshInSeconds = 3;
        public string statusMessage { get; set; }
        private NotifyIcon trayIcon;

        public GettClient(NotifyIcon trayIcon)
        {
            statusMessage = "Not logged in.";
            this.trayIcon = trayIcon;
        }

        public Boolean Login()
        {
            try
            {
                Gett.Sharing.GettUser user = new Gett.Sharing.GettUser();
                user.Login(CredentialManager.apikey, CredentialManager.email, CredentialManager.password);
                user.RefreshMe();
                CredentialManager.valid = true;

                loggedin = true;
                KeepAlive();
                statusMessage = "Logged in.";
                Notify("Succesfully logged into Ge.tt");
                this.user = user;
            }
            catch (WebException e)
            {
                //login was bad
                CredentialManager.valid = false;
                MessageBox.Show("text", "caption");
            }
            return CredentialManager.valid;
        }

        public void Logout()
        {
            loggedin = false;
            KillKeepAliveThread();
            user = null;
            statusMessage = "Not logged in.";
        }

        public void KeepAlive()
        {
            alive = true;
            Thread keepAlive = new Thread(new ThreadStart(this.KeepAliveThread));
            keepAlive.Start();
        }

        public void KeepAliveThread()
        {
            while (alive)
            {
                Thread.Sleep(refreshInSeconds *  1000);
                user.RefreshLogin();
            }
            Thread.EndThreadAffinity();
        }

        public void KillKeepAliveThread()
        {
            Console.WriteLine("KILLED!");
            alive = false;
        }

        public void Upload(string path)
        {
            Upload upload = new Upload(path);
            if (loggedin)
            {
                GettShare share = user.Shares.CreateShare(upload.filename);
                GettFile file = share.CreateFile(upload.filename);
                file.UploadFileAsync(upload.path);
                file.UploadProgressChanged += UploadProgressChangedEventHandler;
                file.UploadFileCompleted += UploadFileCompleted;
                System.Windows.Forms.Clipboard.SetText(file.Info.GettUrl);
                Notify("Upload of " + upload.filename +" started. The URL to the download page has been copied to your clipboard.");
            }
            else
            {
                Notify("Upload of " + upload.filename + " failed!");
                MessageBox.Show("Not logged in!");
            }
        }

        public void UploadProgressChangedEventHandler(Object sender,
    UploadProgressChangedEventArgs e)
        {
            statusMessage = "Upload Progress: " + e.ProgressPercentage + "%";
        }

        public void UploadFileCompleted(Object sender, UploadFileCompletedEventArgs e)
        {
            Notify("Upload completed!");
        }

        public void Notify(string message)
        {
            trayIcon.BalloonTipText = message;
            trayIcon.BalloonTipTitle = "quickge.tt";
            trayIcon.Icon = Properties.Resources.favicon;
            trayIcon.Visible = true;
            trayIcon.ShowBalloonTip(3);
        }

    }
}
