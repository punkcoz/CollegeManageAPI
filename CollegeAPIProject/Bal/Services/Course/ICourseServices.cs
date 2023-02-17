using Bal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Services.Course
{
    public interface ICourseServices
    {
        Task<DataTable> GetCourseDepartmentAndColleges();

        Task<bool> AddNewCourse(CourseModel course);

        Task<bool> UpdateCourse(CourseModel updatecourse);

        Task<bool> DeleteCourse(int deletecourse);
    }
}
