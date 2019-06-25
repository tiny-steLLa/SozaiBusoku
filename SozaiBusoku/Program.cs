using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SozaiBusoku
{
    class Program
    {
        //X:Y = 16:9
        private static float Adjustment = 0.8f;//大きさ調整,簡潔な数字を指定
        public static int WindowSizeX { get; private set; }
        public static int WindowSizeY { get; private set; }
        public static int CellLarge { get; private set; }//1マスの大きさ
        public static string StageFileName { get; private set; }

        static void Main(string[] args)
        {
            WindowSizeX = (int)(1600 * Adjustment);
            WindowSizeY = (int)(900 * Adjustment);
            CellLarge = WindowSizeX / 32;
            StageFileName = "stage";
            asd.Engine.Initialize("ASB", WindowSizeX, WindowSizeY, new asd.EngineOption());
            asd.Engine.File.AddRootDirectory("resource/");
    //        asd.Engine.File.AddRootPackageWithPassword("resource.pack", "2211");
            SoundsController.SoundBGM();
            TitleScene scene = new TitleScene();
            asd.Engine.ChangeSceneWithTransition(scene, new asd.TransitionFade(0, 0));

            while (asd.Engine.DoEvents())
            {
                if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Escape) == asd.KeyState.Push)
                {
                    break;
                }
                asd.Engine.Update();
            }
            asd.Engine.Terminate();
        }
    }
}