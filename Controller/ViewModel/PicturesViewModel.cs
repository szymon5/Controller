using Controller.Helpers;
using Controller.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Controller.ViewModel
{
    public class PicturesViewModel : INotifyPropertyChanged
    {
        private JPGPicture jPGPicture;
        private Image _selectedPicture;
        public ObservableCollection<MyImages> Pictures { get; set; } = new ObservableCollection<MyImages>();

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand GetPicturesCommand { get; private set; }
        public ICommand SelectedPictureCommand { get; private set; }
        public ICommand SaveToMemoryCommand { get; private set; }
        public ICommand DeletePictureCommand { get; private set; }
        public IPageService pageService { get; set; }

        public Image SelectedPicture
        {
            get
            {
                return _selectedPicture;
            }
            set
            {
                if (_selectedPicture == value) return;

                _selectedPicture = value;

                OnPropertyChanged();
            }
        }

        public PicturesViewModel(IPageService pageService)
        {
            this.pageService = pageService;
            this.jPGPicture = new JPGPicture();
            GetPicturesCommand = new Command(GetPictures);
            SelectedPictureCommand = new Command<MyImages>((img) => PictureSelected(img));
            SaveToMemoryCommand = new Command<MyImages>((img) => SaveToMemory(img));
            DeletePictureCommand = new Command<MyImages>((img) => DeletePicture(img));
        }

        private void GetPictures()
        {
            for (int i = 0; i < JPGPicture.ImagesString.Count; i++)
            {
                jPGPicture.Parse(JPGPicture.ImagesString[i]);
                MyImages myImages = new MyImages();
                var stream = new MemoryStream(jPGPicture.ByteArrayListOfImages[i], 0, jPGPicture.ByteArrayListOfImages[i].Length);
                myImages.Picture = ImageSource.FromStream(() => stream);
                Pictures.Add(myImages);
            }
        }

        private void PictureSelected(MyImages myImages)
        {
            if (myImages == null) return;
            SelectedPicture = null;
        }

        private void SaveToMemory(MyImages myImages)
        {

        }

        private async void DeletePicture(MyImages myImages)
        {
            bool decision = await pageService.DisplayAlert("Are you sure to delete this picture?", null, "Yes", "No");

            if (decision) Pictures.Remove(myImages);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
