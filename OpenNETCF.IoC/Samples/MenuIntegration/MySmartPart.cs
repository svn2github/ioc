using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC.UI;
using OpenNETCF.IoC;
using System.Windows.Forms;

namespace MenuIntegration
{
    class MySmartPart : MenuSmartPart
    {
        private Label label;
        private string m_menuText;
        private MainMenu m_menu;
        private ISmartPart m_menuTarget;
        private string m_targetName;

        protected override string MenuText
        {
            get { return m_menuText; }
        }

        public MySmartPart(string menuText, string text, string targetName)
        {
            InitializeComponent();

            m_menuText = menuText;
            label.Text = text;
            m_targetName = targetName;

            // get the items we need from the DI container
            m_menu = RootWorkItem.Items.Get<MainMenu>(ItemNames.Menu);

            Menu.Click += new EventHandler(Menu_Click);
        }

        void Menu_Click(object sender, EventArgs e)
        {
            if (m_menuTarget == null)
            {
                m_menuTarget = RootWorkItem.SmartParts[m_targetName];
            }

            this.Workspace.Show(m_menuTarget);
        }

        public override void OnActivated()
        {
            if (!m_menu.MenuItems.Contains(Menu))
            {
                m_menu.MenuItems.Add(Menu);
            }
        }

        public override void OnDeactivated()
        {
            if (m_menu.MenuItems.Contains(Menu))
            {
                m_menu.MenuItems.Remove(Menu);
            }
        }

        private void InitializeComponent()
        {
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.label.Location = new System.Drawing.Point(0, 44);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(150, 57);
            this.label.Text = "[not set]";
            this.label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MySmartPart
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.label);
            this.Name = "MySmartPart";
            this.ResumeLayout(false);

        }
    }
}
