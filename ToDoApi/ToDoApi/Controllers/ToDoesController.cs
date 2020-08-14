using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ToDoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetToDos()
        {
            return await _context.ToDos.ToListAsync();
        }

        // GET: api/ToDoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> GetToDo(int id)
        {
            var toDo = await _context.ToDos.FindAsync(id);

            if (toDo == null)
            {
                return NotFound();
            }

            return toDo;
        }

        // GET: api/ToDoes/GetTodayToDos
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetTodayToDos()
        {
            return await _context.ToDos.Where(x => x.ExpiryDate.Date == DateTime.Today).ToListAsync();
        }

        // GET: api/ToDoes/GetNextdayToDos
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetNextdayToDos()
        {
            return await _context.ToDos.Where(x => x.ExpiryDate.Date == DateTime.Today.AddDays(1)).ToListAsync();
        }

        // GET: api/ToDoes/GetCurrentWeekToDos
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetCurrentWeekToDos()
        {
            //get first day of week
            DateTime firstDay = DateTime.Today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)DateTime.Today.DayOfWeek);
            //get last day of week, 1 second before next week first day
            DateTime lastDay = firstDay.AddDays(7).AddSeconds(-1);

            return await _context.ToDos.Where(x => x.ExpiryDate >= firstDay && x.ExpiryDate <= lastDay).ToListAsync();
        }

        // PUT: api/ToDoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo(int id, ToDo toDo)
        {
            if (id != toDo.Id)
            {
                return BadRequest("ToDo Id is not match.");
            }

            _context.Entry(toDo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
                {
                    return NotFound("ToDo Id is not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ToDoes
        [HttpPost]
        public async Task<ActionResult<ToDo>> PostToDo(ToDo toDo)
        {
                _context.ToDos.Add(toDo);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetToDo", new { id = toDo.Id }, toDo);
        }

        // DELETE: api/ToDoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDo>> DeleteToDo(int id)
        {
            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            _context.ToDos.Remove(toDo);
            await _context.SaveChangesAsync();

            return toDo;
        }

        // POST: api/ToDoes/UpdatePercentComplete
        [Route("[action]/{id}/{percentComplete}")]
        [HttpPut]
        public async Task<ActionResult<ToDo>> UpdatePercentComplete(int id, double percentComplete)
        {
            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo == null)
            {
                return NotFound("ToDo Id cannot be found.");
            }

            //check if percent complete value is not in accepted range
            if (percentComplete < 0 || percentComplete > 100)
            {
                return BadRequest("Percent complete should be between 0 and 100.");
            }

            toDo.PercentComplete = percentComplete;
            await _context.SaveChangesAsync();

            return toDo;
        }

        // POST: api/ToDoes/CompleteToDo
        [Route("[action]/{id}")]
        [HttpPut]
        public async Task<ActionResult<ToDo>> CompleteToDo(int id)
        {
            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo == null)
            {
                return NotFound("ToDo Id cannot be found.");
            }

            toDo.PercentComplete = 100;
            await _context.SaveChangesAsync();

            return toDo;
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDos.Any(e => e.Id == id);
        }
    }
}
