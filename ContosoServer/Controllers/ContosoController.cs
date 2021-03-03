using ContosoServer.Data;
using ContosoServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoServer.Controllers
{
    public class ContosoController : Controller
    {
        private readonly ContosoDbContext _ContosoDbContext;

        public ContosoController(ContosoDbContext ContosoDbContext)
        {
            _ContosoDbContext = ContosoDbContext;
        }
        public IActionResult returnJSONCount()
        {
            int sCount = _ContosoDbContext.dbStudent.ToList<Student>().Count;
            int cCount = _ContosoDbContext.dbCourse.ToList<Course>().Count;
            var tblCount = new[] {
                        new { name="sCount",count = sCount },
                        new { name="cCount",count = cCount }

            };

            return Json(tblCount);

        }
    }
}
