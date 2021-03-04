using ContosoServer.Data;
using ContosoServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class CourseAPIController : ControllerBase
    {
        private readonly ContosoDbContext _ContosoDbContext;

        public CourseAPIController(ContosoDbContext ContosoDbContext)
        {
            _ContosoDbContext = ContosoDbContext;
        }
        // GET: api/<CourseAPIController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Course> courseList = _ContosoDbContext.dbCourse.ToList<Course>();
                return StatusCode(StatusCodes.Status200OK, courseList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // GET api/<CourseAPIController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                // Cust cust = custDbContext.Custs.First(cust => cust.id == id);
                Course c = (from temp in _ContosoDbContext.dbCourse 
                            select temp)
                            .First(temp => temp.CourseId == id);
                if (c == null)
                {
                    return NotFound();
                }
                return StatusCode(StatusCodes.Status200OK, c);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // POST api/<CourseAPIController>
        [HttpPost]
        public IActionResult Post([FromBody] Course obj)
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
                _ContosoDbContext.dbCourse.Add(obj);
                _ContosoDbContext.SaveChanges();

                //This action needs to return JSON file, so use the return JSON to convert the object to JSON
                return StatusCode(StatusCodes.Status200OK, obj);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, obj);
            }
        }

        // PUT api/<CourseAPIController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Course obj)
        {
            try
            {
                Course c = (from temp in _ContosoDbContext.dbCourse
                            select temp)
                            .First(temp => temp.CourseId == id);
                if (c == null)
                {
                    return NotFound();
                }

                c.CourseName = obj.CourseName;
                _ContosoDbContext.dbCourse.Update(c);
                _ContosoDbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // DELETE api/<CourseAPIController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Course c = (from temp in _ContosoDbContext.dbCourse
                            select temp)
                            .First(temp => temp.CourseId == id);
                if (c == null)
                {
                    return NotFound();
                }

                _ContosoDbContext.dbCourse.Remove(c);
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
