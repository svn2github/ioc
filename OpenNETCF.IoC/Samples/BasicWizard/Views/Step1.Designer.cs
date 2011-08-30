namespace BasicWizard.Views
{
    partial class Step1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Step1));
            this.eula = new System.Windows.Forms.TextBox();
            this.no = new System.Windows.Forms.RadioButton();
            this.yes = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // eula
            // 
            this.eula.Location = new System.Drawing.Point(4, 27);
            this.eula.Multiline = true;
            this.eula.Name = "eula";
            this.eula.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.eula.Size = new System.Drawing.Size(227, 125);
            this.eula.TabIndex = 0;
            this.eula.Text = resources.GetString("eula.Text");
            // 
            // no
            // 
            this.no.Checked = true;
            this.no.Location = new System.Drawing.Point(4, 158);
            this.no.Name = "no";
            this.no.Size = new System.Drawing.Size(193, 20);
            this.no.TabIndex = 1;
            this.no.Text = "Umm, No Thanks";
            this.no.CheckedChanged += new System.EventHandler(this.no_CheckedChanged);
            // 
            // yes
            // 
            this.yes.Location = new System.Drawing.Point(4, 184);
            this.yes.Name = "yes";
            this.yes.Size = new System.Drawing.Size(193, 20);
            this.yes.TabIndex = 2;
            this.yes.TabStop = false;
            this.yes.Text = "Sure! Sounds Awesome!";
            this.yes.CheckedChanged += new System.EventHandler(this.yes_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "License";
            // 
            // Step1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.yes);
            this.Controls.Add(this.no);
            this.Controls.Add(this.eula);
            this.Name = "Step1";
            this.Size = new System.Drawing.Size(234, 218);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox eula;
        private System.Windows.Forms.RadioButton no;
        private System.Windows.Forms.RadioButton yes;
        private System.Windows.Forms.Label label1;
    }
}
