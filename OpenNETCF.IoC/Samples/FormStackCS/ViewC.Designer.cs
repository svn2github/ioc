namespace FormStackCS
{
    partial class ViewC
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
            this.pushB = new System.Windows.Forms.Button();
            this.pushA = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pushB
            // 
            this.pushB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pushB.Location = new System.Drawing.Point(97, 202);
            this.pushB.Name = "pushB";
            this.pushB.Size = new System.Drawing.Size(76, 31);
            this.pushB.TabIndex = 6;
            this.pushB.Text = "B >";
            this.pushB.Click += new System.EventHandler(this.pushB_Click);
            // 
            // pushA
            // 
            this.pushA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pushA.Location = new System.Drawing.Point(97, 166);
            this.pushA.Name = "pushA";
            this.pushA.Size = new System.Drawing.Size(76, 31);
            this.pushA.TabIndex = 7;
            this.pushA.Text = "A >";
            this.pushA.Click += new System.EventHandler(this.pushA_Click);
            // 
            // ViewC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pushA);
            this.Controls.Add(this.pushB);
            this.Name = "ViewC";
            this.Controls.SetChildIndex(this.pushB, 0);
            this.Controls.SetChildIndex(this.pushA, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pushB;
        private System.Windows.Forms.Button pushA;
    }
}
