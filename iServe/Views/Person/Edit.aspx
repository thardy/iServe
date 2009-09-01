<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iServe.Models.Person>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm("Update", "Person")) {%>
        <%= Html.AntiForgeryToken()%>
        <%= Html.HiddenFor(p => p.ID) %>
        <%= Html.HiddenFor(p => p.HouseholdID) %>
        
        <fieldset class="profile_section">
            <legend>Personal info</legend>
            <div>
                <div class="profile_section_heading">First Name</div>
                <%= Html.TextBoxFor(p => p.FirstName)%>
                <%= Html.ValidationMessageFor(p => p.FirstName, "*")%>
            </div>
            <div>
                <div class="profile_section_heading">Last Name</div>
                <%= Html.TextBoxFor(p => p.LastName)%>
                <%= Html.ValidationMessageFor(p => p.LastName, "*")%>
            </div>
            <div>
                <div class="profile_section_heading">Birthday</div>
                <%= Html.TextBox("dateOfBirth", Model.DateOfBirth.Value.ToShortDateString()) %>
                <%= Html.ValidationMessageFor(p => p.DateOfBirth, "*")%>
            </div>
            <div>
                <div class="profile_section_heading">Email</div>
                <%= Html.TextBoxFor(p => p.IndividualEmail)%>
                <%= Html.ValidationMessageFor(p => p.IndividualEmail, "*")%>
                <%= Html.HiddenFor(p => p.IndividualEmailID) %>                
            </div>
            <div>
                <div class="profile_section_heading">Mobile Phone Number</div>
                <%= Html.TextBoxFor(p => p.IndividualPhoneNumber)%>
                <%= Html.ValidationMessageFor(p => p.IndividualPhoneNumber, "*")%>
                <%= Html.HiddenFor(p => p.IndividualPhoneNumberID) %>                
            </div>
        </fieldset>
        <fieldset class="profile_section">
            <legend>Household info</legend>
            <%= Html.HiddenFor(p => p.Address.ID) %>
            <div>
                <div class="profile_section_heading">Street1</div>
                <%= Html.TextBoxFor(p => p.Address.Address1) %>
                <%= Html.ValidationMessageFor(p => p.Address.Address1, "*")%>                
            </div>
            <div>
                <div class="profile_section_heading">Street2</div>
                <%= Html.TextBoxFor(p => p.Address.Address2) %>
            </div>
            <div>
                <div class="profile_section_heading">City</div>
                <%= Html.TextBoxFor(p => p.Address.City) %>
                <%= Html.ValidationMessageFor(p => p.Address.City, "*")%>                                
            </div>
            <div>
                <div class="profile_section_heading">State</div>
                <%= Html.TextBoxFor(p => p.Address.StProvince) %>
                <%= Html.ValidationMessageFor(p => p.Address.StProvince, "*")%>                                
            </div>
            <div>
                <div class="profile_section_heading">Postal Code</div>
                <%= Html.TextBoxFor(p => p.Address.PostalCode) %>
                <%= Html.ValidationMessageFor(p => p.Address.PostalCode, "*")%>                                
            </div>
            <div>
                <div class="profile_section_heading">Email</div>
                <%= Html.TextBoxFor(p => p.HouseholdEmail)%>
                <%= Html.ValidationMessageFor(p => p.HouseholdEmail, "*")%>
                <%= Html.HiddenFor(p => p.HouseholdEmailID) %>
            </div>
            <div>
                <div class="profile_section_heading">Home Phone Number</div>
                <%= Html.TextBoxFor(p => p.HouseholdPhoneNumber)%>
                <%= Html.ValidationMessageFor(p => p.HouseholdPhoneNumber, "*")%>
                <%= Html.HiddenFor(p => p.HouseholdPhoneNumberID) %>                
            </div>
        </fieldset>
        <div>
            <input type="submit" value="Save" />
            <%=Html.ActionLink("Cancel", "Show", new { id = Model.ID })%>
        </div>
    <% } %>
</asp:Content>

