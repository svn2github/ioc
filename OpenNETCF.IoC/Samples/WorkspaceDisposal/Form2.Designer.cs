﻿namespace WorkspaceDisposal
{
    partial class Form2
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
            this.close = new System.Windows.Forms.Button();
            this.deckWorkspace1 = new OpenNETCF.IoC.UI.DeckWorkspace();
            this.hide = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(71, 188);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(61, 39);
            this.close.TabIndex = 1;
            this.close.Text = "Close";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // deckWorkspace1
            // 
            this.deckWorkspace1.Location = new System.Drawing.Point(3, 3);
            this.deckWorkspace1.Name = "deckWorkspace1";
            this.deckWorkspace1.Size = new System.Drawing.Size(200, 134);
            this.deckWorkspace1.TabIndex = 0;
            this.deckWorkspace1.Text = "deckWorkspace1";
            // 
            // hide
            // 
            this.hide.Location = new System.Drawing.Point(3, 143);
            this.hide.Name = "hide";
            this.hide.Size = new System.Drawing.Size(200, 39);
            this.hide.TabIndex = 2;
            this.hide.Text = "Hide";
            this.hide.Click += new System.EventHandler(this.hide_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.hide);
            this.Controls.Add(this.close);
            this.Controls.Add(this.deckWorkspace1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private OpenNETCF.IoC.UI.DeckWorkspace deckWorkspace1;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button hide;
    }
}