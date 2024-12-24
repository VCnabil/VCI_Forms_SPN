using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCI_Forms_SPN._BackEndDataOBJs.OBJ_defs
{
    public class FirmwareParameters
    {
        public string ProjectName { get; set; }
        public string FirmwareDirName { get; set; }
        public string ParametersFilePath { get; set; }
        public string Version { get; set; }

        public string VersionEX { get; set; }
        public string EepromStatus { get; set; }
        public string SvnRevision { get; set; }
        public int NumberOfWaterJets { get; set; }         // # waterjets NUMBER_WATERJET_UNITS

        public int NumberOfLCDs { get; set; }         // # LCD
        public int NumberOfNozzles { get; set; }      // # Nozzles
        public int NumberOfBuckets { get; set; }      // # Buckets
        public int NumberOfBowThrusters { get; set; }   // # Int/Tabs
        public int NumberOfIntTabs { get; set; }      // # Int/Tabs

        public int NumberOfStations { get; set; }      // # Stations

        public int NumberOfJoysticks { get; set; }    // #Joystick
        public int NumberOfLevers { get; set; }       // #Levers
        public int NumberOfHelms { get; set; }        // #Helm
        public int NumberOfTillers { get; set; }      // #Tiller

        public bool HasRS232 { get; set; }            // hasRS232 (Y/N)
        public bool HasCAN { get; set; }              // hasCAN (Y/N)
        public string CalMode { get; set; }           // #Cal (e.g., 'Auto')
        public bool HasIdleKnob { get; set; }         // hasIdleKnob (Y/N)
        public bool HasDim { get; set; }                // Dim
        public string SourceFileName { get; set; } // <-- New
        public Dictionary<string, vcEparam> EParams { get; set; } // Key: Parameter Name, Value: Parameter Value (a)

        public FirmwareParameters()
        {
            EParams = new Dictionary<string, vcEparam>();
        }

        public void SetAdditionalProperties()
        {
            // Initialize counts and defaults
            NumberOfWaterJets = 0;
            NumberOfBowThrusters = 0;
            NumberOfNozzles = 0;
            NumberOfBuckets = 0;
            NumberOfIntTabs = 0;
            NumberOfStations = 0;
            NumberOfJoysticks = 0;
            NumberOfLevers = 0;
            NumberOfHelms = 0;
            NumberOfTillers = 0;
            HasIdleKnob = false;
            HasCAN = false;
            HasRS232 = false;
            CalMode = "Manual"; // Default to 'Manual' if not specified
            NumberOfLCDs = 0;

            // 1. NumberOfWaterJets comes from NUMBER_WATERJET_UNITS
            if (EParams.TryGetValue("NUMBER_WATERJET_UNITS", out vcEparam waterJetUnits))
            {
                NumberOfWaterJets = waterJetUnits.Value;
            }
            // 2. NumberOfBowThrusters comes from NUMBER_BOWTHRUSTER_UNITS
            if (EParams.TryGetValue("NUMBER_BOWTHRUSTER_UNITS", out vcEparam bowThrusterUnits))
            {
                NumberOfBowThrusters = bowThrusterUnits.Value;
            }
            else
            {
                NumberOfBowThrusters = 0; // Default to 0 if not found
            }

            // 3. NumberOfNozzles, NumberOfBuckets, NumberOfIntTabs come from INDICATION_CONFIG
            if (EParams.TryGetValue("INDICATION_CONFIG", out vcEparam indicationConfig))
            {
                switch (indicationConfig.Value)
                {
                    case 1:
                        // 1 = Display one bucket, one nozzle on LCD
                        NumberOfBuckets = 1;
                        NumberOfNozzles = 1;
                        NumberOfIntTabs = 0;
                        break;
                    case 2:
                        // 2 = Display two buckets, two nozzles on LCD
                        NumberOfBuckets = 2;
                        NumberOfNozzles = 2;
                        NumberOfIntTabs = 0;
                        break;
                    case 3:
                        // 3 = Display two buckets, one nozzle on LCD
                        NumberOfBuckets = 2;
                        NumberOfNozzles = 1;
                        NumberOfIntTabs = 0;
                        break;
                    case 4:
                        // 4 = Display two buckets, two nozzles, two interceptors on LCD
                        NumberOfBuckets = 2;
                        NumberOfNozzles = 2;
                        NumberOfIntTabs = 2;
                        break;
                    case 5:
                        // 5 = Display two buckets, one nozzle, two interceptors on LCD
                        NumberOfBuckets = 2;
                        NumberOfNozzles = 1;
                        NumberOfIntTabs = 2;
                        break;
                    default:
                        // Unknown configuration
                        NumberOfBuckets = 0;
                        NumberOfNozzles = 0;
                        NumberOfIntTabs = 0;
                        break;
                }
            }

            // 4. HasIdleKnob comes from OPTION_IDLECONTROL
            // 0 = None, 1 = Separate idle control for each station, 2 = Only one idle control, 3 = Individual idle controls for each engine (at main station only)
            if (EParams.TryGetValue("OPTION_IDLECONTROL", out vcEparam optionIdleControl))
            {
                HasIdleKnob = optionIdleControl.Value != 0;
            }

            // 5. HasCAN comes from VCI_CAN_BUS (0 or 1)
            if (EParams.TryGetValue("VCI_CAN_BUS", out vcEparam vciCanBus))
            {
                HasCAN = vciCanBus.Value == 1;
            }

            // 6. NumberOfStations comes from NUMBER_WIRED_STATIONS plus NUMBER_CANBUS_STATIONS
            if (EParams.TryGetValue("NUMBER_WIRED_STATIONS", out vcEparam numberWiredStations))
            {
                NumberOfStations = numberWiredStations.Value;
            }
            else
            {
                NumberOfStations = 0;
            }

            if (EParams.TryGetValue("NUMBER_CANBUS_STATIONS", out vcEparam numberCanbusStations))
            {
                NumberOfStations += numberCanbusStations.Value;
            }

            // 7. Process station configurations to determine NumberOfHelms, NumberOfJoysticks, NumberOfLevers
            // Initialize counts
            NumberOfHelms = 0;
            NumberOfJoysticks = 0;
            NumberOfLevers = 0;

            // Process STA1_CONFIG
            SetStationConfig("STA1_CONFIG");

            // Process STA2_CONFIG
            SetStationConfig("STA2_CONFIG");

            // Process CANBUS_STATION_CONFIG
            SetStationConfig("CANBUS_STATION_CONFIG");

            // Process STA3_CONFIG if it exists
            SetStationConfig("STA3_CONFIG");

            // 8. NumberOfTillers may be set based on OPTION_INTSTEER
            // If OPTION_INTSTEER > 0, there is at least one tiller
            if (EParams.TryGetValue("OPTION_INTSTEER", out vcEparam optionIntSteer))
            {
                NumberOfTillers = optionIntSteer.Value > 0 ? 1 : 0;
            }

            // 9. HasRS232 comes from LCD_COMMS_MODE
            // If LCD_COMMS_MODE == 0, HasRS232 is true
            if (EParams.TryGetValue("LCD_COMMS_MODE", out vcEparam lcdCommsMode))
            {
                HasRS232 = lcdCommsMode.Value == 0;

                // Also determine CalMode
                CalMode = lcdCommsMode.Value >= 3 ? "Auto" : "Manual";
            }

            // 10. NumberOfLCDs
            // Assuming that if LCD_COMMS_MODE > 0, we have at least one LCD
            if (EParams.TryGetValue("LCD_COMMS_MODE", out lcdCommsMode))
            {
                NumberOfLCDs = lcdCommsMode.Value > 0 ? 1 : 0;
            }

            // Set HasDim based on an EParam (assuming DIMMING_OPTION)
            if (EParams.TryGetValue("DIMMING_OPTION", out vcEparam dimmingOption))
            {
                HasDim = dimmingOption.Value != 0;
            }
            else
            {
                HasDim = false; // Default to false if DIMMING_OPTION is not found
            }
        }
        private void SetStationConfig(string stationParamName)
        {
            if (EParams.TryGetValue(stationParamName, out vcEparam stationConfig))
            {
                int configValue = stationConfig.Value;

                if (stationParamName == "STA1_CONFIG" || stationParamName == "STA2_CONFIG" || stationParamName == "STA3_CONFIG")
                {
                    // For STA1_CONFIG, STA2_CONFIG, STA3_CONFIG
                    // Possible values and their meanings:

                    // 0 = Helm + Joystick
                    // 1 = Helm + Joystick + Lever
                    // 2 = Helm + Lever (NO Joystick)
                    // 3 = Helm + Throttle Levers + Bucket Levers (NO Joystick)
                    // 4 = 2x Selectable Helm + Levers + Joystick (STA1_CONFIG only)

                    // Helm presence (all configurations include a helm)
                    NumberOfHelms += 1;

                    // Joystick presence
                    if (configValue == 0 || configValue == 1 || configValue == 4)
                        NumberOfJoysticks += 1;

                    // Lever presence
                    if (configValue == 1 || configValue == 2 || configValue == 3 || configValue == 4)
                        NumberOfLevers += 1;

                    // Note: Adjust logic for NumberOfTillers if specific configurations indicate a tiller
                }
                else if (stationParamName == "CANBUS_STATION_CONFIG")
                {
                    // Configuration of the CANBUS station:
                    // 0(none), 1(helm + joystick), 2(helm + levers), 3(RSC with EEPROM), 4(helm + joystick + levers)
                    if (configValue != 0)
                    {
                        // Helm presence
                        if (configValue == 1 || configValue == 2 || configValue == 4)
                            NumberOfHelms += 1;

                        // Joystick presence
                        if (configValue == 1 || configValue == 4)
                            NumberOfJoysticks += 1;

                        // Lever presence
                        if (configValue == 2 || configValue == 4)
                            NumberOfLevers += 1;

                        // Handle other configurations if necessary (e.g., configValue == 3 for RSC with EEPROM)
                    }
                }
            }
        }

    }

    public class vcEparam
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public vcEparam(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
