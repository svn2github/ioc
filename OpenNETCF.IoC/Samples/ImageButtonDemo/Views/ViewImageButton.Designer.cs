using ImageButtonDemo;
namespace ImageButtonDemo
{
    partial class ViewImageButton
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
            this.pushGradient = new System.Windows.Forms.Button();
            this.pushMulti = new System.Windows.Forms.Button();
            this.imgButtonToggle = new ImageButtonDemo.ImageButton();
            this.SuspendLayout();
            // 
            // pushGradient
            // 
            this.pushGradient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pushGradient.Location = new System.Drawing.Point(161, 223);
            this.pushGradient.Name = "pushGradient";
            this.pushGradient.Size = new System.Drawing.Size(76, 31);
            this.pushGradient.TabIndex = 6;
            this.pushGradient.Text = "Gradient >";
            this.pushGradient.Click += new System.EventHandler(this.pushGradient_Click);
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
            // imgButtonLeft
            // 
            this.imgButtonToggle.Location = new System.Drawing.Point(79, 72);
            this.imgButtonToggle.Name = "imgButtonLeft";
            this.imgButtonToggle.Size = new System.Drawing.Size(94, 35);
            this.imgButtonToggle.TabIndex = 8;
            this.imgButtonToggle.Click += new System.EventHandler(this.imgButtonLeft_Click);
            // 
            // ViewImageButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.imgButtonToggle);
            this.Controls.Add(this.pushMulti);
            this.Controls.Add(this.pushGradient);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Name = "ViewImageButton";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pushGradient;
        private System.Windows.Forms.Button pushMulti;
        private ImageButton imgButtonToggle;
    }
}
