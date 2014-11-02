using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WBSS
{
    public partial class CS : UserControl
    {
        private Panel parent;
        private Camera camera;

        public CS()
        {
            InitializeComponent();

            this.parent = null;
            this.camera = null;

            saveBtn.Text = "Add Camera";

            deleteBtn.Enabled = false;
            deleteBtn.Visible = false;
        }

        public CS(Panel parent, Camera camera)
        {
            InitializeComponent();

            this.parent = parent;
            this.camera = camera;
        }

        private void CS_Load(object sender, EventArgs e)
        {
            if (this.camera != null)
            {
                nameTextBox.Text = camera.Name;
                urlTextBox.Text = camera.URL;
                portTextBox.Text = camera.Port.ToString();
                streamTextBox.Text = camera.Stream;

                mdCheckBox.Checked = camera.MotionDetection;
                mdTrackBar.Value = (int)camera.Threshold;
                alertsCheckBox.Checked = camera.Alerts;
                rtTimePicker.Value = camera.RecordTime;
            }
        }

        private bool isDataValid()
        {
            // get all the camera data
            string name = nameTextBox.Text;
            string url = urlTextBox.Text;
            string port = portTextBox.Text;
            string stream = streamTextBox.Text;
            
            return !String.IsNullOrEmpty(name)                
                && !String.IsNullOrEmpty(stream)
                && Validator.isURL(url)
                && Validator.isPort(port);
        }

        private void saveData() 
        {
            // get all the camera data
            string name = nameTextBox.Text;
            string url = urlTextBox.Text;
            uint port = Convert.ToUInt32(portTextBox.Text);
            string stream = streamTextBox.Text;

            bool mdOn = mdCheckBox.Checked;
            int mdSensetivity = mdTrackBar.Value;
            bool alertsOn = alertsCheckBox.Checked;
            DateTime rtTime = rtTimePicker.Value;

            ConfigAccess ca = ConfigAccess.getInstance();

            // No camera here. Add one
            if (this.camera == null)
            {
                this.camera = new Camera(
                    name,
                    url,
                    port,
                    stream,
                    mdOn,
                    mdSensetivity,
                    alertsOn,
                    rtTime);

                ca.addCamera(camera);

                ((Form)this.TopLevelControl).Close();
            }
            // Already have a camera. Just update it.
            else
            {
                camera.Name = name;
                camera.URL = url;
                camera.Port = port;
                camera.Stream = stream;

                camera.MotionDetection = mdOn;
                camera.Threshold = mdSensetivity;
                camera.Alerts = alertsOn;
                camera.RecordTime = rtTime;

                ca.setCamera(camera);
            }
            
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (!isDataValid())
                MessageBox.Show("Not all fields are valid");
            else
                saveData();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete camera permanently?", "Delete camera", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ConfigAccess ca = ConfigAccess.getInstance();
                ca.deleteCamera(camera);
                parent.Controls.Remove(this);
                this.Dispose();
            }
        }

        
    }
}
