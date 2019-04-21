using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace NewLife.CMX.Editor
{
    /// <summary>抓取处理器</summary>
    public class Crawler
    {
        public String[] Sources { get; set; }
        public String PathFormat { get; set; }

        public virtual Object Process()
        {
            if (Sources == null || Sources.Length == 0)
                return new { state = "参数错误：没有指定抓取源" };

            var crawlers = Sources.Select(x => new Crawler2(x) { PathFormat = PathFormat }.Fetch()).ToArray();
            return new
            {
                state = "SUCCESS",
                list = crawlers.Select(x => new
                {
                    state = x.State,
                    source = x.SourceUrl,
                    url = x.ServerUrl
                })
            };
        }
    }

    class Crawler2
    {
        public String SourceUrl { get; set; }
        public String ServerUrl { get; set; }
        public String PathFormat { get; set; }
        public String State { get; set; }

        public Crawler2(String sourceUrl)
        {
            SourceUrl = sourceUrl;
        }

        public Crawler2 Fetch()
        {
            if (!IsExternalIPAddress(SourceUrl))
            {
                State = "INVALID_URL";
                return this;
            }
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
                ServerUrl = Uploader.Format(Path.GetFileName(SourceUrl), PathFormat);
                var savePath = ServerUrl.GetFullPath();
                savePath.EnsureDirectory();
                //if (!Directory.Exists(Path.GetDirectoryName(savePath)))
                //{
                //    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                //}
                try
                {
                    var stream = response.GetResponseStream();
                    var reader = new BinaryReader(stream);
                    Byte[] bytes;
                    using (var ms = new MemoryStream())
                    {
                        var buffer = new Byte[4096];
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

        private Boolean IsExternalIPAddress(String url)
        {
            var uri = new Uri(url);
            switch (uri.HostNameType)
            {
                case UriHostNameType.Dns:
                    var ipHostEntry = Dns.GetHostEntry(uri.DnsSafeHost);
                    foreach (var ipAddress in ipHostEntry.AddressList)
                    {
                        var ipBytes = ipAddress.GetAddressBytes();
                        if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            if (!IsPrivateIP(ipAddress))
                            {
                                return true;
                            }
                        }
                    }
                    break;

                case UriHostNameType.IPv4:
                    return !IsPrivateIP(IPAddress.Parse(uri.DnsSafeHost));
            }
            return false;
        }

        private Boolean IsPrivateIP(IPAddress myIPAddress)
        {
            if (IPAddress.IsLoopback(myIPAddress)) return true;
            if (myIPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                var ipBytes = myIPAddress.GetAddressBytes();
                // 10.0.0.0/24 
                if (ipBytes[0] == 10)
                {
                    return true;
                }
                // 172.16.0.0/16
                else if (ipBytes[0] == 172 && ipBytes[1] == 16)
                {
                    return true;
                }
                // 192.168.0.0/16
                else if (ipBytes[0] == 192 && ipBytes[1] == 168)
                {
                    return true;
                }
                // 169.254.0.0/16
                else if (ipBytes[0] == 169 && ipBytes[1] == 254)
                {
                    return true;
                }
            }
            return false;
        }
    }
}