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
using System.ComponentModel;

namespace playSound
{
    using System.Windows;
    using System.IO;
    using Microsoft.Win32;
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        BindData bind = new BindData();

        public MainWindow()
        {
            InitializeComponent();

            mainGrid.DataContext = bind;

            this.StateChanged += new EventHandler(playSound_StateChanged);
        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "WAVファイル|*.wav";
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                bind.FileName = dialog.FileName;
            }
        }

        private void setButton_Click(object sender, RoutedEventArgs e)
        {
            if (CommonFunction.FileName != "")
            {
                bind.controlAudioFile();
                
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

        private void menuQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void menuInfo_Click(object sender, RoutedEventArgs e)
        {
            var infoDialog = VersionInfoDialog._instance;
            if (infoDialog == null || !infoDialog.IsLoaded)
            {
                var dialog = VersionInfoDialog.GetInstance;
                dialog.Show();
            }
        }
    }

    public class BindData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private const string onPLAY = "再生";
        private const string offPLAY = "実行中";
        private void OnPropertyChanged(String text)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(text));
            }
        }

        public string FileName
        {
            get { return CommonFunction.FileName; }
            set
            {
                CommonFunction.FileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        private string _status = CommonFunction.playSound == null ? onPLAY : offPLAY;
        public string Status
        {
            get { return this._status; }
            set
            {
                this._status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private Brush _fontColor = CommonFunction.playSound == null ? Brushes.Black : Brushes.Red;
        public Brush FontColor
        {
            get { return this._fontColor; }
            set
            {
                this._fontColor = value;
                OnPropertyChanged(nameof(FontColor));
            }
        }

        public void controlAudioFile()
        {
            var isPlay = CommonFunction.playSound;
            if(isPlay == null)
            {
                Status = offPLAY;
                FontColor = Brushes.Red;
                Task.Run(() => {
                    var task = CommonFunction.PlayAudioAsync();
                    if(task.IsCompleted)
                    {
                        Status = onPLAY;
                        FontColor = Brushes.Black;
                    }
                });
            }
            else
            {
                CommonFunction.StopAudioFile();
                Status = onPLAY;
                FontColor = Brushes.Black;
            }
        }
    }

}
