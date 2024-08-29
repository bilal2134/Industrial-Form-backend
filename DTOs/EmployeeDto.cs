namespace Enwage_API.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public Guid? ClientId { get; set; }
        public string? ClientName { get; set; }
        public bool? ChangeState { get; set; }
        public DateTime? ExperienceStartDate { get; set; }
        public DateTime? ExperienceEndDate { get; set; }
        public decimal? HourlyRate { get; set; }
        public bool? IsPresent { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public List<Guid> States { get; set; } = new List<Guid>();


    }
}