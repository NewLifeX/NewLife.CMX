
$(document).ready(function(){
//全局函数
var win_w
var win_h
//添加class	
$(".tab_bon").each(function(){
	$(this).find("li:last").css({marginRight:0});
	})

$(".webuser ul li").eq(0).addClass("webuer_01").find("i").html("01");
$(".webuser ul li").eq(1).addClass("webuer_02").find("i").html("02")
$(".webuser ul li").eq(2).addClass("webuer_03").find("i").html("03");


//list_
	$(".list_news_pic li:nth-child(2n)").addClass("nfor")
	$(".txt_list0531_box li:odd").addClass("odd")
//


//ie6hover 事件
if ( $.browser.msie )
{
	$( $.browser.version="6.0")
	{
		$(".nav_links .links_list span .linkdl").hover(function(){
			$(this).find("dd").css({"display":"block"})
		}, function(){
			$(this).find("dd").css({"display":"none"})
		})
	}
}
//


$(window).resize(function(){
	win_w=$(window).width()
	if(win_w<940)
	{
		//首页
		$(".in_bg").addClass("min_body")
		$(".main_right .right_txt").prependTo(".main_left");
		$(".main_right .search").prependTo(".main_left");
		$(".main_right .right_news ").insertAfter(".banner_0");
		$(".main_right .xinfang").appendTo(".main_left");
		//焦点图高度
		$(".banner_0").height($(".banner_0 .banner_0_top li").height())
		$(".banner_0_top li .tit").css({"bottom":$(".banner_0_top li .p").outerHeight()});
		$(".banner_0_bottom").css({"bottom":$(".banner_0_top li .p").outerHeight()+$(".banner_0_top li .tit").outerHeight()});
		$(".header_warp .logo img").css({width:"98%"})
		if(win_w<540)
		{
			$(".in_bg").addClass("min_body480");
		}
		else
		{
			$(".in_bg").removeClass("min_body480");
		}
		
		//二级页
		$(".other_bg").addClass("other_body");
		
		//图片新闻列表
		$(".list_news_pic li").removeClass("nfor")
		$(".list_news_pic2 li").removeClass("nfor")
		$(".list_news_pic4 li").removeClass("nfor")
		
	}
	else
	{
		//首页
		$(".header_warp .logo img").css({width:"auto"})
		$(".banner_0").css({height:"auto"});
		$(".in_bg").removeClass("min_body");
		$(".in_bg").removeClass("min_body480");
		$(".main_left .xinfang").prependTo(".main_right");
		$(".main_left .right_news ").prependTo(".main_right");
		$(".main_left .right_txt").prependTo(".main_right");
		$(".main_left .search").prependTo(".main_right");
		
		//二级页
		$(".other_bg").removeClass("other_body");
		
		//图片新闻列表
		$(".list_news_pic li:nth-child(2n)").addClass("nfor");
		$(".list_news_pic2 li:nth-child(3n)").addClass("nfor");
		$(".list_news_pic4 li:nth-child(3n)").addClass("nfor");
	}
	
	
	
	
})

$(window).resize()

//tab 选显卡效果
$(".tab_a").each(function(q){
	$(".tab_a").eq(q).find(".tab_bon li").mouseover(function(e){
		var numl=$(this).index()
		$(".tab_a").eq(q).find(".tab_bon li").removeClass("on")
		$(".tab_a").eq(q).find(".tab_bon li").eq(numl).addClass("on")
		$(".tab_a").eq(q).find(".tab_list").removeClass("show")
		$(".tab_a").eq(q).find(".tab_list").eq(numl).addClass("show")
		})
	})
//end



//廉政楷模选项卡
$(".other_center_r1 .tith2 span").each(function(e){
	$(this).click(function(){
		$(".other_center_r1 .tith2 span").removeClass("on")
		$(this).addClass("on")
		$(".other_center_r1 .pic2_list00").hide();
		$(".other_center_r1 .pic2_list00").eq(e).show();
		})
})
//


//图片报道切换
$(".img_list0531").each(function(){
	var nul=$(this).find("dt li").size();
	var lx=Math.ceil(nul/4);
	for(var i=1;i<=lx;i++)
	{
		$(this).find("dd").append("<span>"+ i +"</span>")
	}
	$(this).find("dd span:first").addClass("on")
	
	$(this).find("dd span").each(function(cc){
			$(this).click(function(){
				$(".img_list0531 dd span").removeClass("on")
				$(this).addClass("on")
				$(".img_list0531 dt ul").css({top:0-(cc*(($(".img_list0531 dt li").height()+10)*4))})	
			})
	})
})
//end



})




//无图图像
var nullimg='../images/error.jpg';
function lod(t){
	t.onerror = null;
	t.src=nullimg
}
$(document).ready(function(){
	$("img").each(function(){
	if($(this).attr("src")=="")
	{
		$(this).attr({"src":nullimg})
	}
	})
})
//end




//首页banner
$(function(){
	var xxxban=function(){
	var list
	var imgul=$(".banner_0_top")
	var bonul=$(".banner_0_bottom")
	var numx=0
	var maxnum=$(".banner_0_top li").size()-1;
	var speed=4
	imgul.find("li:gt(0)").css({opacity:0});
	imgul.find("li:eq(0)").css({zIndex:5})
	bonul.find("span").eq(0).addClass("on");
	imgul.find("li").eq(0).find(".tit").css({"bottom":imgul.find("li").eq(0).find(".p").outerHeight()});	
	bonul.find("span").each(function(e){
		$(this).click(function(){
			if(e==numx)
			{
				return false;
			}
			else
			{
			bonul.find("span").removeClass("on")
			bonul.find("span").eq(e).addClass("on")
			imgul.find("li").eq(numx).animate({opacity:0,zIndex:0});
			imgul.find("li").eq(e).animate({opacity:1,zIndex:maxnum});	
			imgul.find("li").eq(e).find(".tit").css({"bottom":imgul.find("li").eq(e).find(".p").outerHeight()});		
			numx=e
			}
			})
		})
		
	
		//前翻
		$(".banner_0 .next").unbind( "click" )
		$(".banner_0 .next").click(function(){
			var xx=numx-1
			xx<0?xx=maxnum:0;
			bonul.find("span").eq(xx).click()
		});
		//后翻
		$(".banner_0 .por").unbind( "click" )
		$(".banner_0 .por").click(function(){
			var xx=numx+1
			xx>maxnum?xx=0:0;
			bonul.find("span").eq(xx).click()
		});	
		
		
	var interv=setInterval(xx,speed*1000)
	
	function xx(){
		var xx=numx+1
		xx>maxnum?xx=0:0;
		bonul.find("span").eq(xx).click()
	}
	
	bonul.hover(function(){
		 clearInterval(interv)
		},function(){
		interv=setInterval(xx,speed*1000)
		})
		
	$(".banner_0 .por,.banner_0 .next").hover(function(){
		 clearInterval(interv)
		},function(){
		interv=setInterval(xx,speed*1000)
		})
	}
	
	
	xxxban()
})