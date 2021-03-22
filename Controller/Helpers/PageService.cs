using System.Threading.Tasks;
using Xamarin.Forms;

namespace Controller.Helpers
{
    public class PageService : IPageService
    {
        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return await Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }

        public async Task DisplayAlert(string title, string msg, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, msg, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string msg, string yes, string no)
        {
            return await Application.Current.MainPage.DisplayAlert(title, msg, yes, no);
        }

        public async Task<string> DisplayPromptAsync(string title, string msg)
        {
            return await Application.Current.MainPage.DisplayPromptAsync(title, msg);
        }

        public async Task OpenAsync(string uri)
        {
            await Xamarin.Essentials.Launcher.OpenAsync(uri);
        }

        public async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task PopToRootAsync()
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
