﻿using ActivityReportingService.Models.ActivityManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActivityReportingService.Controllers.ActivityManagement
{
    [Route("[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        public ActivityController()
        {
        }

        private static readonly Activity[] exampleActivities = new[]
        {
            new Activity(){ ActivityId = 1, Name = "learn_more_page", CreatedDate = DateTime.Now.AddMinutes(-781), Value = 16 },
            new Activity(){ ActivityId = 2, Name = "learn_more_page", CreatedDate = DateTime.Now.AddMinutes(-510), Value = 5 },
            new Activity(){ ActivityId = 3, Name = "learn_more_page", CreatedDate = DateTime.Now.AddSeconds(-50), Value = 32 },
            new Activity(){ ActivityId = 4, Name = "learn_more_page", CreatedDate = DateTime.Now.AddSeconds(-3), Value = 4 },
            new Activity(){ ActivityId = 5, Name = "info_page", CreatedDate = DateTime.Now.AddMinutes(-3), Value = 57 }
        };

        [HttpGet]
        public IEnumerable<Activity> Get()
        {
            return exampleActivities;
        }

        [HttpGet("{key}/total")]
        public IActionResult GetTotalByKey(string key)
        {
            // this check ensures that, total requested activity is exists.
            if (!exampleActivities.Any(_ => _.Name == key))
            {
                return NotFound($"{key} activity not found");
            }

            // firstly filtering by activity name after that pruning greater than 12 hour old activities.
            return Ok(exampleActivities.Where(_ => _.Name == key && _.CreatedDate >= DateTime.Now.AddHours(-12)).Sum(_ => _.Value));
        }
    }
}
