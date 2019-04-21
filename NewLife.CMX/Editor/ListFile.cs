using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NewLife.CMX.Editor
{
    /// <summary>
    /// FileManager 的摘要说明
    /// </summary>
    public class ListFile
    {
        enum ResultState
        {
            Success,
            InvalidParam,
            AuthorizError,
            IOError,
            PathNotFound
        }

        public Int32 Start { get; set; }
        public Int32 Size { get; set; }

        public String PathToList { get; set; }
        public String[] SearchExtensions { get; set; }
        public Int32 ListSize { get; set; }

        public virtual Object Process()
        {
            var state = ResultState.Success;

            if (Size <= 0) Size = ListSize;

            String[] fileList = null;
            var list = new List<String>();
            try
            {
                var localPath = PathToList.GetFullPath();
                list.AddRange(Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
                    .Where(x => SearchExtensions.Contains(Path.GetExtension(x).ToLower()))
                    .Select(x => PathToList + x.Substring(localPath.Length).Replace("\\", "/")));
                fileList = list.OrderBy(x => x).Skip(Start).Take(Size).ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                state = ResultState.AuthorizError;
            }
            catch (DirectoryNotFoundException)
            {
                state = ResultState.PathNotFound;
            }
            catch (IOException)
            {
                state = ResultState.IOError;
            }

            return new
            {
                state = GetStateString(state),
                list = fileList?.Select(x => new { url = x }),
                start = Start,
                size = Size,
                total = list.Count
            };
        }

        private String GetStateString(ResultState state)
        {
            switch (state)
            {
                case ResultState.Success: return "SUCCESS";
                case ResultState.InvalidParam: return "参数不正确";
                case ResultState.PathNotFound: return "路径不存在";
                case ResultState.AuthorizError: return "文件系统权限不足";
                case ResultState.IOError: return "文件系统读取错误";
            }
            return "未知错误";
        }
    }
}