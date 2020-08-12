using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using schema.Dao;
using System.Web.Script.Serialization;
using schema.Utills;

namespace schema.Service
{
    public class AlgorithmService
    {
        AlgorithmDao adao = new AlgorithmDao();
        private PatientDao patientDao = new PatientDao();
        public string getDeptName(string IP)
        {
            //通过IP初始化诊室信息
            decimal clinicId = adao.getClinicId(IP);
            decimal doctorId = 123;
            //通过诊室id 初始化 科室id，科室姓名
            string deptCode = adao.getDeptCode(clinicId);
            string deptName = adao.getDeptName(deptCode);
            return deptName;
        }
        public string getNxtDeptInfo(string patientId,string deptName)
        {
            // 对当前病人的检查完成时间 和 状态 赋值 1(就诊) -> 2(完成)
            string deptCode = adao.getDeptCode(deptName);
            adao.updateCompleteStatus(patientId, deptCode);

            //返回当前病人下一站要去的地方
            return adao.GetNxtDeptInfo(patientId);
        }
        /// <summary>
        /// 意外情况发成 回滚正在检查的病人情况
        /// </summary>
        /// <param name="deptName"></param>
        /// <param name="clinicID"></param>
        public void rollback(string deptName, decimal clinicID)
        {
            string deptCode = adao.getDeptCode(deptName);
            string patientId = patientDao.getId(deptCode, clinicID, 1);
            adao.updatePatientStatus(patientId, deptCode, null, 0, null, null);
        }

        public string getFirstUserInfo(string deptName,decimal clinicId,decimal doctorId)
        {

            //获取科室下第一个等待人的信息 status = 0
            string deptCode = adao.getDeptCode(deptName);
            string UserInfo = adao.GetFirstUserInfo(deptCode);
            

            //获取PatientId
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, string> dic = jss.Deserialize<Dictionary<string, string>>(UserInfo);
            string patientId = dic["patient_id"];

            

            //更新病人Call_time Clinic_id,Doctor_id status (0（等待） -> 1（就诊）)
            adao.updatePatientStatus(patientId, deptCode, System.DateTime.Now, 1, clinicId, doctorId);

            //将这个人加入叫号队列
            //SynthesisUtil.GetVoiceURL(patientId, deptCode, clinicId);

            return UserInfo;
        }
        public void OverNumber(string patientId,string deptName)
        {
            string deptCode = adao.getDeptCode(deptName);
            //过号
            adao.overNumber(patientId, deptCode);
        }
        public int getDeptNumber(string deptName)
        {
            string deptCode = adao.getDeptCode(deptName);
            return adao.GetDeptNum(deptCode);
        }
        public void updatePatientStatus(string patientId,string deptName)
        {
            string deptCode = adao.getDeptCode(deptName);
            //更新病人Call_time Clinic_id,Doctor_id status (1（就诊) -> 0（等待）)
            adao.updatePatientStatus(patientId, deptCode, null, 0, null, null);
        }
    }
}