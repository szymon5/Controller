using Controller.Helpers;
using Controller.ViewModel;
using Xamarin.Forms;

namespace Controller
{
    public partial class FlightControll : ContentPage
    {
        FlightControllViewModel FlightViewModel
        {
            get { return BindingContext as FlightControllViewModel; }
            set { BindingContext = this; }
        }
        public FlightControll()
        {
            BindingContext = new FlightControllViewModel(new PageService());
            InitializeComponent();
            Settings.SetSettings();
            //Settings.CameraSettings.isCameraEnabled = true;
            //Settings.CameraSettings.hasRecordingStarted = true;
            //Settings.CameraSettings.hasStreamingStarted = false;

            //Settings.FlightSettings.droneControlMode = true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FlightViewModel.SetLayoutSettings(ButtonsLayout, AccelerometerLayout);

            if (Settings.FlightSettings.droneControlMode) FlightViewModel.EnableAccelerometerCommand.Execute(null);
            else FlightViewModel.DisableAccelerometerCommand.Execute(null);
        }
    }
}
