using CYQ.Data;
using CYQ.Data.Table;
using NFine.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Domain.Entity.ControllerManage.Model;
using System.Data;
using System.Dynamic;
using NFine.Code.Extend;

namespace NFine.Application.ControllerManage
{
   public class ControllerManageApp
    {
        public string GetForm(string keyValue)
        {
            string result = "";
            if (string.IsNullOrEmpty(keyValue))
            {
                return null;
            }
            string sql = $@"  select  tbc.*,tbm.ModuleName   from   TB_Controller  tbc

             left join    TB_Module tbm  on tbc.ModuleID = tbm.TABID

             where tbc.ModuleID={ keyValue} ";
             
             DataTable dataTable = new DataTable();
            MDataTable mdt0 = null;
            using (MProc proc = new MProc(sql, DBConnection.connectionString))
            {
                mdt0  = proc.ExeMDataTable();
                dataTable = mdt0?.ToDataTable();//=mdt0!=null?:
                //dataTable需要转换成目标json  swaggerui.js才能识别 
                //result = JsonSwagger(dataTable);
             }

           
            return mdt0.ToJson();
        }




        public DataTable GetFormDataTable(string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                return null;
            }
            string sql = $@"  select  tbc.*,tbm.ModuleName   from   TB_Controller  tbc

             left join    TB_Module tbm  on tbc.ModuleID = tbm.TABID

             where tbc.ModuleID={ keyValue} ";

            DataTable dataTable = new DataTable();
            MDataTable mdt0 = null;
            using (MProc proc = new MProc(sql, DBConnection.connectionString))
            {
                mdt0 = proc.ExeMDataTable();
                dataTable = mdt0?.ToDataTable();//=mdt0!=null?:
                                                //dataTable需要转换成目标json  swaggerui.js才能识别 
                                                //result = JsonSwagger(dataTable);
            }


             return dataTable;
        }

        public DataTable GetFormData_Module(string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                return null;
            }
            string sql = $@"  select  *   from   TB_Module      where  TABID={keyValue} ";

            DataTable dataTable = new DataTable();
            MDataTable mdt0 = null;
            using (MProc proc = new MProc(sql, DBConnection.connectionString))
            {
                mdt0 = proc.ExeMDataTable();
                dataTable = mdt0?.ToDataTable(); 
            }


