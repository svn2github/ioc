namespace MVPSample
{
    partial class Menu
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
            this.label1 = new System.Windows.Forms.Label();
            this.loadConfig = new System.Windows.Forms.Button();
            this.showSettings = new System.Windows.Forms.Button();
            this.showDataEntry = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 21);
            this.label1.Text = "MENU";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // loadConfig
            // 
            this.loadConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.loadConfig.Location = new System.Drawing.Point(20, 192);
            this.loadConfig.Name = "loadConfig";
            this.loadConfig.Size = new System.Drawing.Size(139, 61);
            this.loadConfig.TabIndex = 5;
            this.loadConfig.Text = "Load Config";
            // 
            // showSettings
            // 
            this.showSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.showSettings.Location = new System.Drawing.Point(20, 125);
            this.showSettings.Name = "showSettings";
            this.showSettings.Size = new System.Drawing.Size(139, 61);
            this.showSettings.TabIndex = 4;
            this.showSettings.Text = "Settings";
            // 
            // showDataEntry
            // 
            this.showDataEntry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.showDataEntry.Location = new System.Drawing.Point(20, 58);
            this.showDataEntry.Name = "showDataEntry";
            this.showDataEntry.Size = new System.Drawing.Size(139, 61);
            this.showDataEntry.TabIndex = 3;
            this.showDataEntry.Text = "Data Entry";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.loadConfig);
            this.Controls.Add(this.showSettings);
            this.Controls.Add(this.showDataEntry);
            this.Controls.Add(this.label1);
            this.Name = "Menu";
            this.Size = new System.Drawing.Size(183, 288);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button loadConfig;
        private System.Windows.Forms.Button showSettings;
        private System.Windows.Forms.Button showDataEntry;
    }
}
