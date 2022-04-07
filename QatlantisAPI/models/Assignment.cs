namespace QatlantisAPI.models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Boolean Status { get; set; }
        public int? CaseId { get; set; }
        public int? EmployeeId { get; set; }
    }
}
