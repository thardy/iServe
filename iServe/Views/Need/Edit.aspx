<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<iServe.Models.Need>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <% if (TempData["ErrorMessage"] != null){ %>
    <div class="error-message">
        <%=TempData["ErrorMessage"]%>
    </div>
    <% } %>
     
    <% if (TempData["Message"] != null){ %>
    <div class="message">
        <%=TempData["Message"] %>
    </div>
    <% } %>
    
    <% using (Html.BeginForm(new {Action="Update", id=Model.ID})) { //using (Html.BeginForm<NeedController>(action => action.Update(Model.ID))) { %>
        <table>
            <%Html.RenderPartial("IndividualForm", ViewData.Model,ViewData); %>
        </table>
        <input type="submit" value="Save" class="editor-button" />
    <% } %>
</asp:Content>