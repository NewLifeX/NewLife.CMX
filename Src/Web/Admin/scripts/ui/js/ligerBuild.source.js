/**
* jQuery ligerUI 1.0.2
* Author leoxie
* 部分修改版
* Author xiekai
*/
//ligerAccordion.js
if (typeof (LigerUIManagers) == "undefined") LigerUIManagers = {};
(function($)
{ 
    ///	<param name="$" type="jQuery"></param>

    $.fn.ligerGetAccordionManager = function()
    {
        return LigerUIManagers[this[0].id + "_Accordion"];
    };
    $.fn.ligerRemoveAccordionManager = function()
    {
        return this.each(function()
        {
            LigerUIManagers[this.id + "_Accordion"] = null;
        });
    };

    $.fn.ligerAccordion = function(p)
    { 
        this.each(function()
        {
            p = $.extend({
                height: null,
                speed : "normal",
                changeHeightOnResize: false,
                heightDiff: 0 // 高度补差  
            }, p || {});
            
            if (this.usedAccordion) return;
            var g = {
                onResize: function()
                {
                    if (!p.height || typeof (p.height) != 'string' || p.height.indexOf('%') == -1) return false;
                    //set accordion height
                    if (g.accordion.parent()[0].tagName.toLowerCase() == "body")
                    {
                        var windowHeight = $(window).height();
                        windowHeight -= parseInt(g.layout.parent().css('paddingTop'));
                        windowHeight -= parseInt(g.layout.parent().css('paddingBottom'));
                        g.height = p.heightDiff + windowHeight * parseFloat(g.height) * 0.01;
                    }
                    else
                    {
                        g.height = p.heightDiff + (g.accordion.parent().height() * parseFloat(p.height) * 0.01);
                    }
                    g.accordion.height(g.height);
                    g.setContentHeight(g.height - g.headerHoldHeight);
                },
                setHeight: function(height)
                {
                    g.accordion.height(height);
                    height -= g.headerHoldHeight;
                    $("> .l-accordion-content", g.accordion).height(height);
                }
            };
            g.accordion = $(this);
            if (!g.accordion.hasClass("l-accordion-panel")) g.accordion.addClass("l-accordion-panel");
            var selectedIndex = 0;
            if ($("> div[lselected=true]", g.accordion).length > 0)
                selectedIndex = $("> div", g.accordion).index($("> div[lselected=true]", g.accordion));

            $("> div", g.accordion).each(function(i, box)
            {
                var header = $('<div class="l-accordion-header"><div class="l-accordion-toggle"></div><div class="l-accordion-header-inner"></div></div>');
                if (i == selectedIndex)
                    $(".l-accordion-toggle", header).addClass("l-accordion-toggle-open");
                if ($(box).attr("title"))
                {
					var iconbox = "";
					if($(box).attr("iconcss"))
					{
						iconbox = '<span class="' + $(box).attr("iconcss") + '"></span>';
					}
                    $(".l-accordion-header-inner", header).html(iconbox + $(box).attr("title"));
                    $(box).attr("title","");
                }
                $(box).before(header);
                if (!$(box).hasClass("l-accordion-content")) $(box).addClass("l-accordion-content");
            });

            //add Even
            $(".l-accordion-toggle", g.accordion).each(function()
            {
                if (!$(this).hasClass("l-accordion-toggle-open") && !$(this).hasClass("l-accordion-toggle-close"))
                {
                    $(this).addClass("l-accordion-toggle-close");
                }
                if ($(this).hasClass("l-accordion-toggle-close"))
                {
                    $(this).parent().next(".l-accordion-content:visible").hide();
                }
            });
            $(".l-accordion-header", g.accordion).hover(function()
            {
                $(this).addClass("l-accordion-header-over");
            }, function()
            {
                $(this).removeClass("l-accordion-header-over");
            });
            $(".l-accordion-toggle", g.accordion).hover(function()
            {
                if ($(this).hasClass("l-accordion-toggle-open"))
                    $(this).addClass("l-accordion-toggle-open-over");
                else if ($(this).hasClass("l-accordion-toggle-close"))
                    $(this).addClass("l-accordion-toggle-close-over");
            }, function()
            {
                if ($(this).hasClass("l-accordion-toggle-open"))
                    $(this).removeClass("l-accordion-toggle-open-over");
                else if ($(this).hasClass("l-accordion-toggle-close"))
                    $(this).removeClass("l-accordion-toggle-close-over");
            });
            $(">.l-accordion-header", g.accordion).click(function()
            {
                var togglebtn = $(".l-accordion-toggle:first",this);
                if (togglebtn.hasClass("l-accordion-toggle-close"))
                {
                    togglebtn.removeClass("l-accordion-toggle-close")
                    .removeClass("l-accordion-toggle-close-over l-accordion-toggle-open-over") 
                    togglebtn.addClass("l-accordion-toggle-open");
                    $(this).next(".l-accordion-content")
                    .show(p.speed)
                    .siblings(".l-accordion-content:visible").hide(p.speed);
                    $(this).siblings(".l-accordion-header").find(".l-accordion-toggle").removeClass("l-accordion-toggle-open").addClass("l-accordion-toggle-close");
                }
                else
                {
                    togglebtn.removeClass("l-accordion-toggle-open")
                    .removeClass("l-accordion-toggle-close-over l-accordion-toggle-open-over") 
                    .addClass("l-accordion-toggle-close");
                    $(this).next(".l-accordion-content").hide(p.speed);
                }
            });
            //init
            g.headerHoldHeight = 0;
            $("> .l-accordion-header", g.accordion).each(function()
            {
                g.headerHoldHeight += $(this).height();
            });
            if (p.height && typeof (p.height) == 'string' && p.height.indexOf('%') > 0)
            {
                g.onResize();
                if (p.changeHeightOnResize)
                {
                    $(window).resize(function()
                    {
                        g.onResize();
                    });
                }
            }
            else
            {
                if (p.height)
                {
                    g.height = p.heightDiff + p.height;
                    g.accordion.height(g.height);
                    g.setHeight(p.height);
                }
                else
                {
                    g.header = g.accordion.height();
                }
            }

            if (this.id == undefined) this.id = "LigerUI_" + new Date().getTime();
            LigerUIManagers[this.id + "_Accordion"] = g;
            this.usedAccordion = true; 
        });
        if (this.length == 0) return null;
        if (this.length == 1) return LigerUIManagers[this[0].id + "_Accordion"];
        var managers = [];
        this.each(function() {
            managers.push(LigerUIManagers[this.id + "_Accordion"]);
        });
        return managers;
    };

})(jQuery);
//ligerDialog.js
//dialog 图片文件夹的路径 针对于IE6设置
var ligerDialogImagePath = "../../scripts/ui/skins/Aqua/images/dialog/";
(function($) {

    $.ligerDefaults = $.ligerDefaults || {};
    $.ligerDefaults.Dialog = {
        cls:null,       //给dialog附加css class
        id:null,        //给dialog附加id
        buttons: null, //按钮集合 
        isDrag: true,   //是否拖动
        width: 280,     //宽度
        height: null,   //高度，默认自适应 
        content: '',    //内容
        target: null,   //目标对象，指定它将以appendTo()的方式载入
        url: null,      //目标页url，默认以iframe的方式载入
        load: false,     //是否以load()的方式加载目标页的内容
        type: 'warn',   //类型 warn、success、error、question
        left: null,     //位置left
        top: null,      //位置top
        modal: true,    //是否模态对话框
        name: null,     //创建iframe时 作为iframe的name和id 
        isResize:false, // 是否调整大小
        allowClose:true, //允许关闭
        opener:null,
        timeParmName:null  //是否给URL后面加上值为new Date().getTime()的参数，如果需要指定一个参数名即可
    };
    $.ligerDefaults.DialogString = { 
        titleMessage: '提示',                     //提示文本标题
        waittingMessage:'正在等待中,请稍候...'
    };
    ///	<param name="$" type="jQuery"></param>
    $.ligerDialog = {};
    $.ligerDialog.open = function(p) {
        p = $.extend({}, $.ligerDefaults.Dialog,$.ligerDefaults.DialogString, p || {});
        var g = {
            applyWindowMask: function() {
                $(".l-window-mask").remove();
                $("<div class='l-window-mask' style='display: block;'></div>").height($(window).height()+$(window).scrollTop()).appendTo('body');
            },
            removeWindowMask: function() {
                $(".l-window-mask").remove();
            },
            applyDrag: function() {
                if ($.fn.ligerDrag)
                    g.dialog.ligerDrag({ handler: '.l-dialog-title' });
            },
            applyResize:function(){
                if($.fn.ligerResizable)
                {
                    g.dialog.ligerResizable({
                    onStopResize: function (current, e)
                    {
                        var top = 0;
                        var left = 0;
                        if (!isNaN(parseInt(g.dialog.css('top'))))
                            top = parseInt(g.dialog.css('top'));
                        if (!isNaN(parseInt(g.dialog.css('left'))))
                            left = parseInt(g.dialog.css('left'));
                        if (current.diffTop != undefined)
                        {
                            g.dialog.css({
                                top: top + current.diffTop,
                                left: left + current.diffLeft
                            });
                            g.dialog.body.css({
                                width : current.newWidth - 26
                            }); 
                            $(".l-dialog-content",g.dialog.body).height(current.newHeight - 46 -  $(".l-dialog-buttons",  g.dialog).height());
                        }
                        return false;
                    }
                    });
                }
            },
            setImage: function() {
                if (p.type) {
                    if (p.type == 'success' || p.type == 'donne' || p.type == 'ok') {
                        $(".l-dialog-image", g.dialog).addClass("l-dialog-image-donne").show();
                        $(".l-dialog-content", g.dialog).css({ paddingLeft: 64, paddingBottom: 30 });
                    }
                    else if (p.type == 'error') {
                        $(".l-dialog-image", g.dialog).addClass("l-dialog-image-error").show();
                        $(".l-dialog-content", g.dialog).css({ paddingLeft: 64, paddingBottom: 30 });
                    }
                    else if (p.type == 'warn') {
                        $(".l-dialog-image", g.dialog).addClass("l-dialog-image-warn").show();
                        $(".l-dialog-content", g.dialog).css({ paddingLeft: 64, paddingBottom: 30 });
                    }
                    else if (p.type == 'question') {
                        $(".l-dialog-image", g.dialog).addClass("l-dialog-image-question").show();
                        $(".l-dialog-content", g.dialog).css({ paddingLeft: 64, paddingBottom: 20 });
                    }
                }
            }
        };
        g.dialog = $('<div class="l-dialog"><table class="l-dialog-table" cellpadding="0" cellspacing="0" border="0"><tbody><tr><td class="l-dialog-tl"></td><td class="l-dialog-tc"><div class="l-dialog-tc-inner"><div class="l-dialog-icon"></div><div class="l-dialog-title"></div><div class="l-dialog-close"></div></div></td><td class="l-dialog-tr"></td></tr><tr><td class="l-dialog-cl"></td><td class="l-dialog-cc"><div class="l-dialog-body"><div class="l-dialog-image"></div> <div class="l-dialog-content"></div><div class="l-dialog-buttons"><div class="l-dialog-buttons-inner"></div></td><td class="l-dialog-cr"></td></tr><tr><td class="l-dialog-bl"></td><td class="l-dialog-bc"></td><td class="l-dialog-br"></td></tr></tbody></table></div>');
        $('body').append(g.dialog);
        g.dialog.body = $(".l-dialog-body:first", g.dialog);
        g.dialog.close = function() {
            if(g.dialog.frame)
            {
                $(g.dialog.frame.document).ready(function(){
                    g.removeWindowMask(); 
                    g.dialog.remove();
                });
            }
            else
            {
                g.removeWindowMask();
                g.dialog.remove();
            }
        };
        g.dialog.doShow = function() {
            g.dialog.show();
        };
        if(p.allowClose == false) $(".l-dialog-close", g.dialog).remove();
        if (p.target || p.url || p.type == "none") p.type = null;
        if(p.cls) g.dialog.addClass(p.cls);
        if(p.id) g.dialog.attr("id",p.id);

        //设置锁定屏幕、拖动支持 和设置图片
        if (p.modal)
            g.applyWindowMask();
        if (p.isDrag)
            g.applyDrag();
        if(p.isResize)
            g.applyResize();
        if (p.type)
            g.setImage();
        else {
            $(".l-dialog-image", g.dialog).remove();
            $(".l-dialog-content", g.dialog.body).addClass("l-dialog-content-noimage"); 
        } 
        //设置主体内容
        if (p.target) {
            $(".l-dialog-content", g.dialog.body).prepend(p.target);
        }
        else if (p.url) {
            if(p.timeParmName) 
            { 
                p.url += p.url.indexOf('?')==-1 ? "?" : "&" ; 
                p.url += p.timeParmName +"="+new Date().getTime();
            }
            var iframe = $("<iframe frameborder='0'></iframe>");
            var framename = p.name ?  p.name : "ligerwindow" + new Date().getTime();
            iframe.attr("name", framename);
            $(".l-dialog-content", g.dialog.body).prepend(iframe); 
            $(".l-dialog-content",g.dialog.body).addClass("l-dialog-content-nopadding"); 
            setTimeout(function(){
                iframe.attr("src",p.url); 
                
                g.dialog.frame  = window.frames[iframe.attr("name")];
            },0);  
        }
        else if (p.content) {
            $(".l-dialog-content", g.dialog.body).html(p.content);
        }
        if(p.opener) g.dialog.opener = p.opener;
        //设置按钮
        if (p.buttons) {
                $(p.buttons).each(function(i,item){ 
                                var btn = $('<div class="l-dialog-btn"><div class="l-dialog-btn-l"></div><div class="l-dialog-btn-r"></div><div class="l-dialog-btn-inner"></div></div>');
                                $(".l-dialog-btn-inner", btn).html(item.text);
                                $(".l-dialog-buttons-inner", g.dialog.body).prepend(btn);
                                item.width && btn.width(item.width);   
                                item.onclick && btn.click(function() { item.onclick(item , g.dialog,i) });
                });
        }else{
            $(".l-dialog-buttons",  g.dialog).remove();
        }
        $(".l-dialog-buttons-inner", g.dialog).append("<div class='l-clear'></div>");

        //设置参数属性
        p.width && g.dialog.body.width(p.width - 26);
        if(p.height)
        { 
            $(".l-dialog-content",g.dialog.body).height(p.height - 46 -  $(".l-dialog-buttons",  g.dialog).height());
        }
        p.title = p.title || p.titleMessage;
        p.title && $(".l-dialog-title", g.dialog).html(p.title);
        $(".l-dialog-title", g.dialog).bind("selectstart", function() { return false; });


        //设置事件
        $(".l-dialog-btn", g.dialog.body).hover(function() {
            $(this).addClass("l-dialog-btn-over");
        }, function() {
            $(this).removeClass("l-dialog-btn-over");
        });
        $(".l-dialog-tc .l-dialog-close", g.dialog).hover(function() {
            $(this).addClass("l-dialog-close-over");
        }, function() {
            $(this).removeClass("l-dialog-close-over");
        }).click(function() {
            g.dialog.close();
        });

        //IE6 PNG Fix
        var ie55 = (navigator.appName == "Microsoft Internet Explorer" && parseInt(navigator.appVersion) == 4 && navigator.appVersion.indexOf("MSIE 5.5") != -1);
        var ie6 = (navigator.appName == "Microsoft Internet Explorer" && parseInt(navigator.appVersion) == 4 && navigator.appVersion.indexOf("MSIE 6.0") != -1);
        if ($.browser.msie && (ie55 || ie6)) {
            $(".l-dialog-tl:first", g.dialog).css({
                "background": "none",
                "filter": "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + ligerDialogImagePath + "dialog-tl.png',sizingMethod='crop');"
            });
            $(".l-dialog-tc:first", g.dialog).css({
                "background": "none",
                "filter": "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + ligerDialogImagePath + "ie6/dialog-tc.png',sizingMethod='crop');"
            });
            $(".l-dialog-tr:first", g.dialog).css({
                "background": "none",
                "filter": "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + ligerDialogImagePath + "dialog-tr.png',sizingMethod='crop');"
            });
            $(".l-dialog-cl:first", g.dialog).css({
                "background": "none",
                "filter": "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + ligerDialogImagePath + "ie6/dialog-cl.png',sizingMethod='crop');"
            });
            $(".l-dialog-cr:first", g.dialog).css({
                "background": "none",
                "filter": "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + ligerDialogImagePath + "ie6/dialog-cr.png',sizingMethod='crop');"
            });
            $(".l-dialog-bl:first", g.dialog).css({
                "background": "none",
                "filter": "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + ligerDialogImagePath + "dialog-bl.png',sizingMethod='crop');"
            });
            $(".l-dialog-bc:first", g.dialog).css({
                "background": "none",
                "filter": "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + ligerDialogImagePath + "ie6/dialog-bc.png',sizingMethod='crop');"
            });
            $(".l-dialog-br:first", g.dialog).css({
                "background": "none",
                "filter": "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + ligerDialogImagePath + "dialog-br.png',sizingMethod='crop');"
            });
        } 
        //位置初始化
        var left = 0;
        var top = 0; 
        var width = p.width || g.dialog.width();
        if (p.left != null) left = p.left;
        else left = 0.5 * ($(window).width() - width);
        if (p.top != null) top = p.top;
        else top = 0.5 * ($(window).height()  -  g.dialog.height()) + $(window).scrollTop() - 10;
        
        g.dialog.css({ left: left, top: top });
        g.dialog.doShow();
        return g.dialog;
    };
    $.ligerDialog.close = function()
    {
        $(".l-dialog,.l-window-mask").remove();
    };
    $.ligerDialog.alert = function(content, title, type, callback) {
        content = content || "";
        if (typeof (title) == "function") {
            callback = title;
            type = null;
        }
        else if (typeof (type) == "function") {
            callback = type;
        }
        var btnclick = function(item, Dialog,index) {
            Dialog.close();
            if (callback)
                callback(item, Dialog,index);
        };
        p = {
            content: content,
            buttons: [{ text: '确定', onclick: btnclick}]
        };
        if (typeof (title) == "string" && title != "") p.title = title;
        if (typeof (type) == "string" && type != "") p.type = type;
        $.ligerDialog.open(p);
    };

    $.ligerDialog.confirm = function(content,title, callback) {
        if (typeof (title) == "function") {
            callback = title;
            type = null;
        }
        var btnclick = function(item, Dialog) {
            Dialog.close();    
            if (callback) {
                callback(item.type=='ok');
            }
        };
        p = {
            type: 'question', 
            content: content,
            buttons: [{ text: '是', onclick: btnclick,type:'ok' }, { text: '否', onclick: btnclick,type:'no'}]
        };
        if (typeof (title) == "string" && title != "") p.title = title;
        $.ligerDialog.open(p);
    };
    $.ligerDialog.warning = function(content, title, callback) {
        if (typeof (title) == "function") {
            callback = title;
            type = null;
        }
       var btnclick= function(item, Dialog) {
            Dialog.close();
            if (callback) {
                callback(item.type);
            }
        };
        p = {
            type: 'question', 
            content: content,
            buttons: [{ text: '是', onclick: btnclick, type: 'yes' }, { text: '否', onclick: btnclick, type: 'no' }, { text: '取消', onclick: btnclick, type: 'cancel'}]
        };
        if (typeof (title) == "string" && title != "") p.title = title;
        $.ligerDialog.open(p);
    };
    $.ligerDialog.waitting = function(title)
    {
        title = title || $.ligerDefaults.Dialog.waittingMessage;
        $.ligerDialog.open({cls:'l-dialog-waittingdialog',type:'none',content:'<div style="padding:4px">'+title+'</div>',allowClose:false});
    };
    $.ligerDialog.closeWaitting = function()
    {
        $(".l-dialog-waittingdialog,.l-window-mask").remove(); 
    };
    $.ligerDialog.success = function(content, title, onBtnClick) {
        $.ligerDialog.alert(content, title, 'success', onBtnClick);
    };
    $.ligerDialog.error = function(content, title, onBtnClick) {
        $.ligerDialog.alert(content, title, 'error', onBtnClick);
    };
    $.ligerDialog.warn = function(content, title, onBtnClick) {
        $.ligerDialog.alert(content, title, 'warn', onBtnClick);
    };
    $.ligerDialog.question = function(content, title) {
        $.ligerDialog.alert(content, title, 'question');
    };


    $.ligerDialog.prompt = function(title,value,multi, callback) {
        var target = $('<input type="text" class="l-dialog-inputtext"/>');
        if(typeof(multi) == "function"){
            callback = multi;
        }
        if (typeof (value) == "function") {
            callback = value; 
        }
        else if (typeof (value) == "boolean") {
            multi = value;  
        }  
        if(typeof(multi) == "boolean" && multi)
        {
            target = $('<textarea class="l-dialog-textarea"></textarea>');
        }
        if(typeof (value) == "string" || typeof (value) == "int")
        { 
            target.val(value);
        }
        var btnclick = function(item, Dialog , index) {
            Dialog.close();
            if (callback) {
                callback(item.type == 'yes', target.val());
            }
        }
        p = {
            title: title,
            target: target,
            width:320,
            buttons: [{ text: '确定', onclick: btnclick, type: 'yes' }, { text: '取消', onclick: btnclick, type: 'cancel'}]
        };
        $.ligerDialog.open(p);
    };

     
})(jQuery);
//ligerDrag.js
(function($) {
    $.ligerDefaults = $.ligerDefaults || {};
    $.ligerDefaults.Drag = {
        onStartDrag: false,
        onDrag: false,
        onStopDrag: false
    };

    ///	<param name="$" type="jQuery"></param>  
    $.fn.ligerDrag = function(p) {
        p = $.extend({}, $.ligerDefaults.Drag, p || {});
        return this.each(function() {
            if (this.useDrag) return;
            var g = {
                start: function(e) {
                    $('body').css('cursor', 'move');
                    g.current = {
                        target: g.target,
                        left: g.target.offset().left,
                        top: g.target.offset().top,
                        startX: e.pageX || e.screenX,
                        startY: e.pageY || e.clientY
                    };
                    $(document).bind('mouseup.drag', g.stop);
                    $(document).bind('mousemove.drag', g.drag);
                    if (p.onStartDrag) p.onStartDrag(g.current, e);
                },
                drag: function(e) {
                    if (!g.current) return;
                    var pageX = e.pageX || e.screenX;
                    var pageY = e.pageY || e.screenY;
                    g.current.diffX = pageX - g.current.startX;
                    g.current.diffY = pageY - g.current.startY;
                    if (p.onDrag) {
                        if (p.onDrag(g.current, e) != false) {
                            g.applyDrag();
                        }
                    }
                    else {
                        g.applyDrag();
                    }

                    //                    //每30毫秒触发一次
                    //                    $(document).unbind('mousemove.drag');
                    //                    setTimeout(function ()
                    //                    {
                    //                        $(document).bind('mousemove.drag', g.drag);
                    //                    }, 30);
                },
                stop: function(e) {
                    $(document).unbind('mousemove.drag');
                    $(document).unbind('mouseup.drag');
                    $("body").css("cursor", "");
                    if (p.onStopDrag) p.onStopDrag(g.current, e);
                    g.current = null;
                },
                //更新当前坐标
                applyDrag: function() {
                    if (g.current.diffX) {
                        g.target.css("left", (g.current.left + g.current.diffX));
                    }
                    if (g.current.diffY) {
                        g.target.css("top", (g.current.top + g.current.diffY));
                    }
                }
            };
            g.target = $(this);
            if (p.handler == undefined || p.handler == null)
                g.handler = $(this);
            else
                g.handler = (typeof p.handler == 'string' ? $(p.handler, this) : p.handle);
            g.handler.hover(function() {
                $('body').css('cursor', 'move');
            }, function() {
                $("body").css("cursor", "default");
            }).mousedown(function(e) { 
                g.start(e);
                return false;
            });
            this.useDrag = true;
        });
    };
})(jQuery);
//ligerMenu.js
if (typeof (LigerUIMenu) == "undefined") LigerUIMenu = {}; 
(function ($)
{
    $.ligerDefaults = $.ligerDefaults || {};
    $.ligerDefaults.Menu = {
        width: 120,
        top: 0,
        left: 0,
        items:null,
        shadow: true
    };
    ///	<param name="$" type="jQuery"></param> 
    $.ligerMenu = function (p)
    {
        p = $.extend({ }, $.ligerDefaults.Menu, p || {});
        var g = {
            show: function (options,menu)
            {
                if(menu==undefined) menu = g.menu;
                if (options && options.left != undefined)
                {
                    menu.css({ left: options.left });
                }
                if (options && options.top != undefined)
                {
                    menu.css({ top: options.top });
                }
                menu.show();
                g.updateShadow(menu);
            },
            updateShadow: function (menu)
            {
                if (!p.shadow) return;
                menu.shadow.css({
                    left: menu.css('left'),
                    top: menu.css('top'),
                    width: menu.outerWidth(),
                    height: menu.outerHeight()
                });
                if (menu.is(":visible"))
                    menu.shadow.show();
                else
                    menu.shadow.hide();
            },
            hide: function (menu)
            {
                if(menu==undefined) menu = g.menu;
                g.hideAllSubMenu(menu);
                menu.hide();
                g.updateShadow(menu);
            },
            toggle: function ()
            {
                g.menu.toggle();
                g.updateShadow(g.menu);
            },
            removeItem: function (itemid)
            {
                $("> .l-menu-item[menuitemid=" + itemid + "]", g.menu.items).remove();
                g.itemCount--;
            },
            setEnable: function (itemid)
            {
                $("> .l-menu-item[menuitemid=" + itemid + "]", g.menu.items).removeClass("l-menu-item-disable");
            },
            setDisable: function (itemid)
            {
                $("> .l-menu-item[menuitemid=" + itemid + "]", g.menu.items).addClass("l-menu-item-disable");
            },
            isEnable: function (itemid)
            {
                return !$("> .l-menu-item[menuitemid=" + itemid + "]", g.menu.items).hasClass("l-menu-item-disable");
            },
            getItemCount: function ()
            {
                return $("> .l-menu-item", g.menu.items).length;
            },
            addItem: function (item, menu)
            {
                if(!item) return ;
                if(menu== undefined) menu = g.menu;
                
                if (item.line)
                {
                    menu.items.append('<div class="l-menu-item-line"></div>');
                    return;
                }
                var ditem = $('<div class="l-menu-item"><div class="l-menu-item-text"></div> </div>');
                var itemcount = $("> .l-menu-item", menu.items).length;
                menu.items.append(ditem); 
                item.id && ditem.attr("menuitemid", item.id);
                item.text && $(">.l-menu-item-text:first", ditem).html(item.text);
                item.icon && ditem.prepend('<div class="l-menu-item-icon l-icon-' + item.icon + '"></div>');
                item.disable && ditem.addClass("l-menu-item-disable");
                if (item.children)
                {
                    if (ditem.attr("menuitemid") == undefined) ditem.attr("menuitemid", new Date().getTime());
                    ditem.append('<div class="l-menu-item-arrow"></div>');
                    var newmenu = g.createMenu(ditem.attr("menuitemid"));
                    LigerUIMenu[ditem.attr("menuitemid")] = newmenu;
                    newmenu.width(p.width);
                    newmenu.hover(null,function(){
                        if(!newmenu.showedSubMenu)
                            g.hide(newmenu);
                    });
                    $(item.children).each(function ()
                    {
                        g.addItem(this, newmenu);
                    });
                }
                item.click && ditem.click(function ()
                {
                    if ($(this).hasClass("l-menu-item-disable")) return;
                    item.click(item, itemcount);
                });
                item.dblclick && ditem.dblclick(function ()
                {
                    if ($(this).hasClass("l-menu-item-disable")) return;
                    item.dblclick(item, itemcount);
                });

                var menuover = $("> .l-menu-over:first", menu);
                ditem.hover(function ()
                { 
                    if ($(this).hasClass("l-menu-item-disable")) return; 
                    var itemtop = $(this).offset().top;
                    var top = itemtop - menu.offset().top;
                    menuover.css({ top: top });
                    g.hideAllSubMenu(menu);
                    if (item.children)
                    {
                        var meniitemid = $(this).attr("menuitemid");
                        if (!meniitemid) return; 
                        if(LigerUIMenu[meniitemid])
                        {
                            g.show({top:itemtop,left:$(this).offset().left+$(this).width()-5},LigerUIMenu[meniitemid]);
                            menu.showedSubMenu = true;
                        } 
                    } 
                }, function ()
                {
                    if ($(this).hasClass("l-menu-item-disable")) return; 
                    var meniitemid = $(this).attr("menuitemid");
                    if (item.children)
                    {
                        var meniitemid = $(this).attr("menuitemid");
                        if (!meniitemid) return;
                    };
                });
            },
            hideAllSubMenu:function(menu)
            {
                if(menu==undefined) menu = g.menu;
                $("> .l-menu-item",menu.items).each(function(){
                    if($("> .l-menu-item-arrow",this).length>0)
                    {
                        var meniitemid = $(this).attr("menuitemid");
                        if (!meniitemid) return;
                        LigerUIMenu[meniitemid] && g.hide(LigerUIMenu[meniitemid]);
                    }
                });
                menu.showedSubMenu = false;
            },
            createMenu: function (parentMenuItemID)
            {
                var menu = $('<div class="l-menu" style="display:none"><div class="l-menu-yline"></div><div class="l-menu-over"><div class="l-menu-over-l"></div> <div class="l-menu-over-r"></div></div><div class="l-menu-inner"></div></div>');
                parentMenuItemID && menu.attr("parentmenuitemid", parentMenuItemID);
                menu.items = $("> .l-menu-inner:first", menu);
                menu.appendTo('body');
                if (p.shadow)
                {
                    menu.shadow = $('<div class="l-menu-shadow"></div>').insertAfter(menu);
                    g.updateShadow(menu);
                }
                menu.hover(null,function(){
                    if(!menu.showedSubMenu)
                        $("> .l-menu-over:first", menu).css({ top: -24 });
                });
                return menu;
            }
        };
        g.menu = g.createMenu();
        g.menu.css({ top: p.top, left: p.left, width: p.width });  
        p.items && $(p.items).each(function (i, item)
        { 
            g.addItem(item);
        });
        return g;
    };
    $(document).click(function ()
    {
        $(".l-menu,.l-menu-shadow").hide();
    });
})(jQuery);
//ligerLayout.js
(function ($) {
    ///	<param name="$" type="jQuery"></param>

    $.fn.ligerGetLayoutManager = function () {
        return LigerUIManagers[this[0].id + "_Layout"];
    };
    $.fn.ligerRemoveLayoutManager = function () {
        return this.each(function () {
            LigerUIManagers[this.id + "_Layout"] = null;
        });
    };
    $.ligerDefaults = $.ligerDefaults || {};
    $.ligerDefaults.Layout = {
        topHeight: 65,
        bottomHeight: 65,
        leftWidth: 110,
        centerWidth: 300,
        rightWidth: 170,
        InWindow : true,     //是否以窗口的高度为准 height设置为百分比时可用
        heightDiff : 0,     //高度补差
        height:'100%',      //高度
        onHeightChanged: null,
        isLeftCollapse: false,      //初始化时 左边是否隐藏
        isRightCollapse: false,     //初始化时 右边是否隐藏
        allowLeftCollapse: true,      //是否允许 左边可以隐藏
        allowRightCollapse: true,     //是否允许 右边可以隐藏
        allowLeftResize: true,      //是否允许 左边可以调整大小
        allowRightResize: true,     //是否允许 右边可以调整大小
        allowTopResize: true,      //是否允许 头部可以调整大小
        allowBottomResize: true,     //是否允许 底部可以调整大小
        space: 5 //间隔
    };
    $.fn.ligerLayout = function (p) {
        this.each(function () {
            p = $.extend({ }, $.ligerDefaults.Layout, p || {});
            if (this.usedLayout) return;
            var g = {
                init: function () {
                    $("> .l-layout-left .l-layout-header,> .l-layout-right .l-layout-header", g.layout).hover(function () {
                        $(this).addClass("l-layout-header-over");
                    }, function () {
                        $(this).removeClass("l-layout-header-over");

                    });
                    $(".l-layout-header-toggle", g.layout).hover(function () {
                        $(this).addClass("l-layout-header-toggle-over");
                    }, function () {
                        $(this).removeClass("l-layout-header-toggle-over");

                    });
                    $(".l-layout-header-toggle", g.left).click(function () {
                        g.setLeftCollapse(true);
                    });
                    $(".l-layout-header-toggle", g.right).click(function () {
                        g.setRightCollapse(true);
                    });
                    //set top
                    g.middleTop = 0;
                    if (g.top) {
                        g.middleTop += g.top.height();
                        g.middleTop += parseInt(g.top.css('borderTopWidth'));
                        g.middleTop += parseInt(g.top.css('borderBottomWidth'));
                        //g.middleTop += p.space;
                    }
                    if (g.left) {
                        g.left.css({ top: g.middleTop });
                        g.leftCollapse.css({ top: g.middleTop });
                    }
                    if (g.center) {
						g.center.css({ top: g.middleTop });
					}
                    if (g.right) {
                        g.right.css({ top: g.middleTop });
                        g.rightCollapse.css({ top: g.middleTop });
                    }
                    //set left
                    if (g.left) g.left.css({ left: 0 }); //=====================================
                    g.onResize();
                    g.onResize();
                },
                setCollapse: function () {

                    g.leftCollapse.hover(function () {
                        $(this).addClass("l-layout-collapse-left-over");
                    }, function () {
                        $(this).removeClass("l-layout-collapse-left-over");
                    });
                    g.leftCollapse.toggle.hover(function () {
                        $(this).addClass("l-layout-collapse-left-toggle-over");
                    }, function () {
                        $(this).removeClass("l-layout-collapse-left-toggle-over");
                    });
                    g.rightCollapse.hover(function () {
                        $(this).addClass("l-layout-collapse-right-over");
                    }, function () {
                        $(this).removeClass("l-layout-collapse-right-over");
                    });
                    g.rightCollapse.toggle.hover(function () {
                        $(this).addClass("l-layout-collapse-right-toggle-over");
                    }, function () {
                        $(this).removeClass("l-layout-collapse-right-toggle-over");
                    });
                    g.leftCollapse.toggle.click(function () {
                        g.setLeftCollapse(false);
                    });
                    g.rightCollapse.toggle.click(function () {
                        g.setRightCollapse(false);
                    });
                    if (g.left && g.isLeftCollapse) {
                        g.leftCollapse.show();
                        g.leftDropHandle && g.leftDropHandle.hide();
                        g.left.hide();
                    }
                    if (g.right && g.isRightCollapse) {
                        g.rightCollapse.show();
                        g.rightDropHandle && g.rightDropHandle.hide();
                        g.right.hide();
                    }
                },
                setLeftCollapse: function (isCollapse) {
                    if (!g.left) return false;
                    g.isLeftCollapse = isCollapse;
                    if (g.isLeftCollapse) {
                        g.leftCollapse.show();
                        g.leftDropHandle && g.leftDropHandle.hide();
                        g.left.hide();
                    }
                    else {
                        g.leftCollapse.hide();
                        g.leftDropHandle && g.leftDropHandle.show();
                        g.left.show();
                    }
                    g.onResize();
                },
                setRightCollapse: function (isCollapse) {
                    if (!g.right) return false;
                    g.isRightCollapse = isCollapse;
                    g.onResize();
                    if (g.isRightCollapse) {
                        g.rightCollapse.show();
                        g.rightDropHandle && g.rightDropHandle.hide();
                        g.right.hide();
                    }
                    else {
                        g.rightCollapse.hide();
                        g.rightDropHandle && g.rightDropHandle.show();
                        g.right.show();
                    }
                    g.onResize();
                },
                addDropHandle: function () {
                    if (g.left && p.allowLeftResize) {
                        g.leftDropHandle = $("<div class='l-layout-drophandle-left'></div>");
                        g.layout.append(g.leftDropHandle);
                        g.leftDropHandle && g.leftDropHandle.show();
                        g.leftDropHandle.mousedown(function (e) {
                            g.start('leftresize', e);
                        });
                    }
                    if (g.right && p.allowRightResize) {
                        g.rightDropHandle = $("<div class='l-layout-drophandle-right'></div>");
                        g.layout.append(g.rightDropHandle);
                        g.rightDropHandle && g.rightDropHandle.show();
                        g.rightDropHandle.mousedown(function (e) {
                            g.start('rightresize', e);
                        });
                    }
                    if (g.top && p.allowTopResize) {
                        g.topDropHandle = $("<div class='l-layout-drophandle-top'></div>");
                        g.layout.append(g.topDropHandle);
                        g.topDropHandle.show();
                        g.topDropHandle.mousedown(function (e) {
                            g.start('topresize', e);
                        });
                    }
                    if (g.bottom  && p.allowBottomResize) {
                        g.bottomDropHandle = $("<div class='l-layout-drophandle-bottom'></div>");
                        g.layout.append(g.bottomDropHandle);
                        g.bottomDropHandle.show();
                        g.bottomDropHandle.mousedown(function (e) {
                            g.start('bottomresize', e);
                        });
                    }
                    g.draggingxline = $("<div class='l-layout-dragging-xline'></div>");
                    g.draggingyline = $("<div class='l-layout-dragging-yline'></div>");
                    g.layout.append(g.draggingxline).append(g.draggingyline);
                },
                setDropHandlePosition: function () {
                    if (g.leftDropHandle) {
                        g.leftDropHandle.css({ left: g.left.width() + parseInt(g.left.css('left')), height: g.middleHeight, top: g.middleTop });
                    }
                    if (g.rightDropHandle) {
                        g.rightDropHandle.css({ left: parseInt(g.right.css('left')) - p.space, height: g.middleHeight, top: g.middleTop });
                    }
                    if (g.topDropHandle) {
                        g.topDropHandle.css({ top: g.top.height() + parseInt(g.top.css('top')), width: g.top.width() });
                    }
                    if (g.bottomDropHandle) {
                        g.bottomDropHandle.css({ top: parseInt(g.bottom.css('top')) - p.space, width: g.bottom.width() });
                    }
                },
                onResize: function () {
                    var oldheight = g.layout.height();
                    //set layout height 
                    var h = 0;
                    var windowHeight = $(window).height(); 
                    var parentHeight = null;
                    if (typeof(p.height) == "string" && p.height.indexOf('%') > 0)
                    { 
                        var layoutparent = g.layout.parent(); 
                        if (p.InWindow || layoutparent[0].tagName.toLowerCase() == "body") { 
                            parentHeight = windowHeight; 
                            parentHeight -= parseInt($('body').css('paddingTop'));
                            parentHeight -= parseInt($('body').css('paddingBottom'));
                        }
                        else{ 
                            parentHeight = layoutparent.height();
                        }  
                        h =  parentHeight * parseFloat(p.height) * 0.01;   
                        if(p.InWindow || layoutparent[0].tagName.toLowerCase() == "body") 
                            h -= (g.layout.offset().top - parseInt($('body').css('paddingTop')));
                    } 
                    else
                    { 
                        h = parseInt(p.height);
                    }    
                    h += p.heightDiff;  
                    g.layout.height(h);
                    g.layoutHeight = g.layout.height();
                    g.middleWidth = g.layout.width();
                    g.middleHeight = g.layout.height();
                    if (g.top) {
                        g.middleHeight -= g.top.height();
                        g.middleHeight -= parseInt(g.top.css('borderTopWidth'));
                        g.middleHeight -= parseInt(g.top.css('borderBottomWidth'));
                        g.middleHeight -= p.space;
                    }
                    if (g.bottom) {
                        g.middleHeight -= g.bottom.height();
                        g.middleHeight -= parseInt(g.bottom.css('borderTopWidth'));
                        g.middleHeight -= parseInt(g.bottom.css('borderBottomWidth'));
                        //g.middleHeight -= p.space;
                    }
                    //specific
                    g.middleHeight -= 2;

                    if (p.onHeightChanged && g.layoutHeight != oldheight) {
                        p.onHeightChanged({ layoutHeight: g.layoutHeight, diff: g.layoutHeight - oldheight, middleHeight: g.middleHeight });
                    }

                    if (g.center) {
                        g.centerWidth = g.middleWidth;
                        if (g.left) {
                            if (g.isLeftCollapse) {
                                g.centerWidth -= g.leftCollapse.width()+2; //================================
                                g.centerWidth -= parseInt(g.leftCollapse.css('borderLeftWidth'));
                                g.centerWidth -= parseInt(g.leftCollapse.css('borderRightWidth'));
                                g.centerWidth -= parseInt(g.leftCollapse.css('left'));
                                g.centerWidth -= p.space;
                            }
                            else {
                                g.centerWidth -= g.leftWidth+2; //=============================
                                g.centerWidth -= parseInt(g.left.css('borderLeftWidth'));
                                g.centerWidth -= parseInt(g.left.css('borderRightWidth'));
                                g.centerWidth -= parseInt(g.left.css('left'));
                                g.centerWidth -= p.space;
                            }
                        }
                        if (g.right) {
                            if (g.isRightCollapse) {
                                g.centerWidth -= g.rightCollapse.width();
                                g.centerWidth -= parseInt(g.rightCollapse.css('borderLeftWidth'));
                                g.centerWidth -= parseInt(g.rightCollapse.css('borderRightWidth'));
                                g.centerWidth -= parseInt(g.rightCollapse.css('right'));
                                g.centerWidth -= p.space;
                            }
                            else {
                                g.centerWidth -= g.rightWidth;
                                g.centerWidth -= parseInt(g.right.css('borderLeftWidth'));
                                g.centerWidth -= parseInt(g.right.css('borderRightWidth'));
                                g.centerWidth -= p.space;
                            }
                        }
                        g.centerLeft = 0;
                        if (g.left) {
                            if (g.isLeftCollapse) {
                                g.centerLeft += g.leftCollapse.width();
                                g.centerLeft += parseInt(g.leftCollapse.css('borderLeftWidth'));
                                g.centerLeft += parseInt(g.leftCollapse.css('borderRightWidth'));
                                g.centerLeft += parseInt(g.leftCollapse.css('left'));
                                g.centerLeft += p.space;
                            }
                            else {
                                g.centerLeft += g.left.width();
                                g.centerLeft += parseInt(g.left.css('borderLeftWidth'));
                                g.centerLeft += parseInt(g.left.css('borderRightWidth'));
                                g.centerLeft += p.space;
                            }
                        }
                        g.center.css({ left: g.centerLeft });
                        g.center.width(g.centerWidth);
                        g.center.height(g.middleHeight);
                        var contentHeight = g.middleHeight;
                        if(g.center.header) contentHeight-= g.center.header.height();
                        g.center.content.height(contentHeight);
                    }
                    if (g.left) {
                        g.leftCollapse.height(g.middleHeight);
                        g.left.height(g.middleHeight);
                    }
                    if (g.right) {
                        g.rightCollapse.height(g.middleHeight);
                        g.right.height(g.middleHeight);
                        //set left
                        g.rightLeft = 0;

                        if (g.left) {
                            if (g.isLeftCollapse) {
                                g.rightLeft += g.leftCollapse.width();
                                g.rightLeft += parseInt(g.leftCollapse.css('borderLeftWidth'));
                                g.rightLeft += parseInt(g.leftCollapse.css('borderRightWidth'));
                                g.rightLeft += p.space;
                            }
                            else {
                                g.rightLeft += g.left.width();
                                g.rightLeft += parseInt(g.left.css('borderLeftWidth'));
                                g.rightLeft += parseInt(g.left.css('borderRightWidth'));
                                g.rightLeft += parseInt(g.left.css('left'));
                                g.rightLeft += p.space;
                            }
                        }
                        if (g.center) {
                            g.rightLeft += g.center.width();
                            g.rightLeft += parseInt(g.center.css('borderLeftWidth'));
                            g.rightLeft += parseInt(g.center.css('borderRightWidth'));
                            g.rightLeft += p.space;
                        }
                        g.right.css({ left: g.rightLeft });
                    }
                    if (g.bottom) {
                        g.bottomTop = g.layoutHeight - g.bottom.height() - 2;
                        g.bottom.css({ top: g.bottomTop });
                    }
                    g.setDropHandlePosition();

                },
                start: function (dragtype, e) {
                    g.dragtype = dragtype;
                    if (dragtype == 'leftresize' || dragtype == 'rightresize') {
                        g.xresize = { startX: e.pageX };
                        g.draggingyline.css({ left: e.pageX - g.layout.offset().left, height: g.middleHeight, top: g.middleTop }).show();
                        $('body').css('cursor', 'col-resize');
                    }
                    else if (dragtype == 'topresize' || dragtype == 'bottomresize') {
                        g.yresize = { startY: e.pageY };
                        g.draggingxline.css({ top: e.pageY - g.layout.offset().top, width: g.layout.width() }).show();
                        $('body').css('cursor', 'row-resize');
                    }
                    else {
                        return;
                    }

                    g.layout.lock.width(g.layout.width());
                    g.layout.lock.height(g.layout.height());
                    g.layout.lock.show();
                    if ($.browser.msie || $.browser.safari)  $('body').bind('selectstart', function () { return false; }); // 不能选择

                    $(document).bind('mouseup', g.stop);
                    $(document).bind('mousemove', g.drag);
                },
                drag: function (e) {
                    if (g.xresize) {
                        g.xresize.diff = e.pageX - g.xresize.startX;
                        g.draggingyline.css({ left: e.pageX - g.layout.offset().left });
                        $('body').css('cursor', 'col-resize');
                    }
                    else if (g.yresize) {
                        g.yresize.diff = e.pageY - g.yresize.startY;
                        g.draggingxline.css({ top: e.pageY - g.layout.offset().top });
                        $('body').css('cursor', 'row-resize');
                    } 
                },
                stop: function (e) {

                    if (g.xresize && g.xresize.diff != undefined) {
                        if (g.dragtype == 'leftresize') {
                            g.leftWidth += g.xresize.diff;
                            g.left.width(g.leftWidth);
                            if (g.center)
                                g.center.width(g.center.width() - g.xresize.diff).css({ left: parseInt(g.center.css('left')) + g.xresize.diff });
                            else if (g.right)
                                g.right.width(g.left.width() - g.xresize.diff).css({ left: parseInt(g.right.css('left')) + g.xresize.diff });
                        }
                        else if (g.dragtype == 'rightresize') {
                            g.rightWidth -= g.xresize.diff;
                            g.right.width(g.rightWidth).css({ left: parseInt(g.right.css('left')) + g.xresize.diff });
                            if (g.center)
                                g.center.width(g.center.width() + g.xresize.diff);
                            else if (g.left)
                                g.left.width(g.left.width() + g.xresize.diff);
                        }
                    }
                    else if (g.yresize && g.yresize.diff != undefined) {
                        if (g.dragtype == 'topresize') {
                            g.top.height(g.top.height() + g.yresize.diff);
                            g.middleTop += g.yresize.diff;
                            g.middleHeight -= g.yresize.diff;
                            if (g.left) {
                                g.left.css({ top: g.middleTop }).height(g.middleHeight);
                                g.leftCollapse.css({ top: g.middleTop }).height(g.middleHeight);
                            }
                            if (g.center) g.center.css({ top: g.middleTop }).height(g.middleHeight);
                            if (g.right) {
                                g.right.css({ top: g.middleTop }).height(g.middleHeight);
                                g.rightCollapse.css({ top: g.middleTop }).height(g.middleHeight);
                            }
                        }
                        else if (g.dragtype == 'bottomresize') {
                            g.bottom.height(g.bottom.height() - g.yresize.diff);
                            g.middleHeight += g.yresize.diff;
                            g.bottomTop += g.yresize.diff;
                            g.bottom.css({ top: g.bottomTop });
                            if (g.left) {
                                g.left.height(g.middleHeight);
                                g.leftCollapse.height(g.middleHeight);
                            }
                            if (g.center) g.center.height(g.middleHeight);
                            if (g.right) {
                                g.right.height(g.middleHeight);
                                g.rightCollapse.height(g.middleHeight);
                            }
                        }
                    }
                    g.setDropHandlePosition();
                    g.draggingxline.hide();
                    g.draggingyline.hide();
                    g.xresize = g.yresize = g.dragtype = false;
                    g.layout.lock.hide();
                    if ($.browser.msie || $.browser.safari)
                        $('body').unbind('selectstart');
                    $(document).unbind('mousemove', g.drag);
                    $(document).unbind('mouseup', g.stop);
                    $('body').css('cursor', '');
                }
            };
            g.layout = $(this);
            if (!g.layout.hasClass("l-layout"))
                g.layout.addClass("l-layout");
            g.width = g.layout.width();
            //top
            if ($("> div[position=top]", g.layout).length > 0) {
                g.top = $("> div[position=top]", g.layout).wrap('<div class="l-layout-top" style="top:0px;"></div>').parent();
                g.top.content = $("> div[position=top]", g.top);
                if (!g.top.content.hasClass("l-layout-content"))
                    g.top.content.addClass("l-layout-content");
                g.topHeight = p.topHeight;
                if (g.topHeight) {
                    g.top.height(g.topHeight);
                }
            }

            //bottom
            if ($("> div[position=bottom]", g.layout).length > 0) {
                g.bottom = $("> div[position=bottom]", g.layout).wrap('<div class="l-layout-bottom"></div>').parent();
                g.bottom.content = $("> div[position=bottom]", g.top);
                if (!g.bottom.content.hasClass("l-layout-content"))
                    g.bottom.content.addClass("l-layout-content");

                g.bottomHeight = p.bottomHeight;
                if (g.bottomHeight) {
                    g.bottom.height(g.bottomHeight);
                }

            }
            //left
            if ($("> div[position=left]", g.layout).length > 0) {
                g.left = $("> div[position=left]", g.layout).wrap('<div class="l-layout-left" style="left:0px;"></div>').parent();
                g.left.header = $('<div class="l-layout-header"><div class="l-layout-header-toggle"></div><div class="l-layout-header-inner"></div></div>');
                g.left.prepend(g.left.header);
                g.left.header.toggle = $(".l-layout-header-toggle", g.left.header);
                g.left.content = $("> div[position=left]", g.left);
                if (!g.left.content.hasClass("l-layout-content"))
                    g.left.content.addClass("l-layout-content");
                if(!p.allowLeftCollapse) $(".l-layout-header-toggle", g.left.header).remove();
                //set title
                var lefttitle = g.left.content.attr("title");
                if (lefttitle) {
                    g.left.content.attr("title", "");
                    $(".l-layout-header-inner", g.left.header).html(lefttitle);
                }
                //set width
                g.leftWidth = p.leftWidth;
                if (g.leftWidth)
                    g.left.width(g.leftWidth);
            }
            //center
            if ($("> div[position=center]", g.layout).length > 0) {
                g.center = $("> div[position=center]", g.layout).wrap('<div class="l-layout-center" ></div>').parent();
                g.center.content = $("> div[position=center]", g.center);
                g.center.content.addClass("l-layout-content");
                //set title
                var centertitle = g.center.content.attr("title");
                if (centertitle) {
                    g.center.content.attr("title", "");
                    g.center.header = $('<div class="l-layout-header"></div>');
                    g.center.prepend(g.center.header);
                    g.center.header.html(centertitle);
                }
                //set width
                g.centerWidth = p.centerWidth;
                if (g.centerWidth)
                    g.center.width(g.centerWidth);
            }
            //right
            if ($("> div[position=right]", g.layout).length > 0) {
                g.right = $("> div[position=right]", g.layout).wrap('<div class="l-layout-right"></div>').parent();

                g.right.header = $('<div class="l-layout-header"><div class="l-layout-header-toggle"></div><div class="l-layout-header-inner"></div></div>');
                g.right.prepend(g.right.header);
                g.right.header.toggle = $(".l-layout-header-toggle", g.right.header);
                if(!p.allowRightCollapse) $(".l-layout-header-toggle", g.right.header).remove();
                g.right.content = $("> div[position=right]", g.right);
                if (!g.right.content.hasClass("l-layout-content"))
                    g.right.content.addClass("l-layout-content");

                //set title
                var righttitle = g.right.content.attr("title");
                if (righttitle) {
                    g.right.content.attr("title", "");
                    $(".l-layout-header-inner", g.right.header).html(righttitle);
                }
                //set width
                g.rightWidth = p.rightWidth;
                if (g.rightWidth)
                    g.right.width(g.rightWidth);
            }
            //lock
            g.layout.lock = $("<div class='l-layout-lock'></div>");
            g.layout.append(g.layout.lock);
            //DropHandle
            g.addDropHandle();

            //Collapse
            g.isLeftCollapse = p.isLeftCollapse;
            g.isRightCollapse = p.isRightCollapse;
            g.leftCollapse = $('<div class="l-layout-collapse-left" style="display: none; "><div class="l-layout-collapse-left-toggle"></div></div>');
            g.rightCollapse = $('<div class="l-layout-collapse-right" style="display: none; "><div class="l-layout-collapse-right-toggle"></div></div>');
            g.layout.append(g.leftCollapse).append(g.rightCollapse);
            g.leftCollapse.toggle = $("> .l-layout-collapse-left-toggle", g.leftCollapse);
            g.rightCollapse.toggle = $("> .l-layout-collapse-right-toggle", g.rightCollapse);
            g.setCollapse();

            //init
            g.init();
            $(window).resize(function () {
                g.onResize();
            });
            if (this.id == undefined) this.id = "LigerUI_" + new Date().getTime();
            LigerUIManagers[this.id + "_Layout"] = g;
            this.usedLayout = true;
        });
        if (this.length == 0) return null;
        if (this.length == 1) return LigerUIManagers[this[0].id + "_Layout"];
        var managers = [];
        this.each(function() {
            managers.push(LigerUIManagers[this.id + "_Layout"]);
        });
        return managers;
    };
})(jQuery);
//ligerResizable.js
(function ($)
{
    $.ligerDefaults = $.ligerDefaults || {};
    $.ligerDefaults.Resizable = {
        handles: 'n, e, s, w, ne, se, sw, nw',
        maxWidth: 2000,
        maxHeight: 2000,
        minWidth: 20,
        minHeight: 20,
        onStartResize: function (e) { },
        onResize: function (e) { },
        onStopResize: function (e) { },
        onEndResize: null
    };

    $.fn.ligerResizable = function (p)
    {
        return this.each(function ()
        {
            p = $.extend({}, $.ligerDefaults.Resizable, p || {});
            var g = {
                init: function ()
                {
                    g.target.append('<div class="l-resizable"></div>');
                    //add handler dom elements 
                    var handles = p.handles.split(',');
                    for (var i = 0; i < handles.length; i++)
                    {
                        switch (handles[i].replace(/(^\s*)|(\s*$)/g, "")) //trim
                        {
                            case "nw":
                                g.target.append('<div class="l-resizable-h-l" direction="nw"></div>');
                                break;
                            case "ne":
                                g.target.append('<div class="l-resizable-h-r" direction="ne"></div>');
                                break;
                            case "n":
                                g.target.append('<div class="l-resizable-h-c" direction="n"></div>');
                                break;
                            case "w":
                                g.target.append('<div class="l-resizable-c-l" direction="w"></div>');
                                break;
                            case "e":
                                g.target.append('<div class="l-resizable-c-r" direction="e"></div>');
                                break;
                            case "sw":
                                g.target.append('<div class="l-resizable-f-l" direction="sw"></div>');
                                break;
                            case "se":
                                g.target.append('<div class="l-resizable-f-r" direction="se"></div>');
                                break;
                            case "s":
                                g.target.append('<div class="l-resizable-f-c" direction="s"></div>');
                                break;
                        }
                    }
                    $("> .l-resizable-h-c , > .l-resizable-f-c", g.target).width(g.target.width());
                    $("> .l-resizable-c-l , > .l-resizable-c-r", g.target).height(g.target.height());
                    g.target.resizable = $("> .l-resizable", g.target);
                },
                start: function (e, dir)
                {

                    if ($.browser.msie || $.browser.safari) 
                        $('body').bind('selectstart', function () { return false; }); // 不能选择
                    $(".l-window-mask-nobackground").remove();
                    $("<div class='l-window-mask-nobackground' style='display: block;'></div>").appendTo($("body"));
                    g.target.resizable.css({
                        width: g.target.width(),
                        height: g.target.height(),
                        left: 0,
                        top: 0
                    });
                    g.current = {
                        dir: dir,
                        left: g.target.offset().left,
                        top: g.target.offset().top,
                        width: g.target.width(),
                        height: g.target.height()
                    };
                    $(document).bind('mouseup', g.stop);
                    $(document).bind('mousemove', g.drag);
                    g.target.resizable.show();
                    if (p.onStartResize) p.onStartResize(g.current, e);
                },
                drag: function (e)
                {
                    var dir = g.current.dir;
                    var resizableObj = g.target.resizable[0];
                    var width = g.current.width;
                    var height = g.current.height;
                    var moveWidth = (e.pageX || e.screenX) - g.current.left;
                    var moveHeight = (e.pageY || e.clientY) - g.current.top;
                    if (dir.indexOf("e") >= 0) moveWidth -= width;
                    if (dir.indexOf("s") >= 0) moveHeight -= height;
                    if (dir != "n" && dir != "s")
                    {
                        width += (dir.indexOf("w") >= 0) ? -moveWidth : moveWidth;
                    }
                    if (width >= p.minWidth)
                    {
                        if (dir.indexOf("w") >= 0)
                        {
                            resizableObj.style.left = moveWidth + 'px';
                        }
                        if (dir != "n" && dir != "s")
                        {
                            resizableObj.style.width = width + 'px';
                        }
                    }
                    if (dir != "w" && dir != "e")
                    {
                        height += (dir.indexOf("n") >= 0) ? -moveHeight : moveHeight;
                    }
                    if (height >= p.minHeight)
                    {
                        if (dir.indexOf("n") >= 0)
                        {
                            resizableObj.style.top = moveHeight + 'px';
                        }
                        if (dir != "w" && dir != "e")
                        {
                            resizableObj.style.height = height + 'px';
                        }
                    }
                    g.current.newWidth = width;
                    g.current.newHeight = height;
                    g.current.diffTop = parseInt(resizableObj.style.top);
                    if (isNaN(g.current.diffTop)) g.current.diffTop = 0;
                    g.current.diffLeft = parseInt(resizableObj.style.left);
                    if (isNaN(g.current.diffLeft)) g.current.diffLeft = 0;
                    $("body").css("cursor", dir + '-resize');
                    if (p.onResize) p.onResize(g.current, e);
                },
                stop: function (e)
                {
                    if ($.browser.msie || $.browser.safari)
                        $('body').unbind('selectstart');
                    $(".l-window-mask-nobackground").remove();
                    if (!p.onStopResize) g.applyResize();
                    else if (p.onStopResize(g.current, e) != false) g.applyResize();
                    p.onEndResize && p.onEndResize(g.current, e);
                    $("body").css("cursor", "");
                    g.target.resizable.hide();
                    $(document).unbind('mousemove', g.drag);
                    $(document).unbind('mouseup', g.stop);
                },
                applyResize: function ()
                {
                    var top = 0;
                    var left = 0;
                    if (!isNaN(parseInt(g.target.css('top'))))
                        top = parseInt(g.target.css('top'));
                    if (!isNaN(parseInt(g.target.css('left'))))
                        left = parseInt(g.target.css('left'));
                    if (g.current.diffTop != undefined)
                    {
                        if (g.current.diffTop != 0)
                            g.target.css({ top: top + g.current.diffTop });
                        if (g.current.diffLeft != 0)
                            g.target.css({ left: left + g.current.diffLeft });
                        g.target.css({
                            width: g.current.newWidth,
                            height: g.current.newHeight
                        });
                    }
                }
            };
            g.target = $(this);
            g.init();
            $(">div", g.target).mousedown(function (e)
            {
                if (!$(this).attr("direction")) return;
                g.start(e, $(this).attr("direction"));
                return false;
            });
        });
    };
})(jQuery);
//ligerTip.js
(function($) {

    $.ligerDefaults = $.ligerDefaults || {};
    $.ligerDefaults.Tip = {
        content: null,
        callback: null,
        width: 150,
        height: null,
        distanceX: 7,
        distanceY: 0,
        appendIdTo: null       //保存ID到那一个对象(jQuery)
    };

    ///	<param name="$" type="jQuery"></param>
    $.fn.ligerTip = function(p) {
        p = $.extend({}, $.ligerDefaults.Tip, p || {});
        this.each(function() { 
            var tip = null;
            var tipid = $(this).attr("ligerTipId");
            if (tipid) {
                tip = $("#" + tipid);
                if (p.content == "") tip.remove();
                else $(".l-verify-tip-content", tip).html(p.content);
            }
            else if (p.content) {
                tip = $('<div class="l-verify-tip"><div class="l-verify-tip-corner"></div><div class="l-verify-tip-content">' + p.content + '</div></div>');
                tip.attr("id", "ligerUI" + new Date().getTime());
				tip.fadeTo(0, 0.85); //对象的透明度
                //tip.appendTo('body');
				$(this).after(tip);
            }
            if (!tip) return;
            //方法一：tip.css({ left: $(this).offset().left + $(this).width() + p.distanceX, top: $(this).offset().top + p.distanceY }).show();
			tip.css({ left: $(this).position().left + $(this).width() + p.distanceX, top: $(this).position().top + p.distanceY }).show();
			//方法三：$(this).parent().css('position','relative');
			//方法三：tip.css({ left: $(this).outerWidth(true) + p.distanceX, top: p.distanceY }).show();
			
            $(this).attr("ligerTipId", tip.attr("id"));
            p.width && $("> .l-verify-tip-content", tip).width(p.width - 8);
            p.height && $("> .l-verify-tip-content", tip).width(p.height);
            p.appendIdTo && p.appendIdTo.attr("ligerTipId", tip.attr("id"));
            p.callback && p.callback(tip);
        });
        if (this.length == 0) return null;
        if (this.length == 1) return this[0].ligerTip;
        var tips = [];
        this.each(function() {
            tips.push(this.ligerTip);
        });
        return tips;
    };
    $.fn.ligerHideTip = function(p) {
        return this.each(function() {
            var tipid = $(this).attr("ligerTipId");
            if (tipid) {
                $("#" + tipid).remove();
                $("[ligerTipId=" + tipid + "]").removeAttr("ligerTipId");
            }
        });
    };
})(jQuery);
//ligerTab.js
(function($)
{
    ///	<param name="$" type="jQuery"></param>

    $.fn.ligerGetTabManager = function()
    {
        return LigerUIManagers[this[0].id + "_Tab"];
    }; 

    $.ligerDefaults = $.ligerDefaults || {};
    $.ligerDefaults.Tab = {
            height: null,
            heightDiff: 0, // 高度补差 
			marginleft: 3,
            changeHeightOnResize: false,
            contextmenu : true,
            closeMessage : "关闭当前页",
            closeOtherMessage : "关闭其他",
            closeAllMessage : "关闭所有",
            reloadMessage : "刷新",
            onBeforeOverrideTabItem:null,
            onAfterOverrideTabItem:null,
            onBeforeRemoveTabItem:null,
            onAfterRemoveTabItem:null,
            onBeforeAddTabItem :null,
            onAfterAddTabItem:null,
            onBeforeSelectTabItem :null,
            onAfterSelectTabItem:null
    };

    $.fn.ligerTab = function(p)
    { 
        p = $.extend({},$.ligerDefaults.Tab, p || {});
        this.each(function()
        {
            if (this.usedTab) return;
            if ($(this).hasClass('l-hidden')) { return; }
            var g = {
                //设置tab按钮(左和右),显示返回true,隐藏返回false
                setTabButton: function()
                {
                    var sumwidth = 0;
                    $("li", g.tab.links.ul).each(function()
                    {
                        sumwidth += ($(this).width() + p.marginleft);
                    });
                    var mainwidth = g.tab.width();
                    if (sumwidth > mainwidth)
                    {
						$(".l-tab-links-left,.l-tab-links-right", g.tab.links).remove();
                        g.tab.links.append('<div class="l-tab-links-left"></div><div class="l-tab-links-right"></div>');
						if($(g.tab).attr("toolsid"))
					    {
							$(".l-tab-links-right", g.tab.links).css("right","17px");
					    }
                        g.setTabButtonEven();
                        return true;
                    } else
                    {
                        g.tab.links.ul.animate({ left: 0 });
                        $(".l-tab-links-left,.l-tab-links-right", g.tab.links).remove();
                        return false;
                    }
                },
                //设置左右按钮的事件 标签超出最大宽度时，可左右拖动
                setTabButtonEven: function()
                {
                    $(".l-tab-links-left", g.tab.links).hover(function()
                    {
                        $(this).addClass("l-tab-links-left-over");
                    }, function()
                    {
                        $(this).removeClass("l-tab-links-left-over");
                    }).click(function()
                    {
                        g.moveToPrevTabItem();
                    });
                    $(".l-tab-links-right", g.tab.links).hover(function()
                    {
                        $(this).addClass("l-tab-links-right-over");
                    }, function()
                    {
                        $(this).removeClass("l-tab-links-right-over");
                    }).click(function()
                    {
                        g.moveToNextTabItem();
                    });
                },
                //切换到上一个tab
                moveToPrevTabItem: function()
                {
                    var btnWitdth = $(".l-tab-links-left", g.tab.links).width();
                    var leftList = new Array(); //记录每个tab的left,由左到右
                    $("li", g.tab.links.ul).each(function(i, item)
                    {
                        var currentItemLeft = -1 * btnWitdth;
                        if (i > 0)
                        {
                            currentItemLeft = parseInt(leftList[i - 1]) + $(this).prev().width() + p.marginleft;
                        }
                        leftList.push(currentItemLeft);
                    });
                    var currentLeft = -1 * parseInt(g.tab.links.ul.css("left"));
                    for (var i = 0; i < leftList.length - 1; i++)
                    {
                        if (leftList[i] < currentLeft && leftList[i + 1] >= currentLeft)
                        {
                            g.tab.links.ul.animate({ left: -1 * parseInt(leftList[i]) });
                            return;
                        }
                    }
                },
                //切换到下一个tab
                moveToNextTabItem: function()
                {
                    var btnWitdth = $(".l-tab-links-right", g.tab).width() + $(".l-tab-links-tools", g.tab.links).width();
                    var sumwidth = 0;
                    var tabItems = $("li", g.tab.links.ul);
                    tabItems.each(function()
                    {
                        sumwidth += $(this).width() + p.marginleft;
                    });
                    var mainwidth = g.tab.width();
                    var leftList = new Array(); //记录每个tab的left,由右到左 
                    for (var i = tabItems.length - 1; i >= 0; i--)
                    {
                        var currentItemLeft = sumwidth - mainwidth + btnWitdth + p.marginleft;
                        if (i != tabItems.length - 1)
                        {
                            currentItemLeft = parseInt(leftList[tabItems.length - 2 - i]) - ($(tabItems[i + 1]).width() + p.marginleft);
                        }
                        leftList.push(currentItemLeft);
                    }
                    var currentLeft = -1 * parseInt(g.tab.links.ul.css("left"));
                    for (var i = 1; i < leftList.length; i++)
                    {
                        if (leftList[i] <= currentLeft && leftList[i - 1] > currentLeft)
                        {
                            g.tab.links.ul.animate({ left: -1 * parseInt(leftList[i - 1]) });
                            return;
                        }
                    }
                },
                getTabItemCount : function()
                {
                    return $("li", g.tab.links.ul).length;
                },
                getSelectedTabItemID:function()
                {
                    return $("li.l-selected", g.tab.links.ul).attr("tabid");
                }, 
                removeSelectedTabItem:function()
                {
                    g.removeTabItem(g.getSelectedTabItemID());
                },
                //覆盖选择的tabitem
                overrideSelectedTabItem : function(options){ 
                    g.overrideTabItem(g.getSelectedTabItemID(),options); 
                },
                //覆盖
                overrideTabItem : function(targettabid,options){
                    if(p.onBeforeOverrideTabItem && p.onBeforeOverrideTabItem(targettabid)==false) return false;

                    var tabid = options.tabid;
                    if (tabid == undefined) tabid = g.getNewTabid();
                    var url = options.url;
                    var content = options.content;
                    var target = options.target;
                    var text = options.text;
                    var showClose = options.showClose;
                    var height = options.height;
                    //如果已经存在
                    if (g.isTabItemExist(tabid))
                    { 
                        return;
                    }
                    var tabitem = $("li[tabid="+targettabid+"]", g.tab.links.ul); 
                    var contentitem = $(".l-tab-content-item[tabid="+targettabid+"]",g.tab.content);
                    if(!tabitem || !contentitem) return ;  
                    tabitem.attr("tabid", tabid);
                    contentitem.attr("tabid", tabid);
                    if($("iframe", contentitem).length==0 && url)
                    {
                        contentitem.html("<iframe frameborder='0'></iframe>");
                    }
                    else if(content)
                    {
                        contentitem.html(content);
                    }
                    $("iframe", contentitem).attr("name", tabid);
                    if (showClose == undefined)  showClose = true; 
                    if (showClose == false) $(".l-tab-links-item-close", tabitem).remove();
                    else{
                        if($(".l-tab-links-item-close", tabitem).length==0)
                           tabitem.append("<div class='l-tab-links-item-close'></div>");
                    }
                    if (text == undefined) text = tabid;
                    if (height) contentitem.height(height);
                    $("a", tabitem).text(text); 
                    $("iframe", contentitem).attr("src", url);


                    p.onAfterOverrideTabItem && p.onAfterOverrideTabItem(targettabid);
                },
                //选中tab项
                selectTabItem: function(tabid)
                {
                    if(p.onBeforeSelectTabItem && p.onBeforeSelectTabItem(tabid)==false) return false;
                    g.selectedTabId = tabid;
                    $("> .l-tab-content-item[tabid=" + tabid + "]", g.tab.content).show().siblings().hide();
                    $("li[tabid=" + tabid + "]", g.tab.links.ul).addClass("l-selected").siblings().removeClass("l-selected");
                    p.onAfterSelectTabItem && p.onAfterSelectTabItem(tabid);
                },
				//移动tab到当前合适的位置
				moveToCurrTabItem: function(tabid)
				{
					var btnWitdth = $(".l-tab-links-left", g.tab.links).width();
					var mainwidth = g.tab.width();
					var selecttableftwidth = 0;
					var selecttabrightwidth = 0;
                    $("li", g.tab.links.ul).each(function()
                    {
						selecttabrightwidth += $(this).width() + p.marginleft;
						if($(this).attr("tabid") == tabid)
						{
							return false;
						}
                        selecttableftwidth += $(this).width() + p.marginleft;
                    });
					var tableftnum = parseInt($(g.tab.links.ul).css("left"));
					if(selecttableftwidth < (0 - tableftnum))
					{
						g.tab.links.ul.animate({ left: -1 * (selecttableftwidth - btnWitdth) });
					}
					else if((selecttabrightwidth + tableftnum) > mainwidth)
					{
						btnWitdth += $(".l-tab-links-tools", g.tab.links).width();
						g.tab.links.ul.animate({ left: (-1 * (selecttabrightwidth - mainwidth + btnWitdth + p.marginleft)) });
					}
				},
                //移动到最后一个tab
                moveToLastTabItem: function()
                {
                    var sumwidth = 0;
                    $("li", g.tab.links.ul).each(function()
                    {
                        sumwidth += $(this).width() + p.marginleft;
                    });
                    var mainwidth = g.tab.width();
                    if (sumwidth > mainwidth)
                    {
                        var btnWitdth = $(".l-tab-links-right", g.tab.links).width() + $(".l-tab-links-tools", g.tab.links).width();
                        g.tab.links.ul.animate({ left: -1 * (sumwidth - mainwidth + btnWitdth + p.marginleft) });
                    }
                },
                //判断tab是否存在
                isTabItemExist: function(tabid)
                {
                    return $("li[tabid=" + tabid + "]", g.tab.links.ul).length > 0;
                },
                //增加一个tab
                addTabItem: function(options)
                {
                    if(p.onBeforeAddTabItem && p.onBeforeAddTabItem(tabid)==false) return false; 
                    var tabid = options.tabid;
                    if (tabid == undefined) tabid = g.getNewTabid();
                    var url = options.url;
					var iconcss = options.iconcss;
                    var content = options.content;
                    var text = options.text;
                    var showClose = options.showClose;
                    var height = options.height;
                    //如果已经存在
                    if (g.isTabItemExist(tabid))
                    {
                        g.selectTabItem(tabid);
						g.moveToCurrTabItem(tabid);
						g.reload(tabid);  //刷新页面
                        return;
                    }
                    var tabitem = $("<li><a></a><div class='l-tab-links-item-left'></div><div class='l-tab-links-item-right'></div><div class='l-tab-links-item-close'></div></li>");
                    var contentitem = $("<div class='l-tab-content-item'><iframe frameborder='0'></iframe></div>");
                    if (g.makeFullHeight)
                    {
                        var newheight = g.tab.height() - g.tab.links.height();
                        contentitem.height(newheight);
                    }
                    tabitem.attr("tabid", tabid);
                    contentitem.attr("tabid", tabid);
                    $("iframe", contentitem).attr("name", tabid);
                    if (showClose == undefined) showClose = true;
                    if (showClose == false) $(".l-tab-links-item-close", tabitem).remove();
                    if (text == undefined) text = tabid;
                    if (height) contentitem.height(height);
                    $("a", tabitem).text(text);
					if (iconcss) $("a", tabitem).addClass(iconcss);
                    $("iframe", contentitem).attr("src", url);
                    g.tab.links.ul.append(tabitem);
                    g.tab.content.append(contentitem);
                    g.selectTabItem(tabid);
                    if (g.setTabButton())
                    {
                        g.moveToLastTabItem();
                    }
                    //增加事件
                    g.addTabItemEvent(tabitem);
                    p.onAfterAddTabItem && p.onAfterAddTabItem(tabid);
                },
                addTabItemEvent: function(tabitem)
                {
                    tabitem.click(function()
                    {
                        var tabid = $(this).attr("tabid");
                        g.selectTabItem(tabid);
                    });
                    //右键事件支持
                    g.tab.menu && po.addTabItemContextMenuEven(tabitem);
                    $(".l-tab-links-item-close", tabitem).hover(function()
                    {
                        $(this).addClass("l-tab-links-item-close-over");
                    }, function()
                    {
                        $(this).removeClass("l-tab-links-item-close-over");
                    }).click(function()
                    {
                        var tabid = $(this).parent().attr("tabid");
                        g.removeTabItem(tabid);
                    });

                },
                //移除tab项
                removeTabItem: function(tabid)
                {
                    if(p.onBeforeRemoveTabItem && p.onBeforeRemoveTabItem(tabid)==false) return false; 
                    var currentIsSelected = $("li[tabid=" + tabid + "]", g.tab.links.ul).hasClass("l-selected");
                    if (currentIsSelected)
                    {
                        $(".l-tab-content-item[tabid=" + tabid + "]", g.tab.content).prev().show();
                        $("li[tabid=" + tabid + "]", g.tab.links.ul).prev().addClass("l-selected").siblings().removeClass("l-selected");
                    }
                    $(".l-tab-content-item[tabid=" + tabid + "]", g.tab.content).remove();
                    $("li[tabid=" + tabid + "]", g.tab.links.ul).remove();
                    g.setTabButton();
                    p.onAfterRemoveTabItem && p.onAfterRemoveTabItem(tabid);
                },
                addHeight: function(heightDiff)
                {
                    var newHeight = g.tab.height() + heightDiff;
                    g.setHeight(newHeight);
                },
                setHeight: function(height)
                {
                    g.tab.height(height);
                    g.setContentHeight();
                },
                setContentHeight: function()
                {
                    var newheight = g.tab.height() - g.tab.links.height();
                    g.tab.content.height(newheight);
                    $("> .l-tab-content-item", g.tab.content).height(newheight);
                },
                getNewTabid: function()
                {
                    var now = new Date();
                    return now.getTime();
                },
                //notabid 过滤掉tabid的
                //noclose 过滤掉没有关闭按钮的
                getTabidList : function(notabid,noclose)
                {
                    var tabidlist = [];
                    $("> li", g.tab.links.ul).each(function(){
                        if($(this).attr("tabid") 
                        && $(this).attr("tabid") != notabid
                        && (!noclose || $(".l-tab-links-item-close",this).length > 0)) 
                        { 
                             tabidlist.push($(this).attr("tabid")); 
                        }
                    });
                    return tabidlist;
                },
                removeOther :function(tabid,compel)
                {
                    var tabidlist = g.getTabidList(tabid,true); 
                    $(tabidlist).each(function(){
                        g.removeTabItem(this);
                    });
                },
                reload :function(tabid)
                {
                      $(".l-tab-content-item[tabid=" + tabid + "] iframe", g.tab.content).each(function(i,iframe){
                            $(iframe).attr("src",$(iframe).attr("src")); 
                      });
                },
                removeAll : function(compel)
                {
                    var tabidlist = g.getTabidList(null,true);
                    $(tabidlist).each(function(){
                        g.removeTabItem(this);
                    }); 
                },
                onResize: function()
                {
                    if (!p.height || typeof (p.height) != 'string' || p.height.indexOf('%') == -1) return false;
                    //set tab height
                    if (g.tab.parent()[0].tagName.toLowerCase() == "body")
                    {
                        var windowHeight = $(window).height();
                        windowHeight -= parseInt(g.tab.parent().css('paddingTop'));
                        windowHeight -= parseInt(g.tab.parent().css('paddingBottom'));
                        g.height = p.heightDiff + windowHeight * parseFloat(g.height) * 0.01;
                    }
                    else
                    {
                        g.height = p.heightDiff + (g.tab.parent().height() * parseFloat(p.height) * 0.01);
                    }
                    g.tab.height(g.height);
                    g.setContentHeight();
                }
            };
            var po = {
                menuItemClick:function(item)
                { 
                    if(!item.id || !g.actionTabid) return; 
                    switch(item.id)
                    {
                         case "close":
                            g.removeTabItem(g.actionTabid);
                            g.actionTabid = null;
                            break;
                        case "closeother":
                            g.removeOther(g.actionTabid);
                            break;
                        case "closeall":
                            g.removeAll();
                            g.actionTabid = null;
                            break;
                        case "reload":
                            g.selectTabItem(g.actionTabid);
                            g.reload(g.actionTabid); 
                            break;
                    }
                },
                addTabItemContextMenuEven:function(tabitem)
                {
                    tabitem.bind("contextmenu",function(e){
                        if(!g.tab.menu) return;
                        g.actionTabid = tabitem.attr("tabid");
                        g.tab.menu.show({ top: e.pageY, left: e.pageX }); 
                        if($(".l-tab-links-item-close",this).length == 0)
                        {
                            g.tab.menu.setDisable('close');
                        }
                        else
                        {
                            g.tab.menu.setEnable('close');
                        }
                        return false;
                    });
                }
            };
            if (p.height) g.makeFullHeight = true;
            g.tab = $(this);
            if (!g.tab.hasClass("l-tab")) g.tab.addClass("l-tab");

            if(p.contextmenu && $.ligerMenu)
            {
                g.tab.menu = $.ligerMenu({width:100,items:[
                    {text:p.closeMessage,id:'close',click:po.menuItemClick},
                    {text:p.closeOtherMessage,id:'closeother',click:po.menuItemClick},
                    {text:p.closeAllMessage,id:'closeall',click:po.menuItemClick},
                    {text:p.reloadMessage,id:'reload',click:po.menuItemClick}
                ]});
            }

            g.tab.content = $('<div class="l-tab-content"></div>');
            $("> div", g.tab).appendTo(g.tab.content);
            g.tab.content.appendTo(g.tab);
            g.tab.links = $('<div class="l-tab-links"><ul style="left: 0px; "></ul></div>');
            g.tab.links.prependTo(g.tab);
			//添加工具栏
			if($(g.tab).attr("toolsid"))
			{
				g.tab.links.append('<div id="' + $(g.tab).attr("toolsid") + '" class="l-tab-links-tools"></div>');
			}
            g.tab.links.ul = $("ul", g.tab.links);
            var haslselected = $("> div[lselected=true]", g.tab.content).length > 0;
            g.selectedTabId = $("> div[lselected=true]", g.tab.content).attr("tabid");
            $("> div", g.tab.content).each(function(i, box)
            {
                var li = $('<li class=""><a></a><div class="l-tab-links-item-left"></div><div class="l-tab-links-item-right"></div></li>'); 
                if ($(box).attr("title"))
                {
                    $("> a", li).html($(box).attr("title"));
                }
				if ($(box).attr("iconcss"))
				{
					$("> a", li).addClass($(box).attr("iconcss"));
				}
                var tabid = $(box).attr("tabid");
                if (tabid == undefined)
                {
                    tabid = g.getNewTabid();
                    $(box).attr("tabid", tabid);
                    if ($(box).attr("lselected"))
                    {
                        g.selectedTabId = tabid;
                    }
                }
                li.attr("tabid", tabid);
                if (!haslselected && i == 0) g.selectedTabId = tabid;
                var showClose = $(box).attr("showClose");
                if (showClose)
                {
                    li.append("<div class='l-tab-links-item-close'></div>");
                }
                $("> ul", g.tab.links).append(li);
                if (!$(box).hasClass("l-tab-content-item")) $(box).addClass("l-tab-content-item");
            });
            //init 
            g.selectTabItem(g.selectedTabId);

            //set content height
            if (p.height)
            {
                if (typeof (p.height) == 'string' && p.height.indexOf('%') > 0)
                {
                    g.onResize();
                    if (p.changeHeightOnResize)
                    {
                        $(window).resize(function()
                        {
                            g.onResize();
                        });
                    }
                } else
                {
                    g.setHeight(p.height);
                }
            }
            if (g.makeFullHeight)
                g.setContentHeight();


            //add even 
            $("li", g.tab.links).each(function()
            {
                g.addTabItemEvent($(this));
            });
            if (this.id == undefined) this.id = "LigerUI_" + new Date().getTime();
            LigerUIManagers[this.id + "_Tab"] = g;
            this.usedTab = true;
        });
        if (this.length == 0) return null;
        if (this.length == 1) return LigerUIManagers[this[0].id + "_Tab"];
        var managers = [];
        this.each(function() {
            managers.push(LigerUIManagers[this.id + "_Tab"]);
        });
        return managers;
    };

})(jQuery);
//ligerTree.js
(function ($)
{
    ///	<param name="$" type="jQuery"></param>

    $.fn.ligerGetTreeManager = function ()
    {
        return LigerUIManagers[this[0].id + "_Tree"];
    }; 
    $.ligerDefaults = $.ligerDefaults || {};
    $.ligerDefaults.Tree = {
            url: null,
            data: null,
            checkbox: true,
            autoCheckboxEven : true,
            parentIcon: 'folder',
            childIcon: 'leaf',
            textFieldName: 'text',
            attribute : ['id','url'],
            treeLine : true,        //是否显示line
            nodeWidth: 90,
            statusName : '__status',
            isLeaf : null, //是否子节点的判断函数
            onBeforeExpand : null,
            onContextmenu : null,
            onExpand : null,
            onBeforeCollapse: null,
            onCollapse : null,
            onBeforeSelect :null,
            onSelect :null,
            onBeforeCancelSelect :null,
            onCancelselect :null,
            onCheck :null,
            onSuccess:null,
            onError:null,
            onClick:null,
            idFieldName : null,
            parentIDFieldName:null,
            topParentIDValue : 0
    };

    $.fn.ligerTree = function (p)
    {
        this.each(function ()
        {
            p = $.extend({},$.ligerDefaults.Tree, p || {});
            if (this.usedTree) return;
            if ($(this).hasClass('l-hidden')) { return; }
            //public Object
            var g = {
                getData :function()
                {
                    return g.data;
                },
                //是否包含子节点
                hasChildren: function (treenodedata)
                {
                    if(p.isLeaf) return p.isLeaf(treenodedata);
                    return treenodedata.children ? true: false;
                },
                //获取父节点
                getParentTreeItem: function (treenode, level)
                {
                    var treeitem = $(treenode);
                    if (treeitem.parent().hasClass("l-tree"))
                        return null;
                    if (level == undefined)
                    {
                        if (treeitem.parent().parent("li").length == 0)
                            return null;
                        return treeitem.parent().parent("li")[0];
                    }
                    var currentLevel = parseInt(treeitem.attr("outlinelevel"));
                    var currenttreeitem = treeitem;
                    for (var i = currentLevel - 1; i >= level; i--)
                    {
                        currenttreeitem = currenttreeitem.parent().parent("li");
                    }
                    return currenttreeitem[0];
                },
                getChecked: function ()
                {
                    if (!p.checkbox) return null;
                    var nodes = [];
                    $(".l-checkbox-checked", g.tree).parent().parent("li").each(function ()
                    {
                        var treedataindex = parseInt($(this).attr("treedataindex"));
                        nodes.push({ target: this,data:po.getDataNodeByTreeDataIndex(g.data,treedataindex) });
                    });
                    return nodes;
                },
                getSelected: function ()
                {
                    var node = {}; 
                    node.target = $(".l-selected", g.tree).parent("li")[0];
                    if (node.target)
                    {
                        var treedataindex = parseInt($(node.target).attr("treedataindex"));
                        node.data = po.getDataNodeByTreeDataIndex(g.data,treedataindex);
                        return node;
                    }
                    return null;
                },
                //升级为父节点级别
                upgrade: function (treeNode)
                {
                    $(".l-note", treeNode).each(function ()
                    {
                        $(this).removeClass("l-note").addClass("l-expandable-open");
                    });
                    $(".l-note-last", treeNode).each(function ()
                    {
                        $(this).removeClass("l-note-last").addClass("l-expandable-open");
                    });
                    $("." + po.getChildNodeClassName(), treeNode).each(function ()
                    {
                        $(this)
                        .removeClass(po.getChildNodeClassName())
                        .addClass(po.getParentNodeClassName(true));
                    });
                },
                //降级为叶节点级别
                demotion: function (treeNode)
                {
                    if (!treeNode && treeNode[0].tagName.toLowerCase() != 'li') return;
                    var islast = $(treeNode).hasClass("l-last");
                    $(".l-expandable-open", treeNode).each(function ()
                    {
                        $(this).removeClass("l-expandable-open")
                        .addClass(islast ? "l-note-last" : "l-note");
                    });
                    $(".l-expandable-close", treeNode).each(function ()
                    {
                        $(this).removeClass("l-expandable-close")
                        .addClass(islast ? "l-note-last" : "l-note");
                    });
                    $("." + po.getParentNodeClassName(true), treeNode).each(function ()
                    {
                        $(this)
                        .removeClass(po.getParentNodeClassName(true))
                        .addClass(po.getChildNodeClassName());
                    });
                },
                collapseAll: function ()
                {
                    $(".l-expandable-open", g.tree).click();
                },
                expandAll: function ()
                {
                    $(".l-expandable-close", g.tree).click();
                },
                loadData: function (node, url,param)
                {
                    g.loading.show(); 
                    param = param || {}; 
                    //请求服务器
                    $.ajax({
                        type: 'post',
                        url: url, 
                        data: param, 
                        dataType: 'json',
                        success: function (data)
                        {  
                            g.loading.hide();
                            g.append(node, data); 
                            if (p.onSuccess) p.onSuccess(data);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown)
                        { 
                            try
                            {
                                g.loading.hide();
                                if (p.onError)
                                    p.onError(XMLHttpRequest, textStatus, errorThrown);
                            }
                            catch (e)
                            {

                            }
                        }
                    });
                },
                //清空
                clear: function ()
                {
                    //g.tree.html("");
                    $("> li",g.tree).each(function(){ g.remove(this);});
                },
                remove: function (treeNode)
                {
                    var treedataindex = parseInt($(treeNode).attr("treedataindex"));
                    var treenodedata = po.getDataNodeByTreeDataIndex(g.data,treedataindex); 
                    if(treenodedata) po.setTreeDataStatus([treenodedata],'delete');
                    var parentNode = g.getParentTreeItem(treeNode);
                    //复选框处理
                    if (p.checkbox)
                    {
                        $(".l-checkbox", treeNode).remove();
                        po.setParentCheckboxStatus($(treeNode));
                    }
                    $(treeNode).remove();
                    if (parentNode == null) //代表顶级节点
                    {
                        parentNode = g.tree.parent();
                    }
                    //set parent
                    var treeitemlength = $("> ul > li", parentNode).length;
                    if (treeitemlength > 0)
                    {
                        //遍历设置子节点
                        $("> ul > li", parentNode).each(function (i, item)
                        {
                            if (i == 0 && !$(this).hasClass("l-first"))
                                $(this).addClass("l-first");
                            if (i == treeitemlength - 1 && !$(this).hasClass("l-last"))
                                $(this).addClass("l-last");
                            if (i == 0 && i == treeitemlength - 1 && !$(this).hasClass("l-onlychild"))
                                $(this).addClass("l-onlychild");
                            $("> div .l-note,> div .l-note-last", this)
                           .removeClass("l-note l-note-last")
                           .addClass(i == treeitemlength - 1 ? "l-note-last" : "l-note");
                            po.setTreeItem(this, { isLast: i == treeitemlength - 1 });
                        });
                    }  

                },
                //增加节点集合
                append: function (parentNode, newdata)
                {
                    if (!newdata || !newdata.length) return false;
                    if(p.idFieldName && p.parentIDFieldName)
                        newdata = po.convertData(newdata); 
                    po.addTreeDataIndexToData(newdata);
                    po.setTreeDataStatus(newdata,'add'); 
                    po.appendData(parentNode,newdata);   
                    if (!parentNode)//增加到根节点
                    { 
                        //remove last node class
                        if ($("> li:last", g.tree).length > 0)
                            po.setTreeItem($("> li:last", g.tree)[0], { isLast: false }); 

                        var gridhtmlarr = po.getTreeHTMLByData(newdata,1,[],true); 
                        gridhtmlarr[gridhtmlarr.length-1] = gridhtmlarr[0] = "";  
                        g.tree.append(gridhtmlarr.join(''));

                        $(".l-body", g.tree).hover(function ()
                        {
                            $(this).addClass("l-over");
                        }, function ()
                        {
                            $(this).removeClass("l-over");
                        }); 

                        po.upadteTreeWidth(); 
                        return;
                    }
                    var treeitem = $(parentNode);
                    var outlineLevel = parseInt(treeitem.attr("outlinelevel"));

                    var hasChildren = $("> ul", treeitem).length > 0;
                    if (!hasChildren)
                    {
                        treeitem.append("<ul class='l-children'></ul>");
                        //设置为父节点
                        g.upgrade(parentNode);
                    }
                    //remove last node class  
                    if ($("> .l-children > li:last", treeitem).length > 0)
                        po.setTreeItem($("> .l-children > li:last", treeitem)[0], { isLast: false });

                    var isLast = [];
                    for (var i = 1; i <= outlineLevel - 1; i++)
                    {
                        var currentParentTreeItem = $(g.getParentTreeItem(parentNode, i));
                        isLast.push(currentParentTreeItem.hasClass("l-last"));
                    }
                    isLast.push(treeitem.hasClass("l-last"));
                    var gridhtmlarr = po.getTreeHTMLByData(newdata,outlineLevel+1,isLast,true); 
                    gridhtmlarr[gridhtmlarr.length-1] = gridhtmlarr[0] = "";  
                    $(">.l-children",parentNode).append(gridhtmlarr.join(''));

                    po.upadteTreeWidth(); 

                    $(">.l-children .l-body", parentNode).hover(function ()
                    {
                        $(this).addClass("l-over");
                    }, function ()
                    {
                        $(this).removeClass("l-over");
                    }); 
                }
            };
            //private Object
            var po = { 
                //根据数据索引获取数据
                getDataNodeByTreeDataIndex:function(data,treedataindex)
                { 
                    for(var i =0;i<data.length;i++)
                    {
                        if(data[i].treedataindex == treedataindex)
                            return data[i];
                        if(data[i].children)
                        {
                            var targetData= po.getDataNodeByTreeDataIndex(data[i].children,treedataindex);
                            if(targetData) return targetData;
                        }
                    }
                    return null;
                },
                //设置数据状态
                setTreeDataStatus : function(data,status)
                {
                    $(data).each(function ()
                    { 
                        this[p.statusName] = status;
                        if(this.children)
                        {
                            po.setTreeDataStatus(this.children,status);
                        }
                    });
                }, 
                //设置data 索引
                addTreeDataIndexToData : function(data)
                {
                    $(data).each(function ()
                    {
                        if(this.treedataindex != undefined) return;
                        this.treedataindex = g.treedataindex++;
                        if(this.children)
                        {
                            po.addTreeDataIndexToData(this.children);
                        }
                    });
                },
                //添加项到g.data
                appendData: function (treeNode,data)
                {
                    var treedataindex = parseInt($(treeNode).attr("treedataindex"));
                    var treenodedata = po.getDataNodeByTreeDataIndex(g.data,treedataindex); 
                    if(g.treedataindex == undefined) g.treedataindex = 0; 
                    if(treenodedata && treenodedata.children == undefined) treenodedata.children =[];
                    $(data).each(function (i,item)
                    {
                        if(treenodedata)
                            treenodedata.children[treenodedata.children.length] = $.extend({},item); 
                        else
                            g.data[g.data.length] =  $.extend({},item); 
                    });
                },
                setTreeItem: function (treeNode, options)
                {
                    if (!options) return;
                    var treeItem = $(treeNode);
                    var outlineLevel = parseInt(treeItem.attr("outlinelevel"));
                    if (options.isLast != undefined)
                    {
                        if (options.isLast == true)
                        {
                            treeItem.removeClass("l-last").addClass("l-last");
                            $("> div .l-note", treeItem).removeClass("l-note").addClass("l-note-last");
                            $(".l-children li", treeItem)
                            .find(".l-box:eq(" + (outlineLevel - 1) + ")")
                            .removeClass("l-line");
                        }
                        else if (options.isLast == false)
                        {
                            treeItem.removeClass("l-last");
                            $("> div .l-note-last", treeItem).removeClass("l-note-last").addClass("l-note");

                            $(".l-children li", treeItem)
                            .find(".l-box:eq(" + (outlineLevel - 1) + ")")
                            .removeClass("l-line")
                            .addClass("l-line");
                        }
                    }
                }, 
                upadteTreeWidth: function ()
                {
                    var treeWidth = g.maxOutlineLevel * 22;
                    if (p.checkbox) treeWidth += 22;
                    if (p.parentIcon || p.childIcon) treeWidth += 22;
                    treeWidth += p.nodeWidth;  
                    g.tree.width(treeWidth);  
                },
                getChildNodeClassName: function ()
                {
                    return 'l-tree-icon-' + p.childIcon;
                },
                getParentNodeClassName: function (isOpen)
                {
                    var nodeclassname = 'l-tree-icon-' + p.parentIcon;
                    if (isOpen) nodeclassname += '-open';
                    return nodeclassname;
                },
                //根据data生成最终完整的tree html
                getTreeHTMLByData:function(data,outlineLevel ,isLast,isExpand)
                {  
                    if (g.maxOutlineLevel < outlineLevel)
                        g.maxOutlineLevel = outlineLevel; 
                    isLast = isLast || [];
                    outlineLevel = outlineLevel || 1;
                    var treehtmlarr = [];
                    if(!isExpand) treehtmlarr.push('<ul class="l-children" style="display:none">');
                    else treehtmlarr.push("<ul class='l-children'>");
                    for (var i = 0; i < data.length; i++)
                    {
                        var isFirst = i==0;
                        var isLastCurrent = i==data.length-1; 
                        var isExpandCurrent = true;
                        if(data[i].isexpand==false || data[i].isexpand == "false") isExpandCurrent = false;
                            
                        treehtmlarr.push('<li ');
                        if(data[i].treedataindex!=undefined)
                            treehtmlarr.push('treedataindex="'+data[i].treedataindex+'" ');
                        if(isExpandCurrent)
                            treehtmlarr.push('isexpand='+data[i].isexpand +' ');
                        treehtmlarr.push('outlinelevel='+outlineLevel +' '); 
                        //增加属性支持
                        for(var j=0;j<g.sysAttribute.length;j++)
                        { 
                            if($(this).attr(g.sysAttribute[j]))
                                data[dataindex][g.sysAttribute[j]] = $(this).attr(g.sysAttribute[j]);
                        }
                        for(var j=0;j<p.attribute.length;j++)
                        {
                            if(data[i][p.attribute[j]])
                                treehtmlarr.push(p.attribute[j] + '="'+data[i][p.attribute[j]]+'" '); 
                        }
                       
                        //css class
                        treehtmlarr.push('class="');
                        isFirst && treehtmlarr.push('l-first ');
                        isLastCurrent && treehtmlarr.push('l-last ');
                        isFirst && isLastCurrent && treehtmlarr.push('l-onlychild ');
                        treehtmlarr.push('"');  
                        treehtmlarr.push('>');
                        treehtmlarr.push('<div class="l-body">'); 
                        for (var k = 0; k <= outlineLevel - 2; k++)
                        {
                            if(isLast[k]) treehtmlarr.push('<div class="l-box"></div>');
                            else treehtmlarr.push('<div class="l-box l-line"></div>'); 
                        } 
                        if (g.hasChildren(data[i]))
                        {
                            if(isExpandCurrent) treehtmlarr.push('<div class="l-box l-expandable-open"></div>');  
                            else treehtmlarr.push('<div class="l-box l-expandable-close"></div>');  
                            if(p.checkbox)
                            {
                                if(data[i].ischecked)
                                    treehtmlarr.push('<div class="l-box l-checkbox l-checkbox-checked"></div>');
                                else
                                    treehtmlarr.push('<div class="l-box l-checkbox l-checkbox-unchecked"></div>');
                            }

                            p.parentIcon && !isExpandCurrent &&  treehtmlarr.push('<div class="l-box ' + po.getParentNodeClassName() + '"></div>');
                            p.parentIcon && isExpandCurrent &&  treehtmlarr.push('<div class="l-box ' + po.getParentNodeClassName(true) + '"></div>');
                        } 
                        else
                        {
                            if(isLastCurrent) treehtmlarr.push('<div class="l-box l-note-last"></div>');
                            else treehtmlarr.push('<div class="l-box l-note"></div>');
                            if(p.checkbox)
                            {
                                if(data[i].ischecked)
                                    treehtmlarr.push('<div class="l-box l-checkbox l-checkbox-checked"></div>');
                                else
                                    treehtmlarr.push('<div class="l-box l-checkbox l-checkbox-unchecked"></div>');
                            }
                            p.childIcon && treehtmlarr.push('<div class="l-box ' + po.getChildNodeClassName() + '"></div>'); 
                        } 

                        treehtmlarr.push('<span>' + data[i][p.textFieldName] + '</span></div>');
                        if (g.hasChildren(data[i]))
                        {  
                            var isLastNew = [];
                            for(var k=0;k<isLast.length;k++)
                            {
                                isLastNew.push(isLast[k]);
                            }
                            isLastNew.push(isLastCurrent);
                            treehtmlarr.push(po.getTreeHTMLByData(data[i].children,outlineLevel+1,isLastNew,isExpandCurrent).join(''));
                        }
                        treehtmlarr.push('</li>');
                    }
                    treehtmlarr.push("</ul>"); 
                    return treehtmlarr;
                    
                },
                //根据简洁的html获取data
                getDataByTreeHTML : function(treeDom)
                {
                    var data = [];
                    $("> li", treeDom).each(function (i, item)
                    {
                        var dataindex = data.length;
                        data[dataindex] = 
                        { 
                            treedataindex:g.treedataindex++
                        }; 
                        data[dataindex][p.textFieldName] = $("> span,> a",this).html();
                        for(var j=0;j<g.sysAttribute.length;j++)
                        { 
                            if($(this).attr(g.sysAttribute[j]))
                                data[dataindex][g.sysAttribute[j]] = $(this).attr(g.sysAttribute[j]);
                        }
                        for(var j=0;j<p.attribute.length;j++)
                        { 
                            if($(this).attr(p.attribute[j]))
                                data[dataindex][p.attribute[j]] = $(this).attr(p.attribute[j]);
                        }
                        if($("> ul",this).length>0)
                        { 
                            data[dataindex].children = po.getDataByTreeHTML($("> ul",this));
                        }
                    });
                    return data;
                },
                applyTree: function ()
                {  
                    g.data = po.getDataByTreeHTML(g.tree);   
                    var gridhtmlarr = po.getTreeHTMLByData(g.data,1,[],true); 
                    gridhtmlarr[gridhtmlarr.length-1] = gridhtmlarr[0] = "";  
                    g.tree.html(gridhtmlarr.join('')); 
                    po.upadteTreeWidth();
                    $(".l-body", g.tree).hover(function ()
                    {
                        $(this).addClass("l-over");
                    }, function ()
                    {
                        $(this).removeClass("l-over");
                    }); 
                },
                applyTreeEven: function (treeNode)
                { 
                    $("> .l-body", treeNode).hover(function ()
                    {
                        $(this).addClass("l-over");
                    }, function ()
                    {
                        $(this).removeClass("l-over");
                    }); 
                },
                setTreeEven : function()
                {
                    p.onContextmenu && g.tree.bind("contextmenu",function(e){ 
                        var obj = (e.target || e.srcElement);
                        var treeitem = null;
                        if(obj.tagName.toLowerCase() == "a" || obj.tagName.toLowerCase()=="span" || $(obj).hasClass("l-box"))
                            treeitem = $(obj).parent().parent();  
                        else if($(obj).hasClass("l-body"))
                            treeitem = $(obj).parent();
                        else if(obj.tagName.toLowerCase() == "li")
                            treeitem = $(obj);
                        if(!treeitem) return;
                        var treedataindex = parseInt(treeitem.attr("treedataindex"));
                        var treenodedata = po.getDataNodeByTreeDataIndex(g.data,treedataindex); 
                        return p.onContextmenu({data:treenodedata,target:treeitem[0]},e);
                    });
                    g.tree.click(function (e)
                    { 
                        var obj = (e.target || e.srcElement);
                        var treeitem = null;
                        if(obj.tagName.toLowerCase() == "a" || obj.tagName.toLowerCase()=="span" || $(obj).hasClass("l-box"))
                            treeitem = $(obj).parent().parent();  
                        else if($(obj).hasClass("l-body"))
                            treeitem = $(obj).parent();
                        else
                            treeitem = $(obj);
                        if(!treeitem) return;
                        var treedataindex = parseInt(treeitem.attr("treedataindex"));
                        var treenodedata = po.getDataNodeByTreeDataIndex(g.data,treedataindex);                                var treeitembtn = $(".l-body:first .l-expandable-open:first,.l-body:first .l-expandable-close:first",treeitem); 
                       if(!$(obj).hasClass("l-checkbox"))
                       {
                           if ($(">div:first",treeitem).hasClass("l-selected"))
                            {
                                if(p.onBeforeCancelSelect 
                                && p.onBeforeCancelSelect({data:treenodedata,target:treeitem[0]}) == false)
                                    return false;
                                $(">div:first",treeitem).removeClass("l-selected");
                                p.onCancelSelect && p.onCancelSelect({data:treenodedata,target:treeitem[0]});
                            }
                            else
                            { 
                                if(p.onBeforeSelect 
                                && p.onBeforeSelect({data:treenodedata,target:treeitem[0]}) == false)
                                    return false;
                                $(".l-body", g.tree).removeClass("l-selected");
                                $(">div:first",treeitem).addClass("l-selected");
                                    p.onSelect && p.onSelect({data:treenodedata,target:treeitem[0]});
                            } 
                       }
                        //chekcbox even
                        if ($(obj).hasClass("l-checkbox"))
                        {
                            if(p.autoCheckboxEven)
                            {
                                //状态：未选中
                                if ($(obj).hasClass("l-checkbox-unchecked"))
                                {
                                    $(obj).removeClass("l-checkbox-unchecked").addClass("l-checkbox-checked");
                                    $(".l-children .l-checkbox", treeitem)
                                    .removeClass("l-checkbox-incomplete l-checkbox-unchecked")
                                    .addClass("l-checkbox-checked");
                                    p.onCheck && p.onCheck({data:treenodedata,target:treeitem[0]},true);
                                }
                                //状态：选中
                                else if ($(obj).hasClass("l-checkbox-checked"))
                                {
                                    $(obj).removeClass("l-checkbox-checked").addClass("l-checkbox-unchecked");
                                    $(".l-children .l-checkbox", treeitem)
                                    .removeClass("l-checkbox-incomplete l-checkbox-checked")
                                    .addClass("l-checkbox-unchecked");
                                    p.onCheck && p.onCheck({data:treenodedata,target:treeitem[0]},false);
                                }
                                //状态：未完全选中
                                else if ($(obj).hasClass("l-checkbox-incomplete"))
                                {
                                    $(obj).removeClass("l-checkbox-incomplete").addClass("l-checkbox-checked");
                                    $(".l-children .l-checkbox", treeitem)
                                    .removeClass("l-checkbox-incomplete l-checkbox-unchecked")
                                    .addClass("l-checkbox-checked");
                                    p.onCheck && p.onCheck({data:treenodedata,target:treeitem[0]},true);
                                }
                                po.setParentCheckboxStatus(treeitem);
                            }
                        } 
                        //状态：已经张开
                        else if (treeitembtn.hasClass("l-expandable-open"))
                        {
                            if(p.onBeforeCollapse 
                            && p.onBeforeCollapse({data:treenodedata,target:treeitem[0]}) == false)
                                return false;
                            treeitembtn
                            .removeClass("l-expandable-open")
                            .addClass("l-expandable-close");
                            $("> .l-children", treeitem).slideToggle('fast');
                            $("> div ." + po.getParentNodeClassName(true), treeitem)
                            .removeClass(po.getParentNodeClassName(true))
                            .addClass(po.getParentNodeClassName());
                            p.onCollapse && p.onCollapse({data:treenodedata,target:treeitem[0]});
                        }
                        //状态：没有张开
                        else if (treeitembtn.hasClass("l-expandable-close"))
                        { 
                            if(p.onBeforeExpand 
                            && p.onBeforeExpand({data:treenodedata,target:treeitem[0]}) == false)
                                return false;
                            treeitembtn
                            .removeClass("l-expandable-close")
                            .addClass("l-expandable-open");
                            $("> .l-children", treeitem).slideToggle('fast',function(){
                                p.onExpand && p.onExpand({data:treenodedata,target:treeitem[0]});
                            });
                            $("> div ." + po.getParentNodeClassName(), treeitem)
                            .removeClass(po.getParentNodeClassName())
                            .addClass(po.getParentNodeClassName(true)); 
                        }
                        p.onClick && p.onClick({data:treenodedata,target:treeitem[0]});
                    });   
                },
                //递归设置父节点的状态
                setParentCheckboxStatus: function (treeitem)
                {
                    //当前同级别或低级别的节点是否都选中了
                    var isCheckedComplete = $(".l-checkbox-unchecked", treeitem.parent()).length == 0;
                    //当前同级别或低级别的节点是否都没有选中
                    var isCheckedNull = $(".l-checkbox-checked", treeitem.parent()).length == 0;
                    if (isCheckedComplete)
                    {
                        treeitem.parent().prev().find(".l-checkbox")
                                    .removeClass("l-checkbox-unchecked l-checkbox-incomplete")
                                    .addClass("l-checkbox-checked");
                    }
                    else if (isCheckedNull)
                    {
                        treeitem.parent().prev().find("> .l-checkbox")
                                    .removeClass("l-checkbox-checked l-checkbox-incomplete")
                                    .addClass("l-checkbox-unchecked");
                    }
                    else
                    {
                        treeitem.parent().prev().find("> .l-checkbox")
                                    .removeClass("l-checkbox-unchecked l-checkbox-checked")
                                    .addClass("l-checkbox-incomplete");
                    }
                    if (treeitem.parent().parent("li").length > 0)
                        po.setParentCheckboxStatus(treeitem.parent().parent("li"));
                },
                convertData:function(data)      //将ID、ParentID这种数据格式转换为树格式
                {
                    if(!data || !data.length) return []; 
                    var isolate = function(pid)//根据ParentID判断是否孤立
                    {
                        if(pid == p.topParentIDValue) return false;
                        for(var i=0;i<data.length;i++)
                        {
                            if(data[i][p.idFieldName] == pid) return false;
                        }
                        return true;
                    };
                    //计算孤立节点的个数
                    var isolateLength =0;
                    for(var i=0;i<data.length;i++)
                    {
                        if(isolate(data[i][p.parentIDFieldName])) isolateLength++;
                    } 
                    var targetData = [];                    //存储数据的容器(返回)
                    var itemLength = data.length;           //数据集合的个数
                    var insertedLength = 0;                 //已插入的数据个数
                    var currentIndex = 0;                   //当前数据索引
                    var getItem = function(container,id)    //获取数据项(为空时表示没有插入)
                    {
                        if(!container.length) return null; 
                        for(var i=0;i<container.length;i++)
                        {
                            if(container[i][p.idFieldName] == id) return container[i];
                            if(container[i].children)
                            {
                                var finditem = getItem(container[i].children,id);
                                if(finditem) return finditem;
                            }
                        }
                        return null;
                    }; 
                    var addItem = function(container,item)  //插入数据项
                    {
                        container.push($.extend({},item));
                        insertedLength++;
                    };
                    //判断已经插入的节点和孤立节点 的个数总和是否已经满足条件
                    while(insertedLength + isolateLength <itemLength)
                    { 
                        var item = data[currentIndex];
                        var id = item[p.idFieldName];
                        var pid = item[p.parentIDFieldName];
                        if(pid == p.topParentIDValue)//根节点
                        {
                            getItem(targetData,id) == null && addItem(targetData,item);
                        }
                        else
                        {
                            var pitem = getItem(targetData,pid); 
                            if(pitem && getItem(targetData,id) == null)//找到父节点数据并且还没插入
                            {  
                                pitem.children =  pitem.children || [];
                                addItem(pitem.children,item);
                            }
                        }
                        currentIndex = (currentIndex+1)%itemLength;
                    }  
                    return targetData; 
                }
            };
            if (!$(this).hasClass('l-tree')) $(this).addClass('l-tree');
            g.tree = $(this);
            if(!p.treeLine) g.tree.addClass("l-tree-noline");
            g.sysAttribute = ['isexpand','ischecked','href','style'];
            g.loading = $("<div class='l-tree-loading'></div>");
            g.tree.after(g.loading);
            g.data = [];
            g.maxOutlineLevel = 1;
            g.treedataindex = 0;
            po.applyTree();
            po.setTreeEven();
            if (p.data)
            { 
                g.append(null, p.data);
            }
            if (p.url)
            {
                g.loadData(null, p.url);
            }
            if (this.id == undefined || this.id == "") this.id = "LigerUI_" + new Date().getTime();
            LigerUIManagers[this.id + "_Tree"] = g;
            this.usedTree = true;
        });
        if (this.length == 0) return null;
        if (this.length == 1) return LigerUIManagers[this[0].id + "_Tree"];
        var managers = [];
        this.each(function() {
            managers.push(LigerUIManagers[this.id + "_Tree"]);
        });
        return managers;
    };

})(jQuery);