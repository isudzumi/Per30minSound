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

        private void toolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            var window = MainWindow.GetInstance();
            window.Show();
        }

        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var window = MainWindow.GetInstance();
            window.Show();
        }
    }
}
