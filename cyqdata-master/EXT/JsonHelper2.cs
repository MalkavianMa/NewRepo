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

        public static string ToJsonForLayUI(object obj)
        {
            return ToJsonForLayUI(obj, false, RowOp.IgnoreNull);
        }


        public static string ToJsonForLayUI(object obj, bool isConvertNameToLower)
        {
            return ToJsonForLayUI(obj, isConvertNameToLower, RowOp.IgnoreNull);
        }
        public static string ToJsonForLayUI(object obj, bool isConvertNameToLower, RowOp rowOp)
        {
            return ToJsonForLayUI(obj, isConvertNameToLower, rowOp, DefaultEscape);
        }

        /// <param name="op">default value is RowOp.All
        /// <para>默认值为RowOp.All</para></param>
        public static string ToJsonForLayUI(object obj, bool isConvertNameToLower, RowOp rowOp, EscapeOp escapeOp)
        {
            string text = Convert.ToString(obj);
            if (text == "")
            {
                return "{}";
            }
            else if (text[0] == '{' || text[0] == '[')
            {
                if (IsJson(text))
                {
                    return text;
                }
            }
            JsonHelper js = new JsonHelper(true);
            js.Escape = escapeOp;
            js.IsConvertNameToLower = isConvertNameToLower;
            js.RowOp = rowOp;
            js.Fill(obj);
            return js.ToStringForLayUI(obj is IList || obj is MDataTable || obj is DataTable);
        }

        /// <summary>
        /// out json result
        /// <para>输出Json字符串</para>
        /// </summary>
        public string ToStringForLayUI()
        {
            int capacity = 100;
            if (jsonItems.Count > 0)
            {
                capacity = jsonItems.Count * jsonItems[0].Length;
            }
            StringBuilder sb = new StringBuilder(capacity);

            if (_AddHead)
            {
                sb.Append("{");
                sb.Append("\"rowcount\":" + rowCount + ",");
                sb.Append("\"count\":" + Total + ",");
                sb.Append("\"msg\":\"" + errorMsg + "\",");
                sb.Append("\"code\":0," );// + Success.ToString().ToLower() + ",");
                sb.Append("\"data\":");
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
        public string ToStringForLayUI(bool arrayEnd)
        {
            string result = ToStringForLayUI();
            if (arrayEnd && !result.StartsWith("["))
            {
                result = '[' + result + ']';
            }
            return result;
        }


    }
}
