using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace WBSS
{
    public class RemoteServerAccess
    {
        private static string PARSE_APPLICATION_ID = "rrXt4pI1S7jo2E4kezSzmDce3CGd4EZbFVas7CM8";
        private static string PARSE_REST_API_KEY = "s0c9Xp2ZX5AoCe3T95PIQ5OTYj8XOR8C2ug29lQU";

        public void registerUser(User user)
        {
            WebRequest request = WebRequest.Create("https://api.parse.com/1/classes/LocalServer");

            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers["X-Parse-Application-Id"] = PARSE_APPLICATION_ID;
            request.Headers["X-Parse-REST-API-Key"] = PARSE_REST_API_KEY;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json =
                    "{\"username\":\"" + user.Name + "\"," +
                    "\"email\":\"" + user.Email + "\"," +
                    "\"password\":\"" + user.Password + "\"," +
                    "\"url\":\"" + user.Url + "\"," +
                    "\"port\":\"" + user.Port + "\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }         
        }

        public void updateUser(User oldUserData, User newUserData)
        {
            string objectId = getUser(oldUserData);

            WebRequest request = WebRequest.Create("https://api.parse.com/1/classes/LocalServer/" + objectId);

            request.ContentType = "application/json";
            request.Method = "PUT";
            request.Headers["X-Parse-Application-Id"] = PARSE_APPLICATION_ID;
            request.Headers["X-Parse-REST-API-Key"] = PARSE_REST_API_KEY;

            

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json =
                    "{\"username\":\"" + newUserData.Name + "\"," +
                    "\"email\":\"" + newUserData.Email + "\"," +
                    "\"url\":\"" + newUserData.Url + "\"," +
                    "\"port\":\"" + newUserData.Port + "\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        public string getUser(User user)
        {
            string json = "where:" +
                    "{\"username\":\"" + user.Name + "\"," +
                    "\"email\":\"" + user.Email + "\"," +
                    "\"password\":\"" + user.Password + "\"," +
                    "}";

            string urlEncodedJson = urlEncode(json);
            
            WebRequest request = WebRequest.Create("https://api.parse.com/1/classes/LocalServer?" + urlEncodedJson);

            //request.ContentType = "application/json";
            request.Method = "GET";
            request.Headers["X-Parse-Application-Id"] = PARSE_APPLICATION_ID;
            request.Headers["X-Parse-REST-API-Key"] = PARSE_REST_API_KEY;            

            var httpResponse = (HttpWebResponse)request.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {                
                var result = streamReader.ReadToEnd();
                JsonResult jsonResult = JsonConvert.DeserializeObject<JsonResult>(result);
                return jsonResult.results[0].objectId;
            }
        }

        private string urlEncode(string json)
        {
            return json.Replace("{", "%7b").Replace("}", "%7d").Replace("\"", "%22");
        }

        public class JsonResult
        {
            public ParseObject[] results;
        }

        public class ParseObject
        {
            public string objectId;
            public string createdAt;
            public string updatedAt;

            public string username;
            public string email;
            public string password;
            public string url;
            public string port;
        }
    }
}
