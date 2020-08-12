using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace schema.Dao
{
    public class PatientDao
    {
        private Entities db = new Entities();
        public  List<string> getIds(string deptId,int status1, int status2)
        {
            List<T_QUEUE_LIST> infos =  db.T_QUEUE_LIST.Where(x => (x.DAPART_CODE == deptId && (x.STATUS == status1 || x.STATUS == status2))).ToList();
            List<string> ids = new List<string>();
            foreach(T_QUEUE_LIST info in infos)
            {
                ids.Add(info.PATIENT_ID);
            }
            return ids;
        }

        public string getName(string pid)
        {
            PHYSICAL_MASTER_INDEX[] infos = db.PHYSICAL_MASTER_INDEX.Where(x => (x.PATIENT_ID == pid)).ToArray();
            if (infos.Length == 0) return "";
            return infos[0].NAME;
        }
        /// <summary>
        /// 获取某个科室下某个诊室下某种状态的病人id
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="cid"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string getId(string deptId, decimal cid, int states)
        {
            T_QUEUE_LIST[] info = db.T_QUEUE_LIST.Where(x => (x.DAPART_CODE == deptId && x.CLINIC_ID == cid && x.STATUS == states)).ToArray();
            if (info.Length == 0) return ""; 
            return info[0].PATIENT_ID;
        }
    }
}