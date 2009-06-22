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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrentAPHeaderView));
            this.lblSSIDName = new System.Windows.Forms.Label();
            this.lblSignalStrength = new System.Windows.Forms.Label();
            this.lblMacAdress = new System.Windows.Forms.Label();
            this.desktopStatusPictureBox = new System.Windows.Forms.PictureBox();
            this.ildesktopStatus = new System.Windows.Forms.ImageList();
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
            // desktopStatusPictureBox
            // 
            this.desktopStatusPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.desktopStatusPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.desktopStatusPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("desktopStatusPictureBox.Image")));
            this.desktopStatusPictureBox.Location = new System.Drawing.Point(197, 3);
            this.desktopStatusPictureBox.Name = "desktopStatusPictureBox";
            this.desktopStatusPictureBox.Size = new System.Drawing.Size(34, 34);
            this.desktopStatusPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // ildesktopStatus
            // 
            this.ildesktopStatus.ImageSize = new System.Drawing.Size(32, 32);
            this.ildesktopStatus.Images.Clear();
            this.ildesktopStatus.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.ildesktopStatus.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            // 
            // CurrentAPHeaderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.desktopStatusPictureBox);
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
        private System.Windows.Forms.PictureBox desktopStatusPictureBox;
        private System.Windows.Forms.ImageList ildesktopStatus;

    }
}
