using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using DAL; 
using Model;
using System.Configuration;
using System.IO;

namespace SincciKC.websystem.bmsz
{
    public partial class PhotoAJAXPage : BPage
    {
       string department = "";
       int userType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string opType = Request["OpType"] ?? "";
            department =  SincciLogin.Sessionstu().U_department;
            userType = SincciLogin.Sessionstu().UserType;
            switch (opType)
            {
                case "GetData":
                    GetData();
                    break;
                case "GetCityAreaData":
                    GetCityAreaData();
                    break;
                case "GetSchoolData":
                    GetSchoolData();
                    break;
                case "GetSchoolClassData":
                    GetSchoolClassData();
                    break;
                case "GetSwSh":
                    GetSwSh();
                    break;
                case "GetUserImagePath":
                    GetUserImagePath();
                    break;
                case "UpLoadImage":
                    UpLoadImage();
                    break;
            }
        }

        /// <summary>
        /// 保存上传的图片
        /// </summary>
        private void UpLoadImage()
        {
            string fileName = Request.Form["FileName"];
            string ksh = Request.Form["ksh"];
            string result = "保存成功";
            string mainPath = "";
            HttpPostedFile file = Request.Files["Filedata"];
            
            if (file != null)
            {
                try
                {

                    string xqdm = ksh.Substring(2, 4);
                    string bmddm = ksh.Substring(2, 6);
                    string FolderPath = Server.MapPath("//13//" + xqdm + "//");
                    mainPath = Server.MapPath("//13//" + xqdm + "//" + ksh + ".jpg");
                    if (!Directory.Exists(FolderPath))
                    {
                        Directory.CreateDirectory(FolderPath);  //不存在就创建
                    }
                   // config.FolderCreate(Server.MapPath(FolderPath ));
                    file.SaveAs(mainPath);
                    if (new BLL_zk_ksxxgl().KsPhoto(ksh))
                    {

                    }
                }
                catch (Exception ex)
                {
                    result = "保存图片时发生错误，原因是：" + ex.Message;
                }
            }

            Response.Write(result + "|" + mainPath);
            Response.End();
        }

        /// <summary>
        /// 获取默认照相的取值范围
        /// </summary>
        private void GetSwSh()
        {
            string result = ConfigurationManager.AppSettings["SwSh"].ToString();
            result += "," + "http://" + HttpContext.Current.Request.Url.Host + systemparam.picPath;

            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 根据指定考生信息获取该考生的照片
        /// </summary>
        private void GetUserImagePath()
        {
            string ksh = Request.Form["ksh"];
            string xqdm = Request.Form["xqdm"];
            string bmddm = Request.Form["bmddm"];
            string pic = Request.Form["pic"];
            string path = "../../images/nopic.gif";
            if (pic == "1")
                path = Server.MapPath("//13//" + xqdm + "//" + ksh + ".jpg");

            Response.Write(path);
            Response.End();
        }

        private void GetData()
        {
            BLL_xxgl bllxxgl = new BLL_xxgl();
            string whereStr = strwhere();
            int pageSize = int.Parse(Request.Form["PageIndex"]);
            int rows = 20;//int.Parse(Request.Form["rows"]);
            int RecordCount = 0;

            DataTable result = bllxxgl.ExecuteProcPic(whereStr, rows, pageSize, ref RecordCount);
            string str = RecordCount + "|" + SqlDbHelper_1.CreateJsonParametersForFlex(result);
            Response.Write(str);
            Response.End();
        }

        /// <summary>
        /// 条件
        /// </summary>
        private string strwhere()
        {
            string str = "";
            string shiqu = Request.Form["CityArea"];
            string xuexiao = Request.Form["School"];
            string banji = Request.Form["SchoolClass"];
            string keyWord = Request.Form["KeyWord"];

            if (shiqu.Length > 0)
            {
                str = str + " bmdxqdm='" + shiqu + "' and ";
            }
            if (xuexiao.Length > 0)
            {
                str = str + " bmddm='" + xuexiao + "' and ";
            }
            if (banji.Length > 0)
            {
                str = str + " bjdm='" + banji + "' and ";
            }
            if (keyWord.Length > 0)
            {
                str = str + " (ksh='" + keyWord + "' or xm like '%" + keyWord + "%') and ";
            }

            //管理部门权限
            string where = "";
            switch (userType)
            {
                case 1:
                    where = " 1 =1 ";
                    break;
                case 2:
                    where = " 1 =1 ";
                    break;
                //区招生办
                case 3:
                    where = " bmdxqdm = '" + department + "'  ";
                    break;
                //学校用户 
                case 4:
                    where = " bmddm = '" + department + "' ";
                    break;
                //班级用户 
                case 5:
                    where = " bjdm = '" + department.Substring(6) + "' and bmddm='" + department.Substring(0, 6) + "' ";
                    break;
                default:
                    where = " 1<>1";
                    break;
            }
            if (str.Length > 0)
            {
                str = str + where;
            }
            else
            {
                str = where;
            }


            return str;
        }

        public void GetCityAreaData()
        {
            /// <summary>
            /// 县区代码控制类
            /// </summary>
            BLL_zk_xqdm bllxqdm = new BLL_zk_xqdm();
            DataTable dtXqdm = bllxqdm.SelectXqdm(department, userType);
            DataRow dr = dtXqdm.NewRow();
            dr["xqdm"] = "";
            dr["xqmc"] = "-请选择-";

            dtXqdm.Rows.InsertAt(dr,0);
            Response.Write(SqlDbHelper_1.CreateJsonParametersForFlex(dtXqdm));
            Response.End();
        }

        public void GetSchoolData()
        {
            /// <summary>
            /// 学校代码控制类
            /// </summary>
            BLL_zk_xxdm bllxxdm = new BLL_zk_xxdm();
            string cityAreaCode = Request.Form["CityAreaCode"];
            DataTable tmDataTable = bllxxdm.Select_zk_xxdmXQ(cityAreaCode, department, userType);
            DataRow dr = tmDataTable.NewRow();
            dr["xxdm"] = "";
            dr["xxmc"] = "-请选择-";
            tmDataTable.Rows.InsertAt(dr, 0);
            Response.Write(SqlDbHelper_1.CreateJsonParametersForFlex(tmDataTable));
            Response.End();
        }

        public void GetSchoolClassData()
        {
            /// <summary>
            /// 班级代码控制类
            /// </summary>
            BLL_zk_bjdm bllbjdm = new BLL_zk_bjdm();

            DataTable tmDataTable = bllbjdm.Select_zk_bjdm(Request.Form["SchoolCode"], department, userType);
            DataRow dr = tmDataTable.NewRow();
            dr["bjdm"] = "";
            dr["bjmc"] = "-请选择-";
            tmDataTable.Rows.InsertAt(dr, 0);
            Response.Write(SqlDbHelper_1.CreateJsonParametersForFlex(tmDataTable));
            Response.End();
        }

        public void GetImageURL()
        {
            Model_zk_ksxxgl info = new Model_zk_ksxxgl();
            info = new BLL_zk_ksxxgl().ViewDisp(Request.Form["ksh"]);
        }
    }
}