using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCI_Forms_SPN.LBJK
{
    public class MBIV_RX
    {
        DateTime _dateTimeReceived;
        TimeSpan argintervalSinceLastRX;

        int[] __all_ains;
        int _ain1;
        int _ain2;
        int _ain3;
        int _ain4;
        int _ain5;
        int _ain6;
        int _ain7;
        int _ain8;
        int _ain9;
        int _ain10;
        int _ain11;
        int _ain12;
        int _ain13;
        int _ain14;
        int _ain15;
        int _ain16;


        string _version; 
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }
        public int AIN1
        {
            get { return _ain1; }
            set
            {

                if (value < 0)
                {
                    _ain1 = 0;
                }
                else if (value > 4095)
                {
                    _ain1 = 4095;
                }
                else
                {
                    _ain1 = value;
                }

            }
        }
        public int AIN2
        {
            get { return _ain2; }
            set
            {

                if (value < 0)
                {
                    _ain2 = 0;
                }
                else if (value > 4095)
                {
                    _ain2 = 4095;
                }
                else
                {
                    _ain2 = value;
                }

            }
        }
        public int AIN3
        {
            get { return _ain3; }
            set
            {

                if (value < 0)
                {
                    _ain3 = 0;
                }
                else if (value > 4095)
                {
                    _ain3 = 4095;
                }
                else
                {
                    _ain3 = value;
                }

            }
        }

        public int AIN4
        {
            get { return _ain4; }
            set
            {

                if (value < 0)
                {
                    _ain4 = 0;
                }
                else if (value > 4095)
                {
                    _ain4 = 4095;
                }
                else
                {
                    _ain4 = value;
                }

            }
        }
        public int AIN5
        {
            get { return _ain5; }
            set
            {

                if (value < 0)
                {
                    _ain5 = 0;
                }
                else if (value > 4095)
                {
                    _ain5 = 4095;
                }
                else
                {
                    _ain5 = value;
                }

            }
        }
        public int AIN6
        {
            get { return _ain6; }
            set
            {

                if (value < 0)
                {
                    _ain6 = 0;
                }
                else if (value > 4095)
                {
                    _ain6 = 4095;
                }
                else
                {
                    _ain6 = value;
                }

            }
        }
        public int AIN7
        {
            get { return _ain7; }
            set
            {

                if (value < 0)
                {
                    _ain7 = 0;
                }
                else if (value > 4095)
                {
                    _ain7 = 4095;
                }
                else
                {
                    _ain7 = value;
                }

            }
        }
        public int AIN8
        {
            get { return _ain8; }
            set
            {

                if (value < 0)
                {
                    _ain8 = 0;
                }
                else if (value > 4095)
                {
                    _ain8 = 4095;
                }
                else
                {
                    _ain8 = value;
                }

            }
        }
        public int AIN9
        {
            get { return _ain9; }
            set
            {

                if (value < 0)
                {
                    _ain9 = 0;
                }
                else if (value > 4095)
                {
                    _ain9 = 4095;
                }
                else
                {
                    _ain9 = value;
                }

            }
        }
        public int AIN10
        {
            get { return _ain10; }
            set
            {

                if (value < 0)
                {
                    _ain10 = 0;
                }
                else if (value > 4095)
                {
                    _ain10 = 4095;
                }
                else
                {
                    _ain10 = value;
                }

            }
        }
        public int AIN11
        {
            get { return _ain11; }
            set
            {

                if (value < 0)
                {
                    _ain11 = 0;
                }
                else if (value > 4095)
                {
                    _ain11 = 4095;
                }
                else
                {
                    _ain11 = value;
                }

            }
        }
        public int AIN12
        {
            get { return _ain12; }
            set
            {

                if (value < 0)
                {
                    _ain12 = 0;
                }
                else if (value > 4095)
                {
                    _ain12 = 4095;
                }
                else
                {
                    _ain12 = value;
                }

            }
        }
        public int AIN13
        {
            get { return _ain13; }
            set
            {

                if (value < 0)
                {
                    _ain13 = 0;
                }
                else if (value > 4095)
                {
                    _ain13 = 4095;
                }
                else
                {
                    _ain13 = value;
                }

            }
        }
        public int AIN14
        {
            get { return _ain14; }
            set
            {

                if (value < 0)
                {
                    _ain14 = 0;
                }
                else if (value > 4095)
                {
                    _ain14 = 4095;
                }
                else
                {
                    _ain14 = value;
                }

            }
        }
        public int AIN15
        {
            get { return _ain15; }
            set
            {

                if (value < 0)
                {
                    _ain15 = 0;
                }
                else if (value > 4095)
                {
                    _ain15 = 4095;
                }
                else
                {
                    _ain15 = value;
                }

            }
        }
        public int AIN16
        {
            get { return _ain16; }
            set
            {

                if (value < 0)
                {
                    _ain16 = 0;
                }
                else if (value > 4095)
                {
                    _ain16 = 4095;
                }
                else
                {
                    _ain16 = value;
                }

            }
        }

        public int Get_Stored_AINVal(int cur_auto_channelIndex)
        {
            switch (cur_auto_channelIndex)
            {
                case 0:
                    return -1;
                case 1:
                    return AIN1;
                case 2:
                    return AIN2;
                case 3:
                    return AIN3;
                case 4:
                    return AIN4;
                case 5:
                    return AIN5;
                case 6:
                    return AIN6;
                case 7:
                    return AIN7;
                case 8:
                    return AIN8;
                case 9:
                    return AIN9;
                case 10:
                    return AIN10;
                case 11:
                    return AIN11;
                case 12:
                    return AIN12;
                case 13:
                    return AIN13;
                case 14:
                    return AIN14;
                case 15:
                    return AIN15;
                case 16:
                    return AIN16;
                default:
                    return AIN1;
            }
        }

        public void Update_IntArray_withTimeDate(string argBody, DateTime argDateTimeReceived, TimeSpan arg_timeSinceLast)
        {

            _dateTimeReceived = argDateTimeReceived;
            argintervalSinceLastRX = arg_timeSinceLast;
            Update_INTarra_FromCommaDelimitedString(argBody);
        }

        private void Update_INTarra_FromCommaDelimitedString(string argBody)
        {
            //the argBody  "$VCIA,1.11_Rev5712,4049,4062,4062,4063,4038,4037,4058,4054,4053,4056,4050,4041,4043,4056,4055,4063 ,511,1,6,26,23,46,32,25,23,63"
            //split the string into an array of strings using the comma as the delimiter
            string[] __split = argBody.Split(',');
            //assign the values to the properties
            _version = __split[1];
            _ain1 = Convert.ToInt32(__split[2]);
            _ain2 = Convert.ToInt32(__split[3]);
            _ain3 = Convert.ToInt32(__split[4]);
            _ain4 = Convert.ToInt32(__split[5]);
            _ain5 = Convert.ToInt32(__split[6]);
            _ain6 = Convert.ToInt32(__split[7]);
            _ain7 = Convert.ToInt32(__split[8]);
            _ain8 = Convert.ToInt32(__split[9]);
            _ain9 = Convert.ToInt32(__split[10]);
            _ain10 = Convert.ToInt32(__split[11]);
            _ain11 = Convert.ToInt32(__split[12]);
            _ain12 = Convert.ToInt32(__split[13]);
            _ain13 = Convert.ToInt32(__split[14]);
            _ain14 = Convert.ToInt32(__split[15]);
            _ain15 = Convert.ToInt32(__split[16]);
            _ain16 = Convert.ToInt32(__split[17]);

            __all_ains[0] = -1;
            __all_ains[1] = AIN1;
            __all_ains[2] = AIN2;
            __all_ains[3] = AIN3;
            __all_ains[4] = AIN4;
            __all_ains[5] = AIN5;
            __all_ains[6] = AIN6;
            __all_ains[7] = AIN7;
            __all_ains[8] = AIN8;
            __all_ains[9] = AIN9;
            __all_ains[10] = AIN10;
            __all_ains[11] = AIN11;
            __all_ains[12] = AIN12;
            __all_ains[13] = AIN13;
            __all_ains[14] = AIN14;
            __all_ains[15] = AIN15;
            __all_ains[16] = AIN16;
        }
    }
}
