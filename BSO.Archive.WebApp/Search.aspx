<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="BSO.Archive.WebApp.Search" %>

<%@ Register Src="~/Controls/PerformanceSearch.ascx" TagPrefix="uc" TagName="PerformanceSearch" %>
<%@ Register Src="~/Controls/ArtistSearch.ascx" TagPrefix="uc" TagName="ArtistSearch" %>
<%@ Register Src="~/Controls/RepertoireSearch.ascx" TagPrefix="uc" TagName="RepertoireSearch" %>
<%@ Register Src="~/Controls/EmailForm.ascx" TagPrefix="uc" TagName="EmailForm" %>
<%@ Register Src="~/Controls/SearchInfo.ascx" TagPrefix="uc" TagName="SearchInfo" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    

    <asp:Panel runat="server" ID="performanceBasicSearch">
        <div id="tab-container" class="tab-container">

            <ul class="etabs">
                <li id="performanceTab" class="tab first_tab"><a href="#tabs-performance">Performance Search</a></li>
                <li id="artistTab" class="tab"><a href="#tabs-artist">Artist Search</a></li>
                <li id="repertoireTab" class="tab"><a href="#tabs-repertoire">Repertoire Search</a></li>
            </ul>

            <section id="tabs-performance" class="clearfix">
                <uc:PerformanceSearch runat="server" ID="PerformanceSearch" />
            </section>

            <section id="tabs-artist">
                <uc:ArtistSearch runat="server" ID="ArtistSearch" />
            </section>

            <section id="tabs-repertoire">
                <uc:RepertoireSearch runat="server" ID="RepertoireSearch" />
            </section>
        </div>

        <asp:HiddenField runat="server" ID="SearchID" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="MaxSearchResults" ClientIDMode="Static" />
        <iframe id="excelFrame" style="display: none;"></iframe>

        <uc:EmailForm runat="server" ID="EmailForm" />
    </asp:Panel>
    <script type="text/javascript">
        $(".dateField").datepicker({
            yearRange: '1881:c',
            changeMonth: true,
            changeYear: true
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.advancedSearch .workPremiere').each(function () {
                return false;
            })
        });
    </script>


    <uc:SearchInfo runat="server" ID="SearchInfo" />
</asp:Content>
