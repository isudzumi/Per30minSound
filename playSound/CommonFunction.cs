using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playSound
{
    public static class CommonFunction
    {
        public static string FileName { get; set; } = "";
        public static System.Media.SoundPlayer playSound = null;

        public static void playAudioFile()
        {
            playSound = new System.Media.SoundPlayer(FileName);
            playSound.Play();
        }

        public static void stopAudioFile()
        {
            if (playSound != null)
            {
                playSound.Stop();
                playSound.Dispose();
                playSound = null;
            }
        }
    }
}
