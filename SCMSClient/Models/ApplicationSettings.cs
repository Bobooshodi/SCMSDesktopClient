using System;

namespace SCMSClient.Models
{
    public class ApplicationSettings
    {
        public Server RemoteServer { get; set; }
        //public MainFacility MainFacility { get; set; }
        public LockedOutModel UserLockedOut { get; set; }
        public ApplicationTheme? SelectedTheme { get; set; }
        public TimeSpan DefaultDuration { get; set; }
    }

    public class LockedOutModel
    {
        public bool IsLockedOut { get; set; }
        public DateTime LockedOutTime { get; set; }
    }

    public enum ApplicationTheme
    {
        LIGHT_THEME,
        DARK_THEME
    }
}
