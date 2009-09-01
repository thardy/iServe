<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<iServe.Models.Need>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NeedAdmin
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>NeedAdmin</h2>

    <table>
        <tr>
            <th></th>
            <th>
                ID
            </th>
            <th>
                ChurchID
            </th>
            <th>
                CategoryID
            </th>
            <th>
                SubmittedByID
            </th>
            <th>
                Name
            </th>
            <th>
                Description
            </th>
            <th>
                RequiredDate
            </th>
            <th>
                IsRequiredOnDate
            </th>
            <th>
                PostalCode
            </th>
            <th>
                ImageName
            </th>
            <th>
                HelpersNeeded
            </th>
            <th>
                NeedStatusID
            </th>
            <th>
                Created
            </th>
            <th>
                CreatedBy
            </th>
            <th>
                Updated
            </th>
            <th>
                UpdatedBy
            </th>
            <th>
                CurrentUserID
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
                <%= Html.ActionLink("Details", "Details", new { id=item.ID })%>
            </td>
            <td>
                <%= Html.Encode(item.ID) %>
            </td>
            <td>
                <%= Html.Encode(item.ChurchID) %>
            </td>
            <td>
                <%= Html.Encode(item.CategoryID) %>
            </td>
            <td>
                <%= Html.Encode(item.SubmittedByID) %>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Description) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.RequiredDate)) %>
            </td>
            <td>
                <%= Html.Encode(item.IsRequiredOnDate) %>
            </td>
            <td>
                <%= Html.Encode(item.PostalCode) %>
            </td>
            <td>
                <%= Html.Encode(item.ImageName) %>
            </td>
            <td>
                <%= Html.Encode(item.HelpersNeeded) %>
            </td>
            <td>
                <%= Html.Encode(item.NeedStatusID) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.Created)) %>
            </td>
            <td>
                <%= Html.Encode(item.CreatedBy) %>
            </td>
            <td>
                <%= Html.Encode(String.Format("{0:g}", item.Updated)) %>
            </td>
            <td>
                <%= Html.Encode(item.UpdatedBy) %>
            </td>
            <td>
                <%= Html.Encode(item.CurrentUserID) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

