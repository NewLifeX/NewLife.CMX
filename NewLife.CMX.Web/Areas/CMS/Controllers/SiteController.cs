using System.ComponentModel;
using NewLife.Cube;

namespace NewLife.CMX.Web.Controllers
{
    /// <summary>站点配置</summary>
    [DisplayName("站点配置")]
    public class SiteController : ConfigController<SiteConfig>
    {
        static SiteController()
        {
            var set = SiteConfig.Current;
            if (set.IsNew) set.Save();
        }
    }
}
