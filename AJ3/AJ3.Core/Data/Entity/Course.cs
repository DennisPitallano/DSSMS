namespace AJ3.Core.Data.Entity
{
    public class Course: EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Hours { get; set; }
        public bool IsDeleted { get; set; }
    }
}
