<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iServe.Models.Need>" %>
<div id="need_involvement">
    <div class="heading">Interested</div>
    <% if (((List<GetNeedHelpersByStatusResult>)ViewData["InterestedUsers"]).Count > 0) { %>
        <ul>
            <% foreach (var helper in ViewData["InterestedUsers"] as List<GetNeedHelpersByStatusResult>) { %>
                <li id="interested_user_<%= helper.UserID %>" class="clearfix">
                    <div class="username"><%= helper.UserName.HtmlEncode()%></div>
                    <div class="rating"><%= helper.Rating%></div>
                    <div class="green">
                        <button onclick="Dashboard.ModifyUserInvolvementStatus('<%= Model.ID %>', '<%= helper.UserID %>', 'interested_user_<%= helper.UserID %>', '<%= Url.Action("AcceptUser", new { needID = Model.ID, userID = helper.UserID }) %>'); return false;">Accept</button>
                    </div>
                    <div class="red">
                        <button onclick="Dashboard.ModifyUserInvolvementStatus('<%= Model.ID %>', '<%= helper.UserID %>', 'interested_user_<%= helper.UserID %>', '<%= Url.Action("DeclineUser", new { needID = Model.ID, userID = helper.UserID }) %>'); return false;">Decline</button>
                    </div>
                </li>
            <% } %>
        </ul>
    <% } %>
    <% else { %>
        No interested users.
    <% } %>
    <div class="heading">Awaiting Commitment</div>
    <% if (((List<GetNeedHelpersByStatusResult>)ViewData["AcceptedUsers"]).Count > 0) { %>
        <ul>
            <% foreach (var helper in ViewData["AcceptedUsers"] as List<GetNeedHelpersByStatusResult>) { %>
                <li id="accepted_user_<%= helper.UserID %>" class="clearfix">
                    <div class="username"><%= helper.UserName.HtmlEncode() %></div>
                    <div class="rating"><%= helper.Rating %></div>
                    <div class="red">
                        <button onclick="Dashboard.ModifyUserInvolvementStatus('<%= Model.ID %>', '<%= helper.UserID %>', 'accepted_user_<%= helper.UserID %>', '<%= Url.Action("DeclineUser", new { needID = Model.ID, userID = helper.UserID }) %>'); return false;">Cancel</button>
                    </div>
                </li>
            <% } %>
        </ul>
    <% } %>
    <% else { %>
        No users awaiting commitment.
    <% } %>
    <div class="heading">Committed</div>
    <% if (((List<GetNeedHelpersByStatusResult>)ViewData["CommittedUsers"]).Count > 0) { %>
        <ul>
            <% foreach (var helper in ViewData["CommittedUsers"] as List<GetNeedHelpersByStatusResult>) { %>
                <li id="committed_user_<%= helper.UserID %>" class="clearfix">
                    <div class="username"><%= helper.UserName.HtmlEncode() %></div>
                    <div class="rating"><%= helper.Rating %></div>
                    <div class="green">
                        <% if (!helper.HasBeenRated && ((NeedStatusEnum)Model.NeedStatusID == NeedStatusEnum.Met || (NeedStatusEnum)Model.NeedStatusID == NeedStatusEnum.Cancelled)) { %>
                            <button onclick="Dashboard.DisplayRateUserDialog('committed_user_<%= helper.UserID %>', '<%= Url.Action("RateHelper", new { needID = Model.ID, userID = helper.UserID }) %>'); return false;">Rate</button>                            
                        <% } %>
                    </div>
                </li>
            <% } %>
        </ul>
    <% } %>
    <% else { %>
        No committed users.
    <% } %>
</div>