using Bal.Model;
using BAL.Configurations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Services.College
{
    public class CollegeServices : AppDbContext, ICollegesServices
    {

        public async Task<DataTable> GetAllCollege()
        {
            try
            {
                OpenContext();
                string query = "select * from college;";
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
        //public async Task<DataTable> GetCollegeById(int collegeid)
        //{
        //    try
        //    {
        //        OpenContext();
        //        string query = $"select * from college where collegeid = {collegeid};";
        //        DataTable result = await _sqlCommand.Select_Table(query, CommandType.Text);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        CloseContext();
        //    }
        //}

        public async Task<bool> AddNewCollege(CollegeModel college)
        {
            try
            {
                OpenContext();
                var result = await _sqlCommand.AddOrEditWithStoredProcedure("college_addnewcollege", null, college, "prm_");
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
        public async Task<bool> UpdateCollege(CollegeModel updatecollege)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var result = await _sqlCommand.AddOrEditWithStoredProcedure("college_updatecollege", null, updatecollege, "prm_");
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
        public async Task<bool> DeleteCollege(int deletecollegerow)
        {
            try
            {
                OpenContext();
                string query = $"delete from college where collegeid = {deletecollegerow};";
                bool isDeleted = await _sqlCommand.Execute_Query(query, CommandType.Text);
                string msg;

                //msg = isDeleted ? $"College {model.college_name} ,  Deleted Successfully !" : "Failed Deleting";
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

        //public async Task<DataSet> GetAllCollegeAndCourse()
        //{
        //    try
        //    {
        //        OpenContext();
        //        string query = "select * from college;SELECT * FROM course;";
        //        DataSet result = await _sqlCommand.Select_TableSet(query, CommandType.Text);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    { 
        //        CloseContext(); 
        //    }
        //}

    }
}

