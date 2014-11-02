namespace WBSS
{
    partial class AddCamera
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
            this.cs1 = new WBSS.CS();
            this.SuspendLayout();
            // 
            // cs1
            // 
            this.cs1.BackColor = System.Drawing.SystemColors.Menu;
            this.cs1.Location = new System.Drawing.Point(12, 12);
            this.cs1.Name = "cs1";
            this.cs1.Padding = new System.Windows.Forms.Padding(20);
            this.cs1.Size = new System.Drawing.Size(880, 207);
            this.cs1.TabIndex = 0;
            // 
            // AddCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 214);
            this.Controls.Add(this.cs1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddCamera";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New Camera";
            this.Load += new System.EventHandler(this.AddCamera_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CS cs1;

    }
}