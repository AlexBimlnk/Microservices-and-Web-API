using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicroserviceCourse.Models;
using Interfaces;
using System.Net.Http;
using System.Text.Json;

namespace MicroserviceCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly CourseContext _context;
        private readonly string _compositeSeviceAddress = "https://localhost:44354/api/compositesc";

        public CoursesController(CourseContext context)
        {
            _context = context;
        }

        //GET: Courses
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        //GET: Courses/all
        [HttpGet("all")]
        public async Task<string> GetAllCourses()
        {
            var courses = await _context.Courses.ToListAsync();

            return JsonSerializer.Serialize(courses);
        }

        //Get: Courses/name/{groupName}
        [HttpGet("name/{groupName}")]
        public async Task<string> GetCourseByName(string groupName)
        {
            var course = await _context.Courses.Where(c => c.Name == groupName).FirstAsync();
            return JsonSerializer.Serialize(course);
        }

        // POST: Courses
        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        //Put: courses/{id}
        [HttpPut("{id}")]
        public async Task Edit(long id, Course course)
        {
            course.Id = id;
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        //Delete: courses/{id}
        [HttpDelete("{id}")]
        public async Task DeleteConfirmed(long id)
        {
            var student = await _context.Courses.FindAsync(id);
            _context.Courses.Attach(student);
            _context.Courses.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}
