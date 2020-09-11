﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZamjenaDomova.Mobile.Services;
using ZamjenaDomova.Mobile.Views;

namespace ZamjenaDomova.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new LoginPage();
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
