using System.ComponentModel;
using NewLife.Cube;

namespace NewLife.CMX.Web.Controllers
{
    /// <summary>附件配置</summary>
    [DisplayName("附件配置")]
    public class AttachController : ConfigController<AttachConfig>
    {
        static AttachController()
        {
            var set = AttachConfig.Current;
            if (set.IsNew) set.Save();
        }
    }
}