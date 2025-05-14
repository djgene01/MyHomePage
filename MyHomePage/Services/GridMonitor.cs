using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MyHomePage.Services
{
    public class GridMonitor
    {
        private readonly SolarDataService _dataService;

        public GridMonitor(SolarDataService dataService)
        {
            _dataService = dataService;
        }

        public string GetGridStatus()
        {
            string voltageString = _dataService.GetSolarValue("inverter_1/grid_voltage/state");
            voltageString = Regex.Replace(voltageString, @"[^\d.-]", "");
            if (float.TryParse(voltageString, NumberStyles.Float, CultureInfo.InvariantCulture, out float gridVoltage))
            {
                return gridVoltage > 0 ? "Connected" : "Offline";
            }
            return "Unknown";
        }

        public bool IsFrequencyOutOfRange()
        {
            if (float.TryParse(_dataService.GetSolarValue("inverter_1/grid_frequency/state"), out float frequency))
            {
                return frequency < 49 || frequency > 51;
            }
            return false;
        }
    }
}