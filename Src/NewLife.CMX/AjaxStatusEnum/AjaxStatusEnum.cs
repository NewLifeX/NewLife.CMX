using System;
using System.Collections.Generic;
using System.Text;

namespace NewLife.CMX
{
    public enum AjaxStatusEnum
    {
        /// <summary>无配置</summary>
        NoConfig = 0,

        /// <summary>无文件</summary>
        NoFile,

        /// <summary>上传失败</summary>
        Error,

        /// <summary>上传成功</summary>
        Success,
    }
}
