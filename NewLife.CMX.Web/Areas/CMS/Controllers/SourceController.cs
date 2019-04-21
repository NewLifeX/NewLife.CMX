using System;
using System.Web.Mvc;
using NewLife.Cube;

namespace NewLife.CMX.Web.Controllers
{
    public class SourceController : EntityController<Source>
    {
        public ActionResult Get(Int32 id)
        {
            var entity = Source.FindByID(id);

            return Json(entity, JsonRequestBehavior.AllowGet);
        }
    }
}