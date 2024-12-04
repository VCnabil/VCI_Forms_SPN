using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCI_Forms_SPN._BackEndDataOBJs.OCVObjs;
using static VCI_Forms_SPN._GLobalz.G_Helpers;
namespace VCI_Forms_SPN._GLobalz
{
    public static class EventsManagerLib
    {
        public delegate void EventLogConsole_Handler(string arg_strval);
        public static event EventLogConsole_Handler OnLogConsoleEvent;
        public static void Call_LogConsole(string srg_str)
        {
            OnLogConsoleEvent?.Invoke(srg_str);
        }
        public delegate void EventLogConsoleCLEAR_Handler();
        public static event EventLogConsoleCLEAR_Handler OnLogConsoleEventCLEAR;
        public static void Call_LogConsoleCLEAR()
        {
            OnLogConsoleEventCLEAR?.Invoke();
        }
        public delegate void EventHandBroadcastHandler(string arg_strval, int arg_intval, bool arg_Bool0);
        public static event EventHandBroadcastHandler OnHandBroadcast;
        public static void CallHandBroadcast(string srg_str, int arg_int, bool arg_bool)
        {
            OnHandBroadcast?.Invoke(srg_str, arg_int, arg_bool);
        }
        public delegate void EventHandTickPingPong();
        public static event EventHandTickPingPong OnHandTickPingPong;
        public static void CallHandTickPingPong()
        {
            OnHandTickPingPong?.Invoke();
        }
        // MyOpenCV3 is registered to this and recalcs on option changed from the CU_OCVfilters 
        public delegate void EventHand_BROADCASOCV_filterObj(OCV_Filter_V2 arg_OCVfilterObj);
        public static event EventHand_BROADCASOCV_filterObj On_BROADCAST_OCVOBJevent;
        public static void Call_Broacdast_OCV_OBJ(OCV_Filter_V2 arg_OCVfilterObj)
        {
            On_BROADCAST_OCVOBJevent?.Invoke(arg_OCVfilterObj);
        }


        public delegate void EventHand_PdfPageSizeRead(float arg_width, float arg_height, bool arg_isAtype);
        public static event EventHand_PdfPageSizeRead On_PdfPageSizeRead;
        public static void Call_PdfPageSizeRead(float arg_width, float arg_height, bool arg_isAtype)
        {
            On_PdfPageSizeRead?.Invoke(arg_width, arg_height, arg_isAtype);
        }

        public delegate void EventHand_UpdateActionFilters(OCV_FilterActions arg_ActionFilter);
        public static event EventHand_UpdateActionFilters On_UpdateActionFilter;
        public static void Call_UpdatActionFilte(OCV_FilterActions arg_actionfilter)
        {
            On_UpdateActionFilter?.Invoke(arg_actionfilter);
        }

    }
}
