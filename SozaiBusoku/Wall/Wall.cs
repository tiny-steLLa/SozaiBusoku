using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{

    public class Wall : asd.TextureObject2D
    {
        public Wall(asd.Vector2DF pos)
            : base()
        {
            Position = pos;
            Texture = asd.Engine.Graphics.CreateTexture2D("Wall.png");
            Color = new asd.Color(255, 255, 255, 100);//透明感出したい
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2, Texture.Size.Y / 2);
            Scale = new asd.Vector2DF((float)Program.CellLarge/(float)Texture.Size.X, (float)Program.CellLarge / (float)Texture.Size.Y);
        }
    }
}
