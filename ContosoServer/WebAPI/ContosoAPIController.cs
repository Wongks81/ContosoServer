using ContosoServer.Data;
using ContosoServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContosoServer.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContosoAPIController : ControllerBase
    {
        private readonly ContosoDbContext _ContosoDbContext;

        public ContosoAPIController(ContosoDbContext ContosoDbContext)
        {
            _ContosoDbContext = ContosoDbContext;
        }
        // GET: api/<ContosoAPIController>
        [HttpGet]
        public IActionResult Get()
        {
            int sCount = _ContosoDbContext.dbStudent.ToList<Student>().Count;
            int cCount = _ContosoDbContext.dbCourse.ToList<Course>().Count;
            var tblCount = new[] {
                        new { name="sCount",count = sCount },
                        new { name="cCount",count = cCount }

            };
            return StatusCode(StatusCodes.Status200OK, tblCount);
        }

        // GET api/<ContosoAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContosoAPIController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ContosoAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContosoAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
