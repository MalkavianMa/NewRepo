using CYQ.Data.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CYQ.Data.Tool
{
    public partial class JsonHelper
    {   
        /// <summary>
        /// out json result
        /// <para>输出Json字符串</para>
        /// <param name="curPageIndex">当前页码</param>
        /// <param name="rowsPerPage">每页显示最大记录数</param>
        /// </summary>
        public string ToStringForPagination(int rowsPerPage, int curPageIndex)
        {
            int capacity = 100;
            if (jsonItems.Count > 0)
            {
                capacity = jsonItems.Count * jsonItems[0].Length;
            }
            StringBuilder sb = new StringBuilder(capacity);

            if (_AddHead)
            {
                //总记录数
                int records = Total;
                //每页行数
                int rows = rowsPerPage;
                //总页数
                int totalPages = 0;
                //计算总页数
                if (records > 0)
                {
                    totalPages = records % rows == 0 ? records / rows : records / rows + 1;
                }

                sb.Append("{");
                sb.Append("\"returnRowCount\":" + rowCount + ",");//当前返回的行数
                sb.Append("\"records\":" + records + ","); //总记录数
                sb.Append("\"page\":" + curPageIndex + ","); //当前页
                sb.Append("\"total\":" + totalPages + ","); //总页数
                sb.Append("\"msg\":\"" + errorMsg + "\",");
                sb.Append("\"code\":0," );// + Success.ToString().ToLower() + ",");
                sb.Append("\"rows\":");
            }
            if (jsonItems.Count <= 0)
            {
                if (_AddHead)
                {
                    sb.Append("[]");
                }
            }
            else
            {
                if (jsonItems[jsonItems.Count - 1] != "[#<br>]")
                {
                    AddBr();
                }
                if (_AddHead || rowCount > 1)
                {
                    sb.Append("[");
                }
                char left = '{', right = '}';
                if (!jsonItems[0].Contains(":") && !jsonItems[rowCount - 1].Contains(":"))
                {
                    //说明为数组
                    left = '[';
                    right = ']';
                }
                sb.Append(left);
                int index = 0;
                foreach (string val in jsonItems)
                {
                    index++;

                    if (val != brFlag)
                    {
                        sb.Append(val);
                        sb.Append(",");
                    }
                    else
                    {
                        sb.Remove(sb.Length - 1, 1);//性能优化（内部时，必须多了一个“，”号）。
                        // sb = sb.Replace(",", "", sb.Length - 1, 1);
                        sb.Append(right + ",");
                        if (index < jsonItems.Count)
                        {
                            sb.Append(left);
                        }
                    }
                }
                if (sb[sb.Length - 1] == ',')
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                //sb = sb.Replace(",", "", sb.Length - 1, 1);//去除最后一个,号。
                //sb.Append("}");
                if (_AddHead || rowCount > 1)
                {
                    sb.Append("]");
                }
            }
            if (_AddHead)
            {
                sb.Append(footText.ToString() + "}");
            }
            return sb.ToString();
            //string json = sb.ToString();
            //if (AppConfig.IsWeb && Escape == EscapeOp.Yes)
            //{
            //    json = json.Replace("\n", "<br/>");
            //}
            //if (Escape != EscapeOp.No) // Web应用
            //{
            //    json = json.Replace("\t", " ").Replace("\r", " ");
            //}
            //return json;

        }

        /// <param name="arrayEnd">end with [] ?</param>
        /// <returns></returns>
        public string ToStringForPagination(bool arrayEnd,int rowsPerPage,int curPageIndex)
        {
            string result = ToStringForPagination( rowsPerPage, curPageIndex);
            if (arrayEnd && !result.StartsWith("["))
            {
                result = '[' + result + ']';
            }
            return result;
        }


    }
}
