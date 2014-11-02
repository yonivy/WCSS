using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace WBSS
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string email = emailTextBox.Text;
            string password = passMaskedTextBox.Text;
            string rePassword = conPassMaskedTextBox.Text;

            string dns = dyndnsTextBox.Text;
            string port = portTextBox.Text;

            bool valid =
                !String.IsNullOrEmpty(name)
                && Validator.isEmailAddress(email)
                && Validator.isPassword(password)
                && password == rePassword
                && Validator.isURL(dns)
                && Validator.isPort(port);

            if (valid)
            {
               // send details to remote server
               WebRequest request = WebRequest.Create("https://api.parse.com/1/classes/LocalServer");

               request.ContentType = "application/json";
               request.Method = "POST";
               request.Headers["X-Parse-Application-Id"] = "rrXt4pI1S7jo2E4kezSzmDce3CGd4EZbFVas7CM8";
               request.Headers["X-Parse-REST-API-Key"] = "s0c9Xp2ZX5AoCe3T95PIQ5OTYj8XOR8C2ug29lQU";

               string passwordHash = sha256_hash(password);
               using (var streamWriter = new StreamWriter(request.GetRequestStream()))
               {
                   string json =
                       "{\"username\":\"" + name + "\"," +
                       "\"email\":\"" + email + "\"," +
                       "\"password\":\"" + passwordHash + "\"," +
                       "\"url\":\"" + dns + "\"," +
                       "\"port\":\"" + port + "\"}";

                   streamWriter.Write(json);
                   streamWriter.Flush();
                   streamWriter.Close();
               }

               var httpResponse = (HttpWebResponse)request.GetResponse();

               using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
               {
                   var result = streamReader.ReadToEnd();
               }

               ConfigAccess ca = ConfigAccess.getInstance();
               ca.setUserData(new User(name, email, passwordHash));
               ca.setLocalServerData(new LocalServer(dns, port));
               ca.setWebAppServerData(new WebAppServer(dns, ""));

               this.DialogResult = DialogResult.OK;
               Close();
            }
            else
               MessageBox.Show("Not all fields are valid");
        }

        private String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
