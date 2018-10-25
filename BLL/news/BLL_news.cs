using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using BLL;
using Model;
 
namespace BLL 
{
  public  class BLL_news
    {
        /// <summary>
        /// 显示文章
        /// </summary>
        /// <param name="ClassID">栏目ID</param>
        /// <param name="Number">文章条数</param>
        /// <param name="TiltLength">标题长度</param>
        /// <param name="TiltLength">h2标识</param>
        /// <param name="TiltLength">page页码</param>
        /// <param name="TiltLength">是否显示时间</param>
        /// <returns></returns>
        public   StringBuilder ShowArticle(int ClassID, int Number, int TiltLength )
        {
            int RecordCount = 0;
            int page = 1;
            DataTable list = ArrayListArticle(ClassID, Number, page, ref RecordCount);
            return ShowInfo(list, TiltLength );
        }

        private StringBuilder ShowInfo(DataTable dt, int TiltLength)
        {
            StringBuilder sb = new StringBuilder();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    sb.Append("<dt>");
                    if (dt.Rows[i]["Urls"].ToString().Length > 0 && dt.Rows[i]["Urls"].ToString() != null)
                    {
                        sb.Append("     <a href=\"" + dt.Rows[i]["Urls"].ToString() + "\" target=\"_blank\" >");
                    }
                    else
                    {
                        sb.Append("     <a href=\"/webUI/news/NewsInfo.aspx?NewsID=" + dt.Rows[i]["N_NewID"].ToString() + "\"  target=\"_blank\" >");
                    }
                    sb.Append("     " + config.SetSubString(dt.Rows[i]["Title"].ToString(), TiltLength) + "");
                    sb.Append("     </a>");
                    sb.Append("</dt>");

                }
            }
            return sb;
        }


        /// <summary>
        /// 显示文章
        /// </summary>
        /// <param name="ClassID">栏目ID</param>
        /// <param name="Number">文章条数</param>
        /// <param name="TiltLength">标题长度</param>
        /// <param name="TiltLength">h2标识</param>
        /// <param name="TiltLength">page页码</param>
        /// <param name="TiltLength">是否显示时间</param>
        /// <returns></returns>
        public StringBuilder ShowArticleList(int ClassID, int Number, int TiltLength, int h2, int page, int datetime, ref int RecordCount)
        {
            DataTable list = ArrayListArticle(ClassID, Number, page, ref RecordCount);
            return ShowInfo(list, TiltLength );
        }

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="ClassID">栏目ID</param>
        /// <param name="Number">文章条数</param>
        /// <returns></returns>
        public DataTable ArrayListArticle(int ClassID, int Number, int page, ref int RecordCount)
        {

            string where =string.Format(" Show={0} and MarkPass={0} and CategoryID='{1}'  ", (int)Tag.Open, ClassID);
            //数据库表名
            string tabName = "PE_NewsList";
            //要查询的字段
            string reField = " NewsID, N_NewID,Title,CategoryID,PublishTime,Remark,Urls ";
            //排序字段
            string orderStr = "  MarkTop desc,NewsID  ";
            //排序标识（0、升序；1、降序）
            int orderType = 1;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = new DAL.SqlDbHelper_1().ExecuteProc(tabName, reField, orderStr, "", where, Number, page, orderType, 0, ref dec, ref RecordCount, ref bFlag);
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
        /// 查找数据  有包含内容这个字段
        /// </summary>
        /// <param name="ClassID">栏目ID</param>
        /// <param name="Number">文章条数</param>
        /// <returns></returns>
        public DataTable DataTableArticle(int ClassID, int Number, int page, ref int RecordCount)
        {

            string where = string.Format(" Show={0} and MarkPass={0} and CategoryID='{1}'  ", (int)Tag.Open, ClassID);
            //数据库表名
            string tabName = "PE_NewsList";
            //要查询的字段
            string reField = " NewsID, N_NewID,Title,CategoryID,PublishTime,Remark,Urls,content ";
            //排序字段
            string orderStr = "  MarkTop desc,NewsID  ";
            //排序标识（0、升序；1、降序）
            int orderType = 1;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = new DAL.SqlDbHelper_1().ExecuteProc(tabName, reField, orderStr, "", where, Number, page, orderType, 0, ref dec, ref RecordCount, ref bFlag);
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
        /// 查找数据后台管理用
        /// </summary>
        /// <param name="ClassID">栏目ID</param>
        /// <param name="Number">文章条数</param>
        /// <returns></returns>
        public DataTable DataTableArticle(string strwhere, int Number, int page, ref int RecordCount)
        {

            string where = strwhere;
            //数据库表名
            string tabName = " PE_NewsList as A inner join PE_NewsCategory as B on  A.CategoryID=B.PCID ";
            //要查询的字段
            string reField = " NewsID,Title,CategoryID,PublishTime,Remark,Urls,Editor,MarkPass,Show,MarkTop,MarkImp,B.CategoryName ";
            //排序字段
            string orderStr = "  NewsID    ";
            //排序标识（0、升序；1、降序）
            int orderType = 1;
            //执行成功与失败的标识（true、表示执行成功；false、表示执行失败）
            bool bFlag = false;
            decimal dec = 0;
            DataSet dst = new DAL.SqlDbHelper_1().ExecuteProc(tabName, reField, orderStr, "", where, Number, page, orderType, 0, ref dec, ref RecordCount, ref bFlag);
            if (bFlag)
            {
                if (dst.Tables.Count > 0)
                {
                    return dst.Tables[0];
                }
            }
            return null;

        }
       

    }
}
