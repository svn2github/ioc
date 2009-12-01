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
            this.debugList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.debugPanel = new System.Windows.Forms.Panel();
            this.clear = new System.Windows.Forms.Button();
            this.enable = new System.Windows.Forms.CheckBox();
            this.debugPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // debugList
            // 
            this.debugList.Columns.Add(this.columnHeader1);
            this.debugList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugList.Location = new System.Drawing.Point(0, 0);
            this.debugList.Name = "debugList";
            this.debugList.Size = new System.Drawing.Size(312, 212);
            this.debugList.TabIndex = 0;
            this.debugList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Debug";
            this.columnHeader1.Width = 60;
            // 
            // debugPanel
            // 
            this.debugPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.debugPanel.Controls.Add(this.debugList);
            this.debugPanel.Location = new System.Drawing.Point(0, 20);
            this.debugPanel.Name = "debugPanel";
            this.debugPanel.Size = new System.Drawing.Size(312, 212);
            // 
            // clear
            // 
            this.clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clear.Location = new System.Drawing.Point(237, 0);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(72, 20);
            this.clear.TabIndex = 2;
            this.clear.Text = "Clear";
            // 
            // enable
            // 
            this.enable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enable.Location = new System.Drawing.Point(152, 0);
            this.enable.Name = "enable";
            this.enable.Size = new System.Drawing.Size(79, 20);
            this.enable.TabIndex = 3;
            this.enable.Text = "Enable";
            // 
            // DebugView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.enable);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.debugPanel);
            this.Name = "DebugView";
            this.Size = new System.Drawing.Size(312, 232);
            this.debugPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView debugList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel debugPanel;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.CheckBox enable;

    }
}
