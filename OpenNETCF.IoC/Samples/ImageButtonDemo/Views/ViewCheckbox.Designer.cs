using ImageButtonDemo;
namespace ImageButtonDemo
{
    partial class ViewCheckbox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewCheckbox));
            this.pushImg = new System.Windows.Forms.Button();
            this.pushGradient = new System.Windows.Forms.Button();
            this.chkOne = new ImageButton();
            this.chkTwo = new ImageButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pushImg
            // 
            this.pushImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pushImg.Location = new System.Drawing.Point(161, 223);
            this.pushImg.Name = "pushImg";
            this.pushImg.Size = new System.Drawing.Size(76, 31);
            this.pushImg.TabIndex = 6;
            this.pushImg.Text = "ImgBut >";
            this.pushImg.Click += new System.EventHandler(this.pushImgButton_Click);
            // 
            // pushGradient
            // 
            this.pushGradient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pushGradient.Location = new System.Drawing.Point(161, 260);
            this.pushGradient.Name = "pushGradient";
            this.pushGradient.Size = new System.Drawing.Size(76, 31);
            this.pushGradient.TabIndex = 7;
            this.pushGradient.Text = "Gradient >";
            this.pushGradient.Click += new System.EventHandler(this.pushGradient_Click);
            // 
            // chkOne
            // 
            this.chkOne.DownImage = ((System.Drawing.Image)(resources.GetObject("chkOne.DownImage")));
            this.chkOne.Location = new System.Drawing.Point(18, 71);
            this.chkOne.Name = "chkOne";
            this.chkOne.Size = new System.Drawing.Size(18, 18);
            this.chkOne.TabIndex = 8;
            this.chkOne.UpImage = ((System.Drawing.Image)(resources.GetObject("chkOne.UpImage")));
            this.chkOne.Click += new System.EventHandler(this.chkOne_Click);
            // 
            // chkTwo
            // 
            this.chkTwo.DownImage = ((System.Drawing.Image)(resources.GetObject("chkTwo.DownImage")));
            this.chkTwo.Location = new System.Drawing.Point(18, 107);
            this.chkTwo.Name = "chkTwo";
            this.chkTwo.Size = new System.Drawing.Size(18, 18);
            this.chkTwo.TabIndex = 9;
            this.chkTwo.UpImage = ((System.Drawing.Image)(resources.GetObject("chkTwo.UpImage")));
            this.chkTwo.Click += new System.EventHandler(this.chkTwo_Click);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(38, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.Text = "Checkbox 1:";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(38, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 20);
            this.label2.Text = "Checkbox 2:";
            // 
            // ViewCheckbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkTwo);
            this.Controls.Add(this.chkOne);
            this.Controls.Add(this.pushGradient);
            this.Controls.Add(this.pushImg);
            this.Name = "ViewCheckbox";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pushImg;
        private System.Windows.Forms.Button pushGradient;
        private ImageButton chkOne;
        private ImageButton chkTwo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
