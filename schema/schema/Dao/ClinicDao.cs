using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace schema.Dao
{
    public class ClinicDao
    {
        private Entities db = new Entities();

        /// <summary>
        /// 查找科室下所有诊室名称
        /// </summary>
        /// <param name="departid"></param>
        /// <returns></returns>
        internal List<string> getNames(string deptCode)
        {
            List<T_CLINIC> infos = db.T_CLINIC.Where(x => (x.DEPT_CODE == deptCode)).ToList();
            List<string> names = new List<string>();
            foreach(T_CLINIC info in infos)
            {
                names.Add(info.CLINIC_NAME);
            }
            return names;
        }
        /// <summary>
        /// 根据科室名称获取所有诊室id
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public List<decimal> getIds(string deptCode)
        {
            List<T_CLINIC> infos = db.T_CLINIC.Where(x => (x.DEPT_CODE == deptCode)).ToList();
            List<decimal> ids = new List<decimal>();
            foreach (T_CLINIC info in infos)
            {
                ids.Add(info.CLINIC_ID);
            }
            return ids;
        }

        public decimal getId(string IP)
        {
            T_CLINIC[] infos = db.T_CLINIC.Where(x => (x.IP_ADDR == IP)).ToArray();
            //if (infos.Length == 0) return -1;
            return infos[0].CLINIC_ID;
        }
    }
}