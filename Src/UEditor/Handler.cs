﻿using System;
using System.Web;
using NewLife.Serialization;

namespace UEditor
{

    /// <summary>处理器基类</summary>
    public abstract class Handler
    {
        public Handler(HttpContext context)
        {
            Request = context.Request;
            Response = context.Response;
            Context = context;
            Server = context.Server;
        }

        public abstract void Process();

        protected void WriteJson(Object response)
        {
            var jsonpCallback = Request["callback"];
            var json = response.ToJson();
            if (String.IsNullOrWhiteSpace(jsonpCallback))
            {
                Response.AddHeader("Content-Type", "text/plain");
                Response.Write(json);
            }
            else
            {
                Response.AddHeader("Content-Type", "application/javascript");
                Response.Write(String.Format("{0}({1});", jsonpCallback, json));
            }
            Response.End();
        }

        public HttpRequest Request { get; private set; }
        public HttpResponse Response { get; private set; }
        public HttpContext Context { get; private set; }
        public HttpServerUtility Server { get; private set; }
    }
}