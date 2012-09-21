using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC;
using OpenNETCF.IoC.UI;
using System.Diagnostics;

namespace WorkspaceDisposal
{
    public partial class Form2 : Form
    {
        ViewA view = new ViewA();

        int state = 0;
        
        public Form2()
        {
            InitializeComponent();
            deckWorkspace1.SmartPartActivated += new EventHandler<DataEventArgs<ISmartPart>>(deckWorkspace1_SmartPartActivated);
            deckWorkspace1.SmartPartDeactivated += new EventHandler<DataEventArgs<ISmartPart>>(deckWorkspace1_SmartPartDeactivated);
            deckWorkspace1.SmartPartClosing += new EventHandler<DataEventArgs<ISmartPart>>(deckWorkspace1_SmartPartClosing);

            deckWorkspace1.Show(view);

            this.KeyPreview = true;
        }

        void deckWorkspace1_SmartPartClosing(object sender, DataEventArgs<ISmartPart> e)
        {
            Debug.WriteLine("SmartPartClosing");
        }

        void deckWorkspace1_SmartPartDeactivated(object sender, DataEventArgs<ISmartPart> e)
        {
            Debug.WriteLine("SmartPartDeactivated");
        }

        void deckWorkspace1_SmartPartActivated(object sender, DataEventArgs<ISmartPart> e)
        {
            Debug.WriteLine("SmartPartActivated");
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hide_Click(object sender, EventArgs e)
        {
            var sp = deckWorkspace1.ActiveSmartPart;

            switch (state)
            {
                case 0:
                    // simple hide
                    view.Hide();
                    hide.Text = "Show";
                    break;
                case 1:
                    // simple show
                    view.Show();
                    hide.Text = "visible=false";
                    break;
                case 2:
                    // hide with visibility
                    view.Visible = false;
                    hide.Text = "visible=true";
                    break;
                case 3:
                    // show with visibility
                    view.Visible = true;
                    hide.Text = "wksp.Hide";
                    break;
                case 4:
                    // hide through workspace
                    deckWorkspace1.Hide(view);
                    hide.Text = "wksp.Show";
                    break;
                case 5:
                    // show through workspace
                    deckWorkspace1.Show(view);
                    hide.Text = "Deactivate";
                    break;
                case 6:
                    // deactivate through workspace
                    deckWorkspace1.Deactivate(view);
                    hide.Text = "Activate";
                    break;
                case 7:
                    // activate through workspace
                    deckWorkspace1.Activate(view);
                    hide.Text = "Close";
                    break;
                case 8:
                    // close and dispose
                    deckWorkspace1.Close(view);
                    hide.Text = "Show<>";
                    break;
                case 9:
                    // recreate and show
                    view = deckWorkspace1.Show<ViewA>();
                    state = -1;
                    hide.Text = "Hide";
                    break;
            }

            state++;
        }
    }
}