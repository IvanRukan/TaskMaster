using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ThingsToDo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GitHubLink(object sender, EventArgs e)
        {
            Uri uri = new Uri("https://github.com/IvanRukan?tab=repositories");
            await Browser.OpenAsync(uri);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void NewGroup(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new GroupCreation());
        }
    }
}
