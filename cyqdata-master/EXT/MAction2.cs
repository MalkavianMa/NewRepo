using CYQ.Data.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace CYQ.Data
{
    public partial class MAction : IDisposable
    {

        /// <param name="conn">Database connection statement or configuration key
        /// <para>数据库链接语句或配置Key</para></param>
        public MAction(object tableNamesEnum, string conn,int i)
        {
            string tableNamesEnum2 = "select * from  PC_SheBei_Class";
            Init(tableNamesEnum2, conn, true, null, true);
        } 
        /// <summary>
        /// 用于将来扩展支持数据权限配置
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public MDataTable SelectExt(int pageIndex, int pageSize, object where)
        { 
            return Select(pageIndex, pageSize, where);
        }
    }
}
