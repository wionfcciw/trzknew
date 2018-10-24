using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Web;
 
using System.Text;
using System.Globalization;
using System.Collections;


namespace BLL
{
    /// <summary>
    /// DbfHelper 导出助手类。
    /// </summary>
    public class DbfHelper
    {
        string _templetFile;//DBF模板文件
        string _fileName;//目标临时文件
        string _serverpath;//服务器目录路径
        string _fields;
        string _fileprefix;
        DataTable _dataSource;

        /// <summary>
        /// 取服务器目录路径
        /// </summary>
        public DbfHelper()
        {
            _serverpath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath) ;
        }
        /// <summary>
        /// DBF模板文件
        /// </summary>
        public string TempletFile
        { 
            set { _templetFile = value; }
            get { return _templetFile; }
        }
        /// <summary>
        /// 目标临时文件
        /// </summary>
        public string FilePrefix
        { 
            set { _fileprefix = value; }
            get { return _fileprefix; }
        }
        /// <summary>
        /// 字段
        /// </summary>
        public string Fields
        { 
            set { _fields = value; }
            get { return _fields; }
        }
        /// <summary>
        /// 数据
        /// </summary>
        public DataTable DataSource
        { 
            set { _dataSource = value; }
            get { return _dataSource; }
        }

        public void Export()
        {//这段好理解，跟导出DOC、XLS文件一样。
            HttpResponse response = HttpContext.Current.Response;

            CreateData();
            response.Clear(); 
            //response.Charset = "GB2312";
            //response.ContentEncoding = Encoding.GetEncoding("GB2312");
            //response.ContentType = "APPLICATION/OCTET-STREAM";
            //response.AppendHeader("Content-Disposition", "attachment;filename=" +
            // HttpUtility.UrlEncode(_fileName));
            //response.WriteFile(_fileName);
            //response.Flush();
            //File.Delete(_fileName);
            //response.End();
            response.Charset = "GB2312";
            response.ContentEncoding = Encoding.GetEncoding("GB2312");
            response.ContentType = "APPLICATION/OCTET-STREAM";
            response.AppendHeader("Content-Disposition", "attachment;filename=" +
             HttpUtility.UrlEncode(_fileName));
            response.WriteFile(_fileName);
            response.Flush();
        //    File.Delete(_fileName);
            response.End();

        }
        private void CreateData()
        {//创建表

            string tempfile = GetRandomFileName();//取随机文件名
            _fileName = _serverpath + @"Temp/" + tempfile + ".dbf";
            File.Copy(_serverpath + _templetFile, _fileName, true);

            string strConn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _serverpath + @"Temp/" + ";Extended Properties=dBASE 5.0";
            string sql = "";
            if (_fields != null && _fields != string.Empty)//这不知道什么意思
                sql = "Select  " + _fields + "  From  [" + tempfile + "]";
            else
                sql = "Select  *  From  [" + tempfile + "]";
            OleDbDataAdapter adpt = new OleDbDataAdapter(sql, strConn);
            OleDbCommandBuilder bd = new OleDbCommandBuilder(adpt);//这不知道什么意思
            bd.QuotePrefix = "[";//这不知道什么意思
            bd.QuoteSuffix = "]";//这不知道什么意思
            DataSet mySet = new DataSet();
            adpt.Fill(mySet, tempfile);
            MoveBatch(_dataSource, mySet.Tables[0]);//批量导出数据,调用函数,前为目标文件，后为源文件（从传入DATATABLE取数据）
            adpt.Update(mySet, tempfile);//这不知道什么意思
            
       //     adpt.Dispose();
            

        }
        /**/
        /// <summary>
        /// 得到一个随意的文件名
        /// </summary>
        /// <returns></returns>
        private string GetRandomFileName()
        {
            Random rnd = new Random((int)(DateTime.Now.Ticks));
            string s = rnd.Next(999).ToString();
            s = FilePrefix + s;
            return s;
        }
        protected virtual void MoveBatch(DataTable src_dt, DataTable dst_dt)//这不知道什么意思
        {
            
            foreach (DataRow dr in src_dt.Rows)
            {
                dst_dt.Rows.Add(dr.ItemArray);
              //  dst_dt.ImportRow(dr);
            }
           

        }

    }
}

