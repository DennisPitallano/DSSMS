namespace AJ3.Core.Data.Entity
{
    public class StudentCourse : EntityBase
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int TotalHours { get; set; }
        public int ConsumeHours { get; set; }
        public int FreeHours { get; set; }
        public int ConsumeFreeHours { get; set; }
        public string Status { get; set; }
    }
}
