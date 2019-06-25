using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    class EnemyGreen : EnemyRed
    {
        public EnemyGreen(asd.Vector2DF pos)
        :base(pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("asobu/EnemyGreen.png");
        }
    }
}
