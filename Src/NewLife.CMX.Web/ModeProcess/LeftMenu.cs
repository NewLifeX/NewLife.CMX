using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NewLife.CMX.Web.Base;
using NewLife.Reflection;

namespace NewLife.CMX.Web.ModeProcess
{
    public class LeftMenu : LeftMenuBase
    {
        public override string Process()
        {
            try
            {
                Channel c = Channel.FindByID(ChannelID);

                IModelProvider imp = c.Model.Provider;

                imp.CurrentChannel = c.ID;

                TypeX type = imp.CategoryType;
                //List<IEntityCategory> list = imp.CategoryType.InvokeMember("FindAllByParentID", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new Object[] { 0 }) as List<IEntityCategory>;
                var t = type.GetMethod("FindAll", null).Invoke("FindAll", null) as List<IEntityCategory>;

             
            }
            finally
            {

            }

            return null;
        }
    }
}
