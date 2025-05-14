namespace MyHomePage.Services
{
    public class BatteryManager
    {
        private readonly SolarDataService _dataService;
        public float BatteryCapacity { get; private set; } // Public getter, private setter

        public BatteryManager(SolarDataService dataService, float batteryCapacity = 14580)
        {
            _dataService = dataService;
            BatteryCapacity = batteryCapacity;
        }

        public string GetBatteryStatus(string prefix)
        {
            if (!int.TryParse(_dataService.GetSolarValue($"{prefix}/power/state"), out int batteryPower))
            {
                Console.WriteLine($"Failed to parse battery power for {prefix}: {_dataService.GetSolarValue($"{prefix}/power/state")}");
                return "Unknown";
            }
            return batteryPower > 0 ? "Charging" : "Discharging";
        }

        public string GetTotalBatteryStatus()
        {
            if (!int.TryParse(_dataService.GetSolarValue("total/battery_power/state"), out int batteryPower))
            {
                Console.WriteLine($"Failed to parse battery power: {_dataService.GetSolarValue("total/battery_power/state")}");
                return "Unknown";
            }
            return batteryPower > 0 ? "Charging" : "Discharging";
        }

        public string GetBatteryTimeRemaining()
        {
            if (!float.TryParse(_dataService.GetSolarValue("total/battery_state_of_charge/state"), out float soc) ||
                !float.TryParse(_dataService.GetSolarValue("total/battery_power/state"), out float batteryPower) ||
                BatteryCapacity <= 0)
                return "N/A";

            if (batteryPower == 0)
                return "N/A";

            float energyRemaining;
            string status;
            if (batteryPower > 0) // Charging
            {
                energyRemaining = (100 - soc) * BatteryCapacity / 100;
                status = "to full";
            }
            else // Discharging
            {
                energyRemaining = soc * BatteryCapacity / 100;
                batteryPower = Math.Abs(batteryPower);
                status = "to empty";
            }

            float hours = energyRemaining / batteryPower;
            if (hours > 1000 || hours < 0)
                return "N/A";

            TimeSpan time = TimeSpan.FromHours(hours);
            return $"{time.Hours}h {time.Minutes}m {status}";
        }
    }
}