namespace Crud.Entity
{
    public class Subject
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? description { get; set; }
    }
}
