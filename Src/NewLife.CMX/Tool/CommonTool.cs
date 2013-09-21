using System;
using System.Collections.Generic;
using System.Text;
using NewLife.Reflection;
using XCode;

namespace NewLife.CMX.Tool
{
    public class CommonTool
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="Version"></param>
        /// <param name="Suffix"></param>
        /// <param name="ID"></param>
        /// <param name="Content"></param>
        /// <param name="Title"></param>
        /// <param name="action"></param>
        public static void SaveModelContent(Type entityType, Int32 Version, String Suffix, EntityBase entity, Action<Type, Int32, String, EntityBase> action)
        {
            if (action != null)
            {
                action(entityType, Version, Suffix, entity);
            }
            else
            {
                IEntityOperate ieo = EntityFactory.CreateOperate(entityType);
                ieo.TableName += Suffix;

                //ArticleContent ac = ieo.Create() as ArticleContent;
                IEntity ientity = ieo.Create();
                try
                {
                    //ac.ParentID = (Int32)entity["ID"];
                    //ac.Content = entity["ArticleConentTxt"].ToString();
                    //ac.Title = entity["Title"].ToString();

                    //ac.Version = Version;

                    //ac.Save();
                    ientity["ParentID"] = (Int32)entity["ID"];
                    ientity["Content"] = entity["ConentTxt"].ToString();
                    ientity["Title"] = entity["Title"].ToString();
                    ientity["Version"] = Version;
                    ientity.Save();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    ieo.TableName = "";
                }
            }
        }
    }
}
