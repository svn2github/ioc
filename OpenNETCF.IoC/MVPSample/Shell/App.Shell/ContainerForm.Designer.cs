namespace App.Shell
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
            this.deckList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // workspace
            // 
            this.workspace.Location = new System.Drawing.Point(3, 87);
            this.workspace.Name = "workspace";
            this.workspace.Size = new System.Drawing.Size(234, 178);
            this.workspace.TabIndex = 0;
            this.workspace.Text = "workspace";
            // 
            // deckList
            // 
            this.deckList.Location = new System.Drawing.Point(3, 23);
            this.deckList.Name = "deckList";
            this.deckList.Size = new System.Drawing.Size(233, 58);
            this.deckList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 20);
            this.label1.Text = "Current Deck";
            // 
            // ContainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deckList);
            this.Controls.Add(this.workspace);
            this.Menu = this.mainMenu1;
            this.Name = "ContainerForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private OpenNETCF.IoC.UI.DeckWorkspace workspace;
        private System.Windows.Forms.ListBox deckList;
        private System.Windows.Forms.Label label1;
    }
}

