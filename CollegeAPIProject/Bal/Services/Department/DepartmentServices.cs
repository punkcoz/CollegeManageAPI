using Bal.Model;
using BAL.Configurations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Services.Department
{
    public class DepartmentServices: AppDbContext, IDepartmentServices

    {
        public async Task<DataTable> GetDepartmentAndColleges()
        {
            try
            {
                OpenContext();
                string query = "SELECT department.departmentid, department.departmentname, college.collegeid, college.collegename, college.collegelocation FROM collegedb.department, collegedb.college WHERE department.collegeid = college.collegeid;";
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

        public async Task<bool> AddNewDepartment(DepartmentModel department)
        {
            try
            {
                OpenContext();
                var result = await _sqlCommand.AddOrEditWithStoredProcedure("addnewdepartment", null, department, "prm_");
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

        public async Task<bool> DeleteDepartment(int deletedepartmentrow)
        {
            try
            {
                OpenContext();
                string query = $"delete from department where departmentid = {deletedepartmentrow};";
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
