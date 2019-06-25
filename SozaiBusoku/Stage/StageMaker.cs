using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
namespace SozaiBusoku
{
    class StageMaker : GeneralStage
    {
        private int MakingNum;//生成物を決定する値
        private int PointX, PointY;//生成物のposition生成に利用
        private bool IsPlayerExist, IsGoalExist, IsWarpSetting, IsWarpSearching;
        private static WarpZone WarpRoot;//Warp先設定で使用
        private static asd.TextureObject2D SelectedTexture;//カーソルの画像
        private asd.Layer2D UILayer;
        private asd.Vector2DF MakePos;//制作物のPos

        protected List<int[]> WarpData;
        protected GeneralText ExplainText, ModeText;
        protected bool IsLoaded;
        protected string FilePass;
        /// <summary>
        /// 新規ステージの場合引数は""で,既存ステージはProgram.StageFileName +"\\"+ fileName + ".xlsx"の形式
        /// </summary>
        /// <param name="fileName"></param>
        public StageMaker(string fileName)
        {
            IsPlayerExist = false;
            IsGoalExist = false;
            IsWarpSetting = false;
            IsWarpSearching = false;
            IsStageMaking = true;
            IsLoaded = false;

            WarpData = new List<int[]>();
            ModeText = new GeneralText(new asd.Vector2DF(0 * Program.CellLarge, 0 * Program.CellLarge), "", 70, new asd.Color(2, 20, 20, 200), 2, new asd.Color(222, 222, 222, 200));
            ModeText.Color = new asd.Color(200, 200, 200, 180);
            var explainString = "0～9とQ,W,Eで選択\n(手探ってください)\n \nShift-作る\n \nR-ワープ位置のセット\n \nSpace-テストプレイ\n" + "   " + "（Saveされます)\n \nCtrl+S-セーブ\n \nZ-タイトルへ\n" + "   " + "(Save無し)";
            ExplainText = new GeneralText(new asd.Vector2DF(28.7f * Program.CellLarge, 8 * Program.CellLarge), explainString, 16, new asd.Color(2, 20, 20, 200), 5, new asd.Color(222, 222, 222, 200));
            if (fileName == "")
                FilePass = MakeFileName();
            else
                FilePass = fileName;
            PointX = (int)asd.Engine.Mouse.Position.X / CellLarge;
            PointY = (int)asd.Engine.Mouse.Position.Y / CellLarge;
            MakePos = new asd.Vector2DF(PointX * CellLarge + CellLarge / 2, PointY * CellLarge + CellLarge / 2);
            MakingNum = 0;
            SelectedTexture = new EnemyRed(MakePos);
        }

        protected override void OnRegistered()
        {
            base.OnRegistered();
            TextLayer.AddObject(ModeText);
            TextLayer.AddObject(ExplainText);
            if (!IsStageMaking)
                SelectedTexture.Dispose();
            UILayer = new asd.Layer2D();
            AddLayer(UILayer);
            UILayer.AddObject(SelectedTexture);
            LoadExcelFile();
        }
        protected override void OnUpdated()
        {
            if (IsStageMaking)
            {
                PointX = (int)asd.Engine.Mouse.Position.X / CellLarge;
                PointY = (int)asd.Engine.Mouse.Position.Y / CellLarge;
                MakePos = new asd.Vector2DF(PointX * CellLarge + CellLarge / 2, PointY * CellLarge + CellLarge / 2);
                if (PositionCheck(MakePos) && !IsWarpSetting)
                {
                    MakingCheck();
                    ChangeNum();
                    SelectedTexture.Position = MakePos;
                }
                else if (IsWarpSetting)
                {
                    SelectedTexture.Position = new asd.Vector2DF(-300, -300);//画面外で見えなくすることで消した感じを出す
                    WarpSetting();
                }
                //ctrl+Sでセーブ
                if ((asd.Engine.Keyboard.GetKeyState(asd.Keys.LeftControl) == asd.KeyState.Hold || asd.Engine.Keyboard.GetKeyState(asd.Keys.RightControl) == asd.KeyState.Hold) && asd.Engine.Keyboard.GetKeyState(asd.Keys.S) == asd.KeyState.Push)
                    SaveStage();
                if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Space) == asd.KeyState.Push)
                    TestPlay();
            }

