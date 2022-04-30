using ActivityReportingService.Controllers.ActivityManagement;
using ActivityReportingService.Interfaces.ActivityManagement;
using ActivityReportingService.Models.ActivityManagement;
using ActivityReportingService.Tests.Services.ActivityManagement;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace ActivityReportingService.Tests.Controllers.ActivityManagement
{
    /// <summary>
    /// Class for testing ActivityController operations
    /// </summary>
    public class ActivityControllerTest
    {
        private readonly ActivityController _controller;
        private readonly IActivityService _service;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ActivityControllerTest()
        {
            _service = new ActivityServiceFake();
            _controller = new ActivityController(_service);
        }

        /// <summary>
        /// Testing that simple get method works. 
        /// Test succeeded when returns Http 200
        /// </summary>
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        /// <summary>
        /// In start of API, there should be no activity. Because of In memory usage.
        /// </summary>
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Activity>>(okResult.Value);
            Assert.Empty(items);
        }

        /// <summary>
        /// This test sent a valid value and checks return of Http 200 result
        /// </summary>
        [Fact]
        public void Add_ValidValue_ReturnsOkResult()
        {
            // Arrange
            var activityParameter = new ActivityParameter()
            {
                Value = 16
            };

            string key = "learn_more_page";

            // Act
            var okResult = _controller.Post(key, activityParameter);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        /// <summary>
        /// In requirement documentation, there is an information about rounding nearest number.
        /// This test is sending a value close to small value and check it is correctly rounded.
        /// </summary>
        [Fact]
        public void Add_CloseToSmallValue_ReturnsOkResult()
        {
            // Arrange
            var activityParameter = new ActivityParameter()
            {
                Value = 16.1M
            };

            string key = "learn_more_page";

            // Act
            var okResultPost = _controller.Post(key, activityParameter);
            var okResultGet = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResultPost as OkObjectResult);
            Assert.IsType<OkObjectResult>(okResultGet as OkObjectResult);
            Assert.Equal(16, ((okResultGet as OkObjectResult).Value as List<Activity>)[0].Value);
        }

        /// <summary>
        /// In requirement documentation, there is an information about rounding nearest number.
        /// This test is sending a value close to great value and check it is correctly rounded.
        /// </summary>
        [Fact]
        public void Add_CloseToGreatValue_ReturnsOkResult()
        {
            // Arrange
            var activityParameter = new ActivityParameter()
            {
                Value = 16.6M
            };

            string key = "learn_more_page";

            // Act
            var okResultPost = _controller.Post(key, activityParameter);

            var okResultGet = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResultPost as OkObjectResult);
            Assert.IsType<OkObjectResult>(okResultGet as OkObjectResult);
            Assert.Equal(17, ((okResultGet as OkObjectResult).Value as List<Activity>)[0].Value);
        }

        /// <summary>
        /// In this scenario, some of activities are created and calculating total durations.
        /// </summary>
        [Fact]
        public void Add_SomeActivities_ReturnsExpectedResult()
        {
            // Arrange
            string key = "learn_more_page";
            var activityParameter = new ActivityParameter();

            // Act
            activityParameter.Value = 16.6M;
            _controller.Post(key, activityParameter);

            activityParameter.Value = 5.1M;
            _controller.Post(key, activityParameter);

            activityParameter.Value = 25;
            _controller.Post(key, activityParameter);

            key = "info_page";
            activityParameter.Value = 19;
            _controller.Post(key, activityParameter);

            key = "learn_more_page";
            var okResultGet = _controller.GetTotalByKey(key);

            // Assert
            Assert.IsType<OkObjectResult>(okResultGet as OkObjectResult);
            Assert.Equal(47, (okResultGet as OkObjectResult).Value);
        }
    }
}
