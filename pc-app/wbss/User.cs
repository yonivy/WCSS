using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WBSS
{
    public class User
    {
        public User(string name, string email)
        {
            Name = name;
            Email = email;
            Password = null;
        }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            Url = null;
            Port = null;
        }

        public User(string name, string email, string password, string url, string port)
        {
            Name = name;
            Email = email;
            Password = password;
            Url = url;
            Port = port;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string Port { get; set; }
    }
}
