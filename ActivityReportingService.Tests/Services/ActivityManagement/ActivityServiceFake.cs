using ActivityReportingService.Interfaces.ActivityManagement;
using ActivityReportingService.Models.ActivityManagement;
using ActivityReportingService.Utils.ActivityManagement;
using System.Collections.Generic;

namespace ActivityReportingService.Tests.Services.ActivityManagement
{
    /// <summary>
    /// Activity operations for testing purpose
    /// </summary>
    public class ActivityServiceFake : IActivityService
    {
        /// <summary>
        /// Statically generated Activities stored in this field
        /// </summary>
        private List<Activity> _exampleActivities;

        public ActivityServiceFake()
        {
            _exampleActivities = new List<Activity>();
        }

        public IEnumerable<Activity> GetActivities()
        {
            return _exampleActivities;
        }

        public Activity CreateActivity(string activityName, ActivityParameter activityParameter)
        {
            Activity newActivity = ActivityHelper.CreateActivity(activityName, activityParameter);
            _exampleActivities.Add(newActivity);
            return newActivity;
        }

        public int GetTotalActivityDurationByName(string activityName)
        {
            return ActivityHelper.GetTotalActivityDurationByName(_exampleActivities, activityName);
        }

        public bool IsActivityExistingByName(string activityName)
        {
            return ActivityHelper.IsActivityExistingByName(_exampleActivities, activityName);
        }
    }
}
