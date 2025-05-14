using System;

namespace MyHomePage.Services
{
    public class PowerCalculator
    {
        private readonly SolarDataService _dataService;

        public PowerCalculator(SolarDataService dataService)
        {
            _dataService = dataService;
        }

        public (float solarPct, float batteryPct, float gridPct) GetPowerSourcePercentages()
        {
            if (!float.TryParse(_dataService.GetSolarValue("inverter_1/load_power/state"), out float loadPower) || loadPower <= 0)
                return (0, 0, 0);

            float pvPower = float.TryParse(_dataService.GetSolarValue("inverter_1/pv_power/state"), out float pv) ? pv : 0;
            float batteryPower = float.TryParse(_dataService.GetSolarValue("total/battery_power/state"), out float bp) ? bp : 0;
            float gridPower = float.TryParse(_dataService.GetSolarValue("inverter_1/grid_power/state"), out float gp) ? gp : 0;

            float batteryContribution = batteryPower < 0 ? Math.Abs(batteryPower) : 0;
            float gridContribution = gridPower > 0 ? gridPower : 0;
            float solarContribution = loadPower - batteryContribution - gridContribution;

            if (solarContribution > pvPower) solarContribution = pvPower;
            if (solarContribution < 0) solarContribution = 0;

            float solarPct = (solarContribution / loadPower) * 100;
            float batteryPct = (batteryContribution / loadPower) * 100;
            float gridPct = (gridContribution / loadPower) * 100;

            return (solarPct, batteryPct, gridPct);
        }

        public string GetInverterEfficiency()
        {
            float P_solar = float.TryParse(_dataService.GetSolarValue("inverter_1/pv_power/state"), out float solar) ? solar : 0;
            float P_battery = float.TryParse(_dataService.GetSolarValue("total/battery_power/state"), out float battery) ? battery : 0;
            float P_grid = float.TryParse(_dataService.GetSolarValue("inverter_1/grid_power/state"), out float grid) ? grid : 0;
            float P_load = float.TryParse(_dataService.GetSolarValue("inverter_1/load_power/state"), out float load) ? load : 0;

            var calculator = new PowerEfficiencyCalculator(P_solar, P_battery, P_grid, P_load);
            return calculator.GetEfficiency().ToString("F1") + "%";
        }

        public string GetInverterLosses()
        {
            float P_solar = float.TryParse(_dataService.GetSolarValue("inverter_1/pv_power/state"), out float solar) ? solar : 0;
            float P_battery = float.TryParse(_dataService.GetSolarValue("total/battery_power/state"), out float battery) ? battery : 0;
            float P_grid = float.TryParse(_dataService.GetSolarValue("inverter_1/grid_power/state"), out float grid) ? grid : 0;
            float P_load = float.TryParse(_dataService.GetSolarValue("inverter_1/load_power/state"), out float load) ? load : 0;

            var calculator = new PowerEfficiencyCalculator(P_solar, P_battery, P_grid, P_load);
            return calculator.GetLosses().ToString("F0") + " W";
        }

        public float GetGridPercentage()
        {
            if (!float.TryParse(_dataService.GetSolarValue("inverter_1/load_power/state"), out float loadPower) || loadPower <= 0)
                return 0;

            float gridPower = float.TryParse(_dataService.GetSolarValue("inverter_1/grid_power/state"), out float gp) ? gp : 0;
            float gridContribution = gridPower > 0 ? gridPower : 0;
            return (gridContribution / loadPower) * 100;
        }

        public float GetInverterUsagePercentage()
        {
            return 100 - GetGridPercentage();
        }
    }
}