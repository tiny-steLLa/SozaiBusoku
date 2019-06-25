using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    /// <summary>
    /// MakingStageで使用
    /// </summary>
    class TestPlayer : asd.TextureObject2D
    {
        public TestPlayer (asd.Vector2DF pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("PlayerUp.png");
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2, Texture.Size.Y / 2);
            Scale = new asd.Vector2DF((float)Program.ObjWidth / (float)Texture.Size.X, (float)Program.ObjWidth / (float)Texture.Size.Y);
            Color = new asd.Color(255, 255, 255, 180);//透明感出したい
            Position = pos;
        }
    }
}
