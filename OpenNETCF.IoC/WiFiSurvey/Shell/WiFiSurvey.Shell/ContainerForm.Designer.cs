namespace WiFiSurvey.Shell
{
    partial class ContainerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainWorkspace = new OpenNETCF.IoC.UI.DeckWorkspace();
            this.SuspendLayout();
            // 
            // mainWorkspace
            // 
            this.mainWorkspace.Location = new System.Drawing.Point(3, 42);
            this.mainWorkspace.Name = "mainWorkspace";
            this.mainWorkspace.Size = new System.Drawing.Size(234, 223);
            this.mainWorkspace.TabIndex = 0;
            this.mainWorkspace.Text = "deckWorkspace1";
            // 
            // ContainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.mainWorkspace);
            this.Menu = this.mainMenu1;
            this.Name = "ContainerForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private OpenNETCF.IoC.UI.DeckWorkspace mainWorkspace;
    }
}

