using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicroserviceCourse.Models;
using System.Net.Http;
using System.Text.Json;

namespace MicroserviceCourse.Controllers
{
    /// <summary>
    /// Контроллер сервиса курсов.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly CourseContext _context;
        //private readonly string _compositeSeviceAddress = "https://localhost:44354/api/compositesc";

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context"> Контекст базы данных. </param>
        public CoursesController(CourseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Асинхронно возвращает html страницу, содержащую список курсов.
        /// </summary>
        /// <returns> <see cref="ViewResult"/>. </returns>
        //GET: Courses
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        /// <summary>
        /// Асинхронно возвращает все курсы.
        /// </summary>
        /// <returns> Список <see cref="List{T}"/>, где T является <see cref="Course"/>. </returns>
        //GET: Courses/all
        [HttpGet("all")]
        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        /// <summary>
        /// Асинхронно возвращает объект <see cref="Course"/> по имени группы.
        /// </summary>
        /// <param name="groupName"> Имя группы. </param>
        /// <returns> Объект типа <see cref="Course"/>. </returns>
        //Get: Courses/name/{groupName}
        [HttpGet("name/{groupName}")]
        public async Task<Course> GetCourseByNameAsync(string groupName)
        {
            return await _context.Courses.Where(c => c.Name == groupName).FirstAsync();
        }

        /// <summary>
        /// Асинхронно возвращает объект <see cref="Course"/> по id.
        /// </summary>
        /// <param name="id"> Id группы. </param>
        /// <returns> Объект типа <see cref="Course"/>. </returns>
        //Get: Courses/{id}
        [HttpGet("{id}")]
        public async Task<Course> GetCourseByIdAsync(long id)
        {
            return await _context.Courses.FindAsync(id);
        }

        /// <summary>
        /// Добавляет новый курс в базу данных, если полученная модель валидна.
        /// </summary>
        /// <param name="course"> Объект <see cref="Course"/>, составленный из Body запроса. </param>
        /// <returns> <see cref="Index"/>, если объект успешно добавлен. </returns>
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

        /// <summary>
        /// Изменяет курс в базе данных по его id.
        /// </summary>
        /// <param name="id"> Id курса. </param>
        /// <param name="course"> Объект <see cref="Course"/>, составленный из Body запроса. </param>
        //Put: courses/{id}
        [HttpPut("{id}")]
        public async Task Edit(long id, Course course)
        {
            course.Id = id;
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет курс по его id.
        /// </summary>
        /// <param name="id"> Id курса. </param>
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
