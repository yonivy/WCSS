using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WBSS
{
    public class WebAppServer
    {
        public WebAppServer(string dns, string port)
        {
            Url = dns;
            Port = port;
        }

        public string Url { get; set; }
        public string Port { get; set; }
    }
}
