using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SincciKC.SsoLogin.UserJsonEntityPack.SSOModel
{
    public class UserJsonEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public ResponseEntity responseEntity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ServerResult serverResult { get; set; }
    }

    public class ResponseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 魏小欧
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userLoginName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userSex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userIdCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userMobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string clientId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userSpellName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userBirthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userPhoto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userNotes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rrtOpenId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int userType { get; set; }
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