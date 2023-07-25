using LocalNotifications;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using Xamarin.Forms.Shapes;
using Image = Xamarin.Forms.Image;
using Label = Xamarin.Forms.Label;

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
            List<NotificationRequest> pending = new List<NotificationRequest>();
            List<UserNotification> existing = new List<UserNotification>();
            existing = App.Db.GetNotifications();
            pending = NotificationCenter.Current.GetPendingNotificationRequests();
            List<int> useless = new List<int>();
            List<int> usefull = new List<int>();
            for(int i = 0; i < existing.Count; i++)
            {
                useless.Add(existing[i].Id);
            }
            for(int i = 0; i < pending.Count; i++)
            {
                for(int j = 0; j < existing.Count; j++)
                {
                    
                    if(pending[i].NotificationId == existing[j].Id) {
                        usefull.Add(pending[i].NotificationId);
                    }
                }
            }
            var to_delete = useless.Except(usefull);
            List<int> delete_int = (to_delete).ToList();
            for(int i =0; i < delete_int.Count; i ++)
            {
                App.Db.DeleteNotification(delete_int[i]);
            }
            NotificationCenter.Current.OnNotificationReceived += Current_OnNotificationReceived;
            List<Group> all_groups = App.Db.GetGroups();
            List<Group> resulting_groups = App.Db.GetGroups();
            if (resulting_groups?.Any() != true)
            {
                outer_layout.Children.Clear();
                outer_layout.VerticalOptions = LayoutOptions.FillAndExpand;
                outer_layout.HorizontalOptions = LayoutOptions.FillAndExpand;
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
                no_bd_recent_events.TextColor = Color.Gray;
                no_bd_recent_events.IsEnabled = false;
                no_bd_recent_events.BackgroundColor = Color.FromHex("#176B87");
                no_bd_recent_events.FontFamily = "InterRegular";
                no_bd_recent_events.FontSize = 11;
                no_bd_recent_events.WidthRequest = 118;
                no_bd_recent_events.CornerRadius = 20;
                no_bd_top_frame_buttons.Children.Add(no_bd_recent_events);
                Button no_bd_all_events = new Button();
                no_bd_all_events.Text = "Все напоминания";
                no_bd_all_events.TextColor = Color.FromHex("#DAFFFB");
                no_bd_all_events.IsEnabled = true;
                no_bd_all_events.BackgroundColor = Color.FromHex("#176B87");
                no_bd_all_events.FontFamily = "InterSemiBold";
                no_bd_all_events.FontSize = 11;
                no_bd_all_events.WidthRequest = 118;
                no_bd_all_events.BorderColor = Color.FromHex("#DAFFFB");
                no_bd_all_events.BorderWidth = 2;
                no_bd_all_events.CornerRadius = 20;
                no_bd_top_frame_buttons.Children.Add(no_bd_all_events);
                Button no_bd_group_events = new Button();
                no_bd_group_events.Text = "Напоминания по группе";
                no_bd_group_events.TextColor = Color.Gray;
                no_bd_group_events.IsEnabled = false;
                no_bd_group_events.BackgroundColor = Color.FromHex("#176B87");
                no_bd_group_events.FontFamily = "InterRegular";
                no_bd_group_events.FontSize = 11;
                no_bd_group_events.WidthRequest = 118;
                no_bd_group_events.CornerRadius = 20;
                no_bd_top_frame_buttons.Children.Add(no_bd_group_events);
                no_bd_top_frame.Content = no_bd_top_frame_buttons;
                Label no_groups = new Label()
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Margin = new Thickness(0, 50, 0, 0),
                    HorizontalTextAlignment = TextAlignment.Center,
                    Text = "Группы не найдены! Создайте новую группу, чтобы добавить напоминания.",
                    TextColor = Color.Gray,
                    FontSize = 30,
                    FontFamily = "JostRegular"
                };
                Button no_db_create_group = new Button()
                {
                    Margin = new Thickness(0, 0, 0, 150),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    CornerRadius = 25,
                    Text = "Создать новую группу",
                    FontSize = 12,
                    FontFamily = "InterSemiBold",
                    TextColor = Color.FromHex("#DAFFFB"),
                    BackgroundColor = Color.FromHex("#176B87")
                };
                no_db_create_group.Clicked += NewGroup;
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
                outer_layout.Children.Add(no_bd_top_frame);
                outer_layout.Children.Add(no_groups);
                outer_layout.Children.Add(no_db_create_group);
                outer_layout.Children.Add(no_db_footer);
                return;
            }
            
            outer_layout.Children.Clear();
            outer_layout.VerticalOptions = LayoutOptions.FillAndExpand;
            outer_layout.HorizontalOptions = LayoutOptions.FillAndExpand;
            
            Frame top_frame = new Frame();
            
            top_frame.BackgroundColor = Color.FromHex("#176B87");
            top_frame.HasShadow = true;
            top_frame.WidthRequest = 355;
            StackLayout top_frame_buttons = new StackLayout();  
            top_frame_buttons.Orientation = StackOrientation.Horizontal;
            top_frame_buttons.BackgroundColor = Color.FromHex("#176B87");
            Button recent_events = new Button();
            recent_events.Text = "Ближайшие напоминания";
            recent_events.TextColor = Color.FromHex("#DAFFFB");
            recent_events.BackgroundColor = Color.FromHex("#176B87");
            recent_events.FontFamily = "InterRegular";
            recent_events.FontSize = 11;
            recent_events.WidthRequest = 118;
            recent_events.CornerRadius = 25;
            recent_events.Clicked += Recent_events_Clicked;
            top_frame_buttons.Children.Add(recent_events);
            Button all_events = new Button();
            all_events.Text = "Все напоминания";
            all_events.TextColor = Color.FromHex("#DAFFFB");
            all_events.BackgroundColor = Color.FromHex("#176B87");
            all_events.FontFamily = "InterSemiBold";
            all_events.FontSize = 11;
            all_events.WidthRequest = 118;
            all_events.BorderColor = Color.FromHex("#DAFFFB");
            all_events.BorderWidth = 2;
            all_events.CornerRadius = 20;
            top_frame_buttons.Children.Add(all_events);
            Button group_events = new Button();
            group_events.Text = "Напоминания по группе";
            group_events.TextColor = Color.FromHex("#DAFFFB");
            group_events.BackgroundColor = Color.FromHex("#176B87");
            group_events.FontFamily = "InterRegular";
            group_events.FontSize = 11;
            group_events.WidthRequest = 118;
            group_events.CornerRadius = 20;
            group_events.IsEnabled = false;
            top_frame_buttons.Children.Add(group_events);
            top_frame.Content = top_frame_buttons;
            
            StackLayout button_layout = new StackLayout();
            BoxView new_line = new BoxView();
            new_line.HorizontalOptions = LayoutOptions.FillAndExpand;
            new_line.VerticalOptions = LayoutOptions.Start;
            new_line.Color = Color.FromHex("#176B87");
            button_layout.Orientation = StackOrientation.Horizontal;
            button_layout.Margin = new Thickness(40, 20, 40, 20);
            Button create_group = new Button();
            create_group.Text = "Создать группу";
            create_group.HorizontalOptions = LayoutOptions.StartAndExpand;
            create_group.VerticalOptions = LayoutOptions.Center;
            create_group.TextColor = Color.FromHex("#DAFFFB");
            create_group.FontFamily = "InterSemiBold";
            create_group.FontSize = 10;
            create_group.BackgroundColor = Color.FromHex("#176B87");
            create_group.CornerRadius = 20;
            create_group.Clicked += NewGroup;
            Button delete_group = new Button();
            delete_group.Text = "Удалить группу";
            delete_group.HorizontalOptions = LayoutOptions.EndAndExpand;
            delete_group.VerticalOptions = LayoutOptions.Center;
            delete_group.TextColor = Color.FromHex("#DAFFFB");
            delete_group.FontFamily = "InterSemiBold";
            delete_group.FontSize = 10;
            delete_group.BackgroundColor = Color.FromHex("#176B87");
            delete_group.CornerRadius = 20;
            delete_group.Margin = new Thickness(20, 0, 0, 0);
            delete_group.Clicked += DeleteGroup;
            button_layout.Children.Add(create_group);
            button_layout.Children.Add(delete_group);
            if(existing?.Any() != true)
            {
                recent_events.IsEnabled = false;
                
            }
            
            Frame footer = new Frame();
            footer.BackgroundColor = Color.FromHex("#176B87");            
            Button git_hub_button = new Button();
            git_hub_button.Text = "Планировщик задач, @IvanRukan";
            git_hub_button.BackgroundColor = Color.FromHex("#176B87");
            git_hub_button.TextColor = Color.FromHex("#DAFFFB");
            git_hub_button.FontFamily = "InterRegular";
            git_hub_button.BorderColor = Color.FromHex("#DAFFFB");
            git_hub_button.BorderWidth = 2;
            git_hub_button.CornerRadius = 25;
            git_hub_button.Clicked += GitHubLink;
            footer.Margin = new Thickness(0, 0, 0, 0);
            footer.Content = git_hub_button;
            footer.VerticalOptions = LayoutOptions.FillAndExpand;
            CollectionView group_box = new CollectionView();
            group_box.ItemsSource = resulting_groups;
            group_box.VerticalOptions = LayoutOptions.FillAndExpand;
            group_box.ItemTemplate = new DataTemplate(() =>
            {
                
                StackLayout group = new StackLayout();
                group.Padding = new Thickness(70, 0, 70, 0);
                Frame notifications_frame = new Frame();
                notifications_frame.CornerRadius = 25;
              //  notifications_frame.MinimumWidthRequest = 50;
                notifications_frame.BackgroundColor = Color.FromHex("#64CCC5");
                notifications_frame.Margin = new Thickness(0, 20, 0, 0);
                StackLayout notification_layout = new StackLayout();
                Label notifications_group = new Label();
                notifications_group.SetBinding(Label.TextProperty, new Binding("Name"));
                notifications_group.TextColor = Color.Black;
                notifications_group.FontFamily = "JostRegular";
                notifications_group.FontSize = 20;
                notifications_group.VerticalOptions = LayoutOptions.Start;
                notifications_group.HorizontalOptions = LayoutOptions.CenterAndExpand;
                BoxView line = new BoxView();
                line.Color = Color.Black;
                line.HorizontalOptions = LayoutOptions.FillAndExpand;
                line.WidthRequest = 1;
                line.HeightRequest = 1;
                ImageButton notifications_button = new ImageButton();
                notifications_button.Source = "Plus.png";
                notifications_button.BackgroundColor = Color.FromHex("#64CCC5");
                notifications_button.HorizontalOptions = LayoutOptions.Center;
                notifications_button.Clicked += Notifications_button_Clicked;
                notification_layout.Children.Add(notifications_group);
                StackLayout vert_notify = new StackLayout();
                Group test = all_groups[0];
                List<UserNotification> all_notify = App.Db.GetNotifications();
                for (int i = 0; i < all_notify.Count; i++)
                {
                    if (all_notify[i].Group == test.Id)
                    {
                        StackLayout horiz = new StackLayout();
                        horiz.Orientation = StackOrientation.Horizontal;
                        Image circle = new Image();
                        circle.Source = "Cirle.png";
                        circle.BackgroundColor = Color.FromHex("#64CCC5");
                        circle.HorizontalOptions = LayoutOptions.Start;
                        Label notify_name = new Label();
                        notify_name.Text = all_notify[i].Name;
                        notify_name.FontFamily = "ManropeBold";
                        notify_name.FontSize = 15;
                        ImageButton edit = new ImageButton();
                        edit.Source = "Edit.png";
                        edit.BackgroundColor = Color.FromHex("#64CCC5");
                        edit.HorizontalOptions = LayoutOptions.End;
                        horiz.Children.Add(circle);
                        horiz.Children.Add(notify_name);
                        horiz.Children.Add(edit);
                        vert_notify.Children.Add(horiz);
                    }
                }
                notification_layout.Children.Add(vert_notify);
                notification_layout.Children.Add(line);
                all_groups.RemoveAt(0);
                notification_layout.Children.Add(notifications_button);
                notifications_frame.Content = notification_layout;
               // notifications_frame.HorizontalOptions = LayoutOptions.CenterAndExpand;
                group.Children.Add(notifications_frame);
                return group;
            }); 
            Label created_groups = new Label();
            Frame created_groups_border = new Frame();
            created_groups_border.BorderColor = Color.Black;
            created_groups_border.BackgroundColor = Color.FromHex("#DAFFFB");
            created_groups_border.HorizontalOptions = LayoutOptions.Center;
            created_groups_border.VerticalOptions = LayoutOptions.Center;
            created_groups_border.CornerRadius = 25;
            created_groups.Text = "Созданные группы:";
            created_groups.TextColor = Color.FromHex("#001C30");
            created_groups.FontFamily = "JostRegular";
            created_groups.FontSize = 25;
            created_groups_border.Content = created_groups;
            StackLayout created_groups_layout = new StackLayout();
            created_groups_layout.Children.Add(created_groups_border);
            StackLayout footer_layout = new StackLayout();
            footer_layout.Children.Add(button_layout);
            group_box.Header = created_groups_layout;
            group_box.Footer = footer_layout;
            outer_layout.Children.Add(top_frame);
            outer_layout.Children.Add(group_box);
            outer_layout.Children.Add(footer);
            











        }
        readonly ClosestNotifications closestNotifications = new ClosestNotifications();
        private async void Recent_events_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(closestNotifications, animated: false);
            }
            catch
            {
                return;
            }
        }

        private void Current_OnNotificationReceived(NotificationEventArgs e)
        {
            int id = (int)e.NotificationId;
            
            App.Db.DeleteNotification(id);
            OnAppearing();
            
        }

        readonly NotificationCreation new_notification = new NotificationCreation();
        private async void Notifications_button_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new_notification, animated:true);
            }
            catch
            {
                return;
            }
        }

        readonly GroupCreation new_group = new GroupCreation();
        private async void NewGroup(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new_group);
            }
            catch
            {
                return;
            }
        }
        readonly GroupDeletion group_to_delete = new GroupDeletion();
        private async void DeleteGroup(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(group_to_delete);
            }
            catch
            {
                return;
            }
        }
        
        
       

    }
}
