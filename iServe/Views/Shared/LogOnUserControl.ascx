<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
    <% if (Request.IsAuthenticated) { %>
        <span>Welcome <b><%= Html.Encode(((User)ViewData["User"]).Name) %></b>!</span>
        <%= Html.ActionLink("Log Off", "Logout", "Account") %>
        | <%=Html.ActionLink("View/Edit Profile", "Show", "UserProfile", new { id = ((User)ViewData["User"]).ID }, null)%>
    <% } else { %> 
        <%= Html.ActionLink("Log On", "LogOn", "Account") %>
    <% } %>
    
