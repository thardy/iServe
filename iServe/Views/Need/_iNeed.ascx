<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<iServe.Models.Need>>" %>
<ul class="need_list">
    <% if (Model.Count() > 0) { %>
        <% foreach (var need in Model) { %>
            <li id="<%= need.ID %>" onclick="Dashboard.ShowInvolvement('<%= need.ID %>', '<%= Url.Action("ShowInvolvement", new {needID = need.ID} ) %>'); return false;" class="clearfix">
                <div class="name">
                    <b><%= need.Name.Truncate(23, "...").HtmlEncode() %></b>
                </div>
                <div class="date">
                    <%= need.RequiredDate.Value.ToShortDateString() %>
                </div>
                <div class="status">
                    <%= need.NeedStatus.Name %>
                </div>
                <% if (((List<int>)ViewData["InterestedUsers"]).Contains(need.ID)) { %>
                    <div class="interested"></div>
                <% } %>
                <% else { %>
                    <div class="empty"></div>
                <% } %>
                <% if (((List<int>)ViewData["UnratedCommittedUsers"]).Contains(need.ID)) { %>
                    <div class="committed"></div>
                <% } %>
                <% else { %>
                    <div class="empty"></div>
                <% } %>
                <div class="action">
                    <div class="green">
                    <% if (((List<int>)ViewData["UnratedCommittedUsers"]).Contains(need.ID) && (NeedStatusEnum)need.NeedStatusID != NeedStatusEnum.Met) { %> 
                        <button onclick="Dashboard.CompleteNeed('<%= need.ID %>', '<%= Url.Action("CompleteNeed", new {needID = need.ID} ) %>'); event.cancelBubble=true; return false;">Complete</button>
                    <% } %>
                    </div>
                    <div class="red">
                    <% if ((NeedStatusEnum)need.NeedStatusID != NeedStatusEnum.Met) { %>    
                        <button onclick="Dashboard.CancelNeed('<%= need.ID %>', '<%= Url.Action("CancelNeed", new {needID = need.ID} ) %>'); event.cancelBubble=true; return false;">Cancel</button>
                    <% } %>
                    </div>
                </div>
            </li>
        <% } %>
    <% } %>
    <% else { %>
        <li>You have not submitted any needs.</li>
    <% } %>
</ul>