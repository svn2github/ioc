namespace IoCSample
{
    partial class SettingsForm
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
            this.backToMenu = new System.Windows.Forms.Button();
            this.raiseEvent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // backToMenu
            // 
            this.backToMenu.Location = new System.Drawing.Point(68, 26);
            this.backToMenu.Name = "backToMenu";
            this.backToMenu.Size = new System.Drawing.Size(104, 68);
            this.backToMenu.TabIndex = 1;
            this.backToMenu.Text = "Back to Menu";
            this.backToMenu.Click += new System.EventHandler(this.backToMenu_Click);
            // 
            // raiseEvent
            // 
            this.raiseEvent.Location = new System.Drawing.Point(68, 100);
            this.raiseEvent.Name = "raiseEvent";
            this.raiseEvent.Size = new System.Drawing.Size(104, 68);
            this.raiseEvent.TabIndex = 2;
            this.raiseEvent.Text = "Raise Event";
            this.raiseEvent.Click += new System.EventHandler(this.raiseEvent_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.raiseEvent);
            this.Controls.Add(this.backToMenu);
            this.Menu = this.mainMenu1;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button backToMenu;
        private System.Windows.Forms.Button raiseEvent;
    }
}