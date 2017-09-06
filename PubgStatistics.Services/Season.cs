using System;

namespace PubgStatistics.Services {
    /// <summary>
    /// Provides methods to help with seasons
    /// </summary>
    public class Season {
        /// <summary>
        /// Gets the current season string to send the API.
        /// Handles the monthly reset that started in September or August, I dont remember.
        /// I assume it started on Sept 1 2017
        /// </summary>
        /// <returns></returns>
        public static string CurrentSeason() {
            var season4StartDate = new DateTime(2017, 9, 1);
            var today = DateTime.Now;
            var monthsSinceSept2017 = ((today.Year - season4StartDate.Year) * 12) + Math.Abs(today.Month - season4StartDate.Month);

            var season = 4 + monthsSinceSept2017;

            return $"{today.Year}-pre{season}";
        }
    }
}
