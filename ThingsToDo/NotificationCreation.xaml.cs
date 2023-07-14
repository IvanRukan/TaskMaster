using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThingsToDo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationCreation : ContentPage
    {
        public NotificationCreation()
        {
            InitializeComponent();
        }
        
        protected override void OnAppearing()

        {
            SavingButton.IsEnabled = false;
            SavingButton.Source = "DisabledSave.png";
            CancellingButton.WidthRequest = 70;
            CancellingButton.HeightRequest = 70;
            List<Group> resulting_groups = App.Db.GetGroups();
            List<string> group_names = new List<string>();
            for (int i = 0; i < resulting_groups?.Count; i++)
            {
                group_names.Add(resulting_groups[i].Name);
            }

            GroupName.ItemsSource = group_names;
        }
        private async void CancellingButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                CancellingButton.WidthRequest = 50;
                CancellingButton.HeightRequest = 50;
                GroupName.SelectedItem = null;
                NotificationName.Text = "Введите название напоминания";
                await Navigation.PopModalAsync();
            }
            catch
            {
                return ;
            }
        }

        private async void SavingButton_Clicked(object sender, EventArgs e)
        {
            try { 
            List<Group> resulting_groups = App.Db.GetGroups();
            string group_to_delete = GroupName.SelectedItem.ToString();
                for (int i = 0; i < resulting_groups.Count; i++)
                {
                    if (resulting_groups[i].Name == group_to_delete)
                    {
                        Group needed_group = resulting_groups[i];
                        UserNotification notification = new UserNotification()

                        {
                            Name = NotificationName.Text,
                            Group = needed_group.Id,
                            Date = Date.Date.Add(Time.Time),

                        };
                        await DisplayAlert(Title = notification.Date.ToString(), "Alert", "Ok");
                        App.Db.SaveNotification(notification);
                        GroupName.SelectedItem = null;
                        NotificationName.Text = "Введите название напоминания";
                        break;
                    }
                }
                await Navigation.PopModalAsync();
            }
            catch
            {
                return;
            }

            

        }

       private void GroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(NotificationName.Text) && NotificationName.Text != "Введите название напоминания")
            {
                SavingButton.IsEnabled = true;
                SavingButton.Source = "Save.png";
            }
        } 

        private void NotificationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (GroupName.SelectedItem == null || string.IsNullOrEmpty(NotificationName.Text))
            {
                return;
            }
            else
            {
                SavingButton.IsEnabled = true;
                SavingButton.Source = "Save.png";
            }
        }
    }
}