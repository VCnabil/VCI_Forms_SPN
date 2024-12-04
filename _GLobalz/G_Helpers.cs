using OpenCvSharp;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VCI_Forms_SPN._GLobalz
{
    public static class G_Helpers
    {
        public readonly struct WindowParameters
        {
            public int Width { get; }
            public int Height { get; }
            public WindowParameters(int width, int height)
            {
                Width = width;
                Height = height;
            }
        }
        public static readonly WindowParameters WP_imagebox_p7_p7 = new WindowParameters(1700, 1100);

        #region SysDiag


        public static readonly Dictionary<string, int> ProjectName_ids = new Dictionary<string, int>
        {
            { "NAB1",9999},
            { "theory0 1100 850",9998},
            { "theory2 1100 850",9997},
            { "the actuv 2368 ",2368},
            { "toosmalltoread",9995},
            { "large 5500 3550",9994},
            { "2014",9993},
            { "MS MARIN" , 2314 },
            { "NAB3",9997},
            { "NAB rect1",9996},
            { "Actuv" , 1576 },
            { "MV DEL NORTE" , 2150 },
            { "INB2" , 972 },
            { "INTINTOLI" , 1156 },
            { "NAMJET" , 1363 },
            { "YF08" , 1453 },
            { "YF12" , 1453 },
            { "BEB" , 1474 },
            { "JMUDS USV" , 2030 },
            { "US NAVY SEAMOB" , 2084 },
            { "LADY MARTHA" , 2163 },
            { "Rodman R101 ESB Dual station" , 1312},
            { "ACTUV Surrogate" , 1908 },
            { "HODGDON A239" , 940 },
            { "5GMARINE" , 1402 },
            { "Rodman 617" , 617},
            { "Rodman 397" , 397},
            { "NSSR" , 902 },
            { "RBM BlueBox",1919 },
            { "MS SAN FRANCISCO" , 1094 },
            { "JET EXPRESS WITH RELAY MODULE (MAY 2016)" , 2028 },
            { "Kitsap Commander" , 2781 },
            { "OIW-CCM" , 1165 },
            { "OIW PORTLAND FIREBOAT" , 1565 },
            { "BHC ASTERIA" , 1772 },
            { "MILLENIUM" , 2042 },
            { "Paul Wronowski" , 2289 },
            { "CCM with MRADs" , 2315 },
            { "MV SONOMA" , 2509 },
            { "IN FPB Dvora AD Mk3" , 2633 },
            { "IN FPB Jet Mk3" , 2634 },
            { "IN FPB SHALDAG Batch 2" , 2635 },
            { "IN FPB Shaldag Jet Mk5" , 2748 },
            { "Safeboat CICG" , 2777 },
            { "Birdon BEB" , 2867 },
            { "Bertram" , 3197 },
            { "ST Engineering Landing Craft TB00391" , 3322 },
            { "NUWC (Naval Underwater Center) RIB 9701" , 1510 },
            { "Sea Innovator - Leidos USMI Crew Boat" , 2599 },
            { "Rodman R101 ESB single station" , 1311 },
            { "AL SEER 5G MARINE" , 2808 },
            { "Jessica W" , 1535 },
            { "Yonca-Onuk" , 2937 },
            { "Metalcraft Single Waterjet" , 2938 },
            { "NOMAD" , 2944 },
            { "Birdon Fireboat" , 2953 },
            { "iNAV AUS Demonstration" , 2981 },
            { "SKIM" , 3071 },
            { "SSRS-12" , 3183 },
            { "SSRS K12" , 3282 },
            { "SSRS K9" , 3283 },
            { "N-CCM" , 3289 },
            { "SSRS-12 Retrofit" , 3305 },
            { "ERAF 14 Retrofit" , 3407 },
            { "RS 22m Idar Ulstein" , 3464 },
            { "RBM RS232 Dual-LCD" , 1481 },
            { "INFT" , 1521 },
            { "IN PROTECTOR" , 1982 },
            { "SSRS 20m No3 -207 Upgrade" , 3305 },
            { "ANACONDA" , 914 },
            { "SeaLion 4 CCH" , 3182 },
            { "SEA LION" , 1742 }               ,
            { "Kitsap Ferry Enetai" , 2335 },
            { "Twister 5" , 2918 },
            { "SIS RIB" , 1578 },
            { "STILETTO-RIB" , 1430 },//toothin to read may try inhancing 

            { "KRVE" , 9999 },
            { "KUWAIT MKV" , 9999 },
            { "NYWW" , 9999 },
            { "PV-KOTKA" , 9999 },
            { "RBM-NYPD" , 9999 },
            { "FMS-RBM" , 9999 },
            { "RBM KA2700 Training Module" , 9999 },
            { "VIDAL AB68" , 9999 },
            { "GOLDEN_GATE" , 9999 },
            { "JET EXPRESS" , 9999 },
            { "BlueBox RBM" , 9999 },


        };

        static readonly Dictionary<int, string> _dICT_SysDiagPaths = new Dictionary<int, string>
        {
            { 1576 , @"C:\_____Ufake\SVN_Projects\actuv\drawings\ACTUVSystemDiagram_1576-P9.pdf" },
            { 1402 , @"C:\_____Ufake\SVN_Projects\5g_marine\5G_Sys_Diag_P7.pdf"},
            { 914  , @"C:\_____Ufake\SVN_Projects\anaconda\documentation\Anaconda_Sys_Diag-914_P5.pdf"},
            { 1908 , @"C:\_____Ufake\SVN_Projects\actuv_2\drawings\ACTUVSurrogateSystemDiagram_1908-P3__rotted.pdf" },
            { 2030 , @"C:\_____Ufake\SVN_Projects\jmuds\cad\system_diagram\JMUDS USV Sys Diag - 2030_P2.pdf" },
            { 940  , @"C:\_____Ufake\SVN_Projects\hodgdon_A235\HodgdonYachtsA235-940_P5.pdf" },
            { 972  , @"C:\_____Ufake\SVN_Projects\inb2\project_documentation\inb2_system_diagram.pdf"},
            { 1982 , @"C:\_____Ufake\SVN_Projects\in_protector\cad\system_diagram\IN Protector System Diagram-1982.pdf"},
            { 1521 , @"C:\_____Ufake\SVN_Projects\in_fast_transport\project_documentation\IN Fast Transport Sys Diag - 1521_P7rotted.pdf"},
            { 1363 , @"C:\_____Ufake\SVN_Projects\namjet\project_documentation\NAMJet ESBE SysDiagram - 1363_P2rotted.pdf" },
            { 902  , @"C:\_____Ufake\SVN_Projects\nssr\documentation\NSSR #2_Sys Diag - 902_P4.pdf"},
            { 1565 , @"C:\_____Ufake\SVN_Projects\oiw-fireboat\documentation\oiw_fireboat-P7.pdf"},
            { 1510 , @"C:\_____Ufake\SVN_Projects\legacy_11m_rib\documentation\11M_RIB_System_Diagram_P7.pdf"},
            { 1430 , @"C:\_____Ufake\SVN_Projects\stiletto-rib\documentation\CCD_11M_RIB_System_Diagram_P4rotted.pdf"},
            { 1578 , @"C:\_____Ufake\SVN_Projects\sis-rib\documentation\SIS11MRIBsystemdiagram.pdf"},
            { 1919 , @"C:\_____Ufake\SVN_Projects\rbm\RBM_BlueBox\drawings\RBM Blue Box System Diag-1919-P2.pdf"},
            { 1481 , @"C:\_____Ufake\SVN_Projects\rbm\Dual_RS232based_LCD\RBM_ECP135_System_Diag-1481_P3.pdf"},
            { 1742 , @"C:\_____Ufake\SVN_Projects\sl1m\system_diagram\Sea Lion with DP_Sys Diag-1742_P10.pdf"},
            { 2084 , @"C:\_____Ufake\SVN_Projects\seamob-demo\cad\USMI_SEAMOB_Sys Diag-051601-2084_P6.pdf"},
            { 1772 , @"C:\_____Ufake\SVN_Projects\bhc_asteria\drawings\BHC Asteria Sys Diag - 1772_C.pdf"},
            { 1156 , @"C:\_____Ufake\SVN_Projects\intintoli\initintoli-1156_P8rotted.pdf"},
            { 2028 , @"C:\_____Ufake\SVN_Projects\jet_express\reference_docs\121501_2028_P1.pdf"},
            { 1535 , @"C:\_____Ufake\SVN_Projects\jessica_w\documentation\Jessica_W_Sys_Diag-P3.pdf"},
            { 2163 , @"C:\_____Ufake\SVN_Projects\lady_martha\Lady Martha Sys Diag - 2163-P1.pdf"},
            { 2042 , @"C:\_____Ufake\SVN_Projects\millenium\drawings\Millennium Sys Diag - 2042-Rev5.pdf"},
            { 1094 , @"C:\_____Ufake\SVN_Projects\ms_san_francisco\reference_docs\MSSanFransiscoSysDiag-1094_P4.pdf"},
            { 2150 , @"C:\_____Ufake\SVN_Projects\mv_del_norte\cad\DelNorte_Sys_Diag-2150_P3.pdf"},
            { 2808 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2800-2899\2808 Al Seer 5G Marine G13-G14 SysDiag\2808_-_Al Seer 5G Marine G13-G14 SysDiag.pdf" },
            { 2981 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2900-2999\2981 iNAV AUS Demonstration\2981_P3-PRELIM_iNAV AUS Demo SysDiag.pdf" },
            { 3197 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\3100-3199\3197 Bertram SysDiag\3197_P6_Bertram SysDiag.pdf" },
            { 1474 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\1400-1499\1474 BEB System Diagram\1474_P3 Birdon BEB SysDiag.pdf" }, //which is a namjet 
            { 2953 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2900-2999\2953 Birdon Fireboat SysDiag\2953_P14_Birdon Fireboat SysDiag.pdf" },
            { 2867 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2800-2899\2867 INAV\2867_P2-PRELIM_Birdon INAV SysDiag.pdf" },
            { 2315 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2300-2399\2315 CCM with MRADs\2315_P7_CCM with Vector 2.0 Upgrade SysDiag_1of2.pdf" },
            { 2633 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2600-2699\2633 IN FPB Dvora AD Mk3 Sys Diag\2633_P5.1_IN Dvora AD Mk3 SysDiag.pdf" },
            { 2634 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2600-2699\2634 IN FPB Dvora Jet Mk3 Sys Diag\2634 Drawing Package\2634_P6-PRELIM_IN FPB Dvora Jet Mk3 SysDiag.pdf" },
            { 2635 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2600-2699\2635 IN FPB SHALDAG Batch 2 SysDiag\2635_P7_IN FPB Shaldag Jet Batch2 + Trim SysDiag.pdf" },
            { 2748 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2700-2799\2748 IN FPB Shaldag Jet Mk5\2748_P6_IN FPB Shaldag Jet Mk5 SysDiag.pdf" },
            { 2781 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2700-2799\2781 Kitsap Commander SysDiag\2781_Kitsap Commander SysDiag RevA and Rev-.pdf" },
            { 2335 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2300-2399\2335 Kitsap Ferry Enetai\2335_Kitsap Ferry Enetai SysDiag.pdf" },
            { 2314 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2300-2399\2314 Marin System Diagram\2314_P8_MARIN SysDiag_1of2.pdf" },
            { 3289 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\3200-3299\3289 N-CCM\3289_P1_N-CCM SysDiag.pdf" },
            { 2944 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2900-2999\2944 NOMAD SysDiag\obsolete\2944_P4-WIP-0812_NOMAD SysDiag.pdf" },
            { 1165 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\1100-1199\1165 OIW CCM SysDiag\1165_P17_OIW CCM SysDiag.pdf" },
            { 2289 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2200-2299\2289 Paul Wronowski\2289_P2.7_Paul Wronowski SysDiag.pdf" },
            { 3464 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\3400-3499\3464 RS 22m Idar Ulstein\3464_P2_RS 22m Idar Ulstein SysDiag.pdf" },
            { 2777 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2700-2799\2777 Safeboat CICG Sys Diag\2777_P4_Safeboat CICG SysDiag.pdf" },
            { 2599 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2500-2599\2599 Sea Innovator - Leidos USMI Crew Boat\2599_P6-PRELIM_Sea Innovator - Leidos USMI Crew Boat - Sys Diag.pdf" },
            { 3182 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\3100-3199\3182 SeaLion 4 CCH\3182_P1_SeaLion 4 CCH with DP SysDiag 1of2.pdf" },
            { 2938 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2900-2999\2938 Metalcraft Single Waterjet SysDiag\2938_P11-6_SeaMachines OTH SysDiag.pdf" },
            { 3298 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\3200-3299\3298 Selacia\obsolete\3298_P1.6_Salacia SysDiag_1of4.pdf" },
            { 3071 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\3000-3099\3071 NY DEP SKIMMER\3071_P6-PRELIM_Skimmer SysDiag.pdf" },
            { 2509 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2500-2599\2509 MV SONOMA\2509_P12_MV Sonoma SysDiag 1of2.pdf" },
            { 3305 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\3300-3399\3305 SSRS 20m No3 -207 Upgrade\3305_P2.3_SSRS 20m no3 Retrofit SysDiag.pdf" },
            { 3282 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\3200-3299\3282 SSRS K12\3282_P10_SSRS K12 SysDiag.pdf" },
            { 3283 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\3200-3299\3283 SSRS K9\3283_P1_SSRS K9 SysDiag.pdf" },
            { 3183 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\3100-3199\3183 SSRS-12 SysDiag\3183_P5_SSRS-12_SysDiag.pdf" },
            { 3322 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\3300-3399\3322 ST Engineering Landing Craft TB00391\3322_P1.5_ST Engineering Landing Craft TB00391 SysDiag.pdf" },
            { 3407 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\3400-3499\3407 ERAF 14 Retrofit SysDiag\3407_P1.4_STEng ERAF 14 Retrofit SysDiag.pdf" },
            { 2918 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2900-2999\2918 Twister 5 SysDiag\2918_P1-PRELIM_Twister 5 SysDiag.pdf" },
            { 1453 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\1400-1499\1453 YF12 System Diagram\1453-P2_YF12 System Diagram.pdf" },
            { 2937 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2900-2999\2937 Yonca-Onuk SysDiag\2937_P6_Yonca-Onuk SysDiag.pdf" },
           // { 2938 , @"C:\_____Ufake\Design__Source_Files\System Diagrams\2900-2999\2938 Metalcraft Single Waterjet SysDiag\2938_P11_Metalcraft Single Jet with Levers and Joystick SysDiag.pdf" },
            { 9999, @"C:\_____Ufake\SVN_Projects\__tempDir\PDF_PXrotted.pdf"}, //KRVE hasno pdf i copied the one above for now. KUWAIT MKV as well t is no a boat and NYWW
            { 9998, @"C:\_____Ufake\SVN_Projects\__tempDir\PDF_1100x850_theory_0.pdf"},
            { 9997, @"C:\_____Ufake\SVN_Projects\__tempDir\2335_F3-PRELIM_Kitsap Ferry Enetai SysDiag_F2 MARKUP WEL 20200721.pdf"},
            { 9996, @"C:\_____Ufake\SVN_Projects\__tempDir\PDF_test3.pdf"},
            { 9995, @"C:\_____Ufake\SVN_Projects\__tempDir\PDF_justopensave.pdf"},
            { 9994, @"C:\_____Ufake\SVN_Projects\__tempDir\PDF_Large_5500x3550_0.pdf"},
            { 9993, @"C:\_____Ufake\SVN_Projects\__tempDir\2014_Control Unit SF_A.pdf"},
            { 617, @"C:\_____Ufake\__unique_sysDiags\TempOCV_deleteMe\Rodman SysDiagram - 617_P3.pdf"},
            { 397, @"C:\_____Ufake\__unique_sysDiags\TempOCV_deleteMe\Rodman Sys Diag-400.pdf"},
            { 1311, @"C:\_____Ufake\__unique_sysDiags\TempOCV_deleteMe\Rodman 101 Single Station SysDiagram - 1311_P2.pdf"},
            { 1312, @"C:\_____Ufake\__unique_sysDiags\TempOCV_deleteMe\Rodman R101 (P2057) System Diagram - 1312_P4.pdf"},
            { 2368 ,@"C:\_____Ufake\Design__Source_Files\System Diagrams\2300-2399\2368 Actuv #2 System Diagram\ACTUV #2 System Diagram-2368_P3.pdf"}
        };//2335_F3-PRELIM_Kitsap Ferry Enetai SysDiag_F2 MARKUP WEL 20200721.pdf

        public static string Get_SysDiagStaticPath(string argProjName)
        {

            int argProjectNumber = ProjectName_ids[argProjName];
            return _dICT_SysDiagPaths[argProjectNumber];
        }

        #endregion

        public static int AdjustReolution(int argResolution)
        {
            float a = 120f / 125f;
            float b = -540f;

            float reult = a * argResolution + b;
            int relutInt = (int)reult;

            reult = (float)argResolution / 2;
            relutInt = (int)reult;
            return relutInt;
        }
        public enum enum_OCVBlurAction
        {
            BLUR = 0,
            GAUSSIAN = 1,
            MEDIAN = 2,
            PYRDOWN = 3,
            PYRUP = 4,
        }
        public static string EnumToString_OCVBlurAction(enum_OCVBlurAction argOcvBlurtype)
        {
            return argOcvBlurtype.ToString();
        }
        public static enum_OCVBlurAction StringToEnum__OCVBlurAction(string argOcvBlurStr)
        {
            if (Enum.TryParse(argOcvBlurStr, out enum_OCVBlurAction ocvblurtypeEnumed))
            {
                return ocvblurtypeEnumed;
            }
            else
            {
                throw new ArgumentException("fyi folders inCode_surce are named differently Invalid string for OCVBlurtypes enum conversion ");
            }
        }

        public enum enum_OCVThresholdAction
        {
            STD = 0,
            ADAPTIVE = 1,

        }
        public static string EnumToString_OCVThresholdAction(enum_OCVThresholdAction argOcvThresholdtype)
        {
            return argOcvThresholdtype.ToString();
        }
        public static enum_OCVThresholdAction StringToEnum__OCVThresholdType(string argOcvThresholdStr)
        {
            if (Enum.TryParse(argOcvThresholdStr, out enum_OCVThresholdAction ocvThresholdtypeEnumed))
            {
                return ocvThresholdtypeEnumed;
            }
            else
            {
                throw new ArgumentException("fyi folders inCode_surce are named differently Invalid string for Chip enum conversion ");
            }
        }

        public enum SysDiagP_PreFetchedListToUse
        {
            //C_Test uses a test file with paths to mu ufake svn projects and other s . it is the same as the dictionary in G_Props 
            C_Test = 0,
            //C_____Udrive uses a file with paths to my fake Udrive projects . it is a copy of the real Udrive
            C_____Udrive = 1,
            // Udrive uses a file with paths to the real Udrive pdfs 
            Udrive = 2,

        }
        public static List<string> Available_PrefetchedList(bool arg_UdriveConnected)
        {
            if (arg_UdriveConnected)
            {
                return new List<string> { "C_Test", "C_____Udrive", "Udrive" };
            }
            else
            {
                return new List<string> { "C_Test", "C_____Udrive" };
            }
        }
        public static string EnumToString_SysDiagP_PreFetchedListToUse(SysDiagP_PreFetchedListToUse argSysDiagP_PreFetchedListToUse)
        {
            return argSysDiagP_PreFetchedListToUse.ToString();
        }

        public static SysDiagP_PreFetchedListToUse StringToEnum_SysDiagP_PreFetchedListToUse(string argSysDiagP_PreFetchedListToUseStr)
        {
            if (Enum.TryParse(argSysDiagP_PreFetchedListToUseStr, out SysDiagP_PreFetchedListToUse sysDiagP_PreFetchedListToUseEnumed))
            {
                return sysDiagP_PreFetchedListToUseEnumed;
            }
            else
            {
                throw new ArgumentException("fyi folders inCode_surce are named differently Invalid string for Chip enum conversion ");
            }
        }

        public enum SysDiagPDFRetreivalMethod
        {
            STATIC = 0,
            SAFE = 1,
            LIVE = 2,
            TEST = 3,
        }

        public static string EnumToString_ThresholdTypes(ThresholdTypes argOCVthreshtype)
        {
            return argOCVthreshtype.ToString();
        }

        public static ThresholdTypes StringToEnum_ThresholdTypes(string argOCVthreshtypeStr)
        {
            if (Enum.TryParse(argOCVthreshtypeStr, out ThresholdTypes ocvthreshtypeEnumed))
            {
                return ocvthreshtypeEnumed;
            }
            else
            {
                throw new ArgumentException("fyi folders inCode_surce are named differently Invalid string for Chip enum conversion ");
            }
        }


        public static string EnumToString_BorderTypes(BorderTypes argBorderTypes)
        {
            return argBorderTypes.ToString();
        }

        public static BorderTypes StringToEnum_BorderTypes(string argBorderTypesStr)
        {
            if (Enum.TryParse(argBorderTypesStr, out BorderTypes borderTypesEnumed))
            {
                return borderTypesEnumed;
            }
            else
            {
                throw new ArgumentException("fyi folders inCode_surce are named differently Invalid string for Chip enum conversion ");
            }
        }


        #region Gprops

        public static readonly float G_ROI_LowerLeft_percent_Width_min = 44.0f;
        public static readonly float G_ROI_LowerLeft_percent_Width_max = 51.0f;
        public static readonly float G_ROI_LowerLeft_percent_Height_min = 40.0f;
        public static readonly float G_ROI_LowerLeft_percent_Height_max = 51.0f;
        public static readonly float G_ROI_LowerRight_percent_Width_min = 20.0f;
        public static readonly float G_ROI_LowerRight_percent_Width_max = 40.0f;
        public static readonly float G_ROI_LowerRight_percent_Height_min = 18.0f;
        public static readonly float G_ROI_LowerRight_percent_Height_max = 25.0f;
        public static readonly int G_PdfheightNeeded = 1100;
        public static readonly int G_PdfwidthNeeded = 1700;
        public static readonly int G_PdfDPI_needed = 300;
        public static readonly int G_TempIntX_0_900 = 300;
        public static readonly int G_TempIntY_0_900 = 500;
        public static readonly int G_Pdf_0_GrayThr = 183;
        public static readonly int G_Pdf_1_GrayMaxVal = 2;
        public static readonly ThresholdTypes G_Pdf_tt = ThresholdTypes.BinaryInv;
        public static readonly enum_OCVBlurAction G_Blur_TypeToUse = enum_OCVBlurAction.GAUSSIAN;
        public static readonly BorderTypes G_bLurBorderType = BorderTypes.Constant;
        public static readonly int G_Blur_ksize = 19;
        public static readonly double G_BlurSigmax = 1.119;
        public static readonly double G_BlurSigmay = 1.538; //1.56
        public static readonly enum_OCVThresholdAction G_ThresholdTypeToUse = enum_OCVThresholdAction.ADAPTIVE;
        public static readonly double G_StdThreshThr = 200;
        public static readonly double G_StdThreshMaxV = 255;
        public static readonly ThresholdTypes G_StdThres_tt = ThresholdTypes.BinaryInv;
        public static readonly AdaptiveThresholdTypes G_AdaptiveThresh_at = AdaptiveThresholdTypes.GaussianC;
        public static readonly ThresholdTypes G_AdaptiveThresh_tt = ThresholdTypes.Binary;
        public static readonly ThresholdTypes G_AdaptiveThresh_tt2 = ThresholdTypes.BinaryInv;
        public static readonly int G_AdaptiveThreshBlockSize = 5;
        public static readonly double G_AdaptiveThreshC = 0.084;
        public static readonly double G_AdaptiveThreshMaxV = 255;
        public static readonly double G_AdaptiveThreshThresh = 250;

        public static readonly double G_Alpha = 1.5;
        public static readonly double G_Beta = -0.5;
        public static readonly double G_Gamma = 0;

        public static readonly int G_Adp2 = 0;
        public static readonly int G_Adp3 = 0;


        public static readonly double G_CannyThr1 = 100;
        public static readonly double G_CannyThr2 = 250;
        public static readonly double G_LinesP_rho = 1.18;
        public static readonly int G_LinesP_thetaMultiplyer = 1;
        public static readonly int G_LinesP_Thr = 100;
        public static readonly double G_LinesP_YGap = 20;
        public static readonly double G_LinesP_XGap = 20;

        public static readonly double G_LinesP_R_minLen = 160;
        public static readonly double G_LinesP_R_maxGap = 6;
        public static readonly int G_Strip_R_width = 340;
        public static readonly int G_Strip_R_Margin = 4;
        public static readonly int G_Strip_R_height = 178;
        public static readonly int G_Strip_R_botMargin = 21;


        public static readonly double G_LinesP_L_minLen = 280;
        public static readonly double G_LinesP_L_maxGap = 30;
        public static readonly int G_Strip_L_width = 300;
        public static readonly int G_Strip_L_Margin = 14;
        public static readonly int G_Strip_L_height = 180;
        public static readonly int G_Strip_L_TopLimit = 14;

        public static readonly double G_LinesP_V_minLen = 160;
        public static readonly double G_LinesP_V_maxGap = 30;
        public static readonly int G_Strip_V_width = 900;
        public static readonly int G_Strip_V_Margin = 14;
        public static readonly int G_Strip_V_height = 400;
        public static readonly int G_Strip_V_TopLimit = 20;

        public static readonly int G_LinesP_MaxDy = 4;
        public static readonly int G_LinesP_MaxDx = 4;

        public static readonly double G_LinesP_PercentHeight = 10.00;
        public static readonly double G_LinesP_PercentWidth = 80.00;
        public static  SysDiagPDFRetreivalMethod G_PDF_retreivalMethod = SysDiagPDFRetreivalMethod.STATIC;
        #endregion

        public static readonly string[] IMMs1000 = new string[] {
            "265032380",
            "338205553",
            "367447520",
            "227071210",
            "228348600",
            "366771550",
            "367367470",
            "368151810",
            "540017830",
            "204206750",
            "204226000",
            "204670570",
            "204706000",
            "205022300",
            "205065000",
            "205096000",
            "205220390",
            "205244890",
            "205247890",
            "205262190",
            "205264000",
            "205270690",
            "205360490",
            "205362290",
            "205386390",
            "205391690",
            "205400610",
            "205415000",
            "205417000",
            "205429390",
            "205458990",
            "205501290",
            "205504190",
            "205505790",
            "205530490",
            "205533690",
            "205540290",
            "205549290",
            "205565000",
            "205603000",
            "205665000",
            "205690000",
            "209275000",
            "209453000",
            "209541000",
            "209670000",
            "209862000",
            "210108000",
            "210195000",
            "210438000",
            "210482000",
            "210595000",
            "210754000",
            "211113010",
            "211116250",
            "211173680",
            "211203630",
            "211207340",
            "211208350",
            "211209290",
            "211217860",
            "211220930",
            "211223120",
            "211225940",
            "211234090",
            "211265240",
            "211265620",
            "211277260",
            "211283490",
            "211304260",
            "211315270",
            "211316470",
            "211317180",
            "211389510",
            "211417420",
            "211431270",
            "211436370",
            "211443640",
            "211467050",
            "211471880",
            "211472760",
            "211476070",
            "211477050",
            "211487620",
            "211495540",
            "211496350",
            "211507020",
            "211509480",
            "211511240",
            "211511770",
            "211549500",
            "211560830",
            "211564500",
            "211588220",
            "211590150",
            "211626350",
            "211662340",
            "211706350",
            "211706910",
            "211712640",
            "211757190",
            "211759860",
            "211773470",
            "211775550",
            "211788910",
            "211838230",
            "211881920",
            "211888150",
            "211889210",
            "211891330",
            "215100000",
            "215238000",
            "215845000",
            "218001460",
            "219000158",
            "219000429",
            "219002005",
            "219005866",
            "219005904",
            "219014974",
            "219016577",
            "219021266",
            "219022256",
            "219025783",
            "219027308",
            "219027995",
            "219030593",
            "219031279",
            "219032375",
            "219530000",
            "219812000",
            "219963000",
            "224006950",
            "224009880",
            "224098640",
            "224100530",
            "224134540",
            "224139000",
            "224297000",
            "224322240",
            "224537560",
            "224761000",
            "224906190",
            "225019990",
            "225988173",
            "225990254",
            "226001810",
            "226006730",
            "226013280",
            "226015910",
            "226016370",
            "226016380",
            "227006760",
            "227009790",
            "227138700",
            "227563000",
            "227658910",
            "227671950",
            "227713010",
            "227969170",
            "228008900",
            "228166600",
            "228179700",
            "229164000",
            "229410000",
            "230063110",
            "230184000",
            "230628000",
            "230705000",
            "230981770",
            "232002521",
            "232006087",
            "232009818",
            "232022358",
            "232025014",
            "232031554",
            "232033197",
            "232033454",
            "232033759",
            "232038123",
            "232039060",
            "234789000",
            "235006582",
            "235007573",
            "235011240",
            "235023045",
            "235032615",
            "235053188",
            "235061705",
            "235067293",
            "235073592",
            "235073785",
            "235075339",
            "235078345",
            "235092103",
            "235093299",
            "235100258",
            "235101102",
            "235108809",
            "235111195",
            "235118697",
            "235640000",
            "237046010",
            "237746400",
            "237829800",
            "239005400",
            "240059300",
            "240150000",
            "240346900",
            "240919000",
            "241000000",
            "241113000",
            "241327000",
            "241347000",
            "241444000",
            "241753000",
            "241804000",
            "242092000",
            "242125100",
            "242400000",
            "244000542",
            "244010283",
            "244010963",
            "244013126",
            "244013600",
            "244020823",
            "244020828",
            "244024561",
            "244026204",
            "244030275",
            "244052165",
            "244059204",
            "244060306",
            "244070219",
            "244070903",
            "244070982",
            "244100377",
            "244100907",
            "244100998",
            "244110235",
            "244110703",
            "244130015",
            "244130327",
            "244130505",
            "244131369",
            "244110007",
            "244150499",
            "244150566",
            "244150604",
            "244170620",
            "244170642",
            "244170814",
            "244170977",
            "244216785",
            "244218293",
            "244236641",
            "244245000",
            "244250731",
            "244260494",
            "244260499",
            "244287000",
            "244375541",
            "244534000",
            "244579288",
            "244615094",
            "244615694",
            "244620848",
            "244620984",
            "244620991",
            "244630202",
            "244630271",
            "244630658",
            "244650787",
            "244650812",
            "244650920",
            "244650941",
            "244660078",
            "244660223",
            "244660309",
            "244660370",
            "244660619",
            "244660649",
            "244660700",
            "244660722",
            "244660729",
            "244660936",
            "244670035",
            "244670258",
            "244670292",
            "244670300",
            "244670355",
            "244670392",
            "244670408",
            "244670571",
            "244670609",
            "244670642",
            "244670872",
            "244670958",
            "244670968",
            "244690067",
            "244690108",
            "244690181",
            "244690409",
            "244690485",
            "244690573",
            "244690750",
            "244690898",
            "244690925",
            "244690976",
            "244700161",
            "244700288",
            "244700444",
            "244700494",
            "244700506",
            "244700532",
            "244700632",
            "244700651",
            "244700680",
            "244700704",
            "244700724",
            "244700780",
            "244700781",
            "244710092",
            "244710206",
            "244710226",
            "244710229",
            "244710289",
            "244710305",
            "244710524",
            "244710560",
            "244710744",
            "244710879",
            "244710886",
            "244721362",
            "244730014",
            "244730312",
            "244730574",
            "244730659",
            "244730905",
            "244730912",
            "244730961",
            "244731686",
            "244740076",
            "244740107",
            "244740200",
            "244740772",
            "244740872",
            "244740972",
            "244742334",
            "244750200",
            "244750243",
            "244750256",
            "244750265",
            "244750348",
            "244770067",
            "244770077",
            "244770192",
            "244770385",
            "244770606",
            "244779000",
            "244780168",
            "244780281",
            "244780485",
            "244780611",
            "244780795",
            "244790541",
            "244810805",
            "244839000",
            "244850336",
            "244850769",
            "244860262",
            "244860892",
            "244870122",
            "244870290",
            "244870448",
            "244929000",
            "244959000",
            "244974000",
            "244994000",
            "245255000",
            "245266000",
            "245272000",
            "245364000",
            "245370000",
            "245384000",
            "245439000",
            "245445000",
            "245450000",
            "245473000",
            "245572000",
            "245813000",
            "245872000",
            "245916000",
            "246022000",
            "246092000",
            "246125000",
            "246178000",
            "246206000",
            "246226000",
            "246303000",
            "246437000",
            "246447000",
            "246488000",
            "246504000",
            "246511000",
            "246514000",
            "246586000",
            "246970000",
            "246994000",
            "247054200",
            "247114000",
            "247150240",
            "247171200",
            "247215600",
            "247247247",
            "247256400",
            "247295700",
            "247335800",
            "247336600",
            "247340700",
            "247354500",
            "247355500",
            "247373800",
            "247444500",
            "247451300",
            "248326000",
            "248655000",
            "248906000",
            "249127000",
            "249912000",
            "249934000",
            "249997000",
            "250003659",
            "250005313",
            "250098000",
            "250116470",
            "252018148",
            "253000039",
            "253242448",
            "253778000",
            "255801590",
            "255805557",
            "255805601",
            "255805652",
            "255805740",
            "255805761",
            "255805984",
            "255806013",
            "255806189",
            "255878000",
            "255915652",
            "255915686",
            "255955000",
            "256002554",
            "256056000",
            "256065000",
            "256644000",
            "256929000",
            "256937000",
            "257001950",
            "257003970",
            "257024820",
            "257028310",
            "257034920",
            "257035110",
            "257040700",
            "257053350",
            "257058280",
            "257075250",
            "257087420",
            "257092500",
            "257099320",
            "257113000",
            "257125290",
            "257125770",
            "257127880",
            "257147920",
            "257201000",
            "257214000",
            "257215600",
            "257230000",
            "257236400",
            "257238500",
            "257239600",
            "257240000",
            "257285400",
            "257294720",
            "257299500",
            "257302140",
            "257308000",
            "257308600",
            "257320600",
            "257335800",
            "257343400",
            "257381400",
            "257456500",
            "257485800",
            "257522600",
            "257602000",
            "257614600",
            "257649500",
            "257707600",
            "257807490",
            "257827700",
            "257852000",
            "257893800",
            "257927000",
            "257941000",
            "258006890",
            "258007770",
            "258022850",
            "258027580",
            "258028180",
            "258028210",
            "258064000",
            "258112000",
            "258221000",
            "258224500",
            "258342000",
            "258489000",
            "258499000",
            "258509000",
            "258527000",
            "258573000",
            "258577000",
            "258659000",
            "258713000",
            "258715000",
            "258751000",
            "258824000",
            "259004190",
            "259029500",
            "259030000",
            "259035000",
            "259139000",
            "259216000",
            "259225000",
            "259449000",
            "259458000",
            "259488000",
            "259540000",
            "259599000",
            "259622000",
            "259951000",
            "259994000",
            "261002095",
            "261036120",
            "263411470",
            "263441300",
            "263602552",
            "265000000",
            "265001070",
            "265008590",
            "265041000",
            "265504700",
            "265522220",
            "265522500",
            "265575480",
            "265576690",
            "265579090",
            "265579740",
            "265609080",
            "265609530",
            "265628500",
            "265632540",
            "265650820",
            "265698740",
            "265762770",
            "265814500",
            "265960000",
            "266066000",
            "266348000",
            "266425000",
            "267920000",
            "269057168",
            "269057517",
            "269057695",
            "269057730",
            "271042197",
            "273113600",
            "273290530",
            "273318820",
            "273350110",
            "273385770",
            "273398460",
            "273424720",
            "273436210",
            "273447510",
            "273542210",
            "273612880",
            "273851100",
            "273890200",
            "273894710",
            "275031000",
            "275515000",
            "276718000",
            "277493000",
            "277552000",
            "303159000",
            "303308000",
            "303364000",
            "304048000",
            "304080000",
            "304087000",
            "304267000",
            "304922000",
            "305040000",
            "305198000",
            "305279000",
            "310829000",
            "311000130",
            "311000974",
            "311001002",
            "311064200",
            "311263000",
            "312009000",
            "312216000",
            "312786000",
            "314199000",
            "314641000",
            "316001193",
            "316004240",
            "316005621",
            "316005713",
            "316006025",
            "316006345",
            "316009843",
            "316011245",
            "316012308",
            "316015546",
            "316022287",
            "316025866",
            "316038603",
            "316047943",
            "316212000",
            "319122700",
            "319146400",
            "319152300",
            "319224700",
            "319322000",
            "319489000",
            "319571000",
            "319729000",
            "338141000",
            "338185000",
            "338185000",
            "338250000",
            "338362987",
            "338371000",
            "338371000",
            "338384000",
            "338387252",
            "338638000",
            "338926003",
            "339197000",
            "339295000",
            "339328000",
            "339544000",
            "339857000",
            "341002001",
            "341239001",
            "341451001",
            "351117000",
            "351340000",
            "352001436",
            "352001488",
            "352001781",
            "352002181",
            "352002310",
            "352002925",
            "352003413",
            "352003522",
            "352986159",
            "352999714",
            "353052000",
            "354294000",
            "355390000",
            "355893000",
            "355899000",
            "355997000",
            "356161000",
            "356440000",
            "362029000",
            "365683530",
            "366235000",
            "366279000",
            "366853070",
            "366913820",
            "366916840",
            "366918650",
            "366929710",
            "366961280",
            "366971980",
            "366988450",
            "366993010",
            "366993250",
            "366995290",
            "367017440",
            "367046890",
            "367053260",
            "367081260",
            "367081260",
            "367101530",
            "367110280",
            "367114050",
            "367125050",
            "367135370",
            "367139510",
            "367157570",
            "367199310",
            "367355240",
            "367360430",
            "367394780",
            "367394940",
            "367404860",
            "367422010",
            "367424810",
            "367425520",
            "367480140",
            "367539090",
            "367543280",
            "367552410",
            "367568460",
            "367570750",
            "367586750",
            "367590270",
            "367590780",
            "367592590",
            "367658440",
            "367661930",
            "367672140",
            "367689060",
            "367693690",
            "367694720",
            "367705530",
            "367712660",
            "367712660",
            "367717450",
            "367731530",
            "367738470",
            "367746670",
            "367752540",
            "367776270",
            "367778010",
            "367796040",
            "367797260",
            "368025250",
            "368049580",
            "368056740",
            "368056750",
            "368106340",
            "368110390",
            "368142910",
            "368149000",
            "368167460",
            "368215660",
            "368217570",
            "368237190",
            "368306980",
            "368319530",
            "368319530",
            "368322980",
            "368331910",
            "368333690",
            "368339380",
            "368612000",
            "368926285",
            "369020000",
            "369234000",
            "369493027",
            "369970406",
            "369970571",
            "369990144",
            "369994000",
            "370472000",
            "371782000",
            "372113000",
            "372820000",
            "373009000",
            "373279000",
            "373832000",
            "374182000",
            "374548000",
            "374744000",
            "374829000",
            "375836000",
            "376117000",
            "377128000",
            "377716000",
            "377906076",
            "377907128",
            "412036390",
            "412458260",
            "412478810",
            "412501720",
            "412765420",
            "413052520",
            "413220630",
            "413238280",
            "413253470",
            "413258710",
            "413264720",
            "413291680",
            "413295210",
            "413305810",
            "413313150",
            "413319110",
            "413369960",
            "413377080",
            "413377710",
            "413392590",
            "413458350",
            "413473540",
            "413488410",
            "413492730",
            "413513530",
            "413540250",
            "413543130",
            "413592260",
            "413696510",
            "413697130",
            "413703680",
            "413827764",
            "413875047",
            "414543000",
            "414637000",
            "414674000",
            "416002225",
            "416002956",
            "416003617",
            "416003923",
            "416004118",
            "416004127",
            "416004602",
            "416005452",
            "416008937",
            "416042000",
            "416343000",
            "428067000",
            "431000892",
            "431001040",
            "431001081",
            "431010068",
            "431011957",
            "431018625",
            "431101046",
            "431301812",
            "432943000",
            "440005120",
            "440017150",
            "440107310",
            "440133000",
            "440233000",
            "440249000",
            "440324850",
            "440363000",
            "440409260",
            "440417000",
            "441294000",
            "441890000",
            "450613000",
            "457535000",
            "470000088",
            "470125000",
            "470532000",
            "471182000",
            "477066600",
            "477110900",
            "477117400",
            "477125800",
            "477271500",
            "477335800",
            "477441900",
            "477547100",
            "477598400",
            "477850500",
            "477890600",
            "477907700",
            "477940600",
            "477995179",
            "477995228",
            "477996086",
            "477996966",
            "503026110",
            "503113360",
            "503137590",
            "503345400",
            "503354000",
            "503382000",
            "503720000",
            "503725000",
            "503727000",
            "511101247",
            "512000322",
            "512143000",
            "512263000",
            "518998911",
            "518998923",
            "525007132",
            "525008022",
            "525011085",
            "525015104",
            "525018154",
            "525103009",
            "525113005",
            "525127006",
            "525301327",
            "525401047",
            "525800162",
            "533000296",
            "533130407",
            "538002125",
            "538005087",
            "538005197",
            "538005231",
            "538005402",
            "538005550",
            "538005866",
            "538007335",
            "538007385",
            "538008690",
            "538009683",
            "538009897",
            "538009965",
            "538010815",
            "538070032",
            "538071354",
            "538071617",
            "538072012",
            "548002100",
            "563009850",
            "563015800",
            "563022820",
            "563024400",
            "563051600",
            "563056100",
            "563071760",
            "563073220",
            "563073360",
            "563083400",
            "563090100",
            "563147600",
            "563178100",
            "563180400",
            "563207500",
            "563562000",
            "564316000",
            "564428000",
            "565243000",
            "565369000",
            "565378000",
            "565733000",
            "565957000",
            "566009000",
            "566133000",
            "566293000",
            "566608000",
            "566895000",
            "567002160",
            "567348000",
            "574004420",
            "574012055",
            "574013289",
            "574013456",
            "574014405",
            "574090909",
            "577331000",
            "601333500",
            "605046150",
            "613407803",
            "636016423",
            "636016912",
            "636017000",
            "636017514",
            "636018332",
            "636019234",
            "636019574",
            "636019913",
            "636019986",
            "636020352",
            "636021491",
            "636021710",
            "636022054",
            "636023665",
            "636092391",
            "636092680",
            "636093194",
            "636093239",
            "645281000",
            "645588000",
            "667001345",
            "667002049",
            "701006066",
            "701024000",
            "701123000",
            "701147000",
            "701151000",
            "710003756",
            "710005050",
            "710006591",
            "710026530",
            "710030380",
            "710030820",
            "710031050",
            "710102039",
            "720001258",
            "725011900",
            "730153594"
                    };

        public static List<string> GetList_top_n_IMMs(int argFirsXelemnts)
        {
            return IMMs1000.Take(argFirsXelemnts).ToList();

        }
    }
    public static class StreamReaderExtensions
    {
        public static async Task<string> ReadLineAsync(this StreamReader reader, CancellationToken cancellationToken)
        {
            var task = reader.ReadLineAsync();
            using (cancellationToken.Register(() => task.Dispose()))
            {
                return await task;
            }
        }
    }

    public static class PdfSysDiagnosticFinder
    {
        public static List<string> FindPdfFiles_Sys_Diag(string rootDirectory)
        {
            List<string> matchingFiles = new List<string>();
            try
            {
                // Use EnumerateFiles to find all .pdf files in all subdirectories
                IEnumerable<string> pdfFiles = Directory.EnumerateFiles(rootDirectory, "*.pdf", SearchOption.AllDirectories);

                // Regular expression to match file names (case-insensitive)
                Regex regex = new Regex("sys(diag)?( diag)?", RegexOptions.IgnoreCase);

                foreach (string file in pdfFiles)
                {
                    string fileName = Path.GetFileName(file);
                    // Check if the file name matches the criteria
                    if (regex.IsMatch(fileName))
                    {
                        matchingFiles.Add(file);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return matchingFiles;
        }

        public static List<string> FindPdfFiles_pdf_sizematche(string rootDirectory)
        {
            List<string> matchingFiles = new List<string>();

            int maxFond = 100;
            int curfound = 0;
            try
            {
                // Use EnumerateFiles to find all .pdf files in all subdirectories
                IEnumerable<string> pdfFiles = Directory.EnumerateFiles(rootDirectory, "*.pdf", SearchOption.AllDirectories);

                foreach (string file in pdfFiles)
                {
                    curfound++;
                    if (curfound > maxFond)
                    {
                        break;
                    }
                    // Load the PDF document to check its dimensions
                    using (PdfDocument document = PdfDocument.Load(file))
                    {
                        // Assuming you want to check the dimensions of the first page
                        var pageSize = document.PageSizes[0]; // PageSizes is a list of SizeF, [0] is the first page

                        // Check the dimensions against the specified criteria
                        if ((pageSize.Width >= 790 && pageSize.Width <= 794 && pageSize.Height >= 610 && pageSize.Height <= 614) ||
                            (pageSize.Height >= 790 && pageSize.Height <= 794 && pageSize.Width >= 610 && pageSize.Width <= 614) ||
                            (pageSize.Width >= 1220 && pageSize.Width <= 1228 && pageSize.Height >= 790 && pageSize.Height <= 794) ||
                            (pageSize.Height >= 1220 && pageSize.Height <= 1228 && pageSize.Width >= 790 && pageSize.Width <= 794))
                        {
                            matchingFiles.Add(file);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return matchingFiles;
        }

        public static List<string> LoadExcludedPaths(string exclusionFilePath)
        {
            try
            {
                // Read all lines from the exclusion file path into a List
                List<string> excludedPaths = new List<string>(File.ReadAllLines(exclusionFilePath));
                return excludedPaths;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while loading excluded paths: {e.Message}");
                return new List<string>(); // Return an empty list on error
            }
        }
        private static bool IsFileExcluded(string filePath, List<string> excludedPaths)
        {
            // a file stringe is a path to a file like : C:\_____Ufake\Design__Source_Files\Electronic PCBs\091415-1732 INFAST Dimming Circuit\Schematic and Layout\INFAST Dimming Circuit_TOP.pdf
            // Normalize the file path for accurate comparison
            string normalizedFilePath = Path.GetFullPath(filePath).ToLowerInvariant();

            foreach (var excludedPath in excludedPaths)
            {
                // Normalize the excluded path for comparison
                string normalizedExcludedPath = Path.GetFullPath(excludedPath).ToLowerInvariant();

                if (normalizedFilePath == normalizedExcludedPath)
                {
                    return true; // The file path matches one of the excluded paths exactly
                }
            }
            return false;


        }

        public static List<string> FindPdfFiles_pdf_sizematch(string rootDirectory, string outputPath)
        {

            rootDirectory = @"U:\_Nabil\"; //@"U:\Dave Ohanian\Projects\";// @"C:\_____Ufake\";  U:\Dave Ohanian\Projects\
            //rootDirectory = @"C:\_____Ufake\Design__Source_Files\Electronic PCBs\091415-1732 INFAST Dimming Circuit\";
            string exclusionFilePath = @"C:\_____Ufake\Found_U_Source_DesignSysDiag.txt";

            List<string> excludedPaths = LoadExcludedPaths(exclusionFilePath);

            List<string> matchingFiles = new List<string>();
            int maxFond = 600;
            int curfound = 0;
            try
            {
                // Use EnumerateFiles to find all .pdf files in all subdirectories
                IEnumerable<string> pdfFiles = Directory.EnumerateFiles(rootDirectory, "*.pdf", SearchOption.AllDirectories);

                foreach (string file in pdfFiles)
                {
                    curfound++;
                    if (curfound > maxFond)
                    {
                        break;
                    }

                    // Check if the file's directory is in the excluded paths list
                    if (IsFileExcluded(file, excludedPaths))
                    {
                        continue; // Skip this file and move to the next one
                    }

                    // if the path is U:\Dave Ohanian\Projects\20201203 2802 enclosure , continue 
                    //for some reason this directory is crashing the program
                    if (file.Contains("20201203 2802 enclosure"))
                    {
                        continue;
                    }

                    // Load the PDF document to check its dimensions
                    using (PdfDocument document = PdfDocument.Load(file))
                    {
                        // Assuming you want to check the dimensions of the first page
                        var pageSize = document.PageSizes[0]; // PageSizes is a list of SizeF, [0] is the first page

                        // Check the dimensions against the specified criteria
                        if ((pageSize.Width >= 790 && pageSize.Width <= 794 && pageSize.Height >= 610 && pageSize.Height <= 614) ||
                            (pageSize.Height >= 790 && pageSize.Height <= 794 && pageSize.Width >= 610 && pageSize.Width <= 614) ||
                            (pageSize.Width >= 1220 && pageSize.Width <= 1228 && pageSize.Height >= 790 && pageSize.Height <= 794) ||
                            (pageSize.Height >= 1220 && pageSize.Height <= 1228 && pageSize.Width >= 790 && pageSize.Width <= 794))
                        {
                            matchingFiles.Add(file);
                        }
                    }
                }

                // Check if there are any matching files to write
                if (matchingFiles.Count > 0)
                {

                    // Write all the matching file paths to the specified output text file
                    //File.WriteAllLines(outputPath, matchingFiles);
                    // Console.WriteLine($"Matching PDF files have been written to {outputPath}");
                    //we append to the liest of files to skip
                    File.AppendAllLines(exclusionFilePath, matchingFiles);
                }
                else
                {
                    MessageBox.Show("No matching PDF files found.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred: " + e.Message);
            }

            return matchingFiles;
        }


        public static List<string> FindIfAnyLineInHardCodedFileContains(string argToken)
        {
            string filePath = @"C:\___Root_VCI_Projects\Generic_VC_PGN_SIMULATOR\genericSim\VC_PGN_ManagerGUI\_Proj_Data_Dir\PDF_FilePathsDir\Udrive_pdfs.txt";
            List<string> linesContainingToken = new List<string>();

            // Normalize the token for case-insensitive comparison
            string normalizedToken = argToken.Trim().ToLowerInvariant();

            // Ensure the file exists to avoid FileNotFoundException
            if (File.Exists(filePath))
            {
                // Read all lines from the file
                foreach (string line in File.ReadAllLines(filePath))
                {
                    // Normalize the line for case-insensitive comparison
                    string normalizedLine = line.Trim().ToLowerInvariant();

                    // Check if the normalized line contains the normalized token
                    if (normalizedLine.Contains(normalizedToken))
                    {
                        linesContainingToken.Add(line); // Add the original line to the list
                    }
                }
            }

            return linesContainingToken;
        }


        public static List<string> FindPdfFiles_pdf_thatContainStr(string arg_containedSubStr)
        {


            string Path_to_listOfValidPDFs = @"C:\_____Ufake\FoundValidPdfs.txt";
            List<string> List_of_validPDFS = new List<string>();
            //load from a file all the paths needed 
            try
            {
                List_of_validPDFS = new List<string>(File.ReadAllLines(Path_to_listOfValidPDFs));

            }
            catch (Exception e)
            {
                MessageBox.Show($"An error occurred while loading excluded paths: {e.Message}");
                return List_of_validPDFS; // Return an empty list on error
            }

            List<string> List_potentialPDFs = new List<string>();

            foreach (string path in List_of_validPDFS)
            {

                string NormalizedPath = Path.GetFullPath(path).ToLowerInvariant();
                string NormalizedArg = arg_containedSubStr.ToLowerInvariant();

                if (NormalizedPath.Contains(NormalizedArg))
                {
                    List_potentialPDFs.Add(path);
                }
            }


            return List_potentialPDFs;
        }





    }


    //HC is fo HArcoded path useage
    public static class ESvee_ExaminerHC
    {
         
        public static List<string> Get_DirectroryNames_inRootSVN()
        {
            string rootSVNDirectory = @"C:\_____Ufake\SVN_Projects\";

            List<string> directoryNames = new List<string>();

            try
            {
                // Use EnumerateDirectories to find all directories in the root SVN directory
                IEnumerable<string> directories = Directory.EnumerateDirectories(rootSVNDirectory);

                foreach (string directory in directories)
                {
                    string directoryName = Path.GetFileName(directory);
                    directoryNames.Add(directoryName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return directoryNames;
        }

    }

}
