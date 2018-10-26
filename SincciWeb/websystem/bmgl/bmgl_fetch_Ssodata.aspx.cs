using SincciKC.SsoLogin.SSOMethod;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SincciKC.websystem.bmgl
{
    public partial class bmgl_fetch_Ssodata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFetchBaseData_Click(object sender, EventArgs e)
        {
            string[] schoolTypeList = { "初中", "完全中学", "九年一贯制" };
            //参数头
            string parameterSchoolStart = "{\"pagination\":{\"pageNo\":1,\"pageSize\":10000},\"schoolStepName\":\"";
            //参数尾
            string parameterSchoolEnd = "\"}";
            //参数拼凑
            string parameterSchool = parameterSchoolStart + schoolTypeList[0] + parameterSchoolEnd;
            //学校地址
            string getschoolhtml = "http://openapi.tredu.gov.cn/basedataApi/school/getSchoolsByCondition";
            //平台获取数据公共类
            GetDataByPlatform gt = new GetDataByPlatform();
            //初中考生
            string jsonStringJs = gt.GetDataByParameterInPost(getschoolhtml, parameterSchool);
        }
    }
}