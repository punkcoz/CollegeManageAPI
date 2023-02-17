using Bal.Model;
using BAL.Configurations;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
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
        public async Task<bool> ArchiveCollege(int deletecollegerow)
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
                var query = $"select * from college where collegeid = {deletecollegerow};";
                DataTable college = await _sqlCommand.Select_Table(query, CommandType.Text);
                if (college.Rows.Count > 0)
                {
                    _sqlCommand.Add_Parameter_WithValue("prm_college_log_date_time", DateTime.Now);
                    _sqlCommand.Add_Parameter_WithValue("prm_collegename", college.Rows[0]["collegename"]);
                    _sqlCommand.Add_Parameter_WithValue("prm_collegeid", college.Rows[0]["collegeid"]);
                    _sqlCommand.Add_Parameter_WithValue("prm_collegelocation", college.Rows[0]["collegelocation"]);
                    _sqlCommand.Add_Parameter_WithValue("prm_collegedetails", college.Rows[0]["collegedetails"]);
                    var archiveCollegeDetail = await _sqlCommand.Execute_Query("college_archive", CommandType.StoredProcedure);

                    if (archiveCollegeDetail)
                    {
                        string deletequery = $"delete from college where collegeid = {deletecollegerow};";
                        bool isDeleted = await _sqlCommand.Execute_Query(deletequery, CommandType.Text);
                        if (isDeleted)
                        {
                            myTrans.Commit();

                        }
                        if (!isDeleted)
                        {
                            myTrans.Rollback();
                            string rollback = $"delete from college_log where collegeid = {deletecollegerow};";
                            bool isrollback = await _sqlCommand.Execute_Query(rollback, CommandType.Text);
                            return isrollback;

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
                //string rollback = $"delete from college_log where collegeid = {deletecollegerow};";
                //bool isrollback = await _sqlCommand.Execute_Query(rollback, CommandType.Text);
                //return isrollback;
                myTrans.Rollback();
                throw ex;
            }
            finally
            {
                CloseContext();
            }
        }




        public async Task<DataTable> College_Archive()
        {
            try
            {
                OpenContext();
                string query = "select * from college_log;";
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

        public async Task<bool> CollegeRestore(int archiverestore)
        {


            OpenContext();
            try
            {
                _sqlCommand.Clear_CommandParameter();
                var query = $"select * from college_log where collegeid = {archiverestore};";
                DataTable collegearchive = await _sqlCommand.Select_Table(query, CommandType.Text);
                if (collegearchive.Rows.Count > 0)
                {
                    _sqlCommand.Add_Parameter_WithValue("prm_collegename", collegearchive.Rows[0]["collegename"]);
                    _sqlCommand.Add_Parameter_WithValue("prm_collegeid", collegearchive.Rows[0]["collegeid"]);
                    _sqlCommand.Add_Parameter_WithValue("prm_collegelocation", collegearchive.Rows[0]["collegelocation"]);
                    _sqlCommand.Add_Parameter_WithValue("prm_collegedetails", collegearchive.Rows[0]["collegedetails"]);
                    var CollegeRestoreSuccess = await _sqlCommand.Execute_Query("college_restore", CommandType.StoredProcedure);

                    if (CollegeRestoreSuccess)
                    {
                        string deletequery = $"delete from college_log where collegeid = {archiverestore};";
                        bool isDeleted = await _sqlCommand.Execute_Query(deletequery, CommandType.Text);
                        return isDeleted;
                    }

                }
                return true;

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

        public async Task<bool> PermanentDeleteCollege(int collegedelete)
        {
            try
            {
                OpenContext();
                string query = $"delete from college_log where college_logid = {collegedelete};";
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

