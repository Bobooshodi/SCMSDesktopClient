using System;
using ToastNotifications;
using ToastNotifications.Core;

namespace SCMSClient.ToastNotification
{
    public static class CustomMessageExtensions
    {
        public static void ShowSuccessToast(this Notifier notifier, MessageOptions options, string title, string message,
            Action<SuccessNotification> closeAction)
        {
            notifier.Notify<SuccessNotification>(() => new SuccessNotification(title, message, closeAction, options));
        }

        public static void ShowErrorToast(this Notifier notifier, MessageOptions options, string title, string message,
            Action<ErrorNotification> closeAction)
        {
            notifier.Notify<ErrorNotification>(() => new ErrorNotification(title, message, closeAction, options));
        }

        public static void ShowWarningToast(this Notifier notifier, MessageOptions options, string title, string message,
            Action<WarningNotification> closeAction)
        {
            notifier.Notify<WarningNotification>(() => new WarningNotification(title, message, closeAction, options));
        }

        public static void ShowInformationToast(this Notifier notifier, MessageOptions options, string title, string message,
           Action<InformationNotification> closeAction)
        {
            notifier.Notify<InformationNotification>(() => new InformationNotification(title, message, closeAction, options));
        }
    }
}