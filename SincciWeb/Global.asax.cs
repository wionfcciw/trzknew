
using FluentScheduler;
using SincciKC.AutoTaskTool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SincciWeb
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            JobManager.Initialize(new AutoTask());
            // 在应用程序启动时运行的代码
        }

        void Application_End(object sender, EventArgs e)
        {

        }

        void Application_Error(object sender, EventArgs e)
        {
             
            // 在出现未处理的错误时运行的代码
            //获得最后一个Exception 
            Exception objErr = this.Context.Server.GetLastError();

           // Response.Write(objErr.ToString());
           // Response.End();

            //独占方式，因为文件只能由一个进程写入.   
            System.IO.StreamWriter writer = null;
            try
            {
                lock (this)
                {
                    // 写入日志   
                    string year = DateTime.Now.Year.ToString();
                    string month = DateTime.Now.Month.ToString();
                    string path = string.Empty;
                    string filename = DateTime.Now.Day.ToString() + ".txt";
                    path = Server.MapPath("~/ErrorLog/") + year + "/" + month;
                    //如果目录不存在则创建   
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    System.IO.FileInfo file = new System.IO.FileInfo(path + "/" + filename);


                    
                    //文件不存在就创建,true表示追加   
                    writer = new System.IO.StreamWriter(file.FullName, true);

                    string ip = "用户IP:" + Request.UserHostAddress;                   
                    string errortime = "发生时间:" + System.DateTime.Now.ToString(); ;
                    string erroraddr = "发生异常页: " + Request.Url.ToString();
                    string line = "------------------------------------------------------------";

                                
                    writer.WriteLine(errortime);                    
                    writer.WriteLine(erroraddr);                  
                    writer.WriteLine(ip);                   
                    writer.WriteLine(objErr);
                    writer.WriteLine(line); 
                    

                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();

            }
            this.Context.Server.ClearError();
            Response.Redirect("/error.html");  //转向出错页面。

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }

    }
}




//获得所有未处理的Exception集 
//Exception[] errors = this.Context.AllErrors;


//然后进行一系列处理，写文件日志，写数据库都行 
////string error = string.Empty;
//string errortime = string.Empty;
//string erroraddr = string.Empty;
//string errorinfo = string.Empty;
//string errorsource = string.Empty;
//string errortrace = string.Empty;

//  error += "发生时间:" + System.DateTime.Now.ToString() + "<br>";
//  errortime = "发生时间:" + System.DateTime.Now.ToString();

////  error += "发生异常页: " + Request.Url.ToString() + "<br>";
//  erroraddr = "发生异常页: " + Request.Url.ToString();

//  error += "异常信息: " + objErr.Message + "<br>";
//  errorinfo = "异常信息: " + objErr.Message;

//   errorsource = "错误源:" + objErr.Source;
//  errortrace = "堆栈信息:" + objErr.StackTrace;
//  error += "--------------------------------------<br>";
//  Server.ClearError();
//  Application["error"] = error;