using ImageButtonDemo;
namespace ImageButtonDemo
{
    partial class ViewMulti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewMulti));
            this.pushCheckbox = new System.Windows.Forms.Button();
            this.pushGradient = new System.Windows.Forms.Button();
            this.imageButton1 = new ImageButton();
            this.imageButton2 = new ImageButton();
            this.imageButton3 = new ImageButton();
            this.SuspendLayout();
            // 
            // pushCheckbox
            // 
            this.pushCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pushCheckbox.Location = new System.Drawing.Point(152, 223);
            this.pushCheckbox.Name = "pushCheckbox";
            this.pushCheckbox.Size = new System.Drawing.Size(76, 31);
            this.pushCheckbox.TabIndex = 6;
            this.pushCheckbox.Text = "Chkbox >";
            this.pushCheckbox.Click += new System.EventHandler(this.pushCheckbox_Click);
            // 
            // pushGradient
            // 
            this.pushGradient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pushGradient.Location = new System.Drawing.Point(152, 260);
            this.pushGradient.Name = "pushGradient";
            this.pushGradient.Size = new System.Drawing.Size(76, 31);
            this.pushGradient.TabIndex = 7;
            this.pushGradient.Text = "Gradient >";
            this.pushGradient.Click += new System.EventHandler(this.pushGradient_Click);
            // 
            // imageButton1
            // 
            this.imageButton1.CenterText = "Centered Test Text";
            this.imageButton1.DownImage = ((System.Drawing.Image)(resources.GetObject("imageButton1.DownImage")));
            this.imageButton1.DownTextColor = System.Drawing.Color.White;
            this.imageButton1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.imageButton1.Location = new System.Drawing.Point(12, 68);
            this.imageButton1.Name = "imageButton1";
            this.imageButton1.Size = new System.Drawing.Size(216, 37);
            this.imageButton1.TabIndex = 2;
            this.imageButton1.UpImage = ((System.Drawing.Image)(resources.GetObject("imageButton1.UpImage")));
            this.imageButton1.UpTextColor = System.Drawing.Color.Empty;
            // 
            // imageButton2
            // 
            this.imageButton2.DownImage = ((System.Drawing.Image)(resources.GetObject("imageButton2.DownImage")));
            this.imageButton2.DownTextColor = System.Drawing.Color.White;
            this.imageButton2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.imageButton2.LeftText = "Left Test Text";
            this.imageButton2.Location = new System.Drawing.Point(12, 111);
            this.imageButton2.Name = "imageButton2";
            this.imageButton2.Size = new System.Drawing.Size(216, 37);
            this.imageButton2.TabIndex = 3;
            this.imageButton2.UpImage = ((System.Drawing.Image)(resources.GetObject("imageButton2.UpImage")));
            this.imageButton2.UpTextColor = System.Drawing.Color.Empty;
            // 
            // imageButton3
            // 
            this.imageButton3.DownImage = ((System.Drawing.Image)(resources.GetObject("imageButton3.DownImage")));
            this.imageButton3.DownTextColor = System.Drawing.Color.White;
            this.imageButton3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.imageButton3.Location = new System.Drawing.Point(12, 154);
            this.imageButton3.Name = "imageButton3";
            this.imageButton3.RightText = "Right Test Text";
            this.imageButton3.Size = new System.Drawing.Size(216, 37);
            this.imageButton3.TabIndex = 4;
            this.imageButton3.UpImage = ((System.Drawing.Image)(resources.GetObject("imageButton3.UpImage")));
            this.imageButton3.UpTextColor = System.Drawing.Color.Empty;
            // 
            // ViewMulti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.imageButton3);
            this.Controls.Add(this.imageButton2);
            this.Controls.Add(this.imageButton1);
            this.Controls.Add(this.pushGradient);
            this.Controls.Add(this.pushCheckbox);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Name = "ViewMulti";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pushCheckbox;
        private System.Windows.Forms.Button pushGradient;
        private ImageButton imageButton1;
        private ImageButton imageButton2;
        private ImageButton imageButton3;
    }
}
