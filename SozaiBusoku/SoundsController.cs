using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozaiBusoku
{
    public static class SoundsController
    {
        private static asd.SoundSource BGM;
        public static int BGM_id { get; private set; }

        public static void SoundBGM()
        {
            BGM = asd.Engine.Sound.CreateSoundSource("nyan.wav", false);
            BGM.IsLoopingMode = true;
            BGM.LoopStartingPoint = 27;
            BGM.LoopEndPoint = 60.103f;
            BGM_id = asd.Engine.Sound.Play(BGM);
            asd.Engine.Sound.SetVolume(BGM_id, 0.17f);
        }
        public static void StopMusic() => asd.Engine.Sound.Pause(BGM_id);
        public static void StartMusic() => asd.Engine.Sound.Resume(BGM_id);
    }
}
