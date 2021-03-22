using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Xamarin.Forms;
using Controller.Helpers;
using Controller.ViewModel;

namespace Controller
{
    public partial class ConnectionPage : ContentPage
    {
        ConnectionPageViewModel ConnectionPageViewModel
        {
            get { return BindingContext as ConnectionPageViewModel; }
            set { BindingContext = this; }
        }

        public ConnectionPage()
        {
            BindingContext = new ConnectionPageViewModel(new PageService());
            InitializeComponent();
        }
        

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            ConnectionPageViewModel.ConnectCommand.Execute(IP_addr.Text + "*" + PortNumber.Text);
        }
    }
}
