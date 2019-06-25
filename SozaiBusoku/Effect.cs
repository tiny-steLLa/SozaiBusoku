using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class Effect :asd.EffectObject2D
    {
        public Effect(asd.Vector2DF pos)
        {
            Position = pos;
            Effect = asd.Engine.Graphics.CreateEffect("aaa.efk");
        }
        protected override void OnUpdate()
        {
            if (IsPlaying == false)
                Dispose();
        }
    }
}
