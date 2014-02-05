/*导航子菜单*/
$(function () {
    $('.child ul').addClass('close');/*查找ID为.child li的ID，并修改它的class（类）为colse类*/

    /*解决鼠标移出菜单后隐藏二级菜单*/
    $('.nav').mouseleave(function () {
        $('.child ul').removeClass('open');
        $('.parent li').removeClass('ulhover');
    });

    $(".parent li").each(function () {
        var $title = $(this);
        var key = $title.attr('class');
        var ulkey = '.child .' + key;

        $title.mouseover(function () {
            $('.child ul').removeClass('open');
            $(ulkey).addClass('open');
            $title.addClass('ulhover');
        });

        $title.mouseleave(function () {
            $('.parent li').removeClass('ulhover');
        });

        $('.child ul').mouseover(function () {
            b = false;
            var keys = $(this).attr('class');
            var keyarray = keys.split(/\s+/);
            var pks = $('.parent li');

            $(keyarray).each(function () {
                var ak = this;
                $('.parent li').each(function () {
                    if ($(this).attr('class') == ak) {
                        $(this).addClass('ulhover');
                    }
                });
            });
        });

        //$('.child ul').mouseleave(function () {
        //    $('.parent li').each(function () {
        //        $(this).removeClass('ulhover');
        //    });
        //    $(this).removeClass('open');
        //});
    });
    /*实现展开收缩符号功能.......................................................*/
    //menulist();

    $('#faq dt').click(function () {
        icoPlusAndMinus();
    });

    function icoPlusAndMinus() {
        var icoclass = $('.ico').attr('class');
        if (icoclass.indexOf('icoMinus') == -1) {
            $('.ico').addClass('icoMinus');
        }
        else {
            $('.ico').removeClass('icoMinus');
        }
    }

    //列表页中的go跳转
    $('.page-num').change(function () {
        if (parseInt($('.page-num').val()) && parseInt($('.page-num').val()) <= parseInt($('.pagecount').text())) {
            var targetUrl = $('.page-num').attr('tarurl');
            $.get(
                targetUrl,
                { PageNum: $('.page-num').val(), urldata: location.href, pagecount: $('.pagecount').text() },
                function (data) {
                    $('.goPage').attr('href', data);
                }
            );
        }
    });
});

/*实现展开收缩符号功能.......................................................*/
//function getObject(objectId) {
//    if (document.getElementById && document.getElementById(objectId)) {
//        return document.getElementById(objectId);
//    } else if (document.all && document.all(objectId)) {
//        return document.all(objectId);
//    } else if (document.layers && document.layers[objectId]) {
//        return document.layers[objectId];
//    } else {
//        return false;
//    }
//}

//function showHide(e, objname) {
//    var obj = getObject(objname);
//    if (obj.style.display == "none") {
//        obj.style.display = "block";
//        e.className = "minus";
//    } else {
//        obj.style.display = "none";
//        e.className = "plus";
//    }
//}

function showHideNew(e, objname) {
    var thisobj = $(e);
    var rootmenu = thisobj.parents('.list_meu');

    var ddchild = $('#' + objname);

    if (thisobj.attr('class') == 'plus') {

        if (rootmenu.find('.minus')) {
            rootmenu.find('.minus').parents('dl').find('dd').css('display', 'none');
            rootmenu.find('.minus').attr('class', 'plus');
        }

        thisobj.attr('class', 'minus');
        ddchild.css('display', 'block');
    }
    else if (thisobj.attr('class') == 'minus') {
        thisobj.attr('class', 'plus');
        ddchild.css('display', 'none');
    }
}
/*flash*/
(function ($) {
    $.fn.extend({
        "nav": function (con) {
            var $this = $(this), $nav = $this.find('.switch-tab'), t = (con && con.t) || 3000, a = (con && con.a) || 500, i = 0, autoChange = function () {
                $nav.find('a:eq(' + (i + 1 === 5 ? 0 : i + 1) + ')').addClass('current').siblings().removeClass('current');
                $this.find('.event-item:eq(' + i + ')').css('display', 'none').end().find('.event-item:eq(' + (i + 1 === 5 ? 0 : i + 1) + ')').css({
                    display: 'block',
                    opacity: 0
                }).animate({
                    opacity: 1
                }, a, function () {
                    i = i + 1 === 5 ? 0 : i + 1;
                }).siblings('.event-item').css({
                    display: 'none',
                    opacity: 0
                });
            }, st = setInterval(autoChange, t);
            $this.hover(function () {
                clearInterval(st);
                return false;
            }, function () {
                st = setInterval(autoChange, t);
                return false;
            }).find('.switch-nav>a').bind('click', function () {
                var current = $nav.find('.current').index();
                i = $(this).attr('class') === 'prev' ? current - 2 : current;
                autoChange();
                return false;
            }).end().find('.switch-tab>a').bind('click', function () {
                i = $(this).index() - 1;
                autoChange();
                return false;
            });
            return $this;
        }
    });
}(jQuery));

$(document).ready(function () {
    $('.hot-event').nav({
        t: 5500,	//轮播时间
        a: 1500  //过渡时间
    });
});