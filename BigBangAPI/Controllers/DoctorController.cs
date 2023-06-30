using BigBangAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BigBangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "admin")]
        // GET: api/Teacher
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetTeachers()
        {
            var doc = await _context.doctors.ToListAsync();
            return Ok(doc);
        }

        [Authorize(Roles = "admin")]
        // GET: api/Teacher/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetTeacher(int id)
        {
            var doc = await _context.doctors.FindAsync(id);

            if (doc == null)
            {
                return NotFound();
            }

            return Ok(doc);
        }

        [Authorize(Roles = "admin")]
        // POST: api/Teacher
        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateTeacher(Doctor doc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.doctors.Add(doc);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeacher), new { id = doc.Id }, doc);
        }

        [Authorize(Roles = "admin")]
        // PUT: api/Teacher/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, Doctor doc)
        {
            if (id != doc.Id)
            {
                return BadRequest();
            }

            _context.Entry(doc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        // DELETE: api/Teacher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var doc = await _context.doctors.FindAsync(id);

            if (doc == null)
            {
                return NotFound();
            }

            _context.doctors.Remove(doc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return _context.doctors.Any(t => t.Id == id);
        }
    }
}
