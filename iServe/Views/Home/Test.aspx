<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iServe.Models.Widget>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Test
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Test</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm(new { Action = "Create" })) { %>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="ID">ID:</label>
                <%= Html.TextBox("ID") %>
                <%= Html.ValidationMessage("ID", "*") %>
            </p>
            <p>
                <label for="Name">Name:</label>
                <%= Html.TextBox("Name") %>
                <%= Html.ValidationMessage("Name", "*") %>
            </p>
            <p>
                <label for="Quantity">Quantity:</label>
                <%= Html.TextBox("Quantity") %>
                <%= Html.ValidationMessage("Quantity", "*") %>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

