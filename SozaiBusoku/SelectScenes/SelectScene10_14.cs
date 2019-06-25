using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class SelectScene10_15 :asd.Scene
    {
        protected override void OnRegistered()
        {
            asd.Layer2D layer = new asd.Layer2D();

            AddLayer(layer);

            var background = new asd.TextureObject2D();

            background.Texture = asd.Engine.Graphics.CreateTexture2D("Select10~14.png");

            layer.AddObject(background);
        }
        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.B) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new SelectScene());
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num0) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new Stage10());
            }
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num1) == asd.KeyState.Push)
            {
                asd.Engine.ChangeScene(new Stage11());
            }
        }
    }
}
