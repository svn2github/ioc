namespace IoCSample
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.showDataEntry = new System.Windows.Forms.Button();
            this.showSettings = new System.Windows.Forms.Button();
            this.loadConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // showDataEntry
            // 
            this.showDataEntry.Location = new System.Drawing.Point(42, 20);
            this.showDataEntry.Name = "showDataEntry";
            this.showDataEntry.Size = new System.Drawing.Size(139, 61);
            this.showDataEntry.TabIndex = 0;
            this.showDataEntry.Text = "Data Entry";
            this.showDataEntry.Click += new System.EventHandler(this.showDataEntry_Click);
            // 
            // showSettings
            // 
            this.showSettings.Location = new System.Drawing.Point(42, 87);
            this.showSettings.Name = "showSettings";
            this.showSettings.Size = new System.Drawing.Size(139, 61);
            this.showSettings.TabIndex = 1;
            this.showSettings.Text = "Settings";
            this.showSettings.Click += new System.EventHandler(this.showSettings_Click);
            // 
            // loadConfig
            // 
            this.loadConfig.Location = new System.Drawing.Point(42, 154);
            this.loadConfig.Name = "loadConfig";
            this.loadConfig.Size = new System.Drawing.Size(139, 61);
            this.loadConfig.TabIndex = 2;
            this.loadConfig.Text = "Load Config";
            this.loadConfig.Click += new System.EventHandler(this.loadConfig_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.loadConfig);
            this.Controls.Add(this.showSettings);
            this.Controls.Add(this.showDataEntry);
            this.Menu = this.mainMenu1;
            this.Name = "MenuForm";
            this.Text = "MenuForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button showDataEntry;
        private System.Windows.Forms.Button showSettings;
        private System.Windows.Forms.Button loadConfig;
    }
}