namespace WBSS
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.AddCamBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.serverOnCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.webPortTextBox = new System.Windows.Forms.TextBox();
            this.serverSaveBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.serverPortTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serverUrlTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.alertsOnCheckBox = new System.Windows.Forms.CheckBox();
            this.mdOnCheckBox = new System.Windows.Forms.CheckBox();
            this.camerasPanel = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.userSaveButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.userEmailTextBox = new System.Windows.Forms.TextBox();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddCamBtn
            // 
            this.AddCamBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AddCamBtn.Location = new System.Drawing.Point(15, 226);
            this.AddCamBtn.Name = "AddCamBtn";
            this.AddCamBtn.Size = new System.Drawing.Size(75, 33);
            this.AddCamBtn.TabIndex = 4;
            this.AddCamBtn.Text = "Add Camera";
            this.AddCamBtn.UseVisualStyleBackColor = true;
            this.AddCamBtn.Click += new System.EventHandler(this.AddCamBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.serverOnCheckBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.webPortTextBox);
            this.groupBox1.Controls.Add(this.serverSaveBtn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.serverPortTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.serverUrlTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox1.Size = new System.Drawing.Size(368, 170);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server";
            // 
            // serverOnCheckBox
            // 
            this.serverOnCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.serverOnCheckBox.Location = new System.Drawing.Point(23, 36);
            this.serverOnCheckBox.Name = "serverOnCheckBox";
            this.serverOnCheckBox.Size = new System.Drawing.Size(75, 75);
            this.serverOnCheckBox.TabIndex = 15;
            this.serverOnCheckBox.Text = "Servers Off";
            this.serverOnCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.serverOnCheckBox.UseVisualStyleBackColor = true;
            this.serverOnCheckBox.CheckedChanged += new System.EventHandler(this.serverOnCheckBox_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Web app port";
            // 
            // webPortTextBox
            // 
            this.webPortTextBox.Location = new System.Drawing.Point(200, 90);
            this.webPortTextBox.Name = "webPortTextBox";
            this.webPortTextBox.Size = new System.Drawing.Size(145, 20);
            this.webPortTextBox.TabIndex = 12;
            // 
            // serverSaveBtn
            // 
            this.serverSaveBtn.Location = new System.Drawing.Point(250, 124);
            this.serverSaveBtn.Name = "serverSaveBtn";
            this.serverSaveBtn.Size = new System.Drawing.Size(90, 23);
            this.serverSaveBtn.TabIndex = 11;
            this.serverSaveBtn.Text = "Save changes";
            this.serverSaveBtn.UseVisualStyleBackColor = true;
            this.serverSaveBtn.Click += new System.EventHandler(this.serverSaveBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Server port";
            // 
            // serverPortTextBox
            // 
            this.serverPortTextBox.Location = new System.Drawing.Point(200, 63);
            this.serverPortTextBox.Name = "serverPortTextBox";
            this.serverPortTextBox.Size = new System.Drawing.Size(145, 20);
            this.serverPortTextBox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Server URL";
            // 
            // serverUrlTextBox
            // 
            this.serverUrlTextBox.Location = new System.Drawing.Point(200, 36);
            this.serverUrlTextBox.Name = "serverUrlTextBox";
            this.serverUrlTextBox.Size = new System.Drawing.Size(145, 20);
            this.serverUrlTextBox.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.alertsOnCheckBox);
            this.groupBox2.Controls.Add(this.mdOnCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(386, 29);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox2.Size = new System.Drawing.Size(208, 170);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Global Motion Detection";
            // 
            // alertsOnCheckBox
            // 
            this.alertsOnCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.alertsOnCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.alertsOnCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.alertsOnCheckBox.Location = new System.Drawing.Point(110, 36);
            this.alertsOnCheckBox.Name = "alertsOnCheckBox";
            this.alertsOnCheckBox.Size = new System.Drawing.Size(75, 75);
            this.alertsOnCheckBox.TabIndex = 15;
            this.alertsOnCheckBox.Text = "Alerts On";
            this.alertsOnCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.alertsOnCheckBox.UseVisualStyleBackColor = true;
            this.alertsOnCheckBox.CheckedChanged += new System.EventHandler(this.alertsOnCheckBox_CheckedChanged);
            // 
            // mdOnCheckBox
            // 
            this.mdOnCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.mdOnCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.mdOnCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mdOnCheckBox.Location = new System.Drawing.Point(23, 36);
            this.mdOnCheckBox.Name = "mdOnCheckBox";
            this.mdOnCheckBox.Size = new System.Drawing.Size(75, 75);
            this.mdOnCheckBox.TabIndex = 13;
            this.mdOnCheckBox.Text = "Motion Detection On";
            this.mdOnCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mdOnCheckBox.UseVisualStyleBackColor = true;
            this.mdOnCheckBox.CheckedChanged += new System.EventHandler(this.mdOnCheckBox_CheckedChanged);
            // 
            // camerasPanel
            // 
            this.camerasPanel.AutoScroll = true;
            this.camerasPanel.BackColor = System.Drawing.SystemColors.Control;
            this.camerasPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.camerasPanel.Location = new System.Drawing.Point(3, 16);
            this.camerasPanel.Name = "camerasPanel";
            this.camerasPanel.Size = new System.Drawing.Size(880, 446);
            this.camerasPanel.TabIndex = 5;
            this.camerasPanel.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.camerasPanel_ControlAdded);
            this.camerasPanel.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.camerasPanel_ControlRemoved);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.camerasPanel);
            this.groupBox3.Location = new System.Drawing.Point(12, 265);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(886, 465);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cameras";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.userSaveButton);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.userEmailTextBox);
            this.groupBox4.Controls.Add(this.userNameTextBox);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(600, 29);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(20);
            this.groupBox4.Size = new System.Drawing.Size(300, 170);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Personal";
            // 
            // userSaveButton
            // 
            this.userSaveButton.Location = new System.Drawing.Point(187, 124);
            this.userSaveButton.Name = "userSaveButton";
            this.userSaveButton.Size = new System.Drawing.Size(90, 23);
            this.userSaveButton.TabIndex = 12;
            this.userSaveButton.Text = "Save changes";
            this.userSaveButton.UseVisualStyleBackColor = true;
            this.userSaveButton.Click += new System.EventHandler(this.userSaveButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Email";
            // 
            // userEmailTextBox
            // 
            this.userEmailTextBox.Location = new System.Drawing.Point(64, 64);
            this.userEmailTextBox.Name = "userEmailTextBox";
            this.userEmailTextBox.Size = new System.Drawing.Size(213, 20);
            this.userEmailTextBox.TabIndex = 2;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(64, 36);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(213, 20);
            this.userNameTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "Double click to restore";
            this.notifyIcon.BalloonTipTitle = "WBSS";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // timer
            // 
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(912, 742);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.AddCamBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control Panel";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddCamBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button serverSaveBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox serverPortTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox serverUrlTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel camerasPanel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button userSaveButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox userEmailTextBox;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox webPortTextBox;
        private System.Windows.Forms.CheckBox mdOnCheckBox;
        private System.Windows.Forms.CheckBox alertsOnCheckBox;
        private System.Windows.Forms.CheckBox serverOnCheckBox;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer timer;
    }
}

