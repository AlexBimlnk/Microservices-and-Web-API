using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceCourse.Models
{
    /// <summary>
    /// Модель данных Курс.
    /// </summary>
    [Serializable]
    public class Course
    {
        /// <summary>
        /// Возвращает или задаёт ID курса.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Возвращает или задаёт имя курса.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает или задаёт дисциплины данного курса.
        /// </summary>
        public string Disciplenes { get; set; }
    }
}
