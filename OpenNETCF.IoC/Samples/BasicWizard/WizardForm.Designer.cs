namespace BasicWizard
{
    partial class WizardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;
        private OpenNETCF.IoC.UI.DeckWorkspace wizardWorkspace;
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.wizardWorkspace = new OpenNETCF.IoC.UI.DeckWorkspace();
            this.next = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.restart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // wizardWorkspace
            // 
            this.wizardWorkspace.Location = new System.Drawing.Point(3, 3);
            this.wizardWorkspace.Name = "wizardWorkspace";
            this.wizardWorkspace.Size = new System.Drawing.Size(234, 218);
            this.wizardWorkspace.TabIndex = 0;
            // 
            // next
            // 
            this.next.Location = new System.Drawing.Point(183, 227);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(54, 38);
            this.next.TabIndex = 1;
            this.next.Text = "Next";
            // 
            // back
            // 
            this.back.Location = new System.Drawing.Point(3, 227);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(54, 38);
            this.back.TabIndex = 2;
            this.back.Text = "Back";
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(123, 227);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(54, 38);
            this.exit.TabIndex = 3;
            this.exit.Text = "Exit";
            // 
            // restart
            // 
            this.restart.Location = new System.Drawing.Point(66, 227);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(54, 38);
            this.restart.TabIndex = 4;
            this.restart.Text = "Restart";
            // 
            // WizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.back);
            this.Controls.Add(this.restart);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.next);
            this.Controls.Add(this.wizardWorkspace);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "WizardForm";
            this.Text = "Sample Wizard";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button next;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button restart;
    }
}

