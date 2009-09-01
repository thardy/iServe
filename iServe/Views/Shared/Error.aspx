<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Error</title>
    <link href="../../Content/styles/Site.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <div class="page">

        <div id="header">
            <div id="title">
                <h1>iServe</h1>
            </div>
              
            <div id="logindisplay">
                <% Html.RenderPartial("LogOnUserControl"); %>
            </div> 
            
            <div id="menucontainer">
            
                <ul id="menu">              
                    <li><%= Html.ActionLink("Home", "Index", "Need")%></li>
                    <li><%= Html.ActionLink("Dashboard", "Dashboard", "Need")%></li>
                    <li><%= Html.ActionLink("About", "About", "Home")%></li>
                </ul>
            
            </div>
        </div>

        <div id="main">
            
    	        <h2>
                    Sorry, an error occurred while processing your request.
                </h2>    
                <%if(ViewData["error"] != null) {%>
                    <%=ViewData["error"].ToString() %>
                <%} %>
            
            
        </div>
        
    </div>
</body>
</html>

