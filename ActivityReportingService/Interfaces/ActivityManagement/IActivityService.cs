using ActivityReportingService.Models.ActivityManagement;
using Microsoft.EntityFrameworkCore;

namespace ActivityReportingService.Interfaces.ActivityManagement
{
    public interface IActivityService
    {
        /// <summary>
        /// Getting all activities
        /// </summary>
        /// <returns>All activities</returns>
        public DbSet<Activity> GetActivities();

        /// <summary>
        /// Creating activity for specified duration value
        /// </summary>
        /// <param name="activityName">Name of activity</param>
        /// <param name="activityParameter">ActivityParameter object for accessing duration value. Possible for decimal value</param>
        /// <returns>Created activity</returns>
        public Activity CreateActivity(string activityName, ActivityParameter activityParameter);

        /// <summary>
        /// Getting activities' total duration for last 12 hour. 
        /// </summary>
        /// <param name="activityName">Activity name for total calculation.</param>
        /// <returns>Total duration for last 12 hour</returns>
        public int GetTotalActivityDurationByName(string activityName);

        /// <summary>
        /// Checking existance of activity for specified name.
        /// </summary>
        /// <param name="activityName">Name of activity for control</param>
        /// <returns>State of existance for specified activity name</returns>
        public bool IsActivityExistingByName(string activityName);
    }
}
