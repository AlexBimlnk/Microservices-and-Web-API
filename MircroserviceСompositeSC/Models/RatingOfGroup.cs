using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MircroserviceСompositeSC.Models
{
    /// <summary>
    /// Модель данных для рейтинга группы.
    /// </summary>
    [Serializable]
    public class RatingOfGroup
    {
        /// <summary>
        /// Возвращает или задаёт имя группы.
        /// </summary>
        [JsonPropertyName("groupName")]
        public string GroupName { get; set; }

        /// <summary>
        /// Возвращает или задаёт средний рейтинг группы.
        /// </summary>
        [JsonPropertyName("averageRating")]
        public double AverageRating { get; set; }
    }
}
