using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace playSound
{
    public static class CommonFunction
    {
        private const Int32 SLEEP_TIME = 1800000;
        private static BindData bind;
        public static DateTime StartTime { get; set; } = new DateTime().AddMilliseconds(SLEEP_TIME);
        private static DateTime RestTime { get; set; } = StartTime;
        public static Timer Timer = new Timer(new TimerCallback(Timer_Tick));

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

                RestTime = StartTime;
                bind = BindData.GetBindDataInstance;
                Timer.Change(0, 1000);
            }
        }

        private static void Timer_Tick(object sender)
        {
            RestTime = RestTime.AddSeconds(-1);
            bind.TxtTimer = RestTime.ToString(@"mm\:ss");
        }

        public static void StopAudioFile()
        {
            if (playSound != null)
            {
                playSound.Stop();
                playSound.Dispose();
                playSound = null;

                Timer.Change(Timeout.Infinite, Timeout.Infinite);
                RestTime = StartTime;
                bind.TxtTimer = StartTime.ToString(@"mm\:ss");
            }
        }

        public static async Task PlayAudioAsync()
        {
            LoadWavFile();
            for (int i = 0; i < 3; i++)
            {
                await Task.Run(() => {
                    PlayAudioFile();
                    if (i < 3)
                    {
                        Thread.Sleep(SLEEP_TIME);
                    }
                });
            }
        }
    }
}
