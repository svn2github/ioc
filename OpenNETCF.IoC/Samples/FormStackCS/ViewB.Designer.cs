namespace FormStackCS
{
    partial class ViewB
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
            this.pushA = new System.Windows.Forms.Button();
            this.pushC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pushA
            // 
            this.pushA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pushA.Location = new System.Drawing.Point(96, 166);
            this.pushA.Name = "pushA";
            this.pushA.Size = new System.Drawing.Size(76, 31);
            this.pushA.TabIndex = 6;
            this.pushA.Text = "A >";
            this.pushA.Click += new System.EventHandler(this.pushA_Click);
            // 
            // pushC
            // 
            this.pushC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pushC.Location = new System.Drawing.Point(96, 202);
            this.pushC.Name = "pushC";
            this.pushC.Size = new System.Drawing.Size(76, 31);
            this.pushC.TabIndex = 7;
            this.pushC.Text = "C >";
            this.pushC.Click += new System.EventHandler(this.pushC_Click);
            // 
            // ViewB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.pushC);
            this.Controls.Add(this.pushA);
            this.Name = "ViewB";
            this.Controls.SetChildIndex(this.pushA, 0);
            this.Controls.SetChildIndex(this.pushC, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pushA;
        private System.Windows.Forms.Button pushC;
    }
}
