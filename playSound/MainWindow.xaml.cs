using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace playSound
{
    using System.Windows;
    using Microsoft.Win32;
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Media.SoundPlayer play = null;

        public MainWindow()
        {
            InitializeComponent();
            this.StateChanged += new EventHandler(playSound_StateChanged);
        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Wavファイル(.wav)|*.wav";
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                fileName.Text = dialog.FileName;
            }
        }

        private void setButton_Click(object sender, RoutedEventArgs e)
        {
            String file = fileName.Text;
            if(file != "")
            {
                playAudioFile(file);
            } else
            {
                MessageBox.Show("ファイルを指定して下さい");
            }
            
        }

        private void playAudioFile(String file)
        {
            play = new System.Media.SoundPlayer(file);
            play.Play();
        }

        private void stopAudioFile()
        {
            if(play != null)
            {
                play.Stop();
                play.Dispose();
                play = null;
            }
        }

        private void playSound_StateChanged(object sender, EventArgs e)
        {
            if(this.WindowState == WindowState.Minimized)
            {
                this.Hide();
            }
        }
    }
}
