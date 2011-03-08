using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ImageButtonDemo
{
    public partial class ViewGradient : ViewBase
    {
        private static int m_count;
        private static string m_typeName;

        public ViewGradient()
        {
            InitializeComponent();
        }


        // Paints the background of the form with a GradientFill pattern.
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            GradientFill.Fill(
            e.Graphics, ClientRectangle,
            Color.Black, Color.LightBlue,
            GradientFill.FillDirection.TopToBottom);
            e.Graphics.Dispose();
        }

        private void pushMulti_Click(object sender, EventArgs e)
        {
            Stack.Push<ViewMulti>();
        }

        private void pushCheckbox_Click(object sender, EventArgs e)
        {
            Stack.Push<ViewCheckbox>();
        }
    }
}
