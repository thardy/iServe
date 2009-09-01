<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Dashboard
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    $(document).ready(Dashboard.Loaded);
</script>
    <h2>Dashboard</h2>
    <div id="tabs" class="clearfix dashboard">
        <ul>
            <li>
                <a href="#tabs-1" onclick="Dashboard.ShowiNeed('<%= Url.Action("iNeed") %>')">iNeed</a>
            </li>
            <li>
                <a href="#tabs-2" onclick="Dashboard.ShowiServe('<%= Url.Action("iServe") %>')">iServe</a>
            </li>
        </ul>
        <div id="tabs-1">
            <div id="ineed_left_section">
                <% Html.RenderAction<NeedController>(c => c.iNeed()); %>
            </div>
            <div id="ineed_right_section">
            </div>
        </div>
        <div id="tabs-2">
            <div id="iserve_left_section">
                
            </div>
        </div>
    </div>
    <!-- Modal dialog for rating user -->
    <div id="rate_user_dialog" title="Rate User">
        
    </div>
</asp:Content>
