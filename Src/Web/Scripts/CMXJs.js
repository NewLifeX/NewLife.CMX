/*获取URL参数*/
function getUrlParam(name)
    {
        var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r!=null) return unescape(r[2]); return null; //返回参数值
}

/*设置传递参数*/
$(function () {
    var href = $('.listpage').attr('href');

    var param = location.search;

    $('.listpage').attr('href', href + param);
});