using LabJack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCI_Forms_SPN._GLobalz
{
    public class LabJManager : IDisposable
    {
        private static readonly Lazy<LabJManager> _instance = new Lazy<LabJManager>(() => new LabJManager());
        public static LabJManager Instance => _instance.Value;


        public delegate void FirstMEssageWasReceivedHandler(string argVersion);
        public event FirstMEssageWasReceivedHandler FirstMEssageWasReceived;
        public delegate void aLabJackDataReceivedHandler(string argSerial, string argFirmware);
        public event aLabJackDataReceivedHandler aLabJackDataReceived;

        private bool disposed = false;

        int handle = 0;
        int devType = 0;
        int conType = 0;
        int serNum = 0;
        int ipAddr = 0;
        int port = 0;
        int maxBytesPerMB = 0;
        string ipAddrStr = "";
        string _labSerial = "";
        string _labFirmware = "";
        bool isOnBus = false;
        #region MUXDAC vars
        const int Const_MUX_Entries = 5;
        int _num_MUXDAC_enries = 5;
        public int Num_MUXDAC_enries
        {
            get { return _num_MUXDAC_enries; }
            private set { _num_MUXDAC_enries = value; }
        }
        public string[] MUXDAC_names_withFIOS;
        public double[] MUXDAC_values_WithFIOS;
        #endregion
        bool muxbit0 = true;
        bool muxbit1 = true;
        bool muxbit2 = true;
        bool muxbit3 = true;
        double _RAW_DACTOSEND = 0.0;

        #region AINs vars
        const int ConstAINs = 4;
        int _num_AINs = 4;
        public int Num_AINs
        {
            get { return _num_AINs; }
            private set { _num_AINs = value; }
        }
        public string[] AINs_names;
        public double[] AINs_values;
        #endregion
        public void INIT_CON_LABJACK()
        {
            if (isOnBus) return;

            LJM.OpenS("ANY", "ANY", "ANY", ref handle);
            LJM.GetHandleInfo(handle, ref devType, ref conType, ref serNum, ref ipAddr, ref port, ref maxBytesPerMB);

            int numFrames = 3;
            string[] names = new string[3] { "SERIAL_NUMBER", "PRODUCT_ID", "FIRMWARE_VERSION" };
            double[] aValues = new double[3] { 0, 0, 0 };
            int errorAddress = 0;
            LJM.eReadNames(handle, numFrames, names, aValues, ref errorAddress);
            _labSerial = aValues[0].ToString();
            _labFirmware = aValues[2].ToString();


            if (maxBytesPerMB > 1)
            {
                isOnBus = true;
                Debug.WriteLine(_labSerial);
                Debug.WriteLine(_labFirmware);
                OnLabJack_FirstInfo_Received(_labSerial, _labFirmware);

                aLabJackDataReceived?.Invoke(_labSerial, _labFirmware);
            }
            else
            {
                isOnBus = false;
            }


            #region MUXDAC init
            _num_MUXDAC_enries = Const_MUX_Entries;
            MUXDAC_names_withFIOS = new string[Const_MUX_Entries];
            MUXDAC_names_withFIOS[0] = "FIO2";
            MUXDAC_names_withFIOS[1] = "FIO3";
            MUXDAC_names_withFIOS[2] = "FIO4";
            MUXDAC_names_withFIOS[3] = "FIO5";
            MUXDAC_names_withFIOS[4] = "DAC0";
            MUXDAC_values_WithFIOS = new double[Const_MUX_Entries];
            MUXDAC_values_WithFIOS[0] = 0;
            MUXDAC_values_WithFIOS[1] = 0;
            MUXDAC_values_WithFIOS[2] = 0;
            MUXDAC_values_WithFIOS[3] = 0;
            MUXDAC_values_WithFIOS[4] = 0;
            #endregion

        }
        public LabJManager() { }

        ~LabJManager()
        {
            Dispose(false);
        }

        public void WRITEDATA_MUXDAC(int argCHannelNumber, double argDACValue)
        {
            if (argDACValue < 0)
            {
                argDACValue = 0;
            }
            if (argDACValue > 5)
            {
                argDACValue = 5;
            }
            switch (argCHannelNumber)
            {
                case 1:

                    muxbit0 = true;
                    muxbit1 = true;
                    muxbit2 = true;
                    muxbit3 = true;
                    break;
                case 2:

                    muxbit0 = false;
                    muxbit1 = true;
                    muxbit2 = true;
                    muxbit3 = true;
                    break;
                case 3:
                    muxbit0 = true;
                    muxbit1 = false;
                    muxbit2 = true;
                    muxbit3 = true;
                    break;
                case 4:
                    muxbit0 = false;
                    muxbit1 = false;
                    muxbit2 = true;
                    muxbit3 = true;
                    break;

                case 5:
                    muxbit0 = true;
                    muxbit1 = true;
                    muxbit2 = false;
                    muxbit3 = true;
                    break;

                case 6:
                    muxbit0 = false;
                    muxbit1 = true;
                    muxbit2 = false;
                    muxbit3 = true;
                    break;
                case 7:
                    muxbit0 = true;
                    muxbit1 = false;
                    muxbit2 = false;
                    muxbit3 = true;
                    break;
                case 8:
                    muxbit0 = false;
                    muxbit1 = false;
                    muxbit2 = false;
                    muxbit3 = true;
                    break;
                case 9:
                    muxbit0 = true;
                    muxbit1 = true;
                    muxbit2 = true;
                    muxbit3 = false;
                    break;
                case 10:
                    muxbit0 = false;
                    muxbit1 = true;
                    muxbit2 = true;
                    muxbit3 = false;
                    break;
                case 11:
                    muxbit0 = true;
                    muxbit1 = false;
                    muxbit2 = true;
                    muxbit3 = false;
                    break;
                case 12:
                    muxbit0 = false;
                    muxbit1 = false;
                    muxbit2 = true;
                    muxbit3 = false;
                    break;
                case 13:

                    muxbit0 = true;
                    muxbit1 = true;
                    muxbit2 = false;
                    muxbit3 = false;
                    break;

                case 14:
                    muxbit0 = false;
                    muxbit1 = true;
                    muxbit2 = false;
                    muxbit3 = false;
                    break;
                case 15:
                    muxbit0 = false;
                    muxbit1 = false;
                    muxbit2 = false;
                    muxbit3 = false;
                    break;
                case 16:
                    muxbit0 = true;
                    muxbit1 = false;
                    muxbit2 = false;
                    muxbit3 = false;
                    break;
                default:
                    muxbit0 = true;
                    muxbit1 = true;
                    muxbit2 = true;
                    muxbit3 = true;
                    break;
            }

            _RAW_DACTOSEND = argDACValue;

            MUXDAC_values_WithFIOS[0] = muxbit0 ? 1 : 0;
            MUXDAC_values_WithFIOS[1] = muxbit1 ? 1 : 0;
            MUXDAC_values_WithFIOS[2] = muxbit2 ? 1 : 0;
            MUXDAC_values_WithFIOS[3] = muxbit3 ? 1 : 0;
            MUXDAC_values_WithFIOS[4] = _RAW_DACTOSEND;

            if (!isOnBus)
            {
                return;
            }

            int errorAddress1 = 0;
            LJM.eWriteNames(handle, Num_MUXDAC_enries, MUXDAC_names_withFIOS, MUXDAC_values_WithFIOS, ref errorAddress1);

        }


        void OnLabJack_FirstInfo_Received(string argSerial, string argFirmware)
        {
            aLabJackDataReceived?.Invoke(argSerial, argFirmware);
        }
        protected void OnFirstMEssageWasReceived(string argVersion)
        {
            FirstMEssageWasReceived?.Invoke(argVersion);
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //if (serialPort != null)
                    //{
                    //    serialPort.Close();
                    //    serialPort.Dispose();
                    //}
                    //if (commTimer != null)
                    //{
                    //    StopTimer();
                    //    commTimer.Dispose();
                    //}
                }
                disposed = true;
            }
        }

        #endregion
    }
}
