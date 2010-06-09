namespace MenuIntegration
{
    partial class Container
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu;

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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.file = new System.Windows.Forms.MenuItem();
            this.workspace = new OpenNETCF.IoC.UI.DeckWorkspace();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.file);
            // 
            // file
            // 
            this.file.Text = "File";
            // 
            // workspace
            // 
            this.workspace.Location = new System.Drawing.Point(4, 4);
            this.workspace.Name = "workspace";
            this.workspace.Size = new System.Drawing.Size(233, 261);
            this.workspace.TabIndex = 0;
            // 
            // Container
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.workspace);
            this.Menu = this.mainMenu;
            this.Name = "Container";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem file;
        private OpenNETCF.IoC.UI.DeckWorkspace workspace;
    }
}

