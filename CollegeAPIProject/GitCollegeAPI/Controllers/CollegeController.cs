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
            _collegeService = collegesServices;
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


        [HttpDelete("ArchiveCollege/{deletecollegerow}")]

        public IActionResult ArchiveCollege(int deletecollegerow)
        {
            return Ok(_collegeService.ArchiveCollege(deletecollegerow));
        }



        [HttpGet("College_Archive")]

        public IActionResult College_Archive()
        {
            return Ok(_collegeService.College_Archive());
        }

        [HttpPostAttribute("CollegeRestore/{archiverestore}")]

        public IActionResult CollegeRestore(int archiverestore)
        {
            return Ok(_collegeService.CollegeRestore(archiverestore));
        }

        [HttpDelete("PermanentDeleteCollege/{collegedelete}")]

        public IActionResult PermanentDeleteCollege(int collegedelete)

        {
            return Ok(_collegeService.PermanentDeleteCollege(collegedelete));
        }

    }
}
