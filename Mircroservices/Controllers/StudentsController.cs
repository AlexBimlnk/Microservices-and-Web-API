using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mircroservices.Models;
using Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Mircroservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        //GET: Students
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        //GET: Students
        //[HttpGet]
        //public IEnumerable<Student> Index()
        //{
        //    var students = _context.Students.ToList();
        //    return students;
        //}

        [HttpGet("{id}")]
        public async ValueTask<IStudent> Get(long? id)
        {
            return await Task.Run(() =>
            {
                var student = _context.Students.FindAsync(id).Result;
                return student;
            });
        }

        [HttpGet("course/{studentId}")]
        public async Task<string> GetCourse(long? studentId)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (HttpClient client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:44385/api/courses/{studentId}");
                if (response.IsSuccessStatusCode)
                {
                    object course = await JsonSerializer.DeserializeAsync<object>(await response.Content.ReadAsStreamAsync());
                    return course.ToString();
                }
            }
            return null;
        }

        // POST: Students
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        //Put 
        [HttpPut("{id}")]
        public async Task Edit(long id, Student student)
        {
            student.Id = id;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task DeleteConfirmed(long id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Attach(student);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}
