namespace Enwage_API.DTOs
{
    public class EmployeeStatenameDto
    {
        public Guid EmployeeId { get; set; }
        public Guid StatenameId { get; set; }
        public Guid Id { get; set; }

        // Optionally, you might want to include additional information:
        public string StateName { get; set; }  // If you want to include the name of the state
    }
}