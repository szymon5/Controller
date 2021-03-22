using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Controller.Helpers;
using Controller.ViewModel;
using Xamarin.Forms;

namespace Controller
{
    public partial class ChatPage : ContentPage
    {
        ChatPageViewModel ChatPageViewModel
        {
            get { return BindingContext as ChatPageViewModel; }
            set { BindingContext = this; }
        }

        public ChatPage()
        {
            BindingContext = new ChatPageViewModel(new PageService());
            InitializeComponent();
        }

        void SendMessage_Clicked(System.Object sender, System.EventArgs e)
        {
            ChatPageViewModel.SendMessageCommand.Execute(Message.Text);
            
            if (ChatPageViewModel.isPictureExpected)
            {
                ChatPageViewModel.SetImage(ReceivedImage);
            }
        }
    }
}
