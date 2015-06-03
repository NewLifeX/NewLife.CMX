namespace NewLife.CMX.Web.Models.Content
{
    public class DetailModel
    {
        public CMX.Channel Channel { get; set; }
        public IEntityCategory Category { get; set; }
        public IEntityTitle Detail { get; set; }
    }
}