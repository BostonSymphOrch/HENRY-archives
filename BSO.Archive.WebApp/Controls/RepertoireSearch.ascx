<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RepertoireSearch.ascx.cs" Inherits="BSO.Archive.WebApp.Controls.RepertoireSearch" %>

<asp:Panel ID="RepertoireSearchPanel" runat="server" CssClass="panel">
    <label class="top_input">
        <span>Composer</span>
        <asp:TextBox ID="ComposerFullName" runat="server" />
        <%--<juice:Autocomplete runat="server" ID="ComposerAutoComplete" TargetControlID="ComposerFullName" />--%>
        <ajaxToolkit:AutoCompleteExtender ID="acComposerFullName" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="ComposerFullName" 
                    ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetComposers" MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
        <%--<input type="text" name="Artist">--%>
    </label>

    <label class="topRightRep">
        <span>Work</span>
        <asp:TextBox ID="WorkItem" runat="server" />
        <%--<juice:Autocomplete runat="server" ID="WorkAutocomplete" TargetControlID="WorkItem" />--%>
        <ajaxToolkit:AutoCompleteExtender ID="acWorkItem" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="WorkItem" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetWorks" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
        <%--<input type="text" name="Artist">--%>
    </label>

    <label class="artistStart">
        <span>Start</span>
        <asp:TextBox ID="StartDate" CssClass="dateField" runat="server" placeholder="10/21/1881" /><i class="icon-calendar"></i>
        <asp:CompareValidator ID="startDateValidator" runat="server" Type="Date" Operator="DataTypeCheck"
            ControlToValidate="StartDate" ErrorMessage="Please enter a valid start date." Display="None" />
    </label>

    <label class="artistEnd">
        <span>End</span>
        <asp:TextBox ID="EndDate" placeholder="mm/dd/yyyy" CssClass="dateField" runat="server" />
        <i class="icon-calendar"></i>
        <asp:CompareValidator ID="endDateValidator" runat="server" Type="Date" Operator="DataTypeCheck"
            ControlToValidate="EndDate" ErrorMessage="Please enter a valid end date." Display="None" />
    </label>


    <label class="repCommissions">
        <span class="commissions">Commissions</span>
        <asp:DropDownList ID="WorkCommissions" runat="server" DataTextField="Name" DataValueField="Value" CssClass="workCommissions">
        </asp:DropDownList>
    </label>
    <div class="searchButtons  rep">
        <asp:Button runat="server" ID="SearchButton" TabIndex="5" CssClass="RepSearchButton artist_button" ClientIDMode="static" class="float_right" Text="Search" PostBackUrl="~/Search.aspx#tabs-repertoire" />
        <input type="button" class="clearButton rep" id="clearSearchButton" value="Clear Search" />
    </div>

</asp:Panel>

<asp:EntityDataSource runat="server" ID="edcEvents" ConnectionString="name=BsoArchiveEntities"
    DefaultContainerName="BsoArchiveEntities" EntitySetName="EventDetails" EnableFlattening="False">
</asp:EntityDataSource>

<asp:QueryExtender ID="customQuery" runat="server" TargetControlID="edcEvents">
    <asp:SearchExpression DataFields="WorkTitle, WorkAddTitle1" SearchType="Contains">
        <asp:ControlParameter ControlID="WorkItem" />
    </asp:SearchExpression>
    <asp:SearchExpression DataFields="ComposerFullName" SearchType="Contains">
        <asp:ControlParameter ControlID="ComposerFullName" />
    </asp:SearchExpression>
    <asp:RangeExpression DataField="EventDate" MaxType="Inclusive" MinType="Inclusive">
        <asp:ControlParameter ControlID="StartDate" />
        <asp:ControlParameter ControlID="EndDate" />
    </asp:RangeExpression>
    <asp:SearchExpression DataFields="WorkCommission" SearchType="Contains">
            <asp:ControlParameter ControlID="WorkCommissions" />
        </asp:SearchExpression>
</asp:QueryExtender>



<asp:ListView ID="dummyListView" runat="server" EnableViewState="False">
    <ItemTemplate></ItemTemplate>
</asp:ListView>
<asp:HiddenField runat="server" ID="hfRepertoireSearchResultIds" ClientIDMode="Static" EnableViewState="false" />

<asp:Label ID="SearchParameterList" runat="server" CssClass="searchParamList" />
<div data-bb="repertoireFilterDiv" class="filterDiv"></div>
<div data-bb="repertoireTableDiv" class="resultsDiv"></div>
