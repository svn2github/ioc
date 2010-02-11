namespace EventMarshalTest
{
    partial class Form1
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
            this.ui = new System.Windows.Forms.Button();
            this.background = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ui
            // 
            this.ui.Location = new System.Drawing.Point(14, 67);
            this.ui.Name = "ui";
            this.ui.Size = new System.Drawing.Size(148, 42);
            this.ui.TabIndex = 0;
            this.ui.Text = "UI Thread";
            this.ui.Click += new System.EventHandler(this.ui_Click);
            // 
            // background
            // 
            this.background.Location = new System.Drawing.Point(14, 116);
            this.background.Name = "background";
            this.background.Size = new System.Drawing.Size(148, 46);
            this.background.TabIndex = 1;
            this.background.Text = "Background Thread";
            this.background.Click += new System.EventHandler(this.background_Click);
            // 
            // result
            // 
            this.result.Location = new System.Drawing.Point(14, 28);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(100, 20);
            this.result.Text = "< ? >";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(638, 455);
            this.Controls.Add(this.result);
            this.Controls.Add(this.background);
            this.Controls.Add(this.ui);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ui;
        private System.Windows.Forms.Button background;
        private System.Windows.Forms.Label result;
    }
}

