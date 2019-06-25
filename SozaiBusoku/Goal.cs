using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class Goal : asd.TextureObject2D
    {
        public Goal(asd.Vector2DF pos)
            : base()
        {
            Position = pos;
            Texture = asd.Engine.Graphics.CreateTexture2D("goal.png");
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2, Texture.Size.Y / 2);
            Color = new asd.Color(255, 255, 255, 200);//透明感出したい
            Scale = 1.5f *new asd.Vector2DF((float)Program.CellLarge / (float)Texture.Size.X, (float)Program.CellLarge / (float)Texture.Size.Y);
        }
    }
}
