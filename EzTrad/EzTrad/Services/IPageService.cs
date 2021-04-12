﻿using System.Threading.Tasks;
using Xamarin.Forms;

namespace EzTrad.Services
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task<Page> PopAsync();
        Task PushModelAsync(Page page);
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task DisplayAlert(string title, string message, string ok);
    }
}
