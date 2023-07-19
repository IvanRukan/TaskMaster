using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThingsToDo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupDeletion : ContentPage
    {
        public GroupDeletion()
        {
            InitializeComponent();
        }
        Picker groups = new Picker();
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CancellingButton.WidthRequest = 70;
            CancellingButton.HeightRequest = 70;
            ConfirmingButton.Source = "DisabledSave.png";
            ConfirmingButton.IsEnabled = false;
            groups.SelectedItem = null;
            List<Group> resulting_groups = App.Db.GetGroups();
            if (resulting_groups?.Any() != true) 
            {
                return;
            }
            List<string> group_names = new List<string>();
            for(int i = 0; i < resulting_groups?.Count; i++)
            {
                group_names.Add(resulting_groups[i].Name);
            }
            
            groups.ItemsSource = group_names;
            groups.Title = "Выберите группу из списка:";
            groups.TitleColor = Color.FromHex("#001C30");
            groups.TextColor = Color.FromHex("#001C30");
            groups.BackgroundColor = Color.FromHex("#DAFFFB");
            groups.FontFamily = "JostRegular";
            groups.FontSize = 30;
            PickerLayout.Children.Clear();
            PickerLayout.Children.Add(groups);
            groups.SelectedIndexChanged += Groups_SelectedIndexChanged;

        }

        private void Groups_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                ConfirmingButton.Source = "Save.png";
                ConfirmingButton.IsEnabled = true;
            
            
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

        private async void ConfirmingButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                List<Group> resulting_groups = App.Db.GetGroups();
                string group_to_delete = groups.SelectedItem.ToString();
                for(int i = 0; i < resulting_groups.Count; i++)
                {
                    if (resulting_groups[i].Name == group_to_delete)
                    {
                        Group needed_group = resulting_groups[i];
                        App.Db.DeleteGroup(needed_group);
                        
                        
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
    }
}