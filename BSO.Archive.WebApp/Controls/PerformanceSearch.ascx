<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PerformanceSearch.ascx.cs" Inherits="BSO.Archive.WebApp.Controls.PerformanceSearch" %>



<div class="border">

    <section id="tabs-performance">
        <asp:Panel runat="server" ID="basicPanel">
            <div class="basicSearch">
                <label class="top_input">
                    <span>Composer</span>
                    <asp:TextBox ID="ComposerFullName" runat="server" />
                <ajaxToolkit:AutoCompleteExtender ID="acComposerFullName" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="ComposerFullName" 
                    ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetComposers" MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                    
                </label>
                <label class="rightColumn">
                    <span>Work</span>
                    <asp:TextBox ID="WorkItem" runat="server" />
                    <%--<juice:Autocomplete runat="server" ID="WorkItemAutoComplete" TargetControlID="WorkItem" />--%>
                  <ajaxToolkit:AutoCompleteExtender ID="acWorkItem" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted"  CompletionListCssClass="autoComplete" TargetControlID="WorkItem" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetWorks" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                </label>

                <label class="leftColumn">
                    <span>Conductor</span>
                    <asp:TextBox ID="ConductorFullName" runat="server" />
                    <%--<juice:Autocomplete runat="server" ID="ConductorAutoComplete" TargetControlID="ConductorFullName" />--%>
                    <ajaxToolkit:AutoCompleteExtender ID="acConductorFullName" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="ConductorFullName" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetConductors" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                </label>

                 <label class="rightColumn">
                    <span>Orchestra/<br>
                        Ensemble</span>
                    <asp:TextBox ID="OrchestraName" runat="server" />
                    <%--<juice:Autocomplete runat="server" ID="OrchestraAutoComplete" TargetControlID="OrchestraName" />--%>
                    <ajaxToolkit:AutoCompleteExtender ID="acOrchestraName" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="OrchestraName" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetOrchestras" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                </label>

                 <label class="bottom_input">
                    <span>Soloist</span>
                    <asp:TextBox ID="Soloist" runat="server" />
                    <%--<juice:Autocomplete runat="server" ID="SoloistAutoComplete" TargetControlID="Soloist" />--%>
                    <ajaxToolkit:AutoCompleteExtender ID="acSoloist" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="Soloist" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetSoloists" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                </label>

               <label class="start">
                    <span>Start</span>
                    <asp:TextBox ID="StartDate" CssClass="dateField" runat="server" /><i class="icon-calendar"></i>
                    <asp:CompareValidator ID="startDateValidator" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="StartDate"
                        Text="*" ErrorMessage="Please enter a valid start date." Display="None" />
                </label>
                <label class="end">
                    <span>End</span>
                    <asp:TextBox ID="EndDate" placeholder="mm/dd/yyyy" CssClass="dateField" runat="server" /><i class="icon-calendar"></i>
                    <asp:CompareValidator ID="endDateValidator" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="EndDate"
                        Text="*" ErrorMessage="Please enter a valid end date." Display="None" />
                </label>
            </div>
        </asp:Panel>


        <asp:Panel runat="server" ID="advancedPanel">
            <div class="input_show advancedSearch">

                <label class="leftColumn">
                    <span>Season</span>
                    <asp:TextBox ID="SeasonName" runat="server" />
                    <%--<juice:Autocomplete runat="server" ID="SeasonAutoComplete" TargetControlID="SeasonName" />--%>
                    <ajaxToolkit:AutoCompleteExtender ID="acSeasonAutoComplete" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="SeasonName" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetSeasons" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                </label>

                <label class="rightColumn">
                    <span>Instrument</span>
                    <asp:TextBox ID="ArtistInstrument" runat="server" />
                    <%--<juice:Autocomplete runat="server" ID="InstrumentAutoComplete" TargetControlID="ArtistInstrument" />--%>
                    <ajaxToolkit:AutoCompleteExtender ID="acArtistInstrument" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="ArtistInstrument" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetArtists" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                </label>

                <label class="leftColumn">
                    <span>Arranger</span>
                    <asp:TextBox ID="WorkArrangement" runat="server" />
                    <%--<juice:Autocomplete runat="server" ID="ArrangerAutoComplete" TargetControlID="WorkArrangement" />--%>
                    <ajaxToolkit:AutoCompleteExtender ID="acWorkArrangement" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="WorkArrangement" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetArrangements" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                </label>

                <label class="rightColumn">
                    <span>Event</span>
                    <asp:TextBox ID="EventTitle" runat="server" />
                    <%--<juice:Autocomplete runat="server" ID="EventTitleAutoComplete" TargetControlID="EventTitle" />--%>
                    <ajaxToolkit:AutoCompleteExtender ID="acEventTitle" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="EventTitle" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetTitles" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                </label>

                <label>
                    <span>Venue</span>
                    <asp:TextBox ID="VenueName" runat="server" />
                    <%--<juice:Autocomplete runat="server" ID="VenueAutoComplete" TargetControlID="VenueName" />--%>
                    <ajaxToolkit:AutoCompleteExtender ID="acVenueName" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="VenueName" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetVenues" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                </label>

                <label>
                    <span>City</span>
                    <asp:TextBox ID="VenueCity" runat="server" CssClass="venueCity" />
                    <%--<juice:Autocomplete runat="server" ID="CityAutoComplete" TargetControlID="VenueCity" />--%>
                    <ajaxToolkit:AutoCompleteExtender ID="acVenueCity" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted"  CompletionListCssClass="autoComplete" TargetControlID="VenueCity" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetCities" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                </label>

                <label class="location state">
                    <span>State</span>
                    <asp:TextBox ID="VenueState" runat="server" CssClass="venueState" />
                    <%--<juice:Autocomplete runat="server" ID="StateAutoComplete" TargetControlID="VenueState" />--%>
                    <ajaxToolkit:AutoCompleteExtender ID="acVenueState" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="VenueState" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetStates" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                </label>

                <label class="location country">
                    <span>Country</span>
                    <asp:TextBox ID="VenueCountry" runat="server" CssClass="venueCountry" />
                    <%--<juice:Autocomplete runat="server" ID="CountryAutoComplete" TargetControlID="VenueCountry" />--%>
                    <ajaxToolkit:AutoCompleteExtender ID="acVenueCountry" runat="server" CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListCssClass="autoComplete" TargetControlID="VenueCountry" ServicePath="~/ArchiveWebService.asmx" ServiceMethod="GetCountries" 
                      MinimumPrefixLength="2" UseContextKey="false" EnableCaching="true" CompletionInterval="200" />
                </label>

                <label class="premiere">
                    <span>Premiere</span>
                    <asp:DropDownList ID="WorkPremiere" runat="server" DataTextField="Name" DataValueField="Value" CssClass="workPremiere">
                    </asp:DropDownList>
                </label>

                <label class="commission">
                    <span>Commission</span>
                    <asp:DropDownList ID="WorkCommissions" runat="server" DataTextField="Name" DataValueField="Value" CssClass="workCommissions">
                    </asp:DropDownList>
                </label>
            </div>
        </asp:Panel>

        <div id="showMore">
            <div id="show_more_image" class="clearfix float_left">
                <span class="search_img">
                    <img src="/Images/showmore.png" alt="triangle button" /></span>
            </div>
            <a class="show_more float_left" tabindex="19" href="#"><span><span class="search_text">Show</span> additional search options</span></a>

        </div>

        <div class="searchButtons performance">


            <asp:Button runat="server" ID="SearchButton" TabIndex="20" CssClass="SearchButton" Text="Search" OnClick="SearchButton_Click" />
            <input type="button" class="clearButton rep" id="clearSearchButton" value="Clear Search" />
        </div>


        <%--<div class="filter_results_button"><a href="#">Filter Results</a></div>--%>
    </section>



    <asp:EntityDataSource runat="server" ID="edcEvents" ConnectionString="name=BsoArchiveEntities"
        DefaultContainerName="BsoArchiveEntities" EntitySetName="EventDetails"
        EnableFlattening="False">
    </asp:EntityDataSource>
    <asp:QueryExtender ID="customQuery" runat="server" TargetControlID="edcEvents">

        <asp:SearchExpression DataFields="WorkPremiere" SearchType="Contains">
            <asp:ControlParameter ControlID="WorkPremiere" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="WorkCommission" SearchType="Contains">
            <asp:ControlParameter ControlID="WorkCommissions" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="ArtistFullName" SearchType="Contains">
            <asp:ControlParameter ControlID="Soloist" />
        </asp:SearchExpression>

        <asp:RangeExpression DataField="EventDate" MaxType="Inclusive" MinType="Inclusive">
            <asp:ControlParameter ControlID="StartDate" />
            <asp:ControlParameter ControlID="EndDate" />
        </asp:RangeExpression>

        <asp:SearchExpression DataFields="WorkTitle, WorkAddTitle1" SearchType="Contains">
            <asp:ControlParameter ControlID="WorkItem" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="ComposerFullName" SearchType="Contains">
            <asp:ControlParameter ControlID="ComposerFullName" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="OrchestraName, ConductorFullName" SearchType="Contains">
            <asp:ControlParameter ControlID="OrchestraName" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="EventProgramTitle" SearchType="Contains">
            <asp:ControlParameter ControlID="EventTitle" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="ConductorFullName" SearchType="Contains">
            <asp:ControlParameter ControlID="ConductorFullName" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="WorkArrangement" SearchType="Contains">
            <asp:ControlParameter ControlID="WorkArrangement" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="Instrument1" SearchType="Contains">
            <asp:ControlParameter ControlID="ArtistInstrument" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="SeasonName" SearchType="Contains">
            <asp:ControlParameter ControlID="SeasonName" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="VenueName" SearchType="Contains">
            <asp:ControlParameter ControlID="VenueName" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="VenueCountry" SearchType="Contains">
            <asp:ControlParameter ControlID="VenueCountry" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="VenueCity" SearchType="Contains">
            <asp:ControlParameter ControlID="VenueCity" />
        </asp:SearchExpression>

        <asp:SearchExpression DataFields="VenueState" SearchType="Contains">
            <asp:ControlParameter ControlID="VenueState" />
        </asp:SearchExpression>
    </asp:QueryExtender>

    <asp:Label ID="SearchParameterList" runat="server" CssClass="searchParamList" />

    <div style="display: none;">
        <asp:ListView runat="server" ID="ResultsListView" DataKeyNames="EventDetailID,EventDate">
            <ItemTemplate>
            </ItemTemplate>
        </asp:ListView>
    </div>
    
    <asp:HiddenField runat="server" ID="hfPerformanceSearchResultIds" ClientIDMode="Static" />
    <div data-bb="performanceFilterDiv">
    </div>

    <div data-bb="performanceTableDiv">
    </div>
</div>

<%----%>

<script type="text/javascript">
    $("#<%=ComposerFullName.ClientID%>").on('change', function () {
        var str = 'hey';
    })

</script>
