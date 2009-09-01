<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<iServe.Models.Need>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Submit a Need</h2>
    
    <% using (Html.BeginForm(new { Action = "Create" })) { %>
        <table>
            <%Html.RenderPartial("_needForm", Model); %>
        </table>
        <input type="submit" value="Save" />
    <% } %>
</asp:Content>