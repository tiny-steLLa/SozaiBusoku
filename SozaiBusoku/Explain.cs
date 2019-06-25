using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class Explain : asd.TextureObject2D
    {
        public Explain(asd.Vector2DF pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("explain.png");
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2, Texture.Size.Y / 2);
            Scale = new asd.Vector2DF((float)Program.ObjWidth *7 / (float)Texture.Size.X, (float)Program.ObjWidth*18 / (float)Texture.Size.Y);

            Position = pos;
        }
    }
}