            if (IsLoaded)//load後に一回だけ呼びたい
            {
                //ワープ先のろーでぃんぐみたいな処理
                WarpZone.ChangeAllID();
                foreach (var data in WarpData)
                {
                    var warp = WarpZone.WarpList.Find(w => w.Position == new asd.Vector2DF(data[0] * CellLarge + CellLarge / 2, data[1] * CellLarge + CellLarge / 2));
                    warp.ChangeWarpPos(WarpZone.WarpList.Find(w => w.Position == new asd.Vector2DF(data[2] * CellLarge + CellLarge / 2, data[3] * CellLarge + CellLarge / 2)));
                }
                IsLoaded = false;
            }
            base.OnUpdated();
        }

        protected void TestPlay()
        {
            SaveStage();
            foreach (var a in GameLayer.Objects)
                a.Dispose();
            asd.Engine.ChangeScene(new ExcelStage(FilePass, true));
        }

        protected void MakingCheck()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.LeftShift) == asd.KeyState.Hold || asd.Engine.Keyboard.GetKeyState(asd.Keys.RightShift) == asd.KeyState.Hold)
            {
                if (PointX < 1 || PointX > 23 || PointY < 1 || PointY > 16)
                    return;
                MakeSomething(MakePos, MakingNum);
            }
        }
        /// <summary>
        /// ワープ先をEnterで変化
        /// </summary>
        protected void WarpSetting()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.LeftShift) == asd.KeyState.Push || asd.Engine.Keyboard.GetKeyState(asd.Keys.RightShift) == asd.KeyState.Push)
            {
                var warp = WarpZone.WarpList.Find(w => w.Position == MakePos);
                if (warp == null)
                    return;
                if (IsWarpSearching == false)
                {
                    ModeText.Text = "ワープ先の位置選択";
                    WarpRoot = warp;
                    IsWarpSearching = true;
                }
                //warp先を決定
                else
                {
                    ModeText.Text = "ワープ元の位置選択";
                    var warpRoot = WarpZone.WarpList.Find(w => w.Position == WarpRoot.Position);
                    warpRoot.ChangeWarpPos(warp);
                    IsWarpSearching = false;
                }
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.R) == asd.KeyState.Push)
            {
                ModeText.Text = "";
                WarpRoot = null;//一応
                IsWarpSearching = false;
                IsWarpSetting = false;
            }
        }

        protected void LoadExcelFile()
        {
            string[] files = System.IO.Directory.GetFiles(Environment.CurrentDirectory, "*.xlsx", System.IO.SearchOption.AllDirectories);
            bool isFileExist = false;
            foreach (var file in files)
            {
                if (file == Environment.CurrentDirectory + "\\" + FilePass)
                {
                    isFileExist = true;
                    break;
                }
            }
            if (!isFileExist)
                return;
            else
                using (var wb = new XLWorkbook(FilePass))
                {
                    foreach (var ws in wb.Worksheets)
                    {
                        for (int t = 1; t < ws.RowsUsed().Count() + 1; t++)
                        {
                            int x = int.Parse(ws.Cell(t, 1).Value.ToString());
                            int y = int.Parse(ws.Cell(t, 2).Value.ToString());
                            int num = int.Parse(ws.Cell(t, 3).Value.ToString());
                            MakeSomething(new asd.Vector2DF(x * CellLarge + CellLarge / 2, y * CellLarge + CellLarge / 2), num);
                            //ワープ制作の処理
                            if (num == 12)
                            {
                                int warpX = int.Parse(ws.Cell(t, 4).Value.ToString());
                                int warpY = int.Parse(ws.Cell(t, 5).Value.ToString());
                                int[] a = { x, y, warpX, warpY };
                                WarpData.Add(a);
                            }
                        }
                    }
                }
            IsLoaded = true;
        }

        /// <summary>
        /// 使われてないファイル名を返す、インスタンス生成時のみ利用
        /// </summary>
        /// <returns></returns>
        private String MakeFileName()
        {
            string[] files = System.IO.Directory.GetFiles(Environment.CurrentDirectory + "\\" + Program.StageFileName, "*.xlsx", System.IO.SearchOption.AllDirectories);
            int t = 0;
            bool isFileExist = true;
            while (isFileExist)
            {
                isFileExist = false;
                foreach (var file in files)
                    if (file == Environment.CurrentDirectory + "\\" + Program.StageFileName + "\\Stage" + t + ".xlsx")
                    {
                        isFileExist = true;
                        break;
                    }
                if (isFileExist == false)
                    break;
                t++;
            }
            return Program.StageFileName + "\\Stage" + t + ".xlsx";
        }

        /// <summary>
        /// 新しい敵とか追加するときloadも変える
        /// </summary>
        private void SaveStage()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Worksheet");
                int count = 1;
                foreach (var obj in GameLayer.Objects)
                {
                    if (PositionCheck(obj.Position) && EnemyToNum(obj) != -1)
                    {
                        worksheet.Cell(count, 1).Value = (int)(obj.Position.X / (float)CellLarge);
                        worksheet.Cell(count, 2).Value = (int)(obj.Position.Y / (float)CellLarge);
                        worksheet.Cell(count, 3).Value = EnemyToNum(obj);
                        if (obj is WarpZone)
                        {
                            var warp = obj as WarpZone;
                            worksheet.Cell(count, 4).Value = (int)(warp.WarpPos.X / (float)CellLarge);
                            worksheet.Cell(count, 5).Value = (int)(warp.WarpPos.Y / (float)CellLarge);
                        }
                        count++;
                    }
                }
                workbook.SaveAs(FilePass);
            }
            UILayer.AddObject(new VanishingText(new asd.Vector2DF(12 * CellLarge, 1 * CellLarge), FilePass + "  Save完了", (int)(1.20f * Program.CellLarge), BackGround.BackGroundColor, 5, new asd.Color(255, 255, 255, 0), 2));
        }

        protected bool PositionCheck(asd.Vector2DF pos)
        {
            if (pos.X > 1 * CellLarge && pos.X < 24 * CellLarge && pos.Y > 1 * CellLarge && pos.Y < 17 * CellLarge)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 数字から物体へ
        /// </summary>
        /// <param name="pos">作り出す位置</param>
        /// <param name="makingNum"></param>
        protected void MakeSomething(asd.Vector2DF pos, int makingNum)
        {
            bool canMake = true;
            foreach (var obj in GameLayer.Objects)
            {
                if (obj.Position == pos && !(obj is Arrow))
                    canMake = false;
                //流れてくるのはArrowのみ
                else if (obj.Position == pos && EnemyToNum(obj) == makingNum)
                    canMake = false;
            }

            if (canMake || makingNum == 100)
            {
                switch (makingNum)
                {
                    case 0:
                        GameLayer.AddObject(new EnemyRed(pos));
                        break;
                    case 1:
                        GameLayer.AddObject(new EnemyWhite(pos));
                        break;
                    case 2:
                        GameLayer.AddObject(new EnemyYellow(pos));
                        break;
                    case 3:
                        GameLayer.AddObject(new Wall(pos));
                        break;
                    case 4:
                        GameLayer.AddObject(new WhiteWall(pos));
                        break;
                    case 5:
                        GameLayer.AddObject(new YellowWall(pos));
                        break;
                    case 6:
                        GameLayer.AddObject(new UpArrow(pos));
                        break;
                    case 7:
                        GameLayer.AddObject(new RightArrow(pos));
                        break;
                    case 8:
                        GameLayer.AddObject(new DownArrow(pos));
                        break;
                    case 9:
                        GameLayer.AddObject(new LeftArrow(pos));
                        break;
                    case 10:
                        if (!IsPlayerExist && IsStageMaking)
                        {
                            GameLayer.AddObject(new PlayerTexture(pos));
                            IsPlayerExist = true;
                        }
                        else if (!IsPlayerExist && !IsStageMaking)
                        {
                            GameLayer.AddObject(new Player(pos));
                            IsPlayerExist = true;//ステージ生成時にplayerが2個生成されなくなる
                        }
                        break;
                    case 11:
                        if (!IsGoalExist)
                        {
                            GameLayer.AddObject(new Goal(pos));
                            IsGoalExist = true;
                        }
                        break;
                    case 12:
                        GameLayer.AddObject(new WarpZone(pos, pos));
                        break;
                    case 100:
                        foreach (var obj in GameLayer.Objects)
                        {
                            if (obj.Position == pos)
                            {

                                if (obj is PlayerTexture)
                                    IsPlayerExist = false;
                                if (obj is Goal)
                                    IsGoalExist = false;
                                obj.Dispose();
                            }
                        }
                        break;
                }
            }
        }


        /// <summary>
        /// 押されたキー番号の記憶、カーソル画像の記憶、変更するときEnemyToNumもかえる
        /// </summary>
        private void ChangeNum()
        {
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num0) == asd.KeyState.Push)
            {//enemyRed
                SelectedTexture.Dispose();
                SelectedTexture = new EnemyRed(MakePos);
                UILayer.AddObject(SelectedTexture);
                MakingNum = 0;
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num1) == asd.KeyState.Push)
            {//enemyYellow
                SelectedTexture.Dispose();
                SelectedTexture = new EnemyWhite(MakePos);
                UILayer.AddObject(SelectedTexture);
                MakingNum = 1;
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num2) == asd.KeyState.Push)
            {//enemyWhite
                SelectedTexture.Dispose();
                SelectedTexture = new EnemyYellow(MakePos);
                UILayer.AddObject(SelectedTexture);
                MakingNum = 2;
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num3) == asd.KeyState.Push)
            {//wall
                SelectedTexture.Dispose();
                SelectedTexture = new Wall(MakePos);
                MakingNum = 3;
                UILayer.AddObject(SelectedTexture);
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num4) == asd.KeyState.Push)
            {//whiteWall
                SelectedTexture.Dispose();
                SelectedTexture = new WhiteWall(MakePos);
                MakingNum = 4;
                UILayer.AddObject(SelectedTexture);
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num5) == asd.KeyState.Push)
            {//yellowWall
                SelectedTexture.Dispose();
                SelectedTexture = new YellowWall(MakePos);
                MakingNum = 5;
                UILayer.AddObject(SelectedTexture);
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num6) == asd.KeyState.Push)
            {//upArrow
                SelectedTexture.Dispose();
                SelectedTexture = new UpArrow(MakePos);
                MakingNum = 6;
                UILayer.AddObject(SelectedTexture);
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num7) == asd.KeyState.Push)
            {//rightArrow
                SelectedTexture.Dispose();
                SelectedTexture = new RightArrow(MakePos);
                MakingNum = 7;
                UILayer.AddObject(SelectedTexture);
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num8) == asd.KeyState.Push)
            {//downArrow
                SelectedTexture.Dispose();
                SelectedTexture = new DownArrow(MakePos);
                MakingNum = 8;
                UILayer.AddObject(SelectedTexture);
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Num9) == asd.KeyState.Push)
            {//leftArrow
                SelectedTexture.Dispose();
                SelectedTexture = new LeftArrow(MakePos);
                MakingNum = 9;
                UILayer.AddObject(SelectedTexture);
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Q) == asd.KeyState.Push)
            {//player
                SelectedTexture.Dispose();
                SelectedTexture = new PlayerTexture(MakePos);
                MakingNum = 10;
                UILayer.AddObject(SelectedTexture);
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.W) == asd.KeyState.Push)
            {//goal
                SelectedTexture.Dispose();
                SelectedTexture = new Goal(MakePos);
                MakingNum = 11;
                UILayer.AddObject(SelectedTexture);
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.E) == asd.KeyState.Push)
            {//warp
                SelectedTexture.Dispose();
                SelectedTexture = new WarpTexture(MakePos);
                MakingNum = 12;
                UILayer.AddObject(SelectedTexture);
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.R) == asd.KeyState.Push)
            {//warp編集Mode
                ModeText.Text = "ワープ元の位置選択";
                SelectedTexture.Position = new asd.Vector2DF(-300, -300);//画面外で消した感じを出す
                IsWarpSetting = true;
            }
            else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Backspace) == asd.KeyState.Push)
            {//disposeMode
                SelectedTexture.Dispose();
                MakingNum = 100;
            }
        }
        /// <summary>
        /// 変更するときChangeNumも変える
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        private int EnemyToNum<Type>(Type type)
            where Type : asd.Object2D
        {
            if (type is EnemyRed && !(type is EnemyWhite) && !(type is EnemyYellow))
                return 0;
            else if (type is EnemyWhite)
                return 1;
            else if (type is EnemyYellow)
                return 2;
            else if (type is Wall && !(type is WhiteWall) && !(type is YellowWall))
                return 3;
            else if (type is WhiteWall)
                return 4;
            else if (type is YellowWall)
                return 5;
            else if (type is UpArrow)
                return 6;
            else if (type is RightArrow)
                return 7;
            else if (type is DownArrow)
                return 8;
            else if (type is LeftArrow)
                return 9;
            else if (type is PlayerTexture)
                return 10;
            else if (type is Goal)
                return 11;
            else if (type is WarpZone)
                return 12;
            else
                return -1;
        }
    }
}

