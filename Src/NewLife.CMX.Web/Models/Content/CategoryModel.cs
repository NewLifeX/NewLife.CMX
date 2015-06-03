using System;

namespace NewLife.CMX.Web.Models.Content
{
    public class CategoryModel
    {
        public CMX.Channel Channel { get; set; }
        public IEntityCategory Category { get; set; }
        public Int32 PageIndex { get; set; }
        public Int32 PageSize { get; set; }
    }
}