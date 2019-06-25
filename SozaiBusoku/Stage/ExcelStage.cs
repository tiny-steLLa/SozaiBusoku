using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace SozaiBusoku
{
    class ExcelStage : StageMaker
    {
        private bool TestPlayMode;
        public ExcelStage(string text, bool testPlayMode) : base(text)
        {
            TestPlayMode = testPlayMode;
            //StageMakerでFilePassが設定される
            IsStageMaking = false;
        }
        protected override void OnRegistered()
        {
            base.OnRegistered();
            if (TestPlayMode)
            {
                ModeText.Text = "テスト中";
                ExplainText.Text = "B-制作に戻る\nSpace-やり直す\nBackSpace-戻す\nZ-タイトルへ";
            }
            else
                ExplainText.Text = "Space-やり直す\nBackSpace-戻す\nZ-タイトルへ";
        }

        protected override void OnUpdated()
        {
            base.OnUpdated();
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Space) == asd.KeyState.Push)
            {
                foreach (var a in GameLayer.Objects)
                    a.Dispose();
                asd.Engine.ChangeScene(new ExcelStage(FilePass, TestPlayMode));
            }

            //以下テストモード
            if ((asd.Engine.Keyboard.GetKeyState(asd.Keys.B) == asd.KeyState.Push) && TestPlayMode)
            {
                foreach (var a in GameLayer.Objects)
                    a.Dispose();
                asd.Engine.ChangeScene(new StageMaker(FilePass));
            }
        }
    }
}
