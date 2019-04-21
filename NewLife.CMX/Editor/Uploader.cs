using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using NewLife.Log;

namespace NewLife.CMX.Editor
{
    /// <summary>文件上传处理器</summary>
    public class Uploader
    {
        /// <summary>上传文件</summary>
        public HttpPostedFileBase File { get; set; }

        /// <summary>上传文件内容</summary>
        public Byte[] FileBytes { get; set; }

        /// <summary>文件命名规则</summary>
        public String PathFormat { get; set; }

        /// <summary>上传大小限制</summary>
        public Int32 MaxSize { get; set; }

        /// <summary>上传允许的文件格式</summary>
        public String[] AllowExtensions { get; set; }

        /// <summary>Base64 字符串所表示的文件名</summary>
        public String Base64Filename { get; set; }

        /// <summary>处理</summary>
        /// <returns></returns>
        public virtual Object Process()
        {
            Byte[] uploadFileBytes = null;
            String uploadFileName = null;
            var rs = new UploadResult() { State = UploadState.Unknown };

            var file = File;
            if (file == null)
            {
                uploadFileName = Base64Filename;
                uploadFileBytes = FileBytes;
            }
            else
            {
                uploadFileName = file.FileName;

                if (!CheckFileType(uploadFileName))
                {
                    rs.State = UploadState.TypeNotAllow;
                    return GetResult(rs);
                }
                if (file.ContentLength > MaxSize)
                {
                    rs.State = UploadState.SizeLimitExceed;
                    return GetResult(rs);
                }

                uploadFileBytes = new Byte[file.ContentLength];
                try
                {
                    file.InputStream.Read(uploadFileBytes, 0, file.ContentLength);
                }
                catch (Exception)
                {
                    rs.State = UploadState.NetworkError;
                    return GetResult(rs);
                }
            }

            rs.OriginFileName = uploadFileName;

            var savePath = Format(uploadFileName, PathFormat);
            var localPath = savePath.TrimStart("/", "\\").GetFullPath();
            try
            {
                localPath.EnsureDirectory(true);
                System.IO.File.WriteAllBytes(localPath, uploadFileBytes);
                rs.Url = savePath;
                rs.State = UploadState.Success;
            }
            catch (Exception e)
            {
                rs.State = UploadState.FileAccessError;
                rs.ErrorMessage = e.Message;
            }

            return GetResult(rs);
        }

        private Object GetResult(UploadResult rs)
        {
            return new
            {
                state = GetStateMessage(rs.State),
                url = rs.Url,
                title = rs.OriginFileName,
                original = rs.OriginFileName,
                error = rs.ErrorMessage
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
            var exts = AllowExtensions;
            if (exts == null) return false;

            var fileExtension = Path.GetExtension(filename).ToLower();
            return exts.Select(x => x.ToLower()).Contains(fileExtension);
        }

        internal static String Format(String originFileName, String pathFormat)
        {
            if (String.IsNullOrWhiteSpace(pathFormat)) pathFormat = "{filename}{rand:6}";

            var invalidPattern = new Regex(@"[\\\/\:\*\?\042\<\>\|]");
            originFileName = invalidPattern.Replace(originFileName, "");

            var extension = Path.GetExtension(originFileName);
            var filename = Path.GetFileNameWithoutExtension(originFileName);

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

    class UploadResult
    {
        public UploadState State { get; set; }
        public String Url { get; set; }
        public String OriginFileName { get; set; }

        public String ErrorMessage { get; set; }
    }

    enum UploadState
    {
        Success = 0,
        SizeLimitExceed = -1,
        TypeNotAllow = -2,
        FileAccessError = -3,
        NetworkError = -4,
        Unknown = 1,
    }
}