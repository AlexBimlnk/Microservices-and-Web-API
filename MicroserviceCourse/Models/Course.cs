using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;

namespace MicroserviceCourse.Models
{
    [Serializable]
    public class Course : ICourse
    {
        public long Id { get; set ; }
        public string Name { get; set; }
        public string Disciplenes { get; set; }
    }
}
