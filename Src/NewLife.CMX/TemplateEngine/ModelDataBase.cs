using System;
using XCode;

namespace NewLife.CMX.TemplateEngine
{
    public class ModelDataBase
    {
        private Int32 _ID;
        /// <summary>编号</summary>
        public Int32 ID { get { return _ID; } set { _ID = value; } }

        private String _Title;
        /// <summary>标题</summary>
        public String Title { get { return _Title; } set { _Title = value; } }

        private String _Content;
        /// <summary>内容</summary>
        public String Content { get { return _Content; } set { _Content = value; } }

        private String _CategoryTitle;
        /// <summary>分类</summary>
        public String CategoryTitle { get { return _CategoryTitle; } set { _CategoryTitle = value; } }

        private Int32 _Hit;
        /// <summary>点击</summary>
        public Int32 Hit { get { return _Hit; } set { _Hit = value; } }

        private DateTime _CreateTime;
        /// <summary>添加时间</summary>
        public DateTime CreateTime { get { return _CreateTime; } set { _CreateTime = value; } }

        private Decimal _Price;
        /// <summary>价格</summary>
        public Decimal Price { get { return _Price; } set { _Price = value; } }

        public ModelDataBase(String ModelName, String suffix)
        {
            IEntityOperate ef = EntityFactory.CreateOperate(ModelName);
            try
            {
                ef.TableName += suffix;
                IEntity entity = ef.Create();

                ID = (Int32)entity["ID"];
                Title = (String)entity["Title"];
                Content = (String)entity["Content"];
                CategoryTitle = (String)entity["CategoryTitle"];
                Hit = (Int32)entity["Hit"];
                CreateTime = (DateTime)entity["CreateTime"];
                Price = (Decimal)entity["Price"];
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                ef.TableName = "";
            }
        }
    }
}