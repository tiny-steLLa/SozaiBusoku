using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class ClearScene :asd.Scene
    {
        String TotalMove;
        public ClearScene(int num)
        {
            TotalMove = num.ToString();
        }
        protected override void OnRegistered()
        {
            asd.Layer2D layer = new asd.Layer2D();
            AddLayer(layer);
            layer.AddObject(new BackGround());
            layer.AddObject(new GeneralText(new asd.Vector2DF(asd.Engine.WindowSize.X / 2, 2 * Program.CellLarge), "Clear", 100, new asd.Color(2, 20, 20, 200), 5, new asd.Color(222, 222, 222, 200)));
            layer.AddObject(new GeneralText(new asd.Vector2DF(asd.Engine.WindowSize.X / 2, 16 * Program.CellLarge), "enterキーでタイトルへ", 50, new asd.Color(2, 20, 20, 200), 2, new asd.Color(222, 222, 222, 200)));
            layer.AddObject(new GeneralText(new asd.Vector2DF(asd.Engine.WindowSize.X / 2, 5 * Program.CellLarge), "総移動数"+TotalMove, 40, new asd.Color(2, 20, 20, 200), 2, new asd.Color(222, 222, 222, 200)));
        }

        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Enter) == asd.KeyState.Push|| DualShockController.IsJoystickPush(2))
            {
                asd.Engine.ChangeSceneWithTransition(new TitleScene(), new asd.TransitionFade(0, 0));
            }
        }
    }
}
