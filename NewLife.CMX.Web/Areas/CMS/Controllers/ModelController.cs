using NewLife.Cube;

namespace NewLife.CMX.Web.Controllers
{
    public class ModelController : EntityController<Model>
    {
        static ModelController()
        {
            var list = ListFields;
            list.RemoveCreateField();
            list.RemoveField("UpdateUserID");
            list.RemoveField("Remark");
        }
    }
}