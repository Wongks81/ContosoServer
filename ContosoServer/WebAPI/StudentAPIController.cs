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
            try
            {
                List<Student> studList = _ContosoDbContext.dbStudent.ToList<Student>();
                return StatusCode(StatusCodes.Status200OK, studList);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<StudentAPIController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
              
                Student s = (from temp in _ContosoDbContext.dbStudent
                            select temp)
                            .First(temp => temp.StudentId == id);
                if (s == null)
                {
                    return NotFound();
                }
                return StatusCode(StatusCodes.Status200OK, s);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
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
        public IActionResult Put(int id, [FromBody] Student obj)
        {
            try
            {
                Student s = (from temp in _ContosoDbContext.dbStudent
                            select temp)
                            .First(temp => temp.StudentId == id);
                if (s == null)
                {
                    return NotFound();
                }

                s.StudentName = obj.StudentName;
                s.AdmissionDate = obj.AdmissionDate;
                s.CourseId = obj.CourseId;
                _ContosoDbContext.dbStudent.Update(s);
                _ContosoDbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE api/<StudentAPIController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Student s = (from temp in _ContosoDbContext.dbStudent
                            select temp)
                            .First(temp => temp.StudentId == id);
                if (s == null)
                {
                    return NotFound();
                }

                _ContosoDbContext.dbStudent.Remove(s);
                _ContosoDbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
