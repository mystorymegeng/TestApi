using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserContext _context;
        public UserController(UserContext context)
        {
            _context = context;

            if (_context.Usertests.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Usertests.Add(new Usertest { Name = "Item1" });
                _context.SaveChanges();
                
            }


        }

        [HttpGet]
        public ActionResult<List<Usertest>> GetAll()
        {
            return _context.Usertests.ToList();
        }



        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<Usertest> GetById(long id)
        {
            var item = _context.Usertests.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
         

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public IActionResult Create(Usertest item)
        {
            if (item.Name == null || item.Username == null || item.Password == null || item.Email == null || item.Age == 0)
            {
                
                return StatusCode(400);
                //return BadRequest();
                //return NoContent();
                //Responses
            }
            else
            {
                _context.Usertests.Add(item);
                _context.SaveChanges();

                return CreatedAtRoute("GetUser", new { id = item.Id }, item);
            }

        }


        [HttpPut("{id}")]
        public IActionResult Update(long id, Usertest item)
        {
            var todo = _context.Usertests.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Name = item.Name;
            todo.Username = item.Username;
            todo.Password = item.Password;
            todo.Age = item.Age;

            _context.Usertests.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Usertests.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Usertests.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
