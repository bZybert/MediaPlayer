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
using System.Windows.Threading;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // creating timer
        DispatcherTimer Timer;  

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            MediaScreen.Play();

            if (Timer != null)
                Timer.Start();
        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            MediaScreen.Pause();

            if (Timer != null)
                Timer.Stop();
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            MediaScreen.Stop();

            if (Timer != null)
                Timer.Stop();

        }

        private void MediaScreen_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
           // MessageBox.Show(e.ErrorException.Message);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MediaScreen.ScrubbingEnabled = true;

            MediaScreen.Stop();
        }

       
        private void MediaScreen_MediaOpened(object sender, RoutedEventArgs e)
        {
            // getting media file length value in seconds and assign it to track time slider
            TrackTime.Maximum = MediaScreen.NaturalDuration.TimeSpan.TotalSeconds;

            //set slider value with timer
            Timer = new DispatcherTimer();
            // set interval tick for 1 second
            Timer.Interval = TimeSpan.FromSeconds(1);

            // for every second slider with create a new position
            Timer.Tick += Timer_Tick;

            // getting clip length and asign it to label in proper format
            TotalTrackTime.Content = MediaScreen.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            // track time slider will have current progress of movie  
            TrackTime.Value = MediaScreen.Position.TotalSeconds;
        }

        private void TrackTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //TimeSpan convert double tracktime.value to int .position
            MediaScreen.Position = TimeSpan.FromSeconds(TrackTime.Value);
        }


        //event for track time slider button
        private void TrackTime_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            //pause a media file when manully change slider position
            MediaScreen.Pause();

            if (Timer != null)
                Timer.Stop();
        }

        private void TrackTime_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            //resume play when slider is in position
            MediaScreen.Play();
            Timer.Start();
        }

        private void ProgressBar_AccessKeyPressed(object sender, AccessKeyPressedEventArgs e)
        {

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog;

            openFileDialog = new OpenFileDialog();
            openFileDialog.AddExtension = true;
            openFileDialog.DefaultExt = "";
            openFileDialog.Filter = "Media Files (*.*)|*.*";
            openFileDialog.ShowDialog();

            try
            {
                MediaScreen.Source = new Uri(openFileDialog.FileName);
            }
            catch
            {
                new NullReferenceException("Error");
            }
        }
    }
}
