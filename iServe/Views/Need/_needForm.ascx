<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iServe.Models.Need>" %>
    <script type="text/javascript">
        $(function() {
            $("#RequiredDate").datepicker({ showOn: 'both',  buttonImage: '/content/images/calendar.gif', buttonImageOnly: true });
        });

    </script>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <fieldset>
        
        <p>
            <label for="CategoryID">Category:</label>
            <%= Html.DropDownListFor(n => n.CategoryID, (SelectList)ViewData["Categories"]) %>
            <%= Html.ValidationMessageFor(n => n.CategoryID, "*") %>
        </p>
        <p>
            <label for="Name">Name:</label>
            <%= Html.TextBoxFor(n => n.Name, Model.Name) %>
            <%= Html.ValidationMessageFor(n => n.Name, "*") %>
        </p>
        <p>
            <label for="Description">Description:</label>
            <%= Html.TextBoxFor(n => n.Description, Model.Description) %>
            <%= Html.ValidationMessageFor(n => n.Description, "*") %>
        </p>
        <p>
            Required&nbsp;
            <%= Html.DropDownListFor(n => n.IsRequiredOnDate,
                    new SelectList(new List<object> {
                        new {Name = "By", Value = "false"},
                        new {Name = "On", Value = "true"} }, "Value", "Name"))%>
            <%= Html.TextBoxFor(n => n.RequiredDate, new Dictionary<string, object> { { "Test", String.Format("{0:g}", Model.RequiredDate) }, { "Value", DateTime.Now.Date.AddMonths(1).ToShortDateString() } })%>
            <%= Html.ValidationMessageFor(n => n.RequiredDate, "*") %>
            <%= Html.ValidationMessageFor(n => n.IsRequiredOnDate, "*")%>
        </p>
        <p>
            <label for="PostalCode">Postal Code:</label>
            <%= Html.TextBoxFor(n => n.PostalCode, Model.PostalCode) %>
            <%= Html.ValidationMessageFor(n => n.PostalCode, "*") %>
        </p>
        <p>
            <label for="HelpersNeeded">Helpers Needed:</label>
            <%= Html.TextBoxFor(n => n.HelpersNeeded, Model.HelpersNeeded) %>
            <%= Html.ValidationMessageFor(n => n.HelpersNeeded, "*")%>
        </p>
    </fieldset>


