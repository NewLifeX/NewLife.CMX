﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace UEditor
{
    /// <summary>抓取处理器</summary>
    public class CrawlerHandler : Handler
    {
        private String[] Sources;
        private Crawler[] Crawlers;
        public CrawlerHandler(HttpContext context) : base(context) { }

        public override void Process()
        {
            Sources = Request.Form.GetValues("source[]");
            if (Sources == null || Sources.Length == 0)
            {
                WriteJson(new
                {
                    state = "参数错误：没有指定抓取源"
                });
                return;
            }
            Crawlers = Sources.Select(x => new Crawler(x, Server).Fetch()).ToArray();
            WriteJson(new
            {
                state = "SUCCESS",
                list = Crawlers.Select(x => new
                {
                    state = x.State,
                    source = x.SourceUrl,
                    url = x.ServerUrl
                })
            });
        }
    }

    public class Crawler
    {
        public String SourceUrl { get; set; }
        public String ServerUrl { get; set; }
        public String State { get; set; }

        private HttpServerUtility Server { get; set; }


        public Crawler(String sourceUrl, HttpServerUtility server)
        {
            SourceUrl = sourceUrl;
            Server = server;
        }

        public Crawler Fetch()
        {
            var request = HttpWebRequest.Create(SourceUrl) as HttpWebRequest;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    State = "Url returns " + response.StatusCode + ", " + response.StatusDescription;
                    return this;
                }
                if (response.ContentType.IndexOf("image") == -1)
                {
                    State = "Url is not an image";
                    return this;
                }
                ServerUrl = PathFormatter.Format(Path.GetFileName(SourceUrl), Config.GetString("catcherPathFormat"));
                var savePath = ServerUrl.GetFullPath().EnsureDirectory();
                try
                {
                    var stream = response.GetResponseStream();
                    var reader = new BinaryReader(stream);
                    Byte[] bytes;
                    using (var ms = new MemoryStream())
                    {
                        Byte[] buffer = new Byte[4096];
                        Int32 count;
                        while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            ms.Write(buffer, 0, count);
                        }
                        bytes = ms.ToArray();
                    }
                    File.WriteAllBytes(savePath, bytes);
                    State = "SUCCESS";
                }
                catch (Exception e)
                {
                    State = "抓取错误：" + e.Message;
                }
                return this;
            }
        }
    }
}