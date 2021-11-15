using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Models
{
    [Serializable]
    public class Course
    {
        public long Id { get; set ; }
        public string Name { get; set; }
        public string Disciplenes { get; set; }
    }
}
