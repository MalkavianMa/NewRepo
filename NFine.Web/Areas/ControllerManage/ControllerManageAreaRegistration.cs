using System.Web.Mvc;

namespace NFine.Web.Areas.ControllerManage
{
    public class ControllerManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ControllerManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ControllerManage_default",
                "ControllerManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
