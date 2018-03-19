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
    /// 

    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer;

        TimeSpan breakInterval = new TimeSpan(0, 30, 0);
        TimeSpan breakTime = new TimeSpan(0, 5, 0);

        TimeSpan timeLeft;

        // make 2 state variables so we can tell what we are doing
        String state = "countdown";
        String countdownType = "toBreak";

        // make it so that the state variable can be monitored for debugging
        public String State
        {
            get { return state; }
            set
            {
                state = value;
                Debug.WriteLine("State: " + state);
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            // reset the timer to the break time
            timeLeft = breakInterval;

            // create a new timer
            timer = new DispatcherTimer();
            // set the timer function
            timer.Tick += Timer_Tick;
            // set the interval of the timer to 1 second
            timer.Interval = new TimeSpan(0, 0, 1);
            // start the timer
            timer.Start();

            Progress.Maximum = breakInterval.TotalSeconds;
        }
        
        private void Timer_Tick(object sender, object e)
        {
            // take away 1 second from the time left
            timeLeft = timeLeft.Subtract(new TimeSpan(0, 0, 1));

            // if the time has not run out
            if (timeLeft >= new TimeSpan(0, 0, 0))
            {
                // display the time on the screen
                Header.Text = timeLeft.ToString(@"mm\:ss");

                // update the progress bar
                Progress.Value = timeLeft.TotalSeconds;
            }

            // if we are counting down to a break
            if (countdownType == "toBreak")
            {

                // check if the time has run out
                if (timeLeft < new TimeSpan(0, 0, 0))
                {
                    // change the text on the button
                    ActionButton.Content = "I'm Ready!";
                    
                    // flash the screen
                    // if the screen was red
                    if (State == "flash:red")
                    {
                        // change the background to black
                        MainGrid.Background = Application.Current.Resources["ApplicationPageBackgroundThemeBrush"] as Brush;
                        State = "flash:black";
                    }
                    // if the screen was not red (so black)
                    else
                    {
                        // change the background to red
                        MainGrid.Background = (SolidColorBrush)Resources["DangerRed"];
                        State = "flash:red";
                    }
                }
            }
            else if (countdownType == "breakCountdown")
            {
                // check if the time has run out
                if (timeLeft < new TimeSpan(0, 0, 0))
                {
                    EndBreak();
                }
            }
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (countdownType == "toBreak")
            {
                BeginNextBreak();
            } else if (countdownType == "breakCountdown")
            {
                EndBreak();
            }
        }

        void BeginNextBreak()
        {
            var exercises = new Exercises();

            SubHeader.Text = exercises.GetExercise();
            countdownType = "breakCountdown";
            timeLeft = breakTime;
            Progress.Maximum = breakTime.TotalSeconds;
            ActionButton.Content = "Finish";
            MainGrid.Background = (SolidColorBrush)Resources["ExerciseBlue"];
        }

        void EndBreak()
        {
            SubHeader.Text = "Time to Next Break";
            countdownType = "toBreak";
            timeLeft = breakInterval;
            Progress.Maximum = breakInterval.TotalSeconds;
            ActionButton.Content = "Break Now";
            MainGrid.Background = Application.Current.Resources["ApplicationPageBackgroundThemeBrush"] as Brush;
        }
    }
}
