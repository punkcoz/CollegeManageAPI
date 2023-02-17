using Bal.Model;
using Bal.Services.Course;
using Microsoft.AspNetCore.Mvc;

namespace PostsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private ICourseServices _courseServices;

        public CourseController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
        }

        [HttpGet("GetCourseDepartmentAndColleges")]
        public IActionResult GetCourseDepartmentAndColleges()
        {
            return Ok(_courseServices.GetCourseDepartmentAndColleges());
        }

        [HttpPost("AddNewCourse")]

        public IActionResult AddNewCourse(CourseModel course)
        {
            return Ok(_courseServices.AddNewCourse(course));
        }


        [HttpPut("UpdateCourse")]
        public IActionResult UpdateCourse(CourseModel updatecourse)
        {
            return Ok(_courseServices.UpdateCourse(updatecourse));
        }

        [HttpDelete("DeleteCourse/{deletecourse}")]

        public IActionResult DeleteCourse(int deletecourse)
        {
            return Ok(_courseServices.DeleteCourse(deletecourse));
        }
    }
}
