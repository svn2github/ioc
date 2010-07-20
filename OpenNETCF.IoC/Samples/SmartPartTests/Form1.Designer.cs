namespace SmartPartTests
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
            this.workspace = new OpenNETCF.IoC.UI.TabWorkspace();
            this.action = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // workspace
            // 
            this.workspace.Location = new System.Drawing.Point(4, 4);
            this.workspace.Name = "workspace";
            this.workspace.SelectedIndex = -1;
            this.workspace.Size = new System.Drawing.Size(233, 223);
            this.workspace.TabIndex = 0;
            this.workspace.Text = "tabWorkspace1";
            // 
            // action
            // 
            this.action.Location = new System.Drawing.Point(134, 233);
            this.action.Name = "action";
            this.action.Size = new System.Drawing.Size(103, 31);
            this.action.TabIndex = 1;
            this.action.Text = "Step 1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.action);
            this.Controls.Add(this.workspace);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private OpenNETCF.IoC.UI.TabWorkspace workspace;
        private System.Windows.Forms.Button action;
    }
}

