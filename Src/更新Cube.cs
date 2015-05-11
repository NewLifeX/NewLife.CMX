// 自动选择最新的文件源
var src = @"..\..\NewLife.Cube";

var dest = "NewLife.CMX.Web";

Console.WriteLine("复制 {0} => {1}", src.AsDirectory().FullName, dest.AsDirectory().FullName);
try
{
	src.AsDirectory().CopyToIfNewer(dest, "*.cs;*.cshtml;*.css;*.js", false,
		name => Console.WriteLine("\t{1}\t{0}", name, src.CombinePath(name).AsFile().LastWriteTime.ToFullString()));
}
catch (Exception ex) { Console.WriteLine(" " + ex.Message); }