<%@ Page Language="C#" AutoEventWireup="true" CodeFile="about.aspx.cs" Inherits="about" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>混合管,灌胶机,AB胶枪,点胶针筒,点胶针头 - 月无声</title>
    <link rel="stylesheet" type="text/css" href="style/css1.css" />
    <script src="Scripts/jquery/jquery-1.9.1.min.js"></script>
   <script type="text/javascript" src="style/javascript.js"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div id="wrap">
        <div id="wrpper">
              <div class="header">
                <div id="logo">
                    <h1>东莞市月无声实业</h1>
                </div>
                <div class="mod">
                    <ul>
                        <li><a href="#" target="_blank">点胶设备</a></li>
                        <li><a href="#" target="_blank">智能自控</a></li>
                        <li><a href="#" target="_blank">化工胶水</a></li>
                        <li><a href="#" target="_blank">五金模具</a></li>
                    </ul>
                    <form>
                        <input name="" type="text" />
                        <input name="搜索" type="submit" class="botton" id="搜索" value="提交" />
                    </form>
                </div>
                <div class="nav">
                    <div class="parent">
                        <ul>
                            <li class="index"><a href="#" onclick="change_bg(this)">网站首页</a></li>
                            <li class="about"><a href="#" onclick="change_bg(this)">关于月无声</a></li>
                            <li class="product"><a href="#" onclick="change_bg(this)">产品与服务</a></li>
                            <li class="com"><a href="#" onclick="change_bg(this)">企业文化</a></li>
                            <li class="recruitment"><a href="#" onclick="change_bg(this)">人才招聘</a></li>
                            <li class="new"><a href="#" onclick="change_bg(this)">新闻中心</a></li>
                            <li class="forum"><a href="#" onclick="change_bg(this)">技术论坛</a></li>
                            <li class="shop"><a href="#" onclick="change_bg(this)">月无声商城</a></li>
                        </ul>
                    </div>
                    <div class="child">
                        <ul class="index">
                            <li>首页</li>
                        </ul>
                        <ul class="about">
                            <li>关于月无声</li>
                        </ul>
                        <ul class="product">
                            <li><a href="#">产品服务1</a></li>
                            <li><a href="#">产品服务2</a></li>
                            <li><a href="#">产品服务3</a></li>
                            <li><a href="#">产品服务4</a></li>
                        </ul>
                        <ul class="com">
                            <li>企业文化</li>
                        </ul>
                        <ul class="recruitment">
                            <li>人才招聘</li>
                        </ul>
                        <ul class="new">
                            <li>新闻中心</li>
                        </ul>
                        <ul class="forum">
                            <li>技术论坛</li>
                        </ul>
                        <ul class="shop">
                            <li>商场</li>
                        </ul>
                    </div>
                </div>
            </div>

      
               <div class="top_image">
                    <img src="images/top_i2.jpg" width="960" height="120" /></div>
              <div class="mainbody">
                    <div class="list">
                        <div id="list_t"><span><strong>关于月无声</strong></span></div>
                        <div class="list_meu">
                            <dl> 
			                   <dt><a href="#" class="minus" onclick="showHide(this,'items0');"><span></span>关于月无声</a></dt> 
			                   <dd id="items0"> 
				                  <ul>
					                    <li><a href="#">公司简介</a></li>
                                     
				                 </ul>
			                 </dd> 
		                 </dl> 
	
		                 <dl> 
			                <dt><a href="#" class="plus" onclick="showHide(this,'items1');"><span></span>点胶设备事业部</a></dt> 
			                <dd id="items1" style="display:none;"> 
				                  <ul>
					                 <li><a href="#"></a></li>
                                     
				                 </ul> 
			              </dd> 
		              </dl>
                      <dl> 
			                <dt><a href="#" class="plus" onclick="showHide(this,'items2');"><span></span>智能声控事业部</a></dt> 
			                <dd id="items2" style="display:none;"> 
				                  <ul>
					                 <li><a href="#"></a></li>
				                 </ul> 
			              </dd> 
		              </dl>
                             <dl> 
			                <dt><a href="#" class="plus" onclick="showHide(this,'items3');"><span></span>化工胶水事业部</a></dt> 
			                <dd id="items3" style="display:none;"> 
				                  <ul>
					                 <li><a href="#"></a></li>
				                 </ul> 
			                  </dd> 
		                  </dl>
                          <dl> 
			                <dt><a href="#" class="plus" onclick="showHide(this,'items3');"><span></span>五金模具事业部</a></dt> 
			                <dd id="Dd1" style="display:none;"> 
				                  <ul>
					                 <li><a href="#"></a></li>
				                 </ul> 
			                  </dd> 
		                  </dl>
                       </div>
                    </div>
                    <div id="culture">
                        <div class="c1"><span>当前位置：首页 > 企业文化</span></div>
                        <div class="c2">
                          <div class="p1"><a href="" target="_blank">
                            <img src="images/product/p1.jpg" width="155" height="127" /><p>产品相册</p></a>
                          </div>
                          <div class="p2"><a href="" target="_blank">
                            <img src="images/product/p2.jpg" width="155" height="127" /><p>商业活动</p></div>
                          <div class="p3"><a href="" target="_blank">
                            <img src="images/product/p2.jpg" width="155" height="127" /><p>娱乐活动</p></div>
                          <div class="p4"><a href="" target="_blank">
                            <img src="images/product/p2.jpg" width="155" height="127" /><p>我们的团队</p></div>
                        </div>
                        <div class="c3">
                            <div class="video">
                                <div id="v_t"></div>
                                <div class="v_con"></div>
                            </div>
                            <div class="c5"></div>
                        </div>
                  </div>
                    
                  
            </div>
            <div id="foot">
                <div class="foot_nav">
                    <div class="g1"><a href="#">返回首页</a> | <a href="#">联系我们</a> | <a href="#">友情链接</a> | <a href="#">技术论坛</a> | <a href="#">月无声商城</a> |</div>
                </div>
                <div class="address">
                    版权所有 © 2008-2012 东莞市月无声电子设备有限公司 粤ICP备09218017号<br />

                    <br />
                    地址：东莞市高埗镇冼沙村广场北路宝源工业区  电话：0769-23107897 0769-23107080
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>