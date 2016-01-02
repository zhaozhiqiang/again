using System;
using System.Windows;
using System.IO;

namespace again
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string MusicDir = "";

        private TimeSpan Position = new TimeSpan(0, 0, 0);

        public MainWindow()
        {
            InitializeComponent();

            play();
        }

        private bool isMusicExist()
        {
            if (File.Exists(MusicDir))
            {
                player.Source = new Uri(MusicDir);
                return true;
            }

            return false;
        }

        private void play()
        {
            if (isMusicExist())
            {
                try
                {
                    player.Position = Position;
                    player.Play();
                }
                catch (NotSupportedException)
                {
                    //do nothing.
                }
            }
        }
    }
}
