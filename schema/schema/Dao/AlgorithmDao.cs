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

            var par2 = new ObjectParameter("res_deptcode", typeof(string));
            var par3 = new ObjectParameter("res_deptname", typeof(string));
            var par4 = new ObjectParameter("res_deptnum", typeof(int));

            db.COMPUTE_TWAIT(patient_id, par2, par3, par4);
            dic.Add("deptcode", par2.Value.ToString());
            dic.Add("deptname", par3.Value.ToString());
            dic.Add("deptnum", par4.Value.ToString());

            return jss.Serialize(dic);

        }
        public string getDeptCode(String deptName)
        {
            DEPT_DICT[] user = db.DEPT_DICT.Where(x => (x.DEPT_NAME == deptName)).ToArray();
            return user[0].DEPT_CODE;
        }
        public string getDeptCode(decimal clinicid)
        {
            T_CLINIC[] clinics = db.T_CLINIC.Where(x => x.CLINIC_ID == clinicid).ToArray();
            return clinics[0].DEPT_CODE;
        }
        public int GetDeptNum(string deptcode)
        {
            return db.T_QUEUE_LIST.Where(x => (x.DAPART_CODE == deptcode && x.STATUS == 0)).ToArray().Length;
        }
        public void overNumber(string patientid, string deptcode)
        { 
                db.OVER_NUMBER(patientid, deptcode);
        }
        public string GetUserInfo(string deptcode, string patientid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            T_QUEUE_LIST[] user = db.T_QUEUE_LIST.Where(x => (x.DAPART_CODE == deptcode && x.PATIENT_ID == patientid)).ToArray();
            PHYSICAL_MASTER_INDEX[] user1 = db.PHYSICAL_MASTER_INDEX.Where(x => (x.PATIENT_ID == user[0].PATIENT_ID)).ToArray();
            dic.Add("name", user1[0].NAME);
            dic.Add("sex", user1[0].SEX_CODE == "1" ? "男" : "女");
            dic.Add("age", user1[0].AGE.ToString());
            return jss.Serialize(dic);
        }
        public string GetFirstUserInfo(string deptcode)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                T_QUEUE_LIST[] user = db.T_QUEUE_LIST.Where(x => x.DAPART_CODE == deptcode && x.STATUS == 0).OrderBy(x => x.QUE_NUM).ToArray();
                string patient_id = user[0].PATIENT_ID;
                PHYSICAL_MASTER_INDEX user1 = db.PHYSICAL_MASTER_INDEX.Where(x => (x.PATIENT_ID == patient_id)).ToList()[0];
                dic.Add("name", user1.NAME);
                dic.Add("sex", user1.SEX_CODE == "1" ? "男":"女");
                dic.Add("age", user1.AGE.ToString());
                return jss.Serialize(dic);
            }
            catch
            {
                throw new Exception("获取科室下第一个人信息失败");
            }
            
        }
        public string getPatientId(string username) 
        { 
            try
            {
                PHYSICAL_MASTER_INDEX[] user1 = db.PHYSICAL_MASTER_INDEX.Where(x => (x.NAME == username)).ToArray();
                return user1[0].PATIENT_ID;
            }
            catch
            {
                return "Fail";
            }
             
        }

        public void updateCompleteStatus(string patientid, string deptcode)
        {
            T_QUEUE_LIST[] users = db.T_QUEUE_LIST.Where(x => x.PATIENT_ID == patientid && x.DAPART_CODE == deptcode && x.STATUS == 1).ToArray();
            if (users.Length == 0) return;
            users[0].COMPLETE_TIME = System.DateTime.Now;
            users[0].STATUS = 2;
            db.SaveChanges();
        }
        public string GetUserInfo(string patientid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            PHYSICAL_MASTER_INDEX[] user1 = db.PHYSICAL_MASTER_INDEX.Where(x => (x.PATIENT_ID == patientid)).ToArray();
            dic.Add("name", user1[0].NAME);
            dic.Add("sex", user1[0].SEX_CODE);
            dic.Add("age", user1[0].AGE.ToString());
            return jss.Serialize(dic);
        }
        public decimal getClinicId(string IP)
        {
            T_CLINIC[] clinics = db.T_CLINIC.Where(x => x.IP_ADDR == IP).ToArray();
            return clinics[0].CLINIC_ID;
        }
        
        public string getDeptName(string deptCode)
        {
            DEPT_DICT[] dicts = db.DEPT_DICT.Where(x => x.DEPT_CODE == deptCode).ToArray();
            return dicts[0].DEPT_NAME;
        }
        
        public void updatePatientStatus(string patientid,string deptCode, Nullable<DateTime> now, int status, Nullable<decimal> clinicId,Nullable<decimal> doctorId)
        {
            T_QUEUE_LIST[] users = db.T_QUEUE_LIST.Where(x => x.PATIENT_ID == patientid && x.DAPART_CODE == deptCode && x.STATUS != status && x.STATUS != 2).ToArray();
            if (users.Length == 0) return;
            users[0].CALL_TIME = now;
            users[0].STATUS = status;
            users[0].CLINIC_ID = clinicId;
            users[0].DOCTOR_ID = doctorId;
            db.SaveChanges();
            return;
        }   
        public void addQueue(string patientid)
        {
            db.ADDQUEUE(patientid);
        }
    }
}