namespace WiFiSurvey.Shell.Views
{
    partial class ToolsView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDesktopConnect = new System.Windows.Forms.Label();
            this.btnEnableDesktop = new System.Windows.Forms.Button();
            this.btnDisableDesktop = new System.Windows.Forms.Button();
            this.lblRefresh = new System.Windows.Forms.Label();
            this.cmbRefreshRate = new System.Windows.Forms.ComboBox();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDesktopConnect
            // 
            this.lblDesktopConnect.Location = new System.Drawing.Point(3, 4);
            this.lblDesktopConnect.Name = "lblDesktopConnect";
            this.lblDesktopConnect.Size = new System.Drawing.Size(77, 29);
            this.lblDesktopConnect.Text = "Desktop Connection";
            // 
            // btnEnableDesktop
            // 
            this.btnEnableDesktop.Location = new System.Drawing.Point(88, 9);
            this.btnEnableDesktop.Name = "btnEnableDesktop";
            this.btnEnableDesktop.Size = new System.Drawing.Size(72, 20);
            this.btnEnableDesktop.TabIndex = 1;
            this.btnEnableDesktop.Text = "Enable";
            this.btnEnableDesktop.Click += new System.EventHandler(this.btnEnableDesktop_Click);
            // 
            // btnDisableDesktop
            // 
            this.btnDisableDesktop.Location = new System.Drawing.Point(160, 9);
            this.btnDisableDesktop.Name = "btnDisableDesktop";
            this.btnDisableDesktop.Size = new System.Drawing.Size(72, 20);
            this.btnDisableDesktop.TabIndex = 2;
            this.btnDisableDesktop.Text = "Disable";
            this.btnDisableDesktop.Click += new System.EventHandler(this.btnDisableDesktop_Click);
            // 
            // lblRefresh
            // 
            this.lblRefresh.Location = new System.Drawing.Point(3, 38);
            this.lblRefresh.Name = "lblRefresh";
            this.lblRefresh.Size = new System.Drawing.Size(77, 35);
            this.lblRefresh.Text = "Connection Refresh";
            // 
            // cmbRefreshRate
            // 
            this.cmbRefreshRate.Items.Add("1");
            this.cmbRefreshRate.Items.Add("5");
            this.cmbRefreshRate.Items.Add("10");
            this.cmbRefreshRate.Items.Add("15");
            this.cmbRefreshRate.Items.Add("20");
            this.cmbRefreshRate.Items.Add("25");
            this.cmbRefreshRate.Items.Add("30");
            this.cmbRefreshRate.Location = new System.Drawing.Point(86, 40);
            this.cmbRefreshRate.Name = "cmbRefreshRate";
            this.cmbRefreshRate.Size = new System.Drawing.Size(74, 22);
            this.cmbRefreshRate.TabIndex = 9;
            // 
            // lblSeconds
            // 
            this.lblSeconds.Location = new System.Drawing.Point(170, 43);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(54, 20);
            this.lblSeconds.Text = "Seconds";
            // 
            // ToolsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.lblSeconds);
            this.Controls.Add(this.cmbRefreshRate);
            this.Controls.Add(this.lblRefresh);
            this.Controls.Add(this.btnDisableDesktop);
            this.Controls.Add(this.btnEnableDesktop);
            this.Controls.Add(this.lblDesktopConnect);
            this.Name = "ToolsView";
            this.Size = new System.Drawing.Size(234, 155);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDesktopConnect;
        private System.Windows.Forms.Button btnEnableDesktop;
        private System.Windows.Forms.Button btnDisableDesktop;
        private System.Windows.Forms.Label lblRefresh;
        private System.Windows.Forms.ComboBox cmbRefreshRate;
        private System.Windows.Forms.Label lblSeconds;

    }
}
