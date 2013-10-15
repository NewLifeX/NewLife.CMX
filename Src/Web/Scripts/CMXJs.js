/*获取URL参数*/
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}

$(function () {
    /*设置添加按钮传递参数*/
    if ($('.listpage')[0] != null) {
        var href = $('.listpage').attr('href');
        var param = location.search;

        //IE10 不支持string.contains()方法
        //if (!href.contains(param)) {
        //    $('.listpage').attr('href', href + param);
        //}

        if (href.indexOf(param) < 0) {
            $('.listpage').attr('href', href + param);
        }
    }

    /*设置编辑已有的记录跳转是传递参数*/
    if ($('.formUrl')[0] != null) {
        $('.formUrl').each(function () {
            var href = $(this).attr('href');
            var param = location.search.substr(1);

            //IE10 不支持string.contains()方法
            //if (!href.contains(param)) {
            //    $(this).attr('href', href + '&' + param);
            //}

            if (href.indexOf(param) < 0) {
                $(this).attr('href', href + '&' + param);
            }
        });
    }
});

//function UpLoadFile(buttonID, formID, customPath, targetControlID, successFunction, errorFunction) {
//    $('#' + buttonID).click(function () {

//        successFunction = successFunction ? successFunction : Defautsuccess;
//        errorFunction = errorFunction ? errorFunction : Defaulterror;
//        customPath = customPath ? customPath : '';

//        $('#' + formID).ajaxSubmit({
//            url: 'UpdateImageLoad.ashx?CustomImagePath=' + customPath,
//            type: "post",
//            target: "#" + targetControlID,
//            success: Defautsuccess,
//            error: Defaulterror,
//        });
//    });
//}

//function Defaulterror(XMLHttpRequest, textStatus, errorThrown) {
//    alert("error:" + errorThrown);
//}

//function Defautsuccess(responseText, statusText, xhr, $form) {
//    var i = responseText;
//    alert(responseText);
//}