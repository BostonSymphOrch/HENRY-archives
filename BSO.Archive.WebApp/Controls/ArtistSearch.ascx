<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArtistSearch.ascx.cs" Inherits="BSO.Archive.WebApp.Controls.ArtistSearch" %>

<asp:Panel runat="server" ID="ArtistSearchPanel" CssClass="panel">

    <label class="top_input">
        <span>Artist/
            <br />
            Ensemble
        </span>
        <asp:TextBox ID="EnsembleName" runat="server" />
        <%--<juice:Autocomplete runat="server" ID="EnsembleNameAutocomplete" TargetControlID="EnsembleName" />--%>
        <ajaxToolkit:AutoCompleteExtender ID="acEnsembleName" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="EnsembleName" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetEnsembles"
            MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
    </label>

    <label class="topRightArtist">
        <span>Instrument/
            <br />
            Role
        </span>
        <asp:TextBox ID="Instrument" runat="server" />
        <%--<juice:Autocomplete runat="server" ID="InstrumentAutocomplete" TargetControlID="Instrument" />--%>
        <ajaxToolkit:AutoCompleteExtender ID="acInstrument" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="Instrument" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetEnsembleTypes"
            MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
    </label>

    <label class="artistStart">
        <span>Start</span>
        <asp:TextBox ID="StartDate" CssClass="dateField" runat="server" /><i class="icon-calendar"></i>
        <asp:CompareValidator ID="startDateValidator" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="StartDate"
            Text="*" ErrorMessage="Please enter a valid start date." Display="None" EnableClientScript="true" />
    </label>

    <label class="artistEnd">
        <span>End</span>
        <asp:TextBox ID="EndDate" placeholder="mm/dd/yyyy" CssClass="dateField" runat="server" />
        <i class="icon-calendar"></i>
        <asp:CompareValidator ID="endDateValidator" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="EndDate"
            Text="*" ErrorMessage="Please enter a valid end date." Display="None" />
    </label>

    <%--<asp:Button runat="server" ID="Button1" CssClass="SearchButton" ClientIDMode="static" class="float_right" Text="Search" />
    --%>
    <div class="searchButtons artist">

        <asp:Button runat="server" ID="SearchButton" TabIndex="5" CssClass="ArtistSearchButton artist_button" ClientIDMode="static" class="float_right" Text="Search" PostBackUrl="~/Search.aspx#tabs-artist" />
        <input type="button" class="clearButton rep" id="clearSearchButton" value="Clear Search" />
    </div>

</asp:Panel>

<asp:Label ID="resultCountLabel" runat="server" Text="" />

<asp:EntityDataSource runat="server" ID="edcArtists" ConnectionString="name=BsoArchiveEntities"
    DefaultContainerName="BsoArchiveEntities" EntitySetName="ArtistDetails" EnableFlattening="False">
</asp:EntityDataSource>

<asp:QueryExtender ID="artistQueryExtender" runat="server" TargetControlID="edcArtists">
    <asp:RangeExpression DataField="EventDate" MaxType="Inclusive" MinType="Inclusive">
        <asp:ControlParameter ControlID="StartDate" />
        <asp:ControlParameter ControlID="EndDate" />
    </asp:RangeExpression>
    <asp:SearchExpression DataFields="Instrument1">
        <asp:ControlParameter ControlID="Instrument" />
    </asp:SearchExpression>
    <asp:SearchExpression DataFields="EnsembleName" SearchType="Contains">
        <asp:ControlParameter ControlID="EnsembleName" />
    </asp:SearchExpression>
    <%--<asp:CustomExpression OnQuerying="ArtistQuery">
        <asp:ControlParameter ControlID="ArtistFullName" />
</asp:CustomExpression>
    --%>
</asp:QueryExtender>


<asp:HiddenField runat="server" ID="hfArtistSearchResultIds" ClientIDMode="Static" EnableViewState="false" />

<asp:Label ID="SearchParameterList" runat="server" CssClass="searchParamList" />
<div data-bb="artistFilterDiv" class="filterDiv"></div>
<div data-bb="artistTableDiv" class="resultsDiv"></div>

<div style="display: none;">
    <asp:ListView runat="server" ID="ResultsListView" EnableViewState="false">
        <ItemTemplate></ItemTemplate>
    </asp:ListView>
</div>
