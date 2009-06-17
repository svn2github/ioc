namespace WiFiSurvey.Shell.Views
{
    partial class HistoryView
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
            this.historyListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // historyListView
            // 
            this.historyListView.Columns.Add(this.columnHeader1);
            this.historyListView.Columns.Add(this.columnHeader2);
            this.historyListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyListView.Location = new System.Drawing.Point(0, 0);
            this.historyListView.Name = "historyListView";
            this.historyListView.Size = new System.Drawing.Size(234, 155);
            this.historyListView.TabIndex = 0;
            this.historyListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Event";
            this.columnHeader2.Width = 161;
            // 
            // HistoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.historyListView);
            this.Name = "HistoryView";
            this.Size = new System.Drawing.Size(234, 155);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView historyListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
