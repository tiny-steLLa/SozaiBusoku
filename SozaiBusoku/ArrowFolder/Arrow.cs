using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    public class Arrow : asd.TextureObject2D
    {
        public Arrow(asd.Vector2DF pos)
            : base()
        {
            //適当な画像
            Texture = asd.Engine.Graphics.CreateTexture2D("arrow4.png");
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2, Texture.Size.Y / 2);
            Color = new asd.Color(255, 255, 255, 200);//透明感出したい
            Scale = new asd.Vector2DF((float)Program.CellLarge/(float)Texture.Size.X, 1.25f*(float)Program.CellLarge / (float)Texture.Size.Y);
            Position = pos;
        }
    }
}
