namespace WiFiSurvey.Shell.Components
{
    partial class CurrentAP
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
            this.lblCurrentAPName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCurrentAPName
            // 
            this.lblCurrentAPName.Location = new System.Drawing.Point(3, 11);
            this.lblCurrentAPName.Name = "lblCurrentAPName";
            this.lblCurrentAPName.Size = new System.Drawing.Size(87, 23);
            this.lblCurrentAPName.Text = "Curent AP";
            // 
            // CurrentAP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.lblCurrentAPName);
            this.Name = "CurrentAP";
            this.Size = new System.Drawing.Size(255, 55);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentAPName;
    }
}
