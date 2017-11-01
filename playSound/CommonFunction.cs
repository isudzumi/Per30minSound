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
        public static DateTime runningTime { get; set; } = new DateTime().AddMilliseconds(SLEEP_TIME);
        public static DateTime StartTime { get; set; } = DateTime.Now;
        public static string FileName { get; set; } = "";
        public static Timer Timer = new Timer(new TimerCallback(Timer_Tick));
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

        private static void Timer_Tick(object sender)
        {
            var elapsedTime = DateTime.Now.Subtract(StartTime);
            var restTime = runningTime.Subtract(elapsedTime);
            var bind = BindData.GetBindDataInstance;
            bind.TxtTimer = restTime.ToString(@"mm\:ss");
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
                    Timer.Change(0, 1000);
                    StartTime = DateTime.Now;
                    if (i < 2)
                    {
                        Thread.Sleep(SLEEP_TIME);
                    }
                });
            }
        }
    }
}
