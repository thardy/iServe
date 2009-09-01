<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<iServe.Models.Need>" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Need Detail</title>
</head>

<body>
	<div class="page">
		<div id="Header">
		</div>
		<h1><%= Model.Name %></h1>
		<%= Model.Description %>
		<div id="Main">
			
		</div>
	</div>
</body>

</html>