using Bal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Services.Department
{
    public interface IDepartmentServices
    {
        Task<DataTable> GetDepartmentAndColleges();
        Task<bool> AddNewDepartment(DepartmentModel department);

        Task<bool> DeleteDepartment(int deletedepartmentrow);
    }
    
}
