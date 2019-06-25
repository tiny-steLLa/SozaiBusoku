using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class TitleScene : asd.Scene
    {
        private static bool Mute = false;
        protected override void OnRegistered()
        {
            asd.Layer2D layer = new asd.Layer2D();
            AddLayer(layer);
            layer.AddObject(new BackGround());
            string[,] buttonText = new string[,] 
            {
                { "音ON OFF" },
                { "作る" },
                { "遊ぶ" }
            };
            Button.MakeButtonArray(layer,buttonText,220,50,200,200,70,30,20);
            int textLarge = 2 * Program.CellLarge;
            String text = "圧倒的素材不足\n         N E O!!";
            var title = new GeneralText(new asd.Vector2DF(asd.Engine.WindowSize.X/2, 4*Program.CellLarge), text, textLarge, new asd.Color(2, 20, 20, 200), 5, new asd.Color(222, 222, 222, 200));
            title.LineSpacing = -1.6f * Program.CellLarge;
            layer.AddObject(title);
        }
        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Enter) == asd.KeyState.Push||DualShockController.IsJoystickPush(2))
            {
                int t =  Button.CurrentRow;
                if (t == 0)
                {
                    if (Mute)
                        SoundsController.StartMusic();
                    else
                        SoundsController.StopMusic();
                    Mute = !Mute;

                }
                else if (t == 1)
                    asd.Engine.ChangeScene(new MakingSelectScene());
                else if (t == 2)
                    asd.Engine.ChangeScene(new ExcelSelect(false));
            }
        }
    }
}