using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ThingsToDo
{
    public class Group
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        
       
    }
}
