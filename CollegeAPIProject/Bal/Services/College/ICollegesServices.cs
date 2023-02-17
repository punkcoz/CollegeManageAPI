using Bal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Services.College
{
    public interface ICollegesServices
    {
        Task<DataTable> GetAllCollege();
        //Task<DataTable> GetCollegeById(int collegeid);

        Task<DataTable> College_Archive();

        Task<bool> AddNewCollege(CollegeModel college);
        //object GetCollegeById(CollegeModel college);

        Task<bool> UpdateCollege(CollegeModel updatecollege);

        Task<bool> ArchiveCollege(int deletecollegerow);

        Task<bool> CollegeRestore(int archiverestore);

        Task<bool> PermanentDeleteCollege(int collegedelete);

    }
}
