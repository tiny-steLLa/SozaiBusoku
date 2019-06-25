using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class Player : asd.TextureObject2D
    {
        public bool IsClear { get; private set; }
        private List<byte[]> PreStatus;//戻るときに使用,[CurrentAxis,壁破壊したか,？(0=破壊してない,1=黄色,2=白),敵が動いたか?(0,動いてない)],PositionX,Position.Yを記憶,2進数で最適化とか考えたり
        private byte CurrentAxis;// 0が上、１が右、2が下、3が左
        private int MoveSpeed;//holdで動くはやさ
        private int MoveCount;//ボタンを押している間増える
        private GeneralText MoveNum;//動いた総数を表示
        public Player(asd.Vector2DF pos)
        {
            IsClear = false;
            PreStatus = new List<byte[]>();
            CurrentAxis = 0;
            MoveSpeed = 8;
            MoveCount = 0;
            Texture = asd.Engine.Graphics.CreateTexture2D("PlayerUp.png");
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2, Texture.Size.Y / 2);
            Scale = new asd.Vector2DF((float)Program.CellLarge / (float)Texture.Size.X, (float)Program.CellLarge / (float)Texture.Size.Y);
            Color = new asd.Color(255, 255, 255, 100);//透明感出したい
            Position = pos;
        }
        protected override void OnAdded()
        {
            MoveNum = new GeneralText(new asd.Vector2DF(27 * Program.CellLarge, 1 * Program.CellLarge), "総移動数" + PreStatus.Count().ToString(), Program.CellLarge/2, new asd.Color(0,0,0,0), Program.CellLarge / 6, new asd.Color(255, 255, 255, 0));
            GeneralStage.TextLayer.AddObject(MoveNum);
            base.OnAdded();
        }
        protected override void OnDispose()
        {
            base.OnDispose();
        }

        protected List<Type> MakeList<Type>()
            where Type : asd.Object2D
        {
            var list = new List<Type>();
            foreach (var obj in Layer.Objects)
            {
                if (!(obj is Type))
                    continue;
                list.Add(obj as Type);
            }
            return list;
        }
        protected bool WarpCheck()
        {
            var walls = MakeList<Wall>();
            var enemys = MakeList<EnemyRed>();
            foreach (var warp in WarpZone.WarpList)
                if (Position == warp.Position && !walls.Exists(w => w.Position == warp.WarpPos) && !enemys.Exists(e => e.Position == warp.WarpPos))
                {
                    Position = warp.WarpPos;
                    return true;
                }
            return false;
        }


        /// <summary>
        /// 数字から移動に変換、0が上、１が右、2が下、3が左、Axisも記憶
        /// </summary>
        /// <param name="n"></param>
        protected void AxisToMove(byte n)
        {
            if (n < 0 || n > 3)
                return;
            CurrentAxis = n;
            ChangeAngle();
            switch (n)
            {
                case 0:
                    Position = Position + new asd.Vector2DF(0, -Program.CellLarge);
                    break;
                case 1:
                    Position = Position + new asd.Vector2DF(+Program.CellLarge, 0);
                    break;
                case 2:
                    Position = Position + new asd.Vector2DF(0, +Program.CellLarge);
                    break;
                case 3:
                    Position = Position + new asd.Vector2DF(-Program.CellLarge, 0);
                    break;
            }
        }
        /// <summary>
        /// 一手戻るときに使う値を返す
        /// </summary>
        /// <typeparam name="WallType"></typeparam>
        /// <typeparam name="EnemyType"></typeparam>
        /// <param name="wall"></param>
        /// <param name="enemy"></param>
        /// <returns></returns>
        protected byte BreakWall<WallType, EnemyType>(WallType wall, EnemyType enemy)
            where WallType : asd.Object2D
            where EnemyType : asd.Object2D
        {
            wall.Dispose();
            enemy.Dispose();
            if (wall is YellowWall)
                return 1;
            else if (wall is WhiteWall)
                return 2;
            else
                return 0;
        }

        /// <summary>
        /// 一手戻す
        /// </summary>
        private void Backward(List<EnemyRed> enemys)
        {
            if (PreStatus.Count() == 0)
                return;
            byte[] currentByte = PreStatus[PreStatus.Count() - 1];

            Position = new asd.Vector2DF(currentByte[3] * Program.CellLarge + Program.CellLarge / 2, currentByte[4] * Program.CellLarge + Program.CellLarge / 2);
            if (currentByte[2] == 1)
            {
                switch (CurrentAxis)
                {
                    case 0:
                        var enemy0 = enemys.Find(enemy => enemy.Position.X == Position.X && enemy.Position.Y == Position.Y -2* Program.CellLarge);
                        if (enemy0 != null)
                            enemy0.Position += new asd.Vector2DF(0, Program.CellLarge);
                        break;
                    case 1:
                        var enemy1 = enemys.Find(enemy => enemy.Position.X == Position.X + 2*Program.CellLarge && enemy.Position.Y == Position.Y);
                        if (enemy1 != null)
                            enemy1.Position += new asd.Vector2DF(-Program.CellLarge, 0);
                        break;
                    case 2:
                        var enemy2 = enemys.Find(enemy => enemy.Position.X == Position.X && enemy.Position.Y == Position.Y + 2*Program.CellLarge);
                        if (enemy2 != null)
                            enemy2.Position += new asd.Vector2DF(0, -Program.CellLarge);
                        break;
                    case 3:
                        var enemy3 = enemys.Find(enemy => enemy.Position.X == Position.X -2* Program.CellLarge && enemy.Position.Y == Position.Y);
                        if (enemy3 != null)
                            enemy3.Position += new asd.Vector2DF(Program.CellLarge, 0);
                        break;
                }
            }
            //壁と敵の再生
            if (currentByte[1] == 1)
            {
                YellowWall yellowWall = new YellowWall(new asd.Vector2DF());//位置はメソッド内で
                EnemyYellow enemyYellow = new EnemyYellow(new asd.Vector2DF());
                RemakeWall(CurrentAxis, yellowWall, enemyYellow);
            }
            else if (currentByte[1] == 2)
            {
                WhiteWall whiteWall = new WhiteWall(new asd.Vector2DF());
                EnemyWhite enemyWhite = new EnemyWhite(new asd.Vector2DF());
                RemakeWall(CurrentAxis, whiteWall, enemyWhite);
            }
            CurrentAxis = currentByte[0];
            ChangeAngle();
            PreStatus.RemoveAt(PreStatus.Count() - 1);
        }

        /// <summary>
        /// ちゃんとCurrentAxisを変えてから呼ばないと不穏な動きをする
        /// </summary>
        protected void ChangeAngle()
        {
            switch(CurrentAxis)
            {
                case 0:
                    Angle = 0;
                    break;
                case 1:
                    Angle = 90;
                    break;
                case 2:
                    Angle = 180;
                    break;
                case 3:
                    Angle = 270;
                    break;
            }
        }
        //Backwardで壁再生
        protected void RemakeWall<TypeWall, TypeEnemy>(byte axis, TypeWall typeWall, TypeEnemy typeEnemy)
            where TypeWall : Wall
            where TypeEnemy : EnemyRed
        {
            switch (axis)
            {
                case 0:
                    typeWall.Position = new asd.Vector2DF(Position.X, Position.Y - 2 * Program.CellLarge);
                    typeEnemy.Position = new asd.Vector2DF(Position.X, Position.Y - Program.CellLarge);
                    break;
                case 1:
                    typeWall.Position = new asd.Vector2DF(Position.X + 2 * Program.CellLarge, Position.Y);
                    typeEnemy.Position = new asd.Vector2DF(Position.X + Program.CellLarge, Position.Y);
                    break;
                case 2:
                    typeWall.Position = new asd.Vector2DF(Position.X, Position.Y + 2 * Program.CellLarge);
                    typeEnemy.Position = new asd.Vector2DF(Position.X, Position.Y + Program.CellLarge);
                    break;
                case 3:
                    typeWall.Position = new asd.Vector2DF(Position.X - 2 * Program.CellLarge, Position.Y);
                    typeEnemy.Position = new asd.Vector2DF(Position.X - Program.CellLarge, Position.Y);
                    break;
            }
            Effect effect = new Effect(typeWall.Position);
            effect.Scale = new asd.Vector2DF(Program.CellLarge / 5, Program.CellLarge / 5);
            Layer.AddObject(effect);
            effect.Play();
            Layer.AddObject(typeWall);
            Layer.AddObject(typeEnemy);
        }
        /// <summary>
        /// 0が上、１が右、2が下、3が左
        /// </summary>
        /// <param name="axis"></param>
        protected void MoveCheck(byte axis)
        {
            var yellowEnemys = MakeList<EnemyYellow>();
            var yellowWalls = MakeList<YellowWall>();
            var whiteEnemys = MakeList<EnemyWhite>();
            var whiteWalls = MakeList<WhiteWall>();
            var walls = MakeList<Wall>();
            var enemys = MakeList<EnemyRed>();
            var goals = MakeList<Goal>();

            var arrowList = new List<Arrow>();//上に進むなら下、右なら左のArrowを全て入れてく
            bool canMove = true;
            byte isPushEnemy = 0;
            byte whichWallBroken = 0;
            asd.Vector2DF nearVector = new asd.Vector2DF();//1マスとなりのvector、switch内で割当
            asd.Vector2DF farVector = new asd.Vector2DF();//2マスとなりのvector、switch内で割当
            switch (axis)
            {
                case 0:
                    nearVector = Position + new asd.Vector2DF(0, -1 * Program.CellLarge);
                    farVector = Position + new asd.Vector2DF(0, -2 * Program.CellLarge);
                    foreach (var down in MakeList<DownArrow>())
                        arrowList.Add(down);
                    break;
                case 1:
                    nearVector = Position + new asd.Vector2DF(1 * Program.CellLarge, 0);
                    farVector = Position + new asd.Vector2DF(2 * Program.CellLarge, 0);
                    foreach (var left in MakeList<LeftArrow>())
                        arrowList.Add(left);
                    break;
                case 2:
                    nearVector = Position + new asd.Vector2DF(0, 1 * Program.CellLarge);
                    farVector = Position + new asd.Vector2DF(0, 2 * Program.CellLarge);
                    foreach (var up in MakeList<UpArrow>())
                        arrowList.Add(up);
                    break;
                case 3:
                    nearVector = Position + new asd.Vector2DF(-1 * Program.CellLarge, 0);
                    farVector = Position + new asd.Vector2DF(-2 * Program.CellLarge, 0);
                    foreach (var right in MakeList<RightArrow>())
                        arrowList.Add(right);
                    break;
            }
            var pushedEnemy = enemys.Find(enemy => enemy.Position == nearVector);
            var pushedEnemyWhite = whiteEnemys.Find(enemy => enemy.Position == nearVector);
            var whiteWall = whiteWalls.Find(wall => wall.Position == farVector);
            var pushedEnemyYellow = yellowEnemys.Find(enemy => enemy.Position == nearVector);
            var yellowWall = yellowWalls.Find(wall => wall.Position == farVector);

            //壁、下矢印が上にないときの状況で動く
            canMove = canMove && !walls.Exists(wall => wall.Position == nearVector);
            canMove = canMove && !arrowList.Exists(arrow => arrow.Position == nearVector);
            //PushedEnemyの上にenemy or wall or　下矢印がないとき動く。
            if (pushedEnemy != null)
            {
                canMove = canMove && !arrowList.Exists(arrow => arrow.Position == farVector);
                canMove = canMove && !walls.Exists(wall => wall.Position == farVector);
                canMove = canMove && !enemys.Exists(enemy => enemy.Position == farVector);
            }

            //同色壁と敵の位置が重なったら消す & canMove = trueに
            if (pushedEnemyWhite != null && whiteWall != null && !arrowList.Exists(arrow => arrow.Position == nearVector))
            {
                whichWallBroken = BreakWall(whiteWall, pushedEnemyWhite);
                canMove = true;
            }
            if (pushedEnemyYellow != null && yellowWall != null && !arrowList.Exists(arrow => arrow.Position == nearVector))
            {
                whichWallBroken = BreakWall(yellowWall, pushedEnemyYellow);
                canMove = true;
            }
            //動く時の処理
            if (canMove)
            {
                if (pushedEnemy != null)
                {
                    pushedEnemy.Position = farVector;
                    isPushEnemy = 1;
                }
                byte[] temp = { CurrentAxis, whichWallBroken, isPushEnemy, (byte)(Position.X / Program.CellLarge), (byte)(Position.Y / Program.CellLarge) };
                PreStatus.Add(temp);
                AxisToMove(axis);
                //ワープ処理
                WarpCheck();
            }
        }

        protected override void OnUpdate()
        {
            var enemys = MakeList<EnemyRed>();
            var goals = MakeList<Goal>();

            bool isPushing = false;
            if (!IsClear)
            {
                if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Backspace) == asd.KeyState.Hold||DualShockController.IsJoystickHold(1))
                {
                    isPushing = true;
                    if (MoveCount % (MoveSpeed / 2) == 0)
                        Backward(enemys);
                    MoveCount++;
                }

                if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Up) == asd.KeyState.Hold||DualShockController.IsJoystickHold(14))
                {
                    isPushing = true;
                    if (MoveCount % MoveSpeed == 0)
                        MoveCheck(0);//CurrentAxis更新位置
                    MoveCount++;
                }
                else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Right) == asd.KeyState.Hold||DualShockController.IsJoystickHold(15))
                {
                    isPushing = true;
                    if (MoveCount % MoveSpeed == 0)
                        MoveCheck(1);
                    MoveCount++;
                }
                else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Down) == asd.KeyState.Hold||DualShockController.IsJoystickHold(16))
                {
                    isPushing = true;
                    if (MoveCount % MoveSpeed == 0)
                        MoveCheck(2);
                    MoveCount++;
                }
                else if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Left) == asd.KeyState.Hold||DualShockController.IsJoystickHold(17))
                {
                    isPushing = true;
                    if (MoveCount % MoveSpeed == 0)
                        MoveCheck(3);
                    MoveCount++;
                }
            }

            if (!isPushing)
                MoveCount = 0;
            MoveNum.Text = "総移動数" + PreStatus.Count.ToString();
            foreach (var goal in goals)
            {
                if (Position == goal.Position && !IsClear)
                {
                    asd.Engine.ChangeSceneWithTransition(new ClearScene(PreStatus.Count), new asd.TransitionFade(1.0f, 1.0f));
                    IsClear = true;
                }
            }
        }
    }
}