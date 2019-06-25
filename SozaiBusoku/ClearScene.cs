using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class ClearScene :asd.Scene
    {
        protected override void OnRegistered()
        {
            asd.Layer2D layer = new asd.Layer2D();

            AddLayer(layer);

            asd.TextureObject2D background = new asd.TextureObject2D();

            background.Texture = asd.Engine.Graphics.CreateTexture2D("clear.png");

            layer.AddObject(background);
        }

        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push)
            {
                asd.Engine.ChangeSceneWithTransition(new SelectScene(), new asd.TransitionFade(0, 0));
            }
        }
    }
}
