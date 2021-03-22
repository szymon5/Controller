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
    public partial class CameraSettings : ContentPage
    {
        CameraSettingsViewModel CameraSettingsViewModel
        {
            get { return BindingContext as CameraSettingsViewModel; }
            set { BindingContext = this; }
        }
        public CameraSettings()
        {
            BindingContext = new CameraSettingsViewModel(new PageService());
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CameraSettingsViewModel.SetLayoutSettings(cam, rec, stream);
        }

        private void CameraEnabled_Clicked(object sender, EventArgs e)
        {
            CameraSettingsViewModel.EnableCamera(cam, rec, stream);
        }

        private void StartRecording_Clicked(object sender, EventArgs e)
        {
            CameraSettingsViewModel.StartRecordingCommand.Execute(rec);
        }

        private void StartStreaming_Clicked(object sender, EventArgs e)
        {
            CameraSettingsViewModel.StartStreamingCommand.Execute(stream);
        }
    }
}