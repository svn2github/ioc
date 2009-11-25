namespace FormStackCS
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
            this.workspace = new OpenNETCF.IoC.UI.DeckWorkspace();
            this.SuspendLayout();
            // 
            // workspace
            // 
            this.workspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspace.Location = new System.Drawing.Point(0, 0);
            this.workspace.Name = "workspace";
            this.workspace.Size = new System.Drawing.Size(240, 268);
            this.workspace.TabIndex = 0;
            this.workspace.Text = "deckWorkspace1";
            // 
            // ContainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.workspace);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "ContainerForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private OpenNETCF.IoC.UI.DeckWorkspace workspace;
    }
}

