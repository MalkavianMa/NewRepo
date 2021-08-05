using CYQ.Data;
using CYQ.Data.Table;
using NFine.Code;
using NFine.ControllerManage.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.ModulesManage
{
  public  class ModulesManageApp
    {

        public string GetList()
        {
            //DataTable dataTable = new DataTable();
           // MAction ma = new MAction("select  *   from   TB_Module ", DBConnection.connectionString);
          //  DataTable dt = ma.Select().ToDataTable();
            //var sqlData = ma.Select();
           // dataTable = sqlData != null ? sqlData.ToDataTable() : dataTable;
            string sql = " select  *   from   TB_Module  ";

            MProc proc = new MProc(sql, DBConnection.connectionString);
            MDataTable mdt0 = proc.ExeMDataTable();



            return mdt0.ToJson() ;
        }

        public string GetSingleInfo(string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                return null;
            }

            string sql = $" select  *   from   TB_Module   where  TABID={keyValue}";

            MProc proc = new MProc(sql, DBConnection.connectionString);
            MDataTable mdt0 = proc.ExeMDataTable();
            if (mdt0.Rows.Count == 1)
            {
                return mdt0.Rows[0].ToJson();
            }
            else return null;
        }



        /// <summary>
        /// 插入更新模块
        /// </summary>
        /// <param name="object_ClassEntity"></param>
        /// <param name="keyValue"></param>

        public void SubmitForm(TB_Module_Entity object_ClassEntity, string keyValue)
        {
            // throw new NotImplementedException();
            string sql = "";
            //添加
            if (string.IsNullOrEmpty(keyValue))
            {
                sql = $@"INSERT INTO TB_Module ( 
                        
                        ModuleName ,
                        ModulePath ,
                        CreateTime ,
                        Remark ) VALUES ('{object_ClassEntity.ModuleName}','{object_ClassEntity.ModulePath}','{DateTime.Now.ToString()}','{object_ClassEntity.Remark}')";
                int res = new MProc(sql, DBConnection.connectionString).ExeNonQuery();



            }
            //更新
            else
            {

                sql = $@"update TB_Module set 
                   
                        ModuleName ={object_ClassEntity.ModuleName},
                        ModulePath = {object_ClassEntity.ModulePath},
                        Remark = {object_ClassEntity.ModulePath}  where   TABID ={keyValue}  ";
                int res = new MProc(sql, DBConnection.connectionString).ExeNonQuery();

            }



        }








    }



}
