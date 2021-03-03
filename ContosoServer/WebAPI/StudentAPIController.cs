using ContosoServer.Data;
using ContosoServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContosoServer.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly ContosoDbContext _ContosoDbContext;

        public StudentAPIController(ContosoDbContext ContosoDbContext)
        {
            _ContosoDbContext = ContosoDbContext;
        }
        // GET: api/<StudentAPIController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Student> studList = _ContosoDbContext.dbStudent.ToList<Student>();
            return StatusCode(StatusCodes.Status200OK, studList);
        }

        // GET api/<StudentAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StudentAPIController>
        [HttpPost]
        public IActionResult Post([FromBody] Student obj)
        {
            //Check validations
            //Check the data if validation is ok
            var context = new ValidationContext(obj, null, null);

            //Store the list of errors
            var errorresult = new List<ValidationResult>();

            //Check if the object is valid
            var isValid = Validator.TryValidateObject(obj, context, errorresult, true);

            if (isValid)
            {
                _ContosoDbContext.Entry(obj.Course).State = EntityState.Unchanged;
                _ContosoDbContext.Entry(obj).State = EntityState.Added;
                //_ContosoDbContext.dbStudent.Add(obj);
                _ContosoDbContext.SaveChanges();

                //This action needs to return JSON file, so use the return JSON to convert the object to JSON
                return StatusCode(StatusCodes.Status200OK, obj);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, errorresult);
            }
        }

        // PUT api/<StudentAPIController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StudentAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
