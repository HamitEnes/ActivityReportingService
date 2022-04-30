using System;

namespace ActivityReportingService.Models.ActivityManagement
{
    /// <summary>
    /// Model class for storing activity data
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// Auto incremented key value
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// Requesting activity name as a parameter of API and storing activities by name.
        /// This will be used for generating totals by activity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// For pruning greater than 12 hour data. This property will be used.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Value of the activity.
        /// </summary>
        public int Value { get; set; }
    }
}
