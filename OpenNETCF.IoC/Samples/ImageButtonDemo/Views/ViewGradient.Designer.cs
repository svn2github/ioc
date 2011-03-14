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
            this.imgButtonBig = new ImageButtonDemo.ImageButton();
            this.lblSample = new ImageButtonDemo.ImageButton();
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
            // imgButtonBig
            // 
            this.imgButtonBig.Location = new System.Drawing.Point(88, 76);
            this.imgButtonBig.Name = "imgButtonBig";
            this.imgButtonBig.Size = new System.Drawing.Size(64, 43);
            this.imgButtonBig.TabIndex = 8;
            this.imgButtonBig.Visible = false;
            // 
            // lblSample
            // 
            this.lblSample.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblSample.ForeColor = System.Drawing.SystemColors.Window;
            this.lblSample.Location = new System.Drawing.Point(32, 141);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(175, 30);
            this.lblSample.TabIndex = 9;
            this.lblSample.Visible = false;
            this.lblSample.Click += new System.EventHandler(this.lblSample_Click);
            // 
            // ViewGradient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.lblSample);
            this.Controls.Add(this.imgButtonBig);
            this.Controls.Add(this.pushMulti);
            this.Controls.Add(this.pushChkbox);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ViewGradient";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pushChkbox;
        private System.Windows.Forms.Button pushMulti;
        private ImageButton imgButtonBig;
        private ImageButton lblSample;
    }
}
