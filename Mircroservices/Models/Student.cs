using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mircroservices.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public int Rating { get; set; }
    }
}
