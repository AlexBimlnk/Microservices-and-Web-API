using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MircroserviceStudent.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MircroserviceStudent.Controllers
{
    /// <summary>
    /// Контроллер сервиса студентов.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly StudentContext _context;
        //private readonly string _compositeSeviceAddress = "https://localhost:44354/api/compositesc";

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context"> Контекст базы данных. </param>
        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Асинхронно возвращает html страницу, содержащую список студентов.
        /// </summary>
        /// <returns> <see cref="ViewResult"/>. </returns>
        //GET: Students
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        /// <summary>
        /// Асинхронно возвращает всех студентов.
        /// </summary>
        /// <returns> Список <see cref="List{T}"/>, где T является <see cref="Student"/>. </returns>
        //GET: Students/all
        [HttpGet("all")]
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        /// <summary>
        /// Асинхронно возвращает объект <see cref="Student"/> по id.
        /// </summary>
        /// <param name="id"> Id студента. </param>
        /// <returns> Объект типа <see cref="Student"/>. </returns>
        [HttpGet("{id}")]
        public async Task<Student> GetStudentByIdAsync(long? id)
        {
            return await Task.Run(() => _context.Students.FindAsync(id).Result);
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


        /// <summary>
        /// Добавляет нового студента в базу данных, если полученная модель валидна.
        /// </summary>
        /// <param name="student"> Объект <see cref="Student"/>, составленный из Body запроса. </param>
        /// <returns> <see cref="Index"/>, если объект успешно добавлен. </returns>
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

        /// <summary>
        /// Изменяет студента в базе данных по его id.
        /// </summary>
        /// <param name="id"> Id студента. </param>
        /// <param name="student"> Объект <see cref="Student"/>, составленный из Body запроса. </param>
        //Put 
        [HttpPut("{id}")]
        public async Task Edit(long id, Student student)
        {
            student.Id = id;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет студента по его id.
        /// </summary>
        /// <param name="id"> Id студента. </param>
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
