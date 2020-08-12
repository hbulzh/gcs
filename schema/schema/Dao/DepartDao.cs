using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace schema.Dao
{
    public class DepartDao
    {
        private Entities db = new Entities();

        /// <summary>
        /// 获取病人下一站科室id
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string getId(string patientid, int status)
        {
            T_QUEUE_LIST[] infos = db.T_QUEUE_LIST.Where(x => (x.PATIENT_ID == patientid && x.STATUS == status)).ToArray();
            if (infos.Length == 0) return "";
            return infos[0].DAPART_CODE;
        }

        public string getName(string deptCode)
        {
            DEPT_DICT[] infos = db.DEPT_DICT.Where(x => (x.DEPT_CODE == deptCode)).ToArray();
            if (infos.Length == 0) return "";
           return infos[0].DEPT_NAME;

        }
        /// <summary>
        /// 获取诊室所在科室id
        /// </summary>
        /// <param name="clinicId"></param>
        /// <returns></returns>

        internal string getId(decimal clinicId)
        {
            T_CLINIC[] infos = db.T_CLINIC.Where(x => (x.CLINIC_ID == clinicId)).ToArray();
            if (infos.Length == 0) return "";
            return infos[0].DEPT_CODE;
        }
    }
}