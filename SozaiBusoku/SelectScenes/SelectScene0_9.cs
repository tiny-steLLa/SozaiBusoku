using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class SelectScene0_9 : asd.Scene
    {
        protected override void OnRegistered()
        {
            asd.Layer2D layer = new asd.Layer2D();
            AddLayer(layer);
            layer.AddObject(new BackGround());
        }
        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.B) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new SelectScene());
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num0) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new Stage0());
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num1) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new Stage1());
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num2) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new Stage2());
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num3) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new Stage3());
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num4) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new Stage4());
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num5) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new Stage5());
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num6) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new Stage6());
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num7) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new Stage7());
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num8) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new Stage8());
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num9) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new Stage9());
            }
        }
    }
}
