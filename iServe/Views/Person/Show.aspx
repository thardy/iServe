<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iServe.Models.Person>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Show
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <span class="profile_title"><%= Model.FirstName.HtmlEncode() %>'s Profile</span> | <%= Html.ActionLink("Edit", "Edit", new { id=Model.ID })%>
    </div>

    <fieldset class="profile_section">
        <legend>Personal info</legend>
        <div>
            <div class="profile_section_heading">Name</div>
            <%= Model.FirstName.HtmlEncode() %> <%= Model.LastName.HtmlEncode() %>
            <br />
        </div>
        <div>
            <div class="profile_section_heading">Birthday</div>
            <%= String.Format("{0:g}", Model.DateOfBirth.Value.ToString("MMMM d, yyyy")) %>
        </div>
        <div>
            <div class="profile_section_heading">Email</div>
            <% if (!Model.IndividualEmail.IsEmpty()) { %>  
                <%= Model.IndividualEmail.HtmlEncode() %>           
            <% } %>
            <% else { %>
                No personal email
            <% } %>
        </div>
        <div>
            <div class="profile_section_heading">Mobile phone number</div>
            <% if (!Model.IndividualPhoneNumber.IsEmpty()) { %>
                <%= Model.IndividualPhoneNumber.HtmlEncode() %>
            <% } %>
            <% else { %>
                No mobile phone number
            <% } %>
        </div>
    </fieldset>
    
    <fieldset class="profile_section">
        <legend>Household info</legend>
        <div>
            <div class="profile_section_heading">Address</div>
            <% if (Model.Address != null) { %>
                <%= Model.Address.ToString() %>
            <% } %>
            <% else { %>
                No address
            <% } %>
        </div>
        <div>
            <div class="profile_section_heading">Email</div>
            <% if (!Model.HouseholdEmail.IsEmpty()) { %>
                <%= Model.HouseholdEmail.HtmlEncode() %>
            <% } %>
            <% else { %>
                No household email
            <% } %>
        </div>
        <div>
            <div class="profile_section_heading">Home phone number</div>
            <% if (!Model.HouseholdPhoneNumber.IsEmpty()) { %>
                 <%= Model.HouseholdPhoneNumber.HtmlEncode() %>
            <% } %>
            <% else { %>
                No home phone number
            <% } %>
        </div>
    </fieldset>
</asp:Content>

