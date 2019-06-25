using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class RandomStage :GeneralStage
    {
        protected override void OnRegistered()
        {
            GameLayer.AddObject(new Goal(new asd.Vector2DF(23*CellLarge, 10*CellLarge)));
            Random r = new Random();
            Random rand = new Random();
            //重複なし乱数配列生成
            int[] array = new int[16];
            for (int i = 0; i < 16; i++)
            {
                array[i] = i;
            }
            for (int i = 0; i < 16; i++)
            {
                int x = r.Next(16);
                int t = array[i];
                array[i] = array[x];
                array[x] = t;
            }
            for (int i = 2; i < 23; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    int p = rand.Next(13);
                    if (p == 0 || p == 1)
                    {
                        GameLayer.AddObject(new YellowWall(new asd.Vector2DF(CellLarge * i , CellLarge * array[j] + CellLarge)));
                    }
                    if (p == 2 || p == 3)
                    {
                        GameLayer.AddObject(new WhiteWall(new asd.Vector2DF(CellLarge * i , CellLarge * array[j] + CellLarge)));
                    }
                    if (p == 4 || p == 5)
                    {
                        GameLayer.AddObject(new EnemyWhite(new asd.Vector2DF(CellLarge * i , CellLarge * array[j] + CellLarge)));
                    }
                    if (p == 6 || p == 7)
                    {
                        GameLayer.AddObject(new EnemyYellow(new asd.Vector2DF(CellLarge * i , CellLarge * array[j] + CellLarge)));
                    }
                }
            }
            GameLayer.AddObject(new Player(new asd.Vector2DF(CellLarge, 10*CellLarge)));
        }
        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.T) == asd.KeyState.Push)
            {
                asd.Engine.ChangeSceneWithTransition(new TitleScene(), new asd.TransitionFade(0, 0));
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.R) == asd.KeyState.Push)
            {
                asd.Engine.ChangeSceneWithTransition(new RandomStage(), new asd.TransitionFade(0, 0));
            }
        }
    }
}
