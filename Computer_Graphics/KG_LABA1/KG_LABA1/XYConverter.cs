using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KG_LABA1
{
    class XYConverter
    {
        private System.Drawing.PointF min, max, centre;
        private System.Drawing.Size panel_s, image_s;
        private double bottom, k_x, k_y;

        public XYConverter(System.Drawing.PointF min, System.Drawing.PointF max, System.Drawing.Size panel_size, int bottom)
        {
            this.min = min;
            this.max = max;
            this.panel_s = panel_size;
            this.image_s = new System.Drawing.Size((int)(max.X - min.X + 2 * bottom), (int)(max.Y - min.Y + 2 * bottom));
            this.centre = new System.Drawing.PointF(min.X + image_s.Width/2, min.Y + image_s.Height/2);

            this.bottom = bottom;
            k_x = ((double)panel_s.Width) / ((double)image_s.Width);
            k_y = ((double)panel_s.Height) / ((double)image_s.Height);
            k_x = k_y = Math.Min(k_y, k_x);
            
        }

        public float GetY(float y)
        {
            double new_y = y - centre.Y;
            double scaled_y = new_y * k_y + bottom * k_y;
            return (float)(panel_s.Height - scaled_y - panel_s.Height/2);
 }

        public float GetX(float x)
        {
            double new_x = x - centre.X ;
            double scaled_x = new_x * k_x + bottom * k_x;
            return (float) (scaled_x+panel_s.Width/2);
        }

        public System.Drawing.PointF GetPointF(System.Drawing.PointF point)
        {
            return new System.Drawing.PointF(GetX(point.X), GetY(point.Y));
        }

        public System.Drawing.PointF GetPointF(Point point)
        {
            return new System.Drawing.PointF(GetX((float)point.x), GetY((float)point.y));
        }

        public System.Drawing.Point GetPoint(Point point)
        {
            return new System.Drawing.Point((int)GetX((float)point.x), (int)GetY((float)point.y));
        }

        public System.Drawing.Point GetPointWithMargin(Point point)
        {
            return new System.Drawing.Point((int)GetX((float)point.x)+5, (int)GetY((float)point.y)+5);
        }

        public System.Drawing.RectangleF GetRectangleF(System.Drawing.PointF point1,float radius)
        {
            return new System.Drawing.RectangleF(GetX(point1.X), GetY(point1.Y), (float)k_x * 2 * radius, (float)k_y * 2 * radius);
        }

    }
}
