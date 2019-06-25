using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class Explain7 : Explain
    {
        public Explain7(asd.Vector2DF pos)
            :base(pos)
        {
           Texture = asd.Engine.Graphics.CreateTexture2D("Explain7.png");
        }
    }
}