            return dataTable;
        }



        /// <summary>
        /// 将dataTable转换成SwageerJson
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private object JsonSwagger(DataTable dataTable)
        {
            

            dynamic infoMain = new ExpandoObject();
            dynamic tags = new ExpandoObject();
            // dynamic paths = new ExpandoObject();
            dynamic paths = new DynamicModel();
            List<dynamic> tagsList = new List<dynamic>();

            dynamic infoDetail = new ExpandoObject();


            //接口请求类型实体
            dynamic  apiType= new ExpandoObject();
            //接口信息实体
            dynamic apiInfo =new ExpandoObject();

            //接口参数信息实体
            dynamic paraApi = new ExpandoObject();
            List<dynamic> paraApiList = new List<dynamic>();

            #region sw主题信息

            infoDetail.title = "汉化版Swagger-UI";


            infoMain.swagger = "2.0";
            infoMain.info = infoDetail;
            infoMain.host = "petstore.swagger.io";
            infoMain.basePath = "/v2";

            #endregion

            #region tags控制器路由信息
            tags.name = "pet";
            tags.description = "有关你宠物的所有事情！";
          //  tags.externalDocs.description = "Find out more";
         ///   tags.externalDocs.url = "http://swagger.io";
            tagsList.Add(tags);
            tags = new ExpandoObject();
            tags.name = "store";
            tags.description = "访问宠物商店订单！";
         //   tags.externalDocs.description = "Find out more";
         //   tags.externalDocs.url = "http://swagger.io";
            tagsList.Add(tags);
            infoMain.tags = tagsList;
            #endregion


            #region paths接口信息
            //paths.(tags.name)
            //利用data转list绕开值不能直接作为变量
            //DataTable dt = new DataTable();
            //paths.king = "";
            //for (int i = 0; i < tagsList.Count; i++)
            //{
            //    dt.Columns.Add(tagsList[i].name, typeof(string));
            //    DataRow dr2 = dt.NewRow();      
            //    dr2[tagsList[i].name] = DateTime.Now;//通过名称赋值
            //    dt.Rows.Add(dr2);
            //}
            //var king = ExtConvertListDt.ToDataList<object>(dt);

            List<string> tagRoute = new List<string>();





            //是否需要增加一个映射方法 用于映射表结构和swaggerUI的json对象

            //枚举判断请求类型是POST还是Get
            apiInfo.tags = tagRoute;
            apiInfo.summary = "添加一个新宠物到商店";
            apiInfo.description = "";
            // apiType.author = "helei 更新于 2015/06/17 19:56";
            //  apiType.operationId = "addPet";
            apiInfo.consumes = new List<string> { "application/json" };
            apiInfo.produces = new List<string> { "application/json" };


            paraApi.@in = "body";
            paraApi.name= "body";
            paraApi.description = "描述";
            paraApi.required = true;
            paraApiList.Add(paraApi);

            apiInfo.parameters = paraApiList;


            apiType.post = apiInfo;
            IDictionary<string, object> dict_path = paths;
            for (int i = 0; i < tagsList.Count; i++)
            {
                //string myAttr = myList[i];
                //paths.PropertyName = tagsList[i].name;
             //   paths.Property = "测试" + i;
                tagRoute.Add(tagsList[i].name);
                dict_path.Add(tagsList[i].name, apiType);
            }
            infoMain.path = paths;
           // var a = paths.store;
           // var b = paths.pet;
            #endregion

            #region 其他信息
            infoMain.schemes = "http";
            #endregion




            return infoMain;

        }



        /// <summary>
        /// 指定模块下所有API
        /// </summary>
        /// <param name="key_ModuleID"></param>
        /// <returns></returns>
        public DataTable GetFormDataTable_Api(string key_ModuleID)
        {
            // throw new NotImplementedException();
            DataTable table = new DataTable();
          //  List<int> controllerIDlists = new List<int>();

          //  if (controllerIDlists!=null&&controllerIDlists.Any())
            if(!string.IsNullOrEmpty(key_ModuleID))
            {
                // string sql = $"select   *   from    TB_Action     where   ControllerID   in  {string.Join(",",controllerIDlists)}";
                string sql = $@"   select   tbc.ControllerName,tbc.ControllerPath,tba.*    from    TB_Action  tba    
             left  join   TB_Controller  tbc  on  tba.ControllerID=tbc.TABID
             where   tba.ControllerID   in 
             (select  TABID  from TB_Controller   where ModuleID = {key_ModuleID})  ";
                using (MProc proc = new MProc(sql, DBConnection.connectionString))
                {
                    table = proc.ExeMDataTable()?.ToDataTable();
                }

            }

            return table;



        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataTable   GetInfoApiKey(string  key)
        {
            string sql = $@"select     tbm.TABID tbmID,tbm.ModulePath,tbm.ModuleName,tbc.ControllerPath,tbc.TABID  as tcID,tbc.ControllerName,tba.*     from  
              tb_action    tba     
             left  join  TB_Controller    tbc  on  tbc.TABID=tba.ControllerID
             left join    TB_Module tbm  on tbc.ModuleID = tbm.TABID
              where  1=1  and(   tba.ActionName  like  '%{key}%'     or     tba.Summary like '%{key}%' )  ";
            DataTable table = new DataTable();
            using (MProc proc = new MProc(sql))
            {
                table = proc.ExeMDataTable()?.ToDataTable();
            }
            return table;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="object_ClassEntity"></param>
        /// <param name="keyValue"></param>



        public void SubmitForm(Controller_Entity object_ClassEntity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
               string sql = $@"INSERT INTO TB_Controller ( 
               
                ControllerName ,
                ControllerPath ,
                Summary ,
                CareateTime ,
                Remark ,
                ModuleID ) VALUES ('{object_ClassEntity.ControllerName}','{object_ClassEntity.ControllerPath}','{object_ClassEntity.Summary}','{DateTime.Now}','{object_ClassEntity.Remark}',{object_ClassEntity.ModuleID})";
              int res=   new MProc(sql, DBConnection.connectionString).ExeNonQuery();
            
            
            }
        }




        /// <summary>
        /// 删除路由
        /// </summary>
        /// <param name="keyValue"></param>

        public void DeleteRoute(string keyValue)
        {
            string sql = $@" DELETE
            FROM
            [TB_Controller]
            WHERE
            [TABID] = {int.Parse(keyValue)} ";

            int res = new MProc(sql, DBConnection.connectionString).ExeNonQuery();



        }
    }
}
