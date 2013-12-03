using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using NewLife.Log;
using NewLife.Reflection;
using NewLife.Serialization.Json;
using XCode;

namespace NewLife.CMX.Tool
{
    public class HelperTool
    {
        /// <summary>
        /// 保存模板内容
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

                IEntity ientity = ieo.Create();
                try
                {
                    //ientity["ParentID"] = (Int32)entity["ID"];
                    //ientity.Dirtys["ParentID"] = true;
                    //ientity["Content"] = entity["ConentTxt"].ToString();
                    //ientity.Dirtys["Content"] = true;
                    //ientity["Title"] = entity["Title"].ToString();
                    //ientity.Dirtys["Title"] = true;
                    //ientity["Version"] = Version;
                    //ientity.Dirtys["Version"] = true;
                    ientity.SetItem("ParentID", (Int32)entity["ID"]);
                    //如果contentTxt为空的时候，在访问该属性的时候会触发基类中的扩展属性。
                    //由于扩展属性中内容属性为了防止出错，在查询完成会自动恢复数据连接。
                    ientity.SetItem("Content", entity["ConentTxt"].ToString());
                    ientity.SetItem("Title", entity["Title"].ToString());
                    ientity.SetItem("Version", Version);
                    //为了防止用户没有添加数据内容而导致数据表连接被重置的情况发生
                    //修改表的连接操作放在数据保存前
                    ieo.TableName += Suffix;
                    ientity.Save();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    ieo.TableName = "";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="Version"></param>
        /// <param name="Suffix"></param>
        /// <param name="entity"></param>
        /// <param name="action"></param>
        public static void SaveModelProductContent(Type entityType, Int32 Version, String Suffix, EntityBase entity, Action<Type, Int32, String, EntityBase> action)
        {
            if (action != null)
            {
                action(entityType, Version, Suffix, entity);
            }
            else
            {
                IEntityOperate ieo = EntityFactory.CreateOperate(entityType);

                IEntity ientity = ieo.Create();
                try
                {
                    ientity.SetItem("ParentID", (Int32)entity["ID"]);
                    //如果contentTxt为空的时候，在访问该属性的时候会触发基类中的扩展属性。
                    //由于扩展属性中内容属性为了防止出错，在查询完成会自动恢复数据连接。
                    ientity.SetItem("Content", entity["ConentTxt"].ToString());
                    ientity.SetItem("Specification", entity["ProductGG"].ToString());
                    ientity.SetItem("Feature", entity["ProductTD"].ToString());
                    ientity.SetItem("App", entity["ProductYY"].ToString());
                    ientity.SetItem("Fitting", entity["ProductPJ"].ToString());
                    ientity.SetItem("Video", entity["ProductSP"].ToString());
                    ientity.SetItem("Title", entity["Title"].ToString());
                    ientity.SetItem("Version", Version);
                    //为了防止用户没有添加数据内容而导致数据表连接被重置的情况发生
                    //修改表的连接操作放在数据保存前
                    ieo.TableName += Suffix;
                    ientity.Save();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    ieo.TableName = "";
                }
            }
        }

        /// <summary>
        /// 上传照片
        /// </summary>
        /// <param name="file"></param>
        /// <param name="UploadPath"></param>
        /// <param name="FailFile"></param>
        /// <returns></returns>
        public static Dictionary<String, String> UpLoadImage(HttpFileCollection file, String RootPath, String UploadPath, out List<String> FailFile)
        {
            String FilePath, UploadFile;
            FailFile = new List<string>();
            Dictionary<String, String> Dic = new Dictionary<string, string>();
            Random r = new Random();

            for (int i = 0; i < file.Count; i++)
            {
                try
                {
                    if (String.IsNullOrEmpty(file[i].FileName)) throw new Exception("文件名称不能为空！");

                    FileInfo fi = new FileInfo(file[i].FileName);
                    String ex = fi.Extension;

                    while (true)
                    {
                        FilePath = DateTime.Now.Date.ToString("yyyyMMdd") + "N" + r.Next() + ex;
                        FilePath = Path.Combine(UploadPath, FilePath);

                        UploadFile = Path.Combine(RootPath, FilePath);
                        if (!File.Exists(UploadFile)) break;
                    }
                    file[i].SaveAs(UploadFile);

                    //将 \ 改为 /  url识别,设置设置路径为相对根目录
                    //设置文件为相对路径
                    Dic.Add(file[i].FileName, "/".CombinePath(FilePath.Replace('\\', '/')));
                    //Dic.Add(file[i].FileName, Directory.GetCurrentDirectory() + FilePath.Replace('\\', '/'));
                }
                catch (Exception ex)
                {
                    FailFile.Add(file[i].FileName);
                    XTrace.WriteLine(ex.Message);
                }
            }
            return Dic;
        }

        /// <summary>
        /// 将字典格式化为json字符串
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static String DicToJson(Dictionary<String, String> dic)
        {
            SimpleJsonUtil sj = new SimpleJsonUtil();

            List<SimpleJson> list = new List<SimpleJson>();

            foreach (KeyValuePair<String, String> item in dic)
            {
                list.Add(sj.Object("key", item.Key, "value", item.Value));
            }

            return sj.To(sj.Array(list.ToArray()));
        }


        /// <summary>
        /// 获取完整的路径
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static String GetFullPath(String strPath)
        {

            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            strPath = strPath.Replace("/", "\\");
            if (strPath.StartsWith("\\"))
            {
                strPath = strPath.TrimStart('\\');
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }

        /// <summary>
        /// 获取请求地址
        /// </summary>
        /// <returns></returns>
        public static String GetReques()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

    }
}
