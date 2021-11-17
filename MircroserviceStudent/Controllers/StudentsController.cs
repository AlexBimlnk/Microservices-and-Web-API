using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MircroserviceStudent.Models;
using Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MircroserviceStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly StudentContext _context;
        private readonly string _compositeSeviceAddress = "https://localhost:44354/api/compositesc";

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

        //GET: Students/all
        [HttpGet("all")]
        public async Task<string> GetAllCourses()
        {
            var students = await _context.Students.ToListAsync();

            return JsonSerializer.Serialize(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(long? id)
        {
            return await Task.Run(() =>
            {
                var student = _context.Students.FindAsync(id).Result;
                return student;
            });
        }

        //[HttpGet("course/{studentId}")]
        //public async Task<string> GetCourseDisciplenes(long? studentId)
        //{
        //    ActionResult<Student> actionResult = await Get(studentId);
        //    Student student = actionResult.Value;
        //    HttpClientHandler clientHandler = new HttpClientHandler();
        //    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        //    using (HttpClient client = new HttpClient(clientHandler))
        //    {
        //        return await client.GetStringAsync($"{_compositeSeviceAddress}/disciplenes/{student.GroupName}");
        //    }
        //}

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
