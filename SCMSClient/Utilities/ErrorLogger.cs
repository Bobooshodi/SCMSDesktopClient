using System;
using System.IO;
using System.Text;

namespace SCMSClient.Utilities
{
    public static class ErrorLogger
    {
        #region Constant Declarations

        private const string baseErrorFolderName = "VMS Error Logs";
        private const string applicationErrorFile = "Application Error Logs.txt";
        private const string deviceFailureErrorFile = "Device Failure Error Logs.txt";
        private const string serverErrorFile = "Server Error Logs.txt";
        private const string applicationErrorFolder = "Application Error Logs";
        private const string deviceFailureErrorFolder = "Device Failure Error Logs";
        private const string serverErrorFolder = "Server Error Logs";

        #endregion

        #region Public Static Methods

        /// <summary>
        /// This Method Takes the content and formats the Text to be written to disk
        /// based on the <see cref="ErrorType"/> errorType
        /// </summary>
        /// <param name="content">
        /// The error to Log
        /// </param>
        /// <param name="errorType">
        /// The type of Error
        /// </param>
        public static void LogError(string content, ErrorType errorType)
        {
            var sb = new StringBuilder();

            switch (errorType)
            {
                case ErrorType.APPLICATION_ERROR:
                    sb.Append("Occurrence DateTime: ").Append(DateTime.Now).AppendLine();
                    sb.Append("Error: ").AppendLine(content);
                    LogError(applicationErrorFolder, applicationErrorFile, sb.ToString());
                    break;

                case ErrorType.DEVICE_FAILURE_ERROR:
                    sb.Append("Occurrence DateTime: ").Append(DateTime.Now).AppendLine();
                    sb.Append("Error: ").AppendLine(content);
                    LogError(deviceFailureErrorFolder, deviceFailureErrorFile, sb.ToString());
                    break;

                case ErrorType.SERVER_ERROR:
                    sb.Append("Occurrence DateTime: ").Append(DateTime.Now).AppendLine();
                    sb.Append("Error: ").AppendLine(content);
                    LogError(serverErrorFolder, serverErrorFile, sb.ToString());
                    break;
            }
        }

        /// <summary>
        /// This Method Takes the content and writes it to a file on the disk
        /// using the <paramref name="folderName"/> and <paramref name="fileName"/>
        /// specified in the call
        /// </summary>
        /// <param name="folderName">
        /// Physical folder on the disk to write the File
        /// </param>
        /// <param name="fileName">
        /// The name to give the Output file
        /// </param>
        /// <param name="content">
        /// The Error to Write to the file
        /// </param>
        private static void LogError(string folderName, string fileName, string content)
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), baseErrorFolderName, folderName);
            var file = Path.Combine(folder, fileName);

            try
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                if (!File.Exists(file))
                {
                    File.WriteAllText(file, content);
                }
                else
                {
                    using (var writer = new StreamWriter(file, true))
                    {
                        writer.WriteLine(content);
                    }
                }
            }
            catch
            {
            }
        }

        #endregion
    }

    public enum ErrorType
    {
        SERVER_ERROR,
        APPLICATION_ERROR,
        DEVICE_FAILURE_ERROR
    }
}
