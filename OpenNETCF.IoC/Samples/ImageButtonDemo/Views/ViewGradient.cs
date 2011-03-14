using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using OpenNETCF.Drawing;
using System.Drawing.Imaging;

namespace ImageButtonDemo
{
    public partial class ViewGradient : ViewBase
    {
        Bitmap transparentUp = new
                Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("ImageButtonDemo.Resources.pin_button_up.png"));
        Bitmap transparentDown = new
                Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("ImageButtonDemo.Resources.pin_button_dwn.png"));

        const string STATE1 = "State 1";
        const string STATE2 = "State 2";

        public ViewGradient()
        {
            InitializeComponent();
            lblSample.Tag = false;
            lblSample.CenterText = STATE1;
        }


        // Paints the background of the form with a GradientFill pattern.
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            GradientFill.Fill(
            e.Graphics, ClientRectangle,
            Color.Black, Color.LightBlue,
            GradientFill.FillDirection.TopToBottom);

            if (imgButtonBig.UpImage == null)
            {
                // Capture background 
                Bitmap background = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                    System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
                using (Graphics gUp = Graphics.FromImage(background))
                {
                    GraphicsEx gxUp = GraphicsEx.FromGraphics(gUp);

                    ImageAttributes transparency = new ImageAttributes();
                    transparency.SetColorKey(Color.FromArgb(255, 0, 255), Color.FromArgb(255, 0, 255));
                    
                    // Paint the button onto the full screen canvas
                    gxUp.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);


                    if (lblSample.UpImage == null)
                    {
                        // Fill background image of lblSample
                        Bitmap lblUpImage = new Bitmap(lblSample.Width, lblSample.Height);
                        using (Graphics gLabel = Graphics.FromImage(lblUpImage))
                        {
                            gLabel.DrawImage(background, 0, 0, lblSample.Bounds, GraphicsUnit.Pixel);
                        }

                        lblUpImage.Save("testtest.png", ImageFormat.Png);
                        lblSample.UpImage = lblSample.DownImage = lblUpImage;
                        lblSample.Visible = true;
                    }

                    gUp.DrawImage(transparentUp, imgButtonBig.Bounds, 0, 0, transparentUp.Width, transparentUp.Height,
                        GraphicsUnit.Pixel, transparency);

                    // Now create a new bitmap that will be just the layered image
                    Bitmap newButtonUp = new Bitmap(imgButtonBig.Width, imgButtonBig.Height);
                    using (Graphics gButtonUp = Graphics.FromImage(newButtonUp))
                    {
                        gButtonUp.DrawImage(background, 0, 0, imgButtonBig.Bounds, GraphicsUnit.Pixel);
                    }

                    imgButtonBig.UpImage = newButtonUp;


                    // clean house
                    gxUp.Dispose();  
                }
                using (Graphics gDown = Graphics.FromImage(background))
                {
                    GraphicsEx gxDown = GraphicsEx.FromGraphics(gDown);

                    ImageAttributes transparency = new ImageAttributes();
                    transparency.SetColorKey(Color.FromArgb(255, 0, 255), Color.FromArgb(255, 0, 255));

                    // Paint the button onto the full screen canvas
                    gxDown.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

                    gDown.DrawImage(transparentDown, imgButtonBig.Bounds, 0, 0, transparentUp.Width, transparentUp.Height,
                        GraphicsUnit.Pixel, transparency);

                    // Now create a new bitmap that will be just the layered image
                    Bitmap newButtonDown = new Bitmap(imgButtonBig.Width, imgButtonBig.Height);

                    using (Graphics gButtonDown = Graphics.FromImage(newButtonDown))
                    {
                        gButtonDown.DrawImage(background, 0, 0, imgButtonBig.Bounds, GraphicsUnit.Pixel);
                    }

                    imgButtonBig.DownImage = newButtonDown;

                    // clean house
                    gxDown.Dispose();
                }

                imgButtonBig.Visible = true;
                background.Dispose();
            }

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

        private void lblSample_Click(object sender, EventArgs e)
        {
            if ((bool)lblSample.Tag)
            {
                lblSample.CenterText = STATE1;
            }
            else
            {
                lblSample.CenterText = STATE2;
            }

            lblSample.Tag = !((bool)lblSample.Tag);
        }
    }
}
