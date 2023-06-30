using BigBangAPI.Models;
using BigBangAPI.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BigBangAPI.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public IActionResult Get()
        {
            var pat = _context.patients.ToList();
            return Ok(pat);
        }

        [HttpGet("Requirements")]
        public IActionResult GetReq()
        {

            var pat = _context.patients
                .Select(s => new patientview
                {
                    Name = s.Name,
                    Age = s.Age,
                    Gender = s.Gender
                }).ToList();

            return Ok(pat);
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pat = _context.patients.Find(id);

            if (pat == null)
            {
                return NotFound();
            }

            return Ok(pat);
        }

        // POST: api/Student
        [HttpPost]
        public IActionResult Post([FromBody] Patients pat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.patients.Add(pat);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = pat.Id }, pat);
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Patients pat)
        {
            if (id != pat.Id)
            {
                return BadRequest();
            }

            _context.Update(pat);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pat = _context.patients.Find(id);

            if (pat == null)
            {
                return NotFound();
            }

            _context.patients.Remove(pat);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
