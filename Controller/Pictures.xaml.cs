using Controller.Helpers;
using Controller.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Controller
{
    public partial class Pictures : ContentPage
    {
        private JPGPicture jPGPicture;
        PicturesViewModel PicturesViewModel
        {
            get { return BindingContext as PicturesViewModel; }
            set { BindingContext = this; }
        }
        public Pictures()
        {
            jPGPicture = new JPGPicture();
            BindingContext = new PicturesViewModel(new PageService());
            InitializeComponent();
        }
        public void test()
        {
            var scroll = new ScrollView();

            Button[] btn = new Button[20];

            for(int i=0;i<btn.Length;i++)
            {
                btn[i] = new Button() { Text = "btn " + i.ToString() };
                scroll.Content = btn[i];
            }
        }
        private void Images()
        {
            var stack = new StackLayout();
            int cnt = JPGPicture.ImagesString.Count;

            Image[] images = new Image[cnt];

            for (int i = 0; i < cnt; i++)
            {
                jPGPicture.Parse(JPGPicture.ImagesString[i]);
                var stream = new MemoryStream(jPGPicture.ByteArrayListOfImages[i], 0, jPGPicture.ByteArrayListOfImages[i].Count());
                images[i] = new Image()
                {
                    Source = ImageSource.FromStream(() => stream)
                };
                stack.Children.Add(images[i]);
            }
            Content = stack;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Images();
            if (PicturesViewModel.Pictures.Count > 0) PicturesViewModel.Pictures.Clear();

            PicturesViewModel.GetPicturesCommand.Execute(null);
        }

        private void DownloadedPictures_Refreshing(object sender, EventArgs e)
        {
            PicturesViewModel.Pictures.Clear();
            PicturesViewModel.GetPicturesCommand.Execute(null);
        }

        private void DownloadedPictures_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            PicturesViewModel.SelectedPictureCommand.Execute(e.SelectedItem);
        }
    }
}