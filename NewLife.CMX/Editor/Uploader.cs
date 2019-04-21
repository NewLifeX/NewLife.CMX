using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NewLife.CMX.Editor
{
    /// <summary>UploadHandler 的摘要说明</summary>
    public class Uploader
    {
        public UploadResult Result { get; private set; } = new UploadResult() { State = UploadState.Unknown };

        public HttpPostedFileBase File { get; set; }
        public Byte[] FileBytes { get; set; }

        /// <summary>文件命名规则</summary>
        public String PathFormat { get; set; }

        /// <summary>上传大小限制</summary>
        public Int32 SizeLimit { get; set; }

        /// <summary>上传允许的文件格式</summary>
        public String[] AllowExtensions { get; set; }

        /// <summary>Base64 字符串所表示的文件名</summary>
        public String Base64Filename { get; set; }

        public virtual Object Process()
        {
            Byte[] uploadFileBytes = null;
            String uploadFileName = null;

            var file = File;
            if (file == null)
            {
                uploadFileName = Base64Filename;
                uploadFileBytes = FileBytes;
            }
            else
            {
                //var file = Request.Files[Config.UploadFieldName];
                uploadFileName = file.FileName;

                if (!CheckFileType(uploadFileName))
                {
                    Result.State = UploadState.TypeNotAllow;
                    return GetResult();
                }
                if (file.ContentLength > SizeLimit)
                {
                    Result.State = UploadState.SizeLimitExceed;
                    return GetResult();
                }

                uploadFileBytes = new Byte[file.ContentLength];
                try
                {
                    file.InputStream.Read(uploadFileBytes, 0, file.ContentLength);
                }
                catch (Exception)
                {
                    Result.State = UploadState.NetworkError;
                    GetResult();
                }
            }

            Result.OriginFileName = uploadFileName;

            var savePath = Format(uploadFileName, PathFormat);
            var localPath = savePath.GetFullPath();
            try
            {
                localPath.EnsureDirectory();
                //if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                //{
                //    Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                //}
                System.IO.File.WriteAllBytes(localPath, uploadFileBytes);
                Result.Url = savePath;
                Result.State = UploadState.Success;
            }
            catch (Exception e)
            {
                Result.State = UploadState.FileAccessError;
                Result.ErrorMessage = e.Message;
            }

            return GetResult();
        }

        private Object GetResult()
        {
            return new
            {
                state = GetStateMessage(Result.State),
                url = Result.Url,
                title = Result.OriginFileName,
                original = Result.OriginFileName,
                error = Result.ErrorMessage
            };
        }

        private String GetStateMessage(UploadState state)
        {
            switch (state)
            {
                case UploadState.Success:
                    return "SUCCESS";
                case UploadState.FileAccessError:
                    return "文件访问出错，请检查写入权限";
                case UploadState.SizeLimitExceed:
                    return "文件大小超出服务器限制";
                case UploadState.TypeNotAllow:
                    return "不允许的文件格式";
                case UploadState.NetworkError:
                    return "网络错误";
            }
            return "未知错误";
        }

        private Boolean CheckFileType(String filename)
        {
            var fileExtension = Path.GetExtension(filename).ToLower();
            return AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }

        public static String Format(String originFileName, String pathFormat)
        {
            if (String.IsNullOrWhiteSpace(pathFormat))
            {
                pathFormat = "{filename}{rand:6}";
            }

            var invalidPattern = new Regex(@"[\\\/\:\*\?\042\<\>\|]");
            originFileName = invalidPattern.Replace(originFileName, "");

            String extension = Path.GetExtension(originFileName);
            String filename = Path.GetFileNameWithoutExtension(originFileName);

            pathFormat = pathFormat.Replace("{filename}", filename);
            pathFormat = new Regex(@"\{rand(\:?)(\d+)\}", RegexOptions.Compiled).Replace(pathFormat, new MatchEvaluator(delegate (Match match)
            {
                var digit = 6;
                if (match.Groups.Count > 2)
                {
                    digit = Convert.ToInt32(match.Groups[2].Value);
                }
                var rand = new Random();
                return rand.Next((Int32)Math.Pow(10, digit), (Int32)Math.Pow(10, digit + 1)).ToString();
            }));

            pathFormat = pathFormat.Replace("{time}", DateTime.Now.Ticks.ToString());
            pathFormat = pathFormat.Replace("{yyyy}", DateTime.Now.Year.ToString());
            pathFormat = pathFormat.Replace("{yy}", (DateTime.Now.Year % 100).ToString("D2"));
            pathFormat = pathFormat.Replace("{mm}", DateTime.Now.Month.ToString("D2"));
            pathFormat = pathFormat.Replace("{dd}", DateTime.Now.Day.ToString("D2"));
            pathFormat = pathFormat.Replace("{hh}", DateTime.Now.Hour.ToString("D2"));
            pathFormat = pathFormat.Replace("{ii}", DateTime.Now.Minute.ToString("D2"));
            pathFormat = pathFormat.Replace("{ss}", DateTime.Now.Second.ToString("D2"));

            return pathFormat + extension;
        }
    }

    public class UploadResult
    {
        public UploadState State { get; set; }
        public String Url { get; set; }
        public String OriginFileName { get; set; }

        public String ErrorMessage { get; set; }
    }

    public enum UploadState
    {
        Success = 0,
        SizeLimitExceed = -1,
        TypeNotAllow = -2,
        FileAccessError = -3,
        NetworkError = -4,
        Unknown = 1,
    }
}