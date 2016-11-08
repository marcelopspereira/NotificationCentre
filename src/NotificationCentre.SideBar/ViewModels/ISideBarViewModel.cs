﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using NotificationCentre.SideBar.Models;

namespace NotificationCentre.SideBar.ViewModels
{
    internal interface ISideBarViewModel
    {
        bool IsOpen { get; set; }

        ICommand SwitchTheme { get; set; }

        ObservableCollection<IAlertModel> Alerts { get; }
    }
}