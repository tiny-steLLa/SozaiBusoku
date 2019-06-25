using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class DownArrow : Arrow
    {
        public DownArrow(asd.Vector2DF pos)
            : base (pos)
        {
            Angle = 180;
        }
    }
}
