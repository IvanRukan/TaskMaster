using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LocalNotifications;

namespace ThingsToDo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClosestNotifications : ContentPage
    {
        public ClosestNotifications()
        {
            InitializeComponent();
        }
        private async void GitHubLink(object sender, EventArgs e)
        {

            Uri uri = new Uri("https://github.com/IvanRukan?tab=repositories");
            await Browser.OpenAsync(uri);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<UserNotification> existing = App.Db.GetNotifications();
            if(existing?.Any() != true)
            {
                await Navigation.PopModalAsync(animated: false);
            }
            NotificationCenter.Current.OnNotificationReceived += Current_OnNotificationReceived;
            main.Children.Clear();
            
            main.VerticalOptions = LayoutOptions.FillAndExpand;
            main.HorizontalOptions = LayoutOptions.FillAndExpand;
            StackLayout no_bd_new_layout = new StackLayout();
            no_bd_new_layout.BackgroundColor = Color.FromHex("#DAFFFB");
            Frame no_bd_top_frame = new Frame();
            no_bd_top_frame.BackgroundColor = Color.FromHex("#176B87");
            no_bd_top_frame.HasShadow = true;
            no_bd_top_frame.WidthRequest = 353;
            StackLayout no_bd_top_frame_buttons = new StackLayout();
            no_bd_top_frame_buttons.Orientation = StackOrientation.Horizontal;
            Button no_bd_recent_events = new Button();
            no_bd_recent_events.Text = "Ближайшие напоминания";
            no_bd_recent_events.TextColor = Color.FromHex("#DAFFFB");
            no_bd_recent_events.BackgroundColor = Color.FromHex("#176B87");
            no_bd_recent_events.FontFamily = "InterSemiBold";
            no_bd_recent_events.FontSize = 11;
            no_bd_recent_events.WidthRequest = 118;
            no_bd_recent_events.CornerRadius = 20;
            no_bd_recent_events.BorderColor = Color.FromHex("#DAFFFB");
            no_bd_recent_events.BorderWidth = 2;
            no_bd_top_frame_buttons.Children.Add(no_bd_recent_events);
            Button no_bd_all_events = new Button();
            no_bd_all_events.Text = "Все напоминания";
            no_bd_all_events.TextColor = Color.FromHex("#DAFFFB");
            no_bd_all_events.BackgroundColor = Color.FromHex("#176B87");
            no_bd_all_events.FontFamily = "InterRegular";
            no_bd_all_events.FontSize = 11;
            no_bd_all_events.WidthRequest = 118;
            no_bd_all_events.CornerRadius = 20;
            no_bd_all_events.Clicked += No_bd_all_events_Clicked;
            no_bd_top_frame_buttons.Children.Add(no_bd_all_events);
            Button no_bd_group_events = new Button();
            no_bd_group_events.Text = "Напоминания по группе";
            no_bd_group_events.TextColor = Color.FromHex("#DAFFFB");
            no_bd_group_events.BackgroundColor = Color.FromHex("#176B87");
            no_bd_group_events.FontFamily = "InterRegular";
            no_bd_group_events.FontSize = 11;
            no_bd_group_events.WidthRequest = 118;
            no_bd_group_events.CornerRadius = 20;
            no_bd_group_events.IsEnabled = false;
            no_bd_top_frame_buttons.Children.Add(no_bd_group_events);
            no_bd_top_frame.Content = no_bd_top_frame_buttons;
            main.Children.Add(no_bd_top_frame);
            
            List<UserNotification> pending = App.Db.GetNotifications();
            List<DateTime> temp = new List<DateTime>();
            for(int i = 0; i < pending.Count; i++)
            {
                temp.Add(pending[i].Date);
            }
            temp.Sort();
            List<UserNotification> res = new List<UserNotification>();
            for(int i = 0; i < temp.Count; i++)
            {
                for(int j = 0; j < pending.Count; j++)
                {
                    if (pending[j].Date == temp[i])
                    {
                        res.Add(pending[j]);
                    }
                } 
            }
            List<Group> groups = App.Db.GetGroups();
            Label recent_events = new Label();
            Frame recent_events_border = new Frame();
            recent_events_border.BorderColor = Color.Black;
            recent_events_border.BackgroundColor = Color.FromHex("#DAFFFB");
            recent_events_border.HorizontalOptions = LayoutOptions.Center;
            recent_events_border.VerticalOptions = LayoutOptions.Center;
            recent_events_border.CornerRadius = 25;
            recent_events.Text = "Ближайшие напоминания:";
            recent_events.TextColor = Color.FromHex("#001C30");
            recent_events.FontFamily = "JostRegular";
            recent_events.FontSize = 25;
            recent_events_border.Content = recent_events;
            main.Children.Add(recent_events_border);
            for(int i = 0; i < 3; i++)
            {
                try { 
                Frame notifications_frame = new Frame();
                notifications_frame.CornerRadius = 25;
                notifications_frame.BackgroundColor = Color.FromHex("#64CCC5");
                notifications_frame.Margin = new Thickness(0, 20, 0, 0);
                notifications_frame.HorizontalOptions = LayoutOptions.Center;
                StackLayout stack = new StackLayout();
                StackLayout horiz = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal
                };
                Label name_notification = new Label()
                {
                    Text = res[i].Name,
                    FontFamily = "JostRegular",
                    FontSize = 25,
                    HorizontalTextAlignment = TextAlignment.Start,
                    TextColor = Color.Black,
                };
                ImageButton pencil = new ImageButton()
                {
                    Source = "Edit.png",
                    BackgroundColor = Color.FromHex("#64CCC5")
                    
                };
                horiz.Children.Add(name_notification);
                horiz.Children.Add(pencil);
                BoxView line = new BoxView();
                line.Color = Color.Black;
                line.HorizontalOptions = LayoutOptions.FillAndExpand;
                line.WidthRequest = 1;
                line.HeightRequest = 1;
                Label datetime = new Label() {
                    Text = res[i].Date.ToString(),
                    FontFamily = "ManropeBold",
                    FontSize = 15,
                    TextColor = Color.Black,
                };
                 Group needed_group = null;
                for(int z = 0; z < groups.Count; z ++) {
                    if (res[i].Group == groups[z].Id)
                    {
                       needed_group = groups[z];
                    }
                } 
                Label group = new Label()
                {
                    Text = "Из группы: " + needed_group.Name,
                    FontFamily = "ManropeBold",
                    FontSize = 15,
                    TextColor = Color.Black,
                };
                stack.Children.Add(horiz);
                stack.Children.Add(line);
                stack.Children.Add(datetime);
                stack.Children.Add(group);
                notifications_frame.Content = stack;
                main.Children.Add(notifications_frame); 
                }
                catch
                {
                    
                    break;
                } 

            } 
            Frame no_db_footer = new Frame();
            no_db_footer.BackgroundColor = Color.FromHex("#176B87");
            Button no_db_git_hub_button = new Button();
            no_db_git_hub_button.Text = "Планировщик задач, @IvanRukan";
            no_db_git_hub_button.BackgroundColor = Color.FromHex("#176B87");
            no_db_git_hub_button.TextColor = Color.FromHex("#DAFFFB");
            no_db_git_hub_button.FontFamily = "InterRegular";
            no_db_git_hub_button.BorderColor = Color.FromHex("#DAFFFB");
            no_db_git_hub_button.BorderWidth = 2;
            no_db_git_hub_button.CornerRadius = 20;
            no_db_git_hub_button.Clicked += GitHubLink;
            no_db_footer.Margin = new Thickness(0, 30, 0, 0);
            no_db_footer.Content = no_db_git_hub_button;
            no_db_footer.VerticalOptions = LayoutOptions.EndAndExpand;
            
            
            main.Children.Add(no_db_footer);
            return;
        }

        private void Current_OnNotificationReceived(NotificationEventArgs e)
        {
            OnAppearing();
        }

        private async void No_bd_all_events_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopModalAsync(animated: false);
            }
            catch
            {
                return;
            }
        }
    }
}