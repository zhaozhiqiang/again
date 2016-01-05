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
        private string MusicFileDir = "";

        private int REFERSH_SLIDER_INTERVAL = 100;

        private int RESET_PLAYER_MIN_INTERVAL = 200;

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
                //LoadedBehavior must be "Manual" first.
                player.Play();
                timerStart();
            }
        }

        void timerStart()
        {
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = TimeSpan.FromMilliseconds(REFERSH_SLIDER_INTERVAL);
            Timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            slider.Value = player.Position.TotalSeconds;
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

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Math.Abs(e.NewValue * 1000 - player.Position.TotalMilliseconds) > RESET_PLAYER_MIN_INTERVAL)
            {
                player.Position = TimeSpan.FromSeconds(Math.Round(slider.Value));
            }
        }
    }
}
