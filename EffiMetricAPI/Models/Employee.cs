namespace EffiMetricAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string fullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public double totalEarnings { get; set; }

        public string Rank => efficiencyScore switch
        {
            >= 90 => "Legendary",
            >= 75 => "Senior Elite",
            >= 50 => "Professional",
            >= 25 => "Junior",
            _ => "Trainee"
        };

        public double efficiencyScore { get; set; }
        
        public List<WorkTask> Tasks { get; set; } = new List<WorkTask>();
    }
}
