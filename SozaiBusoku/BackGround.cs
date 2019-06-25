using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class BackGround : asd.GeometryObject2D
    {
        public static asd.Color BackGroundColor { get; private set; }
        public BackGround()
        {
            BackGroundColor = new asd.Color(242, 220, 223, 255);
            Color = BackGroundColor;
            var rect = new asd.RectangleShape();
            rect.DrawingArea = new asd.RectF(0, 0, Program.WindowSizeX, Program.WindowSizeY);
            Shape = rect;
        }

    }
}
