namespace Crud.DTO
{
    public class SubjectDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? description { get; set; }
    }
}
