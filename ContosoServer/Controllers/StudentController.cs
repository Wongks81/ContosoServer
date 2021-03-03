using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoServer.Models;
using ContosoServer.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ContosoServer.Controllers
{
    public class StudentController : Controller
    {
        private readonly ContosoDbContext _ContosoDbContext;

        public StudentController(ContosoDbContext ContosoDbContext)
        {
            _ContosoDbContext = ContosoDbContext;
        }

        //Functions and Actions form Angular
         
        public JsonResult returnJSONStudents()
        {
            //replace in webAPI
            IEnumerable<Student> studList = _ContosoDbContext.dbStudent;
            //List<Student> studList = _ContosoDbContext.dbStudent.ToList<Student>();

            return Json(studList);
        }

        public IActionResult AddStudent([FromBody] Student obj)
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
                return StatusCode(StatusCodes.Status200OK,obj);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, errorresult);
            }
        }
    }
}
