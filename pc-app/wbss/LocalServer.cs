using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WBSS
{
    public class LocalServer
    {
        public LocalServer(string dns, string port)
        {
            DynDnsUrl = dns;
            Port = port;
        }

        public string DynDnsUrl { get; set; }
        public string Port { get; set; }
    }
}
