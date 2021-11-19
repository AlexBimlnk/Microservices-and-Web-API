using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MircroserviceStudent.Models
{
    /// <summary>
    /// Модель данных Студент.
    /// </summary>
    [Serializable]
    public class Student
    {
        /// <summary>
        /// Возвращает или задаёт id студента.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Возвращает или задаёт имя студента.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возвращает или задаёт имя группы, к которой принадлежит студент.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Возвращает или задаёт рейтинг студента.
        /// </summary>
        public int Rating { get; set; }
    }
}
