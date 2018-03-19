using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DeskFit
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        DispatcherTimer timer;

        TimeSpan breakTime = new TimeSpan(0, 0, 5);

        TimeSpan timeLeft;

        public MainPage()
        {
            this.InitializeComponent();

            timeLeft = breakTime;

            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        
        private void Timer_Tick(object sender, object e)
        {
            timeLeft = timeLeft.Subtract(new TimeSpan(0, 0, 1));
            header.Text = timeLeft.ToString(@"mm\:ss");

            if (timeLeft <= new TimeSpan(0, 0, 0))
            {
                Debug.WriteLine("Time is Up!");
                //mainGrid.Background = Application.Current.Resources["ApplicationPageBackgroundThemeBrush"] as Brush;
                timer.Stop();
            }
        }

        private void mainGrid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Debug.WriteLine("Press");
            }
        }
    }
}
