using SCMSClient.Models;

namespace SCMSClient.Services.Interfaces
{
    public interface ISettingsService
    {
        string fileName { get; set; }

        ApplicationSettings LoadSettings();

        T LoadSettings<T>(string fileName);

        bool SaveSettings(ApplicationSettings settings);

        ApplicationSettings ImportSetting();

        bool ExportSetting(ApplicationSettings settings);

        bool DeleteSettings();

        bool DeleteSettings(string fileName);
    }
}
