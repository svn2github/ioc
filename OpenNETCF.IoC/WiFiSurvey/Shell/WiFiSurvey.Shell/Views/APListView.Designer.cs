namespace WiFiSurvey.Shell.Views
{
    partial class APListView
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
            this.apList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // apList
            // 
            this.apList.Columns.Add(this.columnHeader1);
            this.apList.Columns.Add(this.columnHeader3);
            this.apList.Columns.Add(this.columnHeader2);
            this.apList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apList.Location = new System.Drawing.Point(0, 0);
            this.apList.Name = "apList";
            this.apList.Size = new System.Drawing.Size(259, 172);
            this.apList.TabIndex = 0;
            this.apList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "SSID";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Signal";
            this.columnHeader3.Width = 75;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "MAC Addr.";
            this.columnHeader2.Width = 93;
            // 
            // APListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.apList);
            this.Name = "APListView";
            this.Size = new System.Drawing.Size(259, 172);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView apList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;





    }
}
