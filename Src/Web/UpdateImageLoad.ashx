<%@ WebHandler Language="C#" Class="UpdateImageLoad" %>

using System;
using System.Web;
using NewLife.Configuration;
using NewLife.CMX;
using System.IO;
using System.Collections.Generic;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX.Tool;

public class UpdateImageLoad : IHttpHandler
{
    private String _DefaultImagePath;
    /// <summary>用户自定义保存路径</summary>
    public String DefaultImagePath
    {
        get
        {
            if (_DefaultImagePath == null)
            {
                _DefaultImagePath = Config.GetConfig<String>("DefaultImagePath");
            }
            return _DefaultImagePath;
        }
        set { _DefaultImagePath = value; }
    }

    public void ProcessRequest(HttpContext context)
    {
        Dictionary<String, String> ResultDic = new Dictionary<string, string>();

        try
        {
            //如果设置了ContentType为text/plain返回值中为了区别类型会自动添加<pre>标签
            //context.Response.ContentType = "text/plain";
            // 获取/创建上传路径
            String CustomImagePath = context.Request["CustomImagePath"];
            String RootPath = context.Request.PhysicalApplicationPath;
            String UploadPath = "";
            List<String> FailFile;
            UploadPath = String.IsNullOrEmpty(CustomImagePath) ? DefaultImagePath : CustomImagePath;

            if (String.IsNullOrEmpty(UploadPath))
            {
                throw new Exception((Int32)AjaxStatusEnum.NoConfig + "-无法获取到配置信息！");
            }
            else
            {
                //UploadPath = Path.Combine(RootPath, UploadPath);
                if (!Directory.Exists(UploadPath)) Directory.CreateDirectory(Path.Combine(RootPath, UploadPath));
            }

            //获取提交的文件集合
            HttpFileCollection file = context.Request.Files;

            if ((file.Count == 1 && String.IsNullOrEmpty(file[0].FileName)) || file.Count < 1)
            {
                throw new Exception((Int32)AjaxStatusEnum.NoFile + "-请选择上传文件！");
            }

            //获取到上传成功的文件相对
            Dictionary<String, String> filePathDic = CommonTool.UpLoadImage(file, RootPath, UploadPath, out FailFile);
            //部分文件上传失败
            if (FailFile.Count > 0)
            {
                throw new Exception((Int32)AjaxStatusEnum.Error + "-上传失败的文件列表，" + String.Join(",", FailFile.ToArray()));
            }
            //context.Response.Write(AjaxStatusEnum.Success);
            ResultDic.Add("StatusCode", "200");
            ResultDic.Add("Result", (AjaxStatusEnum.Success).ToString());
            //将上传成功的文件的相对路径添加到返回参数中
            foreach (KeyValuePair<String, String> item in filePathDic)
            {
                ResultDic.Add(item.Key, item.Value);
            }
        }
        catch (Exception ex)
        {
            XTrace.WriteLine(ex.Message);
            context.Response.StatusCode = 500;
            ResultDic.Add("StatusCode", "500");

            String ajaxstatusenumCode = ex.Message.Substring(0, 1);
            Int32 i = 0;
            Int32.TryParse(ajaxstatusenumCode, out i);

            switch (i)
            {
                case (Int32)AjaxStatusEnum.Error:
                    //context.Response.Write(Enum.GetName(typeof(AjaxStatusEnum), AjaxStatusEnum.Error) + ex.Message.Substring(1));
                    ResultDic.Add("Result", (AjaxStatusEnum.Error).ToString());
                    ResultDic.Add("Message", ex.Message.Substring(2));
                    break;
                case (Int32)AjaxStatusEnum.NoConfig:
                    //context.Response.Write(Enum.GetName(typeof(AjaxStatusEnum), AjaxStatusEnum.NoConfig) + ex.Message.Substring(1));
                    ResultDic.Add("Result", (AjaxStatusEnum.NoConfig).ToString());
                    ResultDic.Add("Message", ex.Message.Substring(2));
                    break;
                case (Int32)AjaxStatusEnum.NoFile:
                    //context.Response.Write(Enum.GetName(typeof(AjaxStatusEnum), AjaxStatusEnum.NoFile) + ex.Message.Substring(1));
                    ResultDic.Add("Result", (AjaxStatusEnum.NoFile).ToString());
                    ResultDic.Add("Message", ex.Message.Substring(2));
                    break;
                default:
                    ResultDic.Add("Result", (AjaxStatusEnum.Error).ToString());
                    ResultDic.Add("Message", ex.Message);
                    break;
            }
        }
        finally
        {
            context.Response.Write(CommonTool.DicToJson(ResultDic));
            context.Response.End();
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }

    ///// <summary>
    ///// 上传照片
    ///// </summary>
    ///// <param name="file"></param>
    ///// <param name="UploadPath"></param>
    ///// <param name="FailFile"></param>
    ///// <returns></returns>
    //public static Dictionary<String, String> UpLoadImage(HttpFileCollection file, String UploadPath, out List<String> FailFile)
    //{
    //    Int32 count = 0;
    //    String FilePath;
    //    FailFile = new List<string>();
    //    Dictionary<String, String> Dic = new Dictionary<string, string>();
    //    Random r = new Random();

    //    for (int i = 0; i < file.Count; i++)
    //    {
    //        try
    //        {
    //            if (String.IsNullOrEmpty(file[i].FileName)) throw new Exception("文件名称不能为空！");

    //            FileInfo fi = new FileInfo(file[i].FileName);
    //            String ex = fi.Extension;

    //            while (true)
    //            {
    //                FilePath = DateTime.Now.Date.ToString("yyyyMMdd") + "N" + r.Next() + ex;

    //                UploadPath = Path.Combine(UploadPath, FilePath);
    //                if (!File.Exists(UploadPath)) break;
    //            }
    //            file[i].SaveAs(UploadPath);
    //            Dic.Add(file[i].FileName, FilePath);
    //            count += 1;
    //        }
    //        catch (Exception ex)
    //        {
    //            FailFile.Add(file[i].FileName);
    //            XTrace.WriteLine(ex.Message);
    //        }
    //    }
    //    return Dic;
    //}
}