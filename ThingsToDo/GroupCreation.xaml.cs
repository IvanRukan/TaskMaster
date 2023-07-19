using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThingsToDo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupCreation : ContentPage
    {
        public GroupCreation()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        
        {
            base.OnAppearing();
            CancellingButton.WidthRequest = 70;
            CancellingButton.HeightRequest = 70;
            SavingButton.IsEnabled = false;
            SavingButton.Source = "DisabledSave.png";
            
            
        }
        private async void CancellingButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                CancellingButton.WidthRequest = 50;
                CancellingButton.HeightRequest = 50;
                await Navigation.PopModalAsync();
            }
            catch
            {
                return;
            }
        }

        private async void SavingButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Group group = new Group
                {
                    Name = GroupName.Text,
                };
                
                App.Db.SaveGroup(group);
                await Navigation.PopModalAsync();
                
            }
            catch
            {
                return;
            }
            
            
        }

        private void GroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(GroupName.Text) && GroupName.Text != "Введите название группы")
            {
                SavingButton.IsEnabled = true;
                SavingButton.Source = "Save.png";
            }
        }
    }
}