﻿using EzTrad.ViewModels.FlyoutMenuViewModel;
using EzTrad.Views;
using System;
using Xamarin.Forms;

namespace EzTrad
{
    public partial class MainPage : FlyoutPage
    {      
        public MainPage()
        {
            FlyoutMenuPage flyoutPage = new FlyoutMenuPage();
            InitializeComponent();
            //flyoutPage.listView.ItemSelected += OnItemSelected;
        }
        public MainPage(string x)
        {
            InitializeComponent();
            Detail= new NavigationPage(new MenuHorizontal(x));
        }
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutViewModel;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetPage));
                flyoutPage.LstView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
