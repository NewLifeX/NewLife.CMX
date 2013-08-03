using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Collections;

namespace NewLife.CMX.Editor
{
    public class UEUploader
    {
        string state = "SUCCESS";

        string URL = null;
        string currentType = null;
        string uploadpath = null;
        string filename = null;
        string originalName = null;
        HttpPostedFile uploadFile = null;

        /// <summary>
        /// 上传文件的主处理方法
        /// </summary>
        /// <param name="cxt"></param>
        /// <param name="pathbase"></param>
        /// <param name="filetype"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Hashtable upFile(HttpContext cxt, string pathbase, string[] filetype, int size)
        {
            String SVPath =  DateTime.Now.ToString("yyyy-MM-dd") + "/";
            uploadpath = cxt.Server.MapPath(pathbase+SVPath);//获取文件上传路径

            try
            {
                uploadFile = cxt.Request.Files[0];
                originalName = uploadFile.FileName;
                //目录创建
                createFolder();

                //格式验证
                if (checkType(filetype))
                {
                    state = "不允许的文件类型";
                }
                //大小验证
                if (checkSize(size))
                {
                    state = "文件大小超出网站限制";
                }
                //保存图片
                if (state == "SUCCESS")
                {
                    filename = reName();
                    uploadFile.SaveAs(uploadpath + filename);
                    URL = SVPath + filename;
                }
            }
            catch (Exception)
            {
                state = "未知错误";
                URL = "";
            }
            return getUploadInfo();
        }

        /// <summary>
        /// 上传涂鸦的主处理方法
        /// </summary>
        /// <param name="cxt"></param>
        /// <param name="pathbase"></param>
        /// <param name="tmppath"></param>
        /// <param name="base64Data"></param>
        /// <returns></returns>
        public Hashtable upScrawl(HttpContext cxt, string pathbase, string tmppath, string base64Data)
        {
            String SVPath = DateTime.Now.ToString("yyyy-MM-dd") + "/";
            uploadpath = cxt.Server.MapPath(pathbase + SVPath);//获取文件上传路径
            FileStream fs = null;
            try
            {
                //创建目录
                createFolder();
                //生成图片
                filename = System.Guid.NewGuid() + ".png";
                fs = File.Create(uploadpath + filename);
                byte[] bytes = Convert.FromBase64String(base64Data);
                fs.Write(bytes, 0, bytes.Length);

                URL = SVPath + filename;
            }
            catch (Exception)
            {
                state = "未知错误";
                URL = "";
            }
            finally
            {
                fs.Close();
                deleteFolder(cxt.Server.MapPath(tmppath));
            }
            return getUploadInfo();
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="cxt"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string getOtherInfo(HttpContext cxt, string field)
        {
            string info = null;
            if (cxt.Request.Form[field] != null && !String.IsNullOrEmpty(cxt.Request.Form[field]))
            {
                info = field == "fileName" ? cxt.Request.Form[field].Split(',')[1] : cxt.Request.Form[field];
            }
            return info;
        }

        /// <summary>
        /// 获取上传信息
        /// </summary>
        /// <returns></returns>
        private Hashtable getUploadInfo()
        {
            Hashtable infoList = new Hashtable();

            infoList.Add("state", state);
            infoList.Add("url", URL);

            if (currentType != null)
                infoList.Add("currentType", currentType);
            if (originalName != null)
                infoList.Add("originalName", originalName);
            return infoList;
        }

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <returns></returns>
        private string reName()
        {
            return System.Guid.NewGuid() + getFileExt();
        }
        /// <summary>
        /// 文件类型检测
        /// </summary>
        /// <param name="filetype"></param>
        /// <returns></returns>
        private bool checkType(string[] filetype)
        {
            currentType = getFileExt();
            return Array.IndexOf(filetype, currentType) == -1;
        }
        /// <summary>
        /// 文件大小检测
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private bool checkSize(int size)
        {
            return uploadFile.ContentLength >= (size * 1024 );
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <returns></returns>
        private string getFileExt()
        {
            string[] temp = uploadFile.FileName.Split('.');
            return "." + temp[temp.Length - 1].ToLower();
        }

        /// <summary>
        /// 按照日期自动创建存储文件夹
        /// </summary>
        private void createFolder()
        {
            if (!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }
        }

        /// <summary>
        /// 删除存储文件夹
        /// </summary>
        /// <param name="path"></param>
        public void deleteFolder(string path)
        {
            //if (Directory.Exists(path))
            //{
            //    Directory.Delete(path, true);
            //}
        }
    }
}
