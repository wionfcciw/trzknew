using System;
using System.Collections.Generic;
using System.Text;

namespace Model 
{
    /// <summary>
    /// 新闻实体类 PE_NewsListTable
    /// </summary>
    public class PE_NewsListTable
    {
        #region 新闻字段
        private string _DataTable_Action_;  // 操作方法 Insert:增加 Update:修改 Delete:删除
        private int _NewsID = 0;   //新闻ID
        private string _Title = string.Empty; //新闻标题
        private string _Content = string.Empty; //新闻内容
        private string _Urls = string.Empty; // 链接地址
        private string _Editor = string.Empty; //作者
        private DateTime? _PublishTime ; //发布时间
        private int _Number = 0;  //访问量
        private int _CategoryID = 0; //新闻类型ID
        private int _Show = 0;  //是否显示
        private int _MarkTop = 0; //是否置顶
        private int _MarkPass = 0; //是否审核
        private int _MarkImp = 0; //是否重要 
        private int _GongGao = 0; //是否公告
        private int _MarkType = 0; //标记
        private int _AreaID = 0;  //区域ID
        private string _Remark = string.Empty;  //文章备注
        private string _TitleUrls = string.Empty; //相对链接地址
        private int _ScopeID = 0; //管辖范围ID
        private string _N_NewID = string.Empty; //唯一码

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
        /// 标记
        /// </summary>
        public int MarkType
        {
            get { return _MarkType; }
            set { _MarkType = value; }
        }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public int NewsID
        {
            get { return _NewsID; }
            set { _NewsID = value; }
        }

        /// <summary>
        /// 区域ID
        /// </summary>
        public int AreaID
        {
            get { return _AreaID; }
            set { _AreaID = value; }
        }

        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        /// <summary>
        /// 新闻内容
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        /// <summary>
        /// 文章备注
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        /// <summary>
        /// 绝对链接地址
        /// </summary>
        public string Urls
        {
            get { return _Urls; }
            set { _Urls = value; }
        }

        /// <summary>
        /// 相对链接地址
        /// </summary>
        public string TitleUrls
        {
            get { return _TitleUrls; }
            set { _TitleUrls = value; }
        }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? PublishTime
        {
            get { return _PublishTime; }
            set { _PublishTime = value; }
        }

        /// <summary>
        /// 新闻作者
        /// </summary>
        public string Editor
        {
            get { return _Editor; }
            set { _Editor = value; }
        }

        /// <summary>
        /// 访问量
        /// </summary>
        public int Number
        {
            get { return _Number; }
            set { _Number = value; }
        }

        /// <summary>
        /// 新闻类型ID
        /// </summary>
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        public int Show
        {
            get { return _Show; }
            set { _Show = value; }
        }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int MarkTop
        {
            get { return _MarkTop; }
            set { _MarkTop = value; }
        }

        /// <summary>
        /// 是否审核
        /// </summary>
        public int MarkPass
        {
            get { return _MarkPass; }
            set { _MarkPass = value; }
        }

        /// <summary>
        /// 是否重要
        /// </summary>
        public int MarkImp
        {
            get { return _MarkImp; }
            set { _MarkImp = value; }
        }

        /// <summary>
        /// 是否公告
        /// </summary>
        public int GongGao
        {
            get { return _GongGao; }
            set { _GongGao = value; }
        }
        /// <summary>
        /// 管辖范围
        /// </summary>
        public int ScopeID
        {
            get { return _ScopeID; }
            set { _ScopeID = value; }
        }

        /// <summary>
        /// 唯一码
        /// </summary>
        public string N_NewID
        {
            get { return _N_NewID; }
            set { _N_NewID = value; }
        }
        #endregion
    }
}
