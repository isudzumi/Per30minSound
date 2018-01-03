using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace playSound
{
    public static class CommonFunction
    {
        private const Int32 SLEEP_TIME = 1800000;
        private const int LOOP_COUNT = 3;
        private static BindData bind;
        public static DateTime StartTime { get; set; } = new DateTime().AddMilliseconds(SLEEP_TIME);
        private static DateTime RestTime { get; set; } = StartTime;
        public static Timer Timer = new Timer(new TimerCallback(Timer_Tick));

        public static string FileName { get; set; } = "";
        public static System.Media.SoundPlayer playSound = null;

        public static CancellationTokenSource cancellationToken = null;
        public static CancellationToken token;

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
                Timer.Change(0, 1000);
            }
        }

        private static void PlayAudioFileAsync()
        {
            if (FileName != "")
            {
                playSound = new System.Media.SoundPlayer(FileName);
                playSound.PlaySync();

                RestTime = StartTime;
                Timer.Change(0, 1000);
            }
        }

        private static void Timer_Tick(object sender)
        {
            if (!RestTime.Equals(DateTime.MinValue))
            {
                RestTime = RestTime.AddSeconds(-1);
                bind.TxtTimer = RestTime.ToString(@"mm\:ss");
            }
        }

        public static void StopAudioFile()
        {
            if ((playSound != null) && (cancellationToken != null))
            {
                playSound.Stop();
                playSound.Dispose();
                playSound = null;

                Timer.Change(Timeout.Infinite, Timeout.Infinite);
                RestTime = StartTime;
                bind.TxtTimer = StartTime.ToString(@"mm\:ss");

                cancellationToken.Cancel();
                cancellationToken.Dispose();
            }
        }

        public static async Task<int> PlayAudioAsync()
        {
            LoadWavFile();
            cancellationToken = new CancellationTokenSource();
            token = cancellationToken.Token;
            try
            {
                bind = BindData.GetBindDataInstance;
                for (int i = 0; i < LOOP_COUNT; i++)
                {
                    if (i != (LOOP_COUNT - 1))
                    {
                        await Task.Run(async () =>
                        {
                            PlayAudioFile();
                            await Task.Delay(SLEEP_TIME, token);
                        });
                    }
                    else
                    {
                        PlayAudioFileAsync();
                    }
                }
                return 0;
            }
            catch (TaskCanceledException e)
            {
                return 1;
            }
        }
    }
}
