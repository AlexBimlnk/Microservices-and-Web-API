using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceCourse.Models
{
    /// <summary>
    /// Контекст базы данных для курса.
    /// </summary>
    public class CourseContext : DbContext 
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="options"> Опции для этого контекста. </param>
        public CourseContext(DbContextOptions<CourseContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Возвращает список курсов из базы данных.
        /// </summary>
        public DbSet<Course> Courses { get; set; }
    }
}
