using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace schema.Dao
{
    public class AlgorithmDao
    {
        private Entities db = new Entities();
        public string GetDeptCode(string patient_id)
        {
            patient_id = "123";
            string deptcode, deptname;
            double deptnum;
            using (Entities db = new Entities())
            {
                //patientid deptname deptnum
                var par2 = new ObjectParameter("deptcode", typeof(string));
                var par3 = new ObjectParameter("deptname", typeof(string));
                var par4 = new ObjectParameter("deptnum", typeof(double));
                db.TEST(patient_id,par2,par3,par4);
                deptcode = par2.Value.ToString();
                deptname = par3.Value.ToString();
                deptnum = Double.Parse(par4.Value.ToString());
            }
            return deptcode;
        }
    }
}