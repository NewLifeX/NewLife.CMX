<%@ WebHandler Language="C#" Class="UpdateImageLoad" %>

using System;
using System.Web;
using NewLife.Configuration;
using NewLife.CMX;
using System.IO;
using System.Collections.Generic;
using NewLife.Log;

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
        try
        {
            context.Response.ContentType = "text/plain";

            // 获取/创建上传路径
            String CustomImagePath = context.Request["CustomImagePath"];
            String RootPath = context.Request.PhysicalApplicationPath;
            String UploadPath = "";
            List<String> FailFile;

            UploadPath = String.IsNullOrEmpty(CustomImagePath) ? CustomImagePath : DefaultImagePath;

            if (String.IsNullOrEmpty(UploadPath))
            {
                //context.Response.StatusCode = 500;
                //context.Response.Write(AjaxStatusEnum.NoConfig + "-Message:无法获取到配置信息！");
                //context.Response.End();
                throw new Exception(AjaxStatusEnum.NoConfig + "-Message:无法获取到配置信息！");
            }
            else
            {
                UploadPath = Path.Combine(RootPath, UploadPath);
                if (!Directory.Exists(UploadPath)) Directory.CreateDirectory(UploadPath);
            }

            //获取提交的文件集合
            HttpFileCollection file = context.Request.Files;

            if ((file.Count == 1 && String.IsNullOrEmpty(file[0].FileName)) || file.Count < 1)
            {
                //context.Response.StatusCode = 500;
                //context.Response.Write(AjaxStatusEnum.NoFile + "-Message:请选择上传文件！");
                //context.Response.End();
                throw new Exception(AjaxStatusEnum.NoFile + "-Message:请选择上传文件！");
            }

            //上传文件路径
            UpLoadImage(file, UploadPath, out FailFile);
            //部分文件上传失败
            if (FailFile.Count > 0)
            {
                //context.Response.StatusCode = 500;
                //context.Response.Write(AjaxStatusEnum.Error + "-Message:上传失败的文件列表，" + String.Join(",", FailFile.ToArray()));
                //context.Response.End();
                throw new Exception(AjaxStatusEnum.Error + "-Message:上传失败的文件列表，" + String.Join(",", FailFile.ToArray()));
            }
            context.Response.Write(AjaxStatusEnum.Success);
            context.Response.End();
        }
        catch (Exception ex)
        {
            XTrace.WriteLine(ex.Message);
            context.Response.StatusCode = 500;
            //context.Response.Write(AjaxStatusEnum.Error + "-Message:" + ex.Message);
            //String exmessage = ex.Message;

            String ajaxstatusenumCode = ex.Message.Substring(0, 1);
            Int32 i = 0;
            Int32.TryParse(ajaxstatusenumCode, out i);

            switch (i)
            {
                case (Int32)AjaxStatusEnum.Error:
                    context.Response.Write(Enum.GetName(typeof(AjaxStatusEnum), AjaxStatusEnum.Error) + ex.Message);
                    break;
                case (Int32)AjaxStatusEnum.NoConfig:
                    context.Response.Write(Enum.GetName(typeof(AjaxStatusEnum), AjaxStatusEnum.NoConfig) + ex.Message);
                    break;
                case (Int32)AjaxStatusEnum.NoFile:
                    context.Response.Write(Enum.GetName(typeof(AjaxStatusEnum), AjaxStatusEnum.NoFile) + ex.Message);
                    break;
                default:
                    break;
            }

            context.Response.End();
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }

    /// <summary>
    /// 上传照片
    /// </summary>
    /// <param name="file"></param>
    /// <param name="UploadPath"></param>
    /// <param name="FailFile"></param>
    /// <returns></returns>
    public static Int32 UpLoadImage(HttpFileCollection file, String UploadPath, out List<String> FailFile)
    {
        Int32 count = 0;
        String UpLoadFile = "";
        FailFile = new List<string>();

        foreach (HttpPostedFile item in file)
        {
            //记录未上传成功的文件名称
            try
            {
                //过滤对文件为空的文件
                if (!String.IsNullOrEmpty(item.FileName))
                    UpLoadFile = Path.Combine(UploadPath, item.FileName);
                item.SaveAs(UpLoadFile);
            }
            catch (Exception ex)
            {
                FailFile.Add(item.FileName);
                XTrace.WriteLine(ex.Message);
            }
            count += 1;
        }
        return count;
    }
}