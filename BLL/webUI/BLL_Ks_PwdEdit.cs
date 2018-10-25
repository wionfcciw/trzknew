using System;
using System.Collections.Generic;
using DAL;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model;
namespace BLL 
{
    /// <summary>
    /// 修改考生密码 控制类
    /// </summary>
   public class BLL_Ks_PwdEdit
    {
       /// <summary>
        /// 修改考生密码  0正常 -1 原密码错 -2修改不成功
       /// </summary>
       /// <param name="oldpwd">原密码</param>
       /// <param name="pwd">新密码</param>
       /// <param name="ksh">报名号</param>
       /// <returns>0正常 -1 原密码错 -2修改不成功 -3查找不到考生 </returns>
       public static int Ks_PwdEdit(string oldpwd, string pwd, string ksh)
       {
          
           int flag = 0;
           //List<SqlParameter> lisP = new List<SqlParameter>();

           //SqlParameter lisp_oldpwd = new SqlParameter("@oldpwd", SqlDbType.VarChar, 50);
           //lisp_oldpwd.Value = oldpwd;
           //lisP.Add(lisp_oldpwd);

           //SqlParameter lisp_pwd = new SqlParameter("@pwd", SqlDbType.VarChar, 50);
           //lisp_pwd.Value = pwd;
           //lisP.Add(lisp_pwd);

           //SqlParameter lisp_ksh = new SqlParameter("@ksh", SqlDbType.VarChar, 20);
           //lisp_ksh.Value = ksh;
           //lisP.Add(lisp_ksh);

           //string error = "";
           //bool bReturn = false;
          BLL_zk_ksxxgl ksxxgl=  new BLL_zk_ksxxgl();

           Model_zk_ksxxgl ksinfo =ksxxgl.zk_ksxxglDisp(ksh);

           //string sql = "select count(*)  from zk_ksxxgl where ksh=@ksh and pwd=@oldpwd ";
           //int  i =Convert.ToInt32(_dbHelper.ExecuteScalar(sql, lisP, ref   error, ref   bReturn)) ;
          if (ksinfo.Ksh.Length > 0) 
           {
               if (ksinfo.Pwd == oldpwd)
               {
                   //ClearField.clearSqlParameter(lisP);
                   //lisP.Clear();

                   //sql = "update zk_ksxxgl set pwd=@pwd where ksh=@ksh ";
                   //lisp_ksh.Value = ksh;
                   //lisP.Add(lisp_ksh);
                   //lisp_pwd.Value = pwd;
                   //lisP.Add(lisp_pwd);
                   //i = _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);

                   if (!ksxxgl.zk_ksxxglEditPwd(ksh,pwd))
                       flag = -2;
               }
               else
               {
                   flag = -1;
               }
           }
           else
           {
               flag = -3;
           }
           return flag;
       }
    }
}
