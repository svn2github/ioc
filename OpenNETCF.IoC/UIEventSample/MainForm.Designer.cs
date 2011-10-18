namespace UIEventSample
{
    partial class MainForm
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
            this.viewType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.workspaceType = new System.Windows.Forms.ComboBox();
            this.deckWorkspace = new OpenNETCF.IoC.UI.DeckWorkspace();
            this.tabWorkspace = new OpenNETCF.IoC.UI.TabWorkspace();
            this.label3 = new System.Windows.Forms.Label();
            this.activation = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // viewType
            // 
            this.viewType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewType.FormattingEnabled = true;
            this.viewType.Location = new System.Drawing.Point(128, 287);
            this.viewType.Name = "viewType";
            this.viewType.Size = new System.Drawing.Size(250, 28);
            this.viewType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "View Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Workspace:";
            // 
            // workspaceType
            // 
            this.workspaceType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workspaceType.FormattingEnabled = true;
            this.workspaceType.Location = new System.Drawing.Point(128, 253);
            this.workspaceType.Name = "workspaceType";
            this.workspaceType.Size = new System.Drawing.Size(250, 28);
            this.workspaceType.TabIndex = 3;
            // 
            // deckWorkspace
            // 
            this.deckWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deckWorkspace.Location = new System.Drawing.Point(13, 12);
            this.deckWorkspace.Name = "deckWorkspace";
            this.deckWorkspace.Size = new System.Drawing.Size(592, 235);
            this.deckWorkspace.TabIndex = 0;
            this.deckWorkspace.Text = "deckWorkspace1";
            this.deckWorkspace.Visible = false;
            // 
            // tabWorkspace
            // 
            this.tabWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabWorkspace.DrawMode = System.Windows.Forms.TabDrawMode.Normal;
            this.tabWorkspace.Location = new System.Drawing.Point(13, 12);
            this.tabWorkspace.Name = "tabWorkspace";
            this.tabWorkspace.SelectedIndex = -1;
            this.tabWorkspace.Size = new System.Drawing.Size(592, 235);
            this.tabWorkspace.SizeMode = System.Windows.Forms.TabSizeMode.Normal;
            this.tabWorkspace.TabIndex = 0;
            this.tabWorkspace.Text = "tabWorkspace1";
            this.tabWorkspace.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 324);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Activation";
            // 
            // activation
            // 
            this.activation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activation.FormattingEnabled = true;
            this.activation.Location = new System.Drawing.Point(128, 321);
            this.activation.Name = "activation";
            this.activation.Size = new System.Drawing.Size(250, 28);
            this.activation.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 586);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.activation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.workspaceType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.viewType);
            this.Controls.Add(this.deckWorkspace);
            this.Controls.Add(this.tabWorkspace);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenNETCF.IoC.UI.DeckWorkspace deckWorkspace;
        private System.Windows.Forms.ComboBox viewType;
        private System.Windows.Forms.Label label1;
        private OpenNETCF.IoC.UI.TabWorkspace tabWorkspace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox workspaceType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox activation;
    }
}

