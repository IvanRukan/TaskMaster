using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThingsToDo
{
    public class UserNotification
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Group { get; set; }
    }
}
