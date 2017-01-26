HENRY-archive: Event Performance History Database
======
"HENRY" is the Boston Symphony Orchestra's Performance History Search module, which 
contains all documented concerts of the orchestra beginning with October 21, 1881 through the 
current season. It is named after the BSO's founder, Henry Lee Higginson. The search function 
provides access to the performance history of every work, and of all artists - conductors 
ensembles, and soloists - who have performed with the orchestra.
You will most likely find it under [http://archives.bso.org/](http://archives.bso.org/).  Please tell us about your experience or how we can make this application better by emailing archives@bso.org

## Technology
* ASP.Net 4.5.2
* C#
* BackboneJS
* HandlebarsJS
* UndersoreJS
* HeadJS
* jQuery
* jQuery UI
* Font-Awesome

## Solution Overview
The solution is comprised of seven project 
developed in Visual Studio. The projects ending in .Test 
are the unit tests for the corresponding projects. 
Adage.EF contains custom classes and interfaces. 
Bso.Archive.BusObj and BSO.Archive.DTO are class 
projects, while BSO.Archive.WebApp is an ASP.NET 
Web Forms Application. The discussion in this 
document assumes use of Visual Studio for code 
management.

## Database
The database that holds the performance history records
is modeled after the XML file that holds the information. Each
parent tag has its own table with a foreign key relationship to its
child. As an example, every Event has works, every work has a
composer.

The database is represented by an Object Relational 
Mapping using Microsoft.NET Entity Framework 4.0
the Bso.Archive.BusObj project in the solution. This project also
contains the code required for running the import of data from
the xml file to the database. Within the Editable folder, there are
class files which represent every table in the database in it
contains customized code to read that tag within the xml for that
particular node. The ImportOpasData class file within that same project contains the
initialization for the import process. From there the nodes are pulled from the XML file and sent
to their individual class files to be read and added to the database.

The database also contains a table named OPASUpdate, which is used in the update
database process. This table contains columns for the table name, column name and tag name of
the value to be updated. It also contains a Boolean value to indicate whether or not that entry has
been update already. In the Bso.Archive.BusObj Editable folder, the OPASUpdate class file
contains the code for the update process.The update process reads from an update table
XML file that is in the same format as the import file. The update process updates the database
based on the new values in the XML.

## Sample Data
Within the Bso.Archive.BusObj project, there is a folder 
labeled "ProjectFiles". Within here you will find two sample
files. SampleXML.XML is an XML file that can be used to test
the import process. The file contains sample data that is correctly
formatted to work with the importer. There is also
ArchiveSampleDatabase.bak file. This is a backup of the
database that has been populated by running the import process
on SampleXML.XML. Creating a new database by restoring
from this file will allow you test all the functionality without
having to manually build the database. Information on restoring
a database from a backup can be found at
http://msdn.microsoft.com/en-us/library/ms177429.aspx.

## Source Code
The BSO.Archive.WebApp project within the solution contains the code for the archive
site itself. Within the Controls folder, the Performance, Artist, and Repertoire controls each
represent one of the tabs on the Performance History Search page. When a search is run, the
values in the text fields are used as search expressions to query specific columns in the database.
The Performance Search includes within its results links to a detailed description page. This page
provides more detailed information of the specific event that is not included in the results table.
The results from Performance search also provide a link to the PDF image of the original
program book for that event.

There are two tables within the database that are used for searching
and the ArtistDetail table. These tables were created using the CreateArchiveSearchTables stored
procedure and were designed to be single table representations of all the imported data tables that
the different search types could potentially need to query from. The query returns a list of either
EventDetailIds or ArtistDetailIds, depending on the search type.  On the client side, a series of
asynchronous calls are made from JavaScript using these Ids to create data transfer objects
(DTOs) which are representations of the data that the client can more easily display.

The class files for DTOs are within the BSO.Archive.DTO project. They use the Ids to
query the database for specific data and build the objects to return back to the client.  The client
side technology utilizes Backbone.js and uses models to represent both the search results and the 
search filters. JavaScript asynchronously loads event DTOs using event detail ids generated from
the server. They are displayed in the table and ordered using filters and sorted generated from the
file in filterConfig in the /js/app/config folder. FilterConfigs are objects returning true or false for
whether or not events passed into them apply, and SortConfig returns -1/0/1 (should be sorted
before/same/after) based on the comparison of two key sorting values. The grid results are
reloaded when filters or sorting change.

The performance archive search also provides functionality for saving search history.  The
search history is stored in Cookies and can be displayed using the SearchHistory.aspx page. The
search history page also includes links allowing the user to navigate back to the search page and
regenerate the results. This functionality is also present in the
search results themselves, allowing users to click on linked
text within the search results which will re generate searches
based on that keyword. The search page also provides
functionality for exporting the search result tables to Excel
files.

Within the BSO.Archive.WebApp project there is also
a Web.Config file. This file contains projects applications
settings. These include the file paths for the import and update
processes, URLs for both the site itself and the link to PDF of
the original program book and other general settings.

## Adding Work Document
The OPAS export includes fields for “Work Document” which
represent the recording of a concert. These fields are found in
the \<WorkItem/> section of a record for a concert. A full
concert record is enclosed with \<eventItem /> tags, so the
\<WorkItem/> tags are nested within the \<eventItem /> tags.
The two most relevant fields (into which we are currently
putting data) are \<workDocument_Name/> and \<workDocument_FileLocation/>.
The batch import now reads these fields and imports them in to the HENRY database. A table
was added named WorkDocument to store the corresponding information.
The data stored was:
- Work Id
- Work document name
- Work document notes
- Work document summary
- Work document file location

If the data in \<workDocument_Name/> contains the text "Audio" AND there is data in
\<workDocument_FileLocation/> field, HENRY will display an audio icon on the search results
and work details pages. Clicking on the icon will open a new tab to access the audio where it is
hosted at collections.bso.org website.


## System Requirements
The Archive application runs using .NET Framework 4.5.2. It is hosted using Internet
Information Services (IIS) 7.5. The data is stored using Microsoft SQL Server 2008 R2.
These are the suggested minimal hardware specifications for a consolidated server:
“Consolidated” meaning the server is responsible for both web and back-end SQL duties in a
simple deployment.

1. 1 x CPU Core
2. 4GB Memory
3. 40GB Disk Capacity
4. 1Gbp Network
5. Windows Server 2008R2
6. .NET Framework 4.5.1
7. SQL Server 2008R2 Web Edition or higher

## Contact
* Homepage: http://archives.bso.org/
* e-mail: archives@bso.org
* Twitter: [@bostonsymphony](https://twitter.com/bostonsymphony "bostonsymphony on twitter")

# Licensing and Disclaimer
HENRY is released under GNU General Public License, version 2.
This program is free software; you can redistribute it and/or modify it under the terms of the
GNU General Public License as published by the Free Software Foundation; version 2 of the
License.

This program is distributed in the hope that it will be useful, but WITHOUT ANY
WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR
A PARTICULAR PURPOSE. See the GNU General Public License for more details.
You should have received a copy of the GNU General Public License along with this program; if
not, write to the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA
02110-1301, USA.