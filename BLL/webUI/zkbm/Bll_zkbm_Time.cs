using System;
using System.Collections.Generic;
using Model;
using System.Text;
using BLL;
namespace BLL
{
    /// <summary>
    /// 判断报名时间控制类
    /// </summary>
    public class Bll_zkbm_Time
    {
        /// <summary>
        /// 设置报名控制类
        /// </summary>
        BLL_zk_szbmsj BLLsj = new BLL_zk_szbmsj();

        /// <summary>
        /// 设置报名时间实体类
        /// </summary>
        Model_zk_szbmsj Modelsj = new Model_zk_szbmsj();

      /// <summary>
        /// 判断是否可以报名
      /// </summary>
      /// <param name="xqdm">县区代码</param>
      /// <param name="type">类型1报名2志愿3体育4加试</param>
      /// <returns></returns>
        public bool zkbm_time(string xqdm,int type )
        {
            Modelsj = BLLsj.SelectDispBig();

            if (type == 1)
            {
                //判断大市时间
                if (config.DateTimeCompare(DateTime.Now, Modelsj.Kssj) > 0 && config.DateTimeCompare(DateTime.Now, Modelsj.Jssj) < 0)
                {
                    //判断考生所在区的时间
                    Modelsj = BLLsj.Select_zk_szbmsj(xqdm);
                    if (config.DateTimeCompare(DateTime.Now, Modelsj.Kssj) > 0 && config.DateTimeCompare(DateTime.Now, Modelsj.Jssj) < 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (type == 2)
            {

                //判断大市时间
                if (config.DateTimeCompare(DateTime.Now, Modelsj.Kssj_zy) > 0 && config.DateTimeCompare(DateTime.Now, Modelsj.Jssj_zy) < 0)
                {
                    //判断考生所在区的时间
                    Modelsj = BLLsj.Select_zk_szbmsj(xqdm);
                    if (config.DateTimeCompare(DateTime.Now, Modelsj.Kssj_zy) > 0 && config.DateTimeCompare(DateTime.Now, Modelsj.Jssj_zy) < 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (type == 3)
            {

                //判断大市时间
                if (config.DateTimeCompare(DateTime.Now, Modelsj.Kssj_ty) > 0 && config.DateTimeCompare(DateTime.Now, Modelsj.Jssj_ty) < 0)
                {
                    //判断考生所在区的时间
                    Modelsj = BLLsj.Select_zk_szbmsj(xqdm);
                    if (config.DateTimeCompare(DateTime.Now, Modelsj.Kssj_ty) > 0 && config.DateTimeCompare(DateTime.Now, Modelsj.Jssj_ty) < 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            
            else if (type == 4)
            {

                //判断大市时间
                if (config.DateTimeCompare(DateTime.Now, Modelsj.Kssj_js) > 0 && config.DateTimeCompare(DateTime.Now, Modelsj.Jssj_js) < 0)
                {
                    //判断考生所在区的时间
                    Modelsj = BLLsj.Select_zk_szbmsj(xqdm);
                    if (config.DateTimeCompare(DateTime.Now, Modelsj.Kssj_js) > 0 && config.DateTimeCompare(DateTime.Now, Modelsj.Jssj_js) < 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (type == 5)
            {

                //判断大市时间
                if (config.DateTimeCompare(DateTime.Now, Modelsj.Kssj_sb) > 0 && config.DateTimeCompare(DateTime.Now, Modelsj.Jssj_sb) < 0)
                {
                    //判断考生所在区的时间
                    Modelsj = BLLsj.Select_zk_szbmsj(xqdm);
                    if (config.DateTimeCompare(DateTime.Now, Modelsj.Kssj_sb) > 0 && config.DateTimeCompare(DateTime.Now, Modelsj.Jssj_sb) < 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
