using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playSound
{
    public static class CommonFunction
    {
        private const Int32 SLEEP_TIME = 1800000;
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

        public static async Task<bool> playAudioAsync()
        {
            for (int i = 0; i < 3; i++)
            {
                await Task.Run(() => {
                    playAudioFile();
                    if (i < 2)
                    {
                        System.Threading.Thread.Sleep(SLEEP_TIME);
                    }
                });
            }
            return true;
        }
    }
}
