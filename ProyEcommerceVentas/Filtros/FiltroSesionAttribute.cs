using System.Web;
using System.Web.Mvc;

namespace TuProyectoWeb.Filtros
{
    public class FiltroSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["usuario"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}