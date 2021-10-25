﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;

namespace Mircroservices.Models
{
    public class Student : IStudent
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public int Rating { get; set; }
        public long CourseId { get; set; }
    }
}
