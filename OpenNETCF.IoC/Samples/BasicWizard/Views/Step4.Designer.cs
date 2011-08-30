namespace BasicWizard.Views
{
    partial class Step4
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
            this.label2 = new System.Windows.Forms.Label();
            this.selectedOptions = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 20);
            this.label1.Text = "Confirmation";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 20);
            this.label2.Text = "You selected the following options:";
            // 
            // selectedOptions
            // 
            this.selectedOptions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.selectedOptions.Location = new System.Drawing.Point(3, 57);
            this.selectedOptions.Multiline = true;
            this.selectedOptions.Name = "selectedOptions";
            this.selectedOptions.Size = new System.Drawing.Size(228, 106);
            this.selectedOptions.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(227, 40);
            this.label3.Text = "Tap \'Finish\' to complete or \'Back\' to change options";
            // 
            // Step4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectedOptions);
            this.Controls.Add(this.label1);
            this.Name = "Step4";
            this.Size = new System.Drawing.Size(234, 218);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox selectedOptions;
        private System.Windows.Forms.Label label3;
    }
}
