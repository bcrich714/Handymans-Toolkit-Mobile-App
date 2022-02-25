using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace HandymansToolkit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Torch : ContentPage
    {
        //Variables
        bool isOn = false;
        private string buttonText;

        //Getter/Setter for button text
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
        public Torch()
        {
            ButtonText = "Flash OFF";
            InitializeComponent();
            BindingContext = this;
        }

        //Whenever the button is clicked, switch the state of the flashlight and change the button text
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                // Turn On/Off Flashlight, change button text for toggle effect.
                if (isOn)
                {
                    isOn = !isOn;
                    await Flashlight.TurnOffAsync();
                    ButtonText = "Flash OFF";
                }
                else
                {
                    isOn = !isOn;
                    await Flashlight.TurnOnAsync();
                    ButtonText = "Flash ON";
                }
            }
            catch (Exception)
            {

            }
        }
    }
}