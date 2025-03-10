namespace MyHomePage.Services
{
    public class PowerEfficiencyCalculator
    {
        public float P_solar { get; set; }
        public float P_battery { get; set; }
        public float P_grid { get; set; }
        public float P_load { get; set; }

        /// <summary>
        /// Initializes the calculator with power values.
        /// </summary>
        /// <param name="p_solar">Solar power in watts (positive).</param>
        /// <param name="p_battery">Battery power in watts (positive for discharge, negative for charge).</param>
        /// <param name="p_grid">Grid power in watts (positive for import/usage).</param>
        /// <param name="p_load">Load power in watts (positive).</param>
        public PowerEfficiencyCalculator(float p_solar, float p_battery, float p_grid, float p_load)
        {
            P_solar = p_solar;
            P_battery = p_battery;
            P_grid = p_grid >= 0 ? p_grid : 0; // Ensure P_grid is non-negative
            P_load = p_load;
        }

        /// <summary>
        /// Calculates the DC input power to the inverter.
        /// Negative when battery discharges (usage), positive when battery charges.
        /// </summary>
        /// <returns>DC input power in watts.</returns>
        public float GetDcInput()
        {
            return P_solar - P_battery;
        }

        /// <summary>
        /// Calculates the total input power to the system (DC + Grid).
        /// </summary>
        /// <returns>Total input power in watts.</returns>
        public float GetTotalInput()
        {
            float dcInput = GetDcInput();
            // When discharging (dcInput < 0), use absolute value since it's a power source
            float dcContribution = dcInput < 0 ? Math.Abs(dcInput) : dcInput;
            // Add grid contribution (always positive)
            return dcContribution + P_grid;
        }

        /// <summary>
        /// Calculates the inverter efficiency as a percentage.
        /// Handles discharging (negative DC), charging (positive DC), and grid-only scenarios.
        /// </summary>
        /// <returns>Efficiency in percent, or 0 if no input or output.</returns>
        public float GetEfficiency()
        {
            float totalInput = GetTotalInput();
            if (totalInput > 0 && P_load > 0)
            {
                return (P_load / totalInput) * 100;
            }
            return 0;
        }

        /// <summary>
        /// Calculates the inverter power losses.
        /// Handles discharging (negative DC), charging (positive DC), and grid-only scenarios.
        /// </summary>
        /// <returns>Losses in watts, or 0 if no input or output.</returns>
        public float GetLosses()
        {
            float totalInput = GetTotalInput();
            if (totalInput > 0 && P_load > 0)
            {
                return totalInput - P_load;
            }
            return 0;
        }
    }
}