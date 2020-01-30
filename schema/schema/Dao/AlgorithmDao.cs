using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Web.Script.Serialization;

namespace schema.Dao
{
    public class AlgorithmDao
    {
        private Entities db = new Entities();
        public string GetDeptInfo(string patient_id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            using (Entities db = new Entities())
            {
                //patientid deptname deptnum
                var par2 = new ObjectParameter("deptcode", typeof(string));
                var par3 = new ObjectParameter("deptname", typeof(string));
                var par4 = new ObjectParameter("deptnum", typeof(double));
                db.TEST(patient_id,par2,par3,par4);
                dic.Add("deptcode", par2.Value.ToString());
                dic.Add("deptname", par3.Value.ToString());
                dic.Add("deptnum",  par4.Value.ToString());
            }
            return jss.Serialize(dic);
        }
    }
}