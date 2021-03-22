using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Controller.Helpers
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task PopAsync();
        Task DisplayAlert(string title, string msg, string cancel);
        Task<bool> DisplayAlert(string title, string msg, string yes, string no);
        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
        Task OpenAsync(string uri);
        Task PopToRootAsync();
        Task<string> DisplayPromptAsync(string title, string msg);
    }
}
