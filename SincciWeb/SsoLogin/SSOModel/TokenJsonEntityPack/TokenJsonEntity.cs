using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SincciKC.SsoLogin.TokenJsonEntityPack.SSOModel
{
    [Serializable]
    public class TokenJsonEntity
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

    [Serializable]
    public class ResponseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int expires_in { get; set; }
    }

    [Serializable]
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