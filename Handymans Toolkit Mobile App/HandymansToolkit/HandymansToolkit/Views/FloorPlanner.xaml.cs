using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;
using Xamarin.Forms.Xaml;

namespace HandymansToolkit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloorPlanner : ContentPage
    {
        //Initializers for variables (NOT USED)
        private double MinScale = 1;
        private double MaxScale = 4;

        //Constructor/Initializer
        public FloorPlanner()
        {
            InitializeComponent();
            Scale = MinScale;
        }

        //Asks for the user to select a floor plan image from their gallery to display on the app
        async void PickImage(Object sender, EventArgs e)
        {
            //Wait for the user to pick an image. if one is not selected, nothing happens. If one is selected,
            //The image is then set to the image tag on the front-end
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Pick a Photo to Import"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);
                Console.WriteLine("IMAGE SUCCESSFULLY INSERTED: " + resultImage.Source);
                stream = null;
            }
        }

        //Saves the Image back to gallery (Not Implemented)
        async void Button_Clicked1(Object sender, EventArgs e)
        {

        }

        //On Double tap, fullscreen image (Not Implemented)
        void OnDoubleTapped(object sender, EventArgs e)
        {
            /*
            if (Scale == MinScale)
            {
                this.ScaleTo(MaxScale, 250, Easing.CubicInOut);
                this.TranslateTo(0, 0, 250, Easing.CubicInOut);
            }
            else
            {
                this.ScaleTo(MinScale, 250, Easing.CubicInOut);
                this.TranslateTo(0, 0, 250, Easing.CubicInOut);
            }
            */
        }
    }
}