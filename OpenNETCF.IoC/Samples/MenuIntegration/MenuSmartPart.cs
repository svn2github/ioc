using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC.UI;
using System.Windows.Forms;

namespace MenuIntegration
{
    // should be abstract, but the designer doesn't appreciate abstract UI base classes
    internal class MenuSmartPart : SmartPart
    {
        private MenuItem m_menu;

        protected virtual string MenuText 
        {
            get { return "set me"; } 
        }

        public MenuItem Menu 
        {
            get 
            {
                if (m_menu == null)
                {
                    m_menu = new MenuItem();
                    m_menu.Text = MenuText;
                }
                return m_menu;
            }
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
