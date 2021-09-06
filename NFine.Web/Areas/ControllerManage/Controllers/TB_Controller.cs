using CYQ.Data.Tool;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NFine.Application.ControllerManage;
using NFine.Code;
using NFine.Code.Extend;
using NFine.Domain.Entity.ControllerManage.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.ControllerManage.Controllers
{
    public class TB_Controller : ControllerBase
    {
        private ControllerManageApp controllerManageApp = new ControllerManageApp();
        //
        // GET: /ControllerManage/TB_/

        public override ActionResult Index()
        {
            return View();
        }



        public override ActionResult Form()
        {
            return View();
        }

       
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SubmitForm(Controller_Entity object_ClassEntity, string keyValue)
        {



            //插入到TB_Controller
            controllerManageApp.SubmitForm(object_ClassEntity,keyValue);

            return Success("操作成功。");


        }

        [HttpGet]
        //[HandlerAjaxOnly]

        public ActionResult GetControllerInfo( string keyValue)
        {
             string result = "";
            //  result = controllerManageApp.GetForm(keyValue);
  
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();

            object o = JsonSwagger(keyValue);
            result = JsonConvert.SerializeObject(o, Formatting.Indented, timeConverter);//JsonSwagger(dataTable) ;
            return Content(result);

        }


        [HttpGet]
        public ActionResult GetSearchInfo(string searchValue)
        {
            string result = "";
 
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();

            object o = JsonSwaggerSearch(searchValue);
            result = JsonConvert.SerializeObject(o, Formatting.Indented, timeConverter);//JsonSwagger(dataTable) ;
            return Content(result);

        }

        /// <summary>
        /// 将dataTable转换成SwageerJson
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private object JsonSwagger(string keyValue)
        {

            //查询模块信息的dataTable
            DataTable tableModule = controllerManageApp.GetFormData_Module(keyValue);

            //控制器信息的dataTable
            DataTable dataTable = controllerManageApp.GetFormDataTable(keyValue);

            //查询api信息的DataTable
            DataTable tableApiInfo = controllerManageApp.GetFormDataTable_Api(keyValue);

            dynamic infoMain = new ExpandoObject();
            dynamic tags = new ExpandoObject();
            // dynamic paths = new ExpandoObject();
            dynamic paths = new ExpandoObject();
            List<dynamic> tagsList = new List<dynamic>();

            dynamic infoDetail = new ExpandoObject();


            //接口请求类型实体
            dynamic apiType = new ExpandoObject();
            //接口信息实体
            dynamic apiInfo = new ExpandoObject();

            //接口参数信息实体
            dynamic paraApi = new ExpandoObject();
            List<dynamic> paraApiList = new List<dynamic>();


            //  List<int> controllerIDlists = new List<int>();

            string apiUrl = "";


            #region sw主题信息

            var title_var = tableModule?.AsEnumerable();
            //标题显示模块名
            infoDetail.title = title_var.First().Field<string>("ModuleName");//"汉化版Swagger-UI";


            infoMain.swagger = "2.0";
            infoMain.info = infoDetail;
            infoMain.host = "host";
            //模块路径
            infoMain.basePath = title_var.First().Field<string>("ModulePath");

            #endregion

            #region tags控制器路由信息

            foreach (DataRow item in dataTable.Rows)
            {
                tags = new ExpandoObject();
                if (item["ControllerName"]!=null)
                {
                    tags.name = Convert.ToString(item["ControllerName"]);
                    tags.description =   item["ControllerPath"] ?? "";
                    tagsList.Add(tags);
                    //controllerIDlists.Add(Convert.ToInt32(item["TABID"]));
                }

            }

            #region MyRegion
            // tags.name = "pet";
            // tags.description = "有关你宠物的所有事情！";
            //// tags.externalDocs.description = "Find out more";
            // ///   tags.externalDocs.url = "http://swagger.io";
            // tagsList.Add(tags);
            // tags = new ExpandoObject();
            // tags.name = "store";
            // tags.description = "访问宠物商店订单！";
            // //   tags.externalDocs.description = "Find out more";
            // //   tags.externalDocs.url = "http://swagger.io";
            // tagsList.Add(tags); 
            #endregion
            infoMain.tags = tagsList;
            #endregion


            #region paths接口信息
          //该字典用于直接操作paths的属性
            IDictionary<string, object> dict_path = paths;
          
            foreach (DataRow item in  tableApiInfo.Rows)
            {
                paraApiList = new List<dynamic>();//list一个入参一个出参
                apiType = new ExpandoObject();
                apiInfo = new ExpandoObject();
                #region 入参信息
                paraApi = new ExpandoObject();
                dynamic dy_Json = new ExpandoObject();

                IDictionary<string, object> dict_schema = dy_Json;
                dict_schema.Add("$ref", "Json");


                paraApi.schema = dy_Json;
                paraApi.@in = "body";
                paraApi.name = "入参";
                paraApi.description = Convert.ToString(item["Remark"]);
                paraApi.required = true;
                paraApi.@default = Convert.ToString(item["Parameters"]);


                paraApiList.Add(paraApi);

                #endregion

                #region 出参信息

                paraApi = new ExpandoObject();
                paraApi.schema = dy_Json;
                paraApi.@in = "body";
                paraApi.name = "出参";
                paraApi.description = Convert.ToString(item["Status"]);
                paraApi.required = true;
                paraApi.@default = Convert.ToString(item["ReturnResult"]);
                paraApiList.Add(paraApi);
                #endregion


    


                apiInfo.parameters = paraApiList;
                //枚举判断请求类型是POST还是Get
                //这个tags用于显示接口所在控制器名
                apiInfo.tags = new List<string> { Convert.ToString(item["ControllerName"]) };
                apiInfo.summary = Convert.ToString(item["Summary"]);
               // apiUrl = Request.Url.Scheme + @"://" + Request.Url.Authority;// Request.Url.Host;

                //http:localhost:1852/1/ces
                apiInfo.description =  Convert.ToString(item["ControllerPath"]) + @"/" + Convert.ToString(item["ActionName"]);//"http://localhost:1852/Configs/demo1.json"//
                 // apiInfo.developer = "bi";
                apiType.author = Convert.ToString(item["CreateTime"]);
                // apiType.operationId = "addPet";
                apiInfo.consumes = new List<string> { "application/json" };
                apiInfo.produces = new List<string> { "application/json" };
                
                //这个地方需要考虑同名请求的不同类型
                if (Convert.ToString(item["Method"])== "post")
                {
                    apiType.post = apiInfo;
                  //  apiType.get = "";
                }
                else
                {
                    apiType.get = apiInfo;
                   // apiType.post = "";

                }
               // IDictionary<string, object> dict_
                //接口信息附加到paths上
                dict_path.Add(Convert.ToString(item["ActionName"]), apiType);
            }

            //需要将多余的属性移除


          //  List<string> tagRoute = new List<string>();


            //接口路径替换



         
            //for (int i = 0; i < tagsList.Count; i++)
            //{
            //    //string myAttr = myList[i];
            //    //paths.PropertyName = tagsList[i].name;
            //    //   paths.Property = "测试" + i;
            // //   tagRoute.Add(tagsList[i].name);
            //    dict_path.Add(tagsList[i].name, apiType);
            //}




            infoMain.paths = paths;
            // var a = paths.store;
            // var b = paths.pet;
            #endregion

            #region 其他信息
            infoMain.schemes = "http";
            #endregion




            return infoMain;

        }




        /// <summary>
        /// 根据接口名查询接口
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>

        private object JsonSwaggerSearch(string keyValue)
        {
            //查询api信息的DataTable
            // DataTable tableApiInfo = controllerManageApp.GetFormDataTable_Api(keyValue);


            //查询模块信息的dataTable
            // DataTable tableModule = controllerManageApp.GetFormData_Module(keyValue);

            //控制器信息的dataTable
            // DataTable dataTable = controllerManageApp.GetFormDataTable(keyValue);

            DataTable infoJion = controllerManageApp.GetInfoApiKey(keyValue);



            dynamic infoMain = new ExpandoObject();
            dynamic tags = new ExpandoObject();
            // dynamic paths = new ExpandoObject();
            dynamic paths = new ExpandoObject();
            List<dynamic> tagsList = new List<dynamic>();

            dynamic infoDetail = new ExpandoObject();


            //接口请求类型实体
            dynamic apiType = new ExpandoObject();
            //接口信息实体
            dynamic apiInfo = new ExpandoObject();

            //接口参数信息实体
            dynamic paraApi = new ExpandoObject();
            List<dynamic> paraApiList = new List<dynamic>();


            //  List<int> controllerIDlists = new List<int>();

            string apiUrl = "";


            #region sw主题信息

            var title_var = infoJion?.AsEnumerable();
            //标题显示模块名
            infoDetail.title = title_var.First().Field<string>("ModuleName");//"汉化版Swagger-UI";


            infoMain.swagger = "2.0";
            infoMain.info = infoDetail;
            infoMain.host = "host";
            //模块路径
            infoMain.basePath = title_var.First().Field<string>("ModulePath");

            #endregion

            #region tags控制器路由信息

            foreach (DataRow item in infoJion.Rows)
            {
                tags = new ExpandoObject();
                if (item["ControllerName"] != null)
                {
                    tags.name = Convert.ToString(item["ControllerName"]);
                    tags.description = item["ControllerPath"] ?? "";
                    tagsList.Add(tags);
                    //controllerIDlists.Add(Convert.ToInt32(item["TABID"]));
                }

            }

            #region MyRegion
            // tags.name = "pet";
            // tags.description = "有关你宠物的所有事情！";
            //// tags.externalDocs.description = "Find out more";
            // ///   tags.externalDocs.url = "http://swagger.io";
            // tagsList.Add(tags);
            // tags = new ExpandoObject();
            // tags.name = "store";
            // tags.description = "访问宠物商店订单！";
            // //   tags.externalDocs.description = "Find out more";
            // //   tags.externalDocs.url = "http://swagger.io";
            // tagsList.Add(tags); 
            #endregion
            infoMain.tags = tagsList;
            #endregion


            #region paths接口信息
            //该字典用于直接操作paths的属性
            IDictionary<string, object> dict_path = paths;

            foreach (DataRow item in infoJion.Rows)
            {
                paraApiList = new List<dynamic>();//list一个入参一个出参
                apiType = new ExpandoObject();
                apiInfo = new ExpandoObject();
                #region 入参信息
                paraApi = new ExpandoObject();
                dynamic dy_Json = new ExpandoObject();

                IDictionary<string, object> dict_schema = dy_Json;
                dict_schema.Add("$ref", "Json");


                paraApi.schema = dy_Json;
                paraApi.@in = "body";
                paraApi.name = "入参";
                paraApi.description = Convert.ToString(item["Remark"]);
                paraApi.required = true;
                paraApi.@default = Convert.ToString(item["Parameters"]);


                paraApiList.Add(paraApi);

                #endregion

                #region 出参信息

                paraApi = new ExpandoObject();
                paraApi.schema = dy_Json;
                paraApi.@in = "body";
                paraApi.name = "出参";
                paraApi.description = Convert.ToString(item["Status"]);
                paraApi.required = true;
                paraApi.@default = Convert.ToString(item["ReturnResult"]);
                paraApiList.Add(paraApi);
                #endregion





                apiInfo.parameters = paraApiList;
                //枚举判断请求类型是POST还是Get
                //这个tags用于显示接口所在控制器名
                apiInfo.tags = new List<string> { Convert.ToString(item["ControllerName"]) };
                apiInfo.summary = Convert.ToString(item["Summary"]);
                // apiUrl = Request.Url.Scheme + @"://" + Request.Url.Authority;// Request.Url.Host;

                //http:localhost:1852/1/ces
                apiInfo.description = Convert.ToString(item["ControllerPath"]) + @"/" + Convert.ToString(item["ActionName"]);//"http://localhost:1852/Configs/demo1.json"//
                                                                                                                             // apiInfo.developer = "bi";
                apiType.author = Convert.ToString(item["CreateTime"]);
                // apiType.operationId = "addPet";
                apiInfo.consumes = new List<string> { "application/json" };
                apiInfo.produces = new List<string> { "application/json" };

                //这个地方需要考虑同名请求的不同类型
                if (Convert.ToString(item["Method"]) == "post")
                {
                    apiType.post = apiInfo;
                    //  apiType.get = "";
                }
                else
                {
                    apiType.get = apiInfo;
                    // apiType.post = "";

                }
                // IDictionary<string, object> dict_
                //接口信息附加到paths上
                dict_path.Add(Convert.ToString(item["ActionName"]), apiType);
            }

            //需要将多余的属性移除


            //  List<string> tagRoute = new List<string>();


            //接口路径替换




            //for (int i = 0; i < tagsList.Count; i++)
            //{
            //    //string myAttr = myList[i];
            //    //paths.PropertyName = tagsList[i].name;
            //    //   paths.Property = "测试" + i;
            // //   tagRoute.Add(tagsList[i].name);
            //    dict_path.Add(tagsList[i].name, apiType);
            //}




            infoMain.paths = paths;
            // var a = paths.store;
            // var b = paths.pet;
            #endregion

            #region 其他信息
            infoMain.schemes = "http";
            #endregion




            return infoMain;

        }





        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult DeleteRoute( string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                 return Error("操作失败，未选择任何有效数据");
            }
            controllerManageApp.DeleteRoute(keyValue);

            return Success("操作成功。");
        }
    
    
    }
}
