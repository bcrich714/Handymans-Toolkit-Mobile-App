using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandymansToolkit.Views
{
    public partial class HomePage : ContentPage
    {
        //Constructor/Initializer
        public HomePage()
        {
            InitializeComponent();
        }

        //List of re-directions for the front-end buttons.
        private async void FlashlightClicked(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//Torch");
        }
        private async void SpiritLevelClicked(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//SpiritLevel");
        }

        private async void FloorPlannerClicked(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//FloorPlanner");
        }

        private async void TaskPlannerClicked(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//TaskPlanner");
        }

    }
}