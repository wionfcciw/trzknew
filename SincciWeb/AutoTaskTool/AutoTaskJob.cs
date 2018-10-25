using DAL;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SincciKC.AutoTaskTool
{
    class AutoTaskJob:IJob
    {
        void IJob.Execute()
        {
            AutoTaskData();
        }

        void AutoTaskData()
        {
            string error="";
            bool bReturn=false;
            SqlDbHelper_1 helper = new SqlDbHelper_1();
            string xxglsql = @"select a.kaoci,a.ksh,a.zkzh,a.xm,c.zlbmc as xbmc,a.mzdm,f.zlbmc as zzmm,a.sfzh,
                                      a.csrq,e.zlbmc as kslb,g.zlbmc as bklb,
                                      case a.jzfp when '0' then '否' when '1' then '是' end as jzfp,
                                      d.xxmc,a.bjdm,b.xqmc,case a.ksqr when '0' then '未报名' when '1' then '已报名' when '2' then '已确认' end as ksqr,
                                      case a.xxdy when '0' then '未打印' when '1' then '已打印' end as xxdy,
                                      a.jtdz,a.lxdh,a.txdz,a.yzbm,a.xm as sjr 
                                from zk_ksxxgl a 
                                left outer join zk_xqdm b on a.bmdxqdm=b.xqdm 
                                left outer join zk_zdxxLB c on a.xbdm=c.zlbdm and zdlbdm='XB'
                                left outer join zk_xxdm d on a.bmddm=d.xxdm
                                left outer join zk_zdxxLB e on a.kslbdm=e.zlbdm and e.zdlbdm='KSLB'
                                left outer join zk_zdxxLB f on a.kslbdm=f.zlbdm and f.zdlbdm='zzmm'
                                left outer join zk_zdxxLB g on a.bklb=g.zlbdm and g.zdlbdm='bklb'";
            DataTable xxgldt=helper.selectTab(xxglsql, ref error, ref bReturn);
            
            //创建考生信息表，后续需填充考生id
            string path = System.Web.HttpRuntime.AppDomainAppPath;
            System.IO.Directory.CreateDirectory(path+"/Temp/KsxxInfo");
            //StreamWriter sw = File.CreateText(path + "/Temp/Ksxxb.txt");
            StreamWriter sw = new StreamWriter(path+"/Temp/KsxxInfo/Ksxxb.txt",false,Encoding.Default);
            StringBuilder sb = new StringBuilder();
            sb.Append("考次|报名号|准考证号|考生ID|姓名|性别|民族|政治面貌|身份证号|出生日期|考生类别|报考类别|精准扶贫|毕业中学|班级|所属区县|考生确认|打印|家庭住址|联系电话|邮寄地址|邮政编码|收件人");
            sb.Append("\r\n");
            foreach(DataRow dr in xxgldt.Rows)
            {
                sb.Append(dr["kaoci"] + "|" + dr["ksh"] + "|" + dr["zkzh"] + "|" + dr["xm"] + "|" + dr["xbmc"] + "|" + dr["mzdm"] + "|" + dr["zzmm"] + "|" + dr["sfzh"] + "|" + dr["csrq"] + "|" + dr["kslb"] + "|" + dr["bklb"] 
                    + "|" +dr["jzfp"]+"|"+ dr["xxmc"] + "|" + dr["bjdm"] + "|" + dr["xqmc"] + "|" + dr["ksqr"] + "|" + dr["xxdy"] + "|" + dr["jtdz"] + "|" + dr["lxdh"] + "|" + dr["txdz"] + "|" + dr["yzbm"] + "|" + dr["sjr"]);
                sb.Append("\r\n");
            }
            sw.Write(sb);
            sw.Close();
            //创建招生学校表，后续需填充学校id
            sb.Clear();
            StreamWriter sw2 = new StreamWriter(path + "/Temp/KsxxInfo/Zsxxb.txt", false, Encoding.Default);
            sb.Append("考次|区县|学校代码|学校ID|学校名称|专业名称|计划数|学制|招生批次");
            sb.Append("\r\n");
            //获取考次
            string kaocisql = "select kcdm from zk_kcdm order by kcdm desc";
            DataTable kaocidt=helper.selectTab(kaocisql, ref error, ref bReturn);
            string kaoci = kaocidt.Rows[0][0].ToString();
            string zsxxsql = @"select b.xqmc,c.zsxxdm,c.zsxxmc,isnull(d.zymc,'') as zymc,a.jhs,e.zlbmc as xzmc,a.pcdm 
                               from zk_zsjh a left join zk_xqdm b on a.xqdm=b.xqdm
                               left join zk_zsxxdm c on a.xxdm=c.zsxxdm
                               left join zk_zyk d on a.zydm=d.zydm
                               left join zk_zdxxLB e on e.zlbdm=a.xzdm and e.zdlbdm='XZ'";
            DataTable zsxxdt = helper.selectTab(zsxxsql, ref error, ref bReturn);
            foreach(DataRow dr in zsxxdt.Rows)
            {
                sb.Append(kaoci + "|" + dr["xqmc"] + "|" + dr["zsxxdm"] + "|" + dr["zsxxmc"] + "|" + dr["zymc"] + "|" + dr["jhs"] + "|" + dr["xzmc"] + "|" + dr["pcdm"]);
                sb.Append("\r\n");
            }
            sw2.Write(sb);
            sw2.Close();
            //创建录取信息表，后续需填充考生id
            sb.Clear();
            StreamWriter sw3 = new StreamWriter(path + "/Temp/KsxxInfo/Kslqxxb.txt");
            sb.Append("考次|报名号|考生ID|姓名|总分|体育|文综|地生|综合等级|加分|录取学校|录取类型|录取状态|准考证号");
            sb.Append("\r\n");
            string lqxxsql = @"select a.ksh,a.xm,a.cj,a.ty,a.wkzh,a.dsdj,a.zhdj,a.jf,a.lqxxmc,b.zlbmc as lqlx,case a.td_zt when '5' then '已录取' else '未录取' end as lqzt,a.zkzh 
                               from View_Lqxx a left outer join zk_zdxxLB b 
                               on a.sftzs=b.zlbdm and b.zdlbdm='kslqlx'";
            DataTable lqxxdt = helper.selectTab(lqxxsql, ref error, ref bReturn);
            foreach(DataRow dr in lqxxdt.Rows)
            {
                sb.Append(kaoci + "|" + dr["ksh"] + "|" + dr["xm"] + "|" + dr["cj"] + "|" + dr["ty"] + "|" + dr["wkzh"] + "|" + dr["zhdj"] + "|" + dr["jf"] + "|" + dr["lqxxmc"] + "|" + dr["lqlx"] + "|" + dr["lqzt"] + "|" + dr["zkzh"]);
                sb.Append("\r\n");
            }
            sw3.Write(sb);
            sw3.Close();
            //创建志愿信息表，后续需要填充考生id
            sb.Clear();
            StreamWriter sw4 = new StreamWriter(path + "/Temp/KsxxInfo/Kszyxxb.txt");
            sb.Append("考次|报名号|准考证号|考生ID|姓名|身份证号|志愿填报状态|志愿专业|批次|志愿学校");
            sb.Append("\r\n");
            string zyxxsql = @"select a.kaoci,a.ksh,a.zkzh,a.xm,a.sfzh,isnull(b.zy1,'') as zy,isnull(b.pcdm,'')as pcdm,isnull(c.zsxxmc,'') as lqxxmc,isnull(d.lqxx,'') as lqxxdm,isnull(b.xxdm,'') as zyxxdm
                               from  View_ksxxNew a left join zk_kszyxx b on a.ksh=b.ksh
                               left outer join zk_zsxxdm c on b.xxdm=c.zsxxdm
                               left join zk_lqk d on d.ksh=a.ksh";
            DataTable zyxxdt = helper.selectTab(zyxxsql, ref error, ref bReturn);
            foreach(DataRow dr in zyxxdt.Rows)
            {
                string zytbzt = "";
                if((dr["lqxxdm"].ToString().Length>0&&dr["zyxxdm"].ToString().Length>0)&&dr["lqxxdm"].ToString()==dr["zyxxdm"].ToString())
                {
                    zytbzt="已录取";
                }
                else if((dr["lqxxdm"].ToString().Length==0&&dr["zyxxdm"].ToString().Length>0)||dr["lqxxdm"].ToString()!=dr["zyxxdm"].ToString())
                {
                    zytbzt="已填报未录取";
                }
                else if(dr["lqxxdm"].ToString().Length==0&&dr["zyxxdm"].ToString().Length==0)
                {
                    zytbzt = "未填报";
                }
                sb.Append(dr["kaoci"] + "|" + dr["ksh"] + "|" + dr["zkzh"] + "|" + dr["xm"] + "|" + dr["sfzh"] + "|" +zytbzt+ "|" + dr["zy"] + "|" + dr["pcdm"] + "|" + dr["lqxxmc"]);
                sb.Append("\r\n");
            }
            sw4.Write(sb);
            sw.Close();
            //压缩文件夹
            ZipClass.ZipFiles(path + "/Temp/KsxxInfo", path + "/Temp/KsxxInfo.zip");
            string user = "zslq";
            string password = "g7qgyrsFo";
            string uri = "139.159.156.204";
            string filename = path+"/Temp/KsxxInfo.zip";
            Upload(filename,uri,user,password);
        }

        private void Upload(string filename,string uri,string user,string password)
        {
            FileInfo fileInf = new FileInfo(filename);
            FtpWebRequest reqFTP; 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(user, password);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
