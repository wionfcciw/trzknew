using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SincciKC.SsoLogin.SSOModel.UserRoleAndDeptJsonEntityPack
{
    public class UserRoleAndDeptJsonEntity
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
    //如果好用，请收藏地址，帮忙分享。
    public class ListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string education { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string school { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userSwitch { get; set; }
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
        public string education { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public School school { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserSwitch userSwitch { get; set; }
    }

    //如果好用，请收藏地址，帮忙分享。
    public class UserSwitch
    {
        /// <summary>
        /// 
        /// </summary>
        public string pagination { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accountId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int accountType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string studentUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string studentAccountId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string switchTime { get; set; }
    }

    public class School
    {
        /// <summary>
        /// 
        /// </summary>
        public string pagination { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolId { get; set; }
        /// <summary>
        /// 碧江紫荆小学119
        /// </summary>
        public string schoolName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolSpellName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolStepId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolStepCode { get; set; }
        /// <summary>
        /// 九年一贯制
        /// </summary>
        public string schoolStepName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolKindId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolKindCode { get; set; }
        /// <summary>
        /// 民办
        /// </summary>
        public string schoolKindName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolPid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolProvince { get; set; }
        /// <summary>
        /// 贵州省
        /// </summary>
        public string schoolProvinceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolCity { get; set; }
        /// <summary>
        /// 铜仁市
        /// </summary>
        public string schoolCityName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolArea { get; set; }
        /// <summary>
        /// 铜仁市
        /// </summary>
        public string schoolAreaName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int schoolStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolContact { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolContactType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolCreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolNotes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schoolUpdateOper { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tenantId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rrtOpenId { get; set; }
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