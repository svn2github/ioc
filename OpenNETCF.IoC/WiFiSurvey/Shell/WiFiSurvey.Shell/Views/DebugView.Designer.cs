namespace WiFiSurvey.Shell
{
    partial class DebugView
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
            this.debugBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // debugBox
            // 
            this.debugBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugBox.Location = new System.Drawing.Point(0, 0);
            this.debugBox.Name = "debugBox";
            this.debugBox.Size = new System.Drawing.Size(150, 146);
            this.debugBox.TabIndex = 0;
            // 
            // DebugView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.debugBox);
            this.Name = "DebugView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox debugBox;
    }
}
