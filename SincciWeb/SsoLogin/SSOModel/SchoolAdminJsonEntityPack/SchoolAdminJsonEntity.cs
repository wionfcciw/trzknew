using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SincciKC.SsoLogin.SSOModel.SchoolAdminJsonEntityPack
{
    public class SchoolAdminJsonEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public PageInfo pageInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ResponseEntity responseEntity { get; set; }
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
        public List<string> orgIdList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roleCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roleName { get; set; }
    }

    public class PageInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int endRow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int firstPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hasNextPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hasPreviousPage { get; set; }
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
        public int lastPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int navigatePages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> navigatepageNums { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int nextPage { get; set; }
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
        public int pages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int prePage { get; set; }
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
        public int total { get; set; }
    }

    public class ResponseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> orgIdList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roleCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roleName { get; set; }
    }

    public class ServerResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string errorParam { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string internalMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string resultCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string resultMsg { get; set; }
    }

}