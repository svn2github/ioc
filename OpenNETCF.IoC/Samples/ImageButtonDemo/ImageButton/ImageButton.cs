using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Reflection;
using System.Media;
using System.IO;
using System.Diagnostics;
using OpenNETCF.IoC;

using System.Threading;

namespace ImageButtonDemo
{
    public partial class ImageButton : UserControl
    {
        public ButtonState ButtonState { get; set; }
        public int LeftMargin { get; set; }
        public int RightMargin { get; set; }
        public bool Multiline { get; set; }

        private string m_leftText;
        private string m_rightText;
        private string m_centerText;
        private Bitmap m_downBuffer;
        private Bitmap m_upBuffer;
        private Image m_upImage;
        private Image m_downImage;
        private Image m_rightDownImage;
        private Image m_rightUpImage;
        private Color m_upTextColor;
        private Color m_downTextColor;
        private Color m_transparentColor;
        private PictureBoxSizeMode m_sizeMode = PictureBoxSizeMode.StretchImage;
        private ImageAttributes m_transparency;
        private Brush m_transparentBrush;
        private Brush m_upTextBrush;
        private Brush m_downTextBrush;
        private MethodInfo m_parentPaint;

        public ImageButton()
        {
            InitializeComponent();

            m_upTextColor = Color.Black;
            m_downTextColor = Color.White;
            m_transparentColor = Color.FromArgb(255, 0, 255);

            m_transparency = new ImageAttributes();
            m_transparency.SetColorKey(TransparentColor, TransparentColor);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.ButtonState = ButtonState.Down;
            Refresh();

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.ButtonState = ButtonState.Up;
            Refresh();
            
            base.OnMouseUp(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.ButtonState = ButtonState.Up;
            Refresh();

            base.OnLostFocus(e);
        }

        private void ResetBuffers()
        {
            if (m_upBuffer != null)
            {
                m_upBuffer.Dispose();
                m_upBuffer = null;
                m_downBuffer.Dispose();
                m_downBuffer = null;
            }
            Refresh();
        }

        public Color UpTextColor
        {
            get { return m_upTextColor; }
            set
            {
                if(m_upTextBrush != null)
                {
                    m_upTextBrush.Dispose();
                }

                m_upTextColor = value;

                m_upTextBrush = new SolidBrush(UpTextColor);

                ResetBuffers();
            }
        }

        public Color TransparentColor
        {
            get { return m_transparentColor; }
            set
            {
                if (m_transparentBrush != null)
                {
                    m_transparentBrush.Dispose();
                }

                m_transparentColor = value;

                m_transparentBrush = new SolidBrush(TransparentColor);

                ResetBuffers();
            }
        }

        public PictureBoxSizeMode SizeMode
        {
            get { return m_sizeMode; }
            set
            {
                m_sizeMode = value;
                ResetBuffers();
            }
        }

        public Color DownTextColor
        {
            get { return m_downTextColor; }
            set
            {
                if (m_downTextBrush != null)
                {
                    m_downTextBrush.Dispose();
                }

                m_downTextColor = value;

                m_downTextBrush = new SolidBrush(DownTextColor);

                ResetBuffers();
            }
        }

        public Image UpImage 
        {
            get { return m_upImage; }
            set
            {
                m_upImage = value;
                ResetBuffers();
            }
        }

        public Image DownImage
        {
            get { return m_downImage; }
            set
            {
                m_downImage = value;
                ResetBuffers();
            }
        }

        private Image RightImage
        {
            get { return ButtonState == ButtonState.Up ? RightUpImage : RightDownImage; }
        }

        public Image RightDownImage
        {
            get { return m_rightDownImage; }
            set
            {
                m_rightDownImage = value;
                ResetBuffers();
            }
        }

        public Image RightUpImage
        {
            get { return m_rightUpImage; }
            set
            {
                m_rightUpImage = value;
                ResetBuffers();
            }
        }

        public string CenterText
        {
            get { return m_centerText; }
            set
            {
                m_centerText = value;
                ResetBuffers();
            }
        }

        public string LeftText 
        {
            get { return m_leftText; }
            set
            {
                m_leftText = value;
                ResetBuffers();
            }
        }

        public string RightText 
        {
            get { return m_rightText; }
            set
            {
                m_rightText = value;
                ResetBuffers();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        private void CheckBrushes()
        {
            if (m_downTextBrush == null)
            {
                m_downTextBrush = new SolidBrush(DownTextColor);
            }

            if (m_upTextBrush == null)
            {
                m_upTextBrush = new SolidBrush(UpTextColor);
            }

            if (m_transparentBrush == null)
            {
                m_transparentBrush = new SolidBrush(TransparentColor);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (m_parentPaint == null)
            {
                m_parentPaint = this.Parent.GetType().GetMethod("OnPaint", BindingFlags.Instance | BindingFlags.NonPublic);
            }

            if (m_upBuffer == null)
            {
                m_upBuffer = new Bitmap(this.Width, this.Height);
                m_downBuffer = new Bitmap(this.Width, this.Height);
                Bitmap transparentBackground = new Bitmap(this.Width, this.Height);

                CheckBrushes();

                using (Graphics gdn = Graphics.FromImage(m_downBuffer))
                using (Graphics gup = Graphics.FromImage(m_upBuffer))
                using (Graphics gtrans = Graphics.FromImage(transparentBackground))
                {
                    gtrans.FillRectangle(m_transparentBrush, 0, 0, this.Width, this.Height);

                    Rectangle rcPaint = e.ClipRectangle;
                    rcPaint.Offset(Left, Top);

                    IBackgroundPaintProvider bgPaintProvider = Parent as IBackgroundPaintProvider;
                    if (bgPaintProvider != null)
                    {
                        bgPaintProvider.PaintBackground(gup, e.ClipRectangle, rcPaint);
                        bgPaintProvider.PaintBackground(gdn, e.ClipRectangle, rcPaint);
                    }
                    if (UpImage != null)
                    {
                        gup.DrawImage(transparentBackground, new Rectangle(0, 0, Width, Height), 0, 0, transparentBackground.Width, transparentBackground.Height, GraphicsUnit.Pixel, m_transparency);
                        gup.DrawImage(this.UpImage, new Rectangle(0, 0, Width, Height), 0, 0, UpImage.Width, UpImage.Height, GraphicsUnit.Pixel, m_transparency);
                    }

                    if (DownImage != null)
                    {
                        gdn.DrawImage(this.DownImage, new Rectangle(0, 0, Width, Height), 0, 0, DownImage.Width, DownImage.Height, GraphicsUnit.Pixel, m_transparency);
                    }

                    if (!string.IsNullOrEmpty(LeftText))
                    {
                        SizeF size = gup.MeasureString(LeftText, this.Font);
                        int top = (int)(this.Height - size.Height) / 2;
                        gup.DrawString(this.LeftText, this.Font, m_upTextBrush, LeftMargin, top);
                        gdn.DrawString(this.LeftText, this.Font, m_downTextBrush, LeftMargin, top);
                    }

                    if (!string.IsNullOrEmpty(RightText))
                    {
                        SizeF size = e.Graphics.MeasureString(RightText, this.Font);
                        int top = (int)(this.Height - size.Height) / 2;
                        int left = this.Width - (int)(size.Width + RightMargin);
                        gup.DrawString(this.RightText, this.Font, m_upTextBrush, left, top);
                        gdn.DrawString(this.RightText, this.Font, m_downTextBrush, left, top);
                    }

                    if (!string.IsNullOrEmpty(CenterText))
                    {
                        SizeF size = e.Graphics.MeasureString(CenterText, this.Font);
                        int top = (int)(this.Height - size.Height) / 2;
                        int left = (int)(this.Width - size.Width) / 2;

                        if (Multiline)
                        {
                            Rectangle rectCentered = new Rectangle(
                                (int)((this.Width - size.Width) / 2),
                                10,
                                (int)size.Width, (int)size.Height);

                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Center;
                            gup.DrawString(CenterText, this.Font, m_upTextBrush, rectCentered, sf);
                            gdn.DrawString(CenterText, this.Font, m_downTextBrush, rectCentered, sf);
                        }
                        else
                        {
                            gup.DrawString(CenterText, this.Font, m_upTextBrush, left, top);
                            gdn.DrawString(CenterText, this.Font, m_downTextBrush, left, top);
                        }                        
                    }
                }
            }

            if (RightImage != null)
            {
                using (Graphics gright = Graphics.FromImage((ButtonState == ButtonState.Up) ? m_upBuffer : m_downBuffer))
                {
                    {
                        gright.DrawImage(this.RightImage, new Rectangle(Width - RightImage.Width - RightMargin, (Height - RightImage.Height) / 2, RightImage.Width, RightImage.Height),
                            0, 0, RightImage.Width, RightImage.Height, GraphicsUnit.Pixel, m_transparency);
                    }
                }
            }

            Image image = (ButtonState == ButtonState.Up) ? m_upBuffer : m_downBuffer;

            int x, y, w, h;

            switch (SizeMode)
            {
                default:
                case PictureBoxSizeMode.Normal:
                    x = 0;
                    y = 0;
                    w = UpImage.Width;
                    h = UpImage.Height;
                    break;
                case PictureBoxSizeMode.CenterImage:
                    x = (this.Width - image.Width) / 2;
                    y = (this.Height - image.Height) / 2;
                    w = UpImage.Width;
                    h = UpImage.Height;
                    break;
                case PictureBoxSizeMode.StretchImage:
                    x = 0;
                    y = 0;
                    w = this.Width;
                    h = this.Height;
                    break;
            }
            e.Graphics.DrawImage(image, new Rectangle(x, y, w, h), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, m_transparency);
        }
    }

    public enum ButtonState
    {
        Up,
        Down
    }
}
