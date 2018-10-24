using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Data;
namespace SincciKC.websystem.kwgl
{
    public partial class kc_print : BPage
    {
        
        /// <summary>
        /// 考点代码控制类
        /// </summary>
        BLL_zk_kd BLL_kd = new BLL_zk_kd();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定PageSize数据
              
                if (Request.QueryString["kddm"] != null)
                {
                    BindGv();
                }
              
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGv()
        {
          
          
            string strWhere =  createWhere();
            DataTable tab = BLL_kd.Select_kc(strWhere);
            this.repDisplay.DataSource = tab;
            this.repDisplay.DataBind();

         
        }

 
        /// <summary>
        /// 创建查询条件。
        /// </summary>
        /// <returns></returns>
        private string createWhere()
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            string whereStr = "kddm='" + Request.QueryString["kddm"].ToString() + "' and ";
            //管理部门权限
            string where = "";
            switch (UserType)
            {
                //系统管理员
                case 1:
                    where = " 1=1 ";
                    break;
                //市招生办
                case 2:
                    where = " 1=1 ";
                    break;
                //区招生办
                case 3:
                    where = " xqdm='" + Department + "' ";
                    break;
                //学校用户 
                case 4:
                    where = " 1<>1";
                    break;
                //班级用户 
                case 5:
                    where = " 1<>1";
                    break;
                default:
                    where = " 1<>1";
                    break;  
            }
            if (whereStr.Length > 0)
            {
                whereStr = whereStr + where;
            }
            else
            {
                whereStr = where;
            }

            return whereStr;
        }
       
 
     


       
        /// <summary>
        /// 毕业中学打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
           if (Request.Form["CheckBox1"] != null)
            {

                string str = "";
                if (Request.Form["CheckBox1"] != null)
                {
                    str = Request.Form["CheckBox1"].ToString();
                }
             
                if (str != "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >window.parent.addTab2('照片对照表打印', '/websystem/kwgl/Print_zp.aspx?type=1&kcdm=" + str + "');</script>");
         
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>"); 
                }       
           }
           else
           {
               Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>");
           }
        }
        /// <summary>
        /// 签到表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnqdb_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {

                string str = "";
                if (Request.Form["CheckBox1"] != null)
                {
                    str = Request.Form["CheckBox1"].ToString();
                }

                if (str != "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >window.parent.addTab2('签到表打印', '/websystem/kwgl/Print_zp.aspx?type=2&kcdm=" + str + "');</script>");

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>");
            }
        }
        /// <summary>
        /// 门贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnment_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {

                string str = "";
                if (Request.Form["CheckBox1"] != null)
                {
                    str = Request.Form["CheckBox1"].ToString();
                }

                if (str != "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >window.parent.addTab2('门贴打印', '/websystem/kwgl/Print_zp.aspx?type=3&kcdm=" + str + "');</script>");

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>");
            }
        }
        /// <summary>
        /// 桌贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnzhuot_Click(object sender, EventArgs e)
        {
            if (Request.Form["CheckBox1"] != null)
            {

                string str = "";
                if (Request.Form["CheckBox1"] != null)
                {
                    str = Request.Form["CheckBox1"].ToString();
                }

                if (str != "")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >window.parent.addTab2('桌贴打印', '/websystem/kwgl/Print_zp.aspx?type=4&kcdm=" + str + "');</script>");

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script >alert('请选择打印条件！');</script>");
            }
        }
       

        
    }
}