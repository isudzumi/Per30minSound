using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace playSound
{
    public static class CommonFunction
    {
        private const Int32 SLEEP_TIME = 1800000;
        public static DateTime playTime { get; set; } = DateTime.Now;
        public static string FileName { get; set; } = "";
        public static System.Media.SoundPlayer playSound = null;

        private static void LoadWavFile()
        {
            if (FileName == "")
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                var dir = new DirectoryInfo(currentDirectory);
                var wavFiles = dir.EnumerateFiles("*.wav", SearchOption.AllDirectories);
                var wavFile = wavFiles.First();
                FileName = wavFile.FullName;
            }
        }

        public static void PlayAudioFile()
        {
            if (FileName != "")
            {
                playSound = new System.Media.SoundPlayer(FileName);
                playSound.Play();
            }
        }

        public static void StopAudioFile()
        {
            if (playSound != null)
            {
                playSound.Stop();
                playSound.Dispose();
                playSound = null;
            }
        }

        public static async Task PlayAudioAsync()
        {
            LoadWavFile();
            for (int i = 0; i < 3; i++)
            {
                await Task.Run(() => {
                    PlayAudioFile();
                    if (i < 2)
                    {
                        Thread.Sleep(SLEEP_TIME);
                    }
                });
            }
        }
    }
}
