using System;
using NewLife.Cube;

namespace NewLife.CMX.Web.Controllers
{
    public class CategoryController : EntityTreeController<Category>
    {
        static CategoryController()
        {
            // 过滤掉一些字段
            var list = ListFields;
            list.RemoveAll(e => e.Name.EqualIgnoreCase("ModelID", "ModelName"));

            FormFields.RemoveField("Remark");
        }
    }
}