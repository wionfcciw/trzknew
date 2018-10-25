using DAL;
using Newtonsoft.Json;
using SincciKC.SsoLogin.SSOModel.SchoolClassJsonEntityPack;
using SincciKC.SsoLogin.SSOModel.SchoolGradeJsonEntityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SincciKC.SsoLogin
{
    public partial class AutoAddStudent : System.Web.UI.Page
    {
        private SqlDbHelper_1 helper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //只取学校的schoolid，从而获取年级和班级信息。
            string schoolsql = "select organization_id,sys_name from sys_SSOUserContrast where type=2";
            DataTable dt= helper.selectTab(schoolsql, ref error, ref bReturn);
            foreach(DataRow dr in dt.Rows)
            {
                string bmddm = dr["sys_name"].ToString();
                string gradeHtml = "http://openapi.tredu.gov.cn/basedataApi/grade/getGradeBySchoolId/" + dr["organization_id"].ToString();
                HttpWebRequest gradeRequest = (HttpWebRequest)WebRequest.Create(gradeHtml);
                gradeRequest.Method = "GET";
                //gradeRequest.Headers["Access-Token"] = Session["token"].ToString();
                gradeRequest.Headers["Client-Id"] = "1d98bbaa-0507-49f4-a3dc-ddd51f479d86";
                gradeRequest.Headers["Client-Secret"] = "f60fb940-d7f2-459f-ab08-dc110f9502a3";
                HttpWebResponse gradeResponse = (HttpWebResponse)gradeRequest.GetResponse();
                StreamReader gradesr = new StreamReader(gradeResponse.GetResponseStream());
                string gradeJson = gradesr.ReadToEnd();
                SchoolGradeJsonEntity sgje = JsonConvert.DeserializeObject<SchoolGradeJsonEntity>(gradeJson);
                //年级分成两种，所以此处用lambda表达式来取。
                string gradeLevel = sgje.pageInfo.list.Where(o => o.classGradeLevel == "20"||o.classGradeLevel=="30").ToList()[0].classGradeLevel;
                string classId = "";
                string gradeContext="{\"classId\":\""+classId+"\"}";
                byte[] bytes=Encoding.UTF8.GetBytes(gradeContext);
                string getstudentByGradeHtml = "http://openapi.tredu.gov.cn/basedataApi/student/getStudentsByCondition";
                HttpWebRequest studentByGradeRequest = (HttpWebRequest)WebRequest.Create(getstudentByGradeHtml);
                studentByGradeRequest.Method = "POST";
                //studentByGradeRequest.Headers["Access-Token"] = Session["token"].ToString();
                studentByGradeRequest.Headers["Client-Id"] = "1d98bbaa-0507-49f4-a3dc-ddd51f479d86";
                studentByGradeRequest.Headers["Client-Secret"] = "f60fb940-d7f2-459f-ab08-dc110f9502a3";
                studentByGradeRequest.ContentLength = bytes.Length;
                studentByGradeRequest.ContentType = "application/json";
                Stream sm = studentByGradeRequest.GetRequestStream();
                sm.Write(bytes, 0, bytes.Length);
                studentByGradeRequest.Timeout = 90000;
                HttpWebResponse studentByGradeResponse = (HttpWebResponse)studentByGradeRequest.GetResponse();
                StreamReader studentByGradeSr = new StreamReader(studentByGradeResponse.GetResponseStream());
                string studentByGradeJson = studentByGradeSr.ReadToEnd();

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string sql = "select sso_xxdm,sys_bjdm from Sys_SSOSchoolAndClass where sso_xxdm is not null";
            DataTable dt = helper.selectTab(sql, ref error, ref bReturn);
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                //先按照初三的去取班级信息
                string classHtml = "http://openapi.tredu.gov.cn/basedataApi/class/getClassesBySchoolIdAndGradeId/" + dr["sso_xxdm"] + "/20";
                string ClientId= "1d98bbaa-0507-49f4-a3dc-ddd51f479d86";
                string ClientSecret = "f60fb940-d7f2-459f-ab08-dc110f9502a3";
                string method = "POST";
                SchoolClassJsonEntity scje =GetClassEntity(classHtml,ClientId,ClientSecret,method);
                if(scje.pageInfo==null)
                {
                    //说明是九年一贯制学校
                    classHtml = "http://openapi.tredu.gov.cn/basedataApi/class/getClassesBySchoolIdAndGradeId/" + dr["sso_xxdm"] + "/30";
                    scje = GetClassEntity(classHtml, ClientId, ClientSecret, method);
                }
                //取入学年份为三年前或九年前（九年一贯）的学生，此处可根据实际情况更改。
                List<SincciKC.SsoLogin.SSOModel.SchoolClassJsonEntityPack.ListItem> ClassList = scje.pageInfo.list.Where(o => o.classBuildTime.ToString().Substring(0, 4) == Convert.ToString(DateTime.Now.Year - 3) || o.classBuildTime.ToString().Substring(0, 4) == Convert.ToString(DateTime.Now.Year - 9)).ToList();
                string bjdm = "";
                string classid = "";
                string updatesql = "";
                if (dr["sys_bjdm"].ToString().Length < 2)
                {
                    bjdm = "0" + dr["sys_bjdm"];
                }
                else
                {
                    bjdm = dr["sys_bjdm"].ToString();
                }
                List<SincciKC.SsoLogin.SSOModel.SchoolClassJsonEntityPack.ListItem> DestClassList = ClassList.Where(o => o.classCode.Substring(o.classCode.Length - 2, 2) == bjdm).ToList();
                if (DestClassList.Count>0)
                {
                    SincciKC.SsoLogin.SSOModel.SchoolClassJsonEntityPack.ListItem item = DestClassList[0];

                    if (item != null)
                    {
                        classid = item.classId;
                        //更新到相应的数据库表里面
                        updatesql = "update Sys_SSOSchoolAndClass set sso_bjdm='" + classid + "' where sso_xxdm='" + dr["sso_xxdm"] + "' and sys_bjdm='" + dr["sys_bjdm"] + "'";
                        int j = helper.ExecuteNonQuery(updatesql, ref error, ref bReturn);
                        i = i + j;
                    }
                }
            }
            if(i>0)
            {
                Response.Write("<script>alert('操作成功！共同步"+i+"条数据')");
            }
        }

        private SchoolClassJsonEntity GetClassEntity(string classHtml,string ClientId,string ClientSecret,string method)
        {
            HttpWebRequest classRequest = (HttpWebRequest)WebRequest.Create(classHtml);
            classRequest.Method = method;
            classRequest.Headers["Client-Id"] = ClientId;
            classRequest.Headers["Client-Secret"] = ClientSecret;
            HttpWebResponse classResponse = (HttpWebResponse)classRequest.GetResponse();
            StreamReader classsr = new StreamReader(classResponse.GetResponseStream());
            string classJson = classsr.ReadToEnd();
            SchoolClassJsonEntity scje = JsonConvert.DeserializeObject<SchoolClassJsonEntity>(classJson);
            return scje;
        }
    }
}