using System.Web;
using System.Web.Routing;

namespace UEditor
{
    public class RouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new UEditorHandler();
        }   
    }
}