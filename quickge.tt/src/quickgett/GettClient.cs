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
        private int refreshInMinutes = 1;
        public string statusMessage { get; set; }

        public GettClient()
        {
            statusMessage = "Not logged in.";
        }

        public Boolean Login()
        {
            Console.WriteLine("Logging in...");
            try
            {
                Gett.Sharing.GettUser user = new Gett.Sharing.GettUser();
                user.Login(CredentialManager.apikey, CredentialManager.email, CredentialManager.password);
                
                user.RefreshMe();
                CredentialManager.valid = true;
                Console.WriteLine("Login user: {0} ({1})", user.Me.Email, user.Me.FullName);
                Console.WriteLine("Storage, you are using {0} of {1} bytes, you still have {2} bytes free.",
                    user.Me.Storage.Used, user.Me.Storage.Total, user.Me.Storage.Free);


                loggedin = true;
                KeepAlive();
                statusMessage = "Logged in.";
                this.user = user;
            }
            catch (WebException e)
            {
                //login was bad
                CredentialManager.valid = false;
                MessageBox.Show("Bad login!");
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
                Thread.Sleep(refreshInMinutes * 60 * 1000);
                user.RefreshLogin();
            }
        }

        public void KillKeepAliveThread()
        {
            Console.WriteLine("KILLED!");
            alive = false;
        }

        public void Upload(string path)
        {
            if (loggedin)
            {
                Upload upload = new Upload(path);
                GettShare share = user.Shares.CreateShare(upload.filename);
                GettFile file = share.CreateFile(upload.filename);
                Console.WriteLine("Uploading: " + upload.filename);
                file.UploadFileAsync(upload.path);
                file.UploadProgressChanged += UploadProgressChangedEventHandler;
                System.Windows.Forms.Clipboard.SetText(file.Info.GettUrl);
                Console.WriteLine("URL Copied to clipboard.");
            }
            else
            {
                MessageBox.Show("Not logged in!");
            }
        }

        public void UploadProgressChangedEventHandler(Object sender,
    UploadProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100)
            {
                statusMessage = "Upload Completed";
            }

            if (e.ProgressPercentage % 5 == 0)
            {
                statusMessage = "Upload Progress: " + e.ProgressPercentage + "%";
            }
        }

    }
}
