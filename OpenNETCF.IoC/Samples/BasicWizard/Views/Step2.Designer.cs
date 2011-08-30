namespace BasicWizard.Views
{
    partial class Step2
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
            this.label1 = new System.Windows.Forms.Label();
            this.express = new System.Windows.Forms.RadioButton();
            this.custom = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Setup Type";
            // 
            // express
            // 
            this.express.Checked = true;
            this.express.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.express.Location = new System.Drawing.Point(3, 47);
            this.express.Name = "express";
            this.express.Size = new System.Drawing.Size(190, 20);
            this.express.TabIndex = 2;
            this.express.Text = "Express (use defaults)";
            // 
            // custom
            // 
            this.custom.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.custom.Location = new System.Drawing.Point(3, 93);
            this.custom.Name = "custom";
            this.custom.Size = new System.Drawing.Size(190, 20);
            this.custom.TabIndex = 3;
            this.custom.TabStop = false;
            this.custom.Text = "Custom (show options)";
            // 
            // Step2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.custom);
            this.Controls.Add(this.express);
            this.Controls.Add(this.label1);
            this.Name = "Step2";
            this.Size = new System.Drawing.Size(234, 218);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton express;
        private System.Windows.Forms.RadioButton custom;
    }
}
