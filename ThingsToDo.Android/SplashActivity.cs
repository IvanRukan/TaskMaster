﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThingsToDo.Droid
{
    [Activity(Label = "TaskMaster", MainLauncher =true, Theme ="@style/MyTheme.Splash", NoHistory =true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
        protected async override void OnResume()
        {
            base.OnResume();
            await SimulateStartup();
        }   
        private async Task SimulateStartup()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}