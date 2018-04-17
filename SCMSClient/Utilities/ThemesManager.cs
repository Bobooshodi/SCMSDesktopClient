using SCMSClient.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SCMSClient.Utilities
{
    public static class ThemesManager
    {
        private static readonly List<ResourceDictionary> Styles = new List<ResourceDictionary>
        {
            // cOMPONENT STYLES
            new ResourceDictionary() { Source = new Uri("/Styles/components/_button.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/components/_calendar.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/components/_checkbox.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/components/_combobox.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/components/_datagrid.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/components/_forminputs.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/components/_listview.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/components/_loader.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/components/_modaldialog.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/components/_scrollbar.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/components/_tabcontrol.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/components/_toaster.xaml", UriKind.RelativeOrAbsolute) },

            // LAYOUT STYLES
            new ResourceDictionary() { Source = new Uri("/Styles/layout/_base.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/layout/_header.xaml", UriKind.RelativeOrAbsolute) },

            // PAGES STYLES
            new ResourceDictionary() { Source = new Uri("/Styles/Pages/_cardholder.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/Pages/_login.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/Pages/_visitorinfo.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/Pages/_walkthrough.xaml", UriKind.RelativeOrAbsolute) },
            new ResourceDictionary() { Source = new Uri("/Styles/Pages/_manage-cards.xaml", UriKind.RelativeOrAbsolute) },

            // THEMES STYLES
            new ResourceDictionary() { Source= new Uri("pack://application:,,,/ToastNotifications.Messages;component/Themes/Default.xaml" , UriKind.RelativeOrAbsolute) },
        };

        public static void ChangeTheme(ApplicationTheme selectedTheme)
        {
            ResourceDictionary theme = new ResourceDictionary() { Source = new Uri("/Styles/Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute) };

            try
            {
                switch (selectedTheme)
                {
                    case ApplicationTheme.LIGHT_THEME:
                        theme = new ResourceDictionary() { Source = new Uri("/Styles/Themes/LightTheme.xaml", UriKind.RelativeOrAbsolute) };

                        Application.Current.Resources.MergedDictionaries.Clear();
                        Application.Current.Resources.MergedDictionaries.Add(theme);

                        foreach (var item in Styles)
                        {
                            Application.Current.Resources.MergedDictionaries.Add(item);
                        }

                        break;

                    case ApplicationTheme.DARK_THEME:
                        theme = new ResourceDictionary() { Source = new Uri("/Styles/Themes/DarkTheme.xaml", UriKind.RelativeOrAbsolute) };

                        Application.Current.Resources.MergedDictionaries.Clear();
                        Application.Current.Resources.MergedDictionaries.Add(theme);

                        foreach (var item in Styles)
                        {
                            Application.Current.Resources.MergedDictionaries.Add(item);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Unable to Change Theme \r\n" + ex, ErrorType.APPLICATION_ERROR);
            }
        }
    }
}