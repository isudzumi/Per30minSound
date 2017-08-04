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
        public MainWindow()
        {
            InitializeComponent();

            DataContext = CommonFunction.FileName;

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
                CommonFunction.FileName = fileName.Text;
            }
        }

        private void setButton_Click(object sender, RoutedEventArgs e)
        {
            if (CommonFunction.FileName != "")
            {
                if(CommonFunction.playSound == null)
                {
                    playAudioFile();
                } else
                {
                    stopAudioFile();
                }
                
            } else
            {
                MessageBox.Show("ファイルを指定して下さい");
            }
            
        }

        private void playSound_StateChanged(object sender, EventArgs e)
        {
            if(this.WindowState == WindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void playAudioFile()
        {
            CommonFunction.playAudioFile();
            setButton.Content = "終了";
            setButton.Foreground = Brushes.Red;
        }

        private void stopAudioFile()
        {
            CommonFunction.stopAudioFile();
            setButton.Content = "再生";
            setButton.Foreground = Brushes.Black;
        }
    }
}
