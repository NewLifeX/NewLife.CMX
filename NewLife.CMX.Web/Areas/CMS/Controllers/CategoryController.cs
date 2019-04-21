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
            list.RemoveCreateField();
            list.RemoveField("UpdateUserID");
            list.RemoveField("Remark");
        }
    }
}