<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="BSO.Archive.WebApp.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="errorPage">
        <div>The error has been logged and reported to the development team.</div>
        <div>
            Please try your operation again. If the error persists, please contact site administrator for further assistance.
        </div>
        <div>
            We apologize for the inconvenience.
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="plBeforeBodyClose" runat="server">
</asp:Content>
