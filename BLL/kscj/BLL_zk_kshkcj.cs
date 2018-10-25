using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data.SqlClient;
using System.Data;
using BLL;
using System.IO;
using LinqToExcel;
using LinqToExcel.Query;

namespace BLL
{
  public  class BLL_zk_kshkcj
    {
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
        
      
      /// <summary>
      /// 查询
      /// </summary>
      /// <returns></returns>
        public DataTable zk_kshkcj(string ksh)
        {
            string sql = "select * from zk_kshkcj where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }
        /// <summary>
        /// 根据多个县区代码删除指定数据
        /// </summary>
        /// <param name="xqdms">需要删除的县区代码列表</param>
        /// <returns></returns>
        public bool Deletezk_zhpj(List<string> xqdms)
        {
            string inStr = "";
            foreach (var str in xqdms)
                inStr += "'" + str + "',";
            string sqlCmd = "Delete zk_ksxxgl_zhpj Where ksh In (" + inStr.Substring(0, inStr.Length - 1) + ")";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }
        public DataTable zk_zhpj(string ksh)
        {
            zk_kszhpj info = new zk_kszhpj();
            string sql = " select  a.ksh,b.xm, a.ddpzgmsy,a.cxyssjnl,a.jlhznl,a.smbx,a.xxxgxxnl,a.ydjk,b.xxqr from zk_ksxxgl_zhpj a left join  zk_ksxxgl b  on a.ksh=b.ksh where a.ksh =@ksh ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);
            string error = "";
            bool bReturn = false;
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
      
        }
        /// <summary>
        /// 考生综合评价
        /// </summary> 
        public bool KsZhpj(zk_kszhpj model)
        {
            string sql = " update zk_ksxxgl_zhpj set Ddpzgmsy=@Ddpzgmsy,Jlhznl=@Jlhznl,Xxxgxxnl=@Xxxgxxnl,Ydjk=@Ydjk,Smbx=@Smbx,Cxyssjnl=@Cxyssjnl where ksh=@ksh ";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                new SqlParameter("@ksh", model.Ksh),
                new SqlParameter("@Ddpzgmsy", model.Ddpzgmsy),
                new SqlParameter("@Jlhznl", model.Jlhznl),
                new SqlParameter("@Xxxgxxnl", model.Xxxgxxnl),
                new SqlParameter("@Ydjk", model.Ydjk),
                new SqlParameter("@Smbx", model.Smbx),
               new SqlParameter("@Cxyssjnl", model.Cxyssjnl)
              
            };
            string error = "";
            bool bReturn = false;
            int i = _dbHelper.ExecuteNonQuery(sql, lisP, ref   error, ref   bReturn);
            if (i == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
      /// <summary>
      /// 按报名号查询连表View_ksxxgl
      /// </summary>
      /// <param name="ksh"></param>
      /// <returns></returns>
        public DataTable Select_zk_kshkcj(string ksh)
        {
            Model_zk_kshkcj info = new Model_zk_kshkcj();
            string sql = "select a.xxqr, a.ksh,ISNULL( b.Dldj,'') as Dldj,ISNULL(b.Swdj,'') as Swdj from zk_kshkcj b  left join zk_ksxxgl a on a.ksh=b.ksh where b.ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
        }
      /// <summary>
      /// 新增
      /// </summary>
      /// <param name="item"></param>
      /// <returns></returns>
        public bool Insert_zk_kshkcj(Model_zk_kshkcj item)
        {
            string sql = "insert into zk_kshkcj(ksh,Dldj,Swdj) values(@ksh,@Dldj,@Swdj)";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",item.Ksh),
 			 new SqlParameter("@Dldj",item.Dldj),
 			 new SqlParameter("@Swdj",item.Swdj)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
      /// <summary>
      /// 自定义修改
      /// </summary>
      /// <param name="set"></param>
      /// <param name="where"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public bool update_zk_kshkcj(string set, string where)
        {
            string sql = "update  zk_kshkcj set " + set + " where " + where;
            _dbHelper.ExecuteNonQuery(sql, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
      /// <summary>
      /// 根据报名号修改
      /// </summary>
      /// <param name="item"></param>
      /// <returns></returns>
        public bool update_zk_kshkcj(Model_zk_kshkcj item)
        {
            string sql = "update  zk_kshkcj set Dldj=@Dldj,Swdj=@Swdj where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@Dldj",item.Dldj),
 			 new SqlParameter("@Swdj",item.Swdj),
 			 new SqlParameter("@ksh",item.Ksh)
			};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }
      /// <summary>
      /// 根据报名号删除
      /// </summary>
      /// <param name="ksh"></param>
      /// <param name="error"></param>
      /// <param name="bReturn"></param>
      /// <returns></returns>
        public bool delete_zk_kshkcj(string ksh)
        {
            string sql = "delete  zk_kshkcj where ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh)};
            _dbHelper.ExecuteNonQuery(sql, lisP, ref error, ref bReturn);
            if (bReturn) return true;
            else return false;
        }

        /// <summary>
        /// 根据报名号多个删除
        /// </summary>
        /// <param name="xqdms">报名号</param>
        /// <returns></returns>
        public bool DeleteDatakshkcj(List<string> ksh)
        {
            string inStr = "";

            foreach (var str in ksh)
            {
                    inStr += "'" + str + "',";

            }
         
            string sqlCmd = "Delete zk_kshkcj Where ksh In (" + inStr.Substring(0, inStr.Length - 1) + ") ";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }


        private bool checzhjp(string value)
        {
            switch (value)
            {
                case "A":
                    return true;
                case "B":
                    return true;
                case "C":
                    return true;
                case "D":
                    return true;
                default:
                    return false;
            }
        }
        #region Excel数据导入到SqlServer数据库方法
        /// <summary>
        /// 判断报名号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string isKsh(string str)
        {
            int UserType = SincciLogin.Sessionstu().UserType;
            string Department = SincciLogin.Sessionstu().U_department;
            if (new BLL_zk_kcdm().Select_zk_kcdm().Select(" kcdm='" + str.Substring(0, 2) + "'").Length == 0)
            {
                return "报名号【" + str + "】的考次信息,尚未定义.\r\n";
            }

            else if (new BLL_zk_xxdm().Select_zk_xxdm().Select(" xxdm='" + str.Substring(2, 6) + "'").Length == 0)
            {
                return "报名号【" + str + "】的学校信息,尚未定义.\r\n";
            }
            else
            {
                switch (UserType)
                {
                    //系统管理员
                    case 1:
                        return "";
                    //市招生办
                    case 2:
                        return "";
                    //区招生办
                    case 3:
                        if (Department == str.Trim().Substring(2, 4))
                        {
                            return "";
                        }
                        else
                        {
                            return "导入的考生不属于您所属县区.";
                        }

                    //学校用户 
                    case 4:
                        if (Department == str.Trim().Substring(2, 6))
                        {
                            return "";
                        }
                        else
                        {
                            return "导入的考生不属于您所属学校.";
                        }
                    default:
                        return "*";
                }

            }
        }
        /// <summary>
        /// Excel数据导入到SqlServer数据库方法
        /// </summary>
        /// <param name="excelFilePath">excel文件存放路径</param>
        /// <param name="importSqlCmd">需要用于导入数据的sql语句</param>
        /// <param name="importParams">用于导入数据的sql参数列表</param>
        /// <param name="isZL">是否增量导入</param>
        /// <returns></returns>
        public string ImportExcelData(string excelFilePath)
        {
            try
            {

          
            if (!File.Exists(excelFilePath))
                return "导入失败，原因是：输入的文件路径不正确或指定的文件不存在。";
            ExcelQueryFactory excelFile = new ExcelQueryFactory(excelFilePath);
            ExcelQueryable<Row> excel = excelFile.Worksheet();
            SqlDbHelper_1 dbHelp = new SqlDbHelper_1();
            StringBuilder resultMsg = new StringBuilder();

            string sqlCmd = "update  zk_kshkcj set Dldj=@Dldj,Swdj=@Swdj where ksh=@ksh ";
            string checkSql = "insert into zk_kshkcj(ksh,Dldj,Swdj) values(@ksh,@Dldj,@Swdj)";
            bool bReturn = true;

            int i = 0;
            int lost = 0;
            int finish = 0;

            foreach (var element in excel)
            {
                if (element.ColumnNames.Count() != 3)
                    return "导入失败，原因是：输入的文件路径格式不对应，目标格式为3列，您输入的是：" + element.ColumnNames.Count() + "列的格式。";

                string errMsg = "";
                string baseStr = "系统时间：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒") + "。";

                //空数据则跳过
                if (string.IsNullOrEmpty(element[0].ToString().Trim()))
                    continue;

                if (element[0].ToString().Trim().Length != 12)
                {
                    resultMsg.Append("第" + (i + 1) + "行错误，原因：报名号【" + element[0].ToString() + "】不是12位。\r\n");
                    lost++;
                    i++;
                    continue;
                }
                else
                {
                    string strc = isKsh(element[0].ToString());
                    if (strc != "")
                    {
                        resultMsg.Append("第" + (i + 1) + "行错误，原因：" + strc + "\r\n");
                        lost++;
                        i++;
                        continue;
                    }
                }
                if (element[1].ToString().Trim().Length == 0 || !checzhjp(element[1].ToString().Trim().ToUpper()))
                {
                    resultMsg.Append("第" + (i + 1) + "行错误，原因：生物【" + element[1].ToString() + "】有误。\r\n");
                    lost++;
                    i++;
                    continue;
                }
                if (element[2].ToString().Trim().Length == 0 || !checzhjp(element[2].ToString().Trim().ToUpper()))
                {
                    resultMsg.Append("第" + (i + 1) + "行错误，原因：地理【" + element[2].ToString() + "】有误。\r\n");
                    lost++;
                    i++;
                    continue;
                }

                //修改
                if (zk_kshkcj(element[0].ToString().Trim()).Rows.Count > 0)
                {
                    List<SqlParameter> dataParams = new List<SqlParameter> {
                                              new SqlParameter("@ksh",element[0].ToString().Trim()),
                                                new SqlParameter("@Swdj",element[1].ToString().Trim()),
                                                new SqlParameter("@Dldj",element[2].ToString().Trim())
                                                         };
                    _dbHelper.ExecuteNonQuery(sqlCmd, dataParams, ref   errMsg, ref   bReturn);
                }
                else
                {//新增
                    List<SqlParameter> dataParams = new List<SqlParameter> {
                                              new SqlParameter("@ksh",element[0].ToString().Trim()),
                                                new SqlParameter("@Swdj",element[1].ToString().Trim()),
                                                new SqlParameter("@Dldj",element[2].ToString().Trim())
                                                         };
                    _dbHelper.ExecuteNonQuery(checkSql, dataParams, ref   errMsg, ref   bReturn);
                }
                  
                if (!string.IsNullOrEmpty(errMsg))
                {
                    resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入时发生错误，原因是：\r\n");
                    resultMsg.Append("      " + errMsg + "\r\n");

                    lost++;
                }
                else
                {
                    resultMsg.Append(baseStr + "第" + (i + 1) + "行数据导入成功。\r\n");
                    finish++;
                }

                 i++;
            }

            resultMsg.Append("共处理:" + i + "数据导入完毕。共成功导入：" + finish + "条数据。导入失败：" + lost + "条数据。\r\n");

            File.Delete(excelFilePath);

            return resultMsg.ToString();
            }
            catch (Exception)
            {

                return "数据有误!";
                  
            }
        }

        #endregion
        #region "导出数据"
        /// <summary>
        /// 导出数据excel
        /// </summary> 
        public DataSet ExportEXCELKsh(string where)
        {

            //string sql = " select a.ksh as '报名号', b.xm as '姓名',a.Swdj as '生物',a.Dldj as '地理' from zk_kshkcj as a,zk_ksxxgl as b where a.ksh=b.ksh and  " + where + " ";
            string sql = " select b.ksh as '报名号', b.xm as '姓名',b.bjdm as '班级',ISNULL( a.Swdj,'D') as '生物',ISNULL(a.Dldj,'D') as '地理' from zk_ksxxgl as b left join zk_kshkcj as a on b.ksh=a.ksh  where " + where;
            return _dbHelper.selectDataSet(sql, ref error, ref bReturn);
        }

        #endregion
    }
}
