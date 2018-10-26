using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SincciKC.SsoLogin.SSOModel.SchoolClassJsonEntityPack
{
    /// <summary>
    /// 通过学校id和年级类型查询班级
    /// </summary>
    public class SchoolClassJsonEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string responseEntity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PageInfo pageInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ServerResult serverResult { get; set; }
    }

    public class ListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string classId { get; set; }
        /// <summary>
        /// 初三（16）
        /// </summary>
        public string className { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classPid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classBuildTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classMaster { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classMasterPhone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int classStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classCreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classUpdateOper { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classNotes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classStep { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tenantId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classGradeCode { get; set; }
        /// <summary>
        /// 初三上学期
        /// </summary>
        public string classGrade { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string classResGrade { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int artScienceFlag { get; set; }
    }

    public class PageInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int pageNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int startRow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int endRow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int firstPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int prePage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int nextPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int lastPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isFirstPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isLastPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hasPreviousPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hasNextPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int navigatePages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string navigatepageNums { get; set; }
    }

    public class ServerResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string resultCode { get; set; }
        /// <summary>
        /// 操作成功
        /// </summary>
        public string resultMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string internalMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errorParam { get; set; }
    }

}