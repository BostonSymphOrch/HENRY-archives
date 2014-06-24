<%@ Page Title="Help - " Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="BSO.Archive.WebApp.Help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="showMoreWrapper">
        <div class="info_show">
            <div>
                <h1 style="color: Black; font-size: 16px; font-weight: bold;">PERFORMANCE HISTORY SEARCH HELP</h1>
                <br />
                <br />
                <span style="font-weight: bold;">SEARCH CONVENTIONS</span><br />
                <br />
                <span style="font-weight: bold;">Basic versus Advance Search</span><br />
                By default, the Performance Search offers basic search criteria that have been identified as the most common criteria for searching the performance history.  However, if you require a higher degree of specificity, you can expand your search criteria by clicking on &quot;Show advanced search options.&quot;   This will considerably broaden your search criteria options.  Please review the &quot;Types of Search&quot; section below.<br />
                <br />
                <span style="font-weight: bold;">Large Search Results – </span>If your search retrieves more than 500 records, you will be prompted to narrow your search using the filter results feature or by adding additional search criteria to the search input form.  The default order for search results is chronological. Search results can be sorted based on the results in any of the columns. 
                <br />
                <br />
                <span style="font-weight: bold;">Auto-fill -</span>-- Many (but not all) of the searchable fields are auto-fill.  As you start to enter characters in a particular field, the application will display matching values from the dataset that will appear in the drop down list.  Click on the correct value to populate that search field.
                <br />
                <br />
                <span style="font-weight: bold;">Calendar function— </span>Dates should be entered in the form of <em>mm/dd/yyyy</em>.  If you click on the calendar icon, a calendar will pop up and you can use this to enter beginning and end date information.  If you want to search for a specific date, please use the same date for your start and end date.<br />
                <br />
                <span style="font-weight: bold;">Filter Results Function</span>—The Filter Function allows the user to further refine their search results.  It is located just above the search results grid on the right side. When you click on the Filter Results tab, a number of filter options will appear.  These options are consistent with the form fields available in the basic and advanced search forms.  Filters that are highlighted in dark gray indicate that a filter option is available.  Click on the &quot;+&quot; on a filter to select additional filter options.  When you are done selecting filter options, click on the filter name to apply the filter options. Filters highlighted in purple indicate that a filter has been applied.  Filters that are light gray indicate that the filter is not available based on your search results.  As filter options are selected, the individual options will appear in red buttons just above the search results listing.  You can remove specific filters by clicking on the &quot;-&quot;.<br />
                <br />
                <span style="font-weight: bold;">Export to Excel Function</span><br />
                Search results can be exported to Microsoft Excel by click on the &quot;Export Results (XLS)&quot; link at the top of the search results listing (adjacent to the filter tab).  Click on the link will automatically create an Excel file and download it to your local computer.  To open the file, double click on the file on your computer.  If you do not have Microsoft Excel, there are a number of open source (free) office program available on the Internet (OpenOffice, LibreOffice, NeoOffice).  Alternatively, the Excel file can be uploaded to Google Drive and opened within Google Docs.<br />
                <br />
                <span style="font-weight: bold;">Search History</span><br />
                As you use the application, your searches will be saved in your Search History.  Your Search History can be accessed by clicking on the &quot;My Search History&quot; link at the top of any page.  When you click on thie link, you will see the searches you have performed during you session.  Your searches will be grouped by the search type (performance, artist or repertoire) and listed by the search criteria that you used in each of your searches.  Simply click on the title of the search (in bold letters) to reproduce that search.<br />
                <br />
                <span style="font-weight: bold;">Share Search</span><br />
                In addition to access past searches from your session, you can send your search results to another person via email.  Simply access your Search History using the steps defined above.  For each search history saved, you will see a &quot;Share Results&quot; link.  By clicking on the link, a window will appear that will ask you to specify the sender and recipient of the search results.  Fill out that information along with a brief message (optional) and click the SEND button to send your search results.  The person whom you designated as the recipient will then receive an email with a link to your search.<br />
                <br />
                <span style="font-weight: bold;">Clear Search—</span>Before starting a new search, be sure to clear your current search by clicking on the gray CLEAR SEARCH button located on each search form.<br />
                <br />
                <span style="font-weight: bold;">TYPES OF SEARCH</span><br />
                <span style="font-weight: bold;">PERFORMANCE SEARCH FORM</span><br />
                <br />
                The performance search is the most common search form available in this application.  By default, a basic search form is included in this search that includes the following fields:<br />
                <br />
                <ul>
                    <li>- Composer (auto-fill) - can filter by this criteria.  </li>
                    <li>- Work (auto-fill) - can filter by this criteria.  </li>
                    <li>- Orchestra/Ensemble (auto-fill) - can filter by this criteria.  </li>
                    <li>- Performance Start and End Date (calendar prompt)</li>
                </ul>

                <br />
                The advanced search offers the following fields, in addition to the basic search:<br />
                <br />
                <ul>
                    <li>- Season (auto-fill) - can filter by this criteria.  This refers to the season as defined by the BSO.</li>
                    <li>- Winter seasons typically start in September and end the following April or May. Conductor (auto-fill) - can filter by this criteria.  </li>
                    <li>- Soloist (auto-fill)  - can filter by this criteria.  Major instrumental and vocal soloists in addition to narrators, choruses, string quartets, etc.</li>
                    <li>- Instrument (auto-fill)  - can filter by this criteria.  </li>
                    <li>- Arranger (auto-fill) - can filter by this criteria.  Refers to person or persons responsible for a specific arrangement for the piece of music.</li>
                    <li>- Event  - <em>What is this?</em></li>
                    <li>- Venue (auto-fill) - can filter by this criteria. Best used in conjunction with a city, state, and/or country.</li>
                    <li>- City of Venue (auto-fill) - can filter by this criteria.</li>
                    <li>- State of Venue (auto-fill) - can filter by this criteria.</li>
                    <li>- Country of Venue (auto-fill) - can filter by this criteria.</li>
                    <li>- Premiere Type (drop-down)</li>
                    <li>- Commission Type (drop-down)</li>
                </ul>

                <br />
                <span style="font-style: italic;">NOTE on Diacritical Characters (accented characters): This application has been designed to recognize the unaccented equivalent of accented characters known as diacritical characters.  As such, if you search on the letter &quot;é&quot; using an &quot;e&quot;, your search will find results with both characters. </span>
                <br />
                <br />
                <span style="font-weight: bold;">ARTIST SEARCH FORM</span><br />
                <br />
                Use this search form if you want to search by performer/ensemble and view a summary of repertoire they have performed.  To view details about performances summarized in the results grid, click on the number in the &ldquo;# times&rdquo; column.  This search criteria is then fed into the &ldquo;performance search&rdquo; and you will see the details about the performances. 
                <br />
                <br />
                The artist search is made up of the following fields:<br />
                <br />
                <ul>
                    <li>- Artist/Ensemble (auto-fill) - can filter by this criteria.</li>
                    <li>- Instrument/Role (auto-fill) - can filter by this criteria.  Role can correspond to &quot;conductor&quot; or &quot;vocalist&quot; or &quot;narrator&quot;.</li>
                    <li>- Performance Start and End Date (calendar prompt)</li>
                </ul>

                <br />
                <span style="font-style: italic;">NOTE on Diacritical Characters (accented characters): This application has been designed to recognize the unaccented equivalent of accented characters known as diacritical characters.  As such, if you search on the letter &quot;é&quot; using an &quot;e&quot;, your search will find results with both characters. </span>
                <br />
                <br />
                <span style="font-weight: bold;">REPERTOIRE SEARCH FORM</span><br />
                <br />
                Use this search form if you want a summary of a composer&rsquo;s repertoire represented in the database.   The best way is to first search by composer name and then narrow your search using the filters.  The result form displays the name of the work and the number of times performed.  To view additional performance details about a particular piece and/or to filter/refine your results further, click on the number in the &quot;# times&quot; performed column.  When you do this, the search criteria is fed into the standard performance search and the results will be appear in the result grid.  You can use the filter function to refine your search further.<br />
                <br />
                The repertoire search is made up of the following fields:<br />
                <br />
                <ul>
                    <li>- Composer (auto-fill) - can filter by this criteria.  Additionally, can filter by arranger even though it is not a field leveraged in the search</li>
                    <li>- Work (auto-fill) - can filter by this criteria.</li>
                    <li>- Performance Start and End Date (calendar prompt)</li>
                    <li>- Commission Type (drop-down)</li>
                </ul>

                <br />
                <span style="font-weight: bold;">HELPFUL TIPS</span><br />
                <br />
                If you don&rsquo;t know the correct spelling of composer and/or work, just start typing what you do know.  The auto-fill function displays values that are represented in the dataset.  Select the appropriate value and then continue specifying search criteria as desired.
                <br />
                <br />
                Orchestra/ensemble was included on this initial screen because in addition to Boston Symphony Orchestra concerts, the performance history database also contains information about performances by the Tanglewood Music Center Orchestra, the Boston Symphony Chamber Players, the Boston Pops, and other ensembles that perform under the aegis of the Boston Symphony Orchestra. 
                <br />
                <br />
                If you are only interested in performances by the BSO,  it is  important to include this in your search criteria.  (Currently the majority of the records in the database relate to performances by the Boston Symphony Orchestra, however, as we continue to add information about performances by the Boston Pops and other performers,  it will become more important to limit your in this way.)<br />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="plBeforeBodyClose" runat="server">
</asp:Content>
