using System;
using System.Collections.Generic;
 
using System.Text;

namespace Model
{
    /// <summary>
    /// 权限类
    /// </summary>
    public class Permission
    {
        #region "Private Variables"
        private string _M_modulename;
        private int _Moduleid;
        private string _PageCode;
        private string _PageCodeName;
        private List<PermissionItem> _ItemList = new List<PermissionItem>();
        #endregion


        #region "Public Variables"
        /// <summary>
        /// 模块名称
        /// </summary>
        public string M_modulename
        {
            get
            {
                return _M_modulename;
            }
            set
            {
                _M_modulename = value;
            }
        }

        /// <summary>
        /// 模块ID
        /// </summary>
        public int Moduleid
        {
            get
            {
                return _Moduleid;
            }
            set
            {
                _Moduleid = value;
            }
        }
        /// <summary>
        /// 应用代码
        /// </summary>
        public string PageCode
        {
            get
            {
                return _PageCode;
            }
            set
            {
                _PageCode = value;
            }
        }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string PageCodeName
        {
            get
            {
                return _PageCodeName;
            }
            set
            {
                _PageCodeName = value;
            }
        }

        /// <summary>
        /// 权限文件列表
        /// </summary>
        public List<PermissionItem> ItemList
        {
            get
            {
                return _ItemList;
            }
            set
            {
                _ItemList = value;
            }
        }
        #endregion
    }

    /// <summary>
    /// 权限详细类
    /// </summary>
    public class PermissionItem
    {
        #region "Private Variables"
        private string _Item_Name;
        private int _Item_Value;
        private string _Item_FileList;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Item_Name
        {
            get
            {
                return _Item_Name;
            }
            set
            {
                _Item_Name = value;
            }
        }
        /// <summary>
        /// 权限值
        /// </summary>
        public int Item_Value
        {
            get
            {
                return _Item_Value;
            }
            set
            {
                _Item_Value = value;
            }
        }

        /// <summary>
        /// 权限所属文件列表
        /// </summary>
        public string Item_FileList
        {
            get
            {
                return _Item_FileList;
            }
            set
            {
                _Item_FileList = value;
            }
        }
        #endregion
    }

    #region "权限类型"


    /// <summary>
    /// 权限开通、关闭
    /// </summary>
    public enum Tag
    {
        /// <summary>
        /// 开通 1
        /// </summary>
        Open=1,
        /// <summary>
        /// 关闭 0
        /// </summary>
        Close=0
    }

    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PopedomType
    {
        /// <summary>
        /// 2
        /// </summary>
        A2 = 2,
        /// <summary>
        /// 4
        /// </summary> 
        A4 = 4,
        /// <summary>
        /// 8
        /// </summary>
        A8 = 8,
        /// <summary>
        /// 16
        /// </summary>
        A16 = 16,
        /// <summary>
        /// 32
        /// </summary>
        A32 = 32,
        /// <summary>
        /// 64
        /// </summary>
        A64 = 64,
        /// <summary>
        /// 128
        /// </summary>
        A128 = 128,
        /// <summary>
        /// 256
        /// </summary>
        A256 = 256,
        /// <summary>
        /// 512
        /// </summary>
        A512 = 512,
        /// <summary>
        /// 1024
        /// </summary>
        A1024= 1024,
        /// <summary>
        /// 2048
        /// </summary>
        A2048 = 2048,
        /// <summary>
        /// 自定义权限4096 
        /// </summary>
        A4096 = 4096,
        /// <summary>
        /// 自定义权限8192
        /// </summary>
        A8192 = 8192,
        /// <summary>
        /// 自定义权限16384
        /// </summary>
        A16384 = 16384,
        /// <summary>
        /// 自定义权限32768
        /// </summary>
        A32768 = 32768,
        /// <summary>
        /// 自定义权限65536
        /// </summary>
        A65536 = 65536,
        /// <summary>
        /// 自定义权限131072
        /// </summary>
        A131072 = 131072,
        /// <summary>
        /// 自定义权限262144
        /// </summary>
        Custom262144 = 262144,
        /// <summary>
        /// 自定义权限524288
        /// </summary>
        Custom524288 = 524288,
        /// <summary>
        /// 自定义权限1048576
        /// </summary>
        Custom1048576 = 1048576,
        /// <summary>
        /// 自定义权限2097152
        /// </summary>
        Custom2097152 = 2097152,
        /// <summary>
        /// 自定义权限4194304
        /// </summary>
        Custom4194304 = 4194304,
        /// <summary>
        /// 自定义权限8388608
        /// </summary>
        Custom8388608 = 8388608,
        /// <summary>
        /// 自定义权限16777216
        /// </summary>
        Custom16777216 = 16777216,
        /// <summary>
        /// 自定义权限33554432
        /// </summary>
        Custom33554432 = 33554432,
        /// <summary>
        /// 自定义权限67108864
        /// </summary>
        Custom67108864 = 67108864,
        /// <summary>
        /// 自定义权限134217728
        /// </summary>
        Custom134217728 = 134217728,
        /// <summary>
        /// 自定义权限268435456
        /// </summary>
        Custom268435456 = 268435456,
        /// <summary>
        /// 自定义权限536870912
        /// </summary>
        Custom536870912 = 536870912
    }
    #endregion
}
