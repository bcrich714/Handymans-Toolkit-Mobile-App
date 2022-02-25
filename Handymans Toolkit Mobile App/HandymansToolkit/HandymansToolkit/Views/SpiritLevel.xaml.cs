using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace HandymansToolkit.Views
{

    public partial class SpiritLevel : ContentPage
    {
        //Variable initializers
        private SensorSpeed speed = SensorSpeed.UI;
        private double Angle1, Angle2, DegreesOut, MinutesOut, SecondsOut;
        private bool displayXY = true, displayYZ = false;

        //Binding Text for display and button
        private string buttonText;
        private string angleValue;
        public string AngleValue
        {
            get { return angleValue; }
            set
            {
                angleValue = value;
                OnPropertyChanged(nameof(AngleValue));
            }
        }

        public string ButtonText
        {
            get { return buttonText; }
            set
            {
                buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }


        //Constructor/Initializer
        public SpiritLevel()
        {
            InitializeComponent();
            ButtonText = "Start";
            AngleValue = "0 °  ";
            BindingContext = this;
        }



        //Whenever the accelerometer readings change, update the reading on the display
        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            //Gets the readings from the accelerometer and sets it to data
            var data = e.Reading;


            //Check to see which 2 accelerometer values are needed
            //XY for Rotating the phone left and right
            //YZ for Rotating the phone towards and away from the user
            if (displayXY)
            {
                Angle1 = data.Acceleration.X;
                Angle2 = data.Acceleration.Y;
            }
            if (displayYZ)
            {
                Angle1 = data.Acceleration.Y;
                Angle2 = data.Acceleration.Z;
            }

            //Degrees
            //The 0.0001 at the end is to get around some weird readings, where sometimes the degree will be one less than expected and will have 60' instead
            double PureAngle = Convert.ToSingle(Math.Atan(Math.Abs(Angle1) / Math.Abs(Angle2)) * 180 / Math.PI) + 0.0001;

            //Degree option checked
            if (radioDegrees.IsChecked)
            {
                DegreesOut = Math.Round(Math.Abs(PureAngle));
                AngleValue = DegreesOut.ToString() + " °  ";
            }
            //Degree/Minute option checked
            else if (radioMinutes.IsChecked)
            {
                DegreesOut = Math.Floor(Math.Abs(PureAngle));
                MinutesOut = Math.Round((PureAngle - DegreesOut) * 60);
                AngleValue = DegreesOut.ToString() + " °  " + MinutesOut + " '  ";
            }
            else
            //Degree/Minute/Second option checked
            {
                DegreesOut = Math.Floor(Math.Abs(PureAngle));
                MinutesOut = Math.Floor((PureAngle - DegreesOut) * 60);
                SecondsOut = Math.Round(((PureAngle - DegreesOut) * 60 - MinutesOut) * 60);
                AngleValue = DegreesOut.ToString() + " °  " + MinutesOut + " '  " + SecondsOut + " ''";
            }
        }

        //Starts and stops the accelerometer
        //Also updates the display of the button
        private void ButtonStart_Clicked(object sender, EventArgs e)
        {
            if (Accelerometer.IsMonitoring)
            {
                ButtonText = "Start";
                Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
                Accelerometer.Stop();
            }
            else
            {
                ButtonText = "Stop";
                Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
                Accelerometer.Start(speed);
            }
        }

        //When a switch is checked, this prevents the code from re-running
        //Due to other states peing switched.
        //Also stops the accelerometer
        private bool hasLooped = false;
        void OnColorsRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            displayXY = XY.IsChecked;
            displayYZ = YZ.IsChecked;
            ButtonText = "Start";
            Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
            Accelerometer.Stop();
        }


        //Turns off the accelerometers when the page is left,
        //Removing unnecessary processes
        protected override void OnDisappearing()
        {
            ButtonText = "Start";
            Accelerometer.Stop();
        }

    }
}