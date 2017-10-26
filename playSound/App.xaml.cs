using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace playSound
{
    using System.Windows;
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        ///<summary>
        ///タスクトレイに表示するアイコン
        ///</summary>
        private NotifyIconWrapper notifyIcon;

        ///<summary>
        ///System.Windows.Application.Startupイベントを発生させる
        ///</summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            //多重起動チェック
            App._mutex = new Mutex(false, "playSound");
            if(!App._mutex.WaitOne(0, false))
            {
                App._mutex.Close();
                App._mutex = null;
                this.Shutdown();
                return;
            }

            base.OnStartup(e);
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            this.notifyIcon = new NotifyIconWrapper();
        }

        ///<summary>
        ///System.Windows.Application.Exitイベントを発生させる
        /// </summary>
        protected override void OnExit(ExitEventArgs e)
        {
            if (App._mutex == null)
            {
                return;
            }
            App._mutex.ReleaseMutex();
            App._mutex.Close();
            App._mutex = null;

            base.OnExit(e);
            this.notifyIcon.Dispose();
        }

        private static Mutex _mutex;
    }
}
