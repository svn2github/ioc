namespace BasicWizard.Views
{
    partial class Step3
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
            this.optionA = new System.Windows.Forms.CheckBox();
            this.optionB = new System.Windows.Forms.CheckBox();
            this.optionD = new System.Windows.Forms.CheckBox();
            this.optionC = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 20);
            this.label1.Text = "Setup Options";
            // 
            // optionA
            // 
            this.optionA.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.optionA.Location = new System.Drawing.Point(3, 44);
            this.optionA.Name = "optionA";
            this.optionA.Size = new System.Drawing.Size(140, 20);
            this.optionA.TabIndex = 2;
            this.optionA.Text = "Option A";
            // 
            // optionB
            // 
            this.optionB.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.optionB.Location = new System.Drawing.Point(3, 80);
            this.optionB.Name = "optionB";
            this.optionB.Size = new System.Drawing.Size(140, 20);
            this.optionB.TabIndex = 3;
            this.optionB.Text = "Option B";
            // 
            // optionD
            // 
            this.optionD.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.optionD.Location = new System.Drawing.Point(3, 151);
            this.optionD.Name = "optionD";
            this.optionD.Size = new System.Drawing.Size(140, 20);
            this.optionD.TabIndex = 5;
            this.optionD.Text = "Option D";
            // 
            // optionC
            // 
            this.optionC.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.optionC.Location = new System.Drawing.Point(3, 115);
            this.optionC.Name = "optionC";
            this.optionC.Size = new System.Drawing.Size(140, 20);
            this.optionC.TabIndex = 4;
            this.optionC.Text = "Option C";
            // 
            // Step3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.optionD);
            this.Controls.Add(this.optionC);
            this.Controls.Add(this.optionB);
            this.Controls.Add(this.optionA);
            this.Controls.Add(this.label1);
            this.Name = "Step3";
            this.Size = new System.Drawing.Size(234, 218);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox optionA;
        private System.Windows.Forms.CheckBox optionB;
        private System.Windows.Forms.CheckBox optionD;
        private System.Windows.Forms.CheckBox optionC;
    }
}
