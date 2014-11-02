using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Globalization;
using System.IO;

namespace WBSS
{
    class ConfigAccess
    {        
        static string subPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server\\data\\");

        private static string CONFIG_FILE_PATH = Path.Combine(subPath, "config.xml");
        private XmlDocument configFile;

        private static ConfigAccess instance = null;

        public static ConfigAccess getInstance() {
            if (instance == null)
            {
                instance = new ConfigAccess();
            }

            return instance;
        }

        private ConfigAccess()
        {
            configFile = new XmlDocument();
            configFile.Load(CONFIG_FILE_PATH);
        }      

        public static DateTime getLastWriteTime()
        {
            return File.GetLastWriteTime(CONFIG_FILE_PATH);
        }

        public bool isFirstTime()
        {
            XmlNode rootNode = configFile.SelectSingleNode("/config");
            return Boolean.Parse(rootNode.Attributes["first"].Value);
        }

        public void removeFirstTime()
        {
            XmlNode rootNode = configFile.SelectSingleNode("/config");
            rootNode.Attributes["first"].Value = "false";

            saveChanges();
        }

        public void reload()
        {
            configFile = new XmlDocument();
            configFile.Load(CONFIG_FILE_PATH);
        }

        private void saveChanges()
        {
            configFile.Save(CONFIG_FILE_PATH);
        }

        public void registerUserData(User user)
        {
            XmlNode nameNode = configFile.SelectSingleNode("//user/name");
            XmlNode emailNode = configFile.SelectSingleNode("//user/email");
            XmlNode passwordNode = configFile.SelectSingleNode("//user/password");

            nameNode.InnerText = user.Name;
            emailNode.InnerText = user.Email;
            passwordNode.InnerText = user.Password;

            saveChanges();
        }

        public void setUserData(User user)
        {
            XmlNode nameNode = configFile.SelectSingleNode("//user/name");
            XmlNode emailNode = configFile.SelectSingleNode("//user/email");
            XmlNode passwordNode = configFile.SelectSingleNode("//user/password");

            nameNode.InnerText = user.Name;
            emailNode.InnerText = user.Email;
            passwordNode.InnerText = user.Password;

            saveChanges();
        }

        public User getUserData()
        {
            XmlNode nameNode = configFile.SelectSingleNode("//user/name");
            XmlNode emailNode = configFile.SelectSingleNode("//user/email");
            XmlNode passwordNode = configFile.SelectSingleNode("//user/password");

            return new User(nameNode.InnerText, emailNode.InnerText, passwordNode.InnerText);
        }
        

        public void setMdData(Md md)
        {
            XmlNode mdNode = configFile.SelectSingleNode("//motionDetection");
            XmlNode alertsNode = configFile.SelectSingleNode("//motionDetection/alerts");

            mdNode.Attributes["on"].Value = md.On.ToString().ToLower();
            alertsNode.InnerText = md.AlertsOn.ToString().ToLower();

            saveChanges();
        }

        public Md getMdData()
        {
            XmlNode mdNode = configFile.SelectSingleNode("//motionDetection");
            XmlNode alertsNode = configFile.SelectSingleNode("//motionDetection/alerts");

            Md md = new Md(Boolean.Parse(mdNode.Attributes["on"].Value), Boolean.Parse(alertsNode.InnerText));

            return md;
        }

        public void setLocalServerData(LocalServer server)
        {
            XmlNode urlNode = configFile.SelectSingleNode("//localServer/url");
            XmlNode portNode = configFile.SelectSingleNode("//localServer/port");

            urlNode.InnerText = server.DynDnsUrl;
            portNode.InnerText = server.Port.ToString();

            saveChanges();
        }

        public LocalServer getLocalServerData()
        {
            XmlNode urlNode = configFile.SelectSingleNode("//localServer/url");
            XmlNode portNode = configFile.SelectSingleNode("//localServer/port");

            LocalServer server = new LocalServer(urlNode.InnerText, portNode.InnerText);

            return server;
        }

        public void setWebAppServerData(WebAppServer server)
        {
            XmlNode urlNode = configFile.SelectSingleNode("//webAppServer/url");
            XmlNode portNode = configFile.SelectSingleNode("//webAppServer/port");

            urlNode.InnerText = server.Url;
            portNode.InnerText = server.Port.ToString();

            saveChanges();
        }

        public WebAppServer getWebAppServerData()
        {
            XmlNode urlNode = configFile.SelectSingleNode("//webAppServer/url");
            XmlNode portNode = configFile.SelectSingleNode("//webAppServer/port");

            WebAppServer server = new WebAppServer(urlNode.InnerText, portNode.InnerText);

            return server;
        }

