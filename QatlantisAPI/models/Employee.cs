namespace QatlantisAPI.models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public List<Case>? Cases { get; set; }
    }
}
