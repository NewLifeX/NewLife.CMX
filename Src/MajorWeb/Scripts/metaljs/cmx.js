
//菜单显示状态
$(function () {
    //获取当前请求路径
    var pathname = location.pathname;
    //正则表达式
    var regstr = /Page_([^\./_]+).html/;

    if (regstr) {
        var currentpage = pathname.match(regstr)[1];
        //设置当前页面菜单被选中
        $('#' + currentpage).addClass('current');
    }
});
