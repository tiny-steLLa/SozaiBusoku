using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    public class GeneralText : asd.TextObject2D
    {
        public GeneralText(asd.Vector2DF pos, String text, int large , asd.Color mainColor, int aroundLarge, asd.Color aroundColor)
        {
            Font = asd.Engine.Graphics.CreateDynamicFont("Font.ttf", large, mainColor, aroundLarge, aroundColor);
            Position = pos;
            Text = text;
            var size = Font.CalcTextureSize(Text, asd.WritingDirection.Horizontal);

            CenterPosition = size.To2DF() / 2;
        }
    }
}