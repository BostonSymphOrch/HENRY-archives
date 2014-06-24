<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchHistory.aspx.cs" Inherits="BSO.Archive.WebApp.SearchHistory" %>

<%@ Register Src="~/Controls/EmailForm.ascx" TagPrefix="uc1" TagName="EmailForm" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

	<div id="historyContainer">

		<h1>My Search History</h1>

		<asp:Panel ID="searchHistoryContent" CssClass="searchHistoryContent" runat="server">
			<asp:ListView ID="historyListView" runat="server" OnItemDataBound="PerformanceListView_ItemDataBound">
				<LayoutTemplate>
					<table class="performanceSearchResults" runat="server">
						<tr>
							<td class="headerText">Performance Searches
							</td>
						</tr>
						<tr id="itemPlaceholder" runat="server"></tr>
					</table>
				</LayoutTemplate>
				<ItemTemplate>

					<asp:Label ID="searchEntry" runat="server" />
					<div style="display: none;">
						<%#Eval("SearchID") %>
					</div>
					<tr>
						<td class="Artist">
						    <asp:HyperLink runat="server" class="performanceHistoryItemLink" ID="performanceHistoryLink"></asp:HyperLink>
							<asp:Label ID="perfomanceHistoryEntry" runat="server" />
						</td>
					</tr>
					<tr>
						<td class="date">
							<asp:Label ID="searchTimeEntry" runat="server" />
							|
							<a href="#" class="shareResults" data-id='<%#Eval("SearchID") %>' data-type="Performance">share results</a>

							<%--<asp:LinkButton ID="shareLink" runat="server">SHARE RESULTS</asp:LinkButton>--%>
						</td>

					</tr>


				</ItemTemplate>
			</asp:ListView>

			<asp:ListView ID="artistListView" runat="server" OnItemDataBound="ArtistListView_ItemDataBound">
				<LayoutTemplate>
					<table id="Table1" class="artistResults" runat="server">
						<tr>
							<td class="headerText">Artist Searches
							</td>
						</tr>
						<tr id="itemPlaceholder" runat="server"></tr>
					</table>
				</LayoutTemplate>
				<ItemTemplate>

					<asp:Label ID="searchEntry" runat="server" />
					<div style="display: none;">
						<%#Eval("SearchID") %>
					</div>
					<tr>
						<td class="Artist">
						    <asp:HyperLink runat="server" class="artistHistoryItemLink" ID="artistHistoryLink"></asp:HyperLink>
							<asp:Label ID="artistHistoryEntry" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="date">
							<asp:Label ID="searchTimeEntry" runat="server"></asp:Label>
						
							<a href="#" class="shareResults" data-id='<%#Eval("SearchID") %>' data-type="Artist">share results</a>
							<%--<asp:LinkButton ID="shareLink" runat="server">SHARE RESULTS</asp:LinkButton>--%>
						</td>
					</tr>

				</ItemTemplate>
			</asp:ListView>

			<asp:ListView ID="repertoireListView" runat="server" OnItemDataBound="RepertoireListView_ItemDataBound" DataKeyNames="SearchID">
				<LayoutTemplate>
					<table id="Table1" class="repertoireResults" runat="server">
						<tr>
							<td class="headerText">Repertoire Searches
							</td>
						</tr>
						<tr id="itemPlaceholder" runat="server"></tr>
					</table>
				</LayoutTemplate>
				<ItemTemplate>

					<asp:Label ID="repertoireEntry" runat="server" />
					<div style="display: none;">
						<%#Eval("SearchID") %>
					</div>
					<tr>
						<td class="Artist">
						    <asp:HyperLink runat="server" class="repertoireHistoryItemLink" ID="repertoireHistoryLink"></asp:HyperLink>
							<asp:Label ID="repertoireHistoryEntry" runat="server"></asp:Label>
						</td>
						<tr />
					<tr>
						<td class="date">
							<asp:Label ID="searchTimeEntry" runat="server"></asp:Label>
							|
							<a href="#" class="shareResults" data-id='<%#Eval("SearchID") %>' data-type="Repertoire">share results</a>
							<%--<asp:LinkButton ID="share" runat="server">SHARE RESULTS</asp:LinkButton>--%>
						</td>
					</tr>

				</ItemTemplate>
			</asp:ListView>
				
			<uc1:EmailForm runat="server" ID="EmailForm"  />
		</asp:Panel>
	</div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="plBeforeBodyClose" runat="server">
	<script type="text/javascript">
	    
	</script>
</asp:Content>
