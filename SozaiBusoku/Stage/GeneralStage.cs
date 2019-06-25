using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    public class GeneralStage : asd.Scene
    {
        protected asd.Layer2D GameLayer;
        private asd.Layer2D BackGroundLayer;
        public static asd.Layer2D TextLayer { get; protected set; }//Player移動数はPlayerクラス
        protected int CellLarge = Program.CellLarge;
        public static bool IsStageMaking { get;protected set; }
        public GeneralStage()
        {
            IsStageMaking = false;
        }

        protected override void OnRegistered()
        {
            GameLayer = new asd.Layer2D();
            TextLayer = new asd.Layer2D();
            BackGroundLayer = new asd.Layer2D();
            AddLayer(BackGroundLayer);
            AddLayer(GameLayer);
            AddLayer(TextLayer);
            var back = new BackGround();
            back.Position = new asd.Vector2DF(25 * CellLarge, back.Position.Y);
            BackGroundLayer.AddObject(back);
            var mapObject = new asd.MapObject2D();
            //背景制作
            for (int i = 0; i < 25; ++i)
            {
                for (int j = 0; j < 18; ++j)
                {
                    var chip = new asd.Chip2D();
                    chip.Texture = asd.Engine.Graphics.CreateTexture2D("kusa.png");
                    chip.CenterPosition = new asd.Vector2DF(chip.Texture.Size.X / 2, chip.Texture.Size.Y / 2);
                    chip.Scale = new asd.Vector2DF((float)CellLarge / (float)chip.Texture.Size.X, (float)CellLarge / (float)chip.Texture.Size.Y);
                    chip.Position = new asd.Vector2DF(i * CellLarge, j * CellLarge);
                    mapObject.AddChip(chip);
                }
            }
            BackGroundLayer.AddObject(mapObject);
            for (int i = 0; i < 25; ++i)
            {
                GameLayer.AddObject(new Wall(new asd.Vector2DF(i * CellLarge + CellLarge / 2, 0 * CellLarge + CellLarge / 2)));
                GameLayer.AddObject(new Wall(new asd.Vector2DF(i * CellLarge + CellLarge / 2, 17 * CellLarge + CellLarge / 2)));
            }
            for (int i = 1; i < 17; i++)
            {
                GameLayer.AddObject(new Wall(new asd.Vector2DF(0 * CellLarge + CellLarge / 2, i * CellLarge + CellLarge / 2)));
                GameLayer.AddObject(new Wall(new asd.Vector2DF(24 * CellLarge + CellLarge / 2, i * CellLarge + CellLarge / 2)));
            }
        }
        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push||(DualShockController.IsJoystickHold(4)&&DualShockController.IsJoystickHold(5)))
                asd.Engine.ChangeSceneWithTransition(new TitleScene(), new asd.TransitionFade(0, 0));
        }
    }
}
