using Bal.Model;
using BAL.Configurations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Services.Course
{
    public class CourseServices : AppDbContext, ICourseServices
    { 
        public async Task<DataTable> GetCourseDepartmentAndColleges()
        {
            try
            {
                OpenContext();
                string query = "SELECT department.departmentid, department.departmentname, college.collegeid, college.collegename, college.collegelocation, course.courseid, course.coursename FROM collegedb.department, collegedb.college, collegedb.course WHERE course.departmentid = department.departmentid && course.collegeid=college.collegeid;";
                DataTable result = await _sqlCommand.Select_Table(query, CommandType.Text);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }

        }
        public async Task<bool> AddNewCourse(CourseModel course)
        {
            try
            {
                OpenContext();
                var result = await _sqlCommand.AddOrEditWithStoredProcedure("course_addnewcourse", null, course, "prm_");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<bool> UpdateCourse(CourseModel updatecourse)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var result = await _sqlCommand.AddOrEditWithStoredProcedure("course_updatecourse", null, updatecourse, "prm_");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                CloseContext();
            }
        }

        public async Task<bool> DeleteCourse(int deletecourse)
        {
            try
            {
                OpenContext();
                string query = $"delete from course where courseid = {deletecourse};";
                bool isDeleted = await _sqlCommand.Execute_Query(query, CommandType.Text);
                return isDeleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }
    }
}
