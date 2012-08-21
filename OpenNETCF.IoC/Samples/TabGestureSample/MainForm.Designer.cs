namespace TabGestureSample
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
            this.mainWorkspace = new OpenNETCF.IoC.UI.TabWorkspace();
            this.SuspendLayout();
            // 
            // mainWorkspace
            // 
            this.mainWorkspace.DrawMode = System.Windows.Forms.TabDrawMode.Normal;
            this.mainWorkspace.GesturesEnabled = false;
            this.mainWorkspace.GestureThreshold = 20;
            this.mainWorkspace.Location = new System.Drawing.Point(12, 12);
            this.mainWorkspace.Name = "mainWorkspace";
            this.mainWorkspace.SelectedIndex = -1;
            this.mainWorkspace.Size = new System.Drawing.Size(436, 405);
            this.mainWorkspace.SizeMode = System.Windows.Forms.TabSizeMode.Normal;
            this.mainWorkspace.TabIndex = 0;
            this.mainWorkspace.Text = "mainWorkspace";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 429);
            this.Controls.Add(this.mainWorkspace);
            this.Name = "MainForm";
            this.Text = "My IoC Application";
            this.ResumeLayout(false);

        }

        #endregion

        private OpenNETCF.IoC.UI.TabWorkspace mainWorkspace;

    }
}