        public void setCamera(Camera camera)
        {
            XmlNode camNode = configFile.SelectSingleNode("//camera[@id=" + camera.Id + "]");

            camNode.SelectSingleNode("name").InnerText = camera.Name;
            camNode.SelectSingleNode("url").InnerText = camera.URL;
            camNode.SelectSingleNode("port").InnerText = camera.Port.ToString();
            camNode.SelectSingleNode("streamPath").InnerText = camera.Stream;
            camNode.SelectSingleNode("motionDetection").InnerText = camera.MotionDetection.ToString().ToLower();
            camNode.SelectSingleNode("threshold").InnerText = camera.Threshold.ToString();
            camNode.SelectSingleNode("alerts").InnerText = camera.Alerts.ToString().ToLower();
            camNode.SelectSingleNode("recordTime").InnerText = camera.RecordTime.ToString("mm:ss");

            saveChanges();
        }

        public List<Camera> getAllCameras()
        {
            XmlNodeList camerasNodes = configFile.SelectNodes("//cameras/camera");

            List<Camera> cameras = new List<Camera>();

            foreach (XmlNode c in camerasNodes)
            {
                Camera cam = new Camera(
                    c.SelectSingleNode("name").InnerText,
                    c.SelectSingleNode("url").InnerText,
                    Convert.ToUInt32(c.SelectSingleNode("port").InnerText),
                    c.SelectSingleNode("streamPath").InnerText,
                    Convert.ToBoolean(c.SelectSingleNode("motionDetection").InnerText),
                    Convert.ToDouble(c.SelectSingleNode("threshold").InnerText),
                    Convert.ToBoolean(c.SelectSingleNode("alerts").InnerText),
                    DateTime.ParseExact(c.SelectSingleNode("recordTime").InnerText, "mm:ss", CultureInfo.InvariantCulture),
                    c.Attributes["id"].Value);

                cameras.Add(cam);
            }

            return cameras;
        }

        public void addCamera(Camera camera)
        {
            XmlNode camerasNode = configFile.SelectSingleNode("//cameras");
            XmlNode lastCamNode = configFile.SelectSingleNode("//camera[position() = last()]");

            uint lastId = 0;

            if(lastCamNode != null)
                lastId = Convert.ToUInt32(lastCamNode.Attributes["id"].Value);

            XmlNode newCamera = configFile.CreateNode(XmlNodeType.Element, "camera", null);
            XmlAttribute newCameraId = configFile.CreateAttribute("id");
            newCameraId.Value = (lastId + 1).ToString();
            newCamera.Attributes.Append(newCameraId);

            XmlNode name = configFile.CreateNode(XmlNodeType.Element, "name", null);
            name.InnerText = camera.Name;
            XmlNode url = configFile.CreateNode(XmlNodeType.Element, "url", null);
            url.InnerText = camera.URL;
            XmlNode port = configFile.CreateNode(XmlNodeType.Element, "port", null);
            port.InnerText = camera.Port.ToString();
            XmlNode stream = configFile.CreateNode(XmlNodeType.Element, "streamPath", null);
            stream.InnerText = camera.Stream;
            XmlNode md = configFile.CreateNode(XmlNodeType.Element, "motionDetection", null);
            md.InnerText = camera.MotionDetection.ToString().ToLower();
            XmlNode threshold = configFile.CreateNode(XmlNodeType.Element, "threshold", null);
            threshold.InnerText = camera.Threshold.ToString();
            XmlNode alerts = configFile.CreateNode(XmlNodeType.Element, "alerts", null);
            alerts.InnerText = camera.Alerts.ToString().ToLower();
            XmlNode recordTime = configFile.CreateNode(XmlNodeType.Element, "recordTime", null);
            recordTime.InnerText = camera.RecordTime.ToString("mm:ss");

            newCamera.AppendChild(name);
            newCamera.AppendChild(url);
            newCamera.AppendChild(port);
            newCamera.AppendChild(stream);
            newCamera.AppendChild(md);
            newCamera.AppendChild(threshold);
            newCamera.AppendChild(alerts);
            newCamera.AppendChild(recordTime);

            camerasNode.AppendChild(newCamera);

            saveChanges();
        }

        public void deleteCamera(Camera camera)
        {
            XmlNode camNode = configFile.SelectSingleNode("//camera[@id=" + camera.Id + "]");

            camNode.ParentNode.RemoveChild(camNode);

            saveChanges();
        }
    }
}
