using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC.UI;
using System.Diagnostics;
using OpenNETCF.IoC;

namespace SmartPartTests.SmartParts
{
    class Part1 : SmartPart
    {
        [EventPublication(EventNames.CloseSmartPart)]
        public event EventHandler CloseRequested;

        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Label caption;

        public Part1()
        {
            InitializeComponent();

            close.Click += delegate
            {
                if (CloseRequested != null)
                {
                    CloseRequested(this, null);
                }
            };
        }

        private void InitializeComponent()
        {
            this.caption = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // caption
            // 
            this.caption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.caption.Location = new System.Drawing.Point(11, 37);
            this.caption.Name = "caption";
            this.caption.Size = new System.Drawing.Size(127, 29);
            this.caption.Text = "I am a SmartPart";
            this.caption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(29, 63);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(88, 27);
            this.close.TabIndex = 1;
            this.close.Text = "Close";
            // 
            // Part1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.close);
            this.Controls.Add(this.caption);
            this.Name = "Part1";
            this.ResumeLayout(false);

        }

        public override void OnActivated()
        {
            base.OnActivated();

            Debug.WriteLine("OnActivated");
        }
    }
}
