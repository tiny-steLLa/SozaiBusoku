using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class WarpZone : asd.TextureObject2D
    {
        public asd.Vector2DF WarpPos { get; private set; }
        public int InstanceID { get; private set; }
        public int WarpID { get; private set; }//ワープ先のInstanceID
        private static asd.Color Black = new asd.Color(0, 0, 0, 255);
        protected static float Adjust = 0.33f;
        private GeneralText WarpIDText;
        private GeneralText InstanceIDText;
        public static List<WarpZone> WarpList { get; private set; }
        static WarpZone()
        {
            //staticりストの初期化
            WarpList = new List<WarpZone>();
        }

        public WarpZone(asd.Vector2DF pos, asd.Vector2DF warpPos)
        {
            InstanceID = GenerateID();
            WarpID = InstanceID;
            WarpPos = warpPos;
            Position = pos;
            Texture = asd.Engine.Graphics.CreateTexture2D("WarpZone.png");
            Color = new asd.Color(255, 255, 255, 90);//透明感出したい
            Scale = 1.2f * new asd.Vector2DF((float)Program.CellLarge / (float)Texture.Size.X, (float)Program.CellLarge / (float)Texture.Size.Y);
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2, Texture.Size.Y / 2);
            InstanceIDText = new GeneralText(Position - Adjust * new asd.Vector2DF(Program.CellLarge, Program.CellLarge), InstanceID.ToString(), Program.CellLarge / 2, Black, 0, Black);
            AddDrawnChild(InstanceIDText, asd.ChildManagementMode.RegistrationToLayer|asd.ChildManagementMode.Disposal, asd.ChildTransformingMode.Nothing, asd.ChildDrawingMode.Nothing);
            WarpIDText = new GeneralText(Position + Adjust * new asd.Vector2DF(0, Program.CellLarge), "→" + InstanceID.ToString(), Program.CellLarge / 2, Black, 0, Black);
            AddDrawnChild(WarpIDText, asd.ChildManagementMode.RegistrationToLayer | asd.ChildManagementMode.Disposal, asd.ChildTransformingMode.Nothing, asd.ChildDrawingMode.Nothing);
        }

        /// <summary>
        /// warpPotionの変化とかはこれを通して
        /// </summary>
        /// <param name="warp">ワープ先</param>
        public void ChangeWarpPos(WarpZone warp)
        {
            WarpPos = warp.Position;
            WarpID = warp.InstanceID;
            RemoveChild(WarpIDText);
            WarpIDText = new GeneralText(Position + Adjust * new asd.Vector2DF(0, Program.CellLarge), "→" +　warp.InstanceID.ToString(), Program.CellLarge / 2, Black, 0, Black);
            AddDrawnChild(WarpIDText, asd.ChildManagementMode.RegistrationToLayer|asd.ChildManagementMode.Disposal, asd.ChildTransformingMode.Nothing, asd.ChildDrawingMode.Nothing);
        }

        /// <summary>
        /// 1フレームの更新とかでidが正しく設定されないときに使用(ExcelStage)
        /// </summary>
        public static void ChangeAllID()
        {
            int t = 0;
            foreach(var warp in WarpList)
            {
                warp.InstanceID = t;
                warp.RemoveChild(warp.InstanceIDText);
                warp.InstanceIDText = new GeneralText(warp.Position - Adjust * new asd.Vector2DF(Program.CellLarge, Program.CellLarge), warp.InstanceID.ToString(), Program.CellLarge / 2, Black, 0, Black);
                warp.AddDrawnChild(warp.InstanceIDText, asd.ChildManagementMode.RegistrationToLayer | asd.ChildManagementMode.Disposal, asd.ChildTransformingMode.Nothing, asd.ChildDrawingMode.Nothing);
                t++;
            }
        }

        private int GenerateID()
        {
            //0から割り振る、なんだこのコードはわからない
            int t = 0;
            bool find = true;
            while (find)
            {
                find = false;
                foreach (var warp in WarpList)
                    if (warp.InstanceID == t)
                    {
                        find = true;
                        break;
                    }
                if (find == false)
                    break;
                t++;
            }
            return t;
        }

        protected override void OnAdded()
        {
            WarpList.Add(this);
            base.OnAdded();
        }

        protected override void OnDispose()
        {
            //消されたときワープ位置を参照しているオブジェクトのワープ位置を自分に変える
            foreach (var warp in WarpList)
                if (warp.WarpPos == Position && warp != this)
                    warp.ChangeWarpPos(warp);
            WarpList.Remove(this);
            base.OnDispose();
        }
    }
}
