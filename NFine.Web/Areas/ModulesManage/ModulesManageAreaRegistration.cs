using System.Web.Mvc;

namespace NFine.Web.Areas.ModulesManage
{
    public class ModulesManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ModulesManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ModulesManage_default",
                "ModulesManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
