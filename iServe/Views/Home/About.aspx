<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="quote">
        <q>
            I don't know what your destiny will be, but one thing I do know: the only ones among you who will be really happy are those who have sought and found how to serve.
        </q>
        <br />
        <br />
        --Albert Schweitzer
        
    </div>
    
    <div class="about">
        <strong>I Want to Serve</strong> is a site that allows you to use the gifts and talents God has given you to serve others.
        Please log in to begin meeting needs and submitting needs of your own.
    </div>
</asp:Content>
