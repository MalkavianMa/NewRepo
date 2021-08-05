using CYQ.Data.Table;
using CYQ.Data.Tool;
using System;
using System.Collections.Generic;
using System.Text;

namespace CYQ.Data.Table
{
    public partial class MDataTable
    {
        public int rowsPerPage;
        public int curPageIndex;
        /// <summary>
        /// 输出Json。参数用于拼装返回结果
        /// </summary>
        /// <param name="rows">每页的记录数</param>
        /// <param name="page">当前页</param>
        /// <returns></returns>
        public string ToJsonForPagination(int rows,int page)
        {
            rowsPerPage = rows;
            curPageIndex = page;
            return ToJsonForPagination(true);
        }

        /// <summary>
        /// 输出Json
        /// </summary>
        public string ToJsonForPagination()
        {
            return ToJsonForPagination(true);
        }
        public string ToJsonForPagination(bool addHead)
        {
            return ToJsonForPagination(addHead, false);
        }
        /// <param name="addHead">输出头部信息[带count、Success、ErrorMsg](默认true)</param>
        /// <param name="addSchema">首行输出表架构信息,反接收时可还原架构(默认false)</param>
        public string ToJsonForPagination(bool addHead, bool addSchema)
        {
            return ToJsonForPagination(addHead, addSchema, RowOp.None);
        }
        /// <param name="rowOp">过滤选项</param>
        public string ToJsonForPagination(bool addHead, bool addSchema, RowOp rowOp)
        {
            return ToJsonForPagination(addHead, addSchema, rowOp, false);
        }

        public string ToJsonForPagination(bool addHead, bool addSchema, RowOp rowOp, bool isConvertNameToLower)
        {
            return ToJsonForPagination(addHead, addSchema, rowOp, isConvertNameToLower, JsonHelper.DefaultEscape);
        }
        /// <param name="op">符号转义选项</param>
        public string ToJsonForPagination(bool addHead, bool addSchema, RowOp rowOp, bool isConvertNameToLower, EscapeOp escapeOp)
        {
            JsonHelper helper = new JsonHelper(addHead, addSchema);
            helper.Escape = escapeOp;
            helper.IsConvertNameToLower = isConvertNameToLower;
            helper.RowOp = rowOp;
            helper.Fill(this);
            bool checkArrayEnd = !addHead && !addSchema;
            return helper.ToStringForPagination(checkArrayEnd, rowsPerPage, curPageIndex);
        }
    }
}
