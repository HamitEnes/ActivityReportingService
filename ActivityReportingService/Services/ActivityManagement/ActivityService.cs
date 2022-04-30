using ActivityReportingService.DataManagement;
using ActivityReportingService.Interfaces.ActivityManagement;
using ActivityReportingService.Models.ActivityManagement;
using ActivityReportingService.Utils.ActivityManagement;
using System.Collections.Generic;

namespace ActivityReportingService.Services.ActivityManagement
{
    /// <summary>
    /// A service class for activity related operations.
    /// </summary>
    public class ActivityService : IActivityService
    {
        /// <summary>
        /// Used for accessing DbContext via Dependency Injection
        /// </summary>
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// Constructor for service class. 
        /// </summary>
        /// <param name="appDbContext">Injection for DbContext</param>
        public ActivityService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Getting all activities
        /// </summary>
        /// <returns>All activities</returns>
        public IEnumerable<Activity> GetActivities()
        {
            return _appDbContext.Activities;
        }

        /// <summary>
        /// Creating activity for specified duration value
        /// </summary>
        /// <param name="activityName">Name of activity</param>
        /// <param name="activityParameter">ActivityParameter object for accessing duration value. Possible for decimal value</param>
        /// <returns>Created activity</returns>
        public Activity CreateActivity(string activityName, ActivityParameter activityParameter)
        {
            Activity newActivity = ActivityHelper.CreateActivity(activityName, activityParameter);

            _appDbContext.Activities.Add(newActivity);

            // After adding newActivity to Activities DbSet, ActivityId value is assigned.
            _appDbContext.SaveChanges();
            return newActivity;
        }

        /// <summary>
        /// Getting activities' total duration for last 12 hour. 
        /// </summary>
        /// <param name="activityName">Activity name for total calculation.</param>
        /// <returns>Total duration for last 12 hour</returns>
        public int GetTotalActivityDurationByName(string activityName)
        {
            return ActivityHelper.GetTotalActivityDurationByName(_appDbContext.Activities, activityName);
        }

        /// <summary>
        /// Checking existance of activity for specified name.
        /// </summary>
        /// <param name="activityName">Name of activity for control</param>
        /// <returns>State of existance for specified activity name</returns>
        public bool IsActivityExistingByName(string activityName)
        {
            return ActivityHelper.IsActivityExistingByName(_appDbContext.Activities, activityName);
        }
    }
}
