<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestForm.aspx.cs" Inherits="EvoqueMyStyle.Website.TestForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>only for test</title>
</head>
<body>
  <form id="form1" action="Action.aspx" method="post">
  <div>
    <input name="p" />
    <input name="s" />
    <input type="hidden" name="ac" value="getpics" />
    <input type="hidden" name="hash" value="123456" />
    <input type="submit" />
  </div>
  <asp:HyperLink ID="linkWeibo" runat="server">用微博帐号登录</asp:HyperLink>
  </form>
  <hr />
  <form action="https://api.weibo.com/2/statuses/update.json" method="post">
  <div>
    <textarea cols="40" rows="3" name="status"></textarea>
    <input type="hidden" name="access_token" value="2.00r_UcPBxyT5LB27777a92550wMufj" />
    <br />
    <input type="submit" value="发布" />
  </div>
  </form>
  <hr />
  <form action="Action.aspx" method="post" enctype="multipart/form-data">
  <div>
    <input type="file" name="upload" />
    <input type="hidden" name="ac" value="upload" />
    <input type="submit" value="上传" />
  </div>
  </form>
  <form action="Action.aspx" method="post">
  <div><br />
   url <input type="text" name="url" value="upload/temp/94enfuQ7DZ9x2YMhvWCy2.png" /><br />
   x <input name="x" /><br />
   y <input name="y" /><br />
   rotate <input name="rotate" value="90" /><br />
   width <input name="width" value="120" /><br />
   ratio <input name="ratio" /><br />
   <input type="hidden" name="ac" value="savepic" />
   <input type="submit" value="保存照片" />
  </div>
  </form>
</body>
</html>
