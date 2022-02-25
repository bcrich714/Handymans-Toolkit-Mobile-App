using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HandymansToolkit.Models;
using HandymansToolkit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandymansToolkit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskPlanner : ContentPage
    {
        //Constructor/Initializer
        public TaskPlanner()
        {
            InitializeComponent();
        }

        //When a task is selected (tap), Display an alert with its Name and Status
        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var task = ((ListView)sender).SelectedItem as Task1;
            if (task == null) return;

            await DisplayAlert(task.TaskName, task.Status, "OK");
        }

        //Stops task from being tapped. Both this command and the above run,
        //So having this one do something as well was redundant.
        //This also ran when un-selecting, which was not what we wanted either
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        //Marks an item as complete (NOT IMPLEMENTED IN CURRENT ITERATION DUE TO TIME)
        private void MarkCompleted(object sender, EventArgs e)
        {
            var task = ((MenuItem)sender).BindingContext as Task1;
            if (task == null) return;

            task.Status = "Complete";
        }
    }
}