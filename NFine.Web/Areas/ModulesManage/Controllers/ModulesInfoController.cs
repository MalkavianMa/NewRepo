using NFine.Application.ModulesManage;
using NFine.Code;
using NFine.ControllerManage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.ModulesManage.Controllers
{
    public class ModulesInfoController :   ControllerBase
    {


        private ModulesManageApp modulesApp = new ModulesManageApp();



        //
        // GET: /ModulesManage/ModulesInfo/
        // /ModulesManage/ModulesInfo/SubmitForm
        //public ActionResult Index()
        //{
        //    return View();
        //}


        //public ActionResult Form()
        //{
        //     return View();
        //}



        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SubmitForm(TB_Module_Entity object_ClassEntity, string keyValue)
        {



            //插入到TB_Module
            modulesApp.SubmitForm(object_ClassEntity, keyValue);

            return Success("操作成功。");


        }



         

        [HttpGet]
        [HandlerAjaxOnly]

        public ActionResult GetAllModulesInfo(Pagination pagination, string keyword)
        {

            return Content(modulesApp.GetList());

        }

        //GetSingleInfo
        [HttpGet]
        [HandlerAjaxOnly]

        public ActionResult GetSingleInfo(string keyValue)
        {

            return Content(modulesApp.GetSingleInfo(keyValue));

        }





        [HttpGet]
        [HandlerAjaxOnly]

        public ActionResult GetFormJson(Pagination pagination, string keyword)
        {

            return Content(modulesApp.GetList());

        }




        [HttpGet]
        [HandlerAjaxOnly]

        public ActionResult GetAllHomeInfo(Pagination pagination, string keyword)
        {






            return Content("");

        }





    }
}
