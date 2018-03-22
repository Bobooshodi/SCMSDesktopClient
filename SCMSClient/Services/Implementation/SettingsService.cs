using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using SCMSClient.Models;
using SCMSClient.Services.Interfaces;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;

namespace SCMSClient.Services.Implementation
{
    public class SettingsService : ISettingsService, IDisposable
    {
        #region Private Members

        private StreamReader srReader;

        #endregion Private Members

        #region Public Members

        public string fileName { get; set; }

        #endregion Public Members

        #region Public Methods

        /// <summary>
        /// Tries to read the Data Stored in the Isolated Storage <see cref="IsolatedStorage"/>
        /// by the Application
        /// </summary>
        /// <returns>
        /// returns an ApplicationSettings Model <see cref="ApplicationSettings"/>
        /// containing the Settings Saved by the Application
        /// </returns>
        public ApplicationSettings LoadSettings()
        {
            var appSettings = new ApplicationSettings();
            var settings = string.Empty;

            try
            {
                var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                srReader = new StreamReader(new IsolatedStorageFileStream("AppSettings", FileMode.OpenOrCreate, isolatedStorage));

                //Open the isolated storage
                if (srReader == null)
                {
                    throw new Exception("No Data stored!");
                }
                else
                {
                    while (!srReader.EndOfStream)
                    {
                        string item = srReader.ReadLine();
                        settings += item;
                    }

                    appSettings = JsonConvert.DeserializeObject<ApplicationSettings>(settings);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                srReader?.Close();
            }

            return appSettings;
        }

        /// <summary>
        /// A Generic method to load the Application's Settings from a file <paramref name="fileName"/>
        /// and return a <see cref="Type"/> <typeparamref name="T"/> of the Model Passed in the call
        /// </summary>
        /// <typeparam name="T">
        /// The Structure of the data in the <paramref name="fileName"/> file
        /// </typeparam>
        /// <param name="fileName">
        /// the name of the file to read the data from
        /// </param>
        /// <returns>
        /// returns a Model of <see cref="Type"/> <typeparamref name="T"/>
        /// containing the Settings Saved by the Application
        /// </returns>
        public T LoadSettings<T>(string fileName)
        {
            var appSettings = default(T);
            IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            srReader = new StreamReader(new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, isolatedStorage));
            var settings = string.Empty;

            try
            {
                //Open the isolated storage
                if (srReader == null)
                {
                    throw new Exception("No Data stored!");
                }
                else
                {
                    while (!srReader.EndOfStream)
                    {
                        string item = srReader.ReadLine();
                        settings += item;
                    }

                    appSettings = JsonConvert.DeserializeObject<T>(settings);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (srReader != null)
                    srReader.Close();
            }

            return appSettings;
        }

        /// <summary>
        /// Writes the <paramref name="settings"/> to an IsolatedStorage file <see cref="IsolatedStorage"/>
        /// </summary>
        /// <param name="settings">
        /// The Setting model to write
        /// </param>
        /// <returns>
        /// a boolean value <see cref="bool"/> showing if the operation was successfull or not
        /// </returns>
        public bool SaveSettings(ApplicationSettings settings)
        {
            StreamWriter srWriter = null;

            try
            {
                IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                srWriter = new StreamWriter(new IsolatedStorageFileStream("AppSettings", FileMode.Create, isolatedStorage));

                string output = JsonConvert.SerializeObject(settings);

                srWriter.Write(output);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (srWriter != null)
                {
                    srWriter.Flush();
                    srWriter.Close();
                }
            }

            return true;
        }

        /// <summary>
        /// Deletes the Settings saved by the Application
        /// </summary>
        /// <returns>
        /// a boolean value <see cref="bool"/> showing if the operation was successfull or not
        /// </returns>
        public bool DeleteSettings()
        {
            IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();

            try
            {
                if (isolatedStorage.FileExists("AppSettings"))
                {
                    isolatedStorage.DeleteFile("AppSettings");
                }
                else
                {
                    throw new InvalidOperationException("No settings found");
                }
            }
            catch
            {
                throw;
            }

            return true;
        }

        /// <summary>
        /// Deletes the Settings saved by the Application in the File Specified <paramref name="fileName"/>
        /// </summary>
        /// <param name="fileName">
        /// The file containing the Settings to delete
        /// </param>
        /// <returns>
        /// a boolean value <see cref="bool"/> showing if the operation was successfull or not
        /// </returns>
        public bool DeleteSettings(string fileName)
        {
            IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();

            try
            {
                if (isolatedStorage.FileExists(fileName))
                {
                    isolatedStorage.DeleteFile(fileName);
                }
                else
                {
                    throw new InvalidOperationException("No settings found");
                }
            }
            catch (IsolatedStorageException ex)
            {
                throw ex;
            }

            return true;
        }

        /// <summary>
        /// Opens a SelectFile Dialogue to Import the Application's setting from
        /// </summary>
        /// <returns>
        /// <see cref="ApplicationSettings"/> Model
        /// </returns>
        public ApplicationSettings ImportSetting()
        {
            ApplicationSettings appSettings;
            srReader = new StreamReader(fileName);
            var settings = string.Empty;

            try
            {
                if (srReader == null)
                {
                    throw new Exception("File not Found");
                }
                else
                {
                    while (!srReader.EndOfStream)
                    {
                        string item = srReader.ReadLine();
                        settings += item;
                    }

                    appSettings = JsonConvert.DeserializeObject<ApplicationSettings>(settings);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (srReader != null)
                    srReader.Close();
            }

            return appSettings;
        }

        /// <summary>
        /// Writes the <paramref name="settings"/> to a file on the disk
        /// </summary>
        /// <param name="settings">
        /// The settings to write to disk
        /// </param>
        /// <returns>
        /// a <see cref="Boolean"/> value to show theoperation was successful or not
        /// </returns>
        public bool ExportSetting(ApplicationSettings settings)
        {
            string content = JsonConvert.SerializeObject(settings);

            var dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "VMSClientSetting"; // Default file name
            dlg.DefaultExt = ".Json"; // Default file extension
            dlg.Filter = "JSON (.Json)|*.Json"; // Filter files by extension

            // Show save file dialog box
            bool? result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                fileName = dlg.FileName;

                FileStream file = File.Open(fileName, FileMode.Create);
                var writer = new StreamWriter(file);

                try
                {
                    writer.WriteLine(content);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    writer.Flush();
                    writer.Close();
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Clears all Settings and configuration held in Memory and
        /// Closes the Application and displays the Login Page
        /// </summary>
        public void LogOutUser()
        {
            Application.Current.Properties.Clear();

            var loginPage = new Windows.Login { WindowState = WindowState.Maximized };
            loginPage.Show();

            Messenger.Default.Send(ApplicationCommands.SHUT_DOWN);
        }

        public void Dispose()
        {
            srReader.Close();
            srReader = null;
        }

        #endregion Public Methods
    }
}