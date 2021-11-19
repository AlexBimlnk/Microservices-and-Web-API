using Microsoft.AspNetCore.Mvc;
using MircroserviceСompositeSC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MircroserviceСompositeSC.Controllers
{
    /// <summary>
    /// Контроллер композитного сервиса.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CompositeSCController : Controller
    {
        private readonly string _studentServiceAddress = "https://localhost:44386/api/students";
        private readonly string _courseServiceAddress = "https://localhost:44385/api/courses";


        //Get: api/compositesc
        [HttpGet]
        public string Start()
        {
            return "Composite is run!";
        }

        /// <summary>
        /// Возвращает список курсов, в которых присутствует указанная дисциплина.
        /// </summary>
        /// <param name="discipleneName"> Имя дисциплины. </param>
        /// <returns> Список <see cref="List{T}"/>, где T является <see cref="Course"/>. </returns>
        //Get: api/compositesc/
        [HttpGet("courses/{discipleneName}")]
        public async Task<List<Course>> GetCoursesByDiscipleneAsync(string discipleneName)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_courseServiceAddress}/all");
                if (response.IsSuccessStatusCode)
                {
                    List<Course> courses = await JsonSerializer.DeserializeAsync<List<Course>>(await response.Content.ReadAsStreamAsync());
                    
                    return courses.Where(course => course.Disciplenes.Split(',').Contains(discipleneName)).ToList();
                }
            }
            return null;
        }

        /// <summary>
        /// Возвращает список студентов заданной группы.
        /// </summary>
        /// <param name="groupName"> Имя группы. </param>
        /// <returns> Список <see cref="List{T}"/>, где T является <see cref="Student"/>. </returns>
        //Get: api/compositesc/
        [HttpGet("students/{groupName}")]
        public async Task<List<Student>> GetStudentsByGroupAsync(string groupName)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_studentServiceAddress}/all");
                if (response.IsSuccessStatusCode)
                {
                    List<Student> students = await JsonSerializer.DeserializeAsync<List<Student>>(await response.Content.ReadAsStreamAsync());

                    return students.Where(student => student.GroupName == groupName).ToList();
                }
            }
            return null;
        }

        /// <summary>
        /// Возвращает средний рейтинг группы.
        /// </summary>
        /// <param name="groupName"> Имя группы. </param>
        /// <returns> Объект типа <see cref="RatingOfGroup"/>. </returns>
        //Get: api/compositesc/
        [HttpGet("rating/{groupName}")]
        public async Task<RatingOfGroup> GetAverageGroupRatingAsync(string groupName)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_studentServiceAddress}/all");
                if (response.IsSuccessStatusCode)
                {
                    List<Student> students = await JsonSerializer.DeserializeAsync<List<Student>>(await response.Content.ReadAsStreamAsync());         
                    
                    var collection = students.Where(st => st.GroupName == groupName).ToList();
                    long ratingSum = 0;

                    foreach (var i in collection) 
                        ratingSum += i.Rating;

                    return new RatingOfGroup() { GroupName = groupName, AverageRating = (double) ratingSum / collection.Count };
                }
            }
            return null;
        }
    }
}
