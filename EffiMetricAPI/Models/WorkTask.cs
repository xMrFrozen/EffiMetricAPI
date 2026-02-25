namespace EffiMetricAPI.Models
{
    public class WorkTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int difLevel { get; set; }
        public double estimatedHours { get; set; }
        public double completedHours { get; set; }
        public bool isCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int employeeId { get; set; }
    }
}
