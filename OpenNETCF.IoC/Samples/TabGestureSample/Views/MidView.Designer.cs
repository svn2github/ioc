namespace TabGestureSample.Views
{
    partial class MidView
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
            this.title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.Location = new System.Drawing.Point(4, 27);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(233, 23);
            this.title.TabIndex = 0;
            this.title.Text = "B";
            this.title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MidView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.title);
            this.Name = "MidView";
            this.Size = new System.Drawing.Size(240, 240);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label title;
    }
}
