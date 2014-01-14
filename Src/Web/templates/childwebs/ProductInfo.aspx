<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductInfo.aspx.cs" Inherits="templates_childwebs_ProductInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../style/djComm.css" rel="stylesheet" type="text/css" />
    <link href="../../style/djConts.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">


        function showtabs(num,_this) {
            $(".tabcont").css("display", "none");
            $("#tabcont" + num).css("display", "block");
            $(".tabul li").css("color", "#fff");
            $(".tabul li").css("font-weight", "normal");
            $(_this).css("color", "red");
            $(_this).css("font-weight", "bold");

        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="top">
        <p>
            欢迎光临，现在是2013-12-20 11：01：30
        </p>
    </div>
    <div class="head">
        <div class="headcont">
            <a href="index.aspx">
                <img src="../../images/djimages/logo.png" />
            </a>
            <input type="text" class="txt" /><input type="button" class="btn" />
        </div>
    </div>
    <div class="outmenu">
        <div class="nav">
            <div class="menu">
                <ul>
                    <li><a class="hide" href="Index.aspx">网站首页</a>
                        <!--[if lte IE 6]>
<a href="../menu/index.html">DEMOS
<table><tr><td>
<![endif]-->
                        <ul>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                        </ul>
                        <!--[if lte IE 6]>
</td></tr></table>
</a>
<![endif]-->
                    </li>
                    <li><a class="hide" href="Index.aspx">产品展示</a>
                        <!--[if lte IE 6]>
<a href="index.html">MENUS
<table><tr><td>
<![endif]-->
                        <ul>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                        </ul>
                        <!--[if lte IE 6]>
</td></tr></table>
</a>
<![endif]-->
                    </li>
                    <li><a class="hide" href="Index.aspx">案例演示</a>
                        <!--[if lte IE 6]>
<a href="../layouts/index.html">LAYOUTS
<table><tr><td>
<![endif]-->
                        <ul>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                        </ul>
                        <!--[if lte IE 6]>
</td></tr></table>
</a>
<![endif]-->
                    </li>
                    <li><a class="hide" href="Index.aspx">客服服务</a>
                        <!--[if lte IE 6]>
<a href="../boxes/index.html">BOXES
<table><tr><td>
<![endif]-->
                        <ul>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                        </ul>
                        <!--[if lte IE 6]>
</td></tr></table>
</a>
<![endif]-->
                    </li>
                    <li><a class="hide" href="Index.aspx">咨询中心</a>
                        <!--[if lte IE 6]>
<a href="../mozilla/index.html">MOZILLA
<table><tr><td>
<![endif]-->
                        <ul>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                        </ul>
                        <!--[if lte IE 6]>
</td></tr></table>
</a>
<![endif]-->
                    </li>
                    <li><a class="hide" href="Index.aspx">技术论坛</a>
                        <!--[if lte IE 6]>
<a href="../ie/index.html">EXPLORER
<table><tr><td>
<![endif]-->
                        <ul>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                            <li><a href="Index.aspx">子菜单</a></li>
                        </ul>
                    </li>
                    <!--[if lte IE 6]>
</td></tr></table>
</a>
<![endif]-->
                </ul>
                <!-- clear the floats if required -->
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <div class="swf2">
    </div>
    <div class="mid">
        <div class="left left2">
            <p>
                案例分类</p>
            <ul>
                <li><a href="#">混合管</a> </li>
                <li><a href="#">点胶机</a> </li>
                <li><a href="#">点胶针头</a> </li>
                <li><a href="#">AB胶筒</a> </li>
                <li><a href="#">点胶阀门</a> </li>
                <li><a href="#">点胶针筒</a> </li>
                <li><a href="#">AB胶枪</a> </li>
                <li><a href="#">针筒适配器</a> </li>
                <li><a href="#">压力桶</a> </li>
                <li><a href="#">各种料管</a> </li>
                <li><a href="#">非标产品定制</a> </li>
            </ul>
            <p>
                联系我们</p>
            <span>地址：广东省东莞市高埗镇冼沙二上坊广场北路宝源工业园</span> <span>电话：0769-23107897</span> <span>传真：0769-23102536
            </span>
        </div>
        <div class="right">
            <p class="ptitle">
                您现在的位置：月无声电子设备事业部 > 产品展示
            </p>
            <div class="productInfo">
                <img src="../../images/djimages/infoimg.gif" />
                <p class="infcont">
                    <label>
                        桌面式全自动点胶机</label>
                    桌面式全自动点胶机PM-XYZ-01（亦称三轴点胶机）：是一款从客 户成本效益的角度去设计开发的产品，在满足各项工作手运动性能 指标的前提下，对机器结构进行了优化设计。保证了每次分配的点
                    胶量在相同时间内准确，在所需要点滴的每个产品上也保持了均匀 一致。
                </p>
            </div>
            <div class="tabs">
                <ul class="tabul">
                    <li onmousemove="showtabs('1',this)" style="width: 75px;font-weight:bold; color:Red;">规格参数 </li>
                    <li onmousemove="showtabs('2',this)" style=" margin-left:8px;">功能特点 </li>
                    <li onmousemove="showtabs('3',this)">推荐应用 </li>
                    <li onmousemove="showtabs('4',this)">相关配件 </li>
                    <li onmousemove="showtabs('5',this)">产品视频 </li>
                    <li onmousemove="showtabs('6',this)">文档下载 </li>
                </ul>
                <div class="tabcont" style=" display:block;" id="tabcont1">
                    内容1
                </div>
                <div class="tabcont" id="tabcont2">
                    内容2
                </div>
                <div class="tabcont" id="tabcont3">
                    内容3
                </div>
                <div class="tabcont" id="tabcont4">
                    内容4
                </div>
                <div class="tabcont" id="tabcont5">
                    内容5
                </div>
                <div class="tabcont" id="tabcont6">
                    内容6
                </div>
            </div>
        </div>
    </div>
    <div class="foot">
        <ul>
            <li><a href="Index.aspx">返回首页 </a><span>|</span></li>
            <li><a href="Index.aspx">联系方式 </a><span>|</span></li>
            <li><a href="Index.aspx">咨询中心 </a><span>|</span></li>
            <li><a href="Index.aspx">技术咨询 </a><span>|</span></li>
            <li><a href="Index.aspx">案例演示 </a></li>
        </ul>
        <p>
            版权所有 © 2008-2012 东莞市月无声电子设备有限公司 粤ICP备09218017号</p>
    </div>
    </form>
</body>
</html>
