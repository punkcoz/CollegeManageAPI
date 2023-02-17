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

        Task<bool> DepartmentArcived(int archivedepartment);

        Task<bool> UpdateDepartment(DepartmentModel updatedepartment);


        // Used In Cource Model
        Task<DataTable> GetAllDepartment(int selectcollegeid);
        // End
        Task<DataTable> DepartmentGetArcivedDept();
    }
    
}
