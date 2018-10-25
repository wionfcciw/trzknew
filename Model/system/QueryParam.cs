using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{  /// <summary>
    /// 分页存储过程查询参数类
    /// </summary>
   public class QueryParam
    {
        #region "Private Variables"
        private string _TableName;
        private string _ReturnFields;
        private string _Where;
        private string _OrderId;
        private string _Order;
        private int _PageIndex = 1;
        private int _PageSize = int.MaxValue;
        #endregion

        #region "Public Variables"

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
            }

        }



        /// <summary>
        /// 返回字段
        /// </summary>
        public string ReturnFields
        {
            get
            {
                return _ReturnFields;
            }
            set
            {
                _ReturnFields = value;
            }
        }




        /// <summary>
        /// 查询条件 无需带Where
        /// </summary>
        public string Where
        {
            get
            {
                return _Where;
            }
            set
            {
                _Where = value;
            }
        }


        /// <summary>
        /// 排序字段 主键
        /// </summary>
        public string OrderId
        {
            get
            {
                return _OrderId;
            }
            set
            {
                _OrderId = value;
            }
        }


        /// <summary>
        /// 排序  需带order by 
        /// </summary>
        public string Order
        {
            get
            {
                return _Order;
            }
            set
            {
                _Order = value;
            }
        }


        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
            set
            {
                _PageIndex = value;
            }

        }


        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
            }
        }
        #endregion
    }
    
}
