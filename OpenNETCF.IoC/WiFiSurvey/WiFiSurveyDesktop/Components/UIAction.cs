using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace WiFiSurveryDesktop.Components
{
    public partial class UIAction : UserControl
    {
        private Rectangle m_dragBoxFromMouseDown;
        private string m_helpPath;

        private string m_deviceName;
        private string m_accessPoint;
        private string m_signalStrength;
        private ToolTip tip;
        private IPAddress m_ipAddress;
        private Stopwatch m_downTimer = new Stopwatch();
        private bool m_connected;

        public Boolean Connected
        {
            get { return m_connected; }
            set
            {
                m_connected = value;
                if (m_connected)
                {
                    m_downTimer.Stop();
                }
                else
                {
                    m_downTimer.Reset();
                    m_downTimer.Start();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
                if (tip != null) tip.Dispose();
                if (m_actionFont != null) m_actionFont.Dispose();
            }
            base.Dispose(disposing);
        }

        public void UpdateData(string Data)
        {
            char[] split = new char[1];
            split[0] = ':';
            String[] dataArray = Data.Split(split);

            if (dataArray.Length > 1)
            {
                m_deviceName = dataArray[0];
                m_accessPoint = dataArray[1];
                m_signalStrength = dataArray[2];
            }
            else
            {
                m_deviceName = "";
                m_accessPoint = "";
                m_signalStrength = "";
            }
        }

        public UIAction(IPAddress ipAddress, string Data)
        {
            InitializeComponent();

            m_ipAddress = ipAddress;

            UpdateData(Data);

            SetEventHandlers();
            this.DoubleBuffered = true;

            //Tool Tip
            tip = new ToolTip();
            tip.AutoPopDelay = 3000;
            tip.InitialDelay = 1000;
            tip.ReshowDelay = 500;
            tip.ShowAlways = true;
            tip.SetToolTip(ActionPanel, ipAddress.ToString());            
        }

        void SetEventHandlers()
        {
            this.ActionPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UIAction_MouseDown);
            this.ActionPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UIAction_MouseMove);
            this.ActionPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UIAction_MouseClick);
            this.ActionPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UIAction_MouseUp);
        }

        private Font m_actionFont;
        private Brush m_controlBrush;
        private Brush m_selectedBrush;
        private LinearGradientBrush m_gradientBrush;

        protected override void OnPaint(PaintEventArgs e)
        {
            SuspendLayout();
            base.OnPaint(e);
            Graphics g = e.Graphics;

            if (m_actionFont == null) m_actionFont = new Font("MicrosoftSansSerif", 12);
            if (m_controlBrush == null) m_controlBrush = new SolidBrush(SystemColors.Control);
            if (m_selectedBrush == null) m_selectedBrush = new SolidBrush(SystemColors.ActiveCaption);
            if(m_gradientBrush == null) m_gradientBrush = new LinearGradientBrush(ActionPanel.Location, new Point(ActionPanel.Right, ActionPanel.Bottom), Color.LightBlue, Color.White);

            if (Connected)
            {
                g.FillRectangle(m_gradientBrush, ActionPanel.Bounds);
                label1.Text = m_deviceName + "(" + m_ipAddress.ToString() + ") @ " + m_accessPoint + "(" + m_signalStrength + ")";
            }
            else
            {
                g.FillRectangle(m_selectedBrush, ActionPanel.Bounds);
                label1.Text = m_deviceName + "(Disconnected) " + m_downTimer.Elapsed.Hours.ToString("D2") + ":" + m_downTimer.Elapsed.Minutes.ToString("D2") + ":" + m_downTimer.Elapsed.Seconds.ToString("D2");

            }

            //g.DrawString(m_ipAddress.ToString(), m_actionFont, Brushes.Black, 0, 0);
            ResumeLayout(true);
        }

        #region Mouse Handling
        private void UIAction_MouseUp(object sender, MouseEventArgs e)
        {
        }
        private void UIAction_MouseClick(object sender, MouseEventArgs e)
        {
        }
        private void UIAction_MouseMove(object sender, MouseEventArgs e)
        {
        }
        private void UIAction_MouseDown(object sender, MouseEventArgs e)
        {
        }
        #endregion

    }
}
