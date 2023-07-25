using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using LocalNotifications;
using System.Drawing;

namespace ThingsToDo
{
    public class DB
    {
        private readonly SQLiteConnection conn;
        public DB(string path)
        {
            conn = new SQLiteConnection(path);
            conn.CreateTable<Group>();
            conn.CreateTable<UserNotification>();
        }

        public List<Group> GetGroups()
        {
            return conn.Table<Group>().ToList();
        }
        public List<UserNotification> GetNotifications()
        {
            return conn.Table<UserNotification>().ToList();
        }

        public int SaveGroup(Group group)
        {
            return conn.Insert(group);
        }

        public void DeleteGroup(Group group) 
        {
            DeleteNotificationWithGroup(group.Id);
            conn.Delete(group);
            
        }
        
        public void SaveNotification(UserNotification notification)
        {

            conn.Insert(notification);
            
            
            NotificationCenter.Current.Schedule(notificationId: notification.Id, title: "Новое напоминание!",
                description: notification.Name, dateTime: notification.Date, payload: "", androidOptions: new AndroidOptions {ShowBadge = true});
            
            
        }
        public void DeleteNotification(int id)
        {
            List<UserNotification> all = App.Db.GetNotifications();
            for(int i = 0; i < all.Count; i++)
            {
                if(all[i].Id == id)
                {
                    
                    conn.Delete(all[i]);
                    return;
                }
            }
        }
        public void DeleteNotificationWithGroup(int id)
        {
            List<UserNotification> all = App.Db.GetNotifications();
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Group == id)
                {
                    NotificationCenter.Current.Cancel(notificationId: all[i].Id);
                    conn.Delete(all[i]);
                    
                }
            }
        }

    }
}
