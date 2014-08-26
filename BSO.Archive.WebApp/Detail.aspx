<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="BSO.Archive.WebApp.Results" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="MainContainer clearfix">
    <asp:Panel ID="searchResultsInformation" runat="server">

        <div class="pdfImage">
            <a href="http://cdm15982.contentdm.oclc.org/cdm/ref/collection/PROG/id/0" target="_blank" runat="server" id="programBookLink">
                <img class="float_left" src="/images/pdf.png" alt="image of pdf document" runat="server" id="programBookImage" />
                <p class="float_right">
                    VIEW PROGRAM
                    <br />
                    BOOK (PDF)
                </p>
            </a>
        </div> 

            
          <a href="#" class="backToSearch"><i class="icon-circle-arrow-left"></i> Back to Search Results</a>
            <h1 class="clearfix float_left ProgramDetails">Program Details</h1>
      
            <div id="detailsContent" class="clearfix">
                <label class="label">Program</label>
                <asp:ListView ID="ProgramListView" OnItemDataBound="ProgramListView_ItemDataBound" runat="server">
                    <LayoutTemplate>
                        <table id="workItemsTable" clientidmode="static" runat="server">
           
                            <tr id="workItem" runat="server">
                                <th id="composerColumn" runat="server">Composer
                                </th>
                                <th id="workColumn" runat="server">Work
                                </th>
                                <th>Soloist</th>
                                <th>Instrument/Role</th>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" clientidmode="static"></tr>
        
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <div style="display: none;">
                            <%#Eval("WorkID") %>
                        </div>

                        <tr>
                            <td colspan="4">
                       
                                <ul class="programList">

                                       <li><asp:HyperLink runat="server" ID="ComposerFullName" CssClass="detailPageComposerLink" NavigateUrl="~/Search.aspx?searchType=Performance&Composer={0}" /></li>
                                        
                                       <li><asp:HyperLink runat="server" ID="WorkTitle" CssClass="detailPageWorkLink" NavigateUrl="~/Search.aspx?searchType=Performance&Work={0}&Composer={1}" /></li>
                           
                                        <li>
                                            <ul runat="server" id="programArtistList">
                                                <li>
                                                    <asp:HyperLink runat="server" CssClass="detailPageArtistLink" ID="ArtistFullName" NavigateUrl="~/Search.aspx?searchType=Performance&Soloist={0}" />
                                                </li>
                                            </ul>
                                        </li>
                       
                                        <li>
                                            <ul runat="server" id="programRoleList">
                                                <li>
                                                    <asp:HyperLink runat="server" ID="Role" CssClass="detailPageRoleLink" NavigateUrl="~/Search.aspx?searchType=Performance&Instrument={0}" />
                                                </li>
                                            </ul>
                                        </li>

                                 </ul>
                           
                                <p class="notes"><span class="detailTitle"><asp:Label runat="server" Text="Notes:" ID="WorkNotesLabel"/></span> <asp:Label ID="WorkNotes" runat="server" /><br /><br /><span class="detailTitle"><asp:Label runat="server" ID="WorkPremiereLabel" Text="Work Premiere:" /></span><asp:Label ID="WorkPremiere" runat="server" /></p>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:ListView>

                <table id="eventInfoTable">
                    <tr>
                        <td>
                            <label class="label">Date</label></td>
                        <td>
                            <span>
                                <asp:HyperLink runat="server" ID="EventDate" CssClass="detailPageDateLink" NavigateUrl="~/Search.aspx?searchType=Performance&StartTime={0}&EndTime={1}" />
                                <asp:HyperLink runat="server" ID="EventTime" />
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="label">Season</label></td>
                        <td><span>
                            <%--<asp:Label ID="SeasonName" runat="server" />--%>
                            <asp:HyperLink runat="server" ID="SeasonName" CssClass="detailPageSeasonLink" NavigateUrl="~/Search.aspx?searchType=Performance&Season={0}" />

                            </span></td>
                    </tr>
                    <tr>
                        <td>
                            <label class="label">Orchestra/<br />
                                Ensemble</label></td>
                        <td><span>
                            <%--<asp:Label ID="OrchestraName" runat="server" />--%>
                            <asp:HyperLink runat="server" ID="OrchestraName" CssClass="detailPageEnsembleLink" NavigateUrl="~/Search.aspx?searchType=Performance&Orchestra={0}" />
                            </span></td>
                    </tr>
                    <tr>
                        <td>
                            <label class="label">Conductor</label></td>
                        <td><span>
                            <%--<asp:Label ID="ConductorFullName" runat="server" />--%>
                            <asp:HyperLink runat="server" ID="ConductorFullName" CssClass="detailPageConductorLink" NavigateUrl="~/Search.aspx?searchType=Performance&Conductor={0}" />
                            </span></td>
                    </tr>
                    <tr>
                        <td>
                            <label class="label">Venue</label></td>
                        <td><span>
                            <%--<asp:Label ID="VenueName" runat="server" />--%>
                            <asp:HyperLink ID="VenueName" runat="server" CssClass="detailPageVenueLink" NavigateUrl="~/Search.aspx?searchType=Performance&Venue={0}" />
                            </span></td>
                    </tr>
                    <tr>
                        <td>
                            <label class="label">Location</label></td>
                        <td><span>
                            <%--<asp:Label ID="VenueLocation" runat="server" />--%>
                            <asp:HyperLink runat="server" ID="VenueLocation" CssClass="detailPageLocationLink" NavigateUrl="~/Search.aspx?searchType=Performance&City={0}&State={1}&Country={2}" />
                            </span></td>
                    </tr>
                    <tr>
                        <td>
                            <label class="label">Event(s)</label></td>
                        <td><span>
                            <%--<asp:Label ID="EventTitle" runat="server" />--%>
                            <asp:Literal ID="EventTitles" runat="server" />
                            </span></td>
                                               

                    </tr>
                    <tr>
                        <td>
                            <label class="label">Notes</label></td>
                        <td>
                            <asp:Label ID="EventNote" CssClass="eventNoteText" runat="server" /></td>
                    </tr>
                </table>

                <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
                <script>
                    $(function () {
                        $("a").tooltip();
                    });

                    $(".backToSearch").on("click", function () {
                        window.close();
                    });
                </script>
                <style>
                    label
                    {
                        display: inline-block;
                        width: 5em;
                    }
                </style>
            </div>

        
      
    </asp:Panel>
          </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="plBeforeBodyClose" runat="server">
</asp:Content>
