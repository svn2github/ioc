using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ImageButtonDemo
{
    public interface IBackgroundPaintProvider
    {
        void PaintBackground(Graphics g, Rectangle targetRect, Rectangle sourceRect);
        Image BackgroundImage { get; set; }
    }
}
