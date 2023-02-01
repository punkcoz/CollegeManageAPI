using Bal.Model;
using Bal.Services.College;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;

namespace PostsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        private ICollegesServices _collegeService;

        public CollegeController(ICollegesServices collegesServices)
        {
            _collegeService= collegesServices;
        }


        [HttpGet("GetAllCollege")]

        public IActionResult GetAllCollege()
        {
            return Ok(_collegeService.GetAllCollege());
        }

        //[HttpGet("GetCollegeById/{collegeid}")]  

        //public IActionResult GetCollegeById(int collegeid)
        //{
        //    return Ok(_collegeService.GetCollegeById(collegeid));
        //}

        [HttpPost("AddNewCollege")]

        public IActionResult AddNewCollege(CollegeModel college)
        {
            return Ok(_collegeService.AddNewCollege(college));
        }

        [HttpPut("UpdateCollege")]
        public IActionResult UpdateCollege(CollegeModel UpdateCollege)
        {
            return Ok(_collegeService.UpdateCollege(UpdateCollege));
        }
        
        
        [HttpDelete("DeleteCollege/{deletecollegerow}")]

        public IActionResult DeleteCollege(int deletecollegerow)
        {
            return Ok(_collegeService.DeleteCollege(deletecollegerow));
        }
        
        //[HttpGet("getallcollegeandcourse")]

        //public IActionResult getallcollegeandcourse()
        //{
        //    return Ok(_collegeService.GetAllCollegeAndCourse());
        //}

    }
}
