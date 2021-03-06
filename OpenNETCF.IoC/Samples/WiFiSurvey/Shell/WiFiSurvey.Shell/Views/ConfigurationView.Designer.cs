﻿namespace WiFiSurvey.Shell.Views
{
    partial class ConfigurationView
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnResetEvents = new System.Windows.Forms.Button();
            this.lblEventCount = new System.Windows.Forms.Label();
            this.lblRefreshTime = new System.Windows.Forms.Label();
            this.lblDataRefresh = new System.Windows.Forms.Label();
            this.btnBroadcast = new System.Windows.Forms.Button();
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
            this.btnEnableDesktop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnableDesktop.Location = new System.Drawing.Point(88, 9);
            this.btnEnableDesktop.Name = "btnEnableDesktop";
            this.btnEnableDesktop.Size = new System.Drawing.Size(72, 20);
            this.btnEnableDesktop.TabIndex = 1;
            this.btnEnableDesktop.Text = "Enable";
            this.btnEnableDesktop.Click += new System.EventHandler(this.btnEnableDesktop_Click);
            // 
            // btnDisableDesktop
            // 
            this.btnDisableDesktop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.cmbRefreshRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRefreshRate.Items.Add("1");
            this.cmbRefreshRate.Items.Add("5");
            this.cmbRefreshRate.Items.Add("10");
            this.cmbRefreshRate.Items.Add("15");
            this.cmbRefreshRate.Items.Add("20");
            this.cmbRefreshRate.Items.Add("25");
            this.cmbRefreshRate.Items.Add("30");
            this.cmbRefreshRate.Location = new System.Drawing.Point(88, 43);
            this.cmbRefreshRate.Name = "cmbRefreshRate";
            this.cmbRefreshRate.Size = new System.Drawing.Size(74, 23);
            this.cmbRefreshRate.TabIndex = 9;
            // 
            // lblSeconds
            // 
            this.lblSeconds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSeconds.Location = new System.Drawing.Point(168, 46);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(54, 20);
            this.lblSeconds.Text = "Seconds";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.Text = "Events :";
            // 
            // btnResetEvents
            // 
            this.btnResetEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetEvents.Location = new System.Drawing.Point(160, 77);
            this.btnResetEvents.Name = "btnResetEvents";
            this.btnResetEvents.Size = new System.Drawing.Size(72, 20);
            this.btnResetEvents.TabIndex = 13;
            this.btnResetEvents.Text = "Reset";
            this.btnResetEvents.Click += new System.EventHandler(this.btnResetEvents_Click);
            // 
            // lblEventCount
            // 
            this.lblEventCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEventCount.Location = new System.Drawing.Point(88, 77);
            this.lblEventCount.Name = "lblEventCount";
            this.lblEventCount.Size = new System.Drawing.Size(48, 20);
            this.lblEventCount.Text = "label2";
            // 
            // lblRefreshTime
            // 
            this.lblRefreshTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRefreshTime.Location = new System.Drawing.Point(88, 99);
            this.lblRefreshTime.Name = "lblRefreshTime";
            this.lblRefreshTime.Size = new System.Drawing.Size(48, 20);
            this.lblRefreshTime.Text = "label2";
            // 
            // lblDataRefresh
            // 
            this.lblDataRefresh.Location = new System.Drawing.Point(2, 99);
            this.lblDataRefresh.Name = "lblDataRefresh";
            this.lblDataRefresh.Size = new System.Drawing.Size(94, 20);
            this.lblDataRefresh.Text = "Data Refresh";
            // 
            // btnBroadcast
            // 
            this.btnBroadcast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBroadcast.Location = new System.Drawing.Point(160, 100);
            this.btnBroadcast.Name = "btnBroadcast";
            this.btnBroadcast.Size = new System.Drawing.Size(72, 20);
            this.btnBroadcast.TabIndex = 18;
            this.btnBroadcast.Text = "Broadcast";
            this.btnBroadcast.Click += new System.EventHandler(this.btnBroadcast_Click);
            // 
            // ConfigurationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.btnBroadcast);
            this.Controls.Add(this.lblRefreshTime);
            this.Controls.Add(this.lblDataRefresh);
            this.Controls.Add(this.lblEventCount);
            this.Controls.Add(this.btnResetEvents);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSeconds);
            this.Controls.Add(this.cmbRefreshRate);
            this.Controls.Add(this.lblRefresh);
            this.Controls.Add(this.btnDisableDesktop);
            this.Controls.Add(this.btnEnableDesktop);
            this.Controls.Add(this.lblDesktopConnect);
            this.Name = "ConfigurationView";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResetEvents;
        private System.Windows.Forms.Label lblEventCount;
        private System.Windows.Forms.Label lblRefreshTime;
        private System.Windows.Forms.Label lblDataRefresh;
        private System.Windows.Forms.Button btnBroadcast;

    }
}
