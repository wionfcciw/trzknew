﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
 
using System.IO;
/// <summary>
/// ZipClass 的摘要说明
/// </summary>
public class ZipClass
{
    public ZipClass()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //

    }/// <summary>  

    /// 压缩单个文件  

    /// </summary>  

    /// <param name="fileToZip">要压缩的文件</param>  

    /// <param name="zipedFile">压缩后的文件</param>  

    /// <param name="compressionLevel">压缩等级</param>  

    /// <param name="blockSize">每次写入大小</param>  

    public static void ZipFile(string fileToZip, string zipedFile, int compressionLevel, int blockSize)  

     {  

         //如果文件没有找到，则报错  

         if (!System.IO.File.Exists(fileToZip))  

         {  

             throw new System.IO.FileNotFoundException("指定要压缩的文件: " + fileToZip + " 不存在!");  

         }  

   

         using (System.IO.FileStream ZipFile = System.IO.File.Create(zipedFile))  

         {  

             using (ZipOutputStream ZipStream = new ZipOutputStream(ZipFile))  

             {  

                 using (System.IO.FileStream StreamToZip = new System.IO.FileStream(fileToZip, System.IO.FileMode.Open, System.IO.FileAccess.Read))  

                 {  

                     string fileName = fileToZip.Substring(fileToZip.LastIndexOf("\\") + 1);  

   

                     ZipEntry ZipEntry = new ZipEntry(fileName);  

   

                     ZipStream.PutNextEntry(ZipEntry);  

   

                     ZipStream.SetLevel(compressionLevel);  

   

                     byte[] buffer = new byte[blockSize];  

   

                     int sizeRead = 0;  

   

                     try 

                     {  

                         do 

                         {  

                             sizeRead = StreamToZip.Read(buffer,0 , buffer.Length);  

                             ZipStream.Write(buffer,0 , sizeRead);  

                         }  

                         while (sizeRead > 0);  

                     }  

                     catch (System.Exception ex)  

                     {  

                         throw ex;  

                     }  

   

                     StreamToZip.Close();  

                 }  

   

                 ZipStream.Finish();  

                 ZipStream.Close();  

             }  

   

             ZipFile.Close();  

         }  

     }



    /// <summary>  

    /// 压缩单个文件  

    /// </summary>  

    /// <param name="fileToZip">要进行压缩的文件名</param>  

    /// <param name="zipedFile">压缩后生成的压缩文件名</param>  

    public  void ZipFile(string fileToZip, string zipedFile)  

     {  

         //如果文件没有找到，则报错  

         if (!File.Exists(fileToZip))  

         {  

             throw new System.IO.FileNotFoundException("指定要压缩的文件: " + fileToZip + " 不存在!");  

         }  
         using (FileStream fs = File.OpenRead(fileToZip))  

         {  

             byte[] buffer = new byte[fs.Length];  

             fs.Read(buffer,0 , buffer.Length);  

             fs.Close();  

   

             using (FileStream ZipFile = File.Create(zipedFile))  

             {  

                 using (ZipOutputStream ZipStream = new ZipOutputStream(ZipFile))  

                 {  

                     string fileName = fileToZip.Substring(fileToZip.LastIndexOf("\\") + 1);  

                     ZipEntry ZipEntry = new ZipEntry(fileName);  

                     ZipStream.PutNextEntry(ZipEntry);  

                     ZipStream.SetLevel(5);  
                     ZipStream.Write(buffer, 0, buffer.Length);  

                     ZipStream.Finish();  

                     ZipStream.Close();  

                 }  

             }  

         }  

     }

    /// <summary>
    /// 压缩文件夹
    /// </summary>
     /// <param name="strFile">待压缩文件目录</param>
     /// <param name="strZip">压缩后的目标文件</param>
    public static void ZipFiles(string strFile, string strZip)
     {
         if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
             strFile += Path.DirectorySeparatorChar;
         ZipOutputStream s = new ZipOutputStream(File.Create(strZip));
         s.SetLevel(6); // 0 - store only to 9 - means best compression
         zip(strFile, s, strFile);
         s.Finish();
         s.Close();
     }



    private static void zip(string strFile, ZipOutputStream s, string staticFile)
     {
         if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar) strFile += Path.DirectorySeparatorChar;
         Crc32 crc = new Crc32();
         string[] filenames = Directory.GetFileSystemEntries(strFile);
         foreach (string file in filenames)
         {

             if (Directory.Exists(file))
             {
                 zip(file, s, staticFile);
             }

             else // 否则直接压缩文件
             {
                 //打开压缩文件
                 FileStream fs = File.OpenRead(file);

                 byte[] buffer = new byte[fs.Length];
                 fs.Read(buffer, 0, buffer.Length);
                 string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                 ZipEntry entry = new ZipEntry(tempfile);

                 entry.DateTime = DateTime.Now;
                 entry.Size = fs.Length;
                 fs.Close();
                 crc.Reset();
                 crc.Update(buffer);
                 entry.Crc = crc.Value;
                 s.PutNextEntry(entry);

                 s.Write(buffer, 0, buffer.Length);
             }
         }
     }

    ///// <summary>  

    ///// 压缩多层目录  

    ///// </summary>  

    ///// <param name="strDirectory">The directory.</param>  

    ///// <param name="zipedFile">The ziped file.</param>  

    //public static void ZipFileDirectory(string strDirectory, string zipedFile)
    //{

    //    using (System.IO.FileStream ZipFile = System.IO.File.Create(zipedFile))
    //    {

    //        using (ZipOutputStream s = new ZipOutputStream(ZipFile))
    //        {

    //            ZipSetp(strDirectory, s, "");

    //        }

    //    }

    //}



    ///// <summary>  

    ///// 递归遍历目录  

    ///// </summary>  

    ///// <param name="strDirectory">The directory.</param>  

    ///// <param name="s">The ZipOutputStream Object.</param>  

    ///// <param name="parentPath">The parent path.</param>  

    //private static void ZipSetp(string strDirectory, ZipOutputStream s, string parentPath)
    //{

    //    if (strDirectory[strDirectory.Length - 1] != Path.DirectorySeparatorChar)
    //    {

    //        strDirectory += Path.DirectorySeparatorChar;

    //    }

    //    Crc crc = new Crc();



    //    string[] filenames = Directory.GetFileSystemEntries(strDirectory);



    //    foreach (string file in filenames)// 遍历所有的文件和目录  
    //    {



    //        if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件  
    //        {

    //            string pPath = parentPath;

    //            pPath += file.Substring(file.LastIndexOf("\\") + 1);

    //            pPath += "\\";

    //            ZipSetp(file, s, pPath);

    //        }



    //        else // 否则直接压缩文件  
    //        {

    //            //打开压缩文件  

    //            using (FileStream fs = File.OpenRead(file))
    //            {



    //                byte[] buffer = new byte[fs.Length];

    //                fs.Read(buffer, 0, buffer.Length);



    //                string fileName = parentPath + file.Substring(file.LastIndexOf("\\") + 1);

    //                ZipEntry entry = new ZipEntry(fileName);



    //                entry.DateTime = DateTime.Now;

    //                entry.Size = fs.Length;



    //                fs.Close();



    //                crc.Reset();

    //                crc.Update(buffer);



    //                entry.Crc = crc.Value;

    //                s.PutNextEntry(entry);



    //                s.Write(buffer, 0, buffer.Length);

    //            }

    //        }

    //    }

    //}



    /// <summary>  

    /// 解压缩一个 zip 文件。  

    /// </summary>  

    /// <param name="zipedFile">The ziped file.</param>  

    /// <param name="strDirectory">The STR directory.</param>  

    /// <param name="password">zip 文件的密码。</param>  

    /// <param name="overWrite">是否覆盖已存在的文件。</param>  

    public void UnZip(string zipedFile, string strDirectory, string password, bool overWrite)
    {



        if (strDirectory == "")

            strDirectory = Directory.GetCurrentDirectory();

        if (!strDirectory.EndsWith("\\"))

            strDirectory = strDirectory + "\\";



        using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipedFile)))
        {

            s.Password = password;

            ZipEntry theEntry;



            while ((theEntry = s.GetNextEntry()) != null)
            {

                string directoryName = "";

                string pathToZip = "";

                pathToZip = theEntry.Name;



                if (pathToZip != "")

                    directoryName = Path.GetDirectoryName(pathToZip) + "\\";



                string fileName = Path.GetFileName(pathToZip);



                Directory.CreateDirectory(strDirectory + directoryName);



                if (fileName != "")
                {

                    if ((File.Exists(strDirectory + directoryName + fileName) && overWrite) || (!File.Exists(strDirectory + directoryName + fileName)))
                    {

                        using (FileStream streamWriter = File.Create(strDirectory + directoryName + fileName))
                        {

                            int size = 2048;

                            byte[] data = new byte[2048];

                            while (true)
                            {

                                size = s.Read(data, 0, data.Length);



                                if (size > 0)

                                    streamWriter.Write(data, 0, size);

                                else

                                    break;

                            }

                            streamWriter.Close();

                        }

                    }

                }

            }



            s.Close();

        }

    }



}
