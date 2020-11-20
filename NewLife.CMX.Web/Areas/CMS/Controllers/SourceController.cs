using System;
using Microsoft.AspNetCore.Mvc;
using NewLife.Cube;

namespace NewLife.CMX.Web.Controllers
{
    public class SourceController : EntityController<Source>
    {
        public ActionResult Get(Int32 id)
        {
            var entity = Source.FindByID(id);

            return Json(0, null, entity);
        }
    }
}