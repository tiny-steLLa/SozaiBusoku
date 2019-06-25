using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class Stage11 : GeneralStage
    {
        protected override void OnRegistered()
        {
            base.OnRegistered();
              
            var arrayDown = new[]
            {
                new {x=1,y=4},
                new {x=2,y=4},
                new {x=3,y=4},
                new {x=1,y=8},
                new {x=2,y=8},
                new {x=3,y=8},
                new {x=5,y=4},
                new {x=6,y=4},
                new {x=7,y=4},
                new {x=9,y=4},
                new {x=10,y=4},
                new {x=11,y=4},
                new {x=13,y=4},
                new {x=14,y=4},
                new {x=15,y=4}
            };
            var arrayUp = new[]
            {
                new {x=5,y=12},
                new {x=7,y=12},
                new {x=6,y=12},
                new {x=9,y=12},
                new {x=10,y=12},
                new {x=11,y=12},
                new {x=1,y=12},
                new {x=2,y=12},
                new {x=3,y=12},
                new {x=5,y=8},
                new {x=6,y=8},
                new {x=7,y=8},
                new {x=17,y=8},
                new {x=18,y=8},
                new {x=19,y=8},
                new {x=17,y=12},
                new {x=18,y=12},
                new {x=19,y=12},
                new {x=21,y=4},
                new {x=22,y=4},
                new {x=23,y=4}
            };
            var arrayLeft = new[]
            {
                new {x=8,y=1},
                new {x=8,y=2},
                new {x=8,y=3},
                new {x=12,y=1},
                new {x=12,y=2},
                new {x=12,y=3},
                new {x=12,y=5},
                new {x=12,y=6},
                new {x=12,y=7},
                new {x=12,y=13},
                new {x=12,y=14},
                new {x=12,y=15},
                new {x=12,y=16},
                new {x=16,y=1},
                new {x=16,y=2},
                new {x=16,y=3},
                new {x=16,y=5},
                new {x=16,y=6},
                new {x=16,y=7},
                new {x=20,y=1},
                new {x=20,y=2},
                new {x=20,y=3},
                new {x=20,y=9},
                new {x=20,y=10},
                new {x=20,y=11},
                new {x=20,y=13},
                new {x=20,y=14},
                new {x=20,y=15},
                new {x=20,y=16}
            };
            var arrayRight = new[]
            {
                new {x=4,y=1},
                new {x=4,y=2},
                new {x=4,y=3},
                new {x=4,y=5},
                new {x=4,y=6},
                new {x=4,y=7},
                new {x=4,y=9},
                new {x=4,y=10},
                new {x=4,y=11},
                new {x=8,y=9},
                new {x=8,y=10},
                new {x=8,y=11},
                new {x=12,y=9},
                new {x=12,y=10},
                new {x=12,y=11},
                new {x=16,y=9},
                new {x=16,y=10},
                new {x=16,y=11},
                new {x=20,y=5},
                new {x=20,y=6},
                new {x=20,y=7}
            };
            var yellowWallArray = new[]
            {
                new {x=13,y=8},
                new {x=14,y=8},
                new {x=15,y=8},
                new {x=17,y=4},
                new {x=18,y=4},
                new {x=19,y=4},
                new {x=21,y=8},
                new {x=22,y=8},
                new {x=23,y=8},
                new {x=21,y=12},
                new {x=22,y=12},
                new {x=23,y=12},
                new {x=8,y=5},
                new {x=8,y=6},
                new {x=8,y=7},
                new {x=8,y=13},
                new {x=8,y=14},
                new {x=8,y=15},
                new {x=8,y=16},
                new {x=16,y=13},
                new {x=16,y=14},
                new {x=16,y=15},
                new {x=16,y=16}
            };
            var arrayWall = new[]
            {
                new {x=4,y=4 },
                new {x=8,y=4 },
                new {x=12,y=4 },
                new {x=16,y=4},
                new {x=20,y=4},
                new {x=4,y=8 },
                new {x=8,y=8 },
                new {x=12,y=8 },
                new {x=16,y=8},
                new {x=20,y=8},
                new {x=4,y=12 },
                new {x=8,y=12 },
                new {x=12,y=12 },
                new {x=16,y=12},
                new {x=20,y=12}
            };
            var arrayWhiteWall = new[]
            {
                new {x=9,y=8},
                new {x=10,y=8},
                new {x=11,y=8},
                new {x=4,y=13},
                new {x=4,y=14},
                new {x=4,y=15},
                new {x=4,y=16},
                new {x=13,y=12},
                new {x=14,y=12},
                new {x=15,y=12}
            };
            var enemyWhiteArray = new[]
            {
                new {x=10,y=2},
                new {x=22,y=14},
            };
            var enemyYellowArray = new[]
            {
                new {x=2,y=10},
                new {x=18,y=2},
            };
            GameLayer.AddObject(new Goal(new asd.Vector2DF(14 * Width + Width / 2, 14 * Width + Width / 2)));
            foreach (var p in arrayDown)
                GameLayer.AddObject(new DownArrow(new asd.Vector2DF(p.x * Width + Width / 2, p.y * Width + Width / 2)));
            foreach (var p in arrayUp)
                GameLayer.AddObject(new UpArrow(new asd.Vector2DF(p.x * Width + Width / 2, p.y * Width + Width / 2)));
            foreach (var p in arrayLeft)
                GameLayer.AddObject(new LeftArrow(new asd.Vector2DF(p.x * Width + Width / 2, p.y * Width + Width / 2)));
            foreach (var p in arrayRight)
                GameLayer.AddObject(new RightArrow(new asd.Vector2DF(p.x * Width + Width / 2, p.y * Width + Width / 2)));
            foreach (var p in enemyWhiteArray)
                GameLayer.AddObject(new EnemyWhite(new asd.Vector2DF(p.x * Width + Width / 2, p.y * Width + Width / 2)));
            foreach (var p in enemyYellowArray)
                GameLayer.AddObject(new EnemyYellow(new asd.Vector2DF(p.x * Width + Width / 2, p.y * Width + Width / 2)));
            foreach (var p in arrayWhiteWall)
                GameLayer.AddObject(new WhiteWall(new asd.Vector2DF(p.x * Width + Width / 2, p.y * Width + Width / 2)));
            foreach (var p in yellowWallArray)
                GameLayer.AddObject(new YellowWall(new asd.Vector2DF(p.x * Width + Width / 2, p.y * Width + Width / 2)));
            foreach (var p in arrayWall)
                GameLayer.AddObject(new Wall(new asd.Vector2DF(p.x * Width + Width / 2, p.y * Width + Width / 2)));

            GameLayer.AddObject(new Player(new asd.Vector2DF(2 * Width + Width / 2, 2 * Width + Width / 2)));
        }
        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.R) == asd.KeyState.Push)
            {
                asd.Engine.ChangeSceneWithTransition(new Stage11(), new asd.TransitionFade(0, 0));
            }
            base.OnUpdated();

        }
    }
}
