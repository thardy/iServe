<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewPage<IPagedList<iServe.Models.GetNeedsResult>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(Index.Loaded);
    </script>

    <!-- Need list -->
    <div id="needindex">
	    <div class="addneed"><a href="<%=Url.Action("New", "Need")%>">Submit a need</a></div>
	    <%= Html.Pager<GetNeedsResult>(Model)%>
		<ul id="need_list">
		    <%foreach (GetNeedsResult need in Model) {%>
		    <li id="<%=need.ID%>">
	            <div class="row">
	                <div class="category <%=((CategoryEnum)need.CategoryID).ToString()%>"></div>
	                <div class="zip">75038</div>
	                <div class="name"><%=Html.ActionLink<NeedController>(c => c.Show(need.ID), need.Name)%></div>
	                <div class="date"><%=need.RequiredDate.Value.ToShortDateString()%></div>
	            </div>
	            <div class="row2">
	                <div class="involvement">
	                    <span class="count"><%=need.HelpersNeeded%></span> Needed<br />
	                    <span class="count"><%=need.Committed%></span> Committed<br />
	                    <span class="count"><%=need.Interested + need.Accepted%></span> Interested
	                </div>
	                <div class="description"><%=need.Description.Truncate(200, "...").HtmlEncode() %></div>
	                <div class="status" onclick="Index.ExpressInterest(<%=need.ID%>, '<%= Url.Action("ExpressInterest", new {id = need.ID} ) %>'); event.cancelBubble=true; return false;">I am willing<br />to help</div>
	            </div>
			</li>
		    <% } %>
		</ul>
		<%= Html.Pager<GetNeedsResult>(Model)%>
	</div>
</asp:Content>
