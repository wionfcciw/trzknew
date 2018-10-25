using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using LinqToExcel;
using System.IO;
using LinqToExcel.Query;
using System.Data.SqlClient;
using Model;

namespace BLL
{
    public class BLL_zk_kszkcj
    {
        private SqlDbHelper_1 _dbHelper = new SqlDbHelper_1();
        private string error = "";
        private bool bReturn = false;
        /// <summary>
        /// 执行分页存储过程，返回记录总数和当前页的数据。会考成绩 
        /// </summary>
        /// <param name="where">执行的条件</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="totalRecord">返回的记录总数</param>
        public DataTable ExecuteProcHKCJ(string where, int pageSize, int pageIndex, ref int totalRecord)
        {
            //string sql="select  from AppraisalPersonnel";

            //数据库表名
            string tabName = " zk_zkcj as A  left outer join  zk_ksxxgl  as B  on A.ksh=b.ksh ";
            //要查询的字段 A.ksh,A.Dldj ,A.Swdj ,A.xm,B.xxqr
            string reField = " A.*,b.xm,b.sfzh   ";
            //排序字段
            string orderStr = "  A.ksh ";
            //排序标识（0、升序；1、降序）
            int orderType = 0;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = _dbHelper.ExecuteProc(tabName, reField, orderStr, "", where, pageSize, pageIndex, orderType, 0, ref dec, ref totalRecord, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;
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

                string sqlCmd = "update  zk_zkcj set yw=@cj1,sx=@cj2,yy=@cj3,zs=@cj4,wh=@cj5,ty=@cj6,zf=@cj7,zzf=@cj8 where ksh=@ksh ";
                string checkSql = "insert into zk_zkcj(ksh,cj1,cj2,cj3,cj4,cj5,cj6) values(@ksh,@cj1,@cj2,@cj3,@cj4,@cj5,@cj6)";
                bool bReturn = true;

                int i = 0;
                int lost = 0;
                int finish = 0;

                foreach (var element in excel)
                {
                    if (element.ColumnNames.Count() != 9)
                        return "导入失败，原因是：输入的文件路径格式不对应，目标格式为9列，您输入的是：" + element.ColumnNames.Count() + "列的格式。";

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
              
                    //修改
                    if (zk_kshkcj(element[0].ToString().Trim()).Rows.Count > 0)
                    {
                        List<SqlParameter> dataParams = new List<SqlParameter> {
                                              new SqlParameter("@ksh",element[0].ToString().Trim()),
                                                new SqlParameter("@cj1",element[1].ToString().Trim()),
                                                new SqlParameter("@cj2",element[2].ToString().Trim()),
                                                new SqlParameter("@cj3",element[3].ToString().Trim()),
                                                new SqlParameter("@cj4",element[4].ToString().Trim()),
                                                new SqlParameter("@cj5",element[5].ToString().Trim()),
                                                new SqlParameter("@cj6",element[6].ToString().Trim()),
                                                 new SqlParameter("@cj7",element[7].ToString().Trim()),
                                                new SqlParameter("@cj8",element[8].ToString().Trim())
                                           
                                            
                                                         };
                        _dbHelper.ExecuteNonQuery(sqlCmd, dataParams, ref   errMsg, ref   bReturn);
                    }
                    else
                    {//新增
                        List<SqlParameter> dataParams = new List<SqlParameter> {
                                                new SqlParameter("@ksh",element[0].ToString().Trim()),
                                                new SqlParameter("@cj1",element[1].ToString().Trim()),
                                                new SqlParameter("@cj2",element[2].ToString().Trim()),
                                                new SqlParameter("@cj3",element[3].ToString().Trim()),
                                                new SqlParameter("@cj4",element[4].ToString().Trim()),
                                                new SqlParameter("@cj5",element[5].ToString().Trim()),
                                                new SqlParameter("@cj6",element[6].ToString().Trim()),
                                                 new SqlParameter("@cj7",element[4].ToString().Trim()),
                                                new SqlParameter("@cj8",element[5].ToString().Trim())
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
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable zk_kshkcj(string ksh)
        {
            string sql = "select a.*,xm,sfzh,bmdmc,kslbmc,bjdm,zkzh,bklb,lxdh,xbmc from zk_zkcj a left join View_ksxxNew b on a.ksh=b.ksh where a.ksh=@ksh";
            List<SqlParameter> lisP = new List<SqlParameter>(){
 			 new SqlParameter("@ksh",ksh)};
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            return dt;
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
        /// 按报名号查询一位考生(试图查询)，转换为实体类
        /// </summary>
        /// <param name="ksh">报名号</param>
        /// <returns></returns>
        public Model_zk_zkcj ViewDisp(string ksh)
        {
            Model_zk_zkcj info = new Model_zk_zkcj();

            string sql = " select * from zk_zkcj where ksh =@ksh ";
            List<SqlParameter> lisP = new List<SqlParameter>();
            SqlParameter Ksh = new SqlParameter("@ksh", SqlDbType.VarChar);
            Ksh.Value = ksh;
            lisP.Add(Ksh);

            string error = "";
            bool bReturn = false;
            DataTable dt = _dbHelper.selectTab(sql, lisP, ref   error, ref   bReturn);
            if (dt.Rows.Count > 0)
            {
                info = _dbHelper.DT2EntityList<Model_zk_zkcj>(dt)[0];
            }
            return info;
        }

        public DataTable zk_cj(string ksh)
        {
          
            string sql = " select  a.*,b.xm from zk_zkcj a left join  zk_ksxxgl b  on a.ksh=b.ksh where a.ksh =@ksh ";
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
        /// 考生成绩
        /// </summary> 
        public bool KsZhpj(Model_zk_zkcj model)
        {
            string sql = "update  zk_zkcj set yw=@cj1,sx=@cj2,yy=@cj3,zs=@cj4,wh=@cj5,ty=@cj6,zf=@cj7,zzf=@cj8 where ksh=@ksh  ";
            List<SqlParameter> lisP = new List<SqlParameter>() { 
                new SqlParameter("@ksh", model.Ksh),
               
                                                new SqlParameter("@cj1",model.Yw),
                                                new SqlParameter("@cj2",model.Sx),
                                                new SqlParameter("@cj3",model.Yy),
                                                new SqlParameter("@cj4",model.Zs),
                                                new SqlParameter("@cj5",model.Wh),
                                                new SqlParameter("@cj6",model.Ty),
                                              new SqlParameter("@cj5",model.Zf),
                                                new SqlParameter("@cj6",model.Zzf)
                                               
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

            string sqlCmd = "Delete zk_zkcj Where ksh In (" + inStr.Substring(0, inStr.Length - 1) + ") ";
            _dbHelper.ExecuteNonQuery(sqlCmd, ref error, ref bReturn);

            if (bReturn) return true;
            else return false;
        }
    }
}
