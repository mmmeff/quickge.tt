using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;

namespace quickge.tt.src.quickgett
{
    public static class CredentialManager
    {
        private static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("Shazbot! Shazbot! Shazbot! Shazbot! ");

        public static string email
        {
            get { return (ToInsecureString(DecryptString(Properties.Settings.Default.email))); }
            set { Properties.Settings.Default.email = EncryptString(ToSecureString(value)); }
        }
        public static string password
        {
            get { return (ToInsecureString(DecryptString(Properties.Settings.Default.pass))); }
            set { Properties.Settings.Default.pass = EncryptString(ToSecureString(value)); }
        }
        public static string apikey
        {
            get { return (ToInsecureString(DecryptString(Properties.Settings.Default.apikey))); }
            set { Properties.Settings.Default.apikey = EncryptString(ToSecureString(value)); }
        }
        public static Boolean valid
        {
            get { return Properties.Settings.Default.valid; }
            set { Properties.Settings.Default.valid = value; }
        }

        public static Boolean ValidCredentials()
        {
            return valid;
        }

        public static void StoreCredentials(string e, string pass)
        {
            email = e;
            password = pass;
        }
        public static void ClearCredentials()
        {
            email = "";
            password = "";
        }

        public static string EncryptString(System.Security.SecureString input)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DecryptString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }
    }
}
