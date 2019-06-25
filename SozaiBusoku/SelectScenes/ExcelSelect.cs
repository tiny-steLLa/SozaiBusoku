using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class ExcelSelect : asd.Scene
    {
        public String[,] FileNames { get; private set; }
        private String[] Files;
        asd.Layer2D Layer;
        private int StageCount;//現在いるステージセレクト画面位置
        private bool IsRemaking;//リメイクするのか遊ぶのか
        public ExcelSelect(bool isRemaking)
        {
            IsRemaking = isRemaking;
        }

        protected override void OnRegistered()
        {
            StageCount = -1;
            Layer = new asd.Layer2D();
            AddLayer(Layer);
            Files = System.IO.Directory.GetFiles(Environment.CurrentDirectory +"\\", "*.xlsx", System.IO.SearchOption.AllDirectories);
            for (int i = 0; i < Files.Count(); i++)
                Files[i] = Files[i].Replace(Environment.CurrentDirectory + "\\", "").Replace(".xlsx", "");
            //遷移が分かりにくい
            if (Files.Count() == 0)
            { 
                asd.Engine.ChangeScene(new TitleScene());
            }
            ChangeStage(true);
        }
        /// <summary>
        /// ExcelStageでほかのStageの名前を表示する
        /// </summary>
        /// <param name="isForwarding">次のステージ欄に行くか、戻るか</param>
        protected void ChangeStage(bool isForwarding)
        {
            if (isForwarding)
                StageCount++;
            else
                StageCount--;
            int stageNum = Files.Count() - 10 * StageCount;//現在地のステージ数

            if (StageCount == -1)
            {
                asd.Engine.ChangeScene(new TitleScene());
                return;
            }
            if (stageNum <= 0)
                return;

            if (stageNum <= 10)
            {
                FileNames = new string[stageNum + 1, 1];
                FileNames[0, 0] = "前のステージへ(Z)";
                for (int i = 1; i <= stageNum; i++)
                    FileNames[i, 0] = Files[(i - 1) + 10 * StageCount];
            }
            else
            {
                FileNames = new string[12, 1];
                FileNames[0, 0] = "前のステージへ(Z)";
                FileNames[11, 0] = "次のステージ(X)";
                for (int i = 1; i <= 10; i++)
                    FileNames[i, 0] = Files[(i - 1) + 10 * StageCount];
            }
            foreach (var a in Layer.Objects)
                a.Dispose();
            Layer.AddObject(new BackGround());
            String text = "ステージ選択!！";
            Layer.AddObject(new GeneralText(new asd.Vector2DF(asd.Engine.WindowSize.X / 2, 1 * Program.CellLarge), text, 40, new asd.Color(2, 20, 20, 200), 5, new asd.Color(222, 222, 222, 200)));
            Button.MakeButtonArray(Layer, FileNames, 100, 50, 100, 100, 10, 10,20);
        }

        protected override void OnUpdated()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Enter) == asd.KeyState.Push||DualShockController.IsJoystickPush(2))
            {
                int t = Button.CurrentRow;
                if (t == 0)
                    ChangeStage(false);
                else if (t == 11)
                    ChangeStage(true);
                else if (!IsRemaking)
                    asd.Engine.ChangeScene(new ExcelStage( FileNames[t, 0] + ".xlsx",false));
                else
                    asd.Engine.ChangeScene(new StageMaker( FileNames[t,0] + ".xlsx"));
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.KeyState.Push||DualShockController.IsJoystickPush(4))
                ChangeStage(false);
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.X) == asd.KeyState.Push||DualShockController.IsJoystickPush(5))
                ChangeStage(true);
        }
    }
}
