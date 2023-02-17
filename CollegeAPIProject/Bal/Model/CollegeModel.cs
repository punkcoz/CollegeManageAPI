using System;
using System.Collections.Generic;
using System.Text;

namespace Bal.Model
{
    //college_id, college_name, college_location
    public class CollegeModel
    {
        public int collegeid { get; set; }
        public string collegename { get; set; }

        public string collegelocation { get; set; }

        public string collegedetails { get; set; }
       
        public int isarchive { get; set; }

        public int college_logid { get; set; }

        public DateTime college_log_date_time { get; set; }
    }
}
