using CYQ.Data.Table;
using CYQ.Data.Tool;
using System;
using System.Collections.Generic;
using System.Text;

namespace CYQ.Data.Table
{
    public partial class MDataTable
    {
        /// <summary>
        /// 输出Json
        /// </summary>
        public string ToJsonForLayUI()
        {
            return ToJsonForLayUI(true);
        }
        public string ToJsonForLayUI(bool addHead)
        {
            return ToJsonForLayUI(addHead, false);
        }
        /// <param name="addHead">输出头部信息[带count、Success、ErrorMsg](默认true)</param>
        /// <param name="addSchema">首行输出表架构信息,反接收时可还原架构(默认false)</param>
        public string ToJsonForLayUI(bool addHead, bool addSchema)
        {
            return ToJsonForLayUI(addHead, addSchema, RowOp.None);
        }
        /// <param name="rowOp">过滤选项</param>
        public string ToJsonForLayUI(bool addHead, bool addSchema, RowOp rowOp)
        {
            return ToJsonForLayUI(addHead, addSchema, rowOp, false);
        }

        public string ToJsonForLayUI(bool addHead, bool addSchema, RowOp rowOp, bool isConvertNameToLower)
        {
            return ToJsonForLayUI(addHead, addSchema, rowOp, isConvertNameToLower, JsonHelper.DefaultEscape);
        }
        /// <param name="op">符号转义选项</param>
        public string ToJsonForLayUI(bool addHead, bool addSchema, RowOp rowOp, bool isConvertNameToLower, EscapeOp escapeOp)
        {
            JsonHelper helper = new JsonHelper(addHead, addSchema);
            helper.Escape = escapeOp;
            helper.IsConvertNameToLower = isConvertNameToLower;
            helper.RowOp = rowOp;
            helper.Fill(this);
            bool checkArrayEnd = !addHead && !addSchema;
            return helper.ToStringForLayUI(checkArrayEnd);
        }
        /// <summary>
        /// 字段名改为大写
        /// </summary>
        public MDataTable ToUpperForColumnName() {
            
            for (int i = 0; i < this.Columns.Count; i++)
            { this.Columns[i].ColumnName = this.Columns[i].ColumnName.ToUpper(); }
            return this;

        }
        /// <summary>
        /// 字段名改为小写
        /// </summary>
        public MDataTable ToLowerForColumnName()
        {

            for (int i = 0; i < this.Columns.Count; i++)
            { this.Columns[i].ColumnName = this.Columns[i].ColumnName.ToLower(); }
            return this;
        }

    }
}
