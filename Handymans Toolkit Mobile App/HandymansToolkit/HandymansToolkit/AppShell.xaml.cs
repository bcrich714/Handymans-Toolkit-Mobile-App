using HandymansToolkit.ViewModels;
using HandymansToolkit.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HandymansToolkit
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SpiritLevel), typeof(SpiritLevel));
            Routing.RegisterRoute(nameof(FloorPlanner), typeof(FloorPlanner));
            Routing.RegisterRoute(nameof(Torch), typeof(Torch));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
