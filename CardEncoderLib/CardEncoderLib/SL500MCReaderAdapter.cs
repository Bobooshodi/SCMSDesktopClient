using System;
using System.Management;

namespace CardEncoderLib
{
    /// <summary>
    /// This class manages the device connected to the System
    /// </summary>
    public class SL500MCReaderAdapter : ReaderAdapter
    {
        private CardReader cardReader;

        private ManagementEventWatcher watch;

        /// <summary>
        /// This method returns a new instance of cardWithNewKeys reader installed on the system
        /// returns null if non is found
        /// </summary>
        /// <returns></returns>
        public CardReader GetCardReader()
        {
            if (cardReader == null)
            {
                cardReader = findCardReaderOnSystem();
            }

            return cardReader;
        }

        /// <summary>
        /// This method starts a thread to watch for new hardware devices
        /// </summary>
        public void startUSBEventNotifier()
        {
            WqlEventQuery w = new WqlEventQuery();
            w.EventClassName = "__InstanceCreationEvent";
            w.Condition = "TargetInstance ISA 'Win32_USBControllerDevice'";
            w.WithinInterval = new TimeSpan(0, 0, 2);

            //use a "watcher", to run the query

            watch = new ManagementEventWatcher(w);
            watch.EventArrived += new
            EventArrivedEventHandler(this.usbDetectionHandler);
            watch.Start();
        }

        /// <summary>
        /// This method stoops the USB Event Notifier
        /// </summary>
        public void stopUSBEventNotifier()
        {
            watch.Stop();
        }

        /// <summary>
        /// find the Card Reader device with port number from available devices
        /// </summary>
        /// <returns></returns>
        private CardReader findCardReaderOnSystem()
        {
            CardReader cardReader = null;
            string dPort;
            int port;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_SerialPort");

                //system available devices
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    //check for devices supported by library
                    string pnpDeviceId = (string)queryObj["PNPDeviceID"];

                    if (pnpDeviceId == SL500MCReader.PNPID)
                    {
                        cardReader = new SL500MCReader();
                        dPort = (string)queryObj["DeviceID"];
                        int numDigits = dPort.Length - 3;

                        port = Convert.ToInt32(dPort.Substring(dPort.Length - numDigits, numDigits));
                        ((SL500MCReader)cardReader).PortNumber = port;
                        ((SL500MCReader)cardReader).BaudRate = 9600;
                    }
                }
            }
            catch (ManagementException)
            {
                throw;
            }
            return cardReader;
        }

        /// <summary>
        /// Call back function when a new usb device is detected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usbDetectionHandler(object sender, EventArrivedEventArgs e)
        {
            if (cardReader == null)
            {
                cardReader = findCardReaderOnSystem();
            }

            // code to handle when a new usb device is detected
        }
    }
}