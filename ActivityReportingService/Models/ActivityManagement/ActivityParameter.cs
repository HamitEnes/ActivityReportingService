namespace ActivityReportingService.Models.ActivityManagement
{
    /// <summary>
    /// In Site Activity Reporting Service documentation. There is a little info about that,
    /// Store the activity value as an integer (round to the nearest number).
    /// So, POST requested value data seems may be out of integer data. This class is using
    /// as a parameter of Activity POST requests. With this way endpoint can get the value data with precision.
    /// </summary>
    public class ActivityParameter
    {
        public decimal Value { get; set; }
    }
}
