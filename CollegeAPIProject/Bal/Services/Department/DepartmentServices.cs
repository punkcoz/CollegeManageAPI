using Bal.Model;
using BAL.Configurations;
using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
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
                var result = await _sqlCommand.AddOrEditWithStoredProcedure("department_addnewdepartment", null, department, "prm_");
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



        public async Task<bool> DepartmentArcived(int archivedepartment)
        {

            OpenContext();
            //Author Pankaj

            MySqlTransaction myTrans = _connection._Connection.BeginTransaction(IsolationLevel.Serializable);
            _sqlCommand.Add_Transaction(myTrans);
            try
            {
                //Added Above
                _sqlCommand.Clear_CommandParameter();
                //var result = await _sqlCommand.AddOrEditWithStoredProcedure("college_archive", null, deletecollegerow, "prm_");
                var query = $"select * from department where departmentid = {archivedepartment};";
                DataTable department = await _sqlCommand.Select_Table(query, CommandType.Text);

                //DepartmentModel departmentModel = DataTableVsListOfType.ConvertDataTableToModel<DepartmentModel>(department.Rows[0]);

                //departmentModel.department_logdatetime = DateTime.Now;
                //var success = await _sqlCommand.AddOrEditWithStoredProcedure("department_archive", null, departmentModel,"prm_");

                if (department.Rows.Count > 0)
                {

                    _sqlCommand.Add_Parameter_WithValue("prm_department_logdatetime", DateTime.Now);
                    _sqlCommand.Add_Parameter_WithValue("prm_departmentname", department.Rows[0]["departmentname"]);
                    _sqlCommand.Add_Parameter_WithValue("prm_departmentid", department.Rows[0]["departmentid"]);
                    _sqlCommand.Add_Parameter_WithValue("prm_collegeid", department.Rows[0]["collegeid"]);
                    var archiveDepartmentDetail = await _sqlCommand.Execute_Query("department_archive", CommandType.StoredProcedure);

                    if (archiveDepartmentDetail)
                    {
                        string deletequery = $"delete from department where departmentid = {archivedepartment};";
                        bool isDeleted = await _sqlCommand.Execute_Query(deletequery, CommandType.Text);
                        if (isDeleted)
                        {
                            myTrans.Commit();

                        }

                        return isDeleted;
                    }
                    myTrans.Rollback();
                }
                myTrans.Rollback();
                return false;
            }
            catch (Exception ex)
            {
                myTrans.Rollback();
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }

        //public async Task<bool> DepartmentArcived(int archivedepartment)
        //{
        //    try
        //    {
        //        OpenContext();
        //        string query = $"delete from department where departmentid = {deletedepartment};";
        //        bool isDeleted = await _sqlCommand.Execute_Query(query, CommandType.Text);
        //        return isDeleted;
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

        public async Task<bool> UpdateDepartment(DepartmentModel updatedepartment)
        {
            try
            {
                OpenContext();
                _sqlCommand.Clear_CommandParameter();
                var result = await _sqlCommand.AddOrEditWithStoredProcedure("department_updatedepartment", null, updatedepartment, "prm_");
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


        // Used In Cource Model
        public async Task<DataTable> GetAllDepartment(int selectcollegeid)
        {
            try
            {
                OpenContext();
                string query = $"select * from department where collegeid = {selectcollegeid};";
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
        // End

        public async Task<DataTable> DepartmentGetArcivedDept()
        {
            try
            {
                OpenContext();               
                string query = "SELECT department_log.department_logid, department_log.departmentname, department_log.departmentid, department_log.department_logdatetime, college.collegeid, college.collegename, college.collegelocation FROM collegedb.department_log, collegedb.college WHERE department_log.collegeid = college.collegeid;";
                DataTable departmentlog = await _sqlCommand.Select_Table(query, CommandType.Text);
                return departmentlog;
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
