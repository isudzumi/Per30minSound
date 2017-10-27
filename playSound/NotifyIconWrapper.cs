using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playSound
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    public partial class NotifyIconWrapper : Component
    {
        public NotifyIconWrapper()
        {
            InitializeComponent();
            Task.Run(() => PlayAsync());
            
            this.toolStripMenuItem_Open.Click += this.toolStripMenuItem_Open_Click;
            this.toolStripMenuItem_Exit.Click += this.toolStripMenuItem_Exit_Click;
            this.toolStripMenuItem_VersionInfo.Click += this.toolStripMenuItem_VersionInfo_Click;
        }

        private async Task PlayAsync()
        {
            await CommonFunction.PlayAudioAsync();
            Environment.Exit(0);
        }

        public NotifyIconWrapper(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        private static MainWindow _instance = null;
        public static MainWindow StatusMainWindow
        {
            get
            {
                if (_instance == null || !_instance.IsLoaded)
                {
                    _instance = new MainWindow();
                }
                return _instance;
            }
        }

        private static VersionInfoDialog infoDialog;
        public static VersionInfoDialog GetInfoDialog
        {
            get
            {
                if (infoDialog == null || !infoDialog.IsLoaded)
                {
                    infoDialog = new VersionInfoDialog();
                }
                return infoDialog;
            }
        }

        private static void openMainWindow()
        {
            var window = StatusMainWindow;
            window.Show();
        }

        private void toolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            openMainWindow();
        }

        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            openMainWindow();
        }

        private void toolStripMenuItem_VersionInfo_Click(object sender, EventArgs e)
        {
            var dialog = GetInfoDialog;
            dialog.Show();
        }
    }
}
