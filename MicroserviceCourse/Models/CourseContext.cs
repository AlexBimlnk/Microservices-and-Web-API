using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceCourse.Models
{
    public class CourseContext : DbContext 
    {
        public CourseContext(DbContextOptions<CourseContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
    }
}
