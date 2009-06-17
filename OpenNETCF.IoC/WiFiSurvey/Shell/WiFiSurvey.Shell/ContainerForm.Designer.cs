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
            this.footerWorkspace = new OpenNETCF.IoC.UI.DeckWorkspace();
            this.bodyWorkspace = new OpenNETCF.IoC.UI.TabWorkspace();
            this.headerWorkspace = new OpenNETCF.IoC.UI.DeckWorkspace();
            this.SuspendLayout();
            // 
            // footerWorkspace
            // 
            this.footerWorkspace.Location = new System.Drawing.Point(3, 233);
            this.footerWorkspace.Name = "footerWorkspace";
            this.footerWorkspace.Size = new System.Drawing.Size(234, 40);
            this.footerWorkspace.TabIndex = 2;
            this.footerWorkspace.Text = "deckWorkspace1";
            // 
            // bodyWorkspace
            // 
            this.bodyWorkspace.Location = new System.Drawing.Point(3, 49);
            this.bodyWorkspace.Name = "bodyWorkspace";
            this.bodyWorkspace.Size = new System.Drawing.Size(234, 178);
            this.bodyWorkspace.TabIndex = 1;
            this.bodyWorkspace.Text = "tabWorkspace1";
            // 
            // headerWorkspace
            // 
            this.headerWorkspace.Location = new System.Drawing.Point(3, 3);
            this.headerWorkspace.Name = "headerWorkspace";
            this.headerWorkspace.Size = new System.Drawing.Size(234, 40);
            this.headerWorkspace.TabIndex = 0;
            this.headerWorkspace.Text = "deckWorkspace1";
            // 
            // ContainerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.footerWorkspace);
            this.Controls.Add(this.bodyWorkspace);
            this.Controls.Add(this.headerWorkspace);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "ContainerForm";
            this.Text = "WiFi Survey";
            this.ResumeLayout(false);

        }

        #endregion

        private OpenNETCF.IoC.UI.DeckWorkspace headerWorkspace;
        private OpenNETCF.IoC.UI.TabWorkspace bodyWorkspace;
        private OpenNETCF.IoC.UI.DeckWorkspace footerWorkspace;
    }
}

