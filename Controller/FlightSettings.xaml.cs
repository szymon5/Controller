using Controller.Helpers;
using Controller.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Controller
{
    public partial class FlightSettings : ContentPage
    {
        FlightSettingsViewModel FlightSettingsViewModel
        {
            get { return BindingContext as FlightSettingsViewModel; }
            set { BindingContext = this; }
        }
        public FlightSettings()
        {
            BindingContext = new FlightSettingsViewModel(new PageService());
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FlightSettingsViewModel.SetLayout(Buttons, Accelerometer);
        }
        private void Buttons_Clicked(object sender, EventArgs e)
        {
            FlightSettingsViewModel.ControllByButtons(Buttons, Accelerometer);
        }

        private void Accelerometer_Clicked(object sender, EventArgs e)
        {
            FlightSettingsViewModel.ControllByAccelerometer(Buttons, Accelerometer);
        }
    }
}