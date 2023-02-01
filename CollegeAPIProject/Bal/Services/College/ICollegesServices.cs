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

        Task<bool> AddNewCollege(CollegeModel college);
        //object GetCollegeById(CollegeModel college);

        Task<bool> UpdateCollege(CollegeModel updatecollege);

        Task<bool> DeleteCollege(int deletecollegerow);

        //Task<DataSet> GetAllCollegeAndCourse();
    }
}
