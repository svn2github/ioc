namespace IoCSample
{
    partial class DataEntryForm
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
            this.changeCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backToMenu
            // 
            this.backToMenu.Location = new System.Drawing.Point(63, 58);
            this.backToMenu.Name = "backToMenu";
            this.backToMenu.Size = new System.Drawing.Size(104, 68);
            this.backToMenu.TabIndex = 0;
            this.backToMenu.Text = "Back to Menu";
            this.backToMenu.Click += new System.EventHandler(this.backToMenu_Click);
            // 
            // changeCount
            // 
            this.changeCount.Location = new System.Drawing.Point(4, 166);
            this.changeCount.Name = "changeCount";
            this.changeCount.Size = new System.Drawing.Size(220, 20);
            this.changeCount.Text = "Settings have Changed 0 times";
            // 
            // DataEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.changeCount);
            this.Controls.Add(this.backToMenu);
            this.Menu = this.mainMenu1;
            this.Name = "DataEntryForm";
            this.Text = "DataEntryForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button backToMenu;
        private System.Windows.Forms.Label changeCount;
    }
}