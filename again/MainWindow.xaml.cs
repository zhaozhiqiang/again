using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace again
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool MousePressed = false;

        private string MusicFileDir = "";

        DispatcherTimer Timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            play();
        }

        private void play()
        {
            if (setMusicSource())
            {
                player.Play();
                timerStart();
            }
        }

        void timerStart()
        {
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = TimeSpan.FromMilliseconds(1000);
            Timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (!MousePressed)
            {
                slider.Value = player.Position.TotalSeconds;
            }
        }

        private void initSlider()
        {
            slider.Maximum = player.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private bool isFileExist()
        {
            if (File.Exists(MusicFileDir))
            {
                return true;
            }

            return false;
        }

        private bool setMusicSource()
        {
            if (isFileExist())
            {
                player.Source = new Uri(MusicFileDir);

                return true;
            }

            return false;
        }

        private void player_MediaOpened(object sender, RoutedEventArgs e)
        {
            initSlider();
        }

        private void slider_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MousePressed = true;
        }

        private void slider_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MousePressed = false;

            player.Position = TimeSpan.FromSeconds(Math.Round(slider.Value));
            player.Play();
        }
    }
}
