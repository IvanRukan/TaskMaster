using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using LocalNotifications;



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
            DeleteNotification(group.Id);
            conn.Delete(group);
            
        }
        
        public void SaveNotification(UserNotification notification)
        {

            conn.Insert(notification);
            
            
            NotificationCenter.Current.Schedule(notificationId: notification.Id, title: "Новое напоминание!",
                description: notification.Name, dateTime: notification.Date, payload: "");
            
            
        }
        public void DeleteNotification(int id)
        {
            List<UserNotification> all = App.Db.GetNotifications();
            for(int i = 0; i < all.Count; i++)
            {
                if(all[i].Id == id)
                {
                    NotificationCenter.Current.Cancel(notificationId: id);
                    conn.Delete(all[i]);
                    return;
                }
            }
        }

    }
}
