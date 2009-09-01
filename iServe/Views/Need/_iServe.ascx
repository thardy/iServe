<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<GetNeedInfoByHelperResult>>" %>
<ul class="need_list">
    <% if (Model.Count > 0) { %>
        <% foreach (var info in Model) { %>
            <li id="<%= info.NeedID %>" class="clearfix">
                <div class="name">
                    <b><%= info.NeedName.Truncate(23, "...").HtmlEncode()%></b>
                </div>
                <div class="submitter">
                    <%= info.UserName.HtmlEncode()%>
                </div>
                <div class="rating">
                    <%= info.Rating%>
                </div>
                <div class="date">
                    <%= info.RequiredDate.Value.ToShortDateString()%>
                </div>
                <div class="userneedstatus">
                    <%= ((UserNeedStatusEnum)info.UserNeedStatusID).ToString()%>
                </div>
                <div class="action">
                    <% int userID = ((User)ViewData["User"]).ID; %>
                    <div class="green">
                        <% if (info.CanRate.Value) { %>
                            <button class="green" onclick="Dashboard.DisplayRateUserDialog('<%= info.NeedID %>', '<%= Url.Action("RateSubmitter", new { needID = info.NeedID, userID = info.SubmittedByID }) %>'); return false;">Rate</button>
                        <% } %>
                        <% else if ((UserNeedStatusEnum)info.UserNeedStatusID == UserNeedStatusEnum.Accepted) { %>
                            <button class="green" onclick="Dashboard.CommitUser(<%= info.NeedID %>, '<%= Url.Action("CommitUser", new { needID = info.NeedID, userID = userID }) %>'); event.cancelBubble=true; return false;">Commit</button>
                        <% } %>
                    </div>
                    <div class="red">
                        <% if ((UserNeedStatusEnum)info.UserNeedStatusID != UserNeedStatusEnum.Committed) { %>
                            <button class="red" onclick="Dashboard.ModifyUserInvolvementStatus('<%= info.NeedID %>', null, '<%= info.NeedID %>', '<%= Url.Action("RemoveUser", new { needID = info.NeedID, userID = userID }) %>'); event.cancelBubble=true; return false;">Cancel</button>
                        <% } %>
                    </div>
                </div>
            </li>
        <% } %>
    <% } %>
    <% else { %>
        <li>You are not involved in any needs.</li>
    <% } %>
</ul>