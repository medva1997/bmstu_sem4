using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Laba4
{
    class StandartWorker:BaseWorker
    {

        protected override void drawCircle(ref Bitmap bitmap, int X, int Y, int RX)
        {
            Pen pen = new Pen(drawcolor, 1);
            var graphics = Graphics.FromImage(bitmap);
           
            //graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
            //graphics.Flush(System.Drawing.Drawing2D.FlushIntention.Sync);
            graphics.DrawEllipse(pen, X - RX, Y - RX, 2 * RX, 2 * RX);
            

        }

        protected override void drawEllipse(ref Bitmap bitmap, int X, int Y, int RX, int RY)
        {
            Pen pen = new Pen(drawcolor, 1);
            var graphics = Graphics.FromImage(bitmap);
            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
            graphics.Flush(System.Drawing.Drawing2D.FlushIntention.Sync);
            graphics.DrawEllipse(pen, X - RX, Y - RY, 2 * RX, 2 * RY);
        }

    }
}
