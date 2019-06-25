using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    class EnemyBlue : EnemyRed
    {
        public EnemyBlue(asd.Vector2DF pos)
    : base(pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("asobu/EnemyBlue.png");
        }
    }   
}
