using System;

namespace Interfaces
{
    public interface IStudent
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public int Rating { get; set; }
        public long CourseId { get; set; }
    }

    public interface ICourse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Disciplenes { get; set; }
    }
}
