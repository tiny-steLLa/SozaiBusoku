using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    public class WhiteWall : Wall
    {
        public WhiteWall(asd.Vector2DF pos)
            :base(pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("EnemyWhiteSquare.png");

        }
        protected override void OnDispose()
        {
            if (!GeneralStage.IsStageMaking)
            {
                Effect effect = new Effect(Position);
                effect.Scale = new asd.Vector2DF(Program.CellLarge / 5, Program.CellLarge / 5);
                Layer.AddObject(effect);
                effect.Play();
            }
        }

    }
}
