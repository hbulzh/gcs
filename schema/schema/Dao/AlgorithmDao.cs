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
        JavaScriptSerializer jss = new JavaScriptSerializer();
        public string GetNxtDeptInfo(string patient_id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var par2 = new ObjectParameter("deptcode", typeof(string));
            var par3 = new ObjectParameter("deptname", typeof(string));
            var par4 = new ObjectParameter("deptnum", typeof(double));
            db.ADDQUEUE(patient_id,par2,par3,par4);
            dic.Add("deptcode", par2.Value.ToString());
            dic.Add("deptname", par3.Value.ToString());
            dic.Add("deptnum", par4.Value.ToString());
            return jss.Serialize(dic);
        }
        public string getDeptCode(string deptname)
        {
            DEPT_DICT[] user = db.DEPT_DICT.Where(x => (x.DEPT_NAME == deptname)).ToArray();
            if (user.Length > 0) return user[0].DEPT_CODE;
            return null;
        }
        public int GetDeptNum(string deptcode)
        {
            return db.T_QUEUE_LIST.Where(x => (x.DAPART_CODE == deptcode && x.COMPLETE_TIME == null)).ToArray().Length;
        }
        public void overNumber(string patientid,string deptcode)
        {
            db.OVER_NUMBER(patientid, deptcode);
        }
        public string GetUserInfo(string deptcode,int deptnum)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            T_QUEUE_LIST[] user = db.T_QUEUE_LIST.Where(x => (x.DAPART_CODE == deptcode && x.QUE_NUM==deptnum && x.COMPLETE_TIME == null)).ToArray();
            PHYSICAL_MASTER_INDEX[] user1 = db.PHYSICAL_MASTER_INDEX.Where(x => (x.ID_CARD == user[0].ID_CARD)).ToArray();
            dic.Add("name", user1[0].NAME);
            dic.Add("sex", user1[0].SEX_CODE);
            dic.Add("age", user1[0].AGE.ToString());
            return jss.Serialize(dic);
        }
        public string GetFirstUserInfo(string deptcode)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            T_QUEUE_LIST[] user = db.T_QUEUE_LIST.Where(x => x.DAPART_CODE == deptcode && x.COMPLETE_TIME == null).OrderBy(x => x.QUE_NUM).ToArray();
            if (user.Length == 0) return null;
            string ID_CARD = user[0].ID_CARD;
            PHYSICAL_MASTER_INDEX user1 = db.PHYSICAL_MASTER_INDEX.Where(x => (x.ID_CARD == ID_CARD)).ToList()[0];
            dic.Add("name", user1.NAME);
            dic.Add("sex", user1.SEX_CODE);
            dic.Add("age", user1.AGE.ToString());
            return jss.Serialize(dic);
        }
        public string getPatientId(string username)
        {
            PHYSICAL_MASTER_INDEX[] user1 = db.PHYSICAL_MASTER_INDEX.Where(x => (x.NAME == username)).ToArray();
            if(user1.Length > 0) return user1[0].PATIENT_ID;
            return null;
        }
    }
}