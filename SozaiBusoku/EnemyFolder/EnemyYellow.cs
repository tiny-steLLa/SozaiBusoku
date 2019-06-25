using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku

{
    class EnemyYellow : EnemyRed
    {
        public EnemyYellow(asd.Vector2DF pos)
        : base(pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("EnemyYellow.png");
        }
    }
}
