using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoServer.Models;
using ContosoServer.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ContosoServer.Controllers
{
    public class CourseController : Controller
    {
        private readonly ContosoDbContext _ContosoDbContext;

        public CourseController(ContosoDbContext ContosoDbContext)
        {
            _ContosoDbContext = ContosoDbContext;
        }


        //Angular return functions

        public IActionResult returnJSONCourses()
        {
            List<Course> courseList = _ContosoDbContext.dbCourse.ToList<Course>();
            return Json(courseList);
        }

        public IActionResult AddCourse([FromBody] Course obj)
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
                return StatusCode(StatusCodes.Status200OK,obj);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, obj);
            }
        }
    }
}
