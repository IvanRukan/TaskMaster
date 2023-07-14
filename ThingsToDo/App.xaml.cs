using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThingsToDo
{
    public partial class App : Application
    {
        private static DB db;
        public static DB Db
        {
            get { 
                if(db == null)
                {
                    db = new DB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"db.sqlite3"));
                }
                return db;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
