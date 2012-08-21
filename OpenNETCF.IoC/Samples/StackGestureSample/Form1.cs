using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using OpenNETCF.IoC.UI;

namespace StackGestureSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            pictureBox1.Image = Properties.Resources.center;

            workspace1.GesturesEnabled = true;
            workspace1.GestureReceived += new GestureHandler(workspace1_GestureReceived);
        }

        void workspace1_GestureReceived(IWorkspace workspace, GestureDirection direction)
        {
            Debug.WriteLine("Gesture " + direction.ToString());
            switch (direction)
            {
                case OpenNETCF.IoC.UI.GestureDirection.Down:
                    pictureBox1.Image = Properties.Resources.down;
                    break;
                case OpenNETCF.IoC.UI.GestureDirection.Left:
                    pictureBox1.Image = Properties.Resources.left;
                    break;
                case OpenNETCF.IoC.UI.GestureDirection.Right:
                    pictureBox1.Image = Properties.Resources.right;
                    break;
                case OpenNETCF.IoC.UI.GestureDirection.Up:
                    pictureBox1.Image = Properties.Resources.up;
                    break;

            }
        }
    }
}