using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class WarpTexture : asd.TextureObject2D
    {
        /// <summary>
        /// StageMakerで使用
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="warpPos"></param>
        public WarpTexture(asd.Vector2DF pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("WarpZone.png");
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2, Texture.Size.Y / 2);
            Scale = 1.2f * new asd.Vector2DF((float)Program.CellLarge / (float)Texture.Size.X, (float)Program.CellLarge / (float)Texture.Size.Y);
            Color = new asd.Color(255, 255, 255, 100);//透明感出したい
            Position = pos;
        }
    }
}
