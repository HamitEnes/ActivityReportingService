using ActivityReportingService.DataManagement;
using ActivityReportingService.Models.ActivityManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActivityReportingService.Controllers.ActivityManagement
{
    /// <summary>
    /// This controller manages the activities' operations.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        /// <summary>
        /// Used for accessing DbContext via Dependency Injection
        /// </summary>
        private readonly AppDbContext _appDBContext;

        /// <summary>
        /// Constructor for controller class. 
        /// </summary>
        /// <param name="appDBContext">Injection for DbContext</param>
        public ActivityController(AppDbContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        /// <summary>
        /// Simple get endpoint
        /// </summary>
        /// <returns>List of existing activities</returns>
        [HttpGet]
        public IEnumerable<Activity> Get()
        {
            return _appDBContext.Activities;
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
            // Currently ActivityId not managed by datasource and given static int number.
            Activity newActivity = new Activity()
            {
                Name = key,
                CreatedDate = DateTime.Now,
                Value = (int)Math.Round(activityParameter.Value)
            };

            _appDBContext.Activities.Add(newActivity);
            _appDBContext.SaveChanges();
            return Ok(newActivity);
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
            if (!_appDBContext.Activities.Any(_ => _.Name == key))
            {
                return NotFound($"{key} activity not found");
            }

            // firstly filtering by activity name after that pruning greater than 12 hour old activities.
            return Ok(_appDBContext.Activities.Where(_ => _.Name == key && _.CreatedDate >= DateTime.Now.AddHours(-12)).Sum(_ => _.Value));
        }
    }
}
