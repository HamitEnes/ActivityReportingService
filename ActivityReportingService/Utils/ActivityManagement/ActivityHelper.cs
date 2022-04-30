using ActivityReportingService.Models.ActivityManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActivityReportingService.Utils.ActivityManagement
{
    public static class ActivityHelper
    {
        /// <summary>
        /// Creates in memory activity.
        /// </summary>
        /// <param name="activityName">Name of activity</param>
        /// <param name="activityParameter">Activity Parameter for getting duration value</param>
        /// <returns></returns>
        public static Activity CreateActivity(string activityName, ActivityParameter activityParameter)
        {
            return new Activity()
            {
                Name = activityName,
                CreatedDate = activityParameter.Date == DateTime.MinValue ? DateTime.Now : activityParameter.Date,
                Value = (int)Math.Round(activityParameter.Value)
            };
        }

        /// <summary>
        /// Checking existance of activity for specified name.
        /// </summary>
        /// <param name="activities">List of activities for searching</param>
        /// <param name="activityName">Name of activity for control</param>
        /// <returns>State of existance for specified activity name</returns>
        public static bool IsActivityExistingByName(IEnumerable<Activity> activities, string activityName)
        {
            return activities.Any(_ => _.Name == activityName);
        }

        /// <summary>
        /// Getting activities' total duration for last 12 hour. 
        /// </summary>
        /// <param name="activities">List of activities for searching</param>
        /// <param name="activityName">Activity name for total calculation.</param>
        /// <returns>Total duration for last 12 hour</returns>
        public static int GetTotalActivityDurationByName(IEnumerable<Activity> activities, string activityName)
        {
            return activities.Where(_ => _.Name == activityName && _.CreatedDate >= DateTime.Now.AddHours(-12)).Sum(_ => _.Value);
        }
    }
}
