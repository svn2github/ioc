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
            this.tabWorkspace1 = new OpenNETCF.IoC.UI.TabWorkspace();
            this.headerWorkspace.SuspendLayout();
            this.SuspendLayout();
            // 
            // footerWorkspace
            // 
            this.footerWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.footerWorkspace.Location = new System.Drawing.Point(0, 266);
            this.footerWorkspace.Name = "footerWorkspace";
            this.footerWorkspace.Size = new System.Drawing.Size(240, 28);
            this.footerWorkspace.TabIndex = 2;
            this.footerWorkspace.Text = "deckWorkspace1";
            // 
            // bodyWorkspace
            // 
            this.bodyWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bodyWorkspace.Location = new System.Drawing.Point(0, 39);
            this.bodyWorkspace.Name = "bodyWorkspace";
            this.bodyWorkspace.Size = new System.Drawing.Size(240, 230);
            this.bodyWorkspace.TabIndex = 1;
            this.bodyWorkspace.Text = "tabWorkspace1";
            // 
            // headerWorkspace
            // 
            this.headerWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.headerWorkspace.Controls.Add(this.tabWorkspace1);
            this.headerWorkspace.Location = new System.Drawing.Point(0, 0);
            this.headerWorkspace.Name = "headerWorkspace";
            this.headerWorkspace.Size = new System.Drawing.Size(240, 40);
            this.headerWorkspace.TabIndex = 0;
            this.headerWorkspace.Text = "deckWorkspace1";
            // 
            // tabWorkspace1
            // 
            this.tabWorkspace1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabWorkspace1.Location = new System.Drawing.Point(0, 43);
            this.tabWorkspace1.Name = "tabWorkspace1";
            this.tabWorkspace1.Size = new System.Drawing.Size(240, 208);
            this.tabWorkspace1.TabIndex = 1;
            this.tabWorkspace1.Text = "tabWorkspace1";
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContainerForm";
            this.Text = "WiFi Survey";
            this.headerWorkspace.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenNETCF.IoC.UI.DeckWorkspace headerWorkspace;
        private OpenNETCF.IoC.UI.TabWorkspace bodyWorkspace;
        private OpenNETCF.IoC.UI.DeckWorkspace footerWorkspace;
        private OpenNETCF.IoC.UI.TabWorkspace tabWorkspace1;
    }
}

