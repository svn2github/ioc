namespace FormStackCS
{
    partial class ViewBase
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
            this.viewName = new System.Windows.Forms.Label();
            this.back = new System.Windows.Forms.Button();
            this.stackList = new System.Windows.Forms.ListBox();
            this.forward = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // viewName
            // 
            this.viewName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.viewName.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.viewName.Location = new System.Drawing.Point(3, 0);
            this.viewName.Name = "viewName";
            this.viewName.Size = new System.Drawing.Size(170, 31);
            this.viewName.Text = "{view name}";
            this.viewName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // back
            // 
            this.back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.back.Location = new System.Drawing.Point(3, 202);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(76, 31);
            this.back.TabIndex = 2;
            this.back.Text = "< Back";
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // stackList
            // 
            this.stackList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.stackList.Location = new System.Drawing.Point(3, 46);
            this.stackList.Name = "stackList";
            this.stackList.Size = new System.Drawing.Size(170, 114);
            this.stackList.TabIndex = 4;
            // 
            // forward
            // 
            this.forward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.forward.Location = new System.Drawing.Point(3, 166);
            this.forward.Name = "forward";
            this.forward.Size = new System.Drawing.Size(76, 31);
            this.forward.TabIndex = 6;
            this.forward.Text = "Fwd >";
            this.forward.Click += new System.EventHandler(this.forward_Click);
            // 
            // ViewBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.forward);
            this.Controls.Add(this.stackList);
            this.Controls.Add(this.back);
            this.Controls.Add(this.viewName);
            this.Name = "ViewBase";
            this.Size = new System.Drawing.Size(176, 236);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label viewName;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.ListBox stackList;
        private System.Windows.Forms.Button forward;
    }
}
