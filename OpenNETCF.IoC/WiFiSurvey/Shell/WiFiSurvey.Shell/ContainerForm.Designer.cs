namespace WiFiSurvey.Shell
{
    partial class ContainerForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabWorkspace1 = new OpenNETCF.IoC.UI.TabWorkspace();
            this.deckWorkspace1 = new OpenNETCF.IoC.UI.DeckWorkspace();
            this.headerWorkspace = new OpenNETCF.IoC.UI.DeckWorkspace();
            this.deckWorkspace2 = new OpenNETCF.IoC.UI.DeckWorkspace();
            this.tabWorkspace1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabWorkspace1
            // 
            this.tabWorkspace1.Controls.Add(this.deckWorkspace1);
            this.tabWorkspace1.Location = new System.Drawing.Point(3, 49);
            this.tabWorkspace1.Name = "tabWorkspace1";
            this.tabWorkspace1.Size = new System.Drawing.Size(234, 178);
            this.tabWorkspace1.TabIndex = 1;
            this.tabWorkspace1.Text = "tabWorkspace1";
            // 
            // deckWorkspace1
            // 
            this.deckWorkspace1.Location = new System.Drawing.Point(0, 175);
            this.deckWorkspace1.Name = "deckWorkspace1";
            this.deckWorkspace1.Size = new System.Drawing.Size(234, 40);
            this.deckWorkspace1.TabIndex = 2;
            this.deckWorkspace1.Text = "deckWorkspace1";
            // 
            // headerWorkspace
            // 
            this.headerWorkspace.Location = new System.Drawing.Point(3, 3);
            this.headerWorkspace.Name = "headerWorkspace";
            this.headerWorkspace.Size = new System.Drawing.Size(234, 40);
            this.headerWorkspace.TabIndex = 0;
            this.headerWorkspace.Text = "deckWorkspace1";
            // 
            // deckWorkspace2
            // 
            this.deckWorkspace2.Location = new System.Drawing.Point(3, 233);
            this.deckWorkspace2.Name = "deckWorkspace2";
            this.deckWorkspace2.Size = new System.Drawing.Size(234, 40);
            this.deckWorkspace2.TabIndex = 2;
            this.deckWorkspace2.Text = "deckWorkspace1";
            // 
            // ContainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.deckWorkspace2);
            this.Controls.Add(this.tabWorkspace1);
            this.Controls.Add(this.headerWorkspace);
            this.KeyPreview = true;
            this.Name = "ContainerForm";
            this.Text = "Form1";
            this.tabWorkspace1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenNETCF.IoC.UI.DeckWorkspace headerWorkspace;
        private OpenNETCF.IoC.UI.TabWorkspace tabWorkspace1;
        private OpenNETCF.IoC.UI.DeckWorkspace deckWorkspace1;
        private OpenNETCF.IoC.UI.DeckWorkspace deckWorkspace2;
    }
}

