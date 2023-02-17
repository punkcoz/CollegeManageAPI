using Bal.Model;
using Bal.Services.College;
using Bal.Services.Department;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;
namespace PostsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartmentServices _departmentService;

        public DepartmentController(IDepartmentServices departmentServices)
        {
            _departmentService = departmentServices;
        }

        [HttpGet("GetDepartmentAndColleges")]

        public IActionResult GetDepartmentAndColleges()
        {
            return Ok(_departmentService.GetDepartmentAndColleges());
        }

        [HttpPost("AddNewDepartment")]

        public IActionResult AddNewDepartment(DepartmentModel department)
        {
            return Ok(_departmentService.AddNewDepartment(department));
        }

        [HttpDelete("DepartmentArcived/{archivedepartment}")]

        public IActionResult DepartmentArcived(int archivedepartment)
        {
            return Ok(_departmentService.DepartmentArcived(archivedepartment));
        }

        [HttpPut("UpdateDepartment")]
        public IActionResult UpdateDepartment(DepartmentModel updatedepartment)
        {
            return Ok(_departmentService.UpdateDepartment(updatedepartment));
        }

        // Used In Cource 
        [HttpGet("GetAllDepartment/{selectcollegeid}")]

        public IActionResult GetAllDepartment(int selectcollegeid)
        {
            return Ok(_departmentService.GetAllDepartment(selectcollegeid));
        }
        //End

        [HttpGet("DepartmentGetArcivedDept")]

        public IActionResult DepartmentGetArcivedDept()
        {
            return Ok(_departmentService.DepartmentGetArcivedDept());
        }
    }
}
