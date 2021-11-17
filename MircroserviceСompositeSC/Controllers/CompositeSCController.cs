using Microsoft.AspNetCore.Mvc;
using MircroserviceCompositeSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MircroserviceСompositeSC.Controllers
{
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

        //Get: api/compositesc/
        [HttpGet("disciplenes/{groupName}")]
        public async Task<string> GetDisciplenesByGroup(string groupName)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_courseServiceAddress}/name/{groupName}");
                if (response.IsSuccessStatusCode)
                {
                    Course course = await JsonSerializer.DeserializeAsync<Course>(await response.Content.ReadAsStreamAsync());
                    return course.Disciplenes;
                }
            }
            return null;
        }

        //Get: api/compositesc/
        [HttpGet("disciplenes")]
        public async Task<string> GetAllDisciplenes()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_courseServiceAddress}/all");
                if (response.IsSuccessStatusCode)
                {
                    List<Course> courses = await JsonSerializer.DeserializeAsync<List<Course>>(await response.Content.ReadAsStreamAsync());
                    StringBuilder answer = new StringBuilder();
                    foreach (var i in courses)
                        answer.AppendLine(i.Disciplenes);
                    return answer.ToString();
                }
            }
            return null;
        }

        //Get: api/compositesc/
        [HttpGet("course/names")]
        public async Task<string> GetCourseNames()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_courseServiceAddress}/all");
                if (response.IsSuccessStatusCode)
                {
                    List<Course> courses = await JsonSerializer.DeserializeAsync<List<Course>>(await response.Content.ReadAsStreamAsync());
                    StringBuilder answer = new StringBuilder();
                    foreach (var i in courses)
                        answer.AppendLine(i.Name);
                    return answer.ToString();
                }
            }
            return null;
        }

        //Get: api/compositesc/
        [HttpGet("rating/{groupName}")]
        public async Task<string> GetGroupRating(string groupName)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_studentServiceAddress}/all");
                if (response.IsSuccessStatusCode)
                {
                    List<Student> students = await JsonSerializer.DeserializeAsync<List<Student>>(await response.Content.ReadAsStreamAsync());
                    StringBuilder answer = new StringBuilder();
                    long ratingSum = 0;
                    foreach (var i in students.Where(st => st.GroupName == groupName)) 
                    {
                        answer.AppendLine(i.Rating.ToString());
                        ratingSum += i.Rating;
                    }
                    answer.AppendLine($"Rating sum: {ratingSum}");
                    return answer.ToString();
                }
            }
            return null;
        }
    }
}
