using System;
using System.Collections.Generic;

namespace NewLife.CMX.Web.Models.Content
{
    public class CategoryModel
    {
        public Channel Channel { get; set; }
        public IEntityCategory Category { get; set; }
        public Int32 PageIndex { get; set; }
        public Int32 PageSize { get; set; }
        public Boolean Loaded { get; set; }
        public IEnumerable<IEntityTitle> Titles { get; set; }
        public Int32 RecordCount { get; set; }
        public Int32 PageCount { get; set; }
        public void LoadData()
        {
            this.Loaded = true;
            this.Titles = this.Category.GetTitles(this.PageIndex, this.PageSize);
            this.RecordCount = this.Category.GetTitleCount();
            this.PageCount = this.RecordCount / this.PageSize;
            if (this.RecordCount % this.PageSize != 0)
            {
                this.PageCount++;
            }
        }
    }
}