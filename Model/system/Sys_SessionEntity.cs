using System;
using System.Collections.Generic;
 
using System.Text;

namespace Model
{
    /// <summary>
    /// 用户Session 实体类
    /// </summary>
    [Serializable]
    public class Sys_SessionEntity
    {
        #region "Private Variables"
        private string _UserName;  //用户登录名
        private string _Name; //姓名
        private int _UserType;  //用户类型
        private int _ExamID;   //登录考试
        private string _U_department; //管辖部门
        private bool _Flag = false; //登录成功标识

        #endregion

        #region "Public Variables"

        /// <summary>
        /// 用户登录名
        /// </summary>
        public string UserName
        {
            get
            { return _UserName; }
            set
            { _UserName = value; }

        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get
            { return _Name; }
            set
            { _Name = value; }

        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType
        {
            get
            { return _UserType; }
            set
            {
                _UserType = value;
            }
        }

        /// <summary>
        /// 登录考试
        /// </summary>
        public int ExamID
        {
            get
            { return _ExamID; }
            set
            { _ExamID = value; }
        }
        /// <summary>
        /// 管辖部门
        /// </summary>
        public string U_department
        {
            get
            { return _U_department; }
            set
            { _U_department = value; }
        }

        /// <summary>
        /// 登录成功标识
        /// </summary>
        public bool Flag
        {
            get
            { return _Flag; }
            set
            { _Flag = value; }
        }


        #endregion
    }
}
