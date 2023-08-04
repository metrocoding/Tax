namespace Tax.Models
{
    public class TimeRangeTaxFee
    {
        public TimeRangeTaxFee(int fee, TimeSpan startTime, TimeSpan endTime)
        {
            Fee = fee;
            StartTime = startTime;
            EndTime = endTime;
        }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Fee { get; set; }
    }
}