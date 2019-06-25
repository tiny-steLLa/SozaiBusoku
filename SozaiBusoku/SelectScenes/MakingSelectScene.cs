using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class MakingSelectScene : asd.Scene
    {
        protected override void OnRegistered()
        {
            asd.Layer2D layer = new asd.Layer2D();

            AddLayer(layer);
            layer.AddObject(new BackGround());
            String text = "ステージ選択!！";
            layer.AddObject(new GeneralText(new asd.Vector2DF(asd.Engine.WindowSize.X / 2, 2 * Program.CellLarge), text, 40, new asd.Color(2, 20, 20, 200), 5, new asd.Color(222, 222, 222, 200)));
            string[,] buttonText = new string[,]
            {
                { "新しいステージを作る"},
                { "既存ステージの編集"},
                { "戻る(Z)"},
            };
            Button.MakeButtonArray(layer, buttonText, 200, 50, 200, 200, 50, 50, 20);
            base.OnRegistered();
        }
        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push || DualShockController.IsJoystickPush(4))
            {
                asd.Engine.ChangeScene(new TitleScene());
            }

            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Enter) == asd.KeyState.Push||DualShockController.IsJoystickPush(2))
            {
                int t = Button.CurrentRow;
                if (t == 0)
                    asd.Engine.ChangeScene(new StageMaker(""));
                else if (t == 1)
                    asd.Engine.ChangeScene(new ExcelSelect(true));
                else
                    asd.Engine.ChangeScene(new TitleScene());
            }
            base.OnUpdated();
        }
    }
}
