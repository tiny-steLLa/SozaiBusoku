using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    class VanishingText : GeneralText
    {
        protected byte FadeOutCount;
        protected byte FadeOutTime;

        /// <summary>
        /// 最後の引数が大きいと一瞬で消える
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="text"></param>
        /// <param name="large">フォント大きさ</param>
        /// <param name="mainColor"></param>
        /// <param name="aroundLarge">フォント周囲大きさ</param>
        /// <param name="aroundColor"></param>
        /// <param name="fadeOutTime">fadeoutの速さ</param>
        public VanishingText(asd.Vector2DF pos, String text, int large, asd.Color mainColor, int aroundLarge, asd.Color aroundColor, byte fadeOutTime)
            :base (pos,text,large,mainColor,aroundLarge,aroundColor)
        {
            FadeOutCount = 255;
            mainColor.A = 255;
            Color = mainColor;
            FadeOutTime = fadeOutTime;
        }
        protected override void OnUpdate()
        {
            if (FadeOutCount <= FadeOutTime)
                Dispose();
            FadeOutCount -= FadeOutTime;
            var mColor = Color;
            mColor.A = FadeOutCount;
            Color = mColor;
            base.OnUpdate();
        }
    }
}
