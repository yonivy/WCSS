using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace WBSS
{
    public partial class Main : Form
    {
        private static Color COLOR_ON = Color.LightGreen;
        private static Color COLOR_OFF = Color.LightPink;
        private static string SERVERS_ON_TEXT = "Servers On";
        private static string SERVERS_OFF_TEXT = "Servers Off";
        private static string MD_ON_TEXT = "Motion Detection On";
        private static string MD_OFF_TEXT = "Motion Detection Off";
        private static string ALERTS_ON_TEXT = "Alerts On";
        private static string ALERTS_OFF_TEXT = "Alerts Off";
        private static int CAMERA_CONTROL_V_SPACE = 10;

        private Process localServerProcess = null;
        private Process webAppServerProcess = null;
        private Process mdProcess = null;

        private DateTime lastModTime;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ConfigAccess ca = ConfigAccess.getInstance();

            if (ca.isFirstTime())
            {
                Registration r = new Registration();
                if (r.ShowDialog() == DialogResult.OK)
                {                   
                    ca.removeFirstTime();
                }
                else 
                {
                    Close();
                }
            }

            // display local server + web server data
            loadServerData(ca);

            // display global motion detection data
            loadMdData(ca);

            // display user data
            loadUserData(ca);

            // display cameras data
            loadCamerasData(ca);

            // start polling timer
            lastModTime = ConfigAccess.getLastWriteTime();
            timer.Start();
        }

        private void AddCamBtn_Click(object sender, EventArgs e)
        {
            new AddCamera().ShowDialog();            
        }

        private void camerasPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            CS cs = (CS)e.Control;

            int numCameras = camerasPanel.Controls.Count;
            if (numCameras > 1)
                cs.Top = camerasPanel.Controls[numCameras - 2].Top + (cs.Height + CAMERA_CONTROL_V_SPACE);
        }

        private void camerasPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            // get all the remaining controls
            List<CS> controls = new List<CS>();

            foreach (Control c in camerasPanel.Controls)
            {
                controls.Add((CS)c);
            }

            camerasPanel.Controls.Clear();

            // place each control in its new position
            foreach (CS c in controls)
            {
                int index = controls.IndexOf(c);

                if (index == 0)
                {
                    c.Top = 0;
                }
                else 
                {
                    c.Top = controls[index - 1].Top + (c.Height + CAMERA_CONTROL_V_SPACE);
                }

                camerasPanel.Controls.Add(c);
            }
        }

        private void userSaveButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(userNameTextBox.Text)
                && Validator.isEmailAddress(userEmailTextBox.Text))
            {
                ConfigAccess ca = ConfigAccess.getInstance();

                LocalServer lsData = ca.getLocalServerData();
                User oldUserData = ca.getUserData();
                User newUserData = new User(
                    userNameTextBox.Text,
                    userEmailTextBox.Text,
                    oldUserData.Password,
                    lsData.DynDnsUrl,
                    lsData.Port.ToString());

                ca.setUserData(newUserData);

                RemoteServerAccess rsa = new RemoteServerAccess();
                rsa.updateUser(oldUserData, newUserData);
            }
        }

        private void serverSaveBtn_Click(object sender, EventArgs e)
        {
            if (Validator.isURL(serverUrlTextBox.Text)
                && Validator.isPort(serverPortTextBox.Text)
                && Validator.isPort(webPortTextBox.Text))
            {
                ConfigAccess ca = ConfigAccess.getInstance();

                User oldUserData = ca.getUserData();
                User newUserData = new User(
                    oldUserData.Name,
                    oldUserData.Email,
                    oldUserData.Password,
                    serverUrlTextBox.Text,
                    serverPortTextBox.Text);

                LocalServer ls = new LocalServer(serverUrlTextBox.Text, serverPortTextBox.Text);
                WebAppServer ws = new WebAppServer(serverUrlTextBox.Text, webPortTextBox.Text);
                ca.setLocalServerData(ls);
                ca.setWebAppServerData(ws);

                RemoteServerAccess rsa = new RemoteServerAccess();
                rsa.updateUser(oldUserData, newUserData);
            }
        }

        private void mdOnCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            setGlobalMd();
        }

        private void alertsOnCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            setGlobalMd();
        }

        private void setGlobalMd()
        {
            bool mdOn = mdOnCheckBox.Checked;
            bool alertsOn = alertsOnCheckBox.Checked;

            Md md = new Md(mdOn, alertsOn);

            ConfigAccess ca = ConfigAccess.getInstance();
            ca.setMdData(md);

            setGlobalMdButtonsStyle(md);
        }

        private void setGlobalMdButtonsStyle(Md md)
        {
            if (md.On)
            {
                mdOnCheckBox.Text = MD_ON_TEXT;
                mdOnCheckBox.BackColor = COLOR_ON;
            }
            else 
            {
                mdOnCheckBox.Text = MD_OFF_TEXT;
                mdOnCheckBox.BackColor = COLOR_OFF;
            }

            if (md.AlertsOn)
            {
                alertsOnCheckBox.Text = ALERTS_ON_TEXT;
                alertsOnCheckBox.BackColor = COLOR_ON;
            }
            else 
            {
                alertsOnCheckBox.Text = ALERTS_OFF_TEXT;
                alertsOnCheckBox.BackColor = COLOR_OFF;
            }
            
        }

        private void startLocalServer()
        {
            if (localServerProcess != null)
                return;

            localServerProcess = new Process();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "python";
            startInfo.Arguments = "./server/server.py";
            //startInfo.UseShellExecute = false;
            //startInfo.RedirectStandardOutput = true;

            localServerProcess.StartInfo = startInfo;
            localServerProcess.Start();
        }

        private void startWebAppServer()
        {
            if (webAppServerProcess != null)
                return;

            webAppServerProcess = new Process();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "python";
            startInfo.Arguments = "./webapp/webapp.py";
            //startInfo.UseShellExecute = false;
            //startInfo.RedirectStandardOutput = true;

            webAppServerProcess.StartInfo = startInfo;
            webAppServerProcess.Start();
        }

        private void startMd()
        {
            if (mdProcess != null)
                return;

            mdProcess = new Process();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "python";
            startInfo.Arguments = "./md/md.py";
            //startInfo.UseShellExecute = false;
            //startInfo.RedirectStandardOutput = true;

            mdProcess.StartInfo = startInfo;
            mdProcess.Start();
        }

        private void stopLocalServer()
        {
            if (localServerProcess != null)
            {
                if (!localServerProcess.HasExited)
                {
                    localServerProcess.Kill();
                }
                localServerProcess = null;
            }
        }

        private void stopWebAppServer()
        {
            if (webAppServerProcess != null)
            {
                if (!webAppServerProcess.HasExited)
                {
                    webAppServerProcess.Kill();
                }
                webAppServerProcess = null;
            }
        }

        private void stopMd()
        {
            if (mdProcess != null)
            {
                if (!mdProcess.HasExited)
                {
                    mdProcess.Kill();                    
                }
                mdProcess = null;
            }
        }

        private void serverOnCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (serverOnCheckBox.Checked)
            {
                startLocalServer();
                startWebAppServer();
                if (mdOnCheckBox.Checked)
                {
                    startMd();
                }
                serverOnCheckBox.Text = SERVERS_ON_TEXT;
                serverOnCheckBox.BackColor = COLOR_ON;
            }
            else 
            {
                stopMd();
                stopWebAppServer();
                stopLocalServer();
                serverOnCheckBox.Text = SERVERS_OFF_TEXT;
                serverOnCheckBox.BackColor = COLOR_OFF;
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon.Visible = false;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void checkConfigDataChanged()
        {
            DateTime lastWriteTime = ConfigAccess.getLastWriteTime();

            if (DateTime.Compare(lastWriteTime, lastModTime) > 0)
            {
                ConfigAccess ca = ConfigAccess.getInstance();
                ca.reload();

                loadMdData(ca);
                loadCamerasData(ca);

                lastModTime = lastWriteTime;
            }
        }        

        private void loadServerData(ConfigAccess ca) 
        {
            LocalServer lServer = ca.getLocalServerData();

            serverUrlTextBox.Text = lServer.DynDnsUrl;
            serverPortTextBox.Text = lServer.Port.ToString();

            serverOnCheckBox.Text = SERVERS_OFF_TEXT;
            serverOnCheckBox.BackColor = COLOR_OFF;

            WebAppServer wServer = ca.getWebAppServerData();

            webPortTextBox.Text = wServer.Port.ToString();
        }

        private void loadMdData(ConfigAccess ca)
        {
            Md md = ca.getMdData();

            mdOnCheckBox.Checked = md.On ? true : false;
            
            if (localServerProcess != null && !localServerProcess.HasExited)
            {
                if (md.On)
                {
                    startMd();
                }
                else
                {
                    stopMd();
                }
            }

            setGlobalMdButtonsStyle(md);
        }

        private void loadUserData(ConfigAccess ca)
        {
            User user = ca.getUserData();

            userNameTextBox.Text = user.Name;
            userEmailTextBox.Text = user.Email;
        }

        private void loadCamerasData(ConfigAccess ca)
        {
            List<Camera> cameras = ca.getAllCameras();

            camerasPanel.Controls.Clear();

            foreach (Camera camera in cameras)
            {
                CS cs = new CS(camerasPanel, camera);
                camerasPanel.Controls.Add(cs);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            checkConfigDataChanged();
        }
    }
}
