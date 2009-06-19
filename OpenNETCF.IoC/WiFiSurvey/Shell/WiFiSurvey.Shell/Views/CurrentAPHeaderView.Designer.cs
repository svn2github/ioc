namespace WiFiSurvey.Shell.Views
{
    partial class CurrentAPHeaderView
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
            this.lblSSIDName = new System.Windows.Forms.Label();
            this.lblSignalStrength = new System.Windows.Forms.Label();
            this.lblMacAdress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSSIDName
            // 
            this.lblSSIDName.Location = new System.Drawing.Point(4, 4);
            this.lblSSIDName.Name = "lblSSIDName";
            this.lblSSIDName.Size = new System.Drawing.Size(81, 20);
            this.lblSSIDName.Text = "Locating AP";
            // 
            // lblSignalStrength
            // 
            this.lblSignalStrength.Location = new System.Drawing.Point(91, 4);
            this.lblSignalStrength.Name = "lblSignalStrength";
            this.lblSignalStrength.Size = new System.Drawing.Size(100, 20);
            this.lblSignalStrength.Text = "-";
            // 
            // lblMacAdress
            // 
            this.lblMacAdress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Italic);
            this.lblMacAdress.Location = new System.Drawing.Point(4, 20);
            this.lblMacAdress.Name = "lblMacAdress";
            this.lblMacAdress.Size = new System.Drawing.Size(100, 20);
            this.lblMacAdress.Text = "-";
            // 
            // CurrentAPHeaderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.lblMacAdress);
            this.Controls.Add(this.lblSignalStrength);
            this.Controls.Add(this.lblSSIDName);
            this.Name = "CurrentAPHeaderView";
            this.Size = new System.Drawing.Size(234, 40);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSSIDName;
        private System.Windows.Forms.Label lblSignalStrength;
        private System.Windows.Forms.Label lblMacAdress;

    }
}
