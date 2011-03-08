namespace ImageButtonDemo
{
    partial class ViewGradient
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
            this.pushChkbox = new System.Windows.Forms.Button();
            this.pushMulti = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pushChkbox
            // 
            this.pushChkbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pushChkbox.Location = new System.Drawing.Point(161, 223);
            this.pushChkbox.Name = "pushChkbox";
            this.pushChkbox.Size = new System.Drawing.Size(76, 31);
            this.pushChkbox.TabIndex = 6;
            this.pushChkbox.Text = "Chkbox >";
            this.pushChkbox.Click += new System.EventHandler(this.pushCheckbox_Click);
            // 
            // pushMulti
            // 
            this.pushMulti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pushMulti.Location = new System.Drawing.Point(161, 260);
            this.pushMulti.Name = "pushMulti";
            this.pushMulti.Size = new System.Drawing.Size(76, 31);
            this.pushMulti.TabIndex = 7;
            this.pushMulti.Text = "Multi >";
            this.pushMulti.Click += new System.EventHandler(this.pushMulti_Click);
            // 
            // ViewGradient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.pushMulti);
            this.Controls.Add(this.pushChkbox);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ViewGradient";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pushChkbox;
        private System.Windows.Forms.Button pushMulti;
    }
}
