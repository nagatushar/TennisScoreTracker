﻿using System.Windows;
using TennisScoreTracker.ViewModels;

namespace TennisScoreTracker
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetWindow mainWindow = new SetWindow();
            //TieBreakWindow mainWindow = new TieBreakWindow();
            mainWindow.Show();


        }
    }
}