using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class LeftArrow : Arrow
    {
        public LeftArrow(asd.Vector2DF pos)
            :base(pos)
        {
            Angle = -90;
        } 
    }
}
