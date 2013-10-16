//修正jQuery中调用的jquery核心库中的方法，由于jquery核心库1.4+的版本中都将该方法取消
//jQuery.form 中还存在一个bug 是在ajaxsubmit中只能捕获JS中的异常，无法正常捕获http请求异常
//就是说除非是js中有异常，否则返回的请求中无论HTTP的statusCode 是多少都认为是请求成功

$.handleError = function (s, xhr, status, e) {
    // If a local callback was specified, fire it
    if (s.error) {
        s.error.call(s.context || window, xhr, status, e);
    }

    // Fire the global callback
    if (s.global) {
        (s.context ? jQuery(s.context) : jQuery.event).trigger("ajaxError", [xhr, s, e]);
    }
}

$.httpData = function (xhr, type, s) {
    var ct = xhr.getResponseHeader("content-type") || "",
        xml = type === "xml" || !type && ct.indexOf("xml") >= 0,
        data = xml ? xhr.responseXML : xhr.responseText;

    if (xml && data.documentElement.nodeName === "parsererror") {
        throw "parsererror";
    }

    // Allow a pre-filtering function to sanitize the response
    // s is checked to keep backwards compatibility
    if (s && s.dataFilter) {
        data = s.dataFilter(data, type);
    }

    // The filter can actually parse the response
    if (typeof data === "string") {
        // Get the JavaScript object, if JSON is used.
        if (type === "json" || !type && ct.indexOf("json") >= 0) {
            // Make sure the incoming data is actual JSON
            // Logic borrowed from http://json.org/json2.js
            if (/^[\],:{}\s]*$/.test(data.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, "@")
                .replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, "]")
                .replace(/(?:^|:|,)(?:\s*\[)+/g, ""))) {

                // Try to use the native JSON parser first
                if (window.JSON && window.JSON.parse) {
                    data = window.JSON.parse(data);

                } else {
                    data = (new Function("return " + data))();
                }

            } else {
                throw "Invalid JSON: " + data;
            }

            // If the type is "script", eval it in global context
        } else if (type === "script" || !type && ct.indexOf("javascript") >= 0) {
            jQuery.globalEval(data);
        }
    }

    return data;
}
