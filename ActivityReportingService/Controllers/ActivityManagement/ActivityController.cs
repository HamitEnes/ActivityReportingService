using ActivityReportingService.Interfaces.ActivityManagement;
using ActivityReportingService.Models.ActivityManagement;
using Microsoft.AspNetCore.Mvc;

namespace ActivityReportingService.Controllers.ActivityManagement
{
    /// <summary>
    /// This controller manages the activities' requests.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        /// <summary>
        /// Simple get endpoint
        /// </summary>
        /// <returns>List of existing activities</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_activityService.GetActivities());
        }

        /// <summary>
        /// Creating new activities. Value may be decimal number and will be rounded to nearest number.
        /// </summary>
        /// <param name="key">Activity name</param>
        /// <param name="activityParameter">Getting a value for activity duration. Supports for decimal numbers</param>
        /// <returns>Created activity record</returns>
        [HttpPost("{key}")]
        public IActionResult Post(string key, [FromBody] ActivityParameter activityParameter)
        {
            return Ok(_activityService.CreateActivity(key, activityParameter));
        }

        /// <summary>
        /// Getting totals by activity name, totals are pruning with greater than 12 hour values.
        /// </summary>
        /// <param name="key">Activity name</param>
        /// <returns>The total activity duration for last 12 hour</returns>
        [HttpGet("{key}/total")]
        public IActionResult GetTotalByKey(string key)
        {
            // this check ensures that, total requested activity is exists.
            if (!_activityService.IsActivityExistingByName(key))
            {
                return NotFound($"{key} activity not found");
            }

            // firstly filtering by activity name after that pruning greater than 12 hour old activities.
            return base.Ok(_activityService.GetTotalActivityDurationByName(key));
        }
    }
}
