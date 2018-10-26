using System;
using System.Collections.Generic;
using System.Text;

namespace Model 
{
    /// <summary>
    /// 新闻类型实体类 PE_NewsCategoryTable
    /// </summary>
    public class PE_NewsCategoryTable
    {
        #region 新闻字段
        private string _DataTable_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除
        private int _PCID = 0;   //类型ID
        private string _CategoryName = string.Empty; //类型名称
        private int _ParentID = 0;  //父节点ID
        private int _Level = 0; //树的深度

        #endregion

        #region 新闻属性
        /// <summary>
        /// 操作方法 Insert:增加 Update:修改 Delete:删除
        /// </summary>
        public string DataTable_Action_
        {
            get { return _DataTable_Action_; }
            set { _DataTable_Action_ = value; }
        }

        /// <summary>
        /// 类型ID
        /// </summary>
        public int PCID
        {
            get { return _PCID; }
            set { _PCID = value; }
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        /// <summary>
        /// 父节点的ID
        /// </summary>
        public int ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }

        /// <summary>
        /// 树的深度
        /// </summary>
        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        #endregion
    }
}
